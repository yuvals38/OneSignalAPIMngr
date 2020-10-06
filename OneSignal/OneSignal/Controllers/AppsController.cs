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

    public class AppsController : ApiController
    {
        public AppsController() { }
        [HttpGet]
        //Get ALL Apps
        public JArray View_Apps()
        {

            JArray arrResult = new JArray();
  
            try
            {
                string str = WebApiConfig.URL_Notification;

                var request = WebRequest.Create(str) as HttpWebRequest;
                request.KeepAlive = true;
                request.Method = "GET";
                request.ContentType = "application/json; charset=utf-8";

                request.Headers.Add("Authorization", "Basic " + WebApiConfig.API_KEY);
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;

                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);

                    JArray array = JArray.Parse(reader.ReadToEnd());

                    for (int i = 0; i < array.Count; i++)
                    {


                        int successful = Convert.ToInt32(array[i]["successful"]);
                        int converted = Convert.ToInt32(array[i]["converted"]);

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
        //Add a new App
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
  
                    return objResult;

            }
            catch (WebException e)
            {

                JObject objError = new JObject();

                objError.Add("status", NUMARATOR.ERROR.ToString());
                objError.Add("messages", e.ToString());
                 return objError;

            }

        }

        [HttpGet]
        //Update an existing App
        public JObject Update_App(string id,string name)
        {
   
            var request = WebRequest.Create(WebApiConfig.URL_Notification + "/" + id + "?name=" + name) as HttpWebRequest;

            request.KeepAlive = true;
            request.Method = "PUT";
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
                return objResult;

            }
            catch (WebException e)
            {

                JObject objError = new JObject();

                objError.Add("status", NUMARATOR.ERROR.ToString());
                objError.Add("messages", e.ToString());
                return objError;

            }

        }
        enum NUMARATOR
        {
            SUCCESS,
            ERROR
        }
    }
}