using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ShaulisBlogMvc_3.Controllers
{
    //this is an internal api to call google location webservice since we cannot make a call directly from client due to cors problem
    public class LocationsController : ApiController
    {
        [HttpGet]
        public string Get(string Location, string AdditionalParams)
        {
            WebClient _client = new WebClient();
            string a = _client.DownloadString("https://maps.googleapis.com/maps/api/place/autocomplete/json?key=AIzaSyC27aoaronxgrVHbDB_L7zRLLxAvj_OZsc&input=" + Location + "&" + AdditionalParams);
            return a;
        }
    }
}
