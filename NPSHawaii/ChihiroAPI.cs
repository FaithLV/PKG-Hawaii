using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net;
using System.IO;

namespace NPSHawaii
{
    class ChihiroAPI
    {
        public const string ChihiroBaseUrl = "https://store.playstation.com/chihiro-api/viewfinder/";

        public ChihiroAPI()
        {

        }

        Dictionary<string, string> RegionCodes = new Dictionary<string, string>()
        {
            { "EU" , "GB/en" },
            { "US", "CA/en" },
            { "JP", "JP/jp" },
            { "ASIA", "JP/jp" }
        };

        //Return a PSN store item
        public PSNItem PSNItemAPI(string ContentID, string Region)
        {
            GETResponse APIResponse;
            try
            {
                APIResponse = GETRequestAsync(ContentID, Region);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            
            PSNItem item = new PSNItem();
            string body = APIResponse.Body;
            item = PSNItem.FromJson(body);
            return item;
        }

        //GET request to Playstation Store Chihiro API
        private GETResponse GETRequestAsync(string contentid, string region)
        {
            Console.WriteLine($"Starting GET Request with {contentid} in {region}");

            GETResponse RESTResponse = new GETResponse();
            Uri uri = new Uri($"{ChihiroBaseUrl}/{RegionCodes[region]}/25/{contentid}");
            Console.WriteLine(uri);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                RESTResponse.StatusCode = response.StatusCode.ToString();
                RESTResponse.Body = reader.ReadToEnd();
            }

            Console.WriteLine(RESTResponse.Body);

            return RESTResponse;

        }

    }
}
