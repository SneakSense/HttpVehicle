using System;
using System.Linq;
using System.Net;

namespace HttpVehicle
{
    public partial class HttpEngine
    {
        //PARSERS/EXTRACTORS

        ///<summary>
        /// Extracts cookie value to Storage.
        ///</summary>
        ///<param name="cookieName">
        /// The name of the cookie.
        ///</param>
        ///<param name="key">
        /// The key to be assigned to the element. 
        /// If already assigned, the coresponding element will be set, instead of creating a new one.
        ///</param>
        public HttpEngine ExtractCookieValue(string cookieName, string key = "")
        {
            if (!Initial("ExtractCookieValue()")) return this;
            bool hasCookie = false;
            CookieCollection col = Jar;
            foreach (Cookie ck in col)
            {
                if (ck.Name == cookieName)
                {
                    hasCookie = true;
                    break;
                }
            }
            if (!hasCookie)
            {
                return Fault("Cookie with the given name not found!");
            }
            try
            {
                string value = ParsersExtractors.ExtractCookieValue(col, cookieName);
                AddStorage(value, key);
                return Success("Stored : " + value);
            }
            catch (Exception ex)
            {
                return Exceptional(ex);
            }
        }

        ///<summary>
        /// Extracts proxy value to Storage.
        ///</summary>
        ///<param name="key">
        /// The key to be assigned to the element. 
        /// If already assigned, the coresponding element will be set, instead of creating a new one.
        ///</param>
        public HttpEngine ExtractProxyValue(string key = "")
        {
            if (!Initial("ExtractProxyValue()")) return this;
            if (Memory._Proxy == null)
            {
                return Fault("Proxy server not set!");
            }
            try
            {
                string value = Memory._Proxy.Address.ToString() + ':' + Memory._Proxy.Address.Port.ToString();
                if (Memory._Proxy.Credentials != null) { value += ':' + Memory._Proxy.Credentials.ToString(); }
                AddStorage(value, key);
                return Success("Stored : " + key);
            }
            catch (Exception ex)
            {
                return Exceptional(ex);
            }
        }

        ///<summary>
        /// Extracts UserAgent string to Storage.
        ///</summary>
        ///<param name="key">
        /// The key to be assigned to the element. 
        /// If already assigned, the coresponding element will be set, instead of creating a new one.
        ///</param>
        public HttpEngine ExtractUserAgent(string key = "")
        {
            if (!Initial("ExtractUserAgent()")) return this;
            if (Memory._UserAgent == null)
            {
                return Fault("UserAgent not set!");
            }
            try
            {
                AddStorage(Memory._UserAgent, key);
                return Success("Stored : " + key);
            }
            catch (Exception ex)
            {
                return Exceptional(ex);
            }
        }

        ///<summary>
        /// Extracts url to Storage.
        ///</summary>
        ///<param name="separator">
        /// The string immediately before the url. 
        ///</param>
        ///<param name="toKey">
        /// The key to be assigned to the extracted value.
        ///</param>
        ///<param name="fromKey">
        /// The key corresponding to the text to extract from.
        /// If not set, the last Result will be used.
        ///</param>
        public HttpEngine ExtractUrlBySeparator(string separator, string toKey = "", string fromKey = "")
        {
            if (!Initial("ExtractUrlBySeparator()")) return this;
            object obj = fromKey != "" ? Mapper(fromKey) : Memory._Results[Memory._Results.Count - 1];

            if (obj is string == false)
            {
                return Fault("Not a string!");
            }
            try
            {

                string catched = ParsersExtractors.ExtractUrl(separator, obj as string);
                AddStorage(catched, toKey);
                return Success("Catched : " + catched);
            }
            catch (Exception ex)
            {
                return Exceptional(ex);
            }
        }

        ///<summary>
        /// Extracts url to Storage, by first part of the url.
        ///</summary>
        ///<param name="part">
        /// The first part of the url. 
        ///</param>
        ///<param name="toKey">
        /// The key to be assigned to the extracted value.
        ///</param>
        ///<param name="fromKey">
        /// The key corresponding to the text to extract from.
        /// If not set, the last Result will be used.
        ///</param>
        public HttpEngine ExtractUrlByPart(string part, string toKey = "", string fromKey = "")
        {
            if (!Initial("ExtractUrlByPart()")) return this;
            object obj = fromKey != "" ? Mapper(fromKey) : Memory._Results[Memory._Results.Count - 1];

            if (obj is string == false)
            {
                return Fault("Last result is not string!");
            }
            try
            {

                string catched = ParsersExtractors.ExtractUrl(part, obj as string);
                catched = part + catched;
                AddStorage(catched, toKey);
                return Success("Catched : " + catched);
            }
            catch (Exception ex)
            {
                return Exceptional(ex);
            }
        }

