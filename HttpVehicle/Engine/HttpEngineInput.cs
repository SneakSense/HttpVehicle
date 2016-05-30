using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;

namespace HttpVehicle
{
    public partial class HttpEngine
    {

        ///<summary>
        /// Resets the Map field that contains all keys that have been setted.
        ///</summary>
        public HttpEngine ResetMap()
        {
            if (!Initial("ResetMap()")) return this;
            try
            {
                Memory._Map = new Dictionary<string, object>();
                return Success();
            }
            catch (Exception ex)
            {
                return Exceptional(ex);
            }
        }

        ///<summary>
        /// Resets the FileName field to a string representing a timestamp.
        ///</summary>
        public HttpEngine ResetFileName()
        {
            if (!Initial("ResetFileName()")) return this;
            try
            {
                Memory._FileName = IO.iDatestamp;
                return Success();
            }
            catch (Exception ex)
            {
                return Exceptional(ex);
            }
        }

        ///<summary>
        /// Resets the CookieContainer for this instance of the engine.
        ///</summary>
        public HttpEngine ResetJar()
        {
            if (!Initial("ResetJar()")) return this;
            Memory._Jar = new CookieContainer();
            return Success();
        }

        ///<summary>
        /// Resets the Results shelf for this instance of the engine.
        ///</summary>
        public HttpEngine ResetResults()
        {
            if (!Initial("ResetResults()")) return this;
            try
            {
                //remove from map
                List<string> toRemove = new List<string>();
                if (Results != null)
                {
                    foreach (object o in Memory._Results)
                    {
                        foreach (KeyValuePair<string, object> kvp in Memory._Map)
                        {
                            if (o == kvp.Value) toRemove.Add(kvp.Key);
                        }
                    }
                    foreach (string s in toRemove)
                    {
                        Memory._Map.Remove(s);
                    }
                }

                Memory._Results = new List<object>() { "Initialized" };
                return Success();
            }
            catch (Exception ex)
            {
                return Exceptional(ex);
            }
        }

        ///<summary>
        /// Resets the Storage shelf for this instance of the engine.
        ///</summary>
        public HttpEngine ResetStorage()
        {
            if (!Initial("ResetStorage()")) return this;
            try
            {
                if (Memory._Storage != null)
                {
                    List<string> toRemove = new List<string>();
                    foreach (object o in Memory._Storage)
                    {
                        foreach (KeyValuePair<string, object> kvp in Memory._Map)
                        {
                            if (o == kvp.Value) toRemove.Add(kvp.Key);
                        }
                    }
                    foreach (string s in toRemove)
                    {
                        Memory._Map.Remove(s);
                    }
                }


                Memory._Storage = new List<object>();
                return Success();
            }
            catch (Exception ex)
            {
                return Exceptional(ex);
            }
        }

        ///<summary>
        /// Resets the Log for this instance of the engine.
        ///</summary>
        public HttpEngine ResetLog()
        {
            if (!Initial("ResetLog()")) return this;
            try
            {
                Log = "";
                LogLine("ResetLog() : Ok");
                return this;
            }
            catch (Exception ex)
            {
                return Exceptional(ex);
            }
        }

        ///<summary>
        /// Resets the User agent to 'null' for this instance of the engine.
        ///</summary>
        public HttpEngine ResetUserAgent()
        {
            if (!Initial("ResetUserAgent()")) return this;
            try
            {
                Memory._UserAgent = null;
                return Success();
            }
            catch (Exception ex)
            {
                return Exceptional(ex);
            }
        }

        ///<summary>
        /// Resets the Proxy to 'null' for this instance of the engine.
        ///</summary>
        public HttpEngine ResetProxy()
        {
            if (!Initial("ResetProxy()")) return this;
            try
            {
                Memory._Proxy = null;
                return Success();
            }
            catch (Exception ex)
            {
                return Exceptional(ex);
            }
        }

        ///<summary>
        /// Resets Results and Storage shelfs, as those are not needed when saving the file.
        ///</summary>
        public HttpEngine ResetStripToPersist()
        {
            if (!Initial("ResetStripToPersist()")) return this;
            ResetResults();
            ResetStorage();

            Memory._AdditionalHeaders = null;

            return this;
        }





