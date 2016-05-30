using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;

namespace HttpVehicle
{
    ///<summary>
    /// The Http Engine
    /// </summary>
    public partial class HttpEngine
    {
        //HEAD
        [Serializable]
        private class MemorySet
        {
            public string _FileName = IO.iDatestamp;

            public CookieContainer _Jar;
            public List<object> _Results;
            public List<object> _Storage;
            public string _Log;

            public Dictionary<string, object> _Map;
            public Dictionary<string, string> _AdditionalHeaders;   //autoresetable

            public string _UserAgent;
            public WebProxy _Proxy;
            public int _Timeout;
        }
        MemorySet Memory = new MemorySet();

        ///<summary>
        /// The log for this run of the engine.
        ///</summary>
        public string Log
        {
            get
            {
                if (Memory._Log != null)
                {
                    string log = "";
                    lock (Memory._Log)
                    {
                        log = Memory._Log;
                    }
                    return log;
                }
                else return null;
            }
            private set
            {
                if (Memory._Log != null)
                {
                    lock (Memory._Log)
                    {
                        Memory._Log = value;
                    }
                }
                else { Memory._Log = value; }
            }
        }








        private string FileName
        {
            get
            {
                return Memory._FileName;
            }
        }
        private CookieCollection Jar
        {
            get
            {
                return ParsersExtractors.ExtractAllCookies(Memory._Jar);
            }
        }
        private List<object> Results
        {
            get
            {
                return Memory._Results;
            }
        }
        private List<object> Storage
        {
            get
            {
                return Memory._Storage;
            }
        }
        private string UserAgent
        {
            get
            {
                return Memory._UserAgent;
            }
        }
        private string Proxy
        {
            get
            {
                if (Memory._Proxy == null) return "";
                return (Memory._Proxy.Credentials == null) ?
                    string.Format("{0}:{1}", Memory._Proxy.Address.Host.ToString(), Memory._Proxy.Address.Port.ToString()) :
                    string.Format("{0}:{1}:{2}", Memory._Proxy.Address.Host.ToString(), Memory._Proxy.Address.Port.ToString(), Memory._Proxy.Credentials.ToString());
            }
        }
        private int Timeout
        {
            get
            {
                return Memory._Timeout;
            }
        }









        //INNER
        private object LastElement(IList collection)
        {
            if (collection != null && collection.Count > 0)
            {
                return collection[collection.Count - 1];
            }
            return null;
        }

        private void Map(string key, object obj)
        {
            if (key != "") { Memory._Map.Add(key, obj); }
        }
        private object Mapper(string key)
        {
            if (Memory._Map.ContainsKey(key))
            {
                return Memory._Map[key];
            }
            else if (key.ToLower() == "result")
            {
                return Memory._Results[Memory._Results.Count - 1];
            }
            else if (key.ToLower() == "storage")
            {
                return Memory._Storage[Memory._Storage.Count - 1];
            }
            else if (key.ToLower().Contains("result"))
            {
                int n = Convert.ToInt32(key.ToLower().Split(new string[] { "result" }, StringSplitOptions.None)[1]);
                return Memory._Results[n];
            }
            else if (key.ToLower().Contains("storage"))
            {
                int n = Convert.ToInt32(key.ToLower().Split(new string[] { "storage" }, StringSplitOptions.None)[1]);
                return Memory._Storage[n];
            }
            else return null;
        }
        private void AddResult(object input, string key = "")
        {
            if (key != "" && Memory._Map.ContainsKey(key))
            {
                object o = Mapper(key);
                o = input;
            }
            else
            {
                Memory._Results.Add(input);
                Map(key, LastElement(Memory._Results));
            }
        }
        private void AddStorage(object input, string key = "")
        {
            if (key != "" && Memory._Map.ContainsKey(key))
            {
                object o = Mapper(key);
                o = input;
            }
            else
            {
                Memory._Storage.Add(input);
                Map(key, LastElement(Memory._Storage));
            }
        }

        private void LogLine(string line)
        {
            Log += line;
            if (_ConsoleLog) Console.WriteLine(line);
        }
        private bool Initial(string fName)
        {
            if (Log != null && Log != "") Log += Environment.NewLine;
            Log += fName + " :";
            if (_ConsoleLog) Console.Write(fName + " :");

            if (Memory._Results != null && Memory._Results.Count > 0)
            {
                bool lackErrBefore = Validators.ValidateLackOfError(Memory._Results);
                if (_ErrorOnly) lackErrBefore = !lackErrBefore;
                if (!lackErrBefore)
                {
                    LogLine(" Encountered error before!");
                    return false;
                }
            }
            return true;
        }
        private HttpEngine Exceptional(Exception ex)
        {
            if (_IgnoreError == false) Memory._Results.Add("error");
            LogLine(" " + ex.Message.Replace(Environment.NewLine, "! "));
            return this;
        }
        private HttpEngine Fault(string errMessage = "")
        {
            if(_IgnoreError == false) Memory._Results.Add("error");
            if (errMessage != "" && !string.IsNullOrEmpty(errMessage) && !string.IsNullOrWhiteSpace(errMessage))
            {
                LogLine(" Error -> " + errMessage);
            }
            else
            {
                LogLine(" Error");
            }
            return this;
        }
        private HttpEngine Success(string okMessage = "")
        {
            if (okMessage != "" && !string.IsNullOrEmpty(okMessage) && !string.IsNullOrWhiteSpace(okMessage))
            {
                LogLine(" Ok -> " + okMessage);
            }
            else
            {
                LogLine(" Ok");
            }
            return this;
        }
    }
}
