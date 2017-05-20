using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BurstTools {
    class BurstOptimizer {
        readonly string LOGFILE = "Optimizer.txt";
        readonly long SCOOP_SIZE = 64;
        readonly long PLOTSIZE = 262144;

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
                string newFilename = outputpath + string.Format(@"{0}_{1}_{2}_{3}", key, startnonce, nonces, nonces);

                int i, j, k;

                byte[][] buffers = new byte[blocks][];

                int ssize = 4096;
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

                FileStream fin = new FileStream(inputfile, FileMode.Open);
                if (File.Exists(newFilename)) File.Delete(newFilename);
                FileStream fout = new FileStream(newFilename, FileMode.CreateNew);

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
                                fout.Write(buffers[j], (int)(stagger * SCOOP_SIZE * k), count);
                                bytes += count;
                            } while (bytes < stagger * SCOOP_SIZE);
                        }
                    }
                }
                fin.Close();
                fout.Close();
            }catch(Exception ex) {
                Lib.Logging.logException("", ex);
            }finally {
               
            }

            return "finish";
        }
    }
}
