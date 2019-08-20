using System.Web;
using System.Web.Optimization;

namespace DG_BugTracker
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {


            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            //--------------------------------------------------------------

            //my jquery bundle
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/KitScripts/core/jquery.min.js"));



            //UI kit + main styling
            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/css/material-kit.css",
                "~/Content/css/main.css"));

            //UI kit scripts
            bundles.Add(new ScriptBundle("~/bundles/materialkit").Include(
                "~/Scripts/KitScripts/core/popper.min.js",
                "~/Scripts/KitScripts/core/bootstrap-material-design.min.js",
                "~/Scripts/KitScripts/material-kit.min.js"));

            //data tables plugin styles bundle
            bundles.Add(new StyleBundle("~/Content/DataTables").Include(
                "~/Content/DataTables/datatables.css",
                "~/Content/DataTables/dataTables.bootstrap4.min.css"));
            

            //data tables plugin script bundle
            bundles.Add(new ScriptBundle("~/Scripts/DataTables").Include(
                "~/Scripts/DataTables/dataTables.js",
                "~/Scripts/DataTables/dataTables.bootstrap4.min.js"
                ));
        }
    }
}
