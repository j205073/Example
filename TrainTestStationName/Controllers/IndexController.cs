using ChsChtTranslate;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using TrainTestStationName.Models;

namespace TrainTestStationName.Controllers
{
    public class IndexController : Controller
    {
        public static bool ValidateServerCertificate(Object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        // GET: Index
        public ActionResult Index()
        {
            LanModel model = new LanModel();
            model.StationName = GetChineseStationName();
            return View(model);
        }

        public string GetChineseStationName()
        {
            string url = @"https://kyfw.12306.cn/otn/resources/js/framework/station_name.js";
            // 設定 HTTPS 連線時，不要理會憑證的有效性問題
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(ValidateServerCertificate);
            WebClient client = new WebClient();
            Stream data = client.OpenRead(url);
            StreamReader reader = new StreamReader(data);
            string stationName = reader.ReadToEnd();
            data.Close();
            reader.Close();
            string rStrionName = stationName.Replace("var station_names =", string.Empty);
            string traStationName = LanguageTool.ToTraditional(rStrionName);
            return traStationName;
        }
    }
}