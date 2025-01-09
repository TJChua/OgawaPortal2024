using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using OgawaPortal.Module.BusinessObjects.Maintenance;
using OgawaPortal.Module.BusinessObjects.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace OgawaPortal.Module.BusinessObjects.Sales_Order
{
    [XafDisplayName("Sales Payment")]
    [Appearance("LinkDoc", AppearanceItemType = "Action", TargetItems = "Link", Context = "ListView", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
    [Appearance("UnlinkDoc", AppearanceItemType = "Action", TargetItems = "Unlink", Context = "ListView", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]

    [RuleCriteria("CashSaveValid", DefaultContexts.Save, "IsValid = 0", "Please select Consignment.")]
    [RuleCriteria("VourcherSaveValid", DefaultContexts.Save, "IsValid1 = 0", "Please fill vourcher no. and vourcher type.")]
    public class OGW12ORDR : XPObject
    { 
        public OGW12ORDR(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            CreateUser = Session.GetObjectByKey<ApplicationUser>(SecuritySystem.CurrentUserId).UserName;
            CreateDate = DateTime.Now;
        }

        private OGW10ORDR _DocEntry;
        [Association("OGW10ORDR-OGW12ORDR")]
        [Index(0), VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        [Appearance("DocEntry", Enabled = false)]
        public OGW10ORDR DocEntry
        {
            get { return _DocEntry; }
            set { SetPropertyValue("DocEntry", ref _DocEntry, value); }
        }


        private string _CreateUser;
        [XafDisplayName("Create User")]
        [Index(300), VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public string CreateUser
        {
            get { return _CreateUser; }
            set
            {
                SetPropertyValue("CreateUser", ref _CreateUser, value);
            }
        }

        private DateTime? _CreateDate;
        [Index(301), VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public DateTime? CreateDate
        {
            get { return _CreateDate; }
            set
            {
                SetPropertyValue("CreateDate", ref _CreateDate, value);
            }
        }

        private string _UpdateUser;
        [XafDisplayName("Update User")]
        [Index(302), VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public string UpdateUser
        {
            get { return _UpdateUser; }
            set
            {
                SetPropertyValue("UpdateUser", ref _UpdateUser, value);
            }
        }

        private DateTime? _UpdateDate;
        [Index(303), VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public DateTime? UpdateDate
        {
            get { return _UpdateDate; }
            set
            {
                SetPropertyValue("UpdateDate", ref _UpdateDate, value);
            }
        }

        private PaymentMethod _PaymentMethod;
        [ImmediatePostData]
        [DataSourceCriteria("IsActive = 'True'")]
        [XafDisplayName("Payment Method")]
        [Index(10), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        public PaymentMethod PaymentMethod
        {
            get { return _PaymentMethod; }
            set
            {
                SetPropertyValue("PaymentMethod", ref _PaymentMethod, value);
                if (!IsLoading && value != null)
                {
                    CashAmount = 0;
                    CashAcctCode = null;
                    Consignment = null;
                    CashRefNum = null;

                    CreditCardAmount = 0;
                    CreditCardAcctCode = null;
                    CreditCardNo = null;
                    CardHolderName = null;
                    CardIssuer = null;
                    CardType = null;
                    TerminalID = null;
                    ApprovalCode = null;
                    BatchNo = null;

                    VoucherAmount = 0;
                    VoucherAcctCode = null;
                    VoucherNo = null;
                    VoucherType = null;
                    TaxCode = null;
                }
            }
        }

        private string _CashAcctCode;
        [ImmediatePostData]
        [XafDisplayName("Cash Account")]
        [Appearance("CashAcctCode", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Criteria = "PaymentMethod.Code != 'CASH'")]
        [Index(13), VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public string CashAcctCode
        {
            get { return _CashAcctCode; }
            set
            {
                SetPropertyValue("CashAcctCode", ref _CashAcctCode, value);
            }
        }

        private Consignment _Consignment;
        [XafDisplayName("Consignment")]
        [DataSourceCriteria("IsActive = 'True'")]
        [Appearance("Consignment", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Criteria = "PaymentMethod.Code != 'CASH'")]
        [Index(15), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        public Consignment Consignment
        {
            get { return _Consignment; }
            set
            {
                SetPropertyValue("Consignment", ref _Consignment, value);
            }
        }

        private decimal _CashAmount;
        [ImmediatePostData]
        [XafDisplayName("Amount")]
        [Appearance("CashAmount", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Criteria = "PaymentMethod.Code != 'CASH'")]
        [Index(18), VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        [ModelDefault("DisplayFormat", "n2")]
        [ModelDefault("EditMask", "n2")]
        [DbType("numeric(19,6)")]
        public decimal CashAmount
        {
            get { return _CashAmount; }
            set
            {
                SetPropertyValue("CashAmount", ref _CashAmount, value);
                if (!IsLoading)
                {
                    PaymentTotal = value + CreditCardAmount + VoucherAmount;
                }
            }
        }

        private string _CashRefNum;
        [XafDisplayName("Cash Reference No.")]
        [Appearance("CashRefNum", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Criteria = "PaymentMethod.Code != 'CASH'")]
        [Index(20), VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        public string CashRefNum
        {
            get { return _CashRefNum; }
            set
            {
                SetPropertyValue("CreditCardRefNum", ref _CashRefNum, value);
            }
        }

        private string _CreditCardAcctCode;
        [ImmediatePostData]
        [XafDisplayName("GL Account")]
        [Appearance("CreditCardAcctCode", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Criteria = "PaymentMethod.Code != 'CREDIT CARD'")]
        [Index(23), VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public string CreditCardAcctCode
        {
            get { return _CreditCardAcctCode; }
            set
            {
                SetPropertyValue("CreditCardAcctCode", ref _CreditCardAcctCode, value);
            }
        }

        private CardType _CardType;
        [XafDisplayName("Card Type")]
        [DataSourceCriteria("IsActive = 'True'")]
        [Appearance("CardType", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Criteria = "PaymentMethod.Code != 'CREDIT CARD'")]
        [Index(25), VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        public CardType CardType
        {
            get { return _CardType; }
            set
            {
                SetPropertyValue("CardType", ref _CardType, value);
            }
        }

        private string _CreditCardNo;
        [XafDisplayName("Credit Card No(Last 6 number)")]
        [Appearance("CreditCardNo", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Criteria = "PaymentMethod.Code != 'CREDIT CARD'")]
        [Index(28), VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        public string CreditCardNo
        {
            get { return _CreditCardNo; }
            set
            {
                SetPropertyValue("CreditCardNo", ref _CreditCardNo, value);
            }
        }

        private string _CardHolderName;
        [XafDisplayName("Card Holder Name")]
        [Appearance("CardHolderName", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Criteria = "PaymentMethod.Code != 'CREDIT CARD'")]
        [Index(30), VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        public string CardHolderName
        {
            get { return _CardHolderName; }
            set
            {
                SetPropertyValue("CardHolderName", ref _CardHolderName, value);
            }
        }

        private Instalment _Instalment;
        [XafDisplayName("Instalment")]
        [DataSourceCriteria("IsActive = 'True'")]
        [Appearance("Instalment", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Criteria = "PaymentMethod.Code != 'CREDIT CARD'")]
        [Index(33), VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        public Instalment Instalment
        {
            get { return _Instalment; }
            set
            {
                SetPropertyValue("Instalment", ref _Instalment, value);
            }
        }

        private string _TerminalID;
        [XafDisplayName("Terminal ID")]
        [Appearance("TerminalID", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Criteria = "PaymentMethod.Code != 'CREDIT CARD'")]
        [Index(35), VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        public string TerminalID
        {
            get { return _TerminalID; }
            set
            {
                SetPropertyValue("TerminalID", ref _TerminalID, value);
            }
        }

        private CardIssuer _CardIssuer;
        [XafDisplayName("Card Issuer")]
        [DataSourceCriteria("IsActive = 'True'")]
        [Appearance("CardIssuer", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Criteria = "PaymentMethod.Code != 'CREDIT CARD'")]
        [Index(38), VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        public CardIssuer CardIssuer
        {
            get { return _CardIssuer; }
            set
            {
                SetPropertyValue("CardIssuer", ref _CardIssuer, value);
            }
        }

        private CardMachineBank _Merchant;
        [XafDisplayName("Merchant")]
        [DataSourceCriteria("IsActive = 'True'")]
        [Appearance("Merchant", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Criteria = "PaymentMethod.Code != 'CREDIT CARD'")]
        [Index(40), VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        public CardMachineBank Merchant
        {
            get { return _Merchant; }
            set
            {
                SetPropertyValue("Merchant", ref _Merchant, value);
            }
        }

        private string _ApprovalCode;
        [XafDisplayName("Approval Code")]
        [Appearance("ApprovalCode", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Criteria = "PaymentMethod.Code != 'CREDIT CARD'")]
        [Index(43), VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        public string ApprovalCode
        {
            get { return _ApprovalCode; }
            set
            {
                SetPropertyValue("ApprovalCode", ref _ApprovalCode, value);
            }
        }

        private string _BatchNo;
        [XafDisplayName("Batch No")]
        [Appearance("BatchNo", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Criteria = "PaymentMethod.Code != 'CREDIT CARD'")]
        [Index(45), VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        public string BatchNo
        {
            get { return _BatchNo; }
            set
            {
                SetPropertyValue("BatchNo", ref _BatchNo, value);
            }
        }

        private bool _Transaction;
        [XafDisplayName("Transaction(Online)")]
        [Appearance("Transaction", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Criteria = "PaymentMethod.Code != 'CREDIT CARD'")]
        [Index(48), VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        public bool Transaction
        {
            get { return _Transaction; }
            set
            {
                SetPropertyValue("Transaction", ref _Transaction, value);
            }
        }

        private decimal _CreditCardAmount;
        [ImmediatePostData]
        [XafDisplayName("Amount")]
        [Appearance("CreditCardAmount", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Criteria = "PaymentMethod.Code != 'CREDIT CARD'")]
        [Index(50), VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        [ModelDefault("DisplayFormat", "n2")]
        [ModelDefault("EditMask", "n2")]
        [DbType("numeric(19,6)")]
        public decimal CreditCardAmount
        {
            get { return _CreditCardAmount; }
            set
            {
                SetPropertyValue("CreditCardAmount", ref _CreditCardAmount, value);
                if (!IsLoading)
                {
                    PaymentTotal = value + CashAmount + VoucherAmount;
                }
            }
        }

        private string _VoucherAcctCode;
        [XafDisplayName("GL Account")]
        [Appearance("VoucherAcctCode", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Criteria = "PaymentMethod.Code != 'VOUCHER'")]
        [Index(53), VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public string VoucherAcctCode
        {
            get { return _VoucherAcctCode; }
            set
            {
                SetPropertyValue("VoucherAcctCode", ref _VoucherAcctCode, value);
            }
        }

        private Voucher _VoucherType;
        [XafDisplayName("Voucher Type")]
        [DataSourceCriteria("IsActive = 'True'")]
        [Appearance("VoucherType", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Criteria = "PaymentMethod.Code != 'VOUCHER'")]
        [Index(55), VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        public Voucher VoucherType
        {
            get { return _VoucherType; }
            set
            {
                SetPropertyValue("VoucherType", ref _VoucherType, value);
            }
        }

        private string _VoucherNo;
        [XafDisplayName("Voucher No.")]
        [Appearance("VoucherNo", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Criteria = "PaymentMethod.Code != 'VOUCHER'")]
        [Index(58), VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        public string VoucherNo
        {
            get { return _VoucherNo; }
            set
            {
                SetPropertyValue("VoucherNo", ref _VoucherNo, value);
            }
        }

        private vwTax _TaxCode;
        [NoForeignKey]
        [XafDisplayName("Tax Code")]
        [Appearance("TaxCode", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Criteria = "PaymentMethod.Code != 'VOUCHER'")]
        [Index(60), VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        public vwTax TaxCode
        {
            get { return _TaxCode; }
            set
            {
                SetPropertyValue("TaxCode", ref _TaxCode, value);
            }
        }

        private decimal _VoucherAmount;
        [ImmediatePostData]
        [XafDisplayName("Amount")]
        [Appearance("VoucherAmount", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Criteria = "PaymentMethod.Code != 'VOUCHER'")]
        [Index(63), VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        [ModelDefault("DisplayFormat", "n2")]
        [ModelDefault("EditMask", "n2")]
        [DbType("numeric(19,6)")]
        public decimal VoucherAmount
        {
            get { return _VoucherAmount; }
            set
            {
                if (SetPropertyValue("VoucherAmount", ref _VoucherAmount, value))
                {
                    if (!IsLoading)
                    {
                        PaymentTotal = value + CashAmount + CreditCardAmount;
                    }
                }
            }
        }

        private decimal _PaymentTotal;
        [XafDisplayName("Payment Total")]
        [Index(65), VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        [Appearance("PaymentTotal", Enabled = false)]
        [ModelDefault("DisplayFormat", "n2")]
        [ModelDefault("EditMask", "n2")]
        [DbType("numeric(19,6)")]
        public decimal PaymentTotal
        {
            get { return _PaymentTotal; }
            set
            {
                SetPropertyValue("PaymentTotal", ref _PaymentTotal, value);
            }
        }

        private int? _BaseEntry;
        [XafDisplayName("Base Entry"), ToolTip("Base Entry")]
        [Index(991), VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        [ModelDefault("DisplayFormat", "{0:d0}")]
        [Appearance("BaseEntry", Enabled = false)]
        [DbType("int")]
        public int? BaseEntry
        {
            get { return _BaseEntry; }
            set { SetPropertyValue("BaseEntry", ref _BaseEntry, value); }
        }

        private int? _BaseOid;
        [XafDisplayName("Base Oid"), ToolTip("Base Oid")]
        [Index(992), VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        [ModelDefault("DisplayFormat", "{0:d0}")]
        [Appearance("BaseOid", Enabled = false)]
        [DbType("int")]
        public int? BaseOid
        {
            get { return _BaseOid; }
            set { SetPropertyValue("BaseOid", ref _BaseOid, value); }
        }

        [Browsable(false)]
        public bool IsNew
        {
            get
            { return Session.IsNewObject(this); }
        }

        [Browsable(false)]
        public bool IsValid
        {
            get
            {
                if (this.PaymentMethod != null)
                {
                    if (this.PaymentMethod.Code == "CASH")
                    {
                        if (this.Consignment == null)
                        {
                            return true;
                        }
                    }
                }
               
                return false;
            }
        }

        [Browsable(false)]
        public bool IsValid1
        {
            get
            {
                if (this.PaymentMethod != null)
                {
                    if (this.PaymentMethod.Code == "VOUCHER")
                    {
                        if (this.VoucherNo == null || this.VoucherType == null)
                        {
                            return true;
                        }
                    }
                }

                return false;
            }
        }

        protected override void OnSaving()
        {
            base.OnSaving();
            if (!(Session is NestedUnitOfWork)
                && (Session.DataLayer != null)
                    && (Session.ObjectLayer is SimpleObjectLayer)
                        )
            {
                UpdateUser = Session.GetObjectByKey<ApplicationUser>(SecuritySystem.CurrentUserId).UserName;
                UpdateDate = DateTime.Now;

                if (Session.IsNewObject(this))
                {

                }
            }
        }
    }
}