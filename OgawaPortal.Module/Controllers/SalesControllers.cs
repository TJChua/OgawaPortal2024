using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using OgawaPortal.Module.BusinessObjects;
using OgawaPortal.Module.BusinessObjects.Copy_Screen;
using OgawaPortal.Module.BusinessObjects.Maintenance;
using OgawaPortal.Module.BusinessObjects.Sales_Order;
using OgawaPortal.Module.BusinessObjects.View;
using System;
using System.Data;
using System.Data.SqlClient;

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
            this.CopyFrmSO.Active.SetItemValue("Enabled", false);
            this.ResumeDoc.Active.SetItemValue("Enabled", false);

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
                if (View.Id == "OGW10ORDR_DetailView_EditOrder")
                {
                    if (((DetailView)View).ViewEditMode == ViewEditMode.Edit)
                    {
                        this.CopyFrmSO.Active.SetItemValue("Enabled", true);
                    }
                    else
                    {
                        this.CopyFrmSO.Active.SetItemValue("Enabled", false);
                    }
                }

                if (View.Id == "OGW10ORDR_DetailView_RsmOrder")
                {
                    this.ResumeDoc.Active.SetItemValue("Enabled", true);
                }
                else
                {
                    this.ResumeDoc.Active.SetItemValue("Enabled", false);
                }
            } 
            /* OGW11ORDR */
            else if (View.ObjectTypeInfo.Type == typeof(OGW11ORDR))
            {
                if (View.Id == "OGW10ORDR_OGW11ORDR_ListView")
                {
                    if (View is ListView && !View.IsRoot)
                    {
                        if (View.ObjectSpace.Owner is DetailView)
                        {
                            this.DeleteORDRLine.Active.SetItemValue("Enabled", View.ObjectSpace.Owner is DetailView && ((DetailView)View.ObjectSpace.Owner).ViewEditMode == ViewEditMode.Edit);
                        }
                    }
                }
            }
            else
            {
                this.DeleteORDRLine.Active.SetItemValue("Enabled", false);
                this.CopyFrmSO.Active.SetItemValue("Enabled", false);
                this.ResumeDoc.Active.SetItemValue("Enabled", false);
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

        private void CopyFrmSO_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            try
            {
                int row = 0;
                OGW10ORDR ORDR = (OGW10ORDR)View.CurrentObject;

                foreach (CopyList_OGW11ORDR dtl in ((ListView)e.PopupWindow.View).SelectedObjects)
                {
                    if (dtl.Header.BillName != null)
                    {
                        ORDR.BillName = ORDR.Session.GetObjectByKey<Customer>(dtl.Header.BillName.Oid);
                    }
                    ORDR.BillAddress1 = dtl.Header.BillAddress1;
                    ORDR.BillAddress2 = dtl.Header.BillAddress2;
                    ORDR.BillCity = dtl.Header.BillCity;
                    ORDR.BillDistrict = dtl.Header.BillDistrict;
                    ORDR.BillPostCode = dtl.Header.BillPostCode;
                    ORDR.BillCountry = dtl.Header.BillCountry;
                    ORDR.BillMobilePhone = dtl.Header.BillMobilePhone;
                    ORDR.BillHomePhone = dtl.Header.BillHomePhone;
                    ORDR.BillEmail = dtl.Header.BillEmail;
                    ORDR.BillIdentityNo = dtl.Header.BillIdentityNo;
                    if (ORDR.BillRace != null)
                    {
                        ORDR.BillRace = ORDR.Session.GetObjectByKey<Races>(ORDR.BillRace.Oid);
                    }

                    if (ORDR.DeliveryContact != null)
                    {
                        ORDR.DeliveryContact = ORDR.Session.GetObjectByKey<Customer>(dtl.Header.DeliveryContact.Oid);
                    }
                    ORDR.DeliveryAddress1 = dtl.Header.DeliveryAddress1;
                    ORDR.DeliveryAddress2 = dtl.Header.DeliveryAddress2;
                    ORDR.DeliveryCity = dtl.Header.DeliveryCity;
                    ORDR.DeliveryDistrict = dtl.Header.DeliveryDistrict;
                    ORDR.DeliveryPostCode = dtl.Header.DeliveryPostCode;
                    ORDR.DeliveryCountry = dtl.Header.DeliveryCountry;
                    ORDR.DeliveryMobilePhone = dtl.Header.DeliveryMobilePhone;
                    ORDR.DeliveryHomePhone = dtl.Header.DeliveryHomePhone;
                    if (dtl.Header.DeliveryRace != null)
                    {
                        ORDR.DeliveryRace = ORDR.Session.GetObjectByKey<Races>(dtl.Header.DeliveryRace.Oid);
                    }

                    ORDR.SubTotal = dtl.Header.SubTotal;
                    ORDR.OrderDiscount = dtl.Header.OrderDiscount;
                    ORDR.Tax = dtl.Header.Tax;
                    ORDR.TotalDue = dtl.Header.TotalDue;
                    ORDR.SettlementDiscount = dtl.Header.SettlementDiscount;
                    ORDR.NetTotalDue = dtl.Header.NetTotalDue;
                    ORDR.Cash = dtl.Header.Cash;
                    ORDR.CreditCard = dtl.Header.CreditCard;
                    ORDR.Voucher = dtl.Header.Voucher;
                    ORDR.CreditNote = dtl.Header.CreditNote;
                    ORDR.PreviousPayment = dtl.Header.PreviousPayment;
                    ORDR.OrderBalanceDue = dtl.Header.OrderBalanceDue;
                    ORDR.MinimumDue = dtl.Header.MinimumDue;
                    ORDR.MinDueBalance = dtl.Header.MinDueBalance;

                    foreach (OGW11ORDR item in dtl.Header.OGW11ORDR)
                    {
                        OGW11ORDR ORDR11 = new OGW11ORDR(ORDR.Session);

                        ORDR11.Class = item.Class;
                        ORDR11.ItemCode = ORDR11.Session.GetObjectByKey<vwItemMasters>(item.ItemCode.ItemCode);
                        ORDR11.ItemName = item.ItemName;
                        ORDR11.UnitPrice = item.UnitPrice;
                        ORDR11.Order = item.UnitPrice;
                        ORDR11.Taken = item.Taken;
                        ORDR11.BackOrder = item.BackOrder;
                        ORDR11.BaseEntry = item.DocEntry.Oid;
                        ORDR11.BaseOid = item.Oid;

                        ORDR.OGW11ORDR.Add(ORDR11);
                    }

                    foreach (OGW12ORDR payment in dtl.Header.OGW12ORDR)
                    {
                        OGW12ORDR ORDR12 = new OGW12ORDR(ORDR.Session);

                        ORDR12.PaymentMethod = payment.PaymentMethod;
                        ORDR12.CashAcctCode = payment.CashAcctCode;
                        if (payment.Consignment != null)
                        {
                            ORDR12.Consignment = ORDR12.Session.GetObjectByKey<Consignment>(payment.Consignment.Oid);
                        }
                        ORDR12.CashAmount = payment.CashAmount;
                        ORDR12.CashRefNum = payment.CashRefNum;
                        ORDR12.CreditCardAcctCode = payment.CreditCardAcctCode;
                        if (payment.CardType != null)
                        {
                            ORDR12.CardType = ORDR12.Session.GetObjectByKey<CardType>(payment.CardType.Oid);
                        }
                        ORDR12.CreditCardNo = payment.CreditCardNo;
                        ORDR12.CardHolderName = payment.CardHolderName;
                        if (payment.Instalment != null)
                        {
                            ORDR12.Instalment = ORDR12.Session.GetObjectByKey<Instalment>(payment.Instalment.Oid);
                        }
                        ORDR12.TerminalID = payment.TerminalID;
                        if (payment.CardIssuer != null)
                        {
                            ORDR12.CardIssuer = ORDR12.Session.GetObjectByKey<CardIssuer>(payment.CardIssuer.Oid);
                        }
                        if (payment.Merchant != null)
                        {
                            ORDR12.Merchant = ORDR12.Session.GetObjectByKey<CardMachineBank>(payment.Merchant.Oid);
                        }
                        ORDR12.ApprovalCode = payment.ApprovalCode;
                        ORDR12.BatchNo = payment.BatchNo;
                        ORDR12.Transaction = payment.Transaction;
                        ORDR12.CreditCardAmount = payment.CreditCardAmount;
                        ORDR12.VoucherAcctCode = payment.VoucherAcctCode;
                        if (payment.VoucherType != null)
                        {
                            ORDR12.VoucherType = ORDR12.Session.GetObjectByKey<Voucher>(payment.VoucherType.Oid);
                        }
                        ORDR12.VoucherNo = payment.VoucherNo;
                        if (payment.TaxCode != null)
                        {
                            ORDR12.TaxCode = ORDR12.Session.GetObjectByKey<vwTax>(payment.TaxCode.Code);
                        }
                        ORDR12.VoucherAmount = payment.VoucherAmount;
                        ORDR12.PaymentTotal = payment.PaymentTotal;
                        ORDR12.BaseEntry = payment.DocEntry.Oid;
                        ORDR12.BaseOid = payment.Oid;

                        ORDR.OGW12ORDR.Add(ORDR12);
                    }
                }

                ObjectSpace.CommitChanges();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        private void CopyFrmSO_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            int Customer = 0;
            var ObjType = "";

            OGW10ORDR ORDR = (OGW10ORDR)View.CurrentObject;

            Customer = ORDR.Name != null ? ORDR.Name.Oid : 0;
            ObjType = "EditOrder";

            IObjectSpace os = Application.CreateObjectSpace(typeof(CopyList_OGW11ORDR));
            string listViewId = Application.FindLookupListViewId(typeof(CopyList_OGW11ORDR));
            CollectionSourceBase collectionSource = Application.CreateCollectionSource(os, typeof(CopyList_OGW11ORDR), listViewId);

            using (SqlConnection conn = new SqlConnection(genCon.getConnectionString()))
            {
                using (SqlDataAdapter da = new SqlDataAdapter("", conn))
                {
                    da.SelectCommand.CommandText = "EXEC FTS_sp_CopyFrom 'OGW10ORDR', '" + ObjType + "', '" + Customer + "' ";
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dtrow in dt.Rows)
                        {
                            CopyList_OGW11ORDR SO = os.CreateObject<CopyList_OGW11ORDR>();

                            SO.Oid = int.Parse(dtrow["Oid"].ToString());
                            SO.Header = ObjectSpace.GetObjectByKey<OGW10ORDR>(dtrow["Header"]);
                            //SO.Details = ObjectSpace.GetObjectByKey<OGW11ORDR>(dtrow["Details"]);
                            collectionSource.List.Add(SO);
                        }
                    }
                }
            }
            
            e.View = Application.CreateListView(listViewId, collectionSource, true);
        }

        private void ResumeDoc_Execute(object sender, SimpleActionExecuteEventArgs e)
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

                    ORDR.Status = ObjectSpace.FindObject<vwStatus>(CriteriaOperator.Parse("Code = 'REOPEN'"));
                    ORDR.ResumeOrder = true;

                    ORDR.SubmitBy = user.UserName;
                    ORDR.SubmitDate = DateTime.Now;
                    ObjectSpace.CommitChanges();
                    ObjectSpace.Refresh();

                    IObjectSpace os = Application.CreateObjectSpace();
                    OGW10ORDR trx = os.FindObject<OGW10ORDR>(new BinaryOperator("Oid", ORDR.Oid));
                    genCon.openNewView(os, trx, ViewEditMode.Edit);
                    genCon.showMsg("Successful", "Document Resume.", InformationType.Success);
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException(ex.Message);
                }
            }
        }
    }
}
