using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;

namespace HttpVehicle
{
    class ParsersExtractors
    {
        //Create regex class
        //extract with Regex
        //replace with Regex

        public static string ExtractCookieValue(CookieCollection allCookies, string name)
        {
            foreach (Cookie ck in allCookies)
            {
                if (ck.Name == name)
                {
                    return ck.Value;
                }
            }
            return null;
        }
        public static CookieCollection ExtractAllCookies(CookieContainer container)
        {
            var allCookies = new CookieCollection();
            var domainTableField = container.GetType().GetRuntimeFields().FirstOrDefault(x => x.Name == "m_domainTable");
            var domains = (IDictionary)domainTableField.GetValue(container);

            foreach (var val in domains.Values)
            {
                var type = val.GetType().GetRuntimeFields().First(x => x.Name == "m_list");
                var values = (IDictionary)type.GetValue(val);
                foreach (CookieCollection cookies in values.Values)
                {
                    allCookies.Add(cookies);
                }
            }
            return allCookies;
        }
        public static string[] ExtractAllCookies(CookieCollection collection)
        {
            List<string> otp = new List<string>();
            foreach (HttpCookie c in collection)
            {
                string s = "";
                s += (s == "") ? "Domain : " + c.Domain : Environment.NewLine + "Domain : " + c.Domain;
                s += (s == "") ? "Expires : " + c.Expires.ToLongTimeString() : Environment.NewLine + "Domain : " + c.Expires.ToLongTimeString();
                s += (s == "") ? "Name : " + c.Name : Environment.NewLine + "Domain : " + c.Name;
                s += (s == "") ? "Path : " + c.Path : Environment.NewLine + "Domain : " + c.Path;
                s += (s == "") ? "Secure : " + c.Secure : Environment.NewLine + "Domain : " + c.Secure;

                NameValueCollection vals = c.Values;
                foreach (KeyValuePair<string, string> kvp in vals)
                {
                    s += Environment.NewLine + "Value -> " + kvp.Key + " : " + kvp.Value;
                }
                otp.Add(s);
            }
            return otp.ToArray();
        }
        public static string ExtractSeparatedString(string text, string before, string after)
        {
            string s = text.Split(new string[] { before }, StringSplitOptions.None)[1]
                       .Split(new string[] { after }, StringSplitOptions.None)[0];
            return s;
        }
        public static string ExtractUrl(string separator, string page)
        {
            if (page.Contains(separator) == false) { return ""; }

            string invalidChars = @"[^-\]_.~!*'();:@&=+$,/?%#[A-z0-9]";
            Regex Rx = new Regex(invalidChars, RegexOptions.IgnoreCase);

            string catched = page.Split(new string[] { separator }, StringSplitOptions.None)[1];
            int lenght = 0;
            for (int i = 0; i < catched.Length; i++)
            {
                if (Rx.IsMatch(catched[i].ToString())) { break; }
                lenght++;
            }

            return catched.Substring(0, lenght);
        }
        //ExtractLocalUrl
        //ExtractEmail
        public static string[] ExtractIpPortPairs(string page)
        {
            string ipPort = @"[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}[\s\S]*?[0-9]{1,5}";
            Regex Rx = new Regex(ipPort, RegexOptions.Singleline);
            List<string> catched = new List<string>();


            string pattern = @"(?<=[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3})[^0-9][\s\S]*?(?=[0-9]{1,5})";
            Regex Rx1 = new Regex(pattern, RegexOptions.Singleline);
            foreach (Match ItemMatch in Rx.Matches(page))
            {
                string match = ItemMatch.ToString();
                string sep = Rx1.Match(match).ToString();
                catched.Add(ItemMatch.ToString().Replace(sep, ":"));
            }


            return catched.ToArray();
        }
        public static string[] ExtractSeparatedIpPortPairs(string page, string separator)
        {
            string ipPort = @"[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}" + Regex.Escape(separator) + "[0-9]{1,5}";
            Regex Rx = new Regex(ipPort, RegexOptions.Singleline);
            List<string> catched = new List<string>();

            foreach (Match ItemMatch in Rx.Matches(page))
            {
                catched.Add(ItemMatch.ToString().Replace(separator, ":"));
            }


            return catched.ToArray();
        }
        //ExtractIpPortUserPassPairs
        //ExtractSeparatedIpPortUserPassPairs
        //ExtractUrls
        //ExtractSeparatedUrls
        //ExtractEmails
        //ExtractSeparatedEmails
        //ExtractSeparatedStrings
        //ExtractRandom
        