        ///<summary>
        /// Provides a random UserAgent to be used with this instance of the engine.
        ///</summary>
        public HttpEngine ProvideUserAgent()
        {
            if (!Initial("ProvideUserAgent()")) return this;
            try
            {
                Memory._UserAgent = Informers.CreateUserAgent();
                return Success(Memory._UserAgent);
            }
            catch (Exception ex)
            {
                return Exceptional(ex);
            }
        }

        ///<summary>
        /// Provides a random password string and stores it.
        ///</summary>
        ///<param name="key">
        /// The key to be assigned to.
        ///</param>
        public HttpEngine ProvidePassword(string key = "")
        {
            if (!Initial("ProvidePassword()")) return this;
            try
            {
                string dt = ParsersExtractors.ParseUrlEncode(Informers.CreatePassword());
                AddStorage(dt, key);
                return Success(dt);
            }
            catch (Exception ex)
            {
                return Exceptional(ex);
            }
        }

        ///<summary>
        /// Provides a random @gmail email string and stores it.
        ///</summary>
        ///<param name="key">
        /// The key to be assigned to.
        ///</param>
        public HttpEngine ProvideEmail(string key = "")
        {
            if (!Initial("ProvideEmail()")) return this;
            try
            {
                string dt = Informers.CreateEmail();
                AddStorage(dt, key);
                return Success(dt);
            }
            catch (Exception ex)
            {
                return Exceptional(ex);
            }
        }

        ///<summary>
        /// Provides a random UserName string and stores it.
        ///</summary>
        ///<param name="key">
        /// The key to be assigned to.
        ///</param>
        public HttpEngine ProvideUserName(string key = "")
        {
            if (!Initial("ProvideUserName()")) return this;
            try
            {
                string dt = Informers.CreateUserName();
                AddStorage(dt, key);
                return Success(dt);
            }
            catch (Exception ex)
            {
                return Exceptional(ex);
            }
        }

        ///<summary>
        /// Provides a random Hex string and stores it.
        /// For mocking fingerprints e.g.
        ///</summary>
        ///<param name="lenght">
        /// The lenght of the string.
        ///</param>
        ///<param name="key">
        /// The key to be assigned to.
        ///</param>
        public HttpEngine ProvideRandomHex(int lenght, string key = "")
        {
            if (!Initial("ProvideRandomHex()")) return this;
            try
            {
                string dt = Informers.CreateRandHex(lenght);
                AddStorage(dt, key);
                return Success(dt);
            }
            catch (Exception ex)
            {
                return Exceptional(ex);
            }
        }





        ///<summary>
        /// Loads previously saved engine state from file.
        ///</summary>
        ///<param name="fileName">
        /// The name of the file to load from, or the path to that file.
        /// If a name is given, the file will be expected to be in "[CurrentDir]\Save".
        ///</param>
        public HttpEngine Load(string fileName)
        {
            if (!Initial("Load()")) return this;
            try
            {
                if (Validators.ValidateFileExists(fileName) == false)
                {
                    fileName = IO.iExepath + @"\Save\" + fileName;
                }
                if (Validators.ValidateFileExists(fileName) == false)
                {
                    LogLine(String.Format(" Invalid file name -> {0}", fileName));
                }

                using (Stream stream = File.Open(fileName, FileMode.Open))
                {
                    MemorySet deserialized = (MemorySet)new BinaryFormatter().Deserialize(stream);
                    Memory._FileName = deserialized._FileName;
                    Memory._Jar = deserialized._Jar;
                    Memory._Proxy = deserialized._Proxy;
                    Memory._Timeout = deserialized._Timeout;
                    Memory._UserAgent = deserialized._UserAgent;
                }
                return Success(fileName);
            }
            catch (Exception ex)
            {
                return Exceptional(ex);
            }
        }

