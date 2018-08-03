using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using azureclass;

namespace Azurefn
{
    public partial class azure : System.Web.UI.Page
    {        
        protected void Page_Load(object sender, EventArgs e)
        {
            azurefunciton();
        }

        public void azurefunciton()
        {
            //Authentication a = new Authentication();
            //auth Token = a.gettokenbyrefresh();
           
            PHPRequests.WebRequestO365(Appcontext.resource + "/v1.0/users/xxxxxxxxxx@offix.info/messages/AAMkADkwxxxxxxxxxA=/move", json, Token, Constants.Post, Constants.contentTypeJson);
        }

        
    }

   
}