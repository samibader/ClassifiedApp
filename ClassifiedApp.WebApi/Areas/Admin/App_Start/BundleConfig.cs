using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace ClassifiedApp.WebApi.Areas.Admin.App_Start
{
    internal static class BundleConfig
    {
        internal static void RegisterBundles(BundleCollection bundles)
        {
            // CSS style (bootstrap/inspinia)
            bundles.Add(new StyleBundle("~/Areas/Admin/Content/css").Include(
                      "~/Areas/Admin/Content/bootstrap.min.css",
                      //"~/Areas/Admin/Content/plugins/bootstrap-rtl/bootstrap-rtl.min.css",
                      "~/Areas/Admin/Content/animate.css",
                      "~/Areas/Admin/Content/style.css"));

            // Font Awesome icons
            bundles.Add(new StyleBundle("~/Areas/Admin/font-awesome/css").Include(
                      "~/Areas/Admin/fonts/font-awesome/css/font-awesome.min.css"));

            // jQuery
            bundles.Add(new ScriptBundle("~/Areas/Admin/bundles/jquery").Include(
                        "~/Areas/Admin/Scripts/jquery-2.1.1.min.js"));

            // jQueryUI CSS
            bundles.Add(new ScriptBundle("~/Areas/Admin/Scripts/plugins/jquery-ui/jqueryuiStyles").Include(
                        "~/Areas/Admin/Scripts/plugins/jquery-ui/jquery-ui.css"));

            // jQueryUI 
            bundles.Add(new StyleBundle("~/Areas/Admin/bundles/jqueryui").Include(
                        "~/Areas/Admin/Scripts/plugins/jquery-ui/jquery-ui.min.js"));

            // Bootstrap
            bundles.Add(new ScriptBundle("~/Areas/Admin/bundles/bootstrap").Include(
                      "~/Areas/Admin/Scripts/bootstrap.min.js"));

            // Inspinia script
            bundles.Add(new ScriptBundle("~/Areas/Admin/bundles/inspinia").Include(
                      "~/Areas/Admin/Scripts/plugins/metisMenu/metisMenu.min.js",
                      "~/Areas/Admin/Scripts/plugins/pace/pace.min.js",
                      "~/Areas/Admin/Scripts/app/inspinia.min.js"));

            // Inspinia skin config script
            bundles.Add(new ScriptBundle("~/Areas/Admin/bundles/skinConfig").Include(
                      "~/Areas/Admin/Scripts/app/skin.config.min.js"));

            // SlimScroll
            bundles.Add(new ScriptBundle("~/Areas/Admin/plugins/slimScroll").Include(
                      "~/Areas/Admin/Scripts/plugins/slimscroll/jquery.slimscroll.min.js"));

            // Peity
            bundles.Add(new ScriptBundle("~/Areas/Admin/plugins/peity").Include(
                      "~/Areas/Admin/Scripts/plugins/peity/jquery.peity.min.js"));

            // Video responsible
            bundles.Add(new ScriptBundle("~/Areas/Admin/plugins/videoResponsible").Include(
                      "~/Areas/Admin/Scripts/plugins/video/responsible-video.js"));

            // Lightbox gallery css styles
            bundles.Add(new StyleBundle("~/Areas/Admin/Content/plugins/blueimp/css/").Include(
                      "~/Areas/Admin/Content/plugins/blueimp/css/blueimp-gallery.min.css"));

            // Lightbox gallery
            bundles.Add(new ScriptBundle("~/Areas/Admin/plugins/lightboxGallery").Include(
                      "~/Areas/Admin/Scripts/plugins/blueimp/jquery.blueimp-gallery.min.js"));

            // Sparkline
            bundles.Add(new ScriptBundle("~/Areas/Admin/plugins/sparkline").Include(
                      "~/Areas/Admin/Scripts/plugins/sparkline/jquery.sparkline.min.js"));

            // Morriss chart css styles
            bundles.Add(new StyleBundle("~/Areas/Admin/plugins/morrisStyles").Include(
                      "~/Areas/Admin/Content/plugins/morris/morris-0.4.3.min.css"));

            // Morriss chart
            bundles.Add(new ScriptBundle("~/Areas/Admin/plugins/morris").Include(
                      "~/Areas/Admin/Scripts/plugins/morris/raphael-2.1.0.min.js",
                      "~/Areas/Admin/Scripts/plugins/morris/morris.js"));

            // Flot chart
            bundles.Add(new ScriptBundle("~/Areas/Admin/plugins/flot").Include(
                      "~/Areas/Admin/Scripts/plugins/flot/jquery.flot.js",
                      "~/Areas/Admin/Scripts/plugins/flot/jquery.flot.tooltip.min.js",
                      "~/Areas/Admin/Scripts/plugins/flot/jquery.flot.resize.js",
                      "~/Areas/Admin/Scripts/plugins/flot/jquery.flot.pie.js",
                      "~/Areas/Admin/Scripts/plugins/flot/jquery.flot.time.js",
                      "~/Areas/Admin/Scripts/plugins/flot/jquery.flot.spline.js"));

            // Rickshaw chart
            bundles.Add(new ScriptBundle("~/Areas/Admin/plugins/rickshaw").Include(
                      "~/Areas/Admin/Scripts/plugins/rickshaw/vendor/d3.v3.js",
                      "~/Areas/Admin/Scripts/plugins/rickshaw/rickshaw.min.js"));

            // ChartJS chart
            bundles.Add(new ScriptBundle("~/Areas/Admin/plugins/chartJs").Include(
                      "~/Areas/Admin/Scripts/plugins/chartjs/Chart.min.js"));

            // iCheck css styles
            bundles.Add(new StyleBundle("~/Areas/Admin/Content/plugins/iCheck/iCheckStyles").Include(
                      "~/Areas/Admin/Content/plugins/iCheck/custom.css"));

            // iCheck
            bundles.Add(new ScriptBundle("~/Areas/Admin/plugins/iCheck").Include(
                      "~/Areas/Admin/Scripts/plugins/iCheck/icheck.min.js"));

            // dataTables css styles
            bundles.Add(new StyleBundle("~/Areas/Admin/Content/plugins/dataTables/dataTablesStyles").Include(
                      "~/Areas/Admin/Content/plugins/dataTables/dataTables.bootstrap.css",
                      "~/Areas/Admin/Content/plugins/dataTables/dataTables.responsive.css",
                      "~/Areas/Admin/Content/plugins/dataTables/dataTables.tableTools.min.css"));

            // dataTables 
            bundles.Add(new ScriptBundle("~/Areas/Admin/plugins/dataTables").Include(
                      "~/Areas/Admin/Scripts/plugins/dataTables/jquery.dataTables.js",
                      "~/Areas/Admin/Scripts/plugins/dataTables/dataTables.bootstrap.js",
                      "~/Areas/Admin/Scripts/plugins/dataTables/dataTables.responsive.js",
                      "~/Areas/Admin/Scripts/plugins/dataTables/dataTables.tableTools.min.js"));

            // jeditable 
            bundles.Add(new ScriptBundle("~/Areas/Admin/plugins/jeditable").Include(
                      "~/Areas/Admin/Scripts/plugins/jeditable/jquery.jeditable.js"));

            // jqGrid styles
            bundles.Add(new StyleBundle("~/Areas/Admin/Content/plugins/jqGrid/jqGridStyles").Include(
                      "~/Areas/Admin/Content/plugins/jqGrid/ui.jqgrid.css"));

            // jqGrid 
            bundles.Add(new ScriptBundle("~/Areas/Admin/plugins/jqGrid").Include(
                      "~/Areas/Admin/Scripts/plugins/jqGrid/i18n/grid.locale-en.js",
                      "~/Areas/Admin/Scripts/plugins/jqGrid/jquery.jqGrid.min.js"));

            // codeEditor styles
            bundles.Add(new StyleBundle("~/Areas/Admin/plugins/codeEditorStyles").Include(
                      "~/Areas/Admin/Content/plugins/codemirror/codemirror.css",
                      "~/Areas/Admin/Content/plugins/codemirror/ambiance.css"));

            // codeEditor 
            bundles.Add(new ScriptBundle("~/Areas/Admin/plugins/codeEditor").Include(
                      "~/Areas/Admin/Scripts/plugins/codemirror/codemirror.js",
                      "~/Areas/Admin/Scripts/plugins/codemirror/mode/javascript/javascript.js"));

            // codeEditor 
            bundles.Add(new ScriptBundle("~/Areas/Admin/plugins/nestable").Include(
                      "~/Areas/Admin/Scripts/plugins/nestable/jquery.nestable.js"));

            // validate 
            bundles.Add(new ScriptBundle("~/Areas/Admin/plugins/validate").Include(
                      "~/Areas/Admin/Scripts/plugins/validate/jquery.validate.min.js"));

            // fullCalendar styles
            bundles.Add(new StyleBundle("~/Areas/Admin/plugins/fullCalendarStyles").Include(
                      "~/Areas/Admin/Content/plugins/fullcalendar/fullcalendar.css"));

            // fullCalendar 
            bundles.Add(new ScriptBundle("~/Areas/Admin/plugins/fullCalendar").Include(
                      "~/Areas/Admin/Scripts/plugins/fullcalendar/moment.min.js",
                      "~/Areas/Admin/Scripts/plugins/fullcalendar/fullcalendar.min.js"));

            // vectorMap 
            bundles.Add(new ScriptBundle("~/Areas/Admin/plugins/vectorMap").Include(
                      "~/Areas/Admin/Scripts/plugins/jvectormap/jquery-jvectormap-1.2.2.min.js",
                      "~/Areas/Admin/Scripts/plugins/jvectormap/jquery-jvectormap-world-mill-en.js"));

            // ionRange styles
            bundles.Add(new StyleBundle("~/Areas/Admin/Content/plugins/ionRangeSlider/ionRangeStyles").Include(
                      "~/Areas/Admin/Content/plugins/ionRangeSlider/ion.rangeSlider.css",
                      "~/Areas/Admin/Content/plugins/ionRangeSlider/ion.rangeSlider.skinFlat.css"));

            // ionRange 
            bundles.Add(new ScriptBundle("~/Areas/Admin/plugins/ionRange").Include(
                      "~/Areas/Admin/Scripts/plugins/ionRangeSlider/ion.rangeSlider.min.js"));

            // dataPicker styles
            bundles.Add(new StyleBundle("~/Areas/Admin/plugins/dataPickerStyles").Include(
                      "~/Areas/Admin/Content/plugins/datapicker/datepicker3.css"));

            // dataPicker 
            bundles.Add(new ScriptBundle("~/Areas/Admin/plugins/dataPicker").Include(
                      "~/Areas/Admin/Scripts/plugins/datapicker/bootstrap-datepicker.js"));

            // nouiSlider styles
            bundles.Add(new StyleBundle("~/Areas/Admin/plugins/nouiSliderStyles").Include(
                      "~/Areas/Admin/Content/plugins/nouslider/jquery.nouislider.css"));

            // nouiSlider 
            bundles.Add(new ScriptBundle("~/Areas/Admin/plugins/nouiSlider").Include(
                      "~/Areas/Admin/Scripts/plugins/nouslider/jquery.nouislider.min.js"));

            // jasnyBootstrap styles
            bundles.Add(new StyleBundle("~/Areas/Admin/plugins/jasnyBootstrapStyles").Include(
                      "~/Areas/Admin/Content/plugins/jasny/jasny-bootstrap.min.css"));

            // jasnyBootstrap 
            bundles.Add(new ScriptBundle("~/Areas/Admin/plugins/jasnyBootstrap").Include(
                      "~/Areas/Admin/Scripts/plugins/jasny/jasny-bootstrap.min.js"));

            // switchery styles
            bundles.Add(new StyleBundle("~/Areas/Admin/plugins/switcheryStyles").Include(
                      "~/Areas/Admin/Content/plugins/switchery/switchery.css"));

            // switchery 
            bundles.Add(new ScriptBundle("~/Areas/Admin/plugins/switchery").Include(
                      "~/Areas/Admin/Scripts/plugins/switchery/switchery.js"));

            // chosen styles
            bundles.Add(new StyleBundle("~/Areas/Admin/Content/plugins/chosen/chosenStyles").Include(
                      "~/Areas/Admin/Content/plugins/chosen/chosen.css"));

            // chosen 
            bundles.Add(new ScriptBundle("~/Areas/Admin/plugins/chosen").Include(
                      "~/Areas/Admin/Scripts/plugins/chosen/chosen.jquery.js"));

            // knob 
            bundles.Add(new ScriptBundle("~/Areas/Admin/plugins/knob").Include(
                      "~/Areas/Admin/Scripts/plugins/jsKnob/jquery.knob.js"));

            // wizardSteps styles
            bundles.Add(new StyleBundle("~/Areas/Admin/plugins/wizardStepsStyles").Include(
                      "~/Areas/Admin/Content/plugins/steps/jquery.steps.css"));

            // wizardSteps 
            bundles.Add(new ScriptBundle("~/Areas/Admin/plugins/wizardSteps").Include(
                      "~/Areas/Admin/Scripts/plugins/staps/jquery.steps.min.js"));

            // dropZone styles
            bundles.Add(new StyleBundle("~/Areas/Admin/Content/plugins/dropzone/dropZoneStyles").Include(
                      "~/Areas/Admin/Content/plugins/dropzone/basic.css",
                      "~/Areas/Admin/Content/plugins/dropzone/dropzone.css"));

            // dropZone 
            bundles.Add(new ScriptBundle("~/Areas/Admin/plugins/dropZone").Include(
                      "~/Areas/Admin/Scripts/plugins/dropzone/dropzone.js"));

            // summernote styles
            bundles.Add(new StyleBundle("~/Areas/Admin/plugins/summernoteStyles").Include(
                      "~/Areas/Admin/Content/plugins/summernote/summernote.css",
                      "~/Areas/Admin/Content/plugins/summernote/summernote-bs3.css"));

            // summernote 
            bundles.Add(new ScriptBundle("~/Areas/Admin/plugins/summernote").Include(
                      "~/Areas/Admin/Scripts/plugins/summernote/summernote.min.js"));

            // toastr notification 
            bundles.Add(new ScriptBundle("~/Areas/Admin/plugins/toastr").Include(
                      "~/Areas/Admin/Scripts/plugins/toastr/toastr.min.js"));

            // toastr notification styles
            bundles.Add(new StyleBundle("~/Areas/Admin/plugins/toastrStyles").Include(
                      "~/Areas/Admin/Content/plugins/toastr/toastr.min.css"));

            // color picker
            bundles.Add(new ScriptBundle("~/Areas/Admin/plugins/colorpicker").Include(
                      "~/Areas/Admin/Scripts/plugins/colorpicker/bootstrap-colorpicker.min.js"));

            // color picker styles
            bundles.Add(new StyleBundle("~/Areas/Admin/Content/plugins/colorpicker/colorpickerStyles").Include(
                      "~/Areas/Admin/Content/plugins/colorpicker/bootstrap-colorpicker.min.css"));

            // image cropper
            bundles.Add(new ScriptBundle("~/Areas/Admin/plugins/imagecropper").Include(
                      "~/Areas/Admin/Scripts/plugins/cropper/cropper.min.js"));

            // image cropper styles
            bundles.Add(new StyleBundle("~/Areas/Admin/plugins/imagecropperStyles").Include(
                      "~/Areas/Admin/Content/plugins/cropper/cropper.min.css"));

            // jsTree
            bundles.Add(new ScriptBundle("~/Areas/Admin/plugins/jsTree").Include(
                      "~/Areas/Admin/Scripts/plugins/jsTree/jstree.min.js"));

            // jsTree styles
            bundles.Add(new StyleBundle("~/Areas/Admin/Content/plugins/jsTree").Include(
                      "~/Areas/Admin/Content/plugins/jsTree/style.css"));

        }
    }
}