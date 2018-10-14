using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Optimization;

namespace POC_Web {

    public class BundleConfig {

        public static void RegisterBundles(BundleCollection bundles) {

            var scriptBundle = new ScriptBundle("~/Scripts/bundle");
            var styleBundle = new StyleBundle("~/Content/bundle");

            // jQuery
            scriptBundle
                .Include("~/Scripts/jquery-3.1.0.js");

            // Bootstrap
            scriptBundle
                .Include("~/Scripts/bootstrap.js");

            // Bootstrap
            styleBundle
                .Include("~/Content/bootstrap.css");

            // Custom site styles
            styleBundle
                .Include("~/Content/Site.css");

            styleBundle
                .Include("~/Content/poc_styles.css");

            // Xonomy styles and Js
            scriptBundle
              .Include("~/Content/xonomy-3.5.0/xonomy.js");
            styleBundle
                .Include("~/Content/xonomy-3.5.0/xonomy.css");

            bundles.Add(scriptBundle);
            bundles.Add(styleBundle);

#if !DEBUG
            BundleTable.EnableOptimizations = true;
#endif
        }
    }
}