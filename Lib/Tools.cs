using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace Lib
{
    public static class JsonExtensions {
        public static bool IsNullOrEmpty(this JToken token) {
            return (token == null) ||
                   (token.Type == JTokenType.Array && !token.HasValues) ||
                   (token.Type == JTokenType.Object && !token.HasValues) ||
                   (token.Type == JTokenType.String && token.ToString() == String.Empty) ||
                   (token.Type == JTokenType.Null);
        }
        public static string formatJObject(JObject ob) {
            return ob.ToString(Newtonsoft.Json.Formatting.None).Replace("\r", "").Replace("\n", "");
        }
    }
    public class Converter
    {
        public static byte[] StringToByteArray(string hex) {
            if (hex.ToLower().StartsWith("0x")) hex = hex.Substring(2);
            byte[] ret =  Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();

            return ret.Reverse().ToArray();
        }
        static public string formatTimeSpan(TimeSpan ts)
        {
            string ret = "";
            if (ts.Days > 0) ret += ts.Days + "d ";
            if (ts.Hours > 0) ret += ts.Hours + "h ";
            if (ts.Minutes > 0) ret += ts.Minutes + "m ";
            if (ts.Seconds > 0) ret += ts.Seconds + "s ";
            if (ts.Milliseconds > 0) ret += ts.Milliseconds + "ms ";
            return ret;
        }
        static public string formatHashrate(decimal hashrate) {
            int dec = 0;
            string[] hash = new string[] { "h/s", "Kh/s", "Mh/s", "Gh/s", "Th/s", "Ph/s" };
            while (hashrate > 1000) {
                dec++;
                hashrate /= 1000;
                if (dec >= hash.Length) break;
            }
            return string.Format("{0:0.##}{1}", hashrate, hash[dec]);
        }
        static public string toString(Object ob) {
            if (ob == null) return "";
            if (ob is DateTime) {
                DateTime dt = (DateTime)ob;
                return dt.ToString(Const.DATE_TIME_FORMAT);
            }
            return String.Format(CultureInfo.InvariantCulture, "{0}", ob);
        }
        static public bool toBool(Object ob) {
            return toBool(ob, false);
        }
        static public bool toBool(Object ob, bool defValue) {
            if (ob == null) return defValue;
            string strval = ob.ToString();
            bool retvalue = defValue;
            string[] trueValues = new string[] { "1", "true"};
            string[] falseeValues = new string[] { "0", "false" };
            for (int i = 0; i < trueValues.Length; i++) {
                if (strval.Equals(trueValues[i], StringComparison.InvariantCultureIgnoreCase)) return true;
                if (strval.Equals(falseeValues[i], StringComparison.InvariantCultureIgnoreCase)) return false;
            }
            if (bool.TryParse(strval, out retvalue)) return retvalue;
            return defValue;
        }
        static public double toDouble(Object ob){
            return toDouble(ob, 0);
        }
        static public double toDouble(Object ob, double defValue) {
            if (ob == null) return defValue;
            string strval = ob.ToString();
            double retvalue = defValue;
            if (double.TryParse(strval, System.Globalization.NumberStyles.Any, Const.INV_CULTURE, out retvalue)) return retvalue;
            return defValue;
        }

        static public int toInt(Object ob) {
            return toInt(ob, 0);
        }
        static public int toInt(Object ob, int defValue) {
            return (int)toLong(ob, defValue);
        }
        static public long toLong(Object ob) {
            return toLong(ob, 0);
        }
        static public long toLong(Object ob, long defValue) {
            if (ob == null) return defValue;
            string strval = ob.ToString();
            long retvalue = defValue;
            if (long.TryParse(strval, System.Globalization.NumberStyles.Any, Const.INV_CULTURE, out retvalue)) return retvalue;
            double dbvl = 0;
            if (double.TryParse(strval, System.Globalization.NumberStyles.Any, Const.INV_CULTURE, out dbvl)) return (long)dbvl;
            return defValue;
        }
        static public decimal toDecimal(Object ob) {
            return toDecimal(ob, 0);
        }
        static public decimal toDecimal(Object ob, decimal defValue) {
            if (ob == null) return defValue;
            string strval = toString(ob);
            decimal retvalue = defValue;
            if (decimal.TryParse(strval, System.Globalization.NumberStyles.Any, Const.INV_CULTURE, out retvalue)) return retvalue;
            return defValue;
        }
        static public DateTime toDateTime(Object ob) {
            return toDateTime(ob, DateTime.MinValue);
        }
        static public DateTime toDateTime(Object ob, DateTime defValue) {
            return toDateTime(ob, DateTime.MinValue, Const.DATE_TIME_FORMAT);
        }
        static public DateTime toDateTime(Object ob, DateTime defValue, String format){
            if (ob == null) return defValue;
            string strval = toString(ob);
            DateTime retvalue = defValue;
            if (format.ToLower().Equals("1970")) return Const.ORIGN_DATE.AddMilliseconds(Lib.Converter.toDouble(strval) * 1000);
            if (DateTime.TryParseExact(strval, format, Const.INV_CULTURE, DateTimeStyles.None, out retvalue)) return retvalue;
            if (DateTime.TryParse(strval, Const.INV_CULTURE, DateTimeStyles.None, out retvalue)) return retvalue;
            return defValue;
        }
        public static string Base64Encode(string plainText) {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        public static string Base64Decode(string base64EncodedData) {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
    public class Tools {
        static public bool checkDoubleEqual(double pa1, double pa2) {
            if (Math.Abs(pa1 - pa2) < Lib.Const.ZERO) return true;
            return false;
        }
        public static long getSeconds1970() {
            return (long)((DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds);
        }
        public static string explode(string splitter, params object[] pas) {
            StringBuilder sb = new StringBuilder();
            foreach (object item in pas) {
                if (sb.Length > 0) sb.Append(splitter);
                sb.Append(Lib.Converter.toString(item));
            }
            return sb.ToString();
        }
        public static decimal roundDecimal(decimal dec) {
            if (dec == 0m) return dec;
            decimal acc = 0.0001m;
            int pot = 0;
            while (dec * 10m > 1m / acc) {
                dec /= 10m;
                pot++;
            }
            while (dec < 1m / acc) {
                dec *= 10m;
                pot--;
            }
            dec = Math.Round(dec);
            while (pot > 0) {
                dec *= 10m;
                pot--;
            }
            while (pot < 0) {
                dec /= 10m;
                pot++;
            }
            return dec;
        }
        static public string seperateCSV(char sep, params Object[] pars) {
            string ret = "";
            foreach (Object item in pars) {
                ret += Converter.toString(item) + sep;
            }
            return ret;
        }
        static public void makeFolders(string filename) {
            string folder = Path.GetDirectoryName(filename);
            if (!Directory.Exists(folder)) {
                Directory.CreateDirectory(folder);
            }
        }
        static public string formatSpace(long space) {
            string[] suffix = new string[] { "b", "kb", "mb", "gb", "tb", "pb" };
            int logsize = 0;
            while (space > 1024) {
                logsize++;
                space /= 1024;
            }
            return string.Format("{0}{1}", space, suffix[logsize]);
        }
    }
    public class EtherConvert {
        public static decimal getEtherFromWei(decimal wei) {
            return wei / pow10(18);
        }

        static public decimal pow10(int t) {
            decimal ret = 1;
            for (int i = 0; i < t; i++) {
                ret *= 10;
            }
            return ret;
        }
    }
    public class Performance
    {
        static private Dictionary<string, MyWatch> watches = new Dictionary<string, MyWatch>();

        static public void resetWatches()
        {
            watches.Clear();
        }

        static public void setWatch(string key, bool on)
        {
            if (key != "total") setWatch("total", on);
            if (!watches.ContainsKey(key)) watches.Add(key, new MyWatch());
            if (on)
            {
                watches[key].Start();
            }
            else
            {
                watches[key].Stop();
            }
        }

        static public DataTable getTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("key");
            dt.Columns.Add("last");
            dt.Columns.Add("total");
            dt.Columns.Add("avarage");
            dt.Columns.Add("avarage_ticks");
            dt.Columns.Add("count");

            foreach (KeyValuePair<string, MyWatch> entry in watches)
            {
                DataRow dr = dt.NewRow();
                dr["key"] = entry.Key;
                dr["last"] = Lib.Converter.formatTimeSpan(entry.Value.Last);
                dr["total"] = Lib.Converter.formatTimeSpan(entry.Value.Total);
                dr["avarage"] = Lib.Converter.formatTimeSpan(entry.Value.Avarage);
                dr["avarage_ticks"] = entry.Value.Avarage.Ticks;
                dr["count"] = entry.Value.Count;
                dt.Rows.Add(dr);
            }
            return dt;
        }

        private class MyWatch : Stopwatch
        {
            private int count = 0;
            private long laststart = 0;
            private long lastdur = 0;
            public new void Start()
            {
                base.Start();
                this.laststart = base.ElapsedTicks;
            }
            public new void Stop()
            {
                if (base.IsRunning)
                {
                    count++;
                    this.lastdur = base.ElapsedTicks - this.laststart;
                }
                base.Stop();
            }
            public TimeSpan Avarage
            {
                get { return new TimeSpan(base.ElapsedTicks / count); }
            }
            public TimeSpan Total
            {
                get { return new TimeSpan(base.ElapsedTicks); }
            }
            public TimeSpan Last
            {
                get { return new TimeSpan(this.lastdur); }
            }
            public int Count
            {
                get { return count; }
            }
        }
        
    }

    public class Options {
        Dictionary<string, OptionParam> values = new Dictionary<string, OptionParam>();
        string filename = "";

        public Options(string filename) {
            this.filename = filename;
        }

        public void save() {
            JObject ob = new JObject();
            foreach (KeyValuePair<string, OptionParam> entry in values) {
                ob.Add(new JProperty(entry.Key, entry.Value.ToString()));
            }
            StreamWriter fout = new StreamWriter(filename);
            fout.Write(ob.ToString());
            fout.Close();
        }
        public void load() {
            if (!File.Exists(filename)) return;
            StreamReader fin = new StreamReader(filename);
            JObject ob = JObject.Parse(fin.ReadToEnd());
            fin.Close();
            foreach (JProperty item in (JToken)ob) {
                values[item.Name] = new OptionParam(item.Value);
            }
        }
        public void set(object okey, object value) {
            string key = Lib.Converter.toString(okey);
            bool changed;
            if (!values.ContainsKey(key)) {
                changed = true;
                values[key] = new OptionParam(value);
            } else {
                changed = values[key].setValue(value);
            }
            if (changed) save();
        }
        public OptionParam get(object key, object def) {
            return get(Lib.Converter.toString(key), def);
        }
        public OptionParam get(string key) {
            if (!values.ContainsKey(key)) return null;
            return values[key];
        }
        public OptionParam get(string key, object def) {
            if (!values.ContainsKey(key)) return new OptionParam(def);
            return values[key];
        }
        public string[] AllKeys {
            get {
                return values.Keys.ToArray();
            }
        }

        public class OptionParam {
            string value = "";

            public OptionParam(object val) {
                this.value = Converter.toString(val);
            }
            public string Value {
                get { return value; }
                set { this.value = value; }
            }

            public int IntValue {
                get { return Converter.toInt(value); }
            }
            public long LongValue {
                get { return Converter.toLong(value); }
            }
            public decimal DecimalValue {
                get { return Converter.toDecimal(value); }
            }
            public bool BoolValue {
                get { return Converter.toBool(value); }
            }
            public bool setValue(object ob) {
                string newvalue = "";
                if (ob == null) {
                    newvalue = "";
                } else {
                    newvalue = Converter.toString(ob);
                }
                bool changed = !newvalue.Equals(value);
                value = newvalue;
                return changed;
            }
            public override string ToString() {
                return value;
            }
        }
    }
    public class MyThreadPool {
        static Dictionary<string, Thread> th = new Dictionary<string, Thread>();

        static public void add(string name, Thread thread) {
            thread.Name = name;
            if (th.ContainsKey(name)) {
                th[name].Abort();
                th.Remove(name);
            }
            th.Add(name, thread);
        }
        static public void cancelAll() {
            foreach (Thread item in th.Values) {
                item.Abort();
            }
        }
    }
}
