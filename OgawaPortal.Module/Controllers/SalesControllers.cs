using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using OgawaPortal.Module.BusinessObjects;
using OgawaPortal.Module.BusinessObjects.Copy_Screen;
using OgawaPortal.Module.BusinessObjects.Logistic;
using OgawaPortal.Module.BusinessObjects.Maintenance;
using OgawaPortal.Module.BusinessObjects.POS___Exchange;
using OgawaPortal.Module.BusinessObjects.POS___Logistic;
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
            this.CopyFrmDR.Active.SetItemValue("Enabled", false);
            this.CopyFrmDREX.Active.SetItemValue("Enabled", false);
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
            /* OGW10EXCO */
            else if (View.ObjectTypeInfo.Type == typeof(OGW10EXCO))
            {
                if (View.Id == "OGW10EXCO_DetailView")
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
            /* OGW10DREQ */
            else if (View.ObjectTypeInfo.Type == typeof(OGW10DREQ))
            {
                if (View.Id == "OGW10DREQ_DetailView")
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
            /* OGW10DREX */
            else if (View.ObjectTypeInfo.Type == typeof(OGW10DREX))
            {
                if (View.Id == "OGW10DREX_DetailView")
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
            /* OGW10OPKL */
            else if (View.ObjectTypeInfo.Type == typeof(OGW10OPKL))
            {
                if (View.Id == "OGW10OPKL_DetailView")
                {
                    if (((DetailView)View).ViewEditMode == ViewEditMode.Edit)
                    {
                        this.CopyFrmDR.Active.SetItemValue("Enabled", true);
                    }
                    else
                    {
                        this.CopyFrmDR.Active.SetItemValue("Enabled", false);
                    }
                }
            }
            /* OGW10PLEX */
            else if (View.ObjectTypeInfo.Type == typeof(OGW10PLEX))
            {
                if (View.Id == "OGW10PLEX_DetailView")
                {
                    if (((DetailView)View).ViewEditMode == ViewEditMode.Edit)
                    {
                        this.CopyFrmDREX.Active.SetItemValue("Enabled", true);
                    }
                    else
                    {
                        this.CopyFrmDREX.Active.SetItemValue("Enabled", false);
                    }
                }
            }
            else
            {
                this.DeleteORDRLine.Active.SetItemValue("Enabled", false);
                this.CopyFrmSO.Active.SetItemValue("Enabled", false);
                this.CopyFrmDR.Active.SetItemValue("Enabled", false);
                this.CopyFrmDREX.Active.SetItemValue("Enabled", false);
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

            if (View.ObjectTypeInfo.Type == typeof(OGW10EXCO))
            {
                try
                {
                    int row = 0;
                    OGW10EXCO EXCO = (OGW10EXCO)View.CurrentObject;

                    foreach (CopyList_OGW11ORDR dtl in ((ListView)e.PopupWindow.View).SelectedObjects)
                    {
                        if (dtl.Header.BillName != null)
                        {
                            EXCO.BillName = EXCO.Session.GetObjectByKey<Customer>(dtl.Header.BillName.Oid);
                        }
                        EXCO.BillAddress1 = dtl.Header.BillAddress1;
                        EXCO.BillAddress2 = dtl.Header.BillAddress2;
                        EXCO.BillCity = dtl.Header.BillCity;
                        EXCO.BillDistrict = dtl.Header.BillDistrict;
                        EXCO.BillPostCode = dtl.Header.BillPostCode;
                        EXCO.BillCountry = dtl.Header.BillCountry;
                        EXCO.BillMobilePhone = dtl.Header.BillMobilePhone;
                        EXCO.BillHomePhone = dtl.Header.BillHomePhone;
                        EXCO.BillEmail = dtl.Header.BillEmail;
                        EXCO.BillIdentityNo = dtl.Header.BillIdentityNo;
                        if (EXCO.BillRace != null)
                        {
                            EXCO.BillRace = EXCO.Session.GetObjectByKey<Races>(EXCO.BillRace.Oid);
                        }

                        if (EXCO.DeliveryContact != null)
                        {
                            EXCO.DeliveryContact = EXCO.Session.GetObjectByKey<Customer>(dtl.Header.DeliveryContact.Oid);
                        }
                        EXCO.DeliveryAddress1 = dtl.Header.DeliveryAddress1;
                        EXCO.DeliveryAddress2 = dtl.Header.DeliveryAddress2;
                        EXCO.DeliveryCity = dtl.Header.DeliveryCity;
                        EXCO.DeliveryDistrict = dtl.Header.DeliveryDistrict;
                        EXCO.DeliveryPostCode = dtl.Header.DeliveryPostCode;
                        EXCO.DeliveryCountry = dtl.Header.DeliveryCountry;
                        EXCO.DeliveryMobilePhone = dtl.Header.DeliveryMobilePhone;
                        EXCO.DeliveryHomePhone = dtl.Header.DeliveryHomePhone;
                        if (dtl.Header.DeliveryRace != null)
                        {
                            EXCO.DeliveryRace = EXCO.Session.GetObjectByKey<Races>(dtl.Header.DeliveryRace.Oid);
                        }

                        foreach (OGW11ORDR item in dtl.Header.OGW11ORDR)
                        {
                            OGW11EXCO EXCO11 = new OGW11EXCO(EXCO.Session);

                            EXCO11.Class = item.Class;
                            EXCO11.ItemCode = EXCO11.Session.GetObjectByKey<vwItemMasters>(item.ItemCode.ItemCode);
                            EXCO11.ItemName = item.ItemName;
                            EXCO11.UnitPrice = item.UnitPrice;
                            EXCO11.Order = item.UnitPrice;
                            EXCO11.Taken = item.Taken;
                            EXCO11.BackOrder = item.BackOrder;
                            EXCO11.BaseEntry = item.DocEntry.Oid;
                            EXCO11.BaseOid = item.Oid;

                            EXCO.OGW11EXCO.Add(EXCO11);
                        }

                        foreach (OGW12ORDR payment in dtl.Header.OGW12ORDR)
                        {
                            OGW12EXCO EXCO12 = new OGW12EXCO(EXCO.Session);

                            EXCO12.PaymentMethod = payment.PaymentMethod;
                            EXCO12.CashAcctCode = payment.CashAcctCode;
                            if (payment.Consignment != null)
                            {
                                EXCO12.Consignment = EXCO12.Session.GetObjectByKey<Consignment>(payment.Consignment.Oid);
                            }
                            EXCO12.CashAmount = payment.CashAmount;
                            EXCO12.CashRefNum = payment.CashRefNum;
                            EXCO12.CreditCardAcctCode = payment.CreditCardAcctCode;
                            if (payment.CardType != null)
                            {
                                EXCO12.CardType = EXCO12.Session.GetObjectByKey<CardType>(payment.CardType.Oid);
                            }
                            EXCO12.CreditCardNo = payment.CreditCardNo;
                            EXCO12.CardHolderName = payment.CardHolderName;
                            if (payment.Instalment != null)
                            {
                                EXCO12.Instalment = EXCO12.Session.GetObjectByKey<Instalment>(payment.Instalment.Oid);
                            }
                            EXCO12.TerminalID = payment.TerminalID;
                            if (payment.CardIssuer != null)
                            {
                                EXCO12.CardIssuer = EXCO12.Session.GetObjectByKey<CardIssuer>(payment.CardIssuer.Oid);
                            }
                            if (payment.Merchant != null)
                            {
                                EXCO12.Merchant = EXCO12.Session.GetObjectByKey<CardMachineBank>(payment.Merchant.Oid);
                            }
                            EXCO12.ApprovalCode = payment.ApprovalCode;
                            EXCO12.BatchNo = payment.BatchNo;
                            EXCO12.Transaction = payment.Transaction;
                            EXCO12.CreditCardAmount = payment.CreditCardAmount;
                            EXCO12.VoucherAcctCode = payment.VoucherAcctCode;
                            if (payment.VoucherType != null)
                            {
                                EXCO12.VoucherType = EXCO12.Session.GetObjectByKey<Voucher>(payment.VoucherType.Oid);
                            }
                            EXCO12.VoucherNo = payment.VoucherNo;
                            if (payment.TaxCode != null)
                            {
                                EXCO12.TaxCode = EXCO12.Session.GetObjectByKey<vwTax>(payment.TaxCode.Code);
                            }
                            EXCO12.VoucherAmount = payment.VoucherAmount;
                            EXCO12.PaymentTotal = payment.PaymentTotal;
                            EXCO12.BaseEntry = payment.DocEntry.Oid;
                            EXCO12.BaseOid = payment.Oid;

                            EXCO.OGW12EXCO.Add(EXCO12);
                        }
                    }

                    ObjectSpace.CommitChanges();
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException(ex.Message);
                }
            }

            if (View.ObjectTypeInfo.Type == typeof(OGW10DREQ))
            {
                try
                {
                    int row = 0;
                    OGW10DREQ DREQ = (OGW10DREQ)View.CurrentObject;

                    foreach (CopyList_OGW11ORDR dtl in ((ListView)e.PopupWindow.View).SelectedObjects)
                    {
                        if (dtl.Header.BillName != null)
                        {
                            DREQ.BillName = DREQ.Session.GetObjectByKey<Customer>(dtl.Header.BillName.Oid);
                        }
                        DREQ.BillAddress1 = dtl.Header.BillAddress1;
                        DREQ.BillAddress2 = dtl.Header.BillAddress2;
                        DREQ.BillCity = dtl.Header.BillCity;
                        DREQ.BillDistrict = dtl.Header.BillDistrict;
                        DREQ.BillPostCode = dtl.Header.BillPostCode;
                        DREQ.BillCountry = dtl.Header.BillCountry;
                        DREQ.BillMobilePhone = dtl.Header.BillMobilePhone;
                        DREQ.BillHomePhone = dtl.Header.BillHomePhone;
                        DREQ.BillEmail = dtl.Header.BillEmail;
                        DREQ.BillIdentityNo = dtl.Header.BillIdentityNo;
                        if (DREQ.BillRace != null)
                        {
                            DREQ.BillRace = DREQ.Session.GetObjectByKey<Races>(DREQ.BillRace.Oid);
                        }

                        if (DREQ.DeliveryContact != null)
                        {
                            DREQ.DeliveryContact = DREQ.Session.GetObjectByKey<Customer>(dtl.Header.DeliveryContact.Oid);
                        }
                        DREQ.DeliveryAddress1 = dtl.Header.DeliveryAddress1;
                        DREQ.DeliveryAddress2 = dtl.Header.DeliveryAddress2;
                        DREQ.DeliveryCity = dtl.Header.DeliveryCity;
                        DREQ.DeliveryDistrict = dtl.Header.DeliveryDistrict;
                        DREQ.DeliveryPostCode = dtl.Header.DeliveryPostCode;
                        DREQ.DeliveryCountry = dtl.Header.DeliveryCountry;
                        DREQ.DeliveryMobilePhone = dtl.Header.DeliveryMobilePhone;
                        DREQ.DeliveryHomePhone = dtl.Header.DeliveryHomePhone;
                        if (dtl.Header.DeliveryRace != null)
                        {
                            DREQ.DeliveryRace = DREQ.Session.GetObjectByKey<Races>(dtl.Header.DeliveryRace.Oid);
                        }

                        DREQ.SubTotal = dtl.Header.SubTotal;
                        DREQ.OrderDiscount = dtl.Header.OrderDiscount;
                        DREQ.Tax = dtl.Header.Tax;
                        DREQ.TotalDue = dtl.Header.TotalDue;
                        DREQ.SettlementDiscount = dtl.Header.SettlementDiscount;
                        DREQ.NetTotalDue = dtl.Header.NetTotalDue;
                        DREQ.Cash = dtl.Header.Cash;
                        DREQ.CreditCard = dtl.Header.CreditCard;
                        DREQ.Voucher = dtl.Header.Voucher;
                        DREQ.CreditNote = dtl.Header.CreditNote;
                        DREQ.PreviousPayment = dtl.Header.PreviousPayment;
                        DREQ.OrderBalanceDue = dtl.Header.OrderBalanceDue;
                        DREQ.MinimumDue = dtl.Header.MinimumDue;
                        DREQ.MinDueBalance = dtl.Header.MinDueBalance;

                        foreach (OGW11ORDR item in dtl.Header.OGW11ORDR)
                        {
                            OGW11DREQ DREQ11 = new OGW11DREQ(DREQ.Session);

                            DREQ11.Class = item.Class;
                            DREQ11.ItemCode = DREQ11.Session.GetObjectByKey<vwItemMasters>(item.ItemCode.ItemCode);
                            DREQ11.ItemName = item.ItemName;
                            DREQ11.UnitPrice = item.UnitPrice;
                            DREQ11.Order = item.UnitPrice;
                            DREQ11.Taken = item.Taken;
                            DREQ11.BackOrder = item.BackOrder;
                            DREQ11.BaseEntry = item.DocEntry.Oid;
                            DREQ11.BaseOid = item.Oid;

                            DREQ.OGW11DREQ.Add(DREQ11);
                        }

                        foreach (OGW12ORDR payment in dtl.Header.OGW12ORDR)
                        {
                            OGW12DREQ DREQ12 = new OGW12DREQ(DREQ.Session);

                            DREQ12.PaymentMethod = payment.PaymentMethod;
                            DREQ12.CashAcctCode = payment.CashAcctCode;
                            if (payment.Consignment != null)
                            {
                                DREQ12.Consignment = DREQ12.Session.GetObjectByKey<Consignment>(payment.Consignment.Oid);
                            }
                            DREQ12.CashAmount = payment.CashAmount;
                            DREQ12.CashRefNum = payment.CashRefNum;
                            DREQ12.CreditCardAcctCode = payment.CreditCardAcctCode;
                            if (payment.CardType != null)
                            {
                                DREQ12.CardType = DREQ12.Session.GetObjectByKey<CardType>(payment.CardType.Oid);
                            }
                            DREQ12.CreditCardNo = payment.CreditCardNo;
                            DREQ12.CardHolderName = payment.CardHolderName;
                            if (payment.Instalment != null)
                            {
                                DREQ12.Instalment = DREQ12.Session.GetObjectByKey<Instalment>(payment.Instalment.Oid);
                            }
                            DREQ12.TerminalID = payment.TerminalID;
                            if (payment.CardIssuer != null)
                            {
                                DREQ12.CardIssuer = DREQ12.Session.GetObjectByKey<CardIssuer>(payment.CardIssuer.Oid);
                            }
                            if (payment.Merchant != null)
                            {
                                DREQ12.Merchant = DREQ12.Session.GetObjectByKey<CardMachineBank>(payment.Merchant.Oid);
                            }
                            DREQ12.ApprovalCode = payment.ApprovalCode;
                            DREQ12.BatchNo = payment.BatchNo;
                            DREQ12.Transaction = payment.Transaction;
                            DREQ12.CreditCardAmount = payment.CreditCardAmount;
                            DREQ12.VoucherAcctCode = payment.VoucherAcctCode;
                            if (payment.VoucherType != null)
                            {
                                DREQ12.VoucherType = DREQ12.Session.GetObjectByKey<Voucher>(payment.VoucherType.Oid);
                            }
                            DREQ12.VoucherNo = payment.VoucherNo;
                            if (payment.TaxCode != null)
                            {
                                DREQ12.TaxCode = DREQ12.Session.GetObjectByKey<vwTax>(payment.TaxCode.Code);
                            }
                            DREQ12.VoucherAmount = payment.VoucherAmount;
                            DREQ12.PaymentTotal = payment.PaymentTotal;
                            DREQ12.BaseEntry = payment.DocEntry.Oid;
                            DREQ12.BaseOid = payment.Oid;

                            DREQ.OGW12DREQ.Add(DREQ12);
                        }
                    }

                    ObjectSpace.CommitChanges();
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException(ex.Message);
                }
            }

            if (View.ObjectTypeInfo.Type == typeof(OGW10DREX))
            {
                try
                {
                    int row = 0;
                    OGW10DREX DREX = (OGW10DREX)View.CurrentObject;

                    foreach (CopyList_OGW11ORDR dtl in ((ListView)e.PopupWindow.View).SelectedObjects)
                    {
                        if (dtl.Header.BillName != null)
                        {
                            DREX.BillName = DREX.Session.GetObjectByKey<Customer>(dtl.Header.BillName.Oid);
                        }
                        DREX.BillAddress1 = dtl.Header.BillAddress1;
                        DREX.BillAddress2 = dtl.Header.BillAddress2;
                        DREX.BillCity = dtl.Header.BillCity;
                        DREX.BillDistrict = dtl.Header.BillDistrict;
                        DREX.BillPostCode = dtl.Header.BillPostCode;
                        DREX.BillCountry = dtl.Header.BillCountry;
                        DREX.BillMobilePhone = dtl.Header.BillMobilePhone;
                        DREX.BillHomePhone = dtl.Header.BillHomePhone;
                        DREX.BillEmail = dtl.Header.BillEmail;
                        DREX.BillIdentityNo = dtl.Header.BillIdentityNo;
                        if (DREX.BillRace != null)
                        {
                            DREX.BillRace = DREX.Session.GetObjectByKey<Races>(DREX.BillRace.Oid);
                        }

                        if (DREX.DeliveryContact != null)
                        {
                            DREX.DeliveryContact = DREX.Session.GetObjectByKey<Customer>(dtl.Header.DeliveryContact.Oid);
                        }
                        DREX.DeliveryAddress1 = dtl.Header.DeliveryAddress1;
                        DREX.DeliveryAddress2 = dtl.Header.DeliveryAddress2;
                        DREX.DeliveryCity = dtl.Header.DeliveryCity;
                        DREX.DeliveryDistrict = dtl.Header.DeliveryDistrict;
                        DREX.DeliveryPostCode = dtl.Header.DeliveryPostCode;
                        DREX.DeliveryCountry = dtl.Header.DeliveryCountry;
                        DREX.DeliveryMobilePhone = dtl.Header.DeliveryMobilePhone;
                        DREX.DeliveryHomePhone = dtl.Header.DeliveryHomePhone;
                        if (dtl.Header.DeliveryRace != null)
                        {
                            DREX.DeliveryRace = DREX.Session.GetObjectByKey<Races>(dtl.Header.DeliveryRace.Oid);
                        }

                        DREX.SubTotal = dtl.Header.SubTotal;
                        DREX.OrderDiscount = dtl.Header.OrderDiscount;
                        DREX.Tax = dtl.Header.Tax;
                        DREX.TotalDue = dtl.Header.TotalDue;
                        DREX.SettlementDiscount = dtl.Header.SettlementDiscount;
                        DREX.NetTotalDue = dtl.Header.NetTotalDue;
                        DREX.Cash = dtl.Header.Cash;
                        DREX.CreditCard = dtl.Header.CreditCard;
                        DREX.Voucher = dtl.Header.Voucher;
                        DREX.CreditNote = dtl.Header.CreditNote;
                        DREX.PreviousPayment = dtl.Header.PreviousPayment;
                        DREX.OrderBalanceDue = dtl.Header.OrderBalanceDue;
                        DREX.MinimumDue = dtl.Header.MinimumDue;
                        DREX.MinDueBalance = dtl.Header.MinDueBalance;

                        foreach (OGW11ORDR item in dtl.Header.OGW11ORDR)
                        {
                            OGW11DREX DREX11 = new OGW11DREX(DREX.Session);

                            DREX11.Class = item.Class;
                            DREX11.ItemCode = DREX11.Session.GetObjectByKey<vwItemMasters>(item.ItemCode.ItemCode);
                            DREX11.ItemName = item.ItemName;
                            DREX11.UnitPrice = item.UnitPrice;
                            DREX11.Order = item.UnitPrice;
                            DREX11.Taken = item.Taken;
                            DREX11.BackOrder = item.BackOrder;
                            DREX11.BaseEntry = item.DocEntry.Oid;
                            DREX11.BaseOid = item.Oid;

                            DREX.OGW11DREX.Add(DREX11);
                        }

                        foreach (OGW12ORDR payment in dtl.Header.OGW12ORDR)
                        {
                            OGW12DREX DREX12 = new OGW12DREX(DREX.Session);

                            DREX12.PaymentMethod = payment.PaymentMethod;
                            DREX12.CashAcctCode = payment.CashAcctCode;
                            if (payment.Consignment != null)
                            {
                                DREX12.Consignment = DREX12.Session.GetObjectByKey<Consignment>(payment.Consignment.Oid);
                            }
                            DREX12.CashAmount = payment.CashAmount;
                            DREX12.CashRefNum = payment.CashRefNum;
                            DREX12.CreditCardAcctCode = payment.CreditCardAcctCode;
                            if (payment.CardType != null)
                            {
                                DREX12.CardType = DREX12.Session.GetObjectByKey<CardType>(payment.CardType.Oid);
                            }
                            DREX12.CreditCardNo = payment.CreditCardNo;
                            DREX12.CardHolderName = payment.CardHolderName;
                            if (payment.Instalment != null)
                            {
                                DREX12.Instalment = DREX12.Session.GetObjectByKey<Instalment>(payment.Instalment.Oid);
                            }
                            DREX12.TerminalID = payment.TerminalID;
                            if (payment.CardIssuer != null)
                            {
                                DREX12.CardIssuer = DREX12.Session.GetObjectByKey<CardIssuer>(payment.CardIssuer.Oid);
                            }
                            if (payment.Merchant != null)
                            {
                                DREX12.Merchant = DREX12.Session.GetObjectByKey<CardMachineBank>(payment.Merchant.Oid);
                            }
                            DREX12.ApprovalCode = payment.ApprovalCode;
                            DREX12.BatchNo = payment.BatchNo;
                            DREX12.Transaction = payment.Transaction;
                            DREX12.CreditCardAmount = payment.CreditCardAmount;
                            DREX12.VoucherAcctCode = payment.VoucherAcctCode;
                            if (payment.VoucherType != null)
                            {
                                DREX12.VoucherType = DREX12.Session.GetObjectByKey<Voucher>(payment.VoucherType.Oid);
                            }
                            DREX12.VoucherNo = payment.VoucherNo;
                            if (payment.TaxCode != null)
                            {
                                DREX12.TaxCode = DREX12.Session.GetObjectByKey<vwTax>(payment.TaxCode.Code);
                            }
                            DREX12.VoucherAmount = payment.VoucherAmount;
                            DREX12.PaymentTotal = payment.PaymentTotal;
                            DREX12.BaseEntry = payment.DocEntry.Oid;
                            DREX12.BaseOid = payment.Oid;

                            DREX.OGW12DREX.Add(DREX12);
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
                        da.SelectCommand.CommandText = "EXEC FTS_sp_CopyFrom 'OGW10ORDR', 'OGW10ORDN', '" + Customer + "' ";
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

            if (View.ObjectTypeInfo.Type == typeof(OGW10EXCO))
            {
                OGW10EXCO EXCO = (OGW10EXCO)View.CurrentObject;

                Customer = EXCO.Name != null ? EXCO.Name.Oid.ToString() : "";

                IObjectSpace os = Application.CreateObjectSpace(typeof(CopyList_OGW11ORDR));
                string listViewId = Application.FindLookupListViewId(typeof(CopyList_OGW11ORDR));
                CollectionSourceBase collectionSource = Application.CreateCollectionSource(os, typeof(CopyList_OGW11ORDR), listViewId);

                using (SqlConnection conn = new SqlConnection(genCon.getConnectionString()))
                {
                    using (SqlDataAdapter da = new SqlDataAdapter("", conn))
                    {
                        da.SelectCommand.CommandText = "EXEC FTS_sp_CopyFrom 'OGW10ORDR', 'OGW10EXCO', '" + Customer + "' ";
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

            if (View.ObjectTypeInfo.Type == typeof(OGW10DREQ))
            {
                OGW10DREQ DREQ = (OGW10DREQ)View.CurrentObject;

                //Customer = DREQ.Name != null ? DREQ.Name.Oid.ToString() : "";

                IObjectSpace os = Application.CreateObjectSpace(typeof(CopyList_OGW11ORDR));
                string listViewId = Application.FindLookupListViewId(typeof(CopyList_OGW11ORDR));
                CollectionSourceBase collectionSource = Application.CreateCollectionSource(os, typeof(CopyList_OGW11ORDR), listViewId);

                using (SqlConnection conn = new SqlConnection(genCon.getConnectionString()))
                {
                    using (SqlDataAdapter da = new SqlDataAdapter("", conn))
                    {
                        da.SelectCommand.CommandText = "EXEC FTS_sp_CopyFrom 'OGW10ORDR', 'OGW10DREQ', '" + Customer + "' ";
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

            if (View.ObjectTypeInfo.Type == typeof(OGW10DREX))
            {
                OGW10DREX DREX = (OGW10DREX)View.CurrentObject;

                //Customer = DREQ.Name != null ? DREQ.Name.Oid.ToString() : "";

                IObjectSpace os = Application.CreateObjectSpace(typeof(CopyList_OGW11ORDR));
                string listViewId = Application.FindLookupListViewId(typeof(CopyList_OGW11ORDR));
                CollectionSourceBase collectionSource = Application.CreateCollectionSource(os, typeof(CopyList_OGW11ORDR), listViewId);

                using (SqlConnection conn = new SqlConnection(genCon.getConnectionString()))
                {
                    using (SqlDataAdapter da = new SqlDataAdapter("", conn))
                    {
                        da.SelectCommand.CommandText = "EXEC FTS_sp_CopyFrom 'OGW10ORDR', 'OGW10DREX', '" + Customer + "' ";
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

        private void CopyFrmDR_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            if (View.ObjectTypeInfo.Type == typeof(OGW10OPKL))
            {
                try
                {
                    int row = 0;
                    OGW10OPKL OPKL = (OGW10OPKL)View.CurrentObject;

                    foreach (CopyList_OGW11DREQ dtl in ((ListView)e.PopupWindow.View).SelectedObjects)
                    {
                        if (OPKL.DeliveryContact != null)
                        {
                            OPKL.DeliveryContact = OPKL.Session.GetObjectByKey<Customer>(dtl.Header.DeliveryContact.Oid);
                        }
                        OPKL.DeliveryAddress1 = dtl.Header.DeliveryAddress1;
                        OPKL.DeliveryAddress2 = dtl.Header.DeliveryAddress2;
                        OPKL.DeliveryCity = dtl.Header.DeliveryCity;
                        OPKL.DeliveryDistrict = dtl.Header.DeliveryDistrict;
                        OPKL.DeliveryPostCode = dtl.Header.DeliveryPostCode;
                        OPKL.DeliveryCountry = dtl.Header.DeliveryCountry;
                        OPKL.DeliveryMobilePhone = dtl.Header.DeliveryMobilePhone;
                        OPKL.DeliveryHomePhone = dtl.Header.DeliveryHomePhone;
                        if (dtl.Header.DeliveryRace != null)
                        {
                            OPKL.DeliveryRace = OPKL.Session.GetObjectByKey<Races>(dtl.Header.DeliveryRace.Oid);
                        }

                        foreach (OGW11DREQ item in dtl.Header.OGW11DREQ)
                        {
                            OGW11OPKL OPKL11 = new OGW11OPKL(OPKL.Session);

                            OPKL11.Class = item.Class;
                            OPKL11.ItemCode = OPKL11.Session.GetObjectByKey<vwItemMasters>(item.ItemCode.ItemCode);
                            OPKL11.ItemName = item.ItemName;
                            OPKL11.UnitPrice = item.UnitPrice;
                            OPKL11.Order = item.UnitPrice;
                            OPKL11.Taken = item.Taken;
                            OPKL11.BackOrder = item.BackOrder;
                            OPKL11.BaseEntry = item.DocEntry.Oid;
                            OPKL11.BaseOid = item.Oid;

                            OPKL.OGW11OPKL.Add(OPKL11);
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

        private void CopyFrmDR_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            var Customer = "";
            var ObjType = "";

            if (View.ObjectTypeInfo.Type == typeof(OGW10OPKL))
            {
                OGW10OPKL OPKL = (OGW10OPKL)View.CurrentObject;

                //Customer = ORDR.Name != null ? ORDR.Name.Oid.ToString() : "";
                //ObjType = "EditOrder";

                IObjectSpace os = Application.CreateObjectSpace(typeof(CopyList_OGW11DREQ));
                string listViewId = Application.FindLookupListViewId(typeof(CopyList_OGW11DREQ));
                CollectionSourceBase collectionSource = Application.CreateCollectionSource(os, typeof(CopyList_OGW11DREQ), listViewId);

                using (SqlConnection conn = new SqlConnection(genCon.getConnectionString()))
                {
                    using (SqlDataAdapter da = new SqlDataAdapter("", conn))
                    {
                        da.SelectCommand.CommandText = "EXEC FTS_sp_CopyFrom 'OGW10DREQ', '" + ObjType + "', '" + Customer + "' ";
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow dtrow in dt.Rows)
                            {
                                CopyList_OGW11DREQ DREQ = os.CreateObject<CopyList_OGW11DREQ>();

                                DREQ.Oid = int.Parse(dtrow["Oid"].ToString());
                                DREQ.Header = ObjectSpace.GetObjectByKey<OGW10DREQ>(dtrow["Header"]);
                                //SO.Details = ObjectSpace.GetObjectByKey<OGW11ORDR>(dtrow["Details"]);
                                collectionSource.List.Add(DREQ);
                            }
                        }
                    }
                }

                e.View = Application.CreateListView(listViewId, collectionSource, true);
            }
        }

        private void CopyFrmDREX_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            if (View.ObjectTypeInfo.Type == typeof(OGW10PLEX))
            {
                try
                {
                    int row = 0;
                    OGW10PLEX PLEX = (OGW10PLEX)View.CurrentObject;

                    foreach (CopyList_OGW11DREX dtl in ((ListView)e.PopupWindow.View).SelectedObjects)
                    {
                        if (PLEX.DeliveryContact != null)
                        {
                            PLEX.DeliveryContact = PLEX.Session.GetObjectByKey<Customer>(dtl.Header.DeliveryContact.Oid);
                        }
                        PLEX.DeliveryAddress1 = dtl.Header.DeliveryAddress1;
                        PLEX.DeliveryAddress2 = dtl.Header.DeliveryAddress2;
                        PLEX.DeliveryCity = dtl.Header.DeliveryCity;
                        PLEX.DeliveryDistrict = dtl.Header.DeliveryDistrict;
                        PLEX.DeliveryPostCode = dtl.Header.DeliveryPostCode;
                        PLEX.DeliveryCountry = dtl.Header.DeliveryCountry;
                        PLEX.DeliveryMobilePhone = dtl.Header.DeliveryMobilePhone;
                        PLEX.DeliveryHomePhone = dtl.Header.DeliveryHomePhone;
                        if (dtl.Header.DeliveryRace != null)
                        {
                            PLEX.DeliveryRace = PLEX.Session.GetObjectByKey<Races>(dtl.Header.DeliveryRace.Oid);
                        }

                        foreach (OGW11DREX item in dtl.Header.OGW11DREX)
                        {
                            OGW11PLEX PLEX11 = new OGW11PLEX(PLEX.Session);

                            PLEX11.Class = item.Class;
                            PLEX11.ItemCode = PLEX11.Session.GetObjectByKey<vwItemMasters>(item.ItemCode.ItemCode);
                            PLEX11.ItemName = item.ItemName;
                            PLEX11.UnitPrice = item.UnitPrice;
                            PLEX11.Order = item.UnitPrice;
                            PLEX11.Taken = item.Taken;
                            PLEX11.BackOrder = item.BackOrder;
                            PLEX11.BaseEntry = item.DocEntry.Oid;
                            PLEX11.BaseOid = item.Oid;

                            PLEX.OGW11PLEX.Add(PLEX11);
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

        private void CopyFrmDREX_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            var Customer = "";
            var ObjType = "";

            if (View.ObjectTypeInfo.Type == typeof(OGW10PLEX))
            {
                OGW10PLEX PLEX = (OGW10PLEX)View.CurrentObject;

                //Customer = ORDR.Name != null ? ORDR.Name.Oid.ToString() : "";
                //ObjType = "EditOrder";

                IObjectSpace os = Application.CreateObjectSpace(typeof(CopyList_OGW11DREX));
                string listViewId = Application.FindLookupListViewId(typeof(CopyList_OGW11DREX));
                CollectionSourceBase collectionSource = Application.CreateCollectionSource(os, typeof(CopyList_OGW11DREX), listViewId);

                using (SqlConnection conn = new SqlConnection(genCon.getConnectionString()))
                {
                    using (SqlDataAdapter da = new SqlDataAdapter("", conn))
                    {
                        da.SelectCommand.CommandText = "EXEC FTS_sp_CopyFrom 'OGW10PLEX', '" + ObjType + "', '" + Customer + "' ";
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow dtrow in dt.Rows)
                            {
                                CopyList_OGW11DREX DREX = os.CreateObject<CopyList_OGW11DREX>();

                                DREX.Oid = int.Parse(dtrow["Oid"].ToString());
                                DREX.Header = ObjectSpace.GetObjectByKey<OGW10DREX>(dtrow["Header"]);
                                //SO.Details = ObjectSpace.GetObjectByKey<OGW11ORDR>(dtrow["Details"]);
                                collectionSource.List.Add(DREX);
                            }
                        }
                    }
                }

                e.View = Application.CreateListView(listViewId, collectionSource, true);
            }
        }
    }
}
