using System;

namespace HttpVehicle
{
    public partial class HttpEngine
    {
        //CHECKERS/VALIDATORS         //set last Result to error if not valid

        ///<summary>
        /// Validates a given string equals string.
        ///</summary>
        ///<param name="toCompare">
        /// The string to comparre to.
        ///</param>
        ///<param name="key">
        /// The key that corresponds to the object to validate.
        /// If not setted, the last stored object in Storage will be saved.
        ///</param>
        public HttpEngine ValidateStringEquals(string toCompare, string key = "")
        {
            if (!Initial("ValidateStringEquals()")) return this;
            object obj = key != "" ? Mapper(key) : Memory._Storage[Memory._Storage.Count - 1];

            if (obj is string == false)
            {
                return Fault("Last result is not string!");
            }
            try
            {
                if (Validators.ValidateStringEquals((obj as string), toCompare) == true)
                {
                    return Success("true");
                }
                return Fault("false");
            }
            catch (Exception ex)
            {
                return Exceptional(ex);
            }
        }

        ///<summary>
        /// Validates a given string length looks correct.
        ///</summary>
        ///<param name="min">
        /// Minimum acceptable length.
        ///</param>
        ///<param name="max">
        /// Maximum acceptable length.
        ///</param>
        ///<param name="key">
        /// The key that corresponds to the object to validate.
        /// If not setted, the last stored object in Storage will be saved.
        ///</param>
        public HttpEngine ValidateStringLength(int min, int max, string key = "")
        {
            if (!Initial("ValidateStringLength()")) return this;
            object obj = key != "" ? Mapper(key) : Memory._Storage[Memory._Storage.Count - 1];

            if (obj is string == false)
            {
                return Fault("Last result is not string!");
            }
            try
            {
                if (Validators.ValidateStringLength((obj as string), min, max) == true)
                {
                    return Success("true");
                }
                return Fault("false");
            }
            catch (Exception ex)
            {
                return Exceptional(ex);
            }
        }

        ///<summary>
        /// Validates a given string contains string.
        ///</summary>
        ///<param name="toCompare">
        /// The string to comparre to.
        ///</param>
        ///<param name="key">
        /// The key that corresponds to the object to validate.
        /// If not setted, the last stored object in Storage will be saved.
        ///</param>
        public HttpEngine ValidateStringContains(string toCompare, string key = "")
        {
            if (!Initial("ValidateStringContains()")) return this;
            object obj = key != "" ? Mapper(key) : Memory._Storage[Memory._Storage.Count - 1];

            if (obj is string == false)
            {
                return Fault("Last result is not string!");
            }
            try
            {
                if ((obj as string).Contains(toCompare))
                {
                    return Success("true");
                }
                return Fault("false");
            }
            catch (Exception ex)
            {
                return Exceptional(ex);
            }
        }

        ///<summary>
        /// Validates a given string is in valid url format.
        ///</summary>
        ///<param name="key">
        /// The key that corresponds to the object to validate.
        /// If not setted, the last stored object in Storage will be saved.
        ///</param>
        public HttpEngine ValidateUrl(string key = "")
        {
            if (!Initial("ValidateUrl()")) return this;
            object obj = key != "" ? Mapper(key) : Memory._Storage[Memory._Storage.Count - 1];

            if (obj is string == false)
            {
                return Fault("Last result is not string!");
            }
            try
            {
                if (Validators.ValidateUrl((obj as string)) == true)
                {
                    return Success("true");
                }
                return Fault("false");
            }
            catch (Exception ex)
            {
                return Exceptional(ex);
            }
        }

        ///<summary>
        /// Validates a given string is in valid Ip format.
        ///</summary>
        ///<param name="key">
        /// The key that corresponds to the object to validate.
        /// If not setted, the last stored object in Storage will be saved.
        ///</param>
        public HttpEngine ValidateIp(string key = "")
        {
            if (!Initial("ValidateIp()")) return this;
            object obj = key != "" ? Mapper(key) : Memory._Storage[Memory._Storage.Count - 1];

            if (obj is string == false)
            {
                return Fault("Last result is not string!");
            }
            try
            {
                if (Validators.ValidateIp((obj as string)) == true)
                {
                    return Success("true");
                }
                return Fault("false");
            }
            catch (Exception ex)
            {
                return Exceptional(ex);
            }
        }

        ///<summary>
        /// Validates a given string is in valid [Ip]:[port] format.
        ///</summary>
        ///<param name="key">
        /// The key that corresponds to the object to validate.
        /// If not setted, the last stored object in Storage will be saved.
        ///</param>
        public HttpEngine ValidateIpPort(string key = "")
        {
            if (!Initial("ValidateIpPort()")) return this;
            object obj = key != "" ? Mapper(key) : Memory._Storage[Memory._Storage.Count - 1];

            if (obj is string == false)
            {
                return Fault("Last result is not string!");
            }
            try
            {
                if (Validators.ValidateIpPort((obj as string)) == true)
                {
                    return Success("true");
                }
                return Fault("false");
            }
            catch (Exception ex)
            {
                return Exceptional(ex);
            }
        }

        ///<summary>
        /// Validates a given host is up..
        ///</summary>
        ///<param name="keyOrString">
        /// The key that corresponds to the object to validate.
        /// the string must be in format [Ip]:[Port] or [Ip]:[Port]:[Username]:[Password].
        ///</param>
        public HttpEngine ValidateHostUp(string keyOrString = "")
        {
            if (!Initial("ValidateHostUp()")) return this;
            if (keyOrString == "")
            {
                keyOrString = Proxy;
            }
            else if (Memory._Map.ContainsKey(keyOrString))
            {
                if (Mapper(keyOrString) is string == false)
                {
                    return Fault(string.Format("Stored at {0} is not string!", keyOrString));
                }
                keyOrString = Mapper(keyOrString) as string;
            }
            try
            {
                if (Validators.ValidateHostUp(keyOrString) == true)
                {
                    return Success("true");
                }
                return Fault("false");
            }
            catch (Exception ex)
            {
                return Exceptional(ex);
            }
        }
    }
}