        public static string ParseJoin(string[] s, string delimiter)
        {
            return string.Join(delimiter, s);
        }
        public static string[] ParseSplit(string s, string delimiter)
        {
            return s.Split(new string[] { delimiter }, StringSplitOptions.None);
        }
        public static string ParseGetColumn(string[] cols, int n)
        {
            if (cols.Count() < n) return null;
            return cols[n - 1];
        }
        public static string[] ParseGetColumn(string[][] cols, int n)
        {
            List<string> output = new List<string>();
            foreach (string[] line in cols)
            {
                if (line.Count() >= n)
                {
                    output.Add(line[n - 1]);
                }
            }
            return output.ToArray();
        }
        public static string[] ParseRemoveColumn(string[] cols, int n)
        {
            if (cols.Count() < n) return null;
            List<string> all = cols.ToList();
            all.Remove(all[n - 1]);
            return all.ToArray();
        }
        public static string[][] ParseRemoveColumn(string[][] cols, int n)
        {
            List<string[]> output = new List<string[]>();
            foreach (string[] line in cols)
            {
                if (line.Count() >= n)
                {
                    List<string> curline = line.ToList();
                    curline.Remove(curline[n - 1]);
                    output.Add(curline.ToArray());
                }
            }
            return output.ToArray();
        }

        //ParseRemoveByNContaining
        //ParseRemoveByNNotContaining
        //ParseRemoveByNIs
        //ParseRemoveByNIsNot
        //ParseGetByNContaining
        //ParseGetByNNotContaining
        //ParseGetByNIs
        //ParseGetByNIsNot