        ///<summary>
        /// Extracts Ip:port pairs to Storage.
        ///</summary>
        ///<param name="separator">
        /// The separator between [ip/host] and [port] if it's not ':'.
        ///</param>
        ///<param name="toKey">
        /// The key to be assigned to the extracted value.
        ///</param>
        ///<param name="fromKey">
        /// The key corresponding to the text to extract from.
        /// If not set, the last Result will be used.
        ///</param>
        public HttpEngine ExtractIpPortPairs(string separator = "", string toKey = "", string fromKey = "")
        {
            if (!Initial("ExtractSeparatedIpPortPairs()")) return this;
            object obj = fromKey != "" ? Mapper(fromKey) : Memory._Results[Memory._Results.Count - 1];

            if (obj is string == false)
            {
                return Fault("Last result is not string!");
            }
            try
            {
                string[] pairs;
                if (separator == "")
                {
                    pairs = ParsersExtractors.ExtractIpPortPairs(obj as string);
                }
                else
                {
                    pairs = ParsersExtractors.ExtractSeparatedIpPortPairs(obj as string, separator);
                }
                AddStorage(pairs, toKey);
                return Success();
            }
            catch (Exception ex)
            {
                return Exceptional(ex);
            }
        }

        ///<summary>
        /// Extracts string to Storage.
        ///</summary>
        ///<param name="prefix">
        /// The string immediately before.
        ///</param>
        ///<param name="suffix">
        /// The string immediately after.
        ///</param>
        ///<param name="toKey">
        /// The key to be assigned to the extracted value.
        ///</param>
        ///<param name="fromKey">
        /// The key corresponding to the text to extract from.
        /// If not set, the last Result will be used.
        ///</param>
        public HttpEngine ExtractDelimitedString(string prefix = "", string suffix = "", string toKey = "", string fromKey = "")
        {
            if (!Initial("ExtractDelimitedString()")) return this;
            object obj = fromKey != "" ? Mapper(fromKey) : Memory._Results[Memory._Results.Count - 1];

            if (obj is string == false)
            {
                return Fault("Last result is not string!");
            }
            try
            {
                string catched = ParsersExtractors.ExtractSeparatedString(obj as string, prefix, suffix);
                AddStorage(catched, toKey);
                return Success("Catched : " + catched);
            }
            catch (Exception ex)
            {
                return Exceptional(ex);
            }
        }

        ///<summary>
        /// Extracts a string from an array.
        ///</summary>
        ///<param name="n">
        /// The column No to be extracted.
        ///</param>
        ///<param name="key">
        /// The key of the collection to be extracted from.
        ///</param>
        ///<param name="fromKey">
        /// The separator the collection will be splited on.
        ///</param>
        public HttpEngine ExtractColumn(int n, string key = "", string fromKey = "")
        {
            if (!Initial("ExtractColumn()")) return this;
            object obj = fromKey != "" ? Mapper(fromKey) : Memory._Storage[Memory._Storage.Count - 1];

            try
            {
                if (obj is string[] == false)
                {
                    return Fault("Not a string[]!");
                }
                string s = ParsersExtractors.ParseGetColumn(obj as string[], n);
                AddStorage(s, key);
                return Success();
            }
            catch (Exception ex)
            {
                return Exceptional(ex);
            }
        }

        ///<summary>
        /// Concentrates last N elements from storage to string.
        ///</summary>
        ///<param name="lastNElements">
        /// The number of elements.
        ///</param>
        ///<param name="delimiter">
        /// The delimiter to be used.
        ///</param>
        ///<param name="key">
        /// The key to assign to the result.
        ///</param>
        public HttpEngine ExtractConcentrate(int lastNElements, string delimiter = "", string key = "")
        {
            if (!Initial("ExtractConcentrate()")) return this;
            try
            {
                string output = "";

                for (int i = lastNElements; i > 0; i--)
                {
                    object obj = Storage[Storage.Count - i];
                    if (obj is string == false)
                    {
                        return Fault("Not a string!");
                    }
                    output += (output == "") ? obj as string : delimiter + obj as string;
                }

                AddStorage(output, key);
                return Success();
            }
            catch (Exception ex)
            {
                return Exceptional(ex);
            }
        }