        ///<summary>
        /// Loads text file in Results.
        ///</summary>
        ///<param name="fileName">
        /// The name of the file to be loaded, or the path to that file.
        /// If a name is given, the file will be expected to be in "[CurrentDir]\Save".
        ///</param>
        ///<param name="key">
        /// The key to be assigned.
        /// If already assigned, the coresponding element will be set, instead of creating a new one.
        ///</param>
        public HttpEngine LoadFile(string fileName, string key = "")
        {
            if (!Initial("LoadFile()")) return this;
            try
            {
                if (Validators.ValidateFileExists(fileName) == false)
                {
                    fileName = IO.iExepath + @"\Save\" + fileName;
                }
                if (Validators.ValidateFileExists(fileName) == false)
                {
                    return Fault("Invalid file name!");
                }
                if (Validators.ValidateReadAccess(fileName) == false)
                {
                    return Fault("You don't have read access!");
                }

                string dt = File.ReadAllText(fileName);

                AddResult(dt, key);
                return Success();
            }
            catch (Exception ex)
            {
                return Exceptional(ex);
            }
        }

        ///<summary>
        /// Loads first line from text file into Storage.
        ///</summary>
        ///<param name="fileName">
        /// The name of the file to load from, or the path to that file.
        /// If a name is given, the file will be expected to be in "[CurrentDir]\Save".
        ///</param>
        ///<param name="key">
        /// The key to be assigned.
        ///</param>
        public HttpEngine PopData(string fileName, string key = "")
        {
            if (!Initial("PopData()")) return this;
            try
            {
                string dt;
                if (Validators.ValidateFileExists(fileName) == false)
                {
                    dt = IO.GetLocaLineFromFile(fileName);
                }
                else
                {
                    dt = IO.GetLineFromFile(fileName);
                }

                AddStorage(dt, key);
                return Success(dt);
            }
            catch (Exception ex)
            {
                return Exceptional(ex);
            }
        }

        ///<summary>
        /// Loads random line from text file into Storage.
        ///</summary>
        ///<param name="fileName">
        /// The name of the file to load from, or the path to that file.
        /// If a name is given, the file will be expected to be in "[CurrentDir]\Save".
        ///</param>
        ///<param name="key">
        /// The key to be assigned.
        ///</param>
        public HttpEngine PopDataRandom(string fileName, string key = "")
        {
            if (!Initial("PopDataRandom()")) return this;
            try
            {
                string dt;
                if (Validators.ValidateFileExists(fileName) == false)
                {
                    dt = IO.GetLocalRandomLineFromFile(fileName);
                }
                else
                {
                    dt = IO.GetRandomLineFromFile(fileName);
                }

                AddStorage(dt, key);
                return Success(dt);
            }
            catch (Exception ex)
            {
                return Exceptional(ex);
            }
        }

        ///<summary>
        /// Sets first line from text file to be used as UserAgent with this instance of the engine.
        ///</summary>
        ///<param name="fileName">
        /// The name of the file to load from, or the path to that file.
        /// If a name is given, the file will be expected to be in "[CurrentDir]\Save".
        ///</param>
        public HttpEngine PopUserAgent(string fileName)
        {
            if (!Initial("PopUserAgent()")) return this;
            try
            {
                string ua;
                if (Validators.ValidateFileExists(fileName) == false)
                {
                    ua = IO.GetLocaLineFromFile(fileName);
                }
                else
                {
                    ua = IO.GetLineFromFile(fileName);
                }

                Memory._UserAgent = ua;
                return Success(ua);
            }
            catch (Exception ex)
            {
                return Exceptional(ex);
            }
        }

        ///<summary>
        /// Sets random line from text file to be used as UserAgent with this instance of the engine.
        ///</summary>
        ///<param name="fileName">
        /// The name of the file to load from, or the path to that file.
        /// If a name is given, the file will be expected to be in "[CurrentDir]\Save".
        ///</param>
        public HttpEngine PopUserAgentRandom(string fileName)
        {
            if (!Initial("PopUserAgentRandom()")) return this;
            try
            {
                string ua;
                if (Validators.ValidateFileExists(fileName) == false)
                {
                    ua = IO.GetLocalRandomLineFromFile(fileName);
                }
                else
                {
                    ua = IO.GetRandomLineFromFile(fileName);
                }

                Memory._UserAgent = ua;
                return Success(ua);
            }
            catch (Exception ex)
            {
                return Exceptional(ex);
            }
        }

