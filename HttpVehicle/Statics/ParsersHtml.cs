using HtmlAgilityPack;
using System;
using System.Net;
using System.Text.RegularExpressions;

namespace HttpVehicle
{
    class ParsersHtml
    {
        static HtmlDocument HtmlDoc;




        public static string HtmlReplaceTags(string html, string oldTagName, string newTagName)
        {
            ParseHtml(html);

            HtmlNodeCollection tags = HtmlDoc.DocumentNode.SelectNodes("//" + oldTagName);
            if (tags != null)
            {
                foreach (HtmlNode item in tags)
                {
                    item.Name = newTagName;
                }
            }

            return HtmlDoc.DocumentNode.OuterHtml;
        }
        public static string HtmlReplaceClass(string html, string oldClassName, string newClassName)
        {
            ParseHtml(html);

            HtmlNodeCollection tags = HtmlDoc.DocumentNode.SelectNodes("//*[@class]");
            if (tags != null)
            {
                foreach (HtmlNode item in tags)
                {
                    if (item.Attributes["class"].Value.ToLower() == oldClassName)
                    {
                        item.SetAttributeValue("class", newClassName);
                    }
                }
            }
            return HtmlDoc.DocumentNode.OuterHtml;
        }
        public static string HtmlReplaceId(string html, string oldId, string newId)
        {
            ParseHtml(html);

            HtmlNodeCollection tags = HtmlDoc.DocumentNode.SelectNodes("//*[@id]");
            if (tags != null)
            {
                foreach (HtmlNode item in tags)
                {
                    if (item.Attributes["id"].Value.ToLower() == oldId)
                    {
                        item.SetAttributeValue("id", oldId);
                    }
                }
            }
            return HtmlDoc.DocumentNode.OuterHtml;
        }
        //add class
        //set attr
        //set attr value

