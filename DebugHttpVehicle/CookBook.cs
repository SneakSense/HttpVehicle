using System;
using HttpVehicle;

namespace DebugHttpVehicle
{
    ///<summary>
    /// All 'bout the money.
    ///</summary>
    public class CookBook
    {
        //public static void freebitco_in_Reg()
        //{
        //    object result;
        //    CookieCollection jar;

        //    string log = new HttpEngine()

        //        //.PopProxy("PublicProxies.txt")
        //        .SetTimeout(5000)
        //        .ProvideUserAgent()

        //        .GetRequest(@"https://freebitco.in/")

        //        .ExtractCookieValue("csrf_token")
        //        .ParseAsPostField("csrf_token")
        //        .SetData("op=signup")
        //        .Load(@"btca.txt")
        //        .ProvidePassword()
        //        .ProvideEmail()                                                                             //?
        //        .SetData("adcopy_challenge=")                                                               //?
        //        .ProvideRandomHex(32)                                                                       //?
        //        .ParseAsPostField("fingerprint")
        //        .SetData("referrer=0")
        //        //.GetResult(out result)
        //        //extractString...
        //        //.SetDataAsPostField("token", ParsersExtractors.ExtractSeparatedString(result as string, "$(\"#signup_token\").val(\"", "\"); "))
        //        .SetData("g_recaptcha_response=")


        //        //.PostRequest()
        //        //.GetResult(out result)
        //        .Log;
        //    bool fsfsf = false;



        //    //csrf_token=tBugn8D5UWiK
        //    //&op=signup
        //    //&btc_address=
        //    //&password=
        //    //&email=
        //    //&adcopy_challenge=2%40Tw5szIz9haroDwT2alQdXZvD-dgNdDOh%40UutX0NRqogwENU8bgrS6n39APTC4lJjoFfXwqkwwlrgVNrI.o40lV.dxs6AY572qbq9wYK6jvrHF6xwXDXMvHrfq8r3HZ3Y1W2p3b15rPvHbCDuHzGJM1raWjk5BV9CwHylCHEp2QvETLgMDkbFD60OqAzqiaZxI7J8kCpm9N.CvQPyN1oN3zQIJxJ9aoyazOt7WfM0LHPcxVmr0cHU8bVcgIAZtfBJ1tFq4AbNA0gFyORvfND7kWQbSeLjr-zigBi3dc.Pov8y7QjPW5OwqWc7KmcRbhq81JkhFXIK0uoA
        //    //&fingerprint=21020a225031b8463425eac2bccb41c8
        //    //&referrer=0
        //    //&token=62f45fe74ee7c1efea3ae46479470190d268f4438de66997bf031767b0c29994
        //    //&g_recaptcha_response=
        //}            //https://freebitco.in/
        //public static void MailRu_Reg()
        //{
        //    string log = new HttpEngine()

        //        .GetRequest(@"https://mail.ru/")
        //        .GetRequest(@"https://e.mail.ru/signup?from=main_noc")

        //        .ProvideUserName()
        //        .SetFileName(Items.iLaststored + "-MailRu.acc")

        //        .Log;

        //}
        //public static void ScrapeNamesHobbit()
        //{
        //    List<object> res;
        //    string log = new HttpEngine()

        //        .GetRequest(@"http://hobbit.namegeneratorfun.com")

        //        .SetData("ctl00$ctl00$ScriptManager1=ctl00$ctl00$Content$Form$SimpleAjaxForm1$FormUpdatePanel|ctl00$ctl00$Content$Form$SimpleAjaxForm1$ChooseRadioButton$0")
        //        .SetData("&__EVENTTARGET=ctl00$ctl00$Content$Form$SimpleAjaxForm1$ChooseRadioButton$0")
        //        .SetData("&__EVENTARGUMENT=")
        //        .SetData("&__LASTFOCUS=")
        //        .SetData("&__VIEWSTATE=/wEPDwUKLTk0MjExNDgzNGRkSJ3sXAHWYB24ANtrwCCxlse1miSyNKhOzf1MZccYvIc=")
        //        .SetData("&ctl00$ctl00$Content$Form$SimpleAjaxForm1$ChooseRadioButton=random")
        //        .SetData("&__ASYNCPOST=true&")

