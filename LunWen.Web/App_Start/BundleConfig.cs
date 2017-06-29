using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace LunWen.Web.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/upload.css").Include("~/Content/upload.css"));
            bundles.Add(new ScriptBundle("").Include(""));

            //@Scripts.Render("~/Scripts/admin/DateInput/WdatePicker.js")
            //@Styles.Render("~/Content/autocomplete/jquery-ui.css")
        }
    }
}