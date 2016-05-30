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
        //OUTPUT/SAVERS         //save last Storage if key not set

        ///<summary>
        /// Saves the current engine state to file, so it can be loaded later.
        /// If FileName property is not set, the FileName is a string, representing a timestamp.
        ///</summary>
        ///<param name="saveDirectoryPath">
        /// The folder to save in.
        /// If not setted, the file will be saved in "[CurrentDir]\Save".
        ///</param>
        public HttpEngine FileSave(string saveDirectoryPath = "")
        {
            if (!Initial("FileSave()")) return this;
            try
            {
                string exepath = saveDirectoryPath == "" ? IO.iExepath : saveDirectoryPath;
                if (Validators.ValidateFolderExists(exepath) == false)
                {
                    return Fault("Invalid save path!");
                }
                if (Validators.ValidateWriteAccess(exepath) == false)
                {
                    return Fault("You don't have write access!");
                }
                string savepath = exepath + @"\Save\";
                if (Directory.Exists(savepath) == false) { Directory.CreateDirectory(savepath); }

                savepath += Memory._FileName;

                using (Stream stream = File.Open(savepath, FileMode.Create))
                {
                    new BinaryFormatter().Serialize(stream, Memory);
                }
                return Success(Memory._FileName + " Saved!");
            }
            catch (Exception ex)
            {
                return Exceptional(ex);
            }
        }

        ///<summary>
        /// Saves object at given key to a file.
        /// The object being exported must be either 'string', 'string[]' or 'byte[]'.
        ///</summary>
        ///<param name="fileName">
        /// The filename to be used in format: [filename].[extention]. T:HttpVehicleItems can be used
        ///</param>
        ///<param name="key">
        /// The key that corresponds to the object to save.
        /// If not setted, the last stored object in Storage will be saved.
        ///</param>
        ///<param name="saveDirectoryPath">
        /// The folder to save in.
        /// If not setted, the file will be saved in "[CurrentDir]\Save".
        ///</param>
        public HttpEngine FileExport(string fileName, string key = "", string saveDirectoryPath = "")
        {
            if (!Initial("FileExport()")) return this;
            try
            {
                string exepath = saveDirectoryPath == "" ? IO.iExepath : saveDirectoryPath;
                if (Validators.ValidateFolderExists(exepath) == false)
                {
                    return Fault("Invalid save path!");
                }
                if (Validators.ValidateWriteAccess(exepath) == false)
                {
                    return Fault("You don't have write access!");
                }
                string savepath = exepath + @"\Save\";
                if (Directory.Exists(savepath) == false) { Directory.CreateDirectory(savepath); }

                savepath += fileName;

                object obj = key != "" ? Mapper(key) : Memory._Storage[Memory._Storage.Count - 1];
                if (Validators.ValidateResult(obj) == false)
                {
                    return Fault("Invalid result!");
                }
                else if (obj is string)
                {
                    string str = obj as string;
                    File.WriteAllText(savepath, str);
                    return Success(fileName + " Saved! " + str.Length.ToString() + " chars of data written!");
                }
                else if (obj is string[])
                {
                    string str = String.Join(Environment.NewLine, obj as string[]);
                    File.WriteAllText(savepath, str);
                    return Success(fileName + " Saved! " + str.Length.ToString() + " chars of data written!");
                }
                else if (obj is byte[])
                {
                    byte[] buff = obj as byte[];
                    File.WriteAllBytes(savepath, buff);
                    return Success(fileName + " Saved! " + buff.Count() + " bytes of data written!");
                }
                return Fault(String.Format("Stored at {0} is not string/string[]/byte[]!", key));
            }
            catch (Exception ex)
            {
                return Exceptional(ex);
            }
        }

        ///<summary>
        /// Appends text at given key to a file.
        /// The text being appended must be either 'string' or 'string[]'.
        /// If the file don't exists it will be created.
        ///</summary>
        ///<param name="fileName">
        /// The filename to be used in format: [filename].[extention]. T:HttpVehicleItems can be used
        ///</param>
        ///<param name="key">
        /// The key that corresponds to the object to save.
        /// If not setted, the last stored object in Storage will be saved.
        ///</param>
        ///<param name="saveDirectoryPath">
        /// The folder to save in.
        /// If not setted, the file will be saved in "[CurrentDir]\Save".
        ///</param>
        public HttpEngine FileAppend(string fileName, string key = "", string saveDirectoryPath = "")
        {
            if (!Initial("FileAppend()")) return this;
            try
            {
                string exepath = saveDirectoryPath == "" ? IO.iExepath : saveDirectoryPath;
                if (Validators.ValidateFolderExists(exepath) == false)
                {
                    return Fault("Invalid save path!");
                }
                if (Validators.ValidateWriteAccess(exepath) == false)
                {
                    return Fault("You don't have write access!");
                }
                string savepath = exepath + @"\Save\";
                if (Directory.Exists(savepath) == false) { Directory.CreateDirectory(savepath); }

                savepath += fileName;

                object obj = key != "" ? Mapper(key) : Memory._Storage[Memory._Storage.Count - 1];
                if (Validators.ValidateResult(obj) == false)
                {
                    return Fault("Invalid result!");
                }
                else if (obj is string)
                {
                    string str = obj as string;
                    if (File.Exists(savepath))
                    {
                        File.AppendAllText(savepath, Environment.NewLine + str);
                    }
                    else
                    {
                        File.WriteAllText(savepath, str);
                    }
                    return Success(fileName + " Appended! " + str.Length.ToString() + " chars of data written!");
                }
                else if (obj is string[])
                {
                    string str = String.Join(Environment.NewLine, obj as string[]);
                    if (File.Exists(savepath))
                    {
                        File.AppendAllText(savepath, Environment.NewLine + str);
                    }
                    else
                    {
                        File.WriteAllText(savepath, str);
                    }
                    return Success(fileName + " Appended! " + str.Length.ToString() + " chars of data written!");
                }
                return Fault(String.Format("Stored at {0} is not string/string[]!", key));
            }
            catch (Exception ex)
            {
                return Exceptional(ex);
            }
        }

        ///<summary>
        /// Appends text at given key to a file, line by line, if a line is not present in the file.
        /// If the file don't exists it will be created.
        /// The text being appended must be either 'string' or 'string[]'.
        ///</summary>
        ///<param name="fileName">
        /// The filename to be used in format: [filename].[extention]. T:HttpVehicleItems can be used
        ///</param>
        ///<param name="key">
        /// The key that corresponds to the object to save.
        /// If not setted, the last stored object in Storage will be saved.
        ///</param>
        ///<param name="saveDirectoryPath">
        /// The folder to save in.
        /// If not setted, the file will be accumulated in "[CurrentDir]\Save".
        ///</param>
        public HttpEngine FileAccumulate(string fileName, string key = "", string saveDirectoryPath = "")
        {
            if (!Initial("FileAccumulate()")) return this;
            try
            {
                string exepath = saveDirectoryPath == "" ? IO.iExepath : saveDirectoryPath;
                if (Validators.ValidateFolderExists(exepath) == false)
                {
                    return Fault("Invalid save path!");
                }
                if (Validators.ValidateWriteAccess(exepath) == false)
                {
                    return Fault("You don't have write access!");
                }
                string savepath = exepath + @"\Save\";
                if (Directory.Exists(savepath) == false) { Directory.CreateDirectory(savepath); }

                savepath += fileName;

                object obj = key != "" ? Mapper(key) : Memory._Storage[Memory._Storage.Count - 1];
                if (Validators.ValidateResult(obj) == false)
                {
                    return Fault("Invalid result!");
                }
                if (obj is string == false && obj is string[] == false)
                {
                    return Fault("Last stored is not string/string[]!");
                }

                if (obj is string)
                {
                    string str = obj as string;
                    if (File.Exists(savepath))
                    {
                        string[] all = File.ReadAllLines(savepath);
                        if (all.Contains(str) == false)
                        {
                            File.AppendAllText(savepath, Environment.NewLine + str);
                            return Success(fileName + " Appended! " + str.Length.ToString() + " chars of data written!");
                        }
                        else
                        {
                            return Success("Record already exists");
                        }
                    }
                    else
                    {
                        File.WriteAllText(savepath, str);
                        return Success(fileName + " Saved! " + str.Length.ToString() + " chars of data written!");
                    }
                }
                else
                {
                    string[] sep = obj as string[];

                    if (File.Exists(savepath))
                    {
                        string[] all = File.ReadAllLines(savepath);

                        List<string> toAdd = new List<string>();
                        foreach (string str in sep)
                        {
                            if (all.Contains(str) == false)
                            {
                                toAdd.Add(str);
                            }
                        }
                        foreach (string s in all)
                        {
                            toAdd.Add(s);
                        }
                        File.WriteAllLines(savepath, toAdd);
                        return Success(fileName + " Saved! " + toAdd.Count().ToString() + " values written!");
                    }
                    else
                    {
                        File.WriteAllLines(savepath, sep);
                        return Success(fileName + " Saved! " + sep.Count().ToString() + " values written!");
                    }

                }
            }
            catch (Exception ex)
            {
                return Exceptional(ex);
            }
        }

        ///<summary>
        /// Removes text at given key from file, line by line, if a line is present in the file.
        /// The text being removed must be either 'string' or 'string[]'.
        ///</summary>
        ///<param name="fileName">
        /// The filename to be used in format: [filename].[extention]. T:HttpVehicleItems can be used.
        ///</param>
        ///<param name="key">
        /// The key that corresponds to the object to remove.
        /// If not setted, the last stored object in Storage will be used.
        ///</param>
        ///<param name="directoryPath">
        /// The folder to save in.
        /// If not setted, the file will be expected in "[CurrentDir]\Save".
        ///</param>
        public HttpEngine FileRemove(string fileName, string key = "", string directoryPath = "")
        {
            if (!Initial("FileRemove()")) return this;
            try
            {
                string exepath = directoryPath == "" ? IO.iExepath : directoryPath;
                if (Validators.ValidateFolderExists(exepath) == false)
                {
                    return Fault("Invalid save path!");
                }
                if (Validators.ValidateWriteAccess(exepath) == false)
                {
                    return Fault("You don't have write access!");
                }
                string savepath = exepath + @"\Save\";
                if (Directory.Exists(savepath) == false) { Directory.CreateDirectory(savepath); }

                savepath += fileName;

                object obj = key != "" ? Mapper(key) : Memory._Storage[Memory._Storage.Count - 1];
                if (Validators.ValidateResult(obj) == false)
                {
                    return Fault("Invalid result!");
                }
                if (obj is string == false && obj is string[] == false)
                {
                    return Fault("Last stored is not string/string[]!");
                }

                if (obj is string)
                {
                    string str = obj as string;
                    if (File.Exists(savepath))
                    {
                        List<string> all = File.ReadAllLines(savepath).ToList();
                        if (all.Contains(str))
                        {
                            all.Remove(str);
                            File.WriteAllLines(savepath, all.ToArray());
                            return Success();
                        }
                        else
                        {
                            return Success("Record don't exists");
                        }
                    }
                    else
                    {
                        return Fault("No such file!");
                    }
                }
                else
                {
                    string[] sep = obj as string[];

                    if (File.Exists(savepath))
                    {
                        List<string> all = File.ReadAllLines(savepath).ToList();

                        foreach (string str in sep)
                        {
                            if (all.Contains(str))
                            {
                                all.Remove(str);
                            }
                        }

                        File.WriteAllLines(savepath, all.ToArray());
                        return Success();
                    }
                    else
                    {
                        return Fault("No such file!");
                    }

                }
            }
            catch (Exception ex)
            {
                return Exceptional(ex);
            }
        }

        ///<summary>
        /// Deletes a file.
        /// The text being appended must be either 'string' or 'string[]'.
        ///</summary>
        ///<param name="fileName">
        /// The filename to be used in format: [filename].[extention]. T:HttpVehicleItems can be used
        ///</param>
        ///<param name="saveDirectoryPath">
        /// The folder to delete from.
        /// If not setted, the file will be deleted from "[CurrentDir]\Save".
        ///</param>
        public HttpEngine FileDelete(string fileName, string saveDirectoryPath = "")
        {
            if (!Initial("FileDelete()")) return this;
            try
            {
                string exepath = saveDirectoryPath == "" ? IO.iExepath : saveDirectoryPath;
                if (Validators.ValidateFolderExists(exepath) == false)
                {
                    return Fault("Invalid save path!");
                }
                if (Validators.ValidateWriteAccess(exepath) == false)
                {
                    return Fault(" You don't have write access!");
                }
                string savepath = exepath + @"\Save\";
                if (Directory.Exists(savepath) == false) { Directory.CreateDirectory(savepath); }

                savepath += fileName;
                File.Delete(savepath);
                return Success(fileName + " Deleted!");
            }
            catch (Exception ex)
            {
                return Exceptional(ex);
            }
        }




        ///<summary>
        /// Outputs the cookies for this instance of the engine currently set.
        ///</summary>
        ///<param name="o">
        /// The CookieCollection to extract the cookies to.
        ///</param>
        public HttpEngine GetCookies(out CookieCollection o)
        {
            try
            {
                o = ParsersExtractors.ExtractAllCookies(Memory._Jar);
                return this;
            }
            catch (Exception ex)
            {
                o = null;
                return Exceptional(ex);
            }
        }

        ///<summary>
        /// Outputs the cookies for this instance of the engine currently set.
        ///</summary>
        ///<param name="o">
        /// The CookieContainer to be filled.
        ///</param>
        public HttpEngine GetCookies(out CookieContainer o)
        {
            try
            {
                o = Memory._Jar;
                return this;
            }
            catch (Exception ex)
            {
                o = null;
                return Exceptional(ex);
            }
        }

        ///<summary>
        /// Outputs the Cookie with a given name.
        ///</summary>
        ///<param name="s">
        /// The string to output to.
        ///</param>
        ///<param name="cookieName">
        /// The cookie name.
        ///</param>
        public HttpEngine GetCookieValue(out string s, string cookieName)
        {
            try
            {
                CookieCollection cc;
                GetCookies(out cc);
                s = ParsersExtractors.ExtractCookieValue(cc, cookieName);
                return this;
            }
            catch (Exception ex)
            {
                s = null;
                return Exceptional(ex);
            }
        }

        ///<summary>
        /// Outputs the cookies for this instance of the engine currently set.
        ///</summary>
        ///<param name="s">
        /// The string array to output to.
        ///</param>
        public HttpEngine GetCookies(out string[] s)
        {
            try
            {
                CookieCollection cc;
                GetCookies(out cc);
                s = ParsersExtractors.ExtractAllCookies(cc);
                return this;
            }
            catch (Exception ex)
            {
                s = null;
                return Exceptional(ex);
            }
        }

        ///<summary>
        /// Outputs the Results this instance of the engine currently set.
        ///</summary>
        ///<param name="o">
        /// The List to output to.
        ///</param>
        public HttpEngine GetResults(out List<object> o)
        {
            try
            {
                o = Memory._Results;
                return this;
            }
            catch (Exception ex)
            {
                o = null;
                return Exceptional(ex);
            }
        }

        ///<summary>
        /// Outputs the last Result currently set.
        ///</summary>
        ///<param name="o">
        /// The object to output to.
        ///</param>
        public HttpEngine GetLastResult(out object o)
        {
            try
            {
                o = Memory._Results[Memory._Results.Count - 1];
                return this;
            }
            catch (Exception ex)
            {
                o = null;
                return Exceptional(ex);
            }
        }

        ///<summary>
        /// Outputs the Storage items this instance of the engine currently set.
        ///</summary>
        ///<param name="o">
        /// The List to output to.
        ///</param>
        public HttpEngine GetStorage(out List<object> o)
        {
            try
            {
                o = Memory._Storage;
                return this;
            }
            catch (Exception ex)
            {
                o = null;
                return Exceptional(ex);
            }
        }

        ///<summary>
        /// Outputs the last Storage item currently set.
        ///</summary>
        ///<param name="o">
        /// The object to output to.
        ///</param>
        public HttpEngine GetLastStored(out object o)
        {
            try
            {
                o = Memory._Storage[Memory._Storage.Count - 1];
                return this;
            }
            catch (Exception ex)
            {
                o = null;
                return Exceptional(ex);
            }
        }

        ///<summary>
        /// Outputs the log for this run of the engine
        ///</summary>
        ///<param name="s">
        /// The string to output to.
        ///</param>
        public HttpEngine GetLog(out string s)
        {
            try
            {
                s = Memory._Log;
                return this;
            }
            catch (Exception ex)
            {
                s = null;
                return Exceptional(ex);
            }
        }

        ///<summary>
        /// Outputs the UserAgent for this instance of the engine
        ///</summary>
        ///<param name="s">
        /// The string to output to.
        ///</param>
        public HttpEngine GetUserAgent(out string s)
        {
            try
            {
                s = Memory._UserAgent;
                return this;
            }
            catch (Exception ex)
            {
                s = null;
                return Exceptional(ex);
            }
        }

        ///<summary>
        /// Outputs the Proxy for this instance of the engine
        ///</summary>
        ///<param name="s">
        /// The string to output to.
        ///</param>
        public HttpEngine GetProxy(out string s)
        {
            try
            {
                s = Proxy;
                return this;
            }
            catch (Exception ex)
            {
                s = null;
                return Exceptional(ex);
            }
        }

        ///<summary>
        /// Outputs the Timeout for this instance of the engine
        ///</summary>
        ///<param name="i">
        /// The int to output to.
        ///</param>
        public HttpEngine GetTimeout(out int i)
        {
            try
            {
                i = Memory._Timeout;
                return this;
            }
            catch (Exception ex)
            {
                i = -1;
                return Exceptional(ex);
            }
        }

        ///<summary>
        /// Outputs the Item at a given key
        ///</summary>
        ///<param name="o">
        /// The object to output to.
        ///</param>
        ///<param name="key">
        /// The key corresponding to the item needed.
        ///</param>
        public HttpEngine GetItem(out object o, string key)
        {
            try
            {
                o = Mapper(key);
                return this;
            }
            catch (Exception ex)
            {
                o = null;
                return Exceptional(ex);
            }
        }
    }
}
