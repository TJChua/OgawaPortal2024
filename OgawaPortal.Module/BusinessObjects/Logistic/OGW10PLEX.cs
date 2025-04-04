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

namespace OgawaPortal.Module.BusinessObjects.Logistic
{
    [NavigationItem("Logistic")]
    [XafDisplayName("Pick List Exchange")]
    [DefaultProperty("DocNum")]
    [Appearance("DisableEdit", AppearanceItemType.Action, "True", TargetItems = "SwitchToEditMode; Edit", Criteria = "NOT Status.Code IN ('DRAFT','REOPEN')", Enabled = false, Context = "Any")]
    [Appearance("HideDelete", AppearanceItemType = "Action", TargetItems = "Delete", Context = "Any", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
    [Appearance("HideSubmit", AppearanceItemType = "Action", TargetItems = "SubmitDoc", Criteria = "IsNew or NOT Status.Code IN ('DRAFT','REOPEN')", Context = "Any", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
    [Appearance("HideCancel", AppearanceItemType = "Action", TargetItems = "CancelDoc", Criteria = "IsNew or Status.Code IN ('CANCEL','CLOSED')", Context = "Any", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
    [Appearance("HidePLEXCopyFrmDR", AppearanceItemType = "Action", TargetItems = "CopyFrmDR", Criteria = "IsNew or Status.Code != 'DRAFT'", Context = "Any", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]

    public class OGW10PLEX : XPObject
    { 
        public OGW10PLEX(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            CreateUser = Session.GetObjectByKey<ApplicationUser>(SecuritySystem.CurrentUserId).UserName;
            CreateDate = DateTime.Now;
            DocDate = DateTime.Today;
            ExpectedDate = DateTime.Today;
            DeliveryTime = new TimeSpan(DateTime.Now.TimeOfDay.Hours, DateTime.Now.TimeOfDay.Minutes, 00);

            ObjType = Session.FindObject<vwObjType>(CriteriaOperator.Parse("Code = 'OGW10PLEX'"));
            Status = Session.FindObject<vwStatus>(CriteriaOperator.Parse("Code = 'DRAFT'"));
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

        private DateTime _DocDate;
        [XafDisplayName("Document Date")]
        [Index(2), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        public DateTime DocDate
        {
            get { return _DocDate; }
            set
            {
                SetPropertyValue("DocDate", ref _DocDate, value);
            }

        }
        private DateTime _ExpectedDate;
        [XafDisplayName("Expected Date")]
        [Index(3), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        public DateTime ExpectedDate
        {
            get { return _ExpectedDate; }
            set
            {
                SetPropertyValue("ExpectedDate", ref _ExpectedDate, value);
            }
        }

        private TimeSpan _DeliveryTime;
        [XafDisplayName("Delivery Time")]
        [Index(5), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        public TimeSpan DeliveryTime
        {
            get { return _DeliveryTime; }
            set
            {
                SetPropertyValue("DeliveryTime", ref _DeliveryTime, value);
            }
        }

        private string _SalesOrderNo;
        [XafDisplayName("Sales Order No")]
        [Index(8), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        [Appearance("SalesOrderNo", Enabled = false)]
        public string SalesOrderNo
        {
            get { return _SalesOrderNo; }
            set
            {
                SetPropertyValue("SalesOrderNo", ref _SalesOrderNo, value);
            }
        }

        private DateTime _SalesOrderDate;
        [XafDisplayName("Sales Order Date")]
        [Index(10), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        [Appearance("SalesOrderDate", Enabled = false)]
        public DateTime SalesOrderDate
        {
            get { return _SalesOrderDate; }
            set
            {
                SetPropertyValue("SalesOrderDate", ref _SalesOrderDate, value);
            }
        }

        private string _SORefNo;
        [XafDisplayName("SO Ref No")]
        [Index(12), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        [Appearance("SORefNo", Enabled = false)]
        public string SORefNo
        {
            get { return _SORefNo; }
            set
            {
                SetPropertyValue("SORefNo", ref _SORefNo, value);
            }
        }

        private string _DRRefNo;
        [XafDisplayName("DR Ref No")]
        [Index(15), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        [Appearance("DRRefNo", Enabled = false)]
        public string DRRefNo
        {
            get { return _DRRefNo; }
            set
            {
                SetPropertyValue("DRRefNo", ref _DRRefNo, value);
            }
        }

        private string _PLRefNo;
        [XafDisplayName("PL Ref No")]
        [Index(18), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        [Appearance("PLRefNo", Enabled = false)]
        public string PLRefNo
        {
            get { return _PLRefNo; }
            set
            {
                SetPropertyValue("PLRefNo", ref _PLRefNo, value);
            }
        }

        private Transporter _Transporter;
        [XafDisplayName("Transporter")]
        [DataSourceCriteria("IsActive = 'True'")]
        [Index(20), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        [RuleRequiredField(DefaultContexts.Save)]
        public Transporter Transporter
        {
            get { return _Transporter; }
            set
            {
                SetPropertyValue("Transporter", ref _Transporter, value);
            }
        }

        private vwWarehouse _Warehouse;
        [NoForeignKey]
        [Index(22), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        [RuleRequiredField(DefaultContexts.Save)]
        public vwWarehouse Warehouse
        {
            get { return _Warehouse; }
            set
            {
                SetPropertyValue("Warehouse", ref _Warehouse, value);
            }
        }

        private vwStatus _Status;
        [NoForeignKey]
        [XafDisplayName("Status")]
        [Appearance("Status", Enabled = false)]
        [Index(25), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        public vwStatus Status
        {
            get { return _Status; }
            set
            {
                SetPropertyValue("Status", ref _Status, value);
            }
        }

        private string _SellerRemarks;
        [XafDisplayName("Seller Remarks")]
        [Index(28), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        public string SellerRemarks
        {
            get { return _SellerRemarks; }
            set
            {
                SetPropertyValue("SellerRemarks", ref _SellerRemarks, value);
            }
        }

        private string _LogisticRemarks;
        [XafDisplayName("Logistic Remarks")]
        [Index(30), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        public string LogisticRemarks
        {
            get { return _LogisticRemarks; }
            set
            {
                SetPropertyValue("LogisticRemarks", ref _LogisticRemarks, value);
            }
        }

        private string _DeliveryOrderRemarks;
        [XafDisplayName("Delivery Order Remarks")]
        [Index(32), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        public string DeliveryOrderRemarks
        {
            get { return _DeliveryOrderRemarks; }
            set
            {
                SetPropertyValue("DeliveryOrderRemarks", ref _DeliveryOrderRemarks, value);
            }
        }

        private bool _FullPayment;
        [XafDisplayName("Full Payment")]
        [Index(33), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(false)]
        [Appearance("FullPayment", Enabled = false)]
        public bool FullPayment
        {
            get { return _FullPayment; }
            set
            {
                SetPropertyValue("FullPayment", ref _FullPayment, value);
            }
        }

        // Delivery
        private Customer _DeliveryContact;
        [ImmediatePostData]
        [XafDisplayName("Contact")]
        [Index(35), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
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
        [Index(38), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
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
        [Index(40), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
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
        [Index(42), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
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
        [Index(45), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
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
        [Index(48), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
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
        [Index(50), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
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
        [Index(52), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
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
        [Index(55), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
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
        [Index(58), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
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

        [Association("OGW10PLEX-OGW11PLEX")]
        [XafDisplayName("Items")]
        public XPCollection<OGW11PLEX> OGW11PLEX
        {
            get { return GetCollection<OGW11PLEX>("OGW11PLEX"); }
        }

        [Association("OGW10PLEX-OGW13PLEX")]
        [XafDisplayName("Attachments")]
        public XPCollection<OGW13PLEX> OGW13PLEX
        {
            get { return GetCollection<OGW13PLEX>("OGW13PLEX"); }
        }

        [Association("OGW10PLEX-OGW15PLEX")]
        [XafDisplayName("Document Status")]
        public XPCollection<OGW15PLEX> OGW15PLEX
        {
            get { return GetCollection<OGW15PLEX>("OGW15PLEX"); }
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