        ///<summary>
        /// Splits an array to strings in Storage.
        ///</summary>
        ///<param name="key">
        /// The key corresponding to the array.
        ///</param>
        ///<param name="keys">
        /// The key to assign to each result, followed by a number.
        /// Strings will be stored at [keys]1, [keys]2, [keys]3 ... e.g.
        ///</param>
        public HttpEngine ExtractSeparate(string key = "", string keys = "")
        {
            if (!Initial("ExtractSeparate()")) return this;
            object obj = key != "" ? Mapper(key) : Memory._Storage[Memory._Storage.Count - 1];
            if (obj is string[] == false)
            {
                return Fault("Not a string[]");
            }

            try
            {
                if (keys == "") { keys = "separated"; }
                string[] sep = obj as string[];
                for (int i = 0; i < sep.Count(); i++)
                {
                    AddStorage(sep[i], keys + (i + 1));
                }

                return Success();
            }
            catch (Exception ex)
            {
                return Exceptional(ex);
            }
        }

        ///<summary>
        /// Joins a collection into string.
        ///</summary>
        ///<param name="key">
        /// The key of the collection to be joined.
        ///</param>
        ///<param name="delimiter">
        /// The separator to be used between each value.
        ///</param>
        public HttpEngine ExtractJoin(string delimiter = "", string key = "")
        {
            if (!Initial("ParseJoin()")) return this;
            object obj = key != "" ? Mapper(key) : Memory._Storage[Memory._Storage.Count - 1];

            try
            {
                if (obj is string[] == false)
                {
                    return Fault("Not a string[]!");
                }
                if ((obj as string).Length == 0)
                {
                    return Fault("Empty string[]!");
                }
                obj = ParsersExtractors.ParseJoin(obj as string[], delimiter);
                AddStorage(obj, key);
                return Success();
            }
            catch (Exception ex)
            {
                return Exceptional(ex);
            }
        }

        ///<summary>
        /// Splits a string into an array.
        ///</summary>
        ///<param name="key">
        /// The key of the collection to be splitted.
        ///</param>
        ///<param name="delimiter">
        /// The separator the collection will be splited on.
        ///</param>
        public HttpEngine ExtractSplit(string delimiter = "", string key = "")
        {
            if (!Initial("ParseSplit()")) return this;
            object obj = key != "" ? Mapper(key) : Memory._Storage[Memory._Storage.Count - 1];

            try
            {
                if (obj is string == false)
                {
                    return Fault("Not a string!");
                }
                if ((obj as string).Length == 0)
                {
                    return Fault("Empty string!");
                }
                obj = ParsersExtractors.ParseSplit(obj as string, delimiter);
                AddStorage(obj, key);
                return Success();
            }
            catch (Exception ex)
            {
                return Exceptional(ex);
            }
        }






        //same funcs but as preparsers
        ///<summary>
        /// Replaces every number of whitespaces with a single space.
        ///</summary>
        ///<param name="key">
        /// The key corresponding to the object to parse.
        /// If not set, last Result will be parsed.
        ///</param>
        public HttpEngine PreParseReduceWhitespaces(string key = "")
        {
            if (!Initial("PreParseReduceWhitespaces()")) return this;
            object obj = key != "" ? Mapper(key) : Memory._Results[Memory._Results.Count - 1];


            if (obj is string == false)
            {
                return Fault("Value is not a string!");
            }
            try
            {
                object o = ParsersHtml.HtmlReduceToSpace(obj as string);

                if (key != "")
                {
                    for (int i = 0; i < Memory._Results.Count(); i++)
                    {
                        if (Memory._Results[i] == obj) Memory._Results[i] = o;
                    }
                    for (int i = 0; i < Memory._Storage.Count(); i++)
                    {
                        if (Memory._Storage[i] == obj) Memory._Storage[i] = o;
                    }
                }
                else
                {
                    Memory._Results[Memory._Results.Count - 1] = o;
                }

                return Success();
            }
            catch (Exception ex)
            {
                return Exceptional(ex);
            }
        }

