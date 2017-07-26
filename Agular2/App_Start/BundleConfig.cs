using System.Web.Optimization;

namespace Agular2.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            //Inculde angular js   
            bundles.Add(new ScriptBundle("~/Scripts/AgnularJs").Include(
            "~/Scripts/angular.js"));
            bundles.Add(new ScriptBundle("~/Scripts/jquery").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/jquery.*",
                "~/Scripts/jquery-ui-{version}.js")
            );
        }
    }
}