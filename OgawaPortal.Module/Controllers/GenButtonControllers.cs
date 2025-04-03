using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo.DB;
using OgawaPortal.Module.BusinessObjects;
using OgawaPortal.Module.BusinessObjects.Copy_Screen;
using OgawaPortal.Module.BusinessObjects.Nonpersistent;
using OgawaPortal.Module.BusinessObjects.POS___Exchange;
using OgawaPortal.Module.BusinessObjects.POS___Logistic;
using OgawaPortal.Module.BusinessObjects.Sales_Order;
using OgawaPortal.Module.BusinessObjects.View;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace OgawaPortal.Module.Controllers
{
    public partial class GenButtonControllers : ViewController
    {
        GeneralControllers genCon;
        public GenButtonControllers()
        {
            InitializeComponent();
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            this.AddItem.Active.SetItemValue("Enabled", false);
            this.SubmitDoc.Active.SetItemValue("Enabled", false);
            this.CancelDoc.Active.SetItemValue("Enabled", false);
            this.CloseDoc.Active.SetItemValue("Enabled", false);
            this.ChangeOutlet.Active.SetItemValue("Enabled", false);
            this.ViewDoc.Active.SetItemValue("Enabled", false);

            ApplicationUser user = (ApplicationUser)SecuritySystem.CurrentUser;
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();

            genCon = Frame.GetController<GeneralControllers>();

            ApplicationUser user = (ApplicationUser)SecuritySystem.CurrentUser;

            /* OGW10ORDR */
            if (View.ObjectTypeInfo.Type == typeof(OGW10ORDR))
            {
                if (View.Id == "OGW10ORDR_DetailView" || 
                    View.Id == "OGW10ORDR_DetailView_EditOrder" ||
                    View.Id == "OGW10ORDR_DetailView_RsmOrder")
                {
                    if (((DetailView)View).ViewEditMode == ViewEditMode.Edit)
                    {
                        this.AddItem.Active.SetItemValue("Enabled", true);
                        this.SubmitDoc.Active.SetItemValue("Enabled", false);
                        this.CancelDoc.Active.SetItemValue("Enabled", false);
                        this.CloseDoc.Active.SetItemValue("Enabled", false);
                    }
                    else
                    {
                        this.AddItem.Active.SetItemValue("Enabled", false);
                        this.SubmitDoc.Active.SetItemValue("Enabled", true);
                        this.CancelDoc.Active.SetItemValue("Enabled", true);
                        this.CloseDoc.Active.SetItemValue("Enabled", true);
                    }
                }
            }
            /* OGW10ChgOutlet */
            else if (View.ObjectTypeInfo.Type == typeof(OGW10ChgOutlet))
            {
                if (View.Id == "OGW10ChgOutlet_DetailView")
                {
                    if (((DetailView)View).ViewEditMode == ViewEditMode.Edit)
                    {
                        this.ChangeOutlet.Active.SetItemValue("Enabled", true);
                    }
                    else
                    {
                        this.ChangeOutlet.Active.SetItemValue("Enabled", false);
                    }
                }
            }
            /* CopyList_OGW11ORDR */
            else if (View.ObjectTypeInfo.Type == typeof(CopyList_OGW11ORDR))
            {
                if (View.Id == "CopyList_OGW11ORDR_LookupListView")
                {
                    this.ViewDoc.Active.SetItemValue("Enabled", true);
                }
            }
            /* OGW10ORDN */
            else if (View.ObjectTypeInfo.Type == typeof(OGW10ORDN))
            {
                if (View.Id == "OGW10ORDN_DetailView")
                {
                    if (((DetailView)View).ViewEditMode == ViewEditMode.Edit)
                    {
                        this.SubmitDoc.Active.SetItemValue("Enabled", false);
                        this.CancelDoc.Active.SetItemValue("Enabled", false);
                    }
                    else
                    {
                        this.SubmitDoc.Active.SetItemValue("Enabled", true);
                        this.CancelDoc.Active.SetItemValue("Enabled", true);
                    }
                }
            }
            /* OGW10EXCO */
            else if (View.ObjectTypeInfo.Type == typeof(OGW10EXCO))
            {
                if (View.Id == "OGW10EXCO_DetailView")
                {
                    if (((DetailView)View).ViewEditMode == ViewEditMode.Edit)
                    {
                        this.SubmitDoc.Active.SetItemValue("Enabled", false);
                        this.CancelDoc.Active.SetItemValue("Enabled", false);
                    }
                    else
                    {
                        this.SubmitDoc.Active.SetItemValue("Enabled", true);
                        this.CancelDoc.Active.SetItemValue("Enabled", true);
                    }
                }
            }
            /* OGW10DREQ */
            else if (View.ObjectTypeInfo.Type == typeof(OGW10DREQ))
            {
                if (View.Id == "OGW10DREQ_DetailView")
                {
                    if (((DetailView)View).ViewEditMode == ViewEditMode.Edit)
                    {
                        this.SubmitDoc.Active.SetItemValue("Enabled", false);
                        this.CancelDoc.Active.SetItemValue("Enabled", false);
                    }
                    else
                    {
                        this.SubmitDoc.Active.SetItemValue("Enabled", true);
                        this.CancelDoc.Active.SetItemValue("Enabled", true);
                    }
                }
            }
            else
            {
                this.AddItem.Active.SetItemValue("Enabled", false);
                this.SubmitDoc.Active.SetItemValue("Enabled", false);
                this.CancelDoc.Active.SetItemValue("Enabled", false);
                this.CloseDoc.Active.SetItemValue("Enabled", false);
                this.ChangeOutlet.Active.SetItemValue("Enabled", false);
                this.ViewDoc.Active.SetItemValue("Enabled", false);
            }
        }
        protected override void OnDeactivated()
        {
            base.OnDeactivated();
        }

        private void AddItem_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            /* OGW10ORDR */
            if (View.ObjectTypeInfo.Type == typeof(OGW10ORDR))
            {
                OGW10ORDR ORDR = (OGW10ORDR)e.CurrentObject;
                ItemBrowser popoutview = (ItemBrowser)e.PopupWindow.View.CurrentObject;

                foreach (ItemCarts dtl in popoutview.itemcarts)
                {
                    OGW11ORDR ORDR11 = ObjectSpace.CreateObject<OGW11ORDR>();
                    ORDR11.Class = dtl.Class;
                    ORDR11.ItemCode = ORDR11.Session.GetObjectByKey<vwItemMasters>(dtl.ItemCode);
                    ORDR11.ItemFather = dtl.ItemCode;
                    ORDR11.UnitPrice = dtl.Price;
                    ORDR11.Order = dtl.Quantity;
                    ORDR11.BackOrder = dtl.Quantity;
                    ORDR11.FatherKey = Guid.NewGuid().ToString();
                    // Link BOM Item
                    string fatherkey = ORDR11.FatherKey;
                    ORDR.OGW11ORDR.Add(ORDR11);

                    SqlConnection conn = new SqlConnection(genCon.getConnectionString());

                    string selectbom = "EXEC sp_GetBOM '" + dtl.ItemCode + "'";
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(selectbom, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        OGW11ORDR ORDR11BOM = ObjectSpace.CreateObject<OGW11ORDR>();
                        ORDR11BOM.ItemCode = ORDR11BOM.Session.GetObjectByKey<vwItemMasters>(reader.GetString(0));
                        ORDR11BOM.ItemFather = reader.GetString(1);
                        if (dtl.Class.ToUpper() == "PROMO")
                        {
                            ORDR11BOM.Class = reader.GetString(6) + "-P";
                        }
                        else
                        {
                            ORDR11BOM.Class = dtl.Class;
                        }
                        ORDR11BOM.UnitPrice = dtl.Price;
                        ORDR11BOM.Order = dtl.Quantity * reader.GetDecimal(2);
                        ORDR11BOM.BackOrder = dtl.Quantity * reader.GetDecimal(2);
                        ORDR11BOM.FatherKey = fatherkey;
                        ORDR.OGW11ORDR.Add(ORDR11BOM);
                    }
                    conn.Close();
                }

                ObjectSpace.CommitChanges();
                ObjectSpace.Refresh();
            }
        }

        private void AddItem_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            IObjectSpace objectSpace = Application.CreateObjectSpace(typeof(ItemCodes));

            XPObjectSpace persistentObjectSpace = (XPObjectSpace)Application.CreateObjectSpace();
            SelectedData sprocData = persistentObjectSpace.Session.ExecuteSproc("sp_GetItem", new OperandValue(""), new OperandValue("Sales"));

            var nonPersistentOS = Application.CreateObjectSpace(typeof(ItemBrowser));
            ItemBrowser browser = nonPersistentOS.CreateObject<ItemBrowser>();
            int i = 1;

            if (sprocData.ResultSet.Count() > 0)
            {
                if (sprocData.ResultSet[0].Rows.Count() > 0)
                {
                    foreach (SelectStatementResultRow row in sprocData.ResultSet[0].Rows)
                    {
                        var itemos = Application.CreateObjectSpace(typeof(ItemCodes));
                        var item = itemos.CreateObject<ItemCodes>();
                        item.Id = i;
                        item.Class = row.Values[0].ToString();
                        item.ItemCode = row.Values[1].ToString();
                        item.ItemName = row.Values[2].ToString();
                        item.NewOrDemo = row.Values[3].ToString();
                        item.Price = (decimal)row.Values[4];
                        browser.itemcodes.Add(item);

                        i++;
                    }
                }
            }

            nonPersistentOS.CommitChanges();

            DetailView detailView = Application.CreateDetailView(nonPersistentOS, browser);
            detailView.ViewEditMode = DevExpress.ExpressApp.Editors.ViewEditMode.Edit;
            e.View = detailView;
            e.Maximized = true;

            e.DialogController.CancelAction.Active["NothingToCancel"] = false;
        }

        private void ChangeOutlet_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            OGW10ChgOutlet trx = (OGW10ChgOutlet)e.CurrentObject;
            ApplicationUser user = (ApplicationUser)SecuritySystem.CurrentUser;

            if (trx.Outlet == null)
            {
                throw new InvalidOperationException("No outlet selected.");
            }

            IObjectSpace os = Application.CreateObjectSpace();
            ApplicationUser upduser = os.FindObject<ApplicationUser>(new BinaryOperator("Oid", user.Oid));

            if (upduser != null)
            {
                upduser.Outlet = upduser.Session.GetObjectByKey<vwOutlets>(trx.Outlet.CardCode);

                os.CommitChanges();
                genCon.showMsg("Successful", "Outlet changed.", InformationType.Success);
            }
        }

        private void SubmitDoc_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            ApplicationUser user = (ApplicationUser)SecuritySystem.CurrentUser;

            /* OGW10ORDR */
            if (View.ObjectTypeInfo.Type == typeof(OGW10ORDR))
            {
                try
                {
                    OGW10ORDR ORDR = (OGW10ORDR)View.CurrentObject;

                    /* Validation Start */

                    if (ORDR.SalesRep1 == null || ORDR.SalesRep2 == null)
                        throw new InvalidOperationException("Unable to submit without sales rep.");

                    /* Validation End */

                    ORDR.Status = ObjectSpace.FindObject<vwStatus>(CriteriaOperator.Parse("Code = 'OPEN'"));

                    ORDR.SubmitBy = user.UserName;
                    ORDR.SubmitDate = DateTime.Now;
                    ObjectSpace.CommitChanges();
                    ObjectSpace.Refresh();

                    IObjectSpace os = Application.CreateObjectSpace();
                    OGW10ORDR trx = os.FindObject<OGW10ORDR>(new BinaryOperator("Oid", ORDR.Oid));
                    genCon.openNewView(os, trx, ViewEditMode.View);
                    genCon.showMsg("Successful", "Document Submitted.", InformationType.Success);

                    if (trx.EditAndCancel == true)
                    {
                        genCon.executeNonQuery("UPDATE T0 SET T0.[Status] = 'CANCEL' FROM OGW10ORDR T0 " +
                            "INNER JOIN " +
                            "( " +
                            "SELECT T0.BaseEntry From OGW11ORDR T0 " +
                            "WHERE T0.DocEntry = '" + trx.Oid + "' AND T0.BaseEntry <> 0 " +
                            "GROUP BY T0.BaseEntry " +
                            ") T1 on T0.OID = T1.BaseEntry " +
                            "");
                    }
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException(ex.Message);
                }
            }

            /* OGW10ORDN */
            if (View.ObjectTypeInfo.Type == typeof(OGW10ORDN))
            {
                try
                {
                    OGW10ORDN ORDN = (OGW10ORDN)View.CurrentObject;

                    /* Validation Start */

                    /* Validation End */

                    ORDN.Status = ObjectSpace.FindObject<vwStatus>(CriteriaOperator.Parse("Code = 'OPEN'"));

                    ORDN.SubmitBy = user.UserName;
                    ORDN.SubmitDate = DateTime.Now;
                    ObjectSpace.CommitChanges();
                    ObjectSpace.Refresh();

                    IObjectSpace os = Application.CreateObjectSpace();
                    OGW10ORDN trx = os.FindObject<OGW10ORDN>(new BinaryOperator("Oid", ORDN.Oid));
                    genCon.openNewView(os, trx, ViewEditMode.View);
                    genCon.showMsg("Successful", "Document Submitted.", InformationType.Success);
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException(ex.Message);
                }
            }

            /* OGW10EXCO */
            if (View.ObjectTypeInfo.Type == typeof(OGW10EXCO))
            {
                try
                {
                    OGW10EXCO EXCO = (OGW10EXCO)View.CurrentObject;

                    /* Validation Start */

                    /* Validation End */

                    EXCO.Status = ObjectSpace.FindObject<vwStatus>(CriteriaOperator.Parse("Code = 'OPEN'"));

                    EXCO.SubmitBy = user.UserName;
                    EXCO.SubmitDate = DateTime.Now;
                    ObjectSpace.CommitChanges();
                    ObjectSpace.Refresh();

                    IObjectSpace os = Application.CreateObjectSpace();
                    OGW10EXCO trx = os.FindObject<OGW10EXCO>(new BinaryOperator("Oid", EXCO.Oid));
                    genCon.openNewView(os, trx, ViewEditMode.View);
                    genCon.showMsg("Successful", "Document Submitted.", InformationType.Success);
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException(ex.Message);
                }
            }

            /* OGW10DREQ */
            if (View.ObjectTypeInfo.Type == typeof(OGW10DREQ))
            {
                try
                {
                    OGW10DREQ DREQ = (OGW10DREQ)View.CurrentObject;

                    /* Validation Start */

                    /* Validation End */

                    DREQ.Status = ObjectSpace.FindObject<vwStatus>(CriteriaOperator.Parse("Code = 'OPEN'"));

                    DREQ.SubmitBy = user.UserName;
                    DREQ.SubmitDate = DateTime.Now;
                    ObjectSpace.CommitChanges();
                    ObjectSpace.Refresh();

                    IObjectSpace os = Application.CreateObjectSpace();
                    OGW10DREQ trx = os.FindObject<OGW10DREQ>(new BinaryOperator("Oid", DREQ.Oid));
                    genCon.openNewView(os, trx, ViewEditMode.View);
                    genCon.showMsg("Successful", "Document Submitted.", InformationType.Success);
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException(ex.Message);
                }
            }
        }

        private void CancelDoc_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            ApplicationUser user = (ApplicationUser)SecuritySystem.CurrentUser;

            /* OGW10ORDR */
            if (View.ObjectTypeInfo.Type == typeof(OGW10ORDR))
            {
                try
                {
                    OGW10ORDR ORDR = (OGW10ORDR)View.CurrentObject;

                    /* Validation Start */

                    if (string.IsNullOrEmpty(ORDR.Reason))
                        throw new InvalidOperationException("Unable to cancel without reason.");

                    if (ORDR.CancelType == null)
                        throw new InvalidOperationException("Unable to cancel without cancel type.");

                    /* Validation End */

                    ORDR.Status = ObjectSpace.FindObject<vwStatus>(CriteriaOperator.Parse("Code = 'CANCEL'"));

                    ObjectSpace.CommitChanges();
                    ObjectSpace.Refresh();

                    IObjectSpace os = Application.CreateObjectSpace();
                    OGW10ORDR trx = os.FindObject<OGW10ORDR>(new BinaryOperator("Oid", ORDR.Oid));
                    genCon.openNewView(os, trx, ViewEditMode.View);
                    genCon.showMsg("Successful", "Document Cancelled.", InformationType.Success);
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException(ex.Message);
                }
            }

            /* OGW10ORDN */
            if (View.ObjectTypeInfo.Type == typeof(OGW10ORDN))
            {
                try
                {
                    OGW10ORDN ORDN = (OGW10ORDN)View.CurrentObject;

                    /* Validation Start */

                    if (string.IsNullOrEmpty(ORDN.Reason))
                        throw new InvalidOperationException("Unable to cancel without reason.");

                    if (ORDN.CancelType == null)
                        throw new InvalidOperationException("Unable to cancel without cancel type.");

                    /* Validation End */

                    ORDN.Status = ObjectSpace.FindObject<vwStatus>(CriteriaOperator.Parse("Code = 'CANCEL'"));

                    ObjectSpace.CommitChanges();
                    ObjectSpace.Refresh();

                    IObjectSpace os = Application.CreateObjectSpace();
                    OGW10ORDN trx = os.FindObject<OGW10ORDN>(new BinaryOperator("Oid", ORDN.Oid));
                    genCon.openNewView(os, trx, ViewEditMode.View);
                    genCon.showMsg("Successful", "Document Cancelled.", InformationType.Success);
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException(ex.Message);
                }
            }

            /* OGW10EXCO */
            if (View.ObjectTypeInfo.Type == typeof(OGW10EXCO))
            {
                try
                {
                    OGW10EXCO EXCO = (OGW10EXCO)View.CurrentObject;

                    /* Validation Start */

                    if (string.IsNullOrEmpty(EXCO.Reason))
                        throw new InvalidOperationException("Unable to cancel without reason.");

                    if (EXCO.CancelType == null)
                        throw new InvalidOperationException("Unable to cancel without cancel type.");

                    /* Validation End */

                    EXCO.Status = ObjectSpace.FindObject<vwStatus>(CriteriaOperator.Parse("Code = 'CANCEL'"));

                    ObjectSpace.CommitChanges();
                    ObjectSpace.Refresh();

                    IObjectSpace os = Application.CreateObjectSpace();
                    OGW10EXCO trx = os.FindObject<OGW10EXCO>(new BinaryOperator("Oid", EXCO.Oid));
                    genCon.openNewView(os, trx, ViewEditMode.View);
                    genCon.showMsg("Successful", "Document Cancelled.", InformationType.Success);
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException(ex.Message);
                }
            }

            /* OGW10DREQ */
            if (View.ObjectTypeInfo.Type == typeof(OGW10DREQ))
            {
                try
                {
                    OGW10DREQ DREQ = (OGW10DREQ)View.CurrentObject;

                    /* Validation Start */

                    //if (string.IsNullOrEmpty(DREQ.Reason))
                    //    throw new InvalidOperationException("Unable to cancel without reason.");

                    //if (DREQ.CancelType == null)
                    //    throw new InvalidOperationException("Unable to cancel without cancel type.");

                    /* Validation End */

                    DREQ.Status = ObjectSpace.FindObject<vwStatus>(CriteriaOperator.Parse("Code = 'CANCEL'"));

                    ObjectSpace.CommitChanges();
                    ObjectSpace.Refresh();

                    IObjectSpace os = Application.CreateObjectSpace();
                    OGW10DREQ trx = os.FindObject<OGW10DREQ>(new BinaryOperator("Oid", DREQ.Oid));
                    genCon.openNewView(os, trx, ViewEditMode.View);
                    genCon.showMsg("Successful", "Document Cancelled.", InformationType.Success);
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException(ex.Message);
                }
            }
        }

        private void CloseDoc_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            ApplicationUser user = (ApplicationUser)SecuritySystem.CurrentUser;

            /* OGW10ORDR */
            if (View.ObjectTypeInfo.Type == typeof(OGW10ORDR))
            {
                try
                {
                    OGW10ORDR ORDR = (OGW10ORDR)View.CurrentObject;

                    /* Validation Start */

                    if (string.IsNullOrEmpty(ORDR.Reason))
                        throw new InvalidOperationException("Unable to close without reason.");

                    if (ORDR.CloseType == null)
                        throw new InvalidOperationException("Unable to close without close type.");

                    /* Validation End */

                    ORDR.Status = ObjectSpace.FindObject<vwStatus>(CriteriaOperator.Parse("Code = 'CLOSED'"));

                    ObjectSpace.CommitChanges();
                    ObjectSpace.Refresh();

                    IObjectSpace os = Application.CreateObjectSpace();
                    OGW10ORDR trx = os.FindObject<OGW10ORDR>(new BinaryOperator("Oid", ORDR.Oid));
                    genCon.openNewView(os, trx, ViewEditMode.View);
                    genCon.showMsg("Successful", "Document Closed.", InformationType.Success);
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException(ex.Message);
                }
            }
        }

        private void ViewDoc_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {

        }

        private void ViewDoc_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            if (View.ObjectTypeInfo.Type == typeof(CopyList_OGW11ORDR))
            {
                CopyList_OGW11ORDR view = (CopyList_OGW11ORDR)View.CurrentObject;

                IObjectSpace os = Application.CreateObjectSpace();
                OGW10ORDR trx = os.FindObject<OGW10ORDR>(new BinaryOperator("DocNum", view.Header.DocNum));

                DetailView detailView = Application.CreateDetailView(os, "OGW10ORDR_DetailView_View", true, trx);
                detailView.ViewEditMode = DevExpress.ExpressApp.Editors.ViewEditMode.View;
                e.View = detailView;
                e.Maximized = true;
                e.DialogController.CancelAction.Active["NothingToCancel"] = false;
            }
        }
    }
}
