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
using System.ComponentModel;
using Country = OgawaPortal.Module.BusinessObjects.Maintenance.Country;

namespace OgawaPortal.Module.BusinessObjects.POS___Logistic
{
    [NavigationItem("POS - Logistic")]
    [XafDisplayName("Delivery Request")]
    [DefaultProperty("DocNum")]
    [Appearance("DisableEdit", AppearanceItemType.Action, "True", TargetItems = "SwitchToEditMode; Edit", Criteria = "NOT Status.Code IN ('DRAFT','REOPEN')", Enabled = false, Context = "Any")]
    [Appearance("HideDelete", AppearanceItemType = "Action", TargetItems = "Delete", Context = "Any", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
    [Appearance("HideSubmit", AppearanceItemType = "Action", TargetItems = "SubmitDoc", Criteria = "IsNew or NOT Status.Code IN ('DRAFT','REOPEN')", Context = "Any", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
    [Appearance("HideCancel", AppearanceItemType = "Action", TargetItems = "CancelDoc", Criteria = "IsNew or Status.Code IN ('CANCEL','CLOSED')", Context = "Any", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
    [Appearance("HideDREQCopyFrmSO", AppearanceItemType = "Action", TargetItems = "CopyFrmSO", Criteria = "IsNew or Status.Code != 'DRAFT'", Context = "Any", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]

    public class OGW10DREQ : XPObject
    { 
        public OGW10DREQ(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            CreateUser = Session.GetObjectByKey<ApplicationUser>(SecuritySystem.CurrentUserId).UserName;
            CreateDate = DateTime.Now;
            DeliveryReqDate = DateTime.Today;

            ObjType = Session.FindObject<vwObjType>(CriteriaOperator.Parse("Code = 'OGW10DREQ'"));
            Status = Session.FindObject<vwStatus>(CriteriaOperator.Parse("Code = 'DRAFT'"));
            FullPayment = true;
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

        private string _DocNum;
        [Index(0), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        [XafDisplayName("Document No")]
        [Appearance("DocNum", Enabled = false)]
        public string DocNum
        {
            get { return _DocNum; }
            set
            {
                SetPropertyValue("DocNum", ref _DocNum, value);
            }
        }

        private vwObjType _ObjType;
        [NoForeignKey]
        [Index(1), VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        [XafDisplayName("ObjType")]
        [Appearance("ObjType", Enabled = false)]
        public vwObjType ObjType
        {
            get { return _ObjType; }
            set
            {
                SetPropertyValue("ObjType", ref _ObjType, value);
            }
        }

        private DateTime _DeliveryReqDate;
        [XafDisplayName("Delivery Request Date")]
        [Index(2), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        public DateTime DeliveryReqDate
        {
            get { return _DeliveryReqDate; }
            set
            {
                SetPropertyValue("DeliveryReqDate", ref _DeliveryReqDate, value);
            }
        }

        private string _DRRefNo;
        [XafDisplayName("DR Ref No")]
        [Index(5), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        public string DRRefNo
        {
            get { return _DRRefNo; }
            set
            {
                SetPropertyValue("DRRefNo", ref _DRRefNo, value);
            }
        }

        private vwStatus _Status;
        [NoForeignKey]
        [XafDisplayName("Status")]
        [Appearance("Status", Enabled = false)]
        [Index(8), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        public vwStatus Status
        {
            get { return _Status; }
            set
            {
                SetPropertyValue("Status", ref _Status, value);
            }
        }

        private bool _FullPayment;
        [XafDisplayName("Full Payment")]
        [Index(9), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(false)]
        public bool FullPayment
        {
            get { return _FullPayment; }
            set
            {
                SetPropertyValue("FullPayment", ref _FullPayment, value);
            }
        }

        private string _Remarks;
        [Index(10), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        public string Remarks
        {
            get { return _Remarks; }
            set
            {
                SetPropertyValue("Remarks", ref _Remarks, value);
            }
        }

        private vwWarehouse _Warehouse;
        [NoForeignKey]
        [Index(11), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        [RuleRequiredField(DefaultContexts.Save)]
        public vwWarehouse Warehouse
        {
            get { return _Warehouse; }
            set
            {
                SetPropertyValue("Warehouse", ref _Warehouse, value);
            }
        }

        private string _BaseNum;
        [XafDisplayName("Sales Order No.")]
        [Index(12), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        [Appearance("BaseNum", Enabled = false)]
        public string BaseNum
        {
            get { return _BaseNum; }
            set
            {
                SetPropertyValue("BaseNum", ref _BaseNum, value);
            }
        }

        private string _BaseRefNo;
        [XafDisplayName("SO Ref No.")]
        [Index(15), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        [Appearance("BaseRefNo", Enabled = false)]
        public string BaseRefNo
        {
            get { return _BaseRefNo; }
            set
            {
                SetPropertyValue("BaseRefNo", ref _BaseRefNo, value);
            }
        }

        //Payment
        private decimal _SubTotal;
        [ImmediatePostData]
        [DbType("numeric(19,6)")]
        [ModelDefault("DisplayFormat", "n2")]
        [ModelDefault("EditMask", "n2")]
        [Appearance("SubTotal", Enabled = false)]
        [Index(18), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        [XafDisplayName("Sub Total")]
        public decimal SubTotal
        {
            get { return _SubTotal; }
            set
            {
                SetPropertyValue("SubTotal", ref _SubTotal, value);
            }
        }

        private decimal _OrderDiscount;
        [ImmediatePostData]
        [DbType("numeric(19,6)")]
        [ModelDefault("DisplayFormat", "n2")]
        [ModelDefault("EditMask", "n2")]
        [Appearance("OrderDiscount", Enabled = false)]
        [Index(20), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        [XafDisplayName("Order Discount")]
        public decimal OrderDiscount
        {
            get { return _OrderDiscount; }
            set
            {
                SetPropertyValue("OrderDiscount", ref _OrderDiscount, value);
            }
        }

        private decimal _Tax;
        [ImmediatePostData]
        [DbType("numeric(19,6)")]
        [ModelDefault("DisplayFormat", "n2")]
        [ModelDefault("EditMask", "n2")]
        [Appearance("Tax", Enabled = false)]
        [Index(22), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        [XafDisplayName("Tax")]
        public decimal Tax
        {
            get { return _Tax; }
            set
            {
                SetPropertyValue("Tax", ref _Tax, value);
            }
        }

        private decimal _TotalDue;
        [ImmediatePostData]
        [DbType("numeric(19,6)")]
        [ModelDefault("DisplayFormat", "n2")]
        [ModelDefault("EditMask", "n2")]
        [Appearance("TotalDue", Enabled = false)]
        [Index(25), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        [XafDisplayName("Total Due")]
        public decimal TotalDue
        {
            get { return _TotalDue; }
            set
            {
                SetPropertyValue("TotalDue", ref _TotalDue, value);
            }
        }

        private decimal _SettlementDiscount;
        [ImmediatePostData]
        [DbType("numeric(19,6)")]
        [ModelDefault("DisplayFormat", "n2")]
        [ModelDefault("EditMask", "n2")]
        [Appearance("SettlementDiscount", Enabled = false)]
        [Index(28), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        [XafDisplayName("Settlement Discount")]
        public decimal SettlementDiscount
        {
            get { return _SettlementDiscount; }
            set
            {
                SetPropertyValue("SettlementDiscount", ref _SettlementDiscount, value);
            }
        }

        private decimal _NetTotalDue;
        [ImmediatePostData]
        [DbType("numeric(19,6)")]
        [ModelDefault("DisplayFormat", "n2")]
        [ModelDefault("EditMask", "n2")]
        [Appearance("NetTotalDue", Enabled = false)]
        [Index(30), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        [XafDisplayName("Net Total Due")]
        public decimal NetTotalDue
        {
            get { return _NetTotalDue; }
            set
            {
                SetPropertyValue("NetTotalDue", ref _NetTotalDue, value);
            }
        }

        private decimal _Cash;
        [ImmediatePostData]
        [DbType("numeric(19,6)")]
        [ModelDefault("DisplayFormat", "n2")]
        [ModelDefault("EditMask", "n2")]
        [Appearance("Cash", Enabled = false)]
        [Index(32), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        [XafDisplayName("Cash")]
        public decimal Cash
        {
            get { return _Cash; }
            set
            {
                SetPropertyValue("Cash", ref _Cash, value);
            }
        }

        private decimal _CreditCard;
        [ImmediatePostData]
        [DbType("numeric(19,6)")]
        [ModelDefault("DisplayFormat", "n2")]
        [ModelDefault("EditMask", "n2")]
        [Appearance("CreditCard", Enabled = false)]
        [Index(35), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        [XafDisplayName("Credit Card")]
        public decimal CreditCard
        {
            get { return _CreditCard; }
            set
            {
                SetPropertyValue("CreditCard", ref _CreditCard, value);
            }
        }

        private decimal _Voucher;
        [ImmediatePostData]
        [DbType("numeric(19,6)")]
        [ModelDefault("DisplayFormat", "n2")]
        [ModelDefault("EditMask", "n2")]
        [Appearance("Voucher", Enabled = false)]
        [Index(38), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        [XafDisplayName("Voucher")]
        public decimal Voucher
        {
            get { return _Voucher; }
            set
            {
                SetPropertyValue("Voucher", ref _Voucher, value);
            }
        }

        private decimal _CreditNote;
        [ImmediatePostData]
        [DbType("numeric(19,6)")]
        [ModelDefault("DisplayFormat", "n2")]
        [ModelDefault("EditMask", "n2")]
        [Appearance("CreditNote", Enabled = false)]
        [Index(40), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        [XafDisplayName("Credit Note")]
        public decimal CreditNote
        {
            get { return _CreditNote; }
            set
            {
                SetPropertyValue("CreditNote", ref _CreditNote, value);
            }
        }

        private decimal _PreviousPayment;
        [ImmediatePostData]
        [DbType("numeric(19,6)")]
        [ModelDefault("DisplayFormat", "n2")]
        [ModelDefault("EditMask", "n2")]
        [Appearance("PreviousPayment", Enabled = false)]
        [Index(42), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        [XafDisplayName("Previous Payment")]
        public decimal PreviousPayment
        {
            get { return _PreviousPayment; }
            set
            {
                SetPropertyValue("PreviousPayment", ref _PreviousPayment, value);
            }
        }

        private decimal _TotalPayment;
        [ImmediatePostData]
        [DbType("numeric(19,6)")]
        [ModelDefault("DisplayFormat", "n2")]
        [ModelDefault("EditMask", "n2")]
        [Appearance("TotalPayment", Enabled = false)]
        [Index(45), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        [XafDisplayName("Total Payment")]
        public decimal TotalPayment
        {
            get { return _TotalPayment; }
            set
            {
                SetPropertyValue("TotalPayment", ref _TotalPayment, value);
            }
        }

        private decimal _OrderBalanceDue;
        [ImmediatePostData]
        [DbType("numeric(19,6)")]
        [ModelDefault("DisplayFormat", "n2")]
        [ModelDefault("EditMask", "n2")]
        [Appearance("OrderBalanceDue", Enabled = false)]
        [Index(48), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        [XafDisplayName("Order Balance Due")]
        public decimal OrderBalanceDue
        {
            get { return _OrderBalanceDue; }
            set
            {
                SetPropertyValue("OrderBalanceDue", ref _OrderBalanceDue, value);
            }
        }

        private decimal _MinimumDue;
        [ImmediatePostData]
        [DbType("numeric(19,6)")]
        [ModelDefault("DisplayFormat", "n2")]
        [ModelDefault("EditMask", "n2")]
        [Appearance("MinimumDue", Enabled = false)]
        [Index(50), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        [XafDisplayName("Minimum Due")]
        public decimal MinimumDue
        {
            get { return _MinimumDue; }
            set
            {
                SetPropertyValue("MinimumDue", ref _MinimumDue, value);
            }
        }

        private decimal _MinDueBalance;
        [ImmediatePostData]
        [DbType("numeric(19,6)")]
        [ModelDefault("DisplayFormat", "n2")]
        [ModelDefault("EditMask", "n2")]
        [Appearance("MinDueBalance", Enabled = false)]
        [Index(52), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        [XafDisplayName("Min. Due Balance")]
        public decimal MinDueBalance
        {
            get { return _MinDueBalance; }
            set
            {
                SetPropertyValue("MinDueBalance", ref _MinDueBalance, value);
            }
        }

        // Billing
        private Customer _BillName;
        [ImmediatePostData]
        [XafDisplayName("Customer Name")]
        [Index(55), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        public Customer BillName
        {
            get { return _BillName; }
            set
            {
                SetPropertyValue("BillName", ref _BillName, value);
            }
        }

        private string _BillAddress1;
        [XafDisplayName("Address1")]
        [Index(58), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        public string BillAddress1
        {
            get { return _BillAddress1; }
            set
            {
                SetPropertyValue("BillAddress1", ref _BillAddress1, value);
            }
        }

        private string _BillAddress2;
        [XafDisplayName("Address2")]
        [Index(60), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        public string BillAddress2
        {
            get { return _BillAddress2; }
            set
            {
                SetPropertyValue("BillAddress2", ref _BillAddress2, value);
            }
        }

        private string _BillCity;
        [XafDisplayName("City")]
        [Index(62), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        public string BillCity
        {
            get { return _BillCity; }
            set
            {
                SetPropertyValue("BillCity", ref _BillCity, value);
            }
        }

        private District _BillDistrict;
        [XafDisplayName("District/State")]
        [Index(65), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        public District BillDistrict
        {
            get { return _BillDistrict; }
            set
            {
                SetPropertyValue("BillDistrict", ref _BillDistrict, value);
            }
        }

        private string _BillPostCode;
        [XafDisplayName("PostCode")]
        [Index(68), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        public string BillPostCode
        {
            get { return _BillPostCode; }
            set
            {
                SetPropertyValue("BillPostCode", ref _BillPostCode, value);
            }
        }

        private Country _BillCountry;
        [XafDisplayName("Country")]
        [Index(70), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        public Country BillCountry
        {
            get { return _BillCountry; }
            set
            {
                SetPropertyValue("BillCountry", ref _BillCountry, value);
            }
        }

        private string _BillMobilePhone;
        [XafDisplayName("Mobile Phone")]
        [Index(72), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        public string BillMobilePhone
        {
            get { return _BillMobilePhone; }
            set
            {
                SetPropertyValue("BillMobilePhone", ref _BillMobilePhone, value);
            }
        }

        private string _BillHomePhone;
        [XafDisplayName("Home Phone")]
        [Index(75), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        public string BillHomePhone
        {
            get { return _BillHomePhone; }
            set
            {
                SetPropertyValue("BillHomePhone", ref _BillHomePhone, value);
            }
        }

        private string _BillEmail;
        [XafDisplayName("Email")]
        [Index(78), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        public string BillEmail
        {
            get { return _BillEmail; }
            set
            {
                SetPropertyValue("BillEmail", ref _BillEmail, value);
            }
        }

        private string _BillIdentityNo;
        [XafDisplayName("Identity No/D.O.B")]
        [Index(80), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        public string BillIdentityNo
        {
            get { return _BillIdentityNo; }
            set
            {
                SetPropertyValue("BillIdentityNo", ref _BillIdentityNo, value);
            }
        }

        private Races _BillRace;
        [XafDisplayName("Race")]
        [Index(82), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        public Races BillRace
        {
            get { return _BillRace; }
            set
            {
                SetPropertyValue("BillRace", ref _BillRace, value);
            }
        }

        // Delivery
        private Customer _DeliveryContact;
        [ImmediatePostData]
        [XafDisplayName("Contact")]
        [Index(85), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        public Customer DeliveryContact
        {
            get { return _DeliveryContact; }
            set
            {
                SetPropertyValue("DeliveryContact", ref _DeliveryContact, value);
            }
        }

        private string _DeliveryAddress1;
        [XafDisplayName("Address1")]
        [Index(88), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        public string DeliveryAddress1
        {
            get { return _DeliveryAddress1; }
            set
            {
                SetPropertyValue("DeliveryAddress1", ref _DeliveryAddress1, value);
            }
        }

        private string _DeliveryAddress2;
        [XafDisplayName("Address2")]
        [Index(90), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        public string DeliveryAddress2
        {
            get { return _DeliveryAddress2; }
            set
            {
                SetPropertyValue("DeliveryAddress2", ref _DeliveryAddress2, value);
            }
        }

        private string _DeliveryCity;
        [XafDisplayName("City")]
        [Index(92), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        public string DeliveryCity
        {
            get { return _DeliveryCity; }
            set
            {
                SetPropertyValue("DeliveryCity", ref _DeliveryCity, value);
            }
        }

        private District _DeliveryDistrict;
        [XafDisplayName("District/State")]
        [Index(95), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        public District DeliveryDistrict
        {
            get { return _DeliveryDistrict; }
            set
            {
                SetPropertyValue("DeliveryDistrict", ref _DeliveryDistrict, value);
            }
        }

        private string _DeliveryPostCode;
        [XafDisplayName("PostCode")]
        [Index(98), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        public string DeliveryPostCode
        {
            get { return _DeliveryPostCode; }
            set
            {
                SetPropertyValue("DeliveryPostCode", ref _DeliveryPostCode, value);
            }
        }

        private Country _DeliveryCountry;
        [XafDisplayName("Country")]
        [Index(100), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        public Country DeliveryCountry
        {
            get { return _DeliveryCountry; }
            set
            {
                SetPropertyValue("DeliveryCountry", ref _DeliveryCountry, value);
            }
        }

        private string _DeliveryMobilePhone;
        [XafDisplayName("Mobile Phone")]
        [Index(102), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        public string DeliveryMobilePhone
        {
            get { return _DeliveryMobilePhone; }
            set
            {
                SetPropertyValue("DeliveryMobilePhone", ref _DeliveryMobilePhone, value);
            }
        }

        private string _DeliveryHomePhone;
        [XafDisplayName("Home Phone")]
        [Index(105), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        public string DeliveryHomePhone
        {
            get { return _DeliveryHomePhone; }
            set
            {
                SetPropertyValue("DeliveryHomePhone", ref _DeliveryHomePhone, value);
            }
        }

        private Races _DeliveryRace;
        [XafDisplayName("Race")]
        [Index(108), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        public Races DeliveryRace
        {
            get { return _DeliveryRace; }
            set
            {
                SetPropertyValue("DeliveryRace", ref _DeliveryRace, value);
            }
        }

        private string _SubmitBy;
        [XafDisplayName("Submitted By"), ToolTip("Submitted By")]
        [Index(9991), VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public string SubmitBy
        {
            get { return _SubmitBy; }
            set { SetPropertyValue("SubmitBy", ref _SubmitBy, value); }
        }

        private DateTime _SubmitDate;
        [Index(9992), VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        [XafDisplayName("Submitted Date"), ToolTip("Submitted Date")]
        [ModelDefault("DisplayFormat", "dd/MM/yyyy")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        [DbType("datetime")]
        public DateTime SubmitDate
        {
            get { return _SubmitDate; }
            set { SetPropertyValue("SubmitDate", ref _SubmitDate, value); }
        }

        [Browsable(false)]
        public bool IsNew
        {
            get
            { return Session.IsNewObject(this); }
        }

        [Association("OGW10DREQ-OGW11DREQ")]
        [XafDisplayName("Items")]
        [Appearance("OGW11DREQ", Enabled = false, Criteria = "IsNew")]
        public XPCollection<OGW11DREQ> OGW11DREQ
        {
            get { return GetCollection<OGW11DREQ>("OGW11DREQ"); }
        }

        [Association("OGW10DREQ-OGW13DREQ")]
        [XafDisplayName("Proof of Documents")]
        public XPCollection<OGW13DREQ> OGW13DREQ
        {
            get { return GetCollection<OGW13DREQ>("OGW13DREQ"); }
        }

        [Association("OGW10DREQ-OGW12DREQ")]
        [XafDisplayName("Payment")]
        [Appearance("OGW12DREQ", Enabled = false, Criteria = "IsNew")]
        public XPCollection<OGW12DREQ> OGW12DREQ
        {
            get { return GetCollection<OGW12DREQ>("OGW12DREQ"); }
        }

        [Association("OGW10DREQ-OGW15DREQ")]
        [XafDisplayName("Document Status")]
        public XPCollection<OGW15DREQ> OGW15DREQ
        {
            get { return GetCollection<OGW15DREQ>("OGW15DREQ"); }
        }

        private XPCollection<AuditDataItemPersistent> auditTrail;
        public XPCollection<AuditDataItemPersistent> AuditTrail
        {
            get
            {
                if (auditTrail == null)
                {
                    auditTrail = AuditedObjectWeakReference.GetAuditTrail(Session, this);
                }
                return auditTrail;
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
            }
        }

        protected override void OnSaved()
        {
            base.OnSaved();
            this.Reload();
        }
    }
}