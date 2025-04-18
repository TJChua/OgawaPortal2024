﻿using DevExpress.Data.Filtering;
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
using System.Linq;
using Country = OgawaPortal.Module.BusinessObjects.Maintenance.Country;

namespace OgawaPortal.Module.BusinessObjects.Sales_Order
{
    [NavigationItem("POS - Sales")]
    [XafDisplayName("POS Sales")]
    [DefaultProperty("DocNum")]
    [Appearance("HideNewRsm", AppearanceItemType = "Action", TargetItems = "New", Context = "OGW10ORDR_ListView_RsmOrder", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
    [Appearance("HideNewRsm1", AppearanceItemType = "Action", TargetItems = "New", Context = "OGW10ORDR_DetailView_RsmOrder", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
    [Appearance("DisableEdit", AppearanceItemType.Action, "True", TargetItems = "SwitchToEditMode; Edit", Criteria = "NOT Status.Code IN ('DRAFT','REOPEN')", Enabled = false, Context = "Any")]
    [Appearance("HideDelete", AppearanceItemType = "Action", TargetItems = "Delete", Context = "Any", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
    [Appearance("HideSubmit", AppearanceItemType = "Action", TargetItems = "SubmitDoc", Criteria = "IsNew or NOT Status.Code IN ('DRAFT','REOPEN')", Context = "Any", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
    [Appearance("HideCancel", AppearanceItemType = "Action", TargetItems = "CancelDoc", Criteria = "IsNew or Status.Code IN ('CANCEL','CLOSED')", Context = "Any", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
    [Appearance("HideClose", AppearanceItemType = "Action", TargetItems = "CloseDoc", Criteria = "IsNew or Status.Code IN ('CANCEL','CLOSED')", Context = "Any", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
    [Appearance("HideORDRAddItem", AppearanceItemType = "Action", TargetItems = "AddItem", Criteria = "IsNew or Status.Code != 'DRAFT'", Context = "Any", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
    [Appearance("HideORDRCopyFrmSO", AppearanceItemType = "Action", TargetItems = "CopyFrmSO", Criteria = "IsNew or EditAndCancel = 0 or Status.Code != 'DRAFT'", Context = "Any", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
    [Appearance("HideORDRRsm", AppearanceItemType = "Action", TargetItems = "ResumeDoc", Criteria = "Status.Code != 'OPEN'", Context = "Any", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]

    public class OGW10ORDR : XPObject
    { 
        public OGW10ORDR(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            CreateUser = Session.GetObjectByKey<ApplicationUser>(SecuritySystem.CurrentUserId).UserName;
            CreateDate = DateTime.Now;
            SalesOrderDate = DateTime.Today;

            ObjType = Session.FindObject<vwObjType>(CriteriaOperator.Parse("Code = 'OGW10ORDR'"));
            Status = Session.FindObject<vwStatus>(CriteriaOperator.Parse("Code = 'DRAFT'"));
            PostStatus = Session.FindObject<vwStatus>(CriteriaOperator.Parse("Code = 'PN'"));
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

        private DateTime _SalesOrderDate;
        [XafDisplayName("Sales Order Date")]
        [Index(2), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        public DateTime SalesOrderDate
        {
            get { return _SalesOrderDate; }
            set
            {
                SetPropertyValue("SalesOrderDate", ref _SalesOrderDate, value);
            }
        }

        private string _SalesRefNo;
        [XafDisplayName("Sales Ref No")]
        [Index(5), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        public string SalesRefNo
        {
            get { return _SalesRefNo; }
            set
            {
                SetPropertyValue("SalesRefNo", ref _SalesRefNo, value);
            }
        }

        private vwStatus _Status;
        [NoForeignKey]
        [XafDisplayName("Status")]
        [Appearance("Status", Enabled = false)]
        [Index(6), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        public vwStatus Status
        {
            get { return _Status; }
            set
            {
                SetPropertyValue("Status", ref _Status, value);
            }
        }

        private CancelType _CancelType;
        [XafDisplayName("Cancel Type")]
        [DataSourceCriteria("IsActive = 'True'")]
        [Index(7), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        public CancelType CancelType
        {
            get { return _CancelType; }
            set
            {
                SetPropertyValue("CancelType", ref _CancelType, value);
            }
        }

        private CloseType _CloseType;
        [XafDisplayName("Close Type")]
        [DataSourceCriteria("IsActive = 'True'")]
        [Index(8), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        public CloseType CloseType
        {
            get { return _CloseType; }
            set
            {
                SetPropertyValue("CloseType", ref _CloseType, value);
            }
        }

        private string _Reason;
        [XafDisplayName("Reason")]
        [Index(9), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        public string Reason
        {
            get { return _Reason; }
            set
            {
                SetPropertyValue("Reason", ref _Reason, value);
            }
        }

        private string _Remarks;
        [XafDisplayName("Remarks")]
        [Index(10), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        public string Remarks
        {
            get { return _Remarks; }
            set
            {
                SetPropertyValue("Remarks", ref _Remarks, value);
            }
        }

        private string _BaseNum;
        [XafDisplayName("BaseNum")]
        [Index(11), VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        [Appearance("BaseNum", Enabled = false)]
        public string BaseNum
        {
            get { return _BaseNum; }
            set
            {
                SetPropertyValue("BaseNum", ref _BaseNum, value);
            }
        }

        // Customer
        private Customer _Name;
        [ImmediatePostData]
        [DataSourceCriteria("IsActive = 'True'")]
        [XafDisplayName("Cusotmer Name")]
        [RuleRequiredField(DefaultContexts.Save)]
        [LookupEditorMode(LookupEditorMode.AllItems)]
        [Index(12), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        public Customer Name
        {
            get { return _Name; }
            set
            {
                SetPropertyValue("Name", ref _Name, value);
                if (!IsLoading && value != null)
                {
                    Address1 = Name.Address1;
                    Address2 = Name.Address2;
                    City = Name.City;
                    District = Name.District;
                    PostCode = Name.PostCode;
                    Country = Name.Country;
                    MobilePhone = Name.MobilePhone;
                    HomePhone = Name.HomePhone;
                    Email = Name.Email;
                    Race = Name.Race;

                    //Bill
                    BillName = value;
                    BillAddress1 = Name.Address1;
                    BillAddress2 = Name.Address2;
                    BillCity = Name.City;
                    BillDistrict = Name.District;
                    BillPostCode = Name.PostCode;
                    BillCountry = Name.Country;
                    BillMobilePhone = Name.MobilePhone;
                    BillHomePhone = Name.HomePhone;
                    BillEmail = Name.Email;
                    BillRace = Name.Race;

                    //Delivery
                    DeliveryContact = value;
                    DeliveryAddress1 = Name.Address1;
                    DeliveryAddress2 = Name.Address2;
                    DeliveryCity = Name.City;
                    DeliveryDistrict = Name.District;
                    DeliveryPostCode = Name.PostCode;
                    DeliveryCountry = Name.Country;
                    DeliveryMobilePhone = Name.MobilePhone;
                    DeliveryHomePhone = Name.HomePhone;
                    DeliveryRace = Name.Race;
                }
                else if (!IsLoading && value == null)
                {
                    Address1 = null;
                    Address2 = null;
                    City = null;
                    District = null;
                    PostCode = null;
                    Country = null;
                    MobilePhone = null;
                    HomePhone = null;
                    Email = null;
                    Race = null;

                    //Bill
                    BillName = null;
                    BillAddress1 = null;
                    BillAddress2 = null;
                    BillCity = null;
                    BillDistrict = null;
                    BillPostCode = null;
                    BillCountry = null;
                    BillMobilePhone = null;
                    BillHomePhone = null;
                    BillEmail = null;
                    BillRace = null;

                    //Delivery
                    DeliveryContact = null;
                    DeliveryAddress1 = null;
                    DeliveryAddress2 = null;
                    DeliveryCity = null;
                    DeliveryDistrict = null;
                    DeliveryPostCode = null;
                    DeliveryCountry = null;
                    DeliveryMobilePhone = null;
                    DeliveryHomePhone = null;
                    DeliveryRace = null;
                }
            }
        }

        private string _Address1;
        [XafDisplayName("Address1")]
        [RuleRequiredField(DefaultContexts.Save)]
        [Index(13), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        public string Address1
        {
            get { return _Address1; }
            set
            {
                SetPropertyValue("Address1", ref _Address1, value);
            }
        }

        private string _Address2;
        [XafDisplayName("Address2")]
        [Index(15), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        public string Address2
        {
            get { return _Address2; }
            set
            {
                SetPropertyValue("Address2", ref _Address2, value);
            }
        }

        private string _City;
        [XafDisplayName("City")]
        [RuleRequiredField(DefaultContexts.Save)]
        [Index(18), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        public string City
        {
            get { return _City; }
            set
            {
                SetPropertyValue("City", ref _City, value);
            }
        }

        private District _District;
        [XafDisplayName("District/State")]
        [DataSourceCriteria("IsActive = 'True'")]
        [RuleRequiredField(DefaultContexts.Save)]
        [Index(20), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        public District District
        {
            get { return _District; }
            set
            {
                SetPropertyValue("District", ref _District, value);
            }
        }

        private string _PostCode;
        [XafDisplayName("PostCode")]
        [RuleRequiredField(DefaultContexts.Save)]
        [Index(15), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        public string PostCode
        {
            get { return _PostCode; }
            set
            {
                SetPropertyValue("PostCode", ref _PostCode, value);
            }
        }

        private Country _Country;
        [XafDisplayName("Country")]
        [DataSourceCriteria("IsActive = 'True'")]
        [RuleRequiredField(DefaultContexts.Save)]
        [Index(22), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        public Country Country
        {
            get { return _Country; }
            set
            {
                SetPropertyValue("Country", ref _Country, value);
            }
        }

        private string _MobilePhone;
        [XafDisplayName("Mobile Phone")]
        [RuleRequiredField(DefaultContexts.Save)]
        [Index(25), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        public string MobilePhone
        {
            get { return _MobilePhone; }
            set
            {
                SetPropertyValue("MobilePhone", ref _MobilePhone, value);
            }
        }

        private string _HomePhone;
        [XafDisplayName("Home Phone")]
        [Index(28), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        public string HomePhone
        {
            get { return _HomePhone; }
            set
            {
                SetPropertyValue("HomePhone", ref _HomePhone, value);
            }
        }

        private string _Email;
        [XafDisplayName("Email")]
        [Index(30), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        public string Email
        {
            get { return _Email; }
            set
            {
                SetPropertyValue("Email", ref _Email, value);
            }
        }

        private string _IdentityNo;
        [XafDisplayName("Identity No/D.O.B")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ImmediatePostData]
        [Index(32), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        public string IdentityNo
        {
            get { return _IdentityNo; }
            set
            {
                SetPropertyValue("IdentityNo", ref _IdentityNo, value);
            }
        }

        private Races _Race;
        [XafDisplayName("Race")]
        [DataSourceCriteria("IsActive = 'True'")]
        [RuleRequiredField(DefaultContexts.Save)]
        [Index(35), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        public Races Race
        {
            get { return _Race; }
            set
            {
                SetPropertyValue("Race", ref _Race, value);
            }
        }

        // Billing
        private Customer _BillName;
        [ImmediatePostData]
        [DataSourceCriteria("IsActive = 'True'")]
        [XafDisplayName("Customer Name")]
        [RuleRequiredField(DefaultContexts.Save)]
        [LookupEditorMode(LookupEditorMode.AllItems)]
        [Index(38), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
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
        [RuleRequiredField(DefaultContexts.Save)]
        [Index(40), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
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
        [Index(42), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
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
        [RuleRequiredField(DefaultContexts.Save)]
        [Index(45), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        public string BillCity
        {
            get { return _BillCity; }
            set
            {
                SetPropertyValue("BillCity", ref _BillCity, value);
            }
        }

        private District _BillDistrict;
        [XafDisplayName("District/State bill")]
        [DataSourceCriteria("IsActive = 'True'")]
        [RuleRequiredField(DefaultContexts.Save)]
        [Index(47), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
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
        [RuleRequiredField(DefaultContexts.Save)]
        [Index(50), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        public string BillPostCode
        {
            get { return _BillPostCode; }
            set
            {
                SetPropertyValue("BillPostCode", ref _BillPostCode, value);
            }
        }

        private Country _BillCountry;
        [XafDisplayName("Country bill")]
        [DataSourceCriteria("IsActive = 'True'")]
        [RuleRequiredField(DefaultContexts.Save)]
        [Index(52), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
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
        [RuleRequiredField(DefaultContexts.Save)]
        [Index(55), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
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
        [Index(57), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
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
        [Index(60), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
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
        [RuleRequiredField(DefaultContexts.Save)]
        [ImmediatePostData]
        [Index(62), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        public string BillIdentityNo
        {
            get { return _BillIdentityNo; }
            set
            {
                SetPropertyValue("BillIdentityNo", ref _BillIdentityNo, value);
            }
        }

        private Races _BillRace;
        [XafDisplayName("Race Bill")]
        [DataSourceCriteria("IsActive = 'True'")]
        [RuleRequiredField(DefaultContexts.Save)]
        [Index(65), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
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
        [DataSourceCriteria("IsActive = 'True'")]
        [RuleRequiredField(DefaultContexts.Save)]
        [LookupEditorMode(LookupEditorMode.AllItems)]
        [Index(67), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
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
        [RuleRequiredField(DefaultContexts.Save)]
        [Index(70), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
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
        [Index(72), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
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
        [RuleRequiredField(DefaultContexts.Save)]
        [Index(75), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
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
        [DataSourceCriteria("IsActive = 'True'")]
        [RuleRequiredField(DefaultContexts.Save)]
        [Index(77), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
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
        [RuleRequiredField(DefaultContexts.Save)]
        [Index(80), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
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
        [DataSourceCriteria("IsActive = 'True'")]
        [RuleRequiredField(DefaultContexts.Save)]
        [Index(82), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
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
        [RuleRequiredField(DefaultContexts.Save)]
        [Index(85), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
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
        [Index(87), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
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
        [DataSourceCriteria("IsActive = 'True'")]
        [RuleRequiredField(DefaultContexts.Save)]
        [Index(90), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        public Races DeliveryRace
        {
            get { return _DeliveryRace; }
            set
            {
                SetPropertyValue("DeliveryRace", ref _DeliveryRace, value);
            }
        }

        // Sales Rep
        private vwSalesRep _SalesRep1;
        [NoForeignKey]
        [ImmediatePostData]
        [DataSourceCriteria("Active = 'Y'")]
        [XafDisplayName("Sales Rep 1")]
        [RuleRequiredField(DefaultContexts.Save)]
        [LookupEditorMode(LookupEditorMode.AllItems)]
        [Index(93), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        public vwSalesRep SalesRep1
        {
            get { return _SalesRep1; }
            set
            {
                SetPropertyValue("SalesRep1", ref _SalesRep1, value);
            }
        }

        private vwSalesRep _SalesRep2;
        [NoForeignKey]
        [DataSourceCriteria("Active = 'Y'")]
        [XafDisplayName("Sales Rep 2")]
        [LookupEditorMode(LookupEditorMode.AllItems)]
        [Index(94), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        public vwSalesRep SalesRep2
        {
            get { return _SalesRep2; }
            set
            {
                SetPropertyValue("SalesRep2", ref _SalesRep2, value);
            }
        }

        private vwSalesRep _SalesRep3;
        [NoForeignKey]
        [DataSourceCriteria("Active = 'Y'")]
        [XafDisplayName("Sales Rep 3")]
        [LookupEditorMode(LookupEditorMode.AllItems)]
        [Index(95), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        public vwSalesRep SalesRep3
        {
            get { return _SalesRep3; }
            set
            {
                SetPropertyValue("SalesRep3", ref _SalesRep3, value);
            }
        }

        private vwSalesRep _SalesRep4;
        [NoForeignKey]
        [DataSourceCriteria("Active = 'Y'")]
        [XafDisplayName("Sales Rep 4")]
        [LookupEditorMode(LookupEditorMode.AllItems)]
        [Index(96), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        public vwSalesRep SalesRep4
        {
            get { return _SalesRep4; }
            set
            {
                SetPropertyValue("SalesRep4", ref _SalesRep4, value);
            }
        }

        //Payment
        private decimal _SubTotal;
        [ImmediatePostData]
        [DbType("numeric(19,6)")]
        [ModelDefault("DisplayFormat", "n2")]
        [ModelDefault("EditMask", "n2")]
        [Appearance("SubTotal", Enabled = false)]
        [Index(102), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
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
        [Index(105), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
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
        [Index(108), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
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
        [Index(110), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
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
        [Index(112), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
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
        [Index(115), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
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
        [Index(118), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        [XafDisplayName("Cash")]
        public decimal Cash
        {
            get
            {
                if (Session.IsObjectsSaving != true)
                {
                    decimal rtn = 0;
                    if (OGW12ORDR != null)
                        rtn += OGW12ORDR.Sum(p => p.CashAmount);

                    return rtn;
                }
                else
                {
                    return _Cash;
                }
            }
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
        [Index(120), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        [XafDisplayName("Credit Card")]
        public decimal CreditCard
        {
            get
            {
                if (Session.IsObjectsSaving != true)
                {
                    decimal rtn = 0;
                    if (OGW12ORDR != null)
                        rtn += OGW12ORDR.Sum(p => p.CreditCardAmount);

                    return rtn;
                }
                else
                {
                    return _CreditCard;
                }
            }
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
        [Index(122), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        [XafDisplayName("Voucher")]
        public decimal Voucher
        {
            get
            {
                if (Session.IsObjectsSaving != true)
                {
                    decimal rtn = 0;
                    if (OGW12ORDR != null)
                        rtn += OGW12ORDR.Sum(p => p.VoucherAmount);

                    return rtn;
                }
                else
                {
                    return _Voucher;
                }
            }
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
        [Index(125), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
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
        [Index(128), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
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
        [Index(130), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
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
        [Index(132), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
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
        [Index(135), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
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
        [Index(138), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        [XafDisplayName("Min. Due Balance")]
        public decimal MinDueBalance
        {
            get { return _MinDueBalance; }
            set
            {
                SetPropertyValue("MinDueBalance", ref _MinDueBalance, value);
            }
        }

        private vwStatus _PostStatus;
        [XafDisplayName("Post Status"), ToolTip("Post Status")]
        [Index(600), VisibleInListView(true), VisibleInDetailView(false), VisibleInLookupListView(false)]
        [Appearance("PostStatus", Enabled = false)]
        [NoForeignKey]
        public vwStatus PostStatus
        {
            get { return _PostStatus; }
            set { SetPropertyValue("PostStatus", ref _PostStatus, value); }
        }

        private string _ErrorDesc;
        [XafDisplayName("Error Desc."), ToolTip("Error Desc.")]
        [Index(610), VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(false)]
        [Appearance("ErrorDesc", Enabled = false)]
        [Size(SizeAttribute.Unlimited)]
        [DbType("nvarchar(MAX)")]
        public string ErrorDesc
        {
            get { return _ErrorDesc; }
            set { SetPropertyValue("ErrorDesc", ref _ErrorDesc, value); }
        }

        private bool _EditAndCancel;
        [Appearance("EditAndCancel", Enabled = false)]
        [Index(700), VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        [XafDisplayName("EditAndCancel")]
        public bool EditAndCancel
        {
            get { return _EditAndCancel; }
            set
            {
                SetPropertyValue("EditAndCancel", ref _EditAndCancel, value);
            }
        }

        private bool _ResumeOrder;
        [Appearance("ResumeOrder", Enabled = false)]
        [Index(701), VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        [XafDisplayName("ResumeOrder")]
        public bool ResumeOrder
        {
            get { return _ResumeOrder; }
            set
            {
                SetPropertyValue("ResumeOrder", ref _ResumeOrder, value);
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

        [Browsable(false)]
        public bool IsValid
        {
            get
            {
                if (this.Name == null || this.Address1 == null || this.City == null || this.District == null ||
                    this.PostCode == null || Country == null || this.MobilePhone == null || this.IdentityNo == null || this.Race == null ||
                    this.BillName == null || this.BillAddress1 == null || this.BillCity == null || this.BillDistrict == null ||
                    this.BillPostCode == null || this.BillCountry == null || this.BillMobilePhone == null || this.BillIdentityNo == null ||
                    this.BillRace == null || this.DeliveryContact == null || this.DeliveryAddress1 == null || this.DeliveryCountry == null ||
                    this.DeliveryMobilePhone == null || this.DeliveryRace == null || this.SalesRep1 == null)
                {
                    return true;
                }

                return false;
            }
        }

        [Association("OGW10ORDR-OGW11ORDR")]
        [XafDisplayName("Items")]
        [Appearance("OGW11ORDR", Enabled = false, Criteria = "IsNew")]
        public XPCollection<OGW11ORDR> OGW11ORDR
        {
            get { return GetCollection<OGW11ORDR>("OGW11ORDR"); }
        }

        [Association("OGW10ORDR-OGW12ORDR")]
        [XafDisplayName("Payment")]
        [Appearance("OGW12ORDR", Enabled = false, Criteria = "IsNew")]
        public XPCollection<OGW12ORDR> OGW12ORDR
        {
            get { return GetCollection<OGW12ORDR>("OGW12ORDR"); }
        }

        [Association("OGW10ORDR-OGW15ORDR")]
        [XafDisplayName("Document Status")]
        public XPCollection<OGW15ORDR> OGW15ORDR
        {
            get { return GetCollection<OGW15ORDR>("OGW15ORDR"); }
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