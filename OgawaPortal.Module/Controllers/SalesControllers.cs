using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using OgawaPortal.Module.BusinessObjects;
using OgawaPortal.Module.BusinessObjects.Sales_Order;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace OgawaPortal.Module.Controllers
{
    public partial class SalesControllers : ViewController
    {
        GeneralControllers genCon;
        public SalesControllers()
        {
            InitializeComponent();
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            this.DeleteORDRLine.Active.SetItemValue("Enabled", false);

            ApplicationUser user = (ApplicationUser)SecuritySystem.CurrentUser;
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();

            genCon = Frame.GetController<GeneralControllers>();

            ApplicationUser user = (ApplicationUser)SecuritySystem.CurrentUser;

            /* OGW11ORDR */
            if (View.ObjectTypeInfo.Type == typeof(OGW11ORDR))
            {
                if (View.Id == "OGW10ORDR_OGW11ORDR_ListView")
                {
                    if (View is ListView && !View.IsRoot)
                    {
                        if (View.ObjectSpace.Owner is DetailView)
                        {
                            DetailView masterview = Application.MainWindow.View as DetailView;
                            if (masterview.Id == "OGW10ORDR_DetailView")
                            {
                                this.DeleteORDRLine.Active.SetItemValue("Enabled", View.ObjectSpace.Owner is DetailView && ((DetailView)View.ObjectSpace.Owner).ViewEditMode == ViewEditMode.Edit);
                            }
                        }
                    }
                }
            }
            else
            {
                this.DeleteORDRLine.Active.SetItemValue("Enabled", false);
            }

        }
        protected override void OnDeactivated()
        {
            base.OnDeactivated();
        }

        private void DeleteORDRLine_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            if (e.SelectedObjects.Count > 1)
            {
                foreach (OGW11ORDR dtl in e.SelectedObjects)
                {
                    if (dtl.ItemCode.ItemCode == dtl.ItemFather)
                    {
                        SqlConnection conn = new SqlConnection(genCon.getConnectionString());

                        genCon.executeNonQuery("EXEC sp_DeleteBOM '" + dtl.ItemCode.ItemCode + "', " + dtl.DocEntry.Oid + ", '" + dtl.FatherKey + "'");
                    }
                }

                ObjectSpace.CommitChanges();
                ObjectSpace.Refresh();
            }
            else if (e.SelectedObjects.Count == 1)
            {
                foreach (OGW11ORDR dtl in e.SelectedObjects)
                {
                    if (dtl.ItemCode.ItemCode == dtl.ItemFather)
                    {
                        genCon.executeNonQuery("EXEC sp_DeleteBOM '" + dtl.ItemCode.ItemCode + "', " + dtl.DocEntry.Oid + ", '" + dtl.FatherKey + "'");
                    }
                    else
                    {
                        throw new InvalidOperationException("Child item not allow to delete.");
                    }
                }

                ObjectSpace.CommitChanges();
                ObjectSpace.Refresh();
            }
            else
            {
                throw new InvalidOperationException("No item selected.");
            }
        }
    }
}
