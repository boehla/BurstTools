using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Lib {
    public class HttpRequest {
        static private int TIMEOUT = 5000;
        static public string makeHttpRequestPost(string url) {
            // Create a request using a URL that can receive a post. 
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.UserAgent = @"Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US; rv:1.8.0.4) Gecko/20060508 Firefox/1.5.0.4";
            // Set the Method property of the request to POST.
            request.Timeout = TIMEOUT;
            request.Method = "POST";
            // Create POST data and convert it to a byte array.
            // Set the ContentType property of the WebRequest.
            request.ContentType = "application/x-www-form-urlencoded";
            // Get the request stream.
            Stream dataStream = request.GetRequestStream();
            // Close the Stream object.
            dataStream.Close();
            // Get the response.
            WebResponse response = request.GetResponse();
            // Display the status.
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            // Get the stream containing content returned by the server.
            dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.
            string responseFromServer = reader.ReadToEnd();
            // Display the content.
            Console.WriteLine(responseFromServer);
            // Clean up the streams.
            reader.Close();
            dataStream.Close();
            response.Close();

            return responseFromServer;
        }

        static public string makeHttpRequestGet(string url) {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.UserAgent = @"Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US; rv:1.8.0.4) Gecko/20060508 Firefox/1.5.0.4";
            request.Timeout = TIMEOUT;
            // Set some reasonable limits on resources used by this request
            request.MaximumAutomaticRedirections = 4;
            request.MaximumResponseHeadersLength = 4;
            // Set credentials to use for this request.
            request.Credentials = CredentialCache.DefaultCredentials;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();


            // Get the stream associated with the response.
            Stream receiveStream = response.GetResponseStream();

            // Pipes the stream to a higher level stream reader with the required encoding format. 
            StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);


            string reet = readStream.ReadToEnd();

            response.Close();
            readStream.Close();

            return reet;
        }

        public static JObject makeRequest(string url, List<HttpProp> data, bool post = true) {
            string rurl = url;

            if (data != null) {
                for (int i = 0; i < data.Count; i++) {
                    if (i == 0) rurl += "?";
                    rurl += data[i].Name + "=" + HttpUtility.UrlEncode(data[i].Value) + "&";
                }
            }
            if (post) {
                return JObject.Parse(HttpRequest.makeHttpRequestPost(rurl));
            } else {
                return JObject.Parse(HttpRequest.makeHttpRequestGet(rurl));
            }
            
        }
    }
    public class HttpProp {
        public string Name { get; set; }
        public string Value { get; set; }

        public HttpProp(string name, string value) {
            this.Name = name;
            this.Value = value;
        }
        public HttpProp(string name, object value) {
            this.Name = name;
            this.Value = Lib.Converter.toString(value);
        }
    }
}
