using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace BurstTools {
    class BurstOptimizerDual {
        readonly string LOGFILE = "OptimizerDual.txt";
        readonly long SCOOP_SIZE = 64;
        readonly long PLOTSIZE = 262144;

        private SharedData sdata = new SharedData();
        string outputfile = "";
        Thread writeThread = null;

        public string optimize(string inputfile, string outputpath, long memory) {
            try {
                Lib.Logging.log(LOGFILE, string.Format("Start Optimizer: input={0}; output{1}; memory={2:0.#}GB", inputfile, outputpath, memory / 1024d / 1024d / 1024d));
                if (!File.Exists(inputfile)) return "Input file not exists";

                string filename = Path.GetFileName(inputfile);
                string[] pars = filename.Split('_');
                if (pars.Length != 4) return "Inputfile has false name";

                long key = Lib.Converter.toLong(pars[0]);
                long startnonce = Lib.Converter.toLong(pars[1]);
                long nonces = Lib.Converter.toLong(pars[2]);
                long stagger = Lib.Converter.toLong(pars[3]);

                long expectFileSize = nonces * PLOTSIZE;

                if (new FileInfo(inputfile).Length != expectFileSize) return "File not complete";

                if (nonces % stagger != 0) return "Nonces not a multiple of stagger.";

                int blocks = (int)(nonces / stagger);

                if (blocks == 1) return "File already optimized";
                outputfile = outputpath + string.Format(@"{0}_{1}_{2}_{3}", key, startnonce, nonces, nonces);

                int i, j, k;

                byte[][] buffers = new byte[blocks][];

                int ssize = 1;
                long memused;

                for (; ssize > 1; ssize /= 2) {
                    memused = blocks * stagger * SCOOP_SIZE * ssize;
                    if (memused < memory)
                        break;
               }

                for (i = 0; i < blocks; i++) {
                    buffers[i] = new byte[stagger * SCOOP_SIZE * ssize];
                    if (buffers[i] == null) {
                        return "Error allocating memory";
                    }
                }

                lock (sdata) {
                    sdata = new SharedData();
                    sdata.init();
                }

                writeThread = new Thread(startWriteThread);
                writeThread.IsBackground = true;
                writeThread.Start();

                FileStream fin = new FileStream(inputfile, FileMode.Open);

                Lib.Logging.log(LOGFILE, string.Format("Start reading, ssize={0}", ssize));
                long bytes;
                for (i = 0; i < (PLOTSIZE / SCOOP_SIZE); i += ssize) {

                    Lib.Logging.log(LOGFILE, string.Format("Scope {0} of {1}", i, PLOTSIZE / SCOOP_SIZE));
                    for (j = 0; j < blocks; j++) {
                        bytes = 0;
                        do {
                            fin.Seek((j * stagger * PLOTSIZE) + (i * stagger * SCOOP_SIZE) + bytes, SeekOrigin.Begin);
                            int found = fin.Read(buffers[j], 0, (int)((stagger * SCOOP_SIZE * ssize) - bytes));
                            bytes += found;
                        } while (bytes < stagger * SCOOP_SIZE * ssize);
                    }

                    for (k = 0; k < ssize; k++) {
                        for (j = 0; j < blocks; j++) {
                            bytes = 0;
                            do {
                                int count = (int)((stagger * SCOOP_SIZE) - bytes);
                                int wrote = 0;
                                lock (sdata) {
                                    if(sdata.Buffer.Free > count) {
                                        sdata.Buffer.put(buffers[j], (int)(stagger * SCOOP_SIZE * k), count);
                                        wrote = count;
                                        bytes += count;
                                    }
                                }
                                if(wrote == 0) {
                                    Lib.Logging.log(LOGFILE, "buffer is full, wait until there is more space left");
                                    Thread.Sleep(1000);
                                }
                            } while (bytes < stagger * SCOOP_SIZE);
                        }
                    }
                }
                lock (sdata) {
                    sdata.ReadFinish = true;
                }
                fin.Close();
            }catch(Exception ex) {
                Lib.Logging.logException("", ex);
            }finally {
               
            }

            return "finish";
        }

        public void startWriteThread() {
            FileStream fin = null;
            string LOGFILE_WRITE = "writeThread.txt";
            try {
                Lib.Logging.log(LOGFILE_WRITE, string.Format("Start writeThread, outputfile={0}", outputfile));
                if (File.Exists(outputfile)) File.Delete(outputfile);
                fin = new FileStream(outputfile, FileMode.Create);
                while (true) {
                    byte[] writebuf = null;

                    lock (sdata) {
                        if (sdata.Buffer.Filled == 0 && sdata.ReadFinish) break;
                        writebuf = sdata.Buffer.pull();
                    }
                    if (writebuf == null || writebuf.Length <= 0) {
                        Lib.Logging.log(LOGFILE_WRITE, "buffer is empty, wait until there is some data");
                        Thread.Sleep(1000);
                        continue;
                    }
                    Lib.Logging.log(LOGFILE_WRITE, string.Format("write {0:n0} bytes to outputfile", writebuf.Length));
                    fin.Write(writebuf, 0, writebuf.Length);
                }
            }catch(Exception ex) {
                Lib.Logging.logException("writethread error", ex);
            } finally {
                lock (sdata) {
                    sdata.WriteFinish = true;
                }
                if(fin != null) fin.Close();
                Lib.Logging.log(LOGFILE_WRITE, string.Format("writeThread finished"));
            }
            
        }
    }
    class SharedData {
        public ByteBuffer Buffer { get; set; }
        public bool ReadFinish { get; set; }
        public bool WriteFinish { get; set; }

        public void init() {
            this.Buffer = new ByteBuffer(1024 * 1024 * 1024);
            ReadFinish = false;
            WriteFinish = false;
        }
    }
    class ByteBuffer {
        List<byte> data;
        int maxlength = 0;
        public ByteBuffer(int maxlength) {
            data = new List<byte>(maxlength);
            this.maxlength = maxlength;
        }
        public int Filled {
            get { return data.Count; }
        }
        public int Free {
            get { return maxlength - data.Count; }
        }
        public void put(byte[] arr, int off, int length) {
            if (length > this.Free) throw new Exception("Buffer is to small!");
            byte[] add = new byte[length];
            Array.Copy(arr, off, add, 0, length);
            data.AddRange(add);
        }
        public byte[] pull() {
            return this.pull(this.Filled);
        }
        public byte[] pull(int length) {
            if (data.Count < length) throw new Exception("To less data in buffer!");
            byte[] ret = data.GetRange(0, length).ToArray();

            data.RemoveRange(0, length);
            return ret;
        }
    }
}
