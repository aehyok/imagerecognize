using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    class Program
    {
        private static string AppKey = "GmkdWwpNcjNTS7HGfPCU3han";
        private static string AppSecret = "ZeFmXBGjU4GGj43qLgIp1T4IolPSzAuP";
        static void Main(string[] args)
        {
            //var client = new RestClient("https://aip.baidubce.com/oauth/2.0/token");

            //var request = new RestRequest("", Method.GET);
            //request.AddParameter("grant_type", "client_credentials");
            //request.AddParameter("client_id", AppKey);
            //request.AddParameter("client_secret", AppSecret);

            //// execute the request
            //IRestResponse response = client.Execute(request);
            ////var content = response.Content; // raw content as string
            //var content = JsonConvert.DeserializeObject<dynamic>(response.Content);
            //Console.WriteLine(content.access_token);

            //{{  "access_token": "24.5b9925a3c1c7c4af163d78df796ff8fa.2592000.1539249200.282335-11770938",  "session_key": "9mzdDtAHkwHTAjXilJvtp9+49OvIXzDN2dARW61Pc4Mk2bTx4ZLpXAQKm8m/7kcfIYl08Y5kO1/tp/m3f7KQl03a1E1IRw==",  "scope": "brain_numbers public vis-ocr_ocr brain_ocr_scope brain_ocr_general brain_ocr_general_basic brain_ocr_general_enhanced vis-ocr_business_license brain_ocr_webimage brain_all_scope brain_ocr_idcard brain_ocr_driving_license brain_ocr_vehicle_license vis-ocr_plate_number brain_solution brain_ocr_plate_number brain_ocr_accurate brain_ocr_accurate_basic brain_ocr_receipt brain_ocr_business_license brain_solution_iocr brain_ocr_handwriting brain_ocr_vat_invoice wise_adapt lebo_resource_base lightservice_public hetu_basic lightcms_map_poi kaidian_kaidian ApsMisTest_Test权限 vis-classify_flower lpq_开放 cop_helloScope ApsMis_fangdi_permission smartapp_snsapi_base iop_autocar oauth_tp_app smartapp_smart_game_openapi",  "refresh_token": "25.5b9b7f2680db6f9e11e132bf5cafcb07.315360000.1852017200.282335-11770938",  "session_secret": "a18f9f3645bdf97f0f20786d6cfd4649",  "expires_in": 2592000}}





            var client1 = new RestClient("https://aip.baidubce.com/rest/2.0/ocr/v1/accurate_basic");

            var request1 = new RestRequest("", Method.POST);
            request1.AddParameter("access_token", "24.5b9925a3c1c7c4af163d78df796ff8fa.2592000.1539249200.282335-11770938");
            request1.AddParameter("detect_direction", false);
            request1.AddParameter("probability", false);

            var str = ImgToBase64String("test.png");
            var result = UrlEncode(str);

            request1.AddParameter("image", str);
            // execute the request
            IRestResponse response1 = client1.Execute(request1);
            //var content = response.Content; // raw content as string
            var content1 = JsonConvert.DeserializeObject<dynamic>(response1.Content);
            Console.WriteLine(content1);

            foreach(var item in content1.words_result)
            {
                Console.WriteLine(item.words);
            }

            Console.ReadLine();
        }


        public static string UrlEncode(string str)
        {
            string urlStr = System.Web.HttpUtility.UrlEncode(str);
            string base64Str = Base64Encode(urlStr);
            return urlStr;
        }
        /// <summary>
        /// Base64加密
        /// </summary>
        /// <param name="encodeType">加密采用的编码方式</param>
        /// <param name="source">待加密的明文</param>
        /// <returns></returns>
        public static string Base64Encode(string source)//Encoding encodeType, 
        {
            string encode = string.Empty;
            byte[] bytes = (Encoding.UTF8.GetBytes(source));//encodeType.GetBytes(source);
            try
            {
                encode = Convert.ToBase64String(bytes);
            }
            catch
            {
                encode = source;
            }
            return encode;
        }
        public static string ImgToBase64String(string Imagefilename)
        {
            try
            {
                Bitmap bmp = new Bitmap(Imagefilename);

                MemoryStream ms = new MemoryStream();
                bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] arr = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(arr, 0, (int)ms.Length);
                ms.Close();
                return Convert.ToBase64String(arr);
            }
            catch (Exception ex)
            {
                return null;
            }

        }

    }
}