        ///<summary>
        /// UrlEncode a string.
        ///</summary>
        ///<param name="key">
        /// The key corresponding to the object to parse.
        /// If not set, last Stored will be parsed.
        ///</param>
        public HttpEngine ParseUrlEncode(string key = "")
        {
            if (!Initial("ParseUrlEncode()")) return this;
            object obj = key != "" ? Mapper(key) : Memory._Storage[Memory._Storage.Count - 1];

            if (obj is string == false)
            {
                return Fault("Value is not a string!");
            }
            try
            {
                object o = ParsersExtractors.ParseUrlEncode(obj as string);

                if (key != "")
                {
                    for (int i = 0; i < Memory._Results.Count(); i++)
                    {
                        if (Memory._Results[i] == obj) Memory._Results[i] = o;
                    }
                    for (int i = 0; i < Memory._Storage.Count(); i++)
                    {
                        if (Memory._Storage[i] == obj) Memory._Storage[i] = o;
                    }
                }
                else
                {
                    Memory._Storage[Memory._Storage.Count - 1] = o;
                }

                return Success();
            }
            catch (Exception ex)
            {
                return Exceptional(ex);
            }
        }

        ///<summary>
        /// UrlDecode a string.
        ///</summary>
        ///<param name="key">
        /// The key corresponding to the object to parse.
        /// If not set, last Stored will be parsed.
        ///</param>
        public HttpEngine ParseUrlDecode(string key = "")
        {
            if (!Initial("ParseUrlDecode()")) return this;
            object obj = key != "" ? Mapper(key) : Memory._Storage[Memory._Storage.Count - 1];

            if (obj is string == false)
            {
                return Fault("Value is not a string!");
            }
            try
            {
                object o = ParsersExtractors.ParseUrlDecode(obj as string);

                if (key != "")
                {
                    for (int i = 0; i < Memory._Results.Count(); i++)
                    {
                        if (Memory._Results[i] == obj) Memory._Results[i] = o;
                    }
                    for (int i = 0; i < Memory._Storage.Count(); i++)
                    {
                        if (Memory._Storage[i] == obj) Memory._Storage[i] = o;
                    }
                }
                else
                {
                    Memory._Storage[Memory._Storage.Count - 1] = o;
                }

                return Success();
            }
            catch (Exception ex)
            {
                return Exceptional(ex);
            }
        }

        ///<summary>
        /// Parses a string as a post field pair.
        ///</summary>
        ///<param name="fieldName">
        /// The name of the field.
        ///</param>
        ///<param name="key">
        /// The key corresponding to the object to parse.
        /// If not set, last Stored will be parsed.
        ///</param>
        public HttpEngine ParseAsPostField(string fieldName, string key = "")
        {
            if (!Initial("ParseAsPostField()")) return this;
            object obj = key != "" ? Mapper(key) : Memory._Storage[Memory._Storage.Count - 1];

            if (obj is string == false)
            {
                return Fault("Value is not a string!");
            }
            try
            {
                object o = fieldName + '=' + ParsersExtractors.ParseUrlEncode(obj as string);

                if (key != "")
                {
                    for (int i = 0; i < Memory._Results.Count(); i++)
                    {
                        if (Memory._Results[i] == obj) Memory._Results[i] = o;
                    }
                    for (int i = 0; i < Memory._Storage.Count(); i++)
                    {
                        if (Memory._Storage[i] == obj) Memory._Storage[i] = o;
                    }
                }
                else
                {
                    Memory._Storage[Memory._Storage.Count - 1] = o;
                }
                return Success();
            }
            catch (Exception ex)
            {
                return Exceptional(ex);
            }
        }

        ///<summary>
        /// Parse string to lower case.
        ///</summary>
        ///<param name="key">
        /// The key corresponding to the object to parse.
        /// If not set, last Stored will be parsed.
        ///</param>
        public HttpEngine ParseToLower(string key = "")
        {
            if (!Initial("ParseToLower()")) return this;
            object obj = key != "" ? Mapper(key) : Memory._Storage[Memory._Storage.Count - 1];

            if (obj is string == false)
            {
                return Fault("Value is not a string!");
            }
            try
            {
                object o = (obj as string).ToLower();

                if (key != "")
                {
                    for (int i = 0; i < Memory._Results.Count(); i++)
                    {
                        if (Memory._Results[i] == obj) Memory._Results[i] = o;
                    }
                    for (int i = 0; i < Memory._Storage.Count(); i++)
                    {
                        if (Memory._Storage[i] == obj) Memory._Storage[i] = o;
                    }
                }
                else
                {
                    Memory._Storage[Memory._Storage.Count - 1] = o;
                }

                return Success();
            }
            catch (Exception ex)
            {
                return Exceptional(ex);
            }
        }

