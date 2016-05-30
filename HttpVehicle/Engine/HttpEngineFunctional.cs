using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HttpVehicle
{
    public partial class HttpEngine
    {
        //FUNC         //get last Storage if data not set. Output to Result.

        ///<summary>
        /// Ctor.
        /// Construct the engine.
        ///</summary>
        public HttpEngine()
        {
            ResetLog();
            ResetJar();
            ResetResults();
            ResetStorage();
            Memory._Map = new Dictionary<string, object>();

            ProvideUserAgent();
            ResetProxy();
            SetTimeout();
        }

        ///<summary>
        /// Perform HTTP/HTTPS GET request.
        ///</summary>
        ///<param name="urlOrKey">
        /// The Url of the request, or a key that coresponds to the url.
        ///</param>
        ///<param name="key">
        /// The key to assign to the retrieved result.
        /// If already assigned, the coresponding element will be set, instead of creating a new one.
        ///</param>
        public HttpEngine GetRequest(string urlOrKey = "", string key = "")
        {
            if (!Initial("GetRequest()")) return this;
            try
            {
                if (urlOrKey == "")
                {
                    object o = Memory._Storage[Memory._Storage.Count - 1];
                    if (o is string == false)
                    {
                        return Fault(String.Format("Stored at {0} is not string!", Memory._Storage.Count - 1));
                    }
                }
                else if (Memory._Map.ContainsKey(urlOrKey))
                {
                    if (Mapper(urlOrKey) is string == false)
                    {
                        return Fault(String.Format("Stored at {0} is not string!", urlOrKey));
                    }
                    urlOrKey = Mapper(urlOrKey) as string;
                }
                ln1: if (!urlOrKey.Contains("http://") && !urlOrKey.Contains("https://")) urlOrKey = "http://" + urlOrKey;
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(urlOrKey); req.CookieContainer = Memory._Jar;
                req.Method = "GET";
                req.Host = urlOrKey.Split(new string[] { "//" }, StringSplitOptions.None)[1].Split('/')[0];
                req.KeepAlive = true;
                req.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
                req.UserAgent = Memory._UserAgent;
                req.Headers.Add("Accept-Encoding", "gzip, deflate, sdch");
                req.Headers.Add("Accept-Language", "en-US,en;q=0.8");
                req.AllowAutoRedirect = false;
                req.Proxy = Memory._Proxy;
                req.Timeout = Memory._Timeout;
                if (Memory._AdditionalHeaders != null)
                {
                    foreach (KeyValuePair<string, string> h in Memory._AdditionalHeaders)
                    {
                        if (h.Key == "Content-Type") { req.ContentType = h.Value; }
                        else if (h.Key == "KeepAlive") { req.KeepAlive = Convert.ToBoolean(h.Value); }
                        else if (h.Key == "Accept") { req.Accept = h.Value; }
                        else if (h.Key == "Connection") { req.Connection = h.Value; }
                        else if (h.Key == "ContentLength") { req.ContentLength = Convert.ToInt64(h.Value); }
                        else if (h.Key == "ContentType") { req.ContentType = h.Value; }
                        else if (h.Key == "Date") { req.Date = Convert.ToDateTime(h.Value); }
                        else if (h.Key == "Expect") { req.Expect = h.Value; }
                        else if (h.Key == "ProtocolVersion  ") { req.ProtocolVersion = new Version(h.Value); }
                        else if (h.Key == "Referer") { req.Referer = h.Value; }
                        else if (h.Key == "TransferEncoding") { req.TransferEncoding = h.Value; }
                        else if (h.Key == "UserAgent") { req.UserAgent = h.Value; }
                        else { req.Headers.Add(h.Key, h.Value); }
                    }
                }
                Memory._AdditionalHeaders = null;

                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

                int status = (int)resp.StatusCode;
                if (status == 200)
                {
                    string resph = ""; for (int i = 0; i < resp.Headers.Count; i++) resph += resp.Headers.Keys[i] + ":" + resp.Headers[i] + ";" + Environment.NewLine;
                    string contentType = resp.GetResponseHeader("content-type");
                    if (resph.ToLower().Contains("content-encoding:gzip"))
                    {

                        if (contentType.Contains("text") || contentType == "")
                        {
                            AddResult(new StreamReader(new GZipStream(resp.GetResponseStream(), CompressionMode.Decompress)).ReadToEnd(), key);
                            return Success(urlOrKey + " Error 200");
                        }
                        else
                        {
                            MemoryStream ms = new MemoryStream();
                            new GZipStream(resp.GetResponseStream(), CompressionMode.Decompress).CopyTo(ms);
                            AddResult(ms.ToArray(), key);
                            return Success(urlOrKey + " Error 200");
                        }
                    }
                    else
                    {
                        if (contentType.Contains("text") || contentType == "")
                        {
                            AddResult(new StreamReader(resp.GetResponseStream()).ReadToEnd(), key);
                            return Success(urlOrKey + " Error 200");
                        }
                        else
                        {
                            MemoryStream ms = new MemoryStream(); resp.GetResponseStream().CopyTo(ms);
                            AddResult(ms.ToArray(), key);
                            return Success(urlOrKey + " Error 200");
                        }
                    }
                }
                if (status == 302 || status == 301)
                {
                    for (int i = 0; i < resp.Headers.Count; i++) { if (resp.Headers.Keys[i].Contains("Location")) urlOrKey = resp.Headers[i]; }
                    LogLine(" Error " + status.ToString() + " redirecting...");
                    goto ln1;
                }
                return Fault(urlOrKey);
            }
            catch (Exception ex)
            {
                return Exceptional(ex);
            }
        }

        ///<summary>
        /// Perform HTTP/HTTPS POST request.
        ///</summary>
        ///<param name="urlOrKey">
        /// The Url of the request, or a key that coresponds to the url.
        ///</param>
        ///<param name="postDataOrKey">
        /// The data to be posted, or the key that corresponds to that data.
        ///</param>
        ///<param name="key">
        /// The key to assign to the retrieved result.
        ///</param>
        public HttpEngine PostRequest(string urlOrKey = "", string postDataOrKey = "", string key = "")
        {
            if (!Initial("PostRequest()")) return this;
            try
            {
                bool urlFetchFlag = false;
                if (urlOrKey == "")
                {
                    object o = Memory._Storage[Memory._Storage.Count - 1];
                    urlFetchFlag = true;
                    if (o is string == false)
                    {
                        return Fault(string.Format("Stored at {0} is not string!", Memory._Storage.Count - 1));
                    }
                }
                else if (Memory._Map.ContainsKey(urlOrKey))
                {
                    if (Mapper(urlOrKey) is string == false)
                    {
                        return Fault(String.Format("Stored at {0} is not string!", urlOrKey));
                    }
                    urlOrKey = Mapper(urlOrKey) as string;
                }




                if (postDataOrKey == "")
                {
                    object o = urlFetchFlag ? Memory._Storage[Memory._Storage.Count - 2] : Memory._Storage[Memory._Storage.Count - 1];
                    if (o is string == false)
                    {
                        return Fault(String.Format("Stored at {0} is not string!", Memory._Storage.Count - 2));
                    }
                }
                else if (Memory._Map.ContainsKey(postDataOrKey))
                {
                    if (Mapper(postDataOrKey) is string == false)
                    {
                        return Fault(String.Format("Stored at {0} is not string!", postDataOrKey));
                    }
                    postDataOrKey = Mapper(postDataOrKey) as string;
                }




                if (!urlOrKey.Contains("http://") && !urlOrKey.Contains("https://")) urlOrKey = "http://" + urlOrKey;
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(urlOrKey); WebHeaderCollection c = req.Headers; req.CookieContainer = Memory._Jar;
                req.Method = "POST";
                req.Host = urlOrKey.Split(new string[] { "//" }, StringSplitOptions.None)[1].Split('/')[0];
                string dataString = Mapper(postDataOrKey).ToString();
                byte[] data = new UTF8Encoding().GetBytes(dataString);
                req.KeepAlive = true;
                req.ContentLength = data.Length;
                c.Add("Cache-Control", "max-age=0");
                req.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
                req.UserAgent = Memory._UserAgent;
                req.ContentType = "application/x-www-form-urlencoded";
                c.Add("Accept-Encoding", "gzip, deflate");
                c.Add("Accept-Language", "en-US,en;q=0.8");
                req.AllowAutoRedirect = false;
                req.Proxy = Memory._Proxy;
                req.Timeout = Memory._Timeout;
                if (Memory._AdditionalHeaders != null)
                {
                    foreach (KeyValuePair<string, string> h in Memory._AdditionalHeaders)
                    {
                        if (h.Key == "Content-Type") { req.ContentType = h.Value; }
                        else if (h.Key == "KeepAlive") { req.KeepAlive = Convert.ToBoolean(h.Value); }
                        else if (h.Key == "Accept") { req.Accept = h.Value; }
                        else if (h.Key == "Connection") { req.Connection = h.Value; }
                        else if (h.Key == "ContentLength") { req.ContentLength = Convert.ToInt64(h.Value); }
                        else if (h.Key == "ContentType") { req.ContentType = h.Value; }
                        else if (h.Key == "Date") { req.Date = Convert.ToDateTime(h.Value); }
                        else if (h.Key == "Expect") { req.Expect = h.Value; }
                        else if (h.Key == "ProtocolVersion  ") { req.ProtocolVersion = new Version(h.Value); }
                        else if (h.Key == "Referer") { req.Referer = h.Value; }
                        else if (h.Key == "TransferEncoding") { req.TransferEncoding = h.Value; }
                        else if (h.Key == "UserAgent") { req.UserAgent = h.Value; }
                        else { req.Headers.Add(h.Key, h.Value); }
                    }
                }
                Memory._AdditionalHeaders = null;

                using (Stream stream = req.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }

                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                int status = (int)resp.StatusCode;
                if (status == 200)
                {
                    string resph = ""; for (int i = 0; i < resp.Headers.Count; i++) resph += resp.Headers.Keys[i] + ":" + resp.Headers[i] + ";" + Environment.NewLine;
                    string contentType = resp.GetResponseHeader("content-type");

                    if (resph.ToLower().Contains("content-encoding:gzip"))
                    {
                        if (contentType.Contains("text") || contentType == "")
                        {
                            AddResult(new StreamReader(new GZipStream(resp.GetResponseStream(), CompressionMode.Decompress)).ReadToEnd(), key);
                            return Success(urlOrKey + " Error 200");
                        }
                        else
                        {
                            MemoryStream ms = new MemoryStream();
                            new GZipStream(resp.GetResponseStream(), CompressionMode.Decompress).CopyTo(ms);
                            AddResult(ms.ToArray(), key);
                            return Success(urlOrKey + " Error 200");
                        }
                    }
                    else
                    {
                        if (contentType.Contains("text") || contentType == "")
                        {
                            AddResult(new StreamReader(resp.GetResponseStream()).ReadToEnd(), key);
                            return Success(urlOrKey + " Error 200");
                        }
                        else
                        {
                            MemoryStream ms = new MemoryStream(); resp.GetResponseStream().CopyTo(ms);
                            AddResult(ms.ToArray(), key);
                            return Success(urlOrKey + " Error 200");
                        }
                    }
                }
                if (status == 302 || status == 301)
                {
                    string resph = ""; for (int i = 0; i < resp.Headers.Count; i++) resph += resp.Headers.Keys[i] + ":" + resp.Headers[i] + ";" + Environment.NewLine;
                    string contentType = resp.GetResponseHeader("content-type");

                    if (resph.ToLower().Contains("content-encoding:gzip"))
                    {
                        if (contentType.Contains("text") || contentType == "")
                        {
                            AddResult(new StreamReader(new GZipStream(resp.GetResponseStream(), CompressionMode.Decompress)).ReadToEnd(), key);
                            return Success(urlOrKey + " Error " + status);
                        }
                        else
                        {
                            MemoryStream ms = new MemoryStream();
                            new GZipStream(resp.GetResponseStream(), CompressionMode.Decompress).CopyTo(ms);
                            AddResult(ms.ToArray(), key);
                            return Success(urlOrKey + " Error " + status);
                        }
                    }
                    else
                    {
                        if (contentType.Contains("text") || contentType == "")
                        {
                            AddResult(new StreamReader(resp.GetResponseStream()).ReadToEnd(), key);
                            return Success(urlOrKey + " Error " + status);
                        }
                        else
                        {
                            MemoryStream ms = new MemoryStream(); resp.GetResponseStream().CopyTo(ms);
                            AddResult(ms.ToArray(), key);
                            return Success(urlOrKey + " Error " + status);
                        }
                    }
                }
                return Fault(urlOrKey + ':' + status);
            }
            catch (Exception ex)
            {
                return Exceptional(ex);
            }
        }

        ///<summary>
        /// Pause the execution for a random period of time, in a given interval.
        ///</summary>
        ///<param name="min">
        /// The minimum period to pause for, in milliseconds.
        ///</param>
        ///<param name="max">
        /// The maximum period to pause for, in milliseconds.
        ///</param>
        public HttpEngine Sleep(int min = 0, int max = 5000)
        {
            if (!Initial("Sleep()")) return this;
            int delay = new Random().Next(min, max);
            System.Threading.Thread.Sleep(delay);
            return Success(delay.ToString());
        }

        ///<summary>
        /// Pause the execution for a given period of time
        ///</summary>
        ///<param name="millisecs">
        /// The period to pause for, in milliseconds
        ///</param>
        public HttpEngine Sleep(int millisecs)
        {
            if (!Initial("Sleep()")) return this;
            System.Threading.Thread.Sleep(millisecs);
            return Success(millisecs.ToString());
        }
    }
}
