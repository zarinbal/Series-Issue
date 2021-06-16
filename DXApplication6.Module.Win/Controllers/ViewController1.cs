using AnnualReport;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.Reports;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TES.Utils;

namespace DXApplication6.Module.Win.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class ViewController1 : ViewController
    {
        public ViewController1()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var repName = "Annual Report";
            ShowReport(repName);
        }

        public void ShowReport(string reportName, bool forceChartSeriesLabel = false)
        {

            var reportData = new SafetyAnnualReport();
            reportData.CollisionsByHour = LoadData();


            var theList = new List<SafetyAnnualReport>();
            theList.Add(reportData);
            var report= LoadReportFromList(reportName, theList, forceChartSeriesLabel);
            var printTool = new ReportPrintTool(report);
            printTool.ShowPreviewDialog();
        }

        private List<ReportEntryExtended<DateTime, double, string, bool>> LoadData()
        {
            var result = new List<ReportEntryExtended<DateTime, double, string, bool>>();
            result = JsonTools.ReadJsonFile<List<ReportEntryExtended<DateTime, double, string, bool>>>
                (Path.Combine(DevExpress.Persistent.Base.PathHelper.GetApplicationFolder(), "CollisionsByHour.json"));
            return result;
        }

        public XafReport LoadReportFromList<T>(string reportName, T reportData, bool forceChartSeriesLabel)
        {
            ReportData rd = GetReportFromDb(reportName);

            XafReport report = (XafReport)rd.LoadReport(this.ObjectSpace);

            using (var ms = new MemoryStream())
            {
                report.SaveLayout(ms);
                ms.Position = 0;
                report.LoadLayout(ms);
            }
            report.DataSource = reportData;

            return report;
        }

        private ReportData GetReportFromDb(string reportName)
        {
            ReportData rd = this.ObjectSpace.FindObject<ReportData>(new BinaryOperator("Name", reportName));
            if (rd == null)
            {
                throw new Exception(string.Format("Unable to find the required report: '{0}'. Please make sure, you have already imported the report layout file.", reportName));
            }

            return rd;
        }
    }
}