        ///<summary>
        /// Loads UserAgent from memory to be used with this instance of the engine.
        ///</summary>
        ///<param name="key">
        /// The key that corresponds to the UserAgent stored in memory.
        /// If not setted, the last stored object in Storage will be used.
        ///</param>
        public HttpEngine LoadUserAgent(string key = "")
        {
            if (!Initial("LoadUserAgent()")) return this;
            try
            {
                string ua = "";
                if (key == "")
                {
                    object o = Memory._Storage[Memory._Storage.Count - 1];
                    if (o is string == false)
                    {
                        return Fault(String.Format("Stored at {0} is not string!", Memory._Storage.Count - 1));
                    }
                    ua = o as string;
                }
                else if (Memory._Map.ContainsKey(key))
                {
                    if (Mapper(key) is string == false)
                    {
                        return Fault(String.Format("Stored at {0} is not string!", key));
                    }
                    ua = Mapper(key) as string;
                }
                Memory._UserAgent = ua;
                return Success(ua);
            }
            catch (Exception ex)
            {
                return Exceptional(ex);
            }
        }

        ///<summary>
        /// Loads Proxy from the first line of text file to be used with this instance of the engine.
        /// The text must be 'colon' delimited, in format - [ip/host]:[port] or [ip/host]:[port]:[username]:[password]
        ///</summary>
        ///<param name="fileName">
        /// The name of the file to load from, or the path to that file.
        /// If a name is given, the file will be expected to be in "[CurrentDir]\Save".
        ///</param>
        public HttpEngine PopProxy(string fileName)
        {
            if (!Initial("PopProxy()")) return this;
            try
            {
                string px;
                if (Validators.ValidateFileExists(fileName) == false)
                {
                    px = IO.GetLocaLineFromFile(fileName);
                }
                else
                {
                    px = IO.GetLineFromFile(fileName);
                }

                string[] sep = px.Split(':');
                if (sep.Count() == 2)
                {
                    Memory._Proxy = new WebProxy(sep[0], Convert.ToInt32(sep[1]));
                    return Success(String.Format("{0}:{1}", sep[0], sep[1]));
                }
                else if (sep.Count() == 4)
                {
                    Memory._Proxy = new WebProxy(sep[0], Convert.ToInt32(sep[1]));
                    Memory._Proxy.Credentials = new NetworkCredential(sep[2], sep[3]);
                    return Success(String.Format("{0}:{1}:{2}:{3}", sep[0], sep[1], sep[2], sep[3]));
                }
                return Fault("Invalid proxy string!");
            }
            catch (Exception ex)
            {
                return Exceptional(ex);
            }
        }

        ///<summary>
        /// Loads Proxy from a random line of text file to be used with this instance of the engine.
        /// The text must be 'colon' delimited, in format - [ip/host]:[port] or [ip/host]:[port]:[username]:[password]
        ///</summary>
        ///<param name="fileName">
        /// The name of the file to load from, or the path to that file.
        /// If a name is given, the file will be expected to be in "[CurrentDir]\Save".
        ///</param>
        public HttpEngine PopProxyRandom(string fileName)
        {
            if (!Initial("PopProxyRandom()")) return this;
            try
            {
                string px;
                if (Validators.ValidateFileExists(fileName) == false)
                {
                    px = IO.GetLocalRandomLineFromFile(fileName);
                }
                else
                {
                    px = IO.GetRandomLineFromFile(fileName);
                }

                string[] sep = px.Split(':');
                if (sep.Count() == 2)
                {
                    Memory._Proxy = new WebProxy(sep[0], Convert.ToInt32(sep[1]));
                    return Success(String.Format("{0}:{1}", sep[0], sep[1]));
                }
                else if (sep.Count() == 4)
                {
                    Memory._Proxy = new WebProxy(sep[0], Convert.ToInt32(sep[1]));
                    Memory._Proxy.Credentials = new NetworkCredential(sep[2], sep[3]);
                    return Success(String.Format("{0}:{1}:{2}:{3}", sep[0], sep[1], sep[2], sep[3]));
                }
                return Fault("Invalid proxy string!");
            }
            catch (Exception ex)
            {
                return Exceptional(ex);
            }
        }

