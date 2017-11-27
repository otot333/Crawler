using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Crawler
{
    class Program
    {
        static void Main(string[] args)
        {

            var filePath = AppDomain.CurrentDomain.BaseDirectory + "Test2.html";
            //指定來源網頁
            WebClient url = new WebClient();

            var fs = new FileStream(filePath,FileMode.Open);
            var result = "";
            int iChar;


            StreamReader srReader = new StreamReader(fs);
            iChar = srReader.Read();
            while (iChar != -1)
            {
                result += (Convert.ToChar(iChar));
                iChar = srReader.Read();
            }
            srReader.Close();


            Console.WriteLine(result);
            //將網頁來源資料暫存到記憶體內
            //            MemoryStream ms = new MemoryStream(url.DownloadData("http://tw.stock.yahoo.com/q/q?s=1101"));
            //            //以奇摩股市為例http://tw.stock.yahoo.com
            //            //1101 表示為股票代碼

            // 使用預設編碼讀入 HTML 
            HtmlDocument doc = new HtmlDocument();
            //doc.Load(fs, Encoding.Default);
            doc.LoadHtml(result);

            var result2 = doc.DocumentNode.Descendants()
            .Where(x => x.Attributes.Contains("class")).ToList();
           

            IEnumerable<HtmlNode> hasFloatClass = doc.DocumentNode
    .Descendants("h5")
    .Where(div => div.HasClass("_5pbw _5vra")).ToList();
            // 裝載第一層查詢結果 
            HtmlDocument hdc = new HtmlDocument();

            //            //XPath 來解讀它 /html[1]/body[1]/center[1]/table[2]/tr[1]/td[1]/table[1] 
            //            hdc.LoadHtml(doc.DocumentNode.SelectSingleNode("/html[1]/body[1]/center[1]/table[2]/tr[1]/td[1]/table[1]").InnerHtml);

            //            // 取得個股標頭 
            //            HtmlNodeCollection htnode = hdc.DocumentNode.SelectNodes("./tr[1]/th");
            //            // 取得個股數值 
            //            string[] txt = hdc.DocumentNode.SelectSingleNode("./tr[2]").InnerText.Trim().Split('\n');
            //            int i = 0;

            //            // 輸出資料 
            //            foreach (HtmlNode nodeHeader in htnode)
            //            {
            //                //將 "加到投資組合" 這個字串過濾掉
            ////                Response.Write(nodeHeader.InnerText + ":" + txt[i].Trim().Replace("加到投資組合", "") + "
            ////");
            ////                i++;
            //            }

            //            //清除資料
            //            doc = null;
            //            hdc = null;
            //            url = null;
            //            ms.Close();
            Console.WriteLine("Get Count...");
            Console.ReadLine();
        }
    }
}
