using System;
using System.IO;
using System.Reflection;
using System.Threading;

namespace HttpVehicle
{
    public partial class HttpEngine
    {
        bool _ConsoleLog = false;
        bool _IgnoreError = false;
        bool _ErrorOnly = false;



        ///<summary>
        /// Output log to the console.
        ///</summary>
        public HttpEngine CONSOLE_LOG(ConsoleColor fore = ConsoleColor.DarkCyan)
        {
            Console.ForegroundColor = fore;
            _ConsoleLog = true;
            if (Log != null && Log != "") Console.Write(Log + Environment.NewLine);
            return this;
        }

        ///<summary>
        /// Use around functions that are not critical.
        ///</summary>
        public HttpEngine IGNORE_ERROR()
        {
            if (_IgnoreError == true) _IgnoreError = false;
            else _IgnoreError = true;
            return this;
        }

        ///<summary>
        /// Clear after error.
        ///</summary>
        public HttpEngine UNDO_ERROR()
        {
            object lastRes = LastElement(Memory._Results);
            if (lastRes is string && lastRes as string == "error")
            {
                Memory._Results.Remove(lastRes);
            }
            return this;
        }

        ///<summary>
        /// Use to fix error.
        ///</summary>
        public HttpEngine ERROR_ONLY()
        {
            if (_ErrorOnly == true) _ErrorOnly = false;
            else _ErrorOnly = true;
            return this;
        }

        //PERSIST


        ///<summary>
        /// If Console logging is enabled, wait for user to press a key.
        ///</summary>
        public HttpEngine PRESS_ANY_KEY()
        {
            if (_ConsoleLog)
            {
                LogLine(Environment.NewLine + "Press any key to continue!");
                Console.ReadLine();
            }
            return this;
        }




        ///<summary>
        /// Output log to the console.
        ///</summary>
        public string pExepath
        {
            get { return IO.iExepath; }
        }

        ///<summary>
        /// The default save directory path.
        ///</summary>
        public string pSavepath
        {
            get { return IO.iSavedir; }
        }

        ///<summary>
        /// String representing current datestamp.
        ///</summary>
        public string pDatestamp
        {
            get { return IO.iDatestamp; }
        }

        ///<summary>
        /// String representing the proxy address.
        ///</summary>
        public string pProxyAddress
        {
            get
            {
                return Memory._Proxy == null ? "127-0-0-1" : Memory._Proxy.Address.Host.Replace('.','-');
            }
        }
    }
}