        ///<summary>
        /// Loads Proxy from memory string to be used with this instance of the engine.
        /// The string must be 'colon' delimited, in format - [ip/host]:[port] or [ip/host]:[port]:[username]:[password]
        ///</summary>
        ///<param name="key">
        /// The key that corresponds to the UserAgent stored in memory.
        /// If not setted, the last stored object in Storage will be used.
        ///</param>
        public HttpEngine LoadProxy(string key = "")
        {
            if (!Initial("LoadProxy()")) return this;
            try
            {
                string px = "";
                if (key == "")
                {
                    object o = Memory._Storage[Memory._Storage.Count - 1];
                    if (o is string == false)
                    {
                        return Fault(String.Format("Stored at {0} is not string!", Memory._Storage.Count - 1));
                    }
                    px = o as string;
                }
                else if (Memory._Map.ContainsKey(key))
                {
                    if (Mapper(key) is string == false)
                    {
                        return Fault(String.Format("Stored at {0} is not string!", key));
                    }
                    px = Mapper(key) as string;
                }

                string[] sep = px.Split(':');
                if (sep.Count() == 2)
                {
                    Memory._Proxy = new WebProxy(sep[0], Convert.ToInt32(sep[1]));
                    return Success(String.Format("{0}:{1}", sep[0], sep[1]));
                }
                else if (sep.Count() == 4)
                {
                    Memory._Proxy = new WebProxy(sep[0], Convert.ToInt32(sep[1]));
                    Memory._Proxy.Credentials = new NetworkCredential(sep[2], sep[3]);
                    return Success(String.Format("{0}:{1}:{2}:{3}", sep[0], sep[1], sep[2], sep[3]));
                }
                return Fault("Invalid proxy string!");
            }
            catch (Exception ex)
            {
                return Exceptional(ex);
            }
        }






        ///<summary>
        /// Sets a header, that will be used once, in the next request.
        ///</summary>
        ///<param name="name">
        /// The name of the header.
        ///</param>
        ///<param name="value">
        /// The value of the header.
        ///</param>
        public HttpEngine SetHeader(string name, string value)
        {
            if (!Initial("SetHeader()")) return this;
            try
            {
                if (Memory._AdditionalHeaders == null) Memory._AdditionalHeaders = new Dictionary<string, string>();
                Memory._AdditionalHeaders.Add(name, value);
                return Success(String.Format("{0}:{1}", name, value));
            }
            catch (Exception ex)
            {
                return Exceptional(ex);
            }
        }

        ///<summary>
        /// Sets proxy server.
        ///</summary>
        ///<param name="namePort">
        /// The hostName/ip - port pair to be used in format [Ip]:[Port]
        ///</param>
        public HttpEngine SetProxy(string namePort)
        {
            if (!Initial("SetProxy()")) return this;
            try
            {
                string[] sep = namePort.Split(':');
                Memory._Proxy = new WebProxy(sep[0], Convert.ToInt32(sep[1]));
                return Success(String.Format("{0}:{1}", sep[0], sep[1]));
            }
            catch (Exception ex)
            {
                return Exceptional(ex);
            }
        }

        ///<summary>
        /// Sets proxy server.
        ///</summary>
        ///<param name="namePort">
        /// The hostName/ip - port pair to be used in format [Ip]:[Port].
        ///</param>
        ///<param name="user">
        /// The userName to be used.
        ///</param>
        ///<param name="pass">
        /// The password to be used.
        ///</param>
        public HttpEngine SetProxy(string namePort, string user = "", string pass = "")
        {
            if (!Initial("SetProxy()")) return this;
            try
            {
                string[] sep = namePort.Split(':');
                Memory._Proxy = new WebProxy(sep[0], Convert.ToInt32(sep[1]));
                if (user != "" && pass != "")
                {
                    Memory._Proxy.Credentials = new NetworkCredential(user, pass);
                }
                return Success(String.Format("{0}:{1}", sep[0], sep[1]));
            }
            catch (Exception ex)
            {
                return Exceptional(ex);
            }
        }

