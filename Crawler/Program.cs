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
            var fileName = "下班沒事做色卡";
            var filePath = AppDomain.CurrentDomain.BaseDirectory + $"{fileName}.html";
            Console.WriteLine($"Load File : {fileName}");
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

            Console.WriteLine($"Html Parse...");
            // 使用預設編碼讀入 HTML 
            HtmlDocument doc = new HtmlDocument();
            //doc.Load(fs, Encoding.Default);

            doc.LoadHtml(htmlText);
            var count = 1;
            var f1 = doc.DocumentNode
                .Descendants("h5").Select(x=>x.SelectSingleNode("/span/span/span"));
            

            var f2 = doc.DocumentNode
                .Descendants("h5")
                .Where(x => x.HasClass("_5pbw") && x.HasClass("_5vra"));

            var sharePeople = doc.DocumentNode
                .Descendants("h5")
                .Where(x => x.HasClass("_5pbw") && x.HasClass("_5vra"))
                .Select(x => x.SelectSingleNode("span/span/span/a"))
                .Select(x=>x.InnerHtml).Distinct().ToList();

            using (StreamWriter sw = new StreamWriter($"SharePeople_{fileName}_{DateTime.Now.Second}.txt"))
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
         
            Console.WriteLine("Get Count...");
            Console.ReadLine();
        }
    }
}