        //        .ParseCombine()
        //        .ParseUrlEncode()
        //        .PostRequest("http://hobbit.namegeneratorfun.com/")

        //        .ResetStorage()

        //        .SetData("ctl00$ctl00$ScriptManager1=ctl00$ctl00$Content$Form$SimpleAjaxForm1$FormUpdatePanel|ctl00$ctl00$Content$Form$SimpleAjaxForm1$GeneratorButton")
        //        .SetData("&ctl00$ctl00$Content$Form$SimpleAjaxForm1$ChooseRadioButton=random")
        //        .SetData("&ctl00$ctl00$Content$Form$SimpleAjaxForm1$GenderRadioButton=F")
        //        .SetData("&__EVENTTARGET=")
        //        .SetData("&__EVENTARGUMENT=")
        //        .SetData("&__LASTFOCUS=")
        //        .SetData("&__VIEWSTATE=/wEPDwUKLTk0MjExNDgzNGRkSJ3sXAHWYB24ANtrwCCxlse1miSyNKhOzf1MZccYvIc=")
        //        .SetData("&__ASYNCPOST=true")
        //        .SetData("&ctl00$ctl00$Content$Form$SimpleAjaxForm1$GeneratorButton=Generate Hobbit Name")

        //        .ParseCombine()
        //        .ParseUrlEncode()
        //        .PostRequest("http://hobbit.namegeneratorfun.com/")
        //        //.ExtractIpPortPairs()
        //        //.FileExport("SocksProxyNet-PublicProxies.txt")
        //        .GetResults(out res)
        //        .Log;

        //    bool bp = false;
        //    //System.Threading.Thread.Sleep(11111111);
        //}
        //HMA proxies


        ///<summary>
        /// Test shit here :)
        ///</summary>
        public static void TestMethod()
        {
            var E = new HttpEngine().CONSOLE_LOG();

            E.GetRequest(@"https://freebitco.in/");


            E.PopDataRandom(@"btca.txt", "bitcoin");
            E.ProvidePassword("pass");
            E.ProvideEmail("email");


            E.ExtractCookieValue("csrf_token");
            E.ParseAsPostField("csrf_token");
            E.SetData("op=signup");
            E.SetDataCopy("bitcoin", "btc");

            E.SetData("adcopy_challenge=");                                                              //?
            E.ProvideRandomHex(32);                                                                      //?
            E.ParseAsPostField("fingerprint");
            E.SetData("referrer=0");
            E.ExtractDelimitedString("$(\"#signup_token\").val(\"", "\");");
            E.ValidateStringLength(10, 200);
            E.ParseAsPostField("token");
            E.SetData("g_recaptcha_response=");


            //        //.PostRequest()
            //        //.GetResult(out result)

            //    //csrf_token=tBugn8D5UWiK
            //    //&op=signup
            //    //&btc_address=
            //    //&password=
            //    //&email=
            //    //&adcopy_challenge=2%40Tw5szIz9haroDwT2alQdXZvD-dgNdDOh%40UutX0NRqogwENU8bgrS6n39APTC4lJjoFfXwqkwwlrgVNrI.o40lV.dxs6AY572qbq9wYK6jvrHF6xwXDXMvHrfq8r3HZ3Y1W2p3b15rPvHbCDuHzGJM1raWjk5BV9CwHylCHEp2QvETLgMDkbFD60OqAzqiaZxI7J8kCpm9N.CvQPyN1oN3zQIJxJ9aoyazOt7WfM0LHPcxVmr0cHU8bVcgIAZtfBJ1tFq4AbNA0gFyORvfND7kWQbSeLjr-zigBi3dc.Pov8y7QjPW5OwqWc7KmcRbhq81JkhFXIK0uoA
            //    //&fingerprint=21020a225031b8463425eac2bccb41c8
            //    //&referrer=0
            //    //&token=62f45fe74ee7c1efea3ae46479470190d268f4438de66997bf031767b0c29994
            //    //&g_recaptcha_response=        //    //csrf_token=tBugn8D5UWiK
            //    //&op=signup
            //    //&btc_address=
            //    //&password=
            //    //&email=
            //    //&adcopy_challenge=2%40Tw5szIz9haroDwT2alQdXZvD-dgNdDOh%40UutX0NRqogwENU8bgrS6n39APTC4lJjoFfXwqkwwlrgVNrI.o40lV.dxs6AY572qbq9wYK6jvrHF6xwXDXMvHrfq8r3HZ3Y1W2p3b15rPvHbCDuHzGJM1raWjk5BV9CwHylCHEp2QvETLgMDkbFD60OqAzqiaZxI7J8kCpm9N.CvQPyN1oN3zQIJxJ9aoyazOt7WfM0LHPcxVmr0cHU8bVcgIAZtfBJ1tFq4AbNA0gFyORvfND7kWQbSeLjr-zigBi3dc.Pov8y7QjPW5OwqWc7KmcRbhq81JkhFXIK0uoA
            //    //&fingerprint=21020a225031b8463425eac2bccb41c8
            //    //&referrer=0
            //    //&token=62f45fe74ee7c1efea3ae46479470190d268f4438de66997bf031767b0c29994
            //    //&g_recaptcha_response=

            E.SetTimeout(5000);

            E.PopProxyRandom("FreeProxyList-PublicProxies.txt");
            E.ValidateHostUp();
            E.ERROR_ONLY().ResetProxy().ERROR_ONLY().UNDO_ERROR();


            string s = E.Log;
                E.PRESS_ANY_KEY();
        }


