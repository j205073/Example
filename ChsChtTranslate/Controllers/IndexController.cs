using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChsChtTranslate.Controllers
{
    public class IndexController : Controller
    {
        // GET: Index
        public ActionResult Index()
        {

            string testTraditional = "劉俊晨";
            string testSimplified = "刘俊晨";

            LanguageTool languageTool = new LanguageTool();

            var resultChs = languageTool.ToSimplified(testTraditional);
            var resultCht = languageTool.ToTraditional(testSimplified);
            return View();
        }
    }
}