        public static string HtmlRemoveHidden(string html)
        {
            ParseHtml(html);

            HtmlNodeCollection tags = HtmlDoc.DocumentNode.SelectNodes("//*[@style]");
            if (tags != null)
            {
                foreach (HtmlNode item in tags)
                {
                    if(item.Attributes.Contains("style"))
                    {
                        string val = item.Attributes["style"].Value;
                        val = Regex.Replace(val, " {2,}", " ").Trim().Replace("\n", String.Empty).Replace("\r", String.Empty).Replace("\t", String.Empty);


                        if (val.Contains("display:none") || val.Contains("visibility:hidden")
                            || val.Contains("display: none") || val.Contains("visibility: hidden")
                            || val.Contains("display :none") || val.Contains("visibility :hidden")
                            || val.Contains("display : none") || val.Contains("visibility : hidden"))
                        {
                            item.Remove();
                        }
                    }
                }
            }

            return HtmlDoc.DocumentNode.OuterHtml;
        }
        public static string HtmlRemoveVisible(string html)
        {
            ParseHtml(html);

            HtmlNodeCollection tags = HtmlDoc.DocumentNode.SelectNodes("//*[@style]");
            if (tags != null)
            {
                foreach (HtmlNode item in tags)
                {
                    if (item.Attributes.Contains("style"))
                    {
                        string val = item.Attributes["style"].Value;
                        val = Regex.Replace(val, " {2,}", " ").Trim().Replace("\n", String.Empty).Replace("\r", String.Empty).Replace("\t", String.Empty);


                        if (!val.Contains("display:none") && !val.Contains("visibility:hidden")
                            && !val.Contains("display: none") && !val.Contains("visibility: hidden")
                            && !val.Contains("display :none") && !val.Contains("visibility :hidden")
                            && !val.Contains("display : none") && !val.Contains("visibility : hidden"))
                        {
                            item.Remove();
                        }
                    }
                }
            }

            return HtmlDoc.DocumentNode.OuterHtml;
        }
        public static string HtmlRemoveContaining(string html, string content)
        {
            ParseHtml(html);

            HtmlNodeCollection tags = HtmlDoc.DocumentNode.SelectNodes("/descendant-or-self::node()");
            if (tags != null)
            {
                foreach (HtmlNode item in tags)
                {
                    string it = item.OuterHtml;
                    if (it.Contains(content)) { item.Remove(); }
                }
            }

            return HtmlDoc.DocumentNode.OuterHtml;
        }
        public static string HtmlRemoveByTagName(string html, string tagName)
        {
            ParseHtml(html);

            HtmlNodeCollection tags = HtmlDoc.DocumentNode.SelectNodes("//" + tagName);
            if (tags != null)
            {
                foreach (HtmlNode item in tags)
                {
                    item.Remove();
                }
            }

            return HtmlDoc.DocumentNode.OuterHtml;
        }
        public static string HtmlRemoveFirstByTagName(string html, string tagName)
        {
            ParseHtml(html);

            HtmlNodeCollection tags = HtmlDoc.DocumentNode.SelectNodes("//" + tagName);
            if (tags != null)
            {
                tags[0].Remove();
            }

            return HtmlDoc.DocumentNode.OuterHtml;
        }
        public static string HtmlRemoveByAttributeValue(string html, string attributeName, string value)
        {
            ParseHtml(html);

            HtmlNodeCollection tags = HtmlDoc.DocumentNode.SelectNodes("//*[@" + attributeName + "]");
            if (tags != null)
            {
                foreach (HtmlNode item in tags)
                {
                    string val = item.Attributes[attributeName].Value;

                    if (val == value)
                    {
                        item.Remove();
                    }
                }
            }

            return HtmlDoc.DocumentNode.OuterHtml;
        }
        public static string HtmlRemoveByAttribute(string html, string attributeName)
        {
            ParseHtml(html);

            HtmlNodeCollection tags = HtmlDoc.DocumentNode.SelectNodes("//*[@"+ attributeName +"]");
            if (tags != null)
            {
                foreach (HtmlNode item in tags)
                {
                    item.Remove();
                }
            }

            return HtmlDoc.DocumentNode.OuterHtml;
        }
        public static string HtmlRemoveByClass(string html, string className)
        {
            ParseHtml(html);

            HtmlNodeCollection tags = HtmlDoc.DocumentNode.SelectNodes("//*[@class]");
            if (tags != null)
            {
                foreach (HtmlNode item in tags)
                {
                    string val = item.Attributes["class"].Value;

                    if (val.Contains(className))
                    {
                        item.Remove();
                    }
                }
            }

            return HtmlDoc.DocumentNode.OuterHtml;
        }
        public static string HtmlRemoveById(string html, string idName)
        {
            ParseHtml(html);

            HtmlNodeCollection tags = HtmlDoc.DocumentNode.SelectNodes("//*[@id]");
            if (tags != null)
            {
                foreach (HtmlNode item in tags)
                {
                    string val = item.Attributes["id"].Value;

                    if (val == idName)
                    {
                        item.Remove();
                    }
                }
            }

            return HtmlDoc.DocumentNode.OuterHtml;
        }
        public static string HtmlRemoveText(string html, string content)
        {
            ParseHtml(html);

            HtmlNodeCollection tags = HtmlDoc.DocumentNode.SelectNodes("/descendant-or-self::node()");
            if (tags != null)
            {
                foreach (HtmlNode item in tags)
                {
                    if (item is HtmlTextNode == true
                        || item.Name.ToLower() == "p"
                        || item.Name.ToLower() == "br"
                        || item.Name.ToLower() == "h1"
                        || item.Name.ToLower() == "h2"
                        || item.Name.ToLower() == "h3"
                        || item.Name.ToLower() == "h4"
                        || item.Name.ToLower() == "h5"
                        || item.Name.ToLower() == "h6"
                        || item.Name.ToLower() == "em"
                        || item.Name.ToLower() == "strong"
                        || item.Name.ToLower() == "code"
                        || item.Name.ToLower() == "blockquote"
                        || item.Name.ToLower() == "sup"
                        || item.Name.ToLower() == "sub"
                        || item.Name.ToLower() == "del"
                        || item.Name.ToLower() == "cite"
                        || item.Name.ToLower() == "dfn"
                        || item.Name.ToLower() == "acronym"
                        || item.Name.ToLower() == "abbr"
                        || item.Name.ToLower() == "samp"
                        || item.Name.ToLower() == "kbd"
                        || item.Name.ToLower() == "var"
                        || item.Name.ToLower() == "ins")
                    {
                        item.Remove();
                    }
                }
            }

            return HtmlDoc.DocumentNode.OuterHtml;
        }
        public static string HtmlRemoveImage(string html, string content)
        {
            ParseHtml(html);

            HtmlNodeCollection tags = HtmlDoc.DocumentNode.SelectNodes("/descendant-or-self::node()");
            if (tags != null)
            {
                foreach (HtmlNode item in tags)
                {
                    if (item.Name.ToLower() == "img"
                        || item.Attributes.Contains("background")
                        || (item.Attributes.Contains("style") && item.Attributes["style"].Value.Contains("background-image"))
                        || (item.Attributes["style"].Value.Contains("background") && item.Attributes["style"].Value.Contains("url")))
                    {
                        item.Remove();
                    }
                }
            }

            return HtmlDoc.DocumentNode.OuterHtml;
        }
        public static string HtmlRemoveNthTag(string html, int n)
        {
            ParseHtml(html);

            HtmlNodeCollection tags = HtmlDoc.DocumentNode.SelectNodes("/descendant-or-self::node()");
            if (tags != null)
            {
                for (int i = 0; i < tags.Count; i++)
                {
                    if (i == n)
                    {
                        tags[i].Remove();
                        break;
                    }
                }
            }

            return HtmlDoc.DocumentNode.OuterHtml;
        }