        ///<summary>
        /// Proxy leacher.
        ///</summary>
        public static void LeachSocksProxies1()
        {
            var E = new HttpEngine().CONSOLE_LOG();

            E.GetRequest(@"https://www.socks-proxy.net/");
            E.ExtractIpPortPairs();
            E.FileExport("SocksProxyNet-PublicProxies.txt");

            string s = E.Log;

        }               //https://www.socks-proxy.net/
        ///<summary>
        /// Proxy leacher.
        ///</summary>
        public static void LeachProxies1()
        {
            for (int i = 1; i < 28; i++)
            {
                var E = new HttpEngine().CONSOLE_LOG();

                E.SetTimeout(5000);

                E.PopProxyRandom("FreeProxyList-PublicProxies.txt");
                E.ValidateHostUp();

                E.Sleep(5000, 20000);
                E.GetRequest(@"http://www.freeproxylists.net/?page=" + i);
                //E.ParseUrlEncode();
                E.ExtractIpPortPairs("</a>\")</script></td><td align=\"center\">");
                E.FileAppend("PublicProxies.txt", "key");

                string s = E.Log;
            }
        }                    //http://www.freeproxylists.net/?page=
        ///<summary>
        /// Proxy leacher.
        ///</summary>
        public static void LeachProxies()
        {
            for (int i = 1; i < 28; i++)
            {
                var E = new HttpEngine().CONSOLE_LOG();

                E.SetTimeout(5000);

                E.PopProxyRandom("FreeProxyList-PublicProxies.txt");
                E.ValidateHostUp();

                E.Sleep(5000, 20000);
                E.GetRequest(@"http://www.freeproxylists.net/?page=" + i);
                //E.ParseUrlEncode();
                E.ExtractIpPortPairs("</a>\")</script></td><td align=\"center\">");
                E.FileAppend("PublicProxies.txt", "key");

                string s = E.Log;
            }
        }                    //http://www.freeproxylists.net/?page=
        ///<summary>
        /// Proxy leacher.
        ///</summary>
        public static void LeachProxies2()
        {
            var E = new HttpEngine().CONSOLE_LOG();

            E.SetProxy("127.0.0.1:8888");

            E.GetRequest("https://free-proxy-list.net/");
            E.ExtractIpPortPairs("</td><td>");
            E.FileExport("FreeProxyList-PublicProxies.txt");

            string s = E.Log;
        }                    //https://free-proxy-list.net/

