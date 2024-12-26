using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo.DB;
using OgawaPortal.Module.BusinessObjects;
using OgawaPortal.Module.BusinessObjects.Nonpersistent;
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
                if (View.Id == "OGW10ORDR_DetailView")
                {
                    if (((DetailView)View).ViewEditMode == ViewEditMode.Edit)
                    {
                        this.AddItem.Active.SetItemValue("Enabled", true);
                    }
                    else
                    {
                        this.AddItem.Active.SetItemValue("Enabled", false);
                    }
                }
            }
            else
            {
                this.AddItem.Active.SetItemValue("Enabled", false);
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
    }
}