        ///<summary>
        /// Parse string to upper case.
        ///</summary>
        ///<param name="key">
        /// The key corresponding to the object to parse.
        /// If not set, last Stored will be parsed.
        ///</param>
        public HttpEngine ParseToUpper(string key = "")
        {
            if (!Initial("ParseToUpper()")) return this;
            object obj = key != "" ? Mapper(key) : Memory._Storage[Memory._Storage.Count - 1];

            if (obj is string == false)
            {
                return Fault("Value is not a string!");
            }
            try
            {
                object o = (obj as string).ToUpper();

                if (key != "")
                {
                    for (int i = 0; i < Memory._Results.Count(); i++)
                    {
                        if (Memory._Results[i] == obj) Memory._Results[i] = o;
                    }
                    for (int i = 0; i < Memory._Storage.Count(); i++)
                    {
                        if (Memory._Storage[i] == obj) Memory._Storage[i] = o;
                    }
                }
                else
                {
                    Memory._Storage[Memory._Storage.Count - 1] = o;
                }

                return Success();
            }
            catch (Exception ex)
            {
                return Exceptional(ex);
            }
        }

        ///<summary>
        /// Append string to the beggining.
        ///</summary>
        ///<param name="s">
        /// The string to append.
        ///</param>
        ///<param name="key">
        /// The key corresponding to the object to parse.
        /// If not set, last Stored will be parsed.
        ///</param>
        public HttpEngine ParsePreAppend(string s, string key = "")
        {
            if (!Initial("ParsePreAppend()")) return this;
            object obj = key != "" ? Mapper(key) : Memory._Storage[Memory._Storage.Count - 1];

            if (obj is string == false)
            {
                return Fault("Value is not a string!");
            }
            try
            {
                object o = s + (obj as string);

                if (key != "")
                {
                    for (int i = 0; i < Memory._Results.Count(); i++)
                    {
                        if (Memory._Results[i] == obj) Memory._Results[i] = o;
                    }
                    for (int i = 0; i < Memory._Storage.Count(); i++)
                    {
                        if (Memory._Storage[i] == obj) Memory._Storage[i] = o;
                    }
                }
                else
                {
                    Memory._Storage[Memory._Storage.Count - 1] = o;
                }

                return Success();
            }
            catch (Exception ex)
            {
                return Exceptional(ex);
            }
        }

        ///<summary>
        /// Append string to the end.
        ///</summary>
        ///<param name="s">
        /// The string to append.
        ///</param>
        ///<param name="key">
        /// The key corresponding to the object to parse.
        /// If not set, last Stored will be parsed.
        ///</param>
        public HttpEngine ParsePostAppend(string s, string key = "")
        {
            if (!Initial("ParsePostAppend()")) return this;
            object obj = key != "" ? Mapper(key) : Memory._Storage[Memory._Storage.Count - 1];

            if (obj is string == false)
            {
                return Fault("Value is not a string!");
            }
            try
            {
                object o = (obj as string) + s;

                if (key != "")
                {
                    for (int i = 0; i < Memory._Results.Count(); i++)
                    {
                        if (Memory._Results[i] == obj) Memory._Results[i] = o;
                    }
                    for (int i = 0; i < Memory._Storage.Count(); i++)
                    {
                        if (Memory._Storage[i] == obj) Memory._Storage[i] = o;
                    }
                }
                else
                {
                    Memory._Storage[Memory._Storage.Count - 1] = o;
                }

                return Success();
            }
            catch (Exception ex)
            {
                return Exceptional(ex);
            }
        }

        ///<summary>
        /// Replaces part of the string.
        ///</summary>
        ///<param name="oldString">
        /// The string be replaced.
        ///</param>
        ///<param name="newString">
        /// The string to replace with.
        ///</param>
        ///<param name="key">
        /// The key corresponding to the object to parse.
        /// If not set, last Stored will be parsed.
        ///</param>
        public HttpEngine ParseReplace(string oldString, string newString, string key = "")
        {
            if (!Initial("ParseReplace()")) return this;
            object obj = key != "" ? Mapper(key) : Memory._Storage[Memory._Storage.Count - 1];

            if (obj is string == false)
            {
                return Fault("Value is not a string!");
            }
            try
            {
                object o = ParsersExtractors.ParseReplace((obj as string), oldString, newString);

                if (key != "")
                {
                    for (int i = 0; i < Memory._Results.Count(); i++)
                    {
                        if (Memory._Results[i] == obj) Memory._Results[i] = o;
                    }
                    for (int i = 0; i < Memory._Storage.Count(); i++)
                    {
                        if (Memory._Storage[i] == obj) Memory._Storage[i] = o;
                    }
                }
                else
                {
                    Memory._Storage[Memory._Storage.Count - 1] = o;
                }

                return Success();
            }
            catch (Exception ex)
            {
                return Exceptional(ex);
            }
        }
    }
}
