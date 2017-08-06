using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nlog.Controllers
{
    public class IndexController : Controller
    {
        //
        // GET: /Index/
        public ActionResult Index()
        {
            Logger logger = LogManager.GetCurrentClassLogger();
            logger.Trace("我是Trace");
            logger.Debug("我是Debug");
            logger.Info("我是Info");
            logger.Warn("我是Warn");
            logger.Error("我是Error");
            logger.Fatal("我是Fatal");           
            return View();
        }
	}
}