        ///<summary>
        /// Sets filename to be used with this instance of the engine.
        /// If not set, the filename will be a string representing a timestamp. T:HttpVehicleItems can be used.
        ///</summary>
        ///<param name="fileName">
        /// The filename to be used in format: [filename].[extention]
        ///</param>
        public HttpEngine SetFileName(string fileName)
        {
            if (!Initial("SetFileName()")) return this;
            try
            {
                Memory._FileName = fileName;
                return Success(fileName);
            }
            catch (Exception ex)
            {
                return Exceptional(ex);
            }
        }

        ///<summary>
        /// Copies object at keyFrom to keyTo.
        ///</summary>
        ///<param name="keyFrom">
        /// The key that corresponds to the object being copied.
        ///</param>
        ///<param name="keyTo">
        /// The key that corresponds to the destination object. 
        /// If not allready existing, new object will be created in the Storage, and assigned that key.
        ///</param>
        public HttpEngine SetDataCopy(string keyFrom, string keyTo)
        {
            if (!Initial("SetDataCopy()")) return this;
            try
            {
                object input = Mapper(keyFrom);
                AddStorage(input, keyTo);
                return Success(keyFrom + " > " + keyTo);
            }
            catch (Exception ex)
            {
                return Exceptional(ex);
            }
        }

        ///<summary>
        /// Sets the timeout for this instance of the engine.
        ///</summary>
        ///<param name="timeout">
        /// The timeout in milliseconds.
        ///</param>
        public HttpEngine SetTimeout(int timeout = 7000)
        {
            if (!Initial("SetTimeout()")) return this;
            Memory._Timeout = timeout;
            return Success(timeout.ToString());
        }

        ///<summary>
        /// Sets the UserAgent for this instance of the engine.
        ///</summary>
        ///<param name="userAgent">
        /// The UserAgent to be used.
        ///</param>
        public HttpEngine SetUserAgent(string userAgent)
        {
            if (!Initial("SetUserAgent()")) return this;
            Memory._UserAgent = userAgent;
            return Success(userAgent);
        }

        ///<summary>
        /// Sets Storage element.
        /// When used with assigned key the functionality overlaps with T:SetResult(string, string) when used with assigned key.
        ///</summary>
        ///<param name="data">
        /// The data to be set. 
        ///</param>
        ///<param name="key">
        /// The key to be assigned to the element. 
        /// If already assigned, the coresponding element will be set, instead of creating a new one.
        ///</param>
        public HttpEngine SetData(string data, string key = "")
        {
            if (!Initial("SetData()")) return this;
            try
            {
                AddStorage(data, key);
                return Success(data);
            }
            catch (Exception ex)
            {
                return Exceptional(ex);
            }
        }

        ///<summary>
        /// Sets Result element. 
        /// Usefull for loading allready downloaded web pages.
        /// When used with assigned key the functionality overlaps with T:SetData(string, string) when used with assigned key.
        ///</summary>
        ///<param name="data">
        /// The data to be set. 
        ///</param>
        ///<param name="key">
        /// The key to be assigned to the element. 
        /// If already assigned, the coresponding element will be set, instead of creating a new one.
        ///</param>
        public HttpEngine SetResult(string data, string key = "")
        {
            if (!Initial("SetResult()")) return this;
            try
            {
                AddResult(data, key);
                return Success("At : " + key);
            }
            catch (Exception ex)
            {
                return Exceptional(ex);
            }
        }

        ///<summary>
        /// Sets new empty Storage element.
        ///</summary>
        ///<param name="key">
        /// The key to be assigned to the element.
        ///</param>
        public HttpEngine SetNewData(string key = "")
        {
            if (!Initial("SetNewData()")) return this;
            try
            {
                Memory._Storage.Add(new object());
                Map(key, LastElement(Memory._Storage));
                return Success("At : " + key);
            }
            catch (Exception ex)
            {
                return Exceptional(ex);
            }
        }

        ///<summary>
        /// Sets new empty Result element.
        ///</summary>
        ///<param name="key">
        /// The key to be assigned to the element.
        ///</param>
        public HttpEngine SetNewResult(string key = "")
        {
            if (!Initial("SetNewResult()")) return this;
            try
            {
                Memory._Results.Add(new object());
                Map(key, LastElement(Memory._Results));
                return Success("At : " + key);
            }
            catch (Exception ex)
            {
                return Exceptional(ex);
            }
        }
    }
}
