using HtmlAgilityPack;
using System;
using System.Linq;

namespace TryAgilityPack
{
    class Program
    {
        static void Main(string[] args)
        {
            var wholePageAsText = GetPageText();
            var textStartsWithOpenUlTag = GetTextBlockWithPharmCategories(wholePageAsText);

            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(textStartsWithOpenUlTag);

            var htmlNodes = htmlDoc.DocumentNode.SelectNodes("//ul[@class='ul-menu0']/li/a");

            var groupToLink = htmlNodes.ToDictionary(x => x.InnerText, y => y.Attributes["href"].Value);
            foreach (var group in groupToLink)
            {
                Console.WriteLine("{0} - {1}", group.Key, group.Value);
            }
        }


        private static string GetPageText()
        {
            string RootUrl = "http://mozdocs.kiev.ua/";

            var web = new HtmlWeb()
            {
                AutoDetectEncoding = true,
                BrowserDelay = TimeSpan.FromSeconds(5),
                UsingCacheIfExists = true,
                UseCookies = true,
                UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/74.0.3729.131 Safari/537.36",
            };
            var htmlDoc = web.Load(RootUrl);

            return htmlDoc.ParsedText;
        }

        private static string GetTextBlockWithPharmCategories(string input)
        {
            var idxMenu0 = input.IndexOf("ul-menu0");
            var subStrBeforeUlTag = input.Substring(0, idxMenu0);
            var idxOpenUlTag = subStrBeforeUlTag.LastIndexOf("<ul");
            var subStrStartsWithOpenUl = input.Substring(idxOpenUlTag);

            return subStrStartsWithOpenUl;
        }
    }
}