using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using HtmlAgilityPack;
using MedicinesUrlCrawler;
using ScrapySharp.Network;
using ScrapySharp.Extensions;
using ScrapySharp.Html.Forms;
using System.Net.Http;

namespace TryParse
{
    public class CrawlPharmGroups
    {
        public IEnumerable<PharmGroup> ObtainPharmGroups()
        {
            using (var client = new HttpClient())
            {
                var html = client.GetStringAsync(Constants.SiteUrl).GetAwaiter().GetResult();
            }

            var pharmGroups = Enumerable.Empty<PharmGroup>();

            var browser = new ScrapingBrowser() {
                Encoding = Encoding.UTF8
            };

            //set UseDefaultCookiesParser as false if a website returns invalid cookies format
            //browser.UseDefaultCookiesParser = false;

            WebPage homePage = browser.NavigateToPage(new Uri(Constants.SiteUrl));

            
            var nodeForm = homePage.Html.CssSelect("form").First();
            PageWebForm form = new PageWebForm(nodeForm, browser);
            form.Method = HttpVerb.Post;
            form.Action = @"http://mozdocs.kiev.ua/oops/?hash=865b7da1da7b99afedb34ab1dd3b1072\";
            WebPage resultsPage = form.Submit();

            HtmlNode[] pharmGroupLinks = resultsPage.Html.CssSelect(Constants.PharmGroupCssSelector).ToArray();

            pharmGroups = pharmGroupLinks.Select(a => new PharmGroup
            {
                Id = new MongoDB.Bson.ObjectId(),
                Name = a.InnerText,
                Url = a.Attributes.Single(attr => attr.Name.Equals(Constants.HrefAttribute)).Value
            });

            return pharmGroups;
        }

        private void GetMainPage()
        {



        }

        private void ParsePharmGroups()
        {

        }
    }
}
