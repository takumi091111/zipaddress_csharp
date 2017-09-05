using System;
using System.Net;
using Codeplex.Data;

namespace zipaddress
{
    //class Program
    //{

    //    static void Main(string[] args)
    //    {
    //        ZipAddress addr = new ZipAddress("5430001");
    //        addr.DebugPrint();
    //    }
    //}

    public class ZipAddress
    {
        public int code { get; set; }
        public ZipData data { get; set; }

        public ZipAddress(string zipcode)
        {

            const string BASE_URL = "http://api.zipaddress.net/?zipcode=";
            string json;

            // APIを呼び出してデータを取得
            using (WebClient webClient = new WebClient())
            {
                json = webClient.DownloadString(BASE_URL + zipcode);
            }

            // jsonをパース
            var obj = DynamicJson.Parse(json);

            // メンバ変数にjsonの値を入れる
            code = (int)obj.code;
            data = new ZipData {
                pref = obj.data.pref,
                city = obj.data.city,
                town = obj.data.town,
                address = obj.data.address,
                fullAddress = obj.data.fullAddress
            };
        }

        public void DebugPrint()
        {
            Console.WriteLine($"code: {code}, " +
                $"pref: {data.pref}, " +
                $"city: {data.city}, " +
                $"town: {data.town}, " +
                $"address: {data.address}, " +
                $"fullAddress: {data.fullAddress}"
            );
        }
    }

    public class ZipData
    {
        public string pref { get; set; }
        public string city { get; set; }
        public string town { get; set; }
        public string address { get; set; }
        public string fullAddress { get; set; }
    }
}
