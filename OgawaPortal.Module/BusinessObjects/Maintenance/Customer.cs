using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace OgawaPortal.Module.BusinessObjects.Maintenance
{
    [NavigationItem("Maintenance")]
    [XafDisplayName("Customer")]
    [DefaultProperty("BoFullName")]
    [Appearance("HideDelete", AppearanceItemType.Action, "True", TargetItems = "Delete", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Context = "Any")]
    [Appearance("HideLink", AppearanceItemType.Action, "True", TargetItems = "Link", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Context = "Any")]
    [Appearance("HideUnlink", AppearanceItemType.Action, "True", TargetItems = "Unlink", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Context = "Any")]
    //[Appearance("HideResetViewSetting", AppearanceItemType.Action, "True", TargetItems = "ResetViewSettings", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Context = "Any")]
    [Appearance("HideExport", AppearanceItemType.Action, "True", TargetItems = "Export", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Context = "Any")]
    //[Appearance("HideRefresh", AppearanceItemType.Action, "True", TargetItems = "Refresh", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Context = "Any")]
    public class Customer : XPObject
    {
        public Customer(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();

            CreateUser = Session.GetObjectByKey<ApplicationUser>(SecuritySystem.CurrentUserId).UserName;
            CreateDate = DateTime.Now;
            IsActive = true;
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

        private string _Code;
        [XafDisplayName("Code")]
        [Appearance("Code", Enabled = false, Criteria = "not IsNew")]
        [Index(0), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        [RuleRequiredField(DefaultContexts.Save)]
        public string Code
        {
            get { return _Code; }
            set
            {
                SetPropertyValue("Code", ref _Code, value);
            }
        }

        private string _Name;
        [XafDisplayName("Name")]
        [Index(5), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        [RuleRequiredField(DefaultContexts.Save)]
        public string Name
        {
            get { return _Name; }
            set
            {
                SetPropertyValue("Name", ref _Name, value);
            }
        }

        private string _Address1;
        [XafDisplayName("Address1")]
        [Index(8), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
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
        [Index(10), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        public string Address2
        {
            get { return _Address2; }
            set
            {
                SetPropertyValue("Address2", ref _Address2, value);
            }
        }

        private District _District;
        [XafDisplayName("District")]
        [Index(13), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
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
        [Index(15), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        public string PostCode
        {
            get { return _PostCode; }
            set
            {
                SetPropertyValue("PostCode", ref _PostCode, value);
            }
        }

        private string _City;
        [XafDisplayName("City")]
        [Index(18), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        public string City
        {
            get { return _City; }
            set
            {
                SetPropertyValue("City", ref _City, value);
            }
        }

        private Country _Country;
        [XafDisplayName("Country")]
        [Index(20), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
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
        [ModelDefault("EditMask", "000-00000000")]
        [ModelDefault("DisplayFormat", "000-00000000")]
        [Index(23), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
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
        [ModelDefault("EditMask", "00-00000000")]
        [ModelDefault("DisplayFormat", "00-00000000")]
        [Index(25), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
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
        [Index(28), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        public string Email
        {
            get { return _Email; }
            set
            {
                SetPropertyValue("Email", ref _Email, value);
            }
        }

        private string _IC;
        [XafDisplayName("IC")]
        [Index(30), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        public string IC
        {
            get { return _IC; }
            set
            {
                SetPropertyValue("IC", ref _IC, value);
            }
        }

        private Races _Race;
        [XafDisplayName("Race")]
        [Index(33), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        public Races Race
        {
            get { return _Race; }
            set
            {
                SetPropertyValue("Race", ref _Race, value);
            }
        }

        private bool _IsActive;
        [XafDisplayName("IsActive")]
        [Index(50), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        public bool IsActive
        {
            get { return _IsActive; }
            set
            {
                SetPropertyValue("IsActive", ref _IsActive, value);
            }
        }

        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(true)]
        public string BoFullName
        {
            get { return Name + "-Mobile: " + MobilePhone; }
        }

        [Browsable(false)]
        public bool IsNew
        {
            get
            { return Session.IsNewObject(this); }
        }

        [RuleFromBoolProperty(nameof(IsCustEmailValid), DefaultContexts.Save, CustomMessageTemplate = "Invalid email format!", UsedProperties = nameof(Email))]
        [Browsable(false)]
        public bool IsCustEmailValid
        {
            get
            {
                System.ComponentModel.DataAnnotations.EmailAddressAttribute emailAddressValidator = new System.ComponentModel.DataAnnotations.EmailAddressAttribute();
                return emailAddressValidator.IsValid(Email);
            }
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
    }
}