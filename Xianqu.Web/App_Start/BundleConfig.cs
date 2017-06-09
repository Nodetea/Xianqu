using System.Web;
using System.Web.Optimization;

namespace Xianqu.Web
{
    public class BundleConfig
    {
        
        public static void RegisterBundles(BundleCollection bundles) {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js","~/Scripts/fileinput.min.js","~/Raty/jquery.raty.js","~/wangEditor/js/wangEditor.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css","~/Content/shop-homepage.css","~/Content/fileinput.min.css","~/Content/simple-siderbar.css","~/wangEditor/css/wangEditor.min.css"));


        }
    }
}