        ///<summary>
        /// Database leacher.
        ///</summary>
        public static void LeachHobbitNames()
        {
            var E = new HttpEngine().CONSOLE_LOG();

            E.SetHeader("DNT", "1");
            E.SetHeader("Upgrade-Insecure-Requests", "1");
            E.SetTimeout(10000);

            E.PopProxyRandom("FreeProxyList-PublicProxies.txt");
            E.ValidateHostUp();

            E.IGNORE_ERROR();
            E.Load("LeachHobbitNames--" + E.pProxyAddress + ".act");
            E.IGNORE_ERROR();


            E.GetRequest(@"http://www.fakenamegenerator.com/gen-male-hobbit-uk.php", "Gotten");

            E.PreParseReduceWhitespaces();
            E.ExtractDelimitedString("<div class=\"address\"> <h3>", "</h3>");

            E.ExtractSplit(" ");
            E.ExtractColumn(1, "first name");
            E.ExtractColumn(3, "last name");
            E.FileAccumulate("Hobbit-M1.txt", "first name");
            E.FileAccumulate("Hobbit-M2.txt", "last name");

            E.ResetStripToPersist();
            E.SetFileName("LeachHobbitNames--" + E.pProxyAddress + ".act");
            E.FileSave();

            string s = E.Log;

        }                 //http://www.fakenamegenerator.com/gen-male-hobbit-uk.php
        ///<summary>
        /// Database leacher.
        ///</summary>
        public static void LeachMaleNamesDataFakeGen()
        {
            var E = new HttpEngine().CONSOLE_LOG();

            E.SetHeader("DNT", "1");
            E.SetHeader("Upgrade-Insecure-Requests", "1");

            E.SetProxy("127.0.0.1:8888");
            E.ValidateHostUp();

            E.IGNORE_ERROR();
            E.Load("LeachMaleNamesDataFakeGen--" + E.pProxyAddress + ".act");
            E.IGNORE_ERROR();

            E.GetRequest(@"http://www.datafakegenerator.com/generador.php");

            E.SetHeader("DNT", "1");
            E.SetHeader("Upgrade-Insecure-Requests", "1");
            E.SetHeader("Content-Type", " application/x-www-form-urlencoded");
            E.SetHeader("Cache-Control", "max-age=0");
            E.SetHeader("Origin", "http://www.datafakegenerator.com");

            E.SetData("&pais=United+Kingdom");
            E.SetData("&sexo=Male");//"Female"
            E.SetData("&de=18");
            E.SetData("&hasta=35");
            E.ExtractConcentrate(4);

            E.PostRequest(@"http://www.datafakegenerator.com/generador.php");

            E.PreParseReduceWhitespaces();
            E.ExtractDelimitedString("Name:</h3></div><div class=\"6u 12u(mobile)\"><p class=\"izquierda\">", "</p>");
            E.ExtractSplit(" ");
            E.ExtractColumn(1, "first name");
            E.ExtractColumn(2, "last name");
            E.FileAccumulate("UK-M1.txt", "first name");
            E.FileAccumulate("UK-M2.txt", "last name");

            E.ResetStripToPersist();
            E.SetFileName("LeachMaleNamesDataFakeGen--" + E.pProxyAddress + ".act");
            E.FileSave();

            string s = E.Log;


        }        //http://www.datafakegenerator.com/generador.php

        ///<summary>
        /// Captcha fetcher.
        ///</summary>
        public static void FetchAudioReCAPTCHA()
        {
            var E = new HttpEngine().CONSOLE_LOG();

            E.CONSOLE_LOG();

            E.PopProxy("PublicProxies.txt");
            E.GetRequest(@"http://www.google.com/recaptcha/api/noscript?k=6LeT6gcAAAAAAAZ_yDmTMqPH57dJQZdQcu6VFqog&is_audio=true");
            E.ExtractUrlByPart(@"image?c=");
            E.ParsePreAppend(@"www.google.com/recaptcha/api/");
            E.GetRequest();
            E.FileExport(E.pDatestamp + "-RecaptchaAudio.mp3", "key");

            string s = E.Log;
        }               //http://www.google.com/recaptcha
    }
}





//.SetProxy("127.0.0.1:8888")  -   for Fiddler