        public static string ParseReplace(string s, string oldstr, string newstr)
        {
            return s.Replace(oldstr, newstr);
        }
        public static string ParseRemove(string s, string oldstr)
        {
            return s.Replace(oldstr, "");
        }
        //ParseReplaceDelimited
        //ParseRemoveDelimited
        public static string ParseBGtoLatin(string s)
        {
            try
            {
                string o = "";
                for (int i = 0; i < s.Count(); i++)
                {
                    switch (s[i])
                    {
                        case 'А': o += 'A'; break;
                        case 'а': o += 'a'; break;
                        case 'Б': o += 'B'; break;
                        case 'б': o += 'b'; break;
                        case 'В': o += 'V'; break;
                        case 'в': o += 'v'; break;
                        case 'Г': o += 'G'; break;
                        case 'г': o += 'g'; break;
                        case 'Д': o += 'D'; break;
                        case 'д': o += 'd'; break;
                        case 'Е': o += 'E'; break;
                        case 'е': o += 'e'; break;
                        case 'Ж': o += 'J'; break;
                        case 'ж': o += 'j'; break;
                        case 'З': o += 'Z'; break;
                        case 'з': o += 'z'; break;
                        case 'И': o += 'I'; break;
                        case 'и': o += 'i'; break;
                        case 'Й': o += 'I'; break;
                        case 'й': o += 'i'; break;
                        case 'К': o += 'K'; break;
                        case 'к': o += 'k'; break;
                        case 'Л': o += 'L'; break;
                        case 'л': o += 'l'; break;
                        case 'М': o += 'M'; break;
                        case 'м': o += 'm'; break;
                        case 'Н': o += 'N'; break;
                        case 'н': o += 'n'; break;
                        case 'О': o += 'O'; break;
                        case 'о': o += 'o'; break;
                        case 'П': o += 'P'; break;
                        case 'п': o += 'p'; break;
                        case 'Р': o += 'R'; break;
                        case 'р': o += 'r'; break;
                        case 'С': o += 'S'; break;
                        case 'с': o += 's'; break;
                        case 'Т': o += 'T'; break;
                        case 'т': o += 't'; break;
                        case 'У': o += 'U'; break;
                        case 'у': o += 'u'; break;
                        case 'Ф': o += 'F'; break;
                        case 'ф': o += 'f'; break;
                        case 'Х': o += 'H'; break;
                        case 'х': o += 'h'; break;
                        case 'Ц': o += "Ts"; break;
                        case 'ц': o += "ts"; break;
                        case 'Ч': o += "Ch"; break;
                        case 'ч': o += "ch"; break;
                        case 'Ш': o += "Sh"; break;
                        case 'ш': o += "sh"; break;
                        case 'Щ': o += "Sht"; break;
                        case 'щ': o += "sht"; break;
                        case 'Ъ': o += 'A'; break;
                        case 'ъ': o += 'a'; break;
                        case 'Ь': o += 'I'; break;
                        case 'ь': o += 'i'; break;
                        case 'Ю': o += "Iu"; break;
                        case 'ю': o += "iu"; break;
                        case 'Я': o += "Ia"; break;
                        case 'я': o += "ia"; break;
                        default: o += s[i]; break;
                    }
                }
                return o;
            }
            catch { return "error"; }
        }
        public static string ParseRUtoLatin(string s)
        {
            try
            {
                string o = "";
                for (int i = 0; i < s.Count(); i++)
                {
                    switch (s[i])
                    {
                        case 'А': o += 'A'; break;
                        case 'а': o += 'a'; break;
                        case 'Б': o += 'B'; break;
                        case 'б': o += 'b'; break;
                        case 'В': o += 'V'; break;
                        case 'в': o += 'v'; break;
                        case 'Г': o += 'G'; break;
                        case 'г': o += 'g'; break;
                        case 'Д': o += 'D'; break;
                        case 'д': o += 'd'; break;
                        case 'E': o += (i == 0 || s[i - 1] == 'Ь' || s[i - 1] == 'ь' || s[i - 1] == 'Ъ' || s[i - 1] == 'ъ') ? "Ye" : "E"; break;
                        case 'е': o += (i == 0 || s[i - 1] == 'Ь' || s[i - 1] == 'ь' || s[i - 1] == 'Ъ' || s[i - 1] == 'ъ') ? "ye" : "e"; break;
                        case 'Ё': o += "Yo"; break;
                        case 'ё': o += "yo"; break;
                        case 'Ж': o += "Zh"; break;
                        case 'ж': o += "zh"; break;
                        case 'З': o += 'Z'; break;
                        case 'з': o += 'z'; break;
                        case 'И': o += 'I'; break;
                        case 'и': o += 'i'; break;
                        case 'Й': o += 'I'; break;
                        case 'й': o += 'i'; break;
                        case 'К': o += 'K'; break;
                        case 'к': o += 'k'; break;
                        case 'Л': o += 'L'; break;
                        case 'л': o += 'l'; break;
                        case 'М': o += 'M'; break;
                        case 'м': o += 'm'; break;
                        case 'Н': o += 'N'; break;
                        case 'н': o += 'n'; break;
                        case 'О': o += 'O'; break;
                        case 'о': o += 'o'; break;
                        case 'П': o += 'P'; break;
                        case 'п': o += 'p'; break;
                        case 'Р': o += 'R'; break;
                        case 'р': o += 'r'; break;
                        case 'С': o += 'S'; break;
                        case 'с': o += 's'; break;
                        case 'Т': o += 'T'; break;
                        case 'т': o += 't'; break;
                        case 'У': o += 'U'; break;
                        case 'у': o += 'u'; break;
                        case 'Ф': o += 'F'; break;
                        case 'ф': o += 'f'; break;
                        case 'Х': o += "Kh"; break;
                        case 'х': o += "kh"; break;
                        case 'Ц': o += "Ts"; break;
                        case 'ц': o += "ts"; break;
                        case 'Ч': o += "Ch"; break;
                        case 'ч': o += "ch"; break;
                        case 'Ш': o += "Sh"; break;
                        case 'ш': o += "sh"; break;
                        case 'Щ': o += "Shch"; break;
                        case 'щ': o += "shch"; break;
                        case 'Ъ': o += (i > 0 && (
                                  s[i - 1] == 'А' || s[i - 1] == 'а' || 
                                  s[i - 1] == 'И' || s[i - 1] == 'и' ||
                                  s[i - 1] == 'О' || s[i - 1] == 'о' ||
                                  s[i - 1] == 'У' || s[i - 1] == 'у' ||
                                  s[i - 1] == 'Ы' || s[i - 1] == 'ы' ||
                                  s[i - 1] == 'З' || s[i - 1] == 'э')) ? "Y" : ""; break;
                        case 'ъ':
                            o += (i > 0 && (
                                  s[i - 1] == 'А' || s[i - 1] == 'а' ||
                                  s[i - 1] == 'И' || s[i - 1] == 'и' ||
                                  s[i - 1] == 'О' || s[i - 1] == 'о' ||
                                  s[i - 1] == 'У' || s[i - 1] == 'у' ||
                                  s[i - 1] == 'Ы' || s[i - 1] == 'ы' ||
                                  s[i - 1] == 'З' || s[i - 1] == 'э')) ? "y" : ""; break;
                        case 'Ы': o += 'Y'; break;
                        case 'ы': o += 'y'; break;  
                        case 'Ь':
                            o += (i > 0 && (
                                  s[i - 1] == 'А' || s[i - 1] == 'а' ||
                                  s[i - 1] == 'И' || s[i - 1] == 'и' ||
                                  s[i - 1] == 'О' || s[i - 1] == 'о' ||
                                  s[i - 1] == 'У' || s[i - 1] == 'у' ||
                                  s[i - 1] == 'Ы' || s[i - 1] == 'ы' ||
                                  s[i - 1] == 'З' || s[i - 1] == 'э')) ? "Y" : ""; break;
                        case 'ь':
                            o += (i > 0 && (
                                  s[i - 1] == 'А' || s[i - 1] == 'а' ||
                                  s[i - 1] == 'И' || s[i - 1] == 'и' ||
                                  s[i - 1] == 'О' || s[i - 1] == 'о' ||
                                  s[i - 1] == 'У' || s[i - 1] == 'у' ||
                                  s[i - 1] == 'Ы' || s[i - 1] == 'ы' ||
                                  s[i - 1] == 'З' || s[i - 1] == 'э')) ? "Y" : ""; break;
                        case 'Ю': o += "Yu"; break;
                        case 'ю': o += "yu"; break;
                        case 'Я': o += "Ya"; break;
                        case 'я': o += "ya"; break;
                        default: o += s[i]; break;
                    }
                }
                return o;
            }
            catch { return "error"; }
        }
        public static string ParseUrlEncode(string s)
        {
            return HttpUtility.UrlEncode(s);
        }
        public static string ParseUrlDecode(string s)
        {
            return HttpUtility.UrlDecode(s);
        }
    }
}