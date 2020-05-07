using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
//using OpenQA.Selenium;
//using OpenQA.Selenium.Chrome;
//using OpenQA.Selenium.Support.UI;
using System.IO;
using System.Reflection;

namespace TryParse
{
    /*
     PharmacoGroups:
     jQuery('ul.ul-menu0>li>a');
     for(let i=0; i<links.length; i++) { 		
 	    console.log(jQuery(links[i]).attr('href')) 
     }
 
     Pages:
     jQuery('index:last').siblings('a')
 
     Medicine:
     jQuery('index>table:last>tbody>tr') : 2 rows per medicine
 
     jQuery('div.content-wrapper td:contains("Назва")').next()
     jQuery('div.content-wrapper td:contains("Міжнародна непатентована назва")').next()
     jQuery('div.content-wrapper td:contains("Діючі речовини")').next()
     jQuery('p:contains("Взаємодія")').next()
     */

    class Program
    {
        const string RootUrl = "http://mozdocs.kiev.ua/";

        static void Main(string[] args)
        {
            // get all links to PharmGroups
            // get all links to medicines under each PharmGroup
            // parse pages of each medicine

            GetPharmGroupsLinks().GetAwaiter().GetResult();
            //GetPharmGroupsLinksViaBrowser();
        }

        static async Task<IDictionary<string, string>> GetPharmGroupsLinks()
        {
            // instance or static variable
            var client = new HttpClient();

            // get answer in non-blocking way
            using (var response = await client.GetAsync(RootUrl))
            {
                using (var content = response.Content)
                {
                    // read answer in non-blocking way
                    var result = await content.ReadAsStringAsync();
                    var document = new HtmlDocument();
                    document.LoadHtml(result);

                    var pharmGroupsNodes = document.DocumentNode.SelectNodes("//ul[@class='ul-menu0']/li/a");

                    foreach (var node in pharmGroupsNodes)
                    {
                        Console.WriteLine("{0} - {1}", node.InnerText, node.Attributes["href"].Value);
                    }

                    return pharmGroupsNodes.ToDictionary(x => x.InnerText, y => y.Attributes["href"].Value);
                }
            }
        }

        //static IDictionary<string, string> GetPharmGroupsLinksViaBrowser()
        //{
            //var elements = GetElementsWhenTheyExists(By.XPath("//ul[@class='ul-menu0']/li/a"));
            //var result = elements.ToDictionary(x => x.Text, y => y.GetAttribute("href"));

            //foreach (var node in result)
            //{
            //    Console.WriteLine("{0} - {1}", node.Key, node.Value);
            //}
            //return result;

            //// instance or static variable
            //var client = new HttpClient();

            //// get answer in non-blocking way
            //using (var response = await client.GetAsync(RootUrl))
            //{
            //    using (var content = response.Content)
            //    {
            //        // read answer in non-blocking way
            //        var result = await content.ReadAsStringAsync();
            //        var document = new HtmlDocument();
            //        document.LoadHtml(result);

            //        var pharmGroupsNodes = document.DocumentNode.SelectNodes("//ul[@class='ul-menu0']/li/a");

            //        foreach (var node in pharmGroupsNodes)
            //        {
            //            Console.WriteLine("{0} - {1}", node.InnerText, node.Attributes["href"].Value);
            //        }

            //        return pharmGroupsNodes.ToDictionary(x => x.InnerText, y => y.Attributes["href"].Value);
            //    }
            //}
        //}

        //static IEnumerable<IWebElement> GetElementsWhenTheyExists(By elementLocator, int timeout = 10)
        //{
        //    using (var driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)))
        //    {
        //        driver.Navigate().GoToUrl(RootUrl);
        //        var link = driver.FindElement(elementLocator);
        //        var wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));
        //        wait.Until(ExpectedConditions.ElementExists(elementLocator));
        //        return driver.FindElements(elementLocator);
        //    }
        //}
    }
}
