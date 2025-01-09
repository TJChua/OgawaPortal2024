using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.ExpressApp.Web;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using OgawaPortal.Module.BusinessObjects;
using OgawaPortal.Module.BusinessObjects.Maintenance;
using OgawaPortal.Module.BusinessObjects.Sales_Order;
using OgawaPortal.Module.Controllers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace OgawaPortal.Module.Web.Controllers
{
    public partial class WebCustomizeLayoutController : ViewController
    {
        private SingleChoiceAction ShowInPlaceReportAction;
        GeneralControllers genCon;
        public WebCustomizeLayoutController()
        {
            InitializeComponent();

            ShowInPlaceReportAction = new SingleChoiceAction(this, "InPlaceReport", PredefinedCategory.Unspecified);
            ShowInPlaceReportAction.ItemType = SingleChoiceActionItemType.ItemIsOperation;
            ShowInPlaceReportAction.ImageName = "BO_Report";
            ShowInPlaceReportAction.Caption = "Print Layout";
            ShowInPlaceReportAction.SelectionDependencyType = SelectionDependencyType.RequireMultipleObjects;
            ShowInPlaceReportAction.Execute += ShowInPlaceReportAction_Execute;
            TargetViewNesting = Nesting.Root;
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            ShowInPlaceReportAction.Items.Clear();

            /* Sales Order */
            if (View.ObjectTypeInfo.Type == typeof(OGW10ORDR))
            {
                if (checViewType())
                {
                    bool found = false;
                    bool nullfield = false;
                    ChoiceActionItem setStatusItem;

                    foreach (OGW10SLYT rpt in ObjectSpace.GetObjects<OGW10SLYT>(CriteriaOperator.Parse("ObjDoc.Code = ? and IsActive = 'True'", 
                        View.ObjectTypeInfo.Name)))
                    {
                        found = true;

                        if (nullfield == false)
                        {
                            nullfield = true;
                            setStatusItem = new ChoiceActionItem("...", rpt);
                            ShowInPlaceReportAction.Items.Add(setStatusItem);
                        }
                        setStatusItem = new ChoiceActionItem(rpt.Name, rpt);
                        ShowInPlaceReportAction.Items.Add(setStatusItem);
                    }

                    if (View is DetailView)
                    {
                        this.ShowInPlaceReportAction.Active.SetItemValue("Enabled", found);
                    }
                    else if (View is ListView)
                    {
                        this.ShowInPlaceReportAction.Active.SetItemValue("Enabled", found);
                    }
                }
            }
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
        }
        protected override void OnDeactivated()
        {
            base.OnDeactivated();
        }

        private bool checViewType()
        {
            return (View is DetailView && ((DetailView)View).IsRoot) || (View is ListView && ((ListView)View).IsRoot);
        }

        private void ShowInPlaceReportAction_Execute(object sender, SingleChoiceActionExecuteEventArgs e)
        {
            if (e.SelectedChoiceActionItem.Id != "...")
            {
                if (checViewType())
                {
                    string filename;
                    string url = null;
                    string script = null;

                    SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(genCon.getConnectionString());
                    ApplicationUser user = (ApplicationUser)SecuritySystem.CurrentUser;
                    OGW10SLYT rpt = e.SelectedChoiceActionItem.Data as OGW10SLYT;

                    if (string.IsNullOrEmpty(rpt.FilePath))
                    {
                        throw new InvalidOperationException("Invalid layout path.");
                    }

                    ReportDocument doc = new ReportDocument();
                    doc.Load(rpt.FilePath);
                    doc.DataSourceConnections[0].SetConnection(builder.DataSource, builder.InitialCatalog, builder.UserID, builder.Password);
                    doc.Refresh();

                    ParameterFieldDefinitions crParameterdef;
                    crParameterdef = doc.DataDefinition.ParameterFields;

                    ArrayList docentry = new ArrayList();
                    ArrayList selectedHdr = new ArrayList();    

                    /* Sales Order */
                    if (View.ObjectTypeInfo.Type == typeof(OGW10ORDR))
                    {
                        if (View.SelectedObjects.Count > 0)
                        {
                            foreach (var selectedObject in View.SelectedObjects)
                            {
                                selectedHdr.Add((OGW10ORDR)ObjectSpace.GetObject(selectedObject));
                            }
                        }

                        foreach (OGW10ORDR obj in selectedHdr)
                        {
                            if (obj.DocNum != null)
                            {
                                docentry.Add(obj.Oid.ToString());
                            }
                        }

                        if (docentry.Count == 0)
                        {
                            docentry.Add("0");
                        }

                        //Check if parameter exists only assign else have to filter by report to assign
                        foreach (ParameterFieldDefinition param in crParameterdef)
                        {
                            if (param.Name.Equals("DocEntry"))
                            {
                                doc.SetParameterValue("DocEntry", docentry.ToArray());
                            }

                            if (param.Name.Equals("UserID"))
                                doc.SetParameterValue("UserID", HttpContext.Current.Session["UserID"]);
                        }

                        filename = ConfigurationManager.AppSettings.Get("ReportPath").ToString() + builder.InitialCatalog
                            + "_" + user.UserName + "_OGW10ORDR_"
                            + DateTime.Parse(DateTime.Today.Date.ToString()).ToString("ddMMyyyy") + ".pdf";

                        doc.ExportToDisk(ExportFormatType.PortableDocFormat, filename);
                        doc.Close();
                        doc.Dispose();

                        url = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority +
                            ConfigurationManager.AppSettings.Get("PrintPath").ToString() + builder.InitialCatalog
                            + "_" + user.UserName + "_OGW10ORDR_"
                            + DateTime.Parse(DateTime.Today.Date.ToString()).ToString("ddMMyyyy") + ".pdf";
                        script = "window.open('" + url + "');";
                    }

                    WebWindow.CurrentRequestWindow.RegisterStartupScript("DownloadFile", script);
                }
            }
            else
            {
                throw new InvalidOperationException("No layout selected.");
            }
        }
    }
}
