using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace HttpVehicle
{
    class Informers
    {
        public static string CreateUserAgent()
        {
            string[] uas = Informers.chopEnbeddedString("HttpVehicle.Data.UserAgents.txt");
            int ran = CreateRandInt(1, uas.Count());
            return uas[ran];
        }
        public static string CreatePassword(int minLenght = 12, int maxLenght = 16, string flag = "3")
        {
            string pool = @"023456789";
            switch (flag)
            {
                case "0":
                    break;
                case "1":
                    pool = @"abcdefghijklmnopqrstuvwxyz";
                    break;
                case "2":
                    pool = @"abcdefghijklmnopqrstuvwxyz";
                    break;
                case "3":
                    pool = @"AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwZxYyZz";
                    break;
                case "4":
                    pool = @"AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwZxYyZz1234567890";
                    break;
                case "5":
                    pool = @"AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwZxYyZz1234567890!@#$%^&*()_+{},./\|][";
                    break;
                default:
                    if (String.IsNullOrEmpty(flag) == false && String.IsNullOrWhiteSpace(flag) == false)
                    {
                        string newPool = "";
                        if (flag.ToLower().Contains("numeric")) { newPool += @"023456789"; }
                        if (flag.ToLower().Contains("alpha")) { newPool += @"abcdefghijklmnopqrstuvwxyz"; }
                        if (flag.ToLower().Contains("upper")) { newPool += @"ABCDEFGHIJKLMNOPQRSTUVWXYZ"; }
                        if (flag.ToLower().Contains("symbolic")) { newPool += @"!@#$%^&*()_+ {},./\|]["; }
                        if (newPool != "") { pool = newPool; }
                    }
                    break;
            }
            int lenght = CreateRandInt(minLenght, maxLenght);

            return CreateRandString(lenght, pool);
        }
        public static string CreatePasswordBG(int minLenght = 12, int maxLenght = 16, string flag = "3")
        {
            string pool = @"023456789";
            switch (flag)
            {
                case "0":
                    break;
                case "1":
                    pool = @"абвгдежзийклмнопрстуфхцчшщъьюя";
                    break;
                case "2":
                    pool = @"абвгдежзийклмнопрстуфхцчшщъьюя";
                    break;
                case "3":
                    pool = @"АБВГДЕЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЮЯабвгдежзийклмнопрстуфхцчшщъьюя";
                    break;
                case "4":
                    pool = @"АБВГДЕЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЮЯабвгдежзийклмнопрстуфхцчшщъьюя1234567890";
                    break;
                case "5":
                    pool = @"АБВГДЕЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЮЯабвгдежзийклмнопрстуфхцчшщъьюя1234567890!@#$%^&*()_+{},./\|][";
                    break;
                default:
                    if (String.IsNullOrEmpty(flag) == false && String.IsNullOrWhiteSpace(flag) == false)
                    {
                        string newPool = "";
                        if (flag.ToLower().Contains("numeric")) { newPool += @"023456789"; }
                        if (flag.ToLower().Contains("alpha")) { newPool += @"абвгдежзийклмнопрстуфхцчшщъьюя"; }
                        if (flag.ToLower().Contains("upper")) { newPool += @"АБВГДЕЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЮЯ"; }
                        if (flag.ToLower().Contains("symbolic")) { newPool += @"!@#$%^&*()_+ {},./\|]["; }
                        if (newPool != "") { pool = newPool; }
                    }
                    break;
            }
            int lenght = CreateRandInt(minLenght, maxLenght);

            return CreateRandString(lenght, pool);
        }
        public static string CreateNameEN()
        {
            int sex = CreateRandInt(1, 3);
            string[] names;
            if (sex == 1) { names = Informers.chopEnbeddedString("HttpVehicle.Data.Names_1M_UK.txt"); }
            else { names = Informers.chopEnbeddedString("HttpVehicle.Data.Names_1F_UK.txt"); }
            int ran = CreateRandInt(1, names.Count());
            string name = names[ran];

            string[] sirs = Informers.chopEnbeddedString("HttpVehicle.Data.Names_2_UK.txt");
            int ran2 = CreateRandInt(1, sirs.Count());
            string sir = sirs[ran];

            return name + " " + sir;
        }
        public static string CreateNameBG()
        {
            int sex = CreateRandInt(1, 3);
            string[] names;
            if (sex == 1) { names = Informers.chopEnbeddedString("HttpVehicle.Data.Names_1M_BG.txt"); }
            else { names = Informers.chopEnbeddedString("HttpVehicle.Data.Names_1F_UK.txt"); }
            int ran = CreateRandInt(1, names.Count());
            string name = names[ran];

            string[] sirs;
            if (sex == 1) { sirs = Informers.chopEnbeddedString("HttpVehicle.Data.Names_1F_BG.txt"); }
            else { sirs = Informers.chopEnbeddedString("HttpVehicle.Data.Names_2F_BG.txt"); }
            int ran2 = CreateRandInt(1, sirs.Count());
            string sir = sirs[ran];

            return name + " " + sir;
        }
        public static string CreateNameRU()
        {
            int sex = CreateRandInt(1, 3);
            string[] names;
            if (sex == 1) { names = Informers.chopEnbeddedString("HttpVehicle.Data.Names_1M_RU.txt"); }
            else { names = Informers.chopEnbeddedString("HttpVehicle.Data.Names_1F_RU.txt"); }
            int ran = CreateRandInt(1, names.Count());
            string name = names[ran];

            string[] sirs;
            if (sex == 1) { sirs = Informers.chopEnbeddedString("HttpVehicle.Data.Names_1F_RU.txt"); }
            else { sirs = Informers.chopEnbeddedString("HttpVehicle.Data.Names_2F_RU.txt"); }
            int ran2 = CreateRandInt(1, sirs.Count());
            string sir = sirs[ran];

            return name + " " + sir;
        }
        public static string CreateNameBGLatin()
        {
            int sex = CreateRandInt(1, 3);
            string[] names;
            if (sex == 1) { names = Informers.chopEnbeddedString("HttpVehicle.Data.Names_1M_BG.txt"); }
            else { names = Informers.chopEnbeddedString("HttpVehicle.Data.Names_1F_UK.txt"); }
            int ran = CreateRandInt(1, names.Count());
            string name = names[ran];

            string[] sirs;
            if (sex == 1) { sirs = Informers.chopEnbeddedString("HttpVehicle.Data.Names_1F_BG.txt"); }
            else { sirs = Informers.chopEnbeddedString("HttpVehicle.Data.Names_2F_BG.txt"); }
            int ran2 = CreateRandInt(1, sirs.Count());
            string sir = sirs[ran];

            return ParsersExtractors.ParseBGtoLatin(name + " " + sir);
        }
        public static string CreateNameRULatin()
        {
            int sex = CreateRandInt(1, 3);
            string[] names;
            if (sex == 1) { names = Informers.chopEnbeddedString("HttpVehicle.Data.Names_1M_RU.txt"); }
            else { names = Informers.chopEnbeddedString("HttpVehicle.Data.Names_1F_RU.txt"); }
            int ran = CreateRandInt(1, names.Count());
            string name = names[ran];

            string[] sirs;
            if (sex == 1) { sirs = Informers.chopEnbeddedString("HttpVehicle.Data.Names_1F_RU.txt"); }
            else { sirs = Informers.chopEnbeddedString("HttpVehicle.Data.Names_2F_RU.txt"); }
            int ran2 = CreateRandInt(1, sirs.Count());
            string sir = sirs[ran];

            return ParsersExtractors.ParseRUtoLatin(name + " " + sir);
        }
        public static string CreateUserName()
        {
            string user = CreateNameEN().Replace(" ", string.Empty).ToLower() + CreateRandInt(11, 99).ToString();
            return user;
        }
        public static string CreateUserNameBG()
        {
            string user = CreateNameBG().Replace(" ", string.Empty).ToLower() + CreateRandInt(11, 99).ToString();
            return user;
        }
        public static string CreateUserNameBGLatin()
        {
            string user = CreateNameBGLatin().Replace(" ", string.Empty).ToLower() + CreateRandInt(11, 99).ToString();
            return user;
        }
        public static string CreateUserNameRU()
        {
            string user = CreateNameRU().Replace(" ", string.Empty).ToLower() + CreateRandInt(11, 99).ToString();
            return user;
        }
        public static string CreateUserNameRULatin()
        {
            string user = CreateNameRULatin().Replace(" ", string.Empty).ToLower() + CreateRandInt(11, 99).ToString();
            return user;
        }
        public static string CreateEmail(string domain = "@gmail.com")
        {
            string user = CreateNameEN().Replace(" ", string.Empty).ToLower();
            return user + domain;
        }
        public static string CreateEmailBG(string domain = "@gmail.com")
        {
            string user = CreateUserNameBGLatin().Replace(" ", string.Empty).ToLower();
            return user + domain;
        }
        public static string CreatePhoneBG()
        {
            try
            {
                return "+3598" + new Random().Next(7, 10).ToString() + new Random().Next(1000000, 9999999).ToString();
            }
            catch { return "error"; }
        }

        public static int CreateRandInt(int min, int max)
        {
            try
            {
                Random ran = new Random(); return ran.Next(min, max);
            }
            catch { return max; }
        }
        public static string CreateRandString(int lenght, string pool = @"abcdefghijklmnopqrstuvwxyz")
        {
            try
            {
                string a = "";
                Random ran = new Random();
                for (int i = 1; i < lenght + 1; i++) { a = a + pool[ran.Next(0, pool.Length)]; }
                return a;
            }
            catch { return ""; }
        }
        public static string CreateRandHex(int lenght)
        {
            try
            {
                string pool = @"1234567890abcdef";
                string a = "";
                Random ran = new Random();
                for (int i = 1; i < lenght + 1; i++) { a = a + pool[ran.Next(0, pool.Length)]; }
                return a;
            }
            catch { return ""; }
        }
        public static string CreateRandUpperHex(int lenght)
        {
            try
            {
                string pool = @"1234567890ABCDEF";
                string a = "";
                Random ran = new Random();
                for (int i = 1; i < lenght + 1; i++) { a = a + pool[ran.Next(0, pool.Length)]; }
                return a;
            }
            catch { return ""; }
        }

        static string extractEnbeddedString(string name)
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            StreamReader str = new StreamReader(asm.GetManifestResourceStream(name));
            return str.ReadToEnd();
        }
        static string[] chopEnbeddedString(string name)
        {
            string str = extractEnbeddedString(name);
            string[] result = str.Split(new string[] { "\", \"" }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < result.Count(); i++)
            {
                result[i] = result[i].Replace("\"", "");
            }
            return result;
        }
    }
}

//TODO : public static string GetMePassGetPhrase(int minWords = 9, string flag = "2")
//TODO : random hashes; 