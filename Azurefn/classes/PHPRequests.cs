using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace azureclass
{
    /// <summary>
    /// All PHP request will create the object of the class to execute the HTTPRequest  <see cref="HttpRequest"/> and HTTPResponse <seealso cref="HttpResponse"/>
    /// </summary>
    public class PHPRequests
    {
        /// <summary>
        ///  this method only handle  POST request and it  will not wait for response
        /// </summary>
        /// <param name="objDictRequests">object in <see cref="Dictionary{TKey, TValue}"/> form </param>
        /// <param name="urlToRequest">url to give the <see cref="HttpRequest"/></param>
        public static void RequestToPHP(Dictionary<string, string> objDictRequests,string urlToRequest)
        {
            try
            {               
                HttpWebRequest requestGC = WebRequest.Create(urlToRequest) as HttpWebRequest;
                requestGC.Method = "POST";
                
                StringBuilder postData = new StringBuilder();
                int i = 0;            
                foreach (KeyValuePair<string, string> kvpRequest in objDictRequests)
                {
                    postData.AppendFormat(((i == 0) ? string.Empty : "&") + "{0}={1}", kvpRequest.Key, HttpUtility.UrlEncode(kvpRequest.Value));
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
            catch (Exception ex)
            {
                throw ex;
            }       

        }

        public static object RequestToPHPJson(string urlToRequest,string Json,bool sync = false)
        {
            var result = new object();
            try
            {
              
                var httpWebRequest = WebRequest.Create(urlToRequest) as HttpWebRequest;
                httpWebRequest.ContentType = "text/json";
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {                   
                    streamWriter.Write(Json);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                if (sync)
                {
                    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        result = streamReader.ReadToEnd();
                    }
                }
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public static string RequestToPHPSync(Dictionary<string, string> objDictRequests, string urlToRequest)
        {
            string responseAT = null;
            try
            {
                HttpWebRequest requestGC = WebRequest.Create(urlToRequest) as HttpWebRequest;
                requestGC.Method = "POST";
                
                StringBuilder postData = new StringBuilder();
                int i = 0;
                foreach (KeyValuePair<string, string> kvpRequest in objDictRequests)
                {
                    postData.AppendFormat(((i == 0) ? string.Empty : "&") + "{0}={1}", kvpRequest.Key, HttpUtility.UrlEncode(kvpRequest.Value));
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
                HttpWebResponse response = requestGC.GetResponse() as HttpWebResponse;
                if (response != null)
                {
                    using (var sr = new StreamReader(response.GetResponseStream()))
                    {
                        responseAT = sr.ReadToEnd();
                    }
                }

                    }
            catch (Exception ex)
            {
                throw ex;
            }
            return responseAT;
        }

        //public static string GetRequestByUrl(string urlToRequest,auth a)
        //{
        //    string responseAT = null;
        //    try
        //    {
        //        HttpWebRequest requestGC = WebRequest.Create(urlToRequest) as HttpWebRequest;
        //        requestGC.Method = "GET";
        //        requestGC.Headers.Add("Authorization", "Bearer "+ a.access_token);
        //        StringBuilder postData = new StringBuilder();                           
        //        HttpWebResponse response = requestGC.GetResponse() as HttpWebResponse;
        //        if (response != null)
        //        {
        //            using (var sr = new StreamReader(response.GetResponseStream()))
        //            {
        //                responseAT = sr.ReadToEnd();
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return responseAT;
        //}

        public static string HttpRequest(string urlToRequest,string json,auth a,string Method,string contentType)
        {
            string responseAT = null;
            try
            {
                HttpWebRequest requestGC = WebRequest.Create(urlToRequest) as HttpWebRequest;
                requestGC.Method = Method;
                requestGC.Headers.Add("Authorization", "Bearer " + a.access_token);
                StringBuilder postData = new StringBuilder();

                if (json != string.Empty)
                {
                    requestGC.ContentType = contentType;
                    requestGC.ContentLength = json.Length;
                    using (var streamWriter = new StreamWriter(requestGC.GetRequestStream()))
                    {
                        streamWriter.Write(json);
                        streamWriter.Flush();
                        streamWriter.Close();
                    }
                }


                HttpWebResponse response = requestGC.GetResponse() as HttpWebResponse;
                if (response != null)
                {
                    using (var sr = new StreamReader(response.GetResponseStream()))
                    {
                        responseAT = sr.ReadToEnd();
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return responseAT;
        }
    }
}
