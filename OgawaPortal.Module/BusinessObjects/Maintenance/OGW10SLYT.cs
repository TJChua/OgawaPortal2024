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
    [XafDisplayName("Layouts")]
    [DefaultProperty("Name")]
    [NavigationItem("Maintenance")]
    [Appearance("HideLink", AppearanceItemType.Action, "True", TargetItems = "Link", Visibility = ViewItemVisibility.Hide, Context = "Any")]
    [Appearance("HideUnlink", AppearanceItemType.Action, "True", TargetItems = "Unlink", Visibility = ViewItemVisibility.Hide, Context = "Any")]
    [Appearance("HideSave&New", AppearanceItemType.Action, "True", TargetItems = "SaveAndNew", Visibility = ViewItemVisibility.Hide, Context = "Any")]
    [Appearance("HideSave&Close", AppearanceItemType.Action, "True", TargetItems = "SaveAndClose", Visibility = ViewItemVisibility.Hide, Context = "Any")]
    [Appearance("HideValidate", AppearanceItemType.Action, "True", TargetItems = "ShowAllContexts", Visibility = ViewItemVisibility.Hide, Context = "Any")]
    public class OGW10SLYT : XPObject
    { 
        public OGW10SLYT(Session session)
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
            IsActive = true;
        }

        private string _Name;
        [XafDisplayName("Name"), ToolTip("Name")]
        [Index(10), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        [RuleRequiredField(DefaultContexts.Save)]
        public string Name
        {
            get { return _Name; }
            set { SetPropertyValue("Name", ref _Name, value); }
        }

        private string _FilePath;
        [XafDisplayName("CR Layout File Path"), ToolTip("CR Layout File Path")]
        [Index(20), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        [RuleRequiredField(DefaultContexts.Save)]
        public string FilePath
        {
            get { return _FilePath; }
            set { SetPropertyValue("FilePath", ref _FilePath, value); }
        }

        private vwObjType _ObjDoc;
        [XafDisplayName("Document"), ToolTip("Document")]
        [Index(30), VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        [NoForeignKey]
        [RuleRequiredField(DefaultContexts.Save)]
        public vwObjType ObjDoc
        {
            get { return _ObjDoc; }
            set { SetPropertyValue("ObjDoc", ref _ObjDoc, value); }
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
        [XafDisplayName("Create By"), ToolTip("Create By")]
        [Index(9993), VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public string CreateBy
        {
            get { return _CreateBy; }
            set { SetPropertyValue("CreateBy", ref _CreateBy, value); }
        }

        private DateTime _CreateDate;
        [Index(9994), VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        [XafDisplayName("Create Date")]
        [ModelDefault("DisplayFormat", "dd/MM/yyyy")]
        [DbType("datetime")]
        public DateTime CreateDate
        {
            get { return _CreateDate; }
            set { SetPropertyValue("CreateDate", ref _CreateDate, value); }
        }

        private string _UpdateBy;
        [XafDisplayName("Update By"), ToolTip("Update By")]
        [Index(9996), VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public string UpdateBy
        {
            get { return _UpdateBy; }
            set { SetPropertyValue("UpdateBy", ref _UpdateBy, value); }
        }

        private DateTime _UpdateDate;
        [Index(9997), VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        [XafDisplayName("Update Date")]
        [ModelDefault("DisplayFormat", "dd/MM/yyyy")]
        [DbType("datetime")]
        public DateTime UpdateDate
        {
            get { return _UpdateDate; }
            set { SetPropertyValue("UpdateDate", ref _UpdateDate, value); }
        }

        private string _ObjType;
        [XafDisplayName("Object Type"), ToolTip("Object Type")]
        [Index(9999), VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public string ObjType
        {
            get { return _ObjType; }
            set { SetPropertyValue("ObjType", ref _ObjType, value); }
        }

        [Browsable(false)]
        public bool IsNew
        {
            get { return Session.IsNewObject(this); }
        }
    }
}