        public static string HtmlGetContaining(string html, string content)
        {
            ParseHtml(html);

            HtmlNodeCollection tags = HtmlDoc.DocumentNode.SelectNodes("/descendant-or-self::node()");
            if (tags != null)
            {
                foreach (HtmlNode item in tags)
                {
                    string it = item.OuterHtml;
                    if (!it.Contains(content)) { item.Remove(); }
                }
            }

            return HtmlDoc.DocumentNode.OuterHtml;
        }
        public static string HtmlGetByTagName(string html, string tagName)
        {
            ParseHtml(html);

            HtmlNodeCollection tags = HtmlDoc.DocumentNode.SelectNodes("/descendant-or-self::node()");
            if (tags != null)
            {
                foreach (HtmlNode item in tags)
                {
                    if (item.Name != tagName)
                    {
                        item.Remove();
                    }
                }
            }

            return HtmlDoc.DocumentNode.OuterHtml;
        }
        public static string HtmlGetFirstByTagName(string html, string tagName)
        {
            ParseHtml(html);

            HtmlNodeCollection tags = HtmlDoc.DocumentNode.SelectNodes("/descendant-or-self::node()");
            if (tags != null)
            {
                foreach (HtmlNode item in tags)
                {
                    if (item.Name != tagName)
                    {
                        item.Remove();
                    }
                    else return item.OuterHtml;
                }
            }

            return HtmlDoc.DocumentNode.OuterHtml;
        }
        public static string HtmlGetByAttributeValue(string html, string attributeName, string value)
        {
            ParseHtml(html);

            HtmlNodeCollection tags = HtmlDoc.DocumentNode.SelectNodes("/descendant-or-self::node()");
            if (tags != null)
            {
                foreach (HtmlNode item in tags)
                {
                    if (item.Attributes.Contains(attributeName))
                    {
                        string val = item.Attributes[attributeName].Value;

                        if(val != value)
                        {
                            item.Remove();
                        }
                    }
                    else
                    {
                        item.Remove();
                    }
                }
            }

            return HtmlDoc.DocumentNode.OuterHtml;
        }
        public static string HtmlGetByAttribute(string html, string attributeName)
        {
            ParseHtml(html);

            HtmlNodeCollection tags = HtmlDoc.DocumentNode.SelectNodes("/descendant-or-self::node()");
            if (tags != null)
            {
                foreach (HtmlNode item in tags)
                {
                    if (item.Attributes.Contains(attributeName) == false)
                    {
                        item.Remove();
                    }
                }
            }

            return HtmlDoc.DocumentNode.OuterHtml;
        }
        public static string HtmlGetByClass(string html, string className)
        {
            ParseHtml(html);

            HtmlNodeCollection tags = HtmlDoc.DocumentNode.SelectNodes("/descendant-or-self::node()");
            if (tags != null)
            {
                foreach (HtmlNode item in tags)
                {
                    if (item.Attributes.Contains("class"))
                    {
                        string val = item.Attributes["class"].Value;

                        if (val.Contains(className) == false)
                        {
                            item.Remove();
                        }
                    }
                    else
                    {
                        item.Remove();
                    }
                }
            }

            return HtmlDoc.DocumentNode.OuterHtml;
        }
        public static string HtmlGetById(string html, string idName)
        {
            ParseHtml(html);

            HtmlNodeCollection tags = HtmlDoc.DocumentNode.SelectNodes("/descendant-or-self::node()");
            if (tags != null)
            {
                foreach (HtmlNode item in tags)
                {
                    if (item.Attributes.Contains("id"))
                    {
                        string val = item.Attributes["id"].Value;

                        if (val == idName)
                        {
                            item.Remove();
                        }
                    }
                    else
                    {
                        item.Remove();
                    }
                }
            }

            return HtmlDoc.DocumentNode.OuterHtml;
        }
        public static string HtmlGetText(string html, string content)
        {
            ParseHtml(html);

            HtmlNodeCollection tags = HtmlDoc.DocumentNode.SelectNodes("/descendant-or-self::node()");
            if (tags != null)
            {
                foreach (HtmlNode item in tags)
                {
                    if (item is HtmlTextNode == false
                        && item.Name.ToLower() != "p"
                        && item.Name.ToLower() != "br"
                        && item.Name.ToLower() != "h1"
                        && item.Name.ToLower() != "h2"
                        && item.Name.ToLower() != "h3"
                        && item.Name.ToLower() != "h4"
                        && item.Name.ToLower() != "h5"
                        && item.Name.ToLower() != "h6"
                        && item.Name.ToLower() != "em"
                        && item.Name.ToLower() != "strong"
                        && item.Name.ToLower() != "code"
                        && item.Name.ToLower() != "blockquote"
                        && item.Name.ToLower() != "sup"
                        && item.Name.ToLower() != "sub"
                        && item.Name.ToLower() != "del"
                        && item.Name.ToLower() != "cite"
                        && item.Name.ToLower() != "dfn"
                        && item.Name.ToLower() != "acronym"
                        && item.Name.ToLower() != "abbr"
                        && item.Name.ToLower() != "samp"
                        && item.Name.ToLower() != "kbd"
                        && item.Name.ToLower() != "var"
                        && item.Name.ToLower() != "ins")
                    {
                        item.Remove();
                    }
                }
            }

            return HtmlDoc.DocumentNode.OuterHtml;
        }
        public static string HtmlGetImage(string html, string content)
        {
            ParseHtml(html);

            HtmlNodeCollection tags = HtmlDoc.DocumentNode.SelectNodes("/descendant-or-self::node()");
            if (tags != null)
            {
                foreach (HtmlNode item in tags)
                {
                    if (item.Name.ToLower() != "img"
                        || !item.Attributes.Contains("background") || item.Attributes["background"].Value == null || item.Attributes["background"].Value == ""
                        || !item.Attributes.Contains("style") || 
                        (!item.Attributes["style"].Value.Contains("background-image") && !(item.Attributes["style"].Value.Contains("background") && item.Attributes["style"].Value.Contains("url"))))
                    {
                        item.Remove();
                    }
                }
            }

            return HtmlDoc.DocumentNode.OuterHtml;
        }
        public static string HtmlGetNthTag(string html, int n)
        {
            ParseHtml(html);

            HtmlNodeCollection tags = HtmlDoc.DocumentNode.SelectNodes("/descendant-or-self::node()");
            if (tags != null)
            {
                for(int i = 0; i < tags.Count; i++)
                {
                    if (i != n) { tags[i].Remove(); }
                }
            }

            return HtmlDoc.DocumentNode.OuterHtml;
        }


