using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace LunWen.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Scripts/rsa/js").Include(
                "~/Scripts/Rsa/Barrett.js",
                "~/Scripts/Rsa/BigInt.js",
                "~/Scripts/Rsa/RSA.js",
                "~/Scripts/Rsa/RSAClient.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/Site.css", "~/Content/bootstrap.min.css"));
            bundles.Add(new ScriptBundle("~/base").Include("~/Scripts/jquery-1.10.2.min.js", "~/Scripts/bootstrap.min.js"));

            //@Scripts.Render("~/Scripts/admin/DateInput/WdatePicker.js")
            //@Styles.Render("~/Content/autocomplete/jquery-ui.css")
        }
    }
}