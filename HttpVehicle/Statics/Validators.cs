using System;
using System.Collections.Generic;
using System.IO;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Security.AccessControl;
using System.Text.RegularExpressions;

namespace HttpVehicle
{
    class Validators
    {
        public static bool ValidateUrl(string url)
        {
            string invalidChars = @"[^-\]_.~!*'();:@&=+$,/?%#[A-z0-9]";
            Regex Rx = new Regex(invalidChars, RegexOptions.IgnoreCase);
            return !Rx.IsMatch(url);
        }
        public static bool ValidateIp(string ip)
        {
            string invalidChars = @"25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)(\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)){3}";
            Regex Rx = new Regex(invalidChars, RegexOptions.IgnoreCase);
            return !Rx.IsMatch(ip);
        }
        public static bool ValidateIpPort(string ip)
        {
            string invalidChars = @"25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)(\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)){3}:[0-9]{1,5}";
            Regex Rx = new Regex(invalidChars, RegexOptions.IgnoreCase);
            return !Rx.IsMatch(ip);
        }
        public static bool ValidateStringEquals(string compare, string toCompare)
        {
            if (compare == toCompare) return true;
            return false;
        }
        public static bool ValidateStringContains(string compare, string toCompare)
        {
            if (compare.Contains(toCompare)) return true;
            return false;
        }
        public static bool ValidateStringLength(string s, int min, int max)
        {
            if(string.IsNullOrEmpty(s) || 
                string.IsNullOrWhiteSpace(s) || 
                s.Length < min || 
                s.Length > max) return false;
            return true;
        }

        public static bool ValidateFolderExists(string path)
        {
            try
            {
                if (Directory.Exists(path) == true)
                {
                    return true;
                }
                else { return false; }
            }
            catch
            {
                return false;
            }
        }
        public static bool ValidateFileExists(string path)
        {
            try
            {
                if (File.Exists(path) == true)
                {
                    return true;
                }
                else { return false; }
            }
            catch
            {
                return false;
            }
        }
        public static bool ValidateLocalFileExists(string fileName)
        {
            string path = new Uri(Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase)).LocalPath + @"\" + fileName;
            return ValidateFileExists(fileName);
        }
        public static bool ValidateWriteAccess(string path)
        {
            try
            {
                DirectorySecurity ds = Directory.GetAccessControl(path);
                return true;
            }
            catch (UnauthorizedAccessException)
            {
                return false;
            }
        }
        public static bool ValidateLocalWriteAccess(string fileName)
        {
            string path = new Uri(Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase)).LocalPath + @"\" + fileName;
            return ValidateWriteAccess(path);
        }
        public static bool ValidateReadAccess(string folderPath)
        {
            try
            {
                Directory.GetFiles(folderPath);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool ValidateResult(object result)
        {
            bool output = true;

            if (result == null)
            {
                output = false;
            }
            if (result is string)
            {
                string res = result as string;
                if (String.IsNullOrEmpty(res)) output = false;
                else if (String.IsNullOrWhiteSpace(res)) output = false;
                else if (res.ToLower() == "error") output = false;
            }
            else if (result is string[])
            {
                string res = String.Join(Environment.NewLine, result as string[]);
                if (String.IsNullOrEmpty(res)) output = false;
                else if (String.IsNullOrWhiteSpace(res)) output = false;
                else if (res.ToLower() == "error") output = false;
            }
            else if (result is byte[])
            {
                byte[] res = result as byte[];
                if (res.Length == 0) output = false;
            }

            return output;
        }
        public static bool ValidateLackOfError(List<object> results)
        {
            bool output = true;

            if (results[results.Count - 1] is string)
            {
                if (results[results.Count - 1] as string == "error")
                {
                    output = false;
                }
            }

            return output;
        }

        public static bool ValidateHostUp(string address)
        {
            //string[] sep = address.Split(':');
            PingReply reply = new Ping().Send(address.ToString(), 7000);
            if (reply.Status == IPStatus.Success)
            {
                return true;
            }
            return false;
        }
    }
}