        //scr/href - relative path!


        public static string HtmlReduceToSpace(string html)
        {
            html = Regex.Replace(html, " {2,}", " ").Trim().Replace("\n", String.Empty).Replace("\r", String.Empty).Replace("\t", String.Empty);
            return html;
        }
        public static string HtmlTagsToLower(string html)
        {
            ParseHtml(html);

            HtmlNodeCollection tags = HtmlDoc.DocumentNode.SelectNodes("/descendant-or-self::node()");
            if (tags != null)
            {
                foreach (HtmlNode item in tags)
                {
                    item.Name = item.Name.ToLower();
                }
            }

            return HtmlDoc.DocumentNode.OuterHtml;
        }
        public static string HtmlTagsToUpper(string html)
        {
            ParseHtml(html);

            HtmlNodeCollection tags = HtmlDoc.DocumentNode.SelectNodes("/descendant-or-self::node()");
            if (tags != null)
            {
                foreach (HtmlNode item in tags)
                {
                    item.Name = item.Name.ToUpper();
                }
            }

            return HtmlDoc.DocumentNode.OuterHtml;
        }
        public static string HtmlNormalizeRelativePaths(string html, string host)
        {
            ParseHtml(html);

            HtmlNodeCollection tags = HtmlDoc.DocumentNode.SelectNodes("/descendant-or-self::node()");
            if (tags != null)
            {
                foreach (HtmlNode item in tags)
                {
                    if (item.Attributes.Contains("scr")
                        && item.Attributes["scr"].Value != null
                        && item.Attributes["scr"].Value != ""
                        && item.Attributes["scr"].Value.Contains("http:") == false
                        && item.Attributes["scr"].Value.Contains("https:") == false)
                    {
                        item.Attributes["scr"].Value = host + item.Attributes["scr"].Value;
                    }
                    if (item.Attributes.Contains("href")
                        && item.Attributes["href"].Value != null
                        && item.Attributes["href"].Value != ""
                        && item.Attributes["href"].Value.Contains("http:") == false
                        && item.Attributes["href"].Value.Contains("https:") == false)
                    {
                        item.Attributes["href"].Value = host + item.Attributes["href"].Value;
                    }
                }
            }

            return HtmlDoc.DocumentNode.OuterHtml;
        }
        public static string HtmlInstyleCSS(string html, string css)
        {
            html = WebUtility.HtmlDecode(html);
            css = "<STYLE>" + Environment.NewLine + css + Environment.NewLine + "</STYLE>";

            HtmlDoc.LoadHtml(html);

            HtmlNode head = HtmlDoc.DocumentNode.SelectSingleNode("/html/head");
            HtmlNode style = HtmlDoc.CreateElement("style");
            HtmlNode text = HtmlDoc.CreateTextNode(css);
            style.ChildNodes.Add(text);
            head.AppendChild(style);

            return HtmlDoc.DocumentNode.OuterHtml;
        }
        public static string HtmlInlineCSS(string html)
        {
            html = WebUtility.HtmlDecode(html);
            PreMailer.Net.PreMailer pm = new PreMailer.Net.PreMailer(html);
            string inlined = pm.MoveCssInline(removeStyleElements: true, removeComments: true).Html;

            HtmlDoc = new HtmlDocument();
            HtmlDoc.LoadHtml(inlined);

            return HtmlDoc.DocumentNode.OuterHtml;
        }
        public static string HtmlInlineCSS(string html, string css)
        {
            html = HtmlInstyleCSS(html, css);

            html = WebUtility.HtmlDecode(html);
            PreMailer.Net.PreMailer pm = new PreMailer.Net.PreMailer(html);
            string inlined = pm.MoveCssInline(removeStyleElements: true, removeComments: true).Html;

            HtmlDoc = new HtmlDocument();
            HtmlDoc.LoadHtml(inlined);

            return HtmlDoc.DocumentNode.OuterHtml;
        }



        private static void ParseHtml(string html, string css)
        {
            html = WebUtility.HtmlDecode(html);
            css = "<STYLE>" + Environment.NewLine + css + Environment.NewLine + "</STYLE>";

            HtmlDoc.LoadHtml(html);

            HtmlNode head = HtmlDoc.DocumentNode.SelectSingleNode("/html/head");
            HtmlNode style = HtmlDoc.CreateElement("style");
            HtmlNode text = HtmlDoc.CreateTextNode(css);
            style.ChildNodes.Add(text);
            head.AppendChild(style);
        }
        private static void ParseHtml(string html)
        {
            html = WebUtility.HtmlDecode(html);
            PreMailer.Net.PreMailer pm = new PreMailer.Net.PreMailer(html);
            string inlined = pm.MoveCssInline(removeStyleElements: true, removeComments: true).Html;

            HtmlDoc = new HtmlDocument();
            HtmlDoc.LoadHtml(inlined);
        }
    }
}

//parent removal problem
//TODO : THIS MODULE IS BARELEY FUNCTIONAL! REFACTOR ASAP!
//TODO : test and refactor
//TODO : contains instead of = for classes and other multyvalue attributes
//TODO : test proper item removing in methods
////@import url("stylesheetB.css") issue