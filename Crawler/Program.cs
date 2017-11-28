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
            //WebClient url = new WebClient();
            //var fs = new FileStream(filePath,FileMode.Open);
            var htmlText = "";
            //這方法開大檔案很快
            using (FileStream fs = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (BufferedStream bs = new BufferedStream(fs))
            using (StreamReader sr = new StreamReader(bs))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    htmlText += line;
                }
            }
            //Console.WriteLine(result);
            // MemoryStream ms = new MemoryStream(url.DownloadData("http://www.yahoo.com.tw"));

            // 使用預設編碼讀入 HTML 
            HtmlDocument doc = new HtmlDocument();
            //doc.Load(fs, Encoding.Default);

            doc.LoadHtml(htmlText);
            var count = 1;
            var sharePeople = doc.DocumentNode
                .Descendants("h5")
                .Where(x => x.HasClass("_5pbw") && x.HasClass("_5vra"))
                .Select(x=>x.FirstChild)
                .Select(x=>x.FirstChild)
                .Select(x=>x.FirstChild)
                .Select(x=>x.FirstChild)
                .Select(x=>x.InnerText).Distinct().ToList();

            using (StreamWriter sw = new StreamWriter("SharePeople.txt"))
            {             
          
                foreach (var personName in sharePeople)
                {
                    Console.WriteLine($"{count}.{personName}");
                    sw.WriteLine($"{count}.{personName}");
                    //sw.WriteLine(Environment.NewLine);
                    count++;
                }
            }

            Console.WriteLine($"公開 分享總人數 : {sharePeople.Count}");

            // 裝載第一層查詢結果 
            HtmlDocument hdc = new HtmlDocument();

         
            Console.WriteLine("Get Count...");
            Console.ReadLine();
        }
    }
}
