using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using OgawaPortal.Module.BusinessObjects;
using OgawaPortal.Module.BusinessObjects.Copy_Screen;
using OgawaPortal.Module.BusinessObjects.Maintenance;
using OgawaPortal.Module.BusinessObjects.POS___Exchange;
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
            /* OGW10ORDN */
            else if (View.ObjectTypeInfo.Type == typeof(OGW10ORDN))
            {
                if (View.Id == "OGW10ORDN_DetailView")
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
            if (View.ObjectTypeInfo.Type == typeof(OGW10ORDR))
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

            if (View.ObjectTypeInfo.Type == typeof(OGW10ORDN))
            {
                try
                {
                    int row = 0;
                    OGW10ORDN ORDN = (OGW10ORDN)View.CurrentObject;

                    foreach (CopyList_OGW11ORDR dtl in ((ListView)e.PopupWindow.View).SelectedObjects)
                    {
                        if (dtl.Header.BillName != null)
                        {
                            ORDN.BillName = ORDN.Session.GetObjectByKey<Customer>(dtl.Header.BillName.Oid);
                        }
                        ORDN.BillAddress1 = dtl.Header.BillAddress1;
                        ORDN.BillAddress2 = dtl.Header.BillAddress2;
                        ORDN.BillCity = dtl.Header.BillCity;
                        ORDN.BillDistrict = dtl.Header.BillDistrict;
                        ORDN.BillPostCode = dtl.Header.BillPostCode;
                        ORDN.BillCountry = dtl.Header.BillCountry;
                        ORDN.BillMobilePhone = dtl.Header.BillMobilePhone;
                        ORDN.BillHomePhone = dtl.Header.BillHomePhone;
                        ORDN.BillEmail = dtl.Header.BillEmail;
                        ORDN.BillIdentityNo = dtl.Header.BillIdentityNo;
                        if (ORDN.BillRace != null)
                        {
                            ORDN.BillRace = ORDN.Session.GetObjectByKey<Races>(ORDN.BillRace.Oid);
                        }

                        if (ORDN.DeliveryContact != null)
                        {
                            ORDN.DeliveryContact = ORDN.Session.GetObjectByKey<Customer>(dtl.Header.DeliveryContact.Oid);
                        }
                        ORDN.DeliveryAddress1 = dtl.Header.DeliveryAddress1;
                        ORDN.DeliveryAddress2 = dtl.Header.DeliveryAddress2;
                        ORDN.DeliveryCity = dtl.Header.DeliveryCity;
                        ORDN.DeliveryDistrict = dtl.Header.DeliveryDistrict;
                        ORDN.DeliveryPostCode = dtl.Header.DeliveryPostCode;
                        ORDN.DeliveryCountry = dtl.Header.DeliveryCountry;
                        ORDN.DeliveryMobilePhone = dtl.Header.DeliveryMobilePhone;
                        ORDN.DeliveryHomePhone = dtl.Header.DeliveryHomePhone;
                        if (dtl.Header.DeliveryRace != null)
                        {
                            ORDN.DeliveryRace = ORDN.Session.GetObjectByKey<Races>(dtl.Header.DeliveryRace.Oid);
                        }

                        ORDN.SubTotal = dtl.Header.SubTotal;
                        ORDN.OrderDiscount = dtl.Header.OrderDiscount;
                        ORDN.Tax = dtl.Header.Tax;
                        ORDN.TotalDue = dtl.Header.TotalDue;
                        ORDN.SettlementDiscount = dtl.Header.SettlementDiscount;
                        ORDN.NetTotalDue = dtl.Header.NetTotalDue;
                        ORDN.Cash = dtl.Header.Cash;
                        ORDN.CreditCard = dtl.Header.CreditCard;
                        ORDN.Voucher = dtl.Header.Voucher;
                        ORDN.CreditNote = dtl.Header.CreditNote;
                        ORDN.PreviousPayment = dtl.Header.PreviousPayment;
                        ORDN.OrderBalanceDue = dtl.Header.OrderBalanceDue;
                        ORDN.MinimumDue = dtl.Header.MinimumDue;
                        ORDN.MinDueBalance = dtl.Header.MinDueBalance;

                        foreach (OGW11ORDR item in dtl.Header.OGW11ORDR)
                        {
                            OGW11ORDN ORDN11 = new OGW11ORDN(ORDN.Session);

                            ORDN11.Class = item.Class;
                            ORDN11.ItemCode = ORDN11.Session.GetObjectByKey<vwItemMasters>(item.ItemCode.ItemCode);
                            ORDN11.ItemName = item.ItemName;
                            ORDN11.UnitPrice = item.UnitPrice;
                            ORDN11.Order = item.UnitPrice;
                            ORDN11.Taken = item.Taken;
                            ORDN11.BackOrder = item.BackOrder;
                            ORDN11.BaseEntry = item.DocEntry.Oid;
                            ORDN11.BaseOid = item.Oid;

                            ORDN.OGW11ORDN.Add(ORDN11);
                        }

                        foreach (OGW12ORDR payment in dtl.Header.OGW12ORDR)
                        {
                            OGW12ORDN ORDN12 = new OGW12ORDN(ORDN.Session);

                            ORDN12.PaymentMethod = payment.PaymentMethod;
                            ORDN12.CashAcctCode = payment.CashAcctCode;
                            if (payment.Consignment != null)
                            {
                                ORDN12.Consignment = ORDN12.Session.GetObjectByKey<Consignment>(payment.Consignment.Oid);
                            }
                            ORDN12.CashAmount = payment.CashAmount;
                            ORDN12.CashRefNum = payment.CashRefNum;
                            ORDN12.CreditCardAcctCode = payment.CreditCardAcctCode;
                            if (payment.CardType != null)
                            {
                                ORDN12.CardType = ORDN12.Session.GetObjectByKey<CardType>(payment.CardType.Oid);
                            }
                            ORDN12.CreditCardNo = payment.CreditCardNo;
                            ORDN12.CardHolderName = payment.CardHolderName;
                            if (payment.Instalment != null)
                            {
                                ORDN12.Instalment = ORDN12.Session.GetObjectByKey<Instalment>(payment.Instalment.Oid);
                            }
                            ORDN12.TerminalID = payment.TerminalID;
                            if (payment.CardIssuer != null)
                            {
                                ORDN12.CardIssuer = ORDN12.Session.GetObjectByKey<CardIssuer>(payment.CardIssuer.Oid);
                            }
                            if (payment.Merchant != null)
                            {
                                ORDN12.Merchant = ORDN12.Session.GetObjectByKey<CardMachineBank>(payment.Merchant.Oid);
                            }
                            ORDN12.ApprovalCode = payment.ApprovalCode;
                            ORDN12.BatchNo = payment.BatchNo;
                            ORDN12.Transaction = payment.Transaction;
                            ORDN12.CreditCardAmount = payment.CreditCardAmount;
                            ORDN12.VoucherAcctCode = payment.VoucherAcctCode;
                            if (payment.VoucherType != null)
                            {
                                ORDN12.VoucherType = ORDN12.Session.GetObjectByKey<Voucher>(payment.VoucherType.Oid);
                            }
                            ORDN12.VoucherNo = payment.VoucherNo;
                            if (payment.TaxCode != null)
                            {
                                ORDN12.TaxCode = ORDN12.Session.GetObjectByKey<vwTax>(payment.TaxCode.Code);
                            }
                            ORDN12.VoucherAmount = payment.VoucherAmount;
                            ORDN12.PaymentTotal = payment.PaymentTotal;
                            ORDN12.BaseEntry = payment.DocEntry.Oid;
                            ORDN12.BaseOid = payment.Oid;

                            ORDN.OGW12ORDN.Add(ORDN12);
                        }
                    }

                    ObjectSpace.CommitChanges();
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException(ex.Message);
                }
            }
        }

        private void CopyFrmSO_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            var Customer = "";
            var ObjType = "";

            if (View.ObjectTypeInfo.Type == typeof(OGW10ORDR))
            {
                OGW10ORDR ORDR = (OGW10ORDR)View.CurrentObject;

                Customer = ORDR.Name != null ? ORDR.Name.Oid.ToString() : "";
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

            if (View.ObjectTypeInfo.Type == typeof(OGW10ORDN))
            {
                OGW10ORDN ORDN = (OGW10ORDN)View.CurrentObject;

                Customer = ORDN.Name != null ? ORDN.Name.Oid.ToString() : "";

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
