using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class Program
    {
        

        static void Main(string[] args)
        {
            //for (int i = 1; i < int.MaxValue; i++)
            //{
                NewMethod("%22Cochise%22_(cyclist)");
            //}
            
        }

        private async static void  NewMethod(string appcontinue)
        {
            int page = 500;
            string responseAT = null;

            string urlToRequest = @"https://en.wikipedia.org/w/api.php?action=query&aplimit=" + @page + "&format=json&list=allpages&apcontinue=" + appcontinue;
            HttpWebRequest requestGC = WebRequest.Create(urlToRequest) as HttpWebRequest;
            requestGC.Method = "POST";

            StringBuilder postData = new StringBuilder();
            //int i = 0;
            //foreach (KeyValuePair<string, string> kvpRequest in objDictRequests)
            //{
            //    postData.AppendFormat(((i == 0) ? string.Empty : "&") + "{0}={1}", kvpRequest.Key, HttpUtility.UrlEncode(kvpRequest.Value));
            //    i++;
            //}
            byte[] byteArray = Encoding.UTF8.GetBytes(postData.ToString());
            requestGC.ContentType = "application/x-www-form-urlencoded";
            requestGC.ContentLength = byteArray.Length;
            using (Stream dataStream = requestGC.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
            }
            HttpWebResponse response = requestGC.GetResponse() as HttpWebResponse;
            if (response != null)
            {
                using (var sr = new StreamReader(response.GetResponseStream()))
                {
                    responseAT = sr.ReadToEnd();
                }
            }

            RootObject e = Newtonsoft.Json.JsonConvert.DeserializeObject<RootObject>(responseAT.ToString());
            try
            {
                Task returnedTask = NewMethod1(e.query);
                await returnedTask;


            }
            catch (Exception exxxx) { }


            NewMethod(e.@continue.apcontinue.ToString());
        }

        private async static Task NewMethod1(Query e)
        {
            
            foreach (var item in e.allpages)
            {
                   Dictionary<string, string> d = new Dictionary<string, string>();
                    string s = Newtonsoft.Json.JsonConvert.SerializeObject(new { title = item.title });
                    d.Add("title", s);
                Console.WriteLine(s);
                

                Task.Factory.StartNew(() => QueryProcess(d));
                
                
            }
        }

        private async static void  QueryProcess(Dictionary<string, string> objDictRequests)
        {
            try
            {
                //string urlToRequest = "http://hackloadbalance-1793559531.us-east-1.elb.amazonaws.com/HackDay/notificationReceiver/receive.php";
                string urlToRequest = "http://localhost:50233/azure.aspx";
                HttpWebRequest requestGC = WebRequest.Create(urlToRequest) as HttpWebRequest;
                requestGC.Method = "POST";
                
                StringBuilder postData = new StringBuilder();
                int i = 0;
                foreach (KeyValuePair<string, string> kvpRequest in objDictRequests)
                {
                    postData.AppendFormat(((i == 0) ? string.Empty : "&") + "{0}={1}", kvpRequest.Key, Uri.EscapeDataString(kvpRequest.Value));
                    
                    i++;
                }
                byte[] byteArray = Encoding.UTF8.GetBytes(postData.ToString());
                requestGC.ContentType = "application/x-www-form-urlencoded";
                requestGC.ContentLength = byteArray.Length;
                using (Stream dataStream = requestGC.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    dataStream.Close();
                }
            }
            catch (Exception e) { }


        }

    }



    public class Continue
    {
        public string apcontinue { get; set; }
        public string @continue { get; set; }
    }

    public class Allpage
    {
        public int pageid { get; set; }
        public int ns { get; set; }
        public string title { get; set; }
    }

    public class Query
    {
        public List<Allpage> allpages { get; set; }
    }

    public class RootObject
    {
        public string batchcomplete { get; set; }
        public Continue @continue { get; set; }
        public Query query { get; set; }
    }
    public class head {
        public string title { get; set; }
    }
}
