using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace OneSignal.Controllers
{
    //[Authorize(Roles = "Admin")]
    //[RoutePrefix("api/Account")]
    public class AppsController : ApiController
    {
        public AppsController() { }
        [HttpGet]
        public JArray View_Apps()
        {


            JArray arrResult = new JArray();
           // string responseContent = null;
            try
            {
                string str = WebApiConfig.URL_Notification;// +  WebApiConfig.APP_ID;// "?app_id=" + WebApiConfig.API_KEY;

                var request = WebRequest.Create(str) as HttpWebRequest;
                request.KeepAlive = true;
                request.Method = "GET";
                request.ContentType = "application/json; charset=utf-8";

                request.Headers.Add("Authorization", "Basic " + WebApiConfig.API_KEY);


                //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(str);
                //request.ContentType = "application/json";
                //request.Headers["Authorization"] = "Basic " + WebApiConfig.API_KEY;
                //request.PreAuthenticate = true;
                 
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;




                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);

                    JArray array = JArray.Parse(reader.ReadToEnd());
                 //   JObject Jobject = JObject.Parse(reader.ReadToEnd());
                  //  JArray array = JArray.Parse(Jobject.GetValue("apps").ToString());


                    for (int i = 0; i < array.Count; i++)
                    {

                        // successful = Number of devices successfully transmitted
                        // converted = Number of users who have clicked / tapped on your notification.


                        int successful = Convert.ToInt32(array[i]["successful"]);
                        int converted = Convert.ToInt32(array[i]["converted"]);
                        //int rate_of_click = (100 / successful * converted);

                        JObject obj = new JObject();
                        obj.Add("id", array[i]["id"]);
                        obj.Add("name", array[i]["name"]);
                        obj.Add("players", array[i]["players"]);
                        obj.Add("site_name", array[i]["site_name"]);
         

                        arrResult.Add(obj);


                    }

                }
                return arrResult;
            }
            catch (Exception e)
            {
                JArray arrError = new JArray();

                JObject objError = new JObject();
                objError.Add("status", NUMARATOR.ERROR.ToString());
                objError.Add("messages", e.ToString());
                arrError.Add(objError);


                return arrError;

            }

        }

        [HttpGet]
        public JObject Add_App(string name)
        {
           
            var request = WebRequest.Create(WebApiConfig.URL_Notification + "?name=" + name) as HttpWebRequest;

            request.KeepAlive = true;
            request.Method = "POST";
            request.ContentType = "application/json; charset=utf-8";
            request.Headers.Add("Authorization", "Basic " + WebApiConfig.API_KEY);

            var serializer = new JavaScriptSerializer();
            var obj = new
            {
                name = name,

            };
            var param = serializer.Serialize(obj);
            byte[] byteArray = Encoding.UTF8.GetBytes(param);

            JObject objResult = new JObject();

            try
            {
                using (var writer = request.GetRequestStream())
                {
                    writer.Write(byteArray, 0, byteArray.Length);
                }

                using (var response = request.GetResponse() as HttpWebResponse)
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {

                        JObject Jobject = JObject.Parse(reader.ReadToEnd());

                        objResult.Add("status", NUMARATOR.SUCCESS.ToString());
                        objResult.Add("id", Jobject.GetValue("id"));


                    }
                }
               // return responses;
                    return objResult;

            }
            catch (WebException e)
            {

                JObject objError = new JObject();

                objError.Add("status", NUMARATOR.ERROR.ToString());
                objError.Add("messages", e.ToString());
                 return objError;
                //return responses;

            }

        }

        [HttpGet]
        public JObject Update_App(string id,string name)
        {
            HttpResponseMessage responses = null;
            var request = WebRequest.Create(WebApiConfig.URL_Notification + "/" + id + "?name=" + name) as HttpWebRequest;

            request.KeepAlive = true;
            request.Method = "PUT";
            request.ContentType = "application/json; charset=utf-8";
            request.Headers.Add("Authorization", "Basic " + WebApiConfig.API_KEY);

            //HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            //return response;

            var serializer = new JavaScriptSerializer();
            var obj = new
            {
                name = name,

            };
            var param = serializer.Serialize(obj);
            byte[] byteArray = Encoding.UTF8.GetBytes(param);

            JObject objResult = new JObject();

            try
            {
                using (var writer = request.GetRequestStream())
                {
                    writer.Write(byteArray, 0, byteArray.Length);
                }

                using (var response = request.GetResponse() as HttpWebResponse)
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {

                        JObject Jobject = JObject.Parse(reader.ReadToEnd());

                        objResult.Add("status", NUMARATOR.SUCCESS.ToString());
                        objResult.Add("id", Jobject.GetValue("id"));


                    }
                }
                // return responses;
                return objResult;

            }
            catch (WebException e)
            {

                JObject objError = new JObject();

                objError.Add("status", NUMARATOR.ERROR.ToString());
                objError.Add("messages", e.ToString());
                return objError;
                //return responses;

            }

        }
        enum NUMARATOR
        {
            SUCCESS,
            ERROR
        }
    }
}