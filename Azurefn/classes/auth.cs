using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Web.Script.Serialization;

namespace azureclass
{
    public class auth
    {
        public string token_type { get; set; }
        public string expires_in { get; set; }
        public string ext_expires_in { get; set; }
        public string expires_on { get; set; }
        public string not_before { get; set; }
        public string resource { get; set; }
        public string access_token { get; set; }
        public int issue_time { get; set; }
        public string tenantId { get; set; }
    }


    public static class Appcontext
    {
        public static string client_id
        {
            get
            {
                return ConfigurationManager.AppSettings[Constants.client_id].ToString();
            }
        }
        public static string client_secret
        {
            get
            {
                return ConfigurationManager.AppSettings[Constants.client_secret].ToString();
            }
        }
        public static string resource
        {
            get
            {
                return ConfigurationManager.AppSettings[Constants.resource].ToString();
            }
        }
        public static string urlof365
        {
            get
            {
                return ConfigurationManager.AppSettings["365url"].ToString();
            }
        }
    }


    public class Constants
    {
        public const string client_id = "client_id";
        public const string client_secret = "client_secret";
        public const string resource = "resource";
        public const string grant_type = "grant_type";
        public const string client_credentials = "client_credentials";
        public const string Get = "GET";
        public const string Post = "POST";
        public const string contentTypeUrl = "application/x-www-form-urlencoded";
        public const string contentTypeJson = "application/json";

    }

    public class Authentication
    {
        public auth gettokenbyrefresh(string tenantId)
        {
            Dictionary<string, string> d = new Dictionary<string, string>();
            d.Add(Constants.grant_type, Constants.client_credentials);
            d.Add(Constants.client_id, Appcontext.client_id);
            d.Add(Constants.client_secret, Appcontext.client_secret);
            d.Add(Constants.resource, Appcontext.resource);
            string s = PHPRequests.RequestToPHPSync(d, Appcontext.urlof365 + tenantId+"/oauth2/token");
            JavaScriptSerializer js = new JavaScriptSerializer();
            auth authres = js.Deserialize<auth>(s);
            return authres;
        }        
    }

    public class Functions
    {
        private auth Authetication = null;
        public Functions()
        {
            Authentication a = new Authentication();
            this.Authetication = a.gettokenbyrefresh("1e08xxxxxxxxxxx");
        }

        public Mail getemail(string id, string email)
        {
            Mail m = new Mail();
            try
            {
                string Result = PHPRequests.HttpRequest(Appcontext.resource + "/v1.0/users/" + email + "/messages/" + id, string.Empty, this.Authetication, Constants.Get, string.Empty);
                JavaScriptSerializer js = new JavaScriptSerializer();
                m = js.Deserialize<Mail>(Result);
            }
            catch (Exception e) { }
            return m;
        }

        public Mail moveEmail(string id, string folder, string email)
        {
            Mail m = new Mail();
            try
            {
                Move move = new Move() { destinationId = folder };
                string json = new JavaScriptSerializer().Serialize(move);
                string Result = PHPRequests.HttpRequest(Appcontext.resource + "/v1.0/users/" + email + "/messages/" + id + "/move", json, this.Authetication, Constants.Get, string.Empty);
                JavaScriptSerializer js = new JavaScriptSerializer();
                m = js.Deserialize<Mail>(Result);
            }
            catch (Exception e) { }
            return m;
        }

    }

    public class Move
    {
        public string destinationId { get; set; }
    }

    /// <summary>
    /// <see cref="Client_state"/> 
    /// </summary>
    public class Client_state
    {
        public string uid { get; set; }
        public string did { get; set; }
        public string cldid { get; set; }
    }
}