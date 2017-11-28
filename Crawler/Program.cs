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

            var filePath = AppDomain.CurrentDomain.BaseDirectory + "11281132.html";
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


            //Console.WriteLine(result);
            // MemoryStream ms = new MemoryStream(url.DownloadData("http://www.yahoo.com.tw"));
       

            // 使用預設編碼讀入 HTML 
            HtmlDocument doc = new HtmlDocument();
            //doc.Load(fs, Encoding.Default);
            doc.LoadHtml(result);

            var result2 = doc.DocumentNode.Descendants()
            .Where(x => x.Attributes.Contains("class") && x.Name =="h5").Select(x =>x.InnerText).ToList();
           

            var sharePeople = doc.DocumentNode
                .Descendants("h5")
                .Where(x => x.HasClass("_5pbw") && x.HasClass("_5vra"))
                .Select(x=>x.FirstChild)
                .Select(x=>x.FirstChild)
                .Select(x=>x.FirstChild)
                .Select(x=>x.FirstChild)
                .Select(x=>x.InnerText).ToList();

            foreach (var item in sharePeople)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine($"公開 分享總人數 : {sharePeople.Count}");
            // 裝載第一層查詢結果 
            HtmlDocument hdc = new HtmlDocument();

         
            Console.WriteLine("Get Count...");
            Console.ReadLine();
        }
    }
}
