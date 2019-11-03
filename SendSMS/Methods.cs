using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Collections.Specialized;
using System.Net;
using Newtonsoft.Json.Linq;

namespace SendSMS
{
    class Methods
    {
        public static string sendSMS(string Message, String Recepient, string APIKey)
        {
            String message = System.Web.HttpUtility.UrlEncode(Message);
            using (var wb = new WebClient())
            {
                byte[] response = wb.UploadValues("https://api.textlocal.in/send/", new NameValueCollection()
                {
                {"apikey" , APIKey},
                {"numbers" , Recepient},
                {"message" , message},
                {"sender" , "TXTLCL"},
                {"test","true" }
                });
                string result = System.Text.Encoding.UTF8.GetString(response);
                return result;
            }
        }

        public static string sendBulkSMS(Dictionary<String, String> NumberMessageDict, string APIKey)
        {
            string result = "";
            string message = "";
            if (NumberMessageDict.Count > 0)
            {
                foreach (var key in NumberMessageDict)
                {
                    message = message + "{\"number\":\"" + key.Key.ToString() + "\",\"text\":\"" + HttpUtility.UrlEncode(key.Value.ToString()) + "\"},";
                }
                message = message.Substring(0, message.Length - 1);
                using (var wb = new WebClient())
                {
                    byte[] response = wb.UploadValues("https://api.textlocal.in/bulk_json", new NameValueCollection()
                {
                {"apikey" , APIKey},
                {"data" , "{\"messages\":["+message+"]}" },
                {"sender" , "TXTLCL"},
                {"test","true" }
                });
                    result = System.Text.Encoding.UTF8.GetString(response);

                }
            }
            else
            {
                result = "{\"errors\":[{\"message\":\"No Recepients in the input.\"}],\"status\":\"failure\"}";
            }
            return result;
        }

        public static string sendBulkWhatsapp(Dictionary<String, String> NumberMessageDict, string APIKey)
        {
            string result = "";
            string message = "";
            if (NumberMessageDict.Count > 0)
            {
                foreach (var key in NumberMessageDict)
                {
                    message = message + "{\"number\":\"" + key.Key.ToString() + "\",\"template\":{\"id\":\"1\",\"merge_fields\":{\"msg\":\"" + key.Value + "\"}}},";
                }
                message = message.Substring(0, message.Length - 1);
                using (var wb = new WebClient())
                {
                    byte[] response = wb.UploadValues("https://api.textlocal.in/bulk_json", new NameValueCollection()
                {
                {"send_channel","whatsapp" },
                {"apikey" , APIKey},
                {"data" , "{\"messages\":["+message+"]}" },
                {"sender" , "TXTLCL"},
                {"test","true" }
                });
                    result = System.Text.Encoding.UTF8.GetString(response);

                }
            }
            else
            {
                result = "{\"errors\":[{\"message\":\"No Recepients in the input.\"}],\"status\":\"failure\"}";
            }
            return result;
        }

        public static Boolean getStatusFromResponse(String json)
        {
            Boolean status = false;
            string source = json;
            dynamic data = JObject.Parse(source);
            Console.WriteLine(data.status);
            if (!data.status.ToString().Trim().Equals("failure"))
            {
                status = true;
            }
            return status;
        }

    }
}
