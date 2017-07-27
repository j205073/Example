using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using log4net;
using log4net.Config;
using log4net.Repository.Hierarchy;
namespace Log4Net.Controllers
{
    public class IndexController : Controller
    {
        //
        // GET: /Index/
        ILog log = log4net.LogManager.GetLogger(typeof(IndexController));

        public ActionResult Index()
        {
            log.Debug("Debug message");
            log.Warn("Warn message");
            log.Error("Error message");
            log.Fatal("Fatal message");
            ViewBag.Title = "Home Page";
            return View();
        }
	}
}