using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using OgawaPortal.Module.BusinessObjects.View;
using System;
using System.ComponentModel;

namespace OgawaPortal.Module.BusinessObjects.Maintenance
{
    [XafDisplayName("Document Numbering")]
    [DefaultProperty("SeriesName")]
    [NavigationItem("System Initialization")]
    [Appearance("HideLink", AppearanceItemType.Action, "True", TargetItems = "Link", Visibility = ViewItemVisibility.Hide, Context = "Any")]
    [Appearance("HideUnlink", AppearanceItemType.Action, "True", TargetItems = "Unlink", Visibility = ViewItemVisibility.Hide, Context = "Any")]
    [Appearance("HideSave&New", AppearanceItemType.Action, "True", TargetItems = "SaveAndNew", Visibility = ViewItemVisibility.Hide, Context = "Any")]
    [Appearance("HideSave&Close", AppearanceItemType.Action, "True", TargetItems = "SaveAndClose", Visibility = ViewItemVisibility.Hide, Context = "Any")]
    [Appearance("HideValidate", AppearanceItemType.Action, "True", TargetItems = "ShowAllContexts", Visibility = ViewItemVisibility.Hide, Context = "Any")]
    public class OGW10NNM1 : XPObject
    { 
        public OGW10NNM1(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            if (SecuritySystem.CurrentUserId != null && SecuritySystem.CurrentUserId.ToString() != "")
            {
                CreateBy = Session.GetObjectByKey<ApplicationUser>(SecuritySystem.CurrentUserId).UserName;
                UpdateBy = Session.GetObjectByKey<ApplicationUser>(SecuritySystem.CurrentUserId).UserName;
            }
            CreateDate = DateTime.Now;
            UpdateDate = DateTime.Now;
            Month = true;
            Year = true;
            IsActive = true;
        }

        private string _SeriesCode;
        [XafDisplayName("Series"), ToolTip("Series")]
        [Index(1), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        [RuleRequiredField(DefaultContexts.Save), RuleUniqueValue(DefaultContexts.Save)]
        public string SeriesCode
        {
            get { return _SeriesCode; }
            set { SetPropertyValue("SeriesCode", ref _SeriesCode, value); }
        }

        private string _SeriesName;
        [XafDisplayName("Series Name"), ToolTip("Series Name")]
        [Index(2), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        [RuleRequiredField(DefaultContexts.Save)]
        public string SeriesName
        {
            get { return _SeriesName; }
            set { SetPropertyValue("SeriesName", ref _SeriesName, value); }
        }

        private vwObjType _DocType;
        [XafDisplayName("Document Object Type"), ToolTip("Document Object Type")]
        [Index(10), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        [RuleRequiredField(DefaultContexts.Save)]
        [NoForeignKey]
        public vwObjType DocType
        {
            get { return _DocType; }
            set { SetPropertyValue("DocType", ref _DocType, value); }
        }

        private string _Prefix;
        [XafDisplayName("Prefix"), ToolTip("Prefix")]
        [Index(20), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        public string Prefix
        {
            get { return _Prefix; }
            set { SetPropertyValue("Prefix", ref _Prefix, value); }
        }

        private int _Seq;
        [XafDisplayName("Seq"), ToolTip("Seq")]
        [Index(30), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        [RuleRequiredField(DefaultContexts.Save)]
        public int Seq
        {
            get { return _Seq; }
            set { SetPropertyValue("Seq", ref _Seq, value); }
        }

        private string _Suffix;
        [XafDisplayName("Suffix"), ToolTip("Suffix")]
        [Index(40), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        public string Suffix
        {
            get { return _Suffix; }
            set { SetPropertyValue("Suffix", ref _Suffix, value); }
        }

        private int _Length;
        [XafDisplayName("Length"), ToolTip("Length")]
        [Index(50), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        [RuleRequiredField(DefaultContexts.Save)]
        public int Length
        {
            get { return _Length; }
            set { SetPropertyValue("Length", ref _Length, value); }
        }

        private bool _Month;
        [Index(60), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(false)]
        [XafDisplayName("Month"), ToolTip("Month")]
        public bool Month
        {
            get { return _Month; }
            set { SetPropertyValue("Month", ref _Month, value); }
        }

        private bool _Year;
        [Index(70), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(false)]
        [XafDisplayName("Year"), ToolTip("Year")]
        public bool Year
        {
            get { return _Year; }
            set { SetPropertyValue("Year", ref _Year, value); }
        }

        private bool _IsActive;
        [Index(90), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(false)]
        [XafDisplayName("Active"), ToolTip("Active")]
        public bool IsActive
        {
            get { return _IsActive; }
            set { SetPropertyValue("IsActive", ref _IsActive, value); }
        }

        private string _CreateBy;
        [XafDisplayName("Created By"), ToolTip("Create By")]
        [Index(9993), VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public string CreateBy
        {
            get { return _CreateBy; }
            set { SetPropertyValue("CreateBy", ref _CreateBy, value); }
        }

        private DateTime _CreateDate;
        [Index(9994), VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        [XafDisplayName("Created Date"), ToolTip("Created Date")]
        [ModelDefault("DisplayFormat", "dd/MM/yyyy")]
        [DbType("datetime")]
        public DateTime CreateDate
        {
            get { return _CreateDate; }
            set { SetPropertyValue("CreateDate", ref _CreateDate, value); }
        }

        private string _UpdateBy;
        [XafDisplayName("Updated By"), ToolTip("Updated By")]
        [Index(9996), VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public string UpdateBy
        {
            get { return _UpdateBy; }
            set { SetPropertyValue("UpdateBy", ref _UpdateBy, value); }
        }

        private DateTime _UpdateDate;
        [Index(9997), VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        [XafDisplayName("Updated Date"), ToolTip("Updated Date")]
        [ModelDefault("DisplayFormat", "dd/MM/yyyy")]
        [DbType("datetime")]
        public DateTime UpdateDate
        {
            get { return _UpdateDate; }
            set { SetPropertyValue("UpdateDate", ref _UpdateDate, value); }
        }

        [Browsable(false)]
        public bool IsNew
        {
            get { return Session.IsNewObject(this); }
        }

        [Association("OGW10NNM1-OGW11NNM1", typeof(OGW11NNM1))]
        [XafDisplayName("Document Numbering")]
        public XPCollection<OGW11NNM1> OGW11NNM1
        {
            get { return GetCollection<OGW11NNM1>("OGW11NNM1"); }
        }
    }
}