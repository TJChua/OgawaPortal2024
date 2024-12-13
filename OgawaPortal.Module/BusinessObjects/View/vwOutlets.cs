using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System.ComponentModel;

namespace OgawaPortal.Module.BusinessObjects.View
{
    [NavigationItem("SAP")]
    [XafDisplayName("Outlet")]
    [DefaultProperty("FullName")]
    [Appearance("HideNew", AppearanceItemType.Action, "True", TargetItems = "New", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Context = "Any")]
    [Appearance("HideEdit", AppearanceItemType.Action, "True", TargetItems = "SwitchToEditMode; Edit", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Context = "Any")]
    [Appearance("HideDelete", AppearanceItemType.Action, "True", TargetItems = "Delete", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Context = "Any")]
    [Appearance("HideLink", AppearanceItemType.Action, "True", TargetItems = "Link", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Context = "Any")]
    [Appearance("HideUnlink", AppearanceItemType.Action, "True", TargetItems = "Unlink", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Context = "Any")]
    [Appearance("hideSave", AppearanceItemType = "Action", TargetItems = "Save", Context = "Any", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
    //[Appearance("HideResetViewSetting", AppearanceItemType.Action, "True", TargetItems = "ResetViewSettings", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Context = "Any")]
    //[Appearance("HideExport", AppearanceItemType.Action, "True", TargetItems = "Export", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Context = "Any")]
    [Appearance("HideRefresh", AppearanceItemType.Action, "True", TargetItems = "Refresh", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Context = "Any")]
    public class vwOutlets : XPLiteObject
    { 
        public vwOutlets(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        [Key]
        [Browsable(true)]
        [XafDisplayName("CardCode")]
        [VisibleInLookupListView(true), VisibleInListView(true), VisibleInDetailView(true)]
        [Appearance("CardCode", Enabled = false)]
        public string CardCode { get; set; }

        [XafDisplayName("CardName")]
        [VisibleInLookupListView(true), VisibleInListView(true), VisibleInDetailView(true)]
        [Appearance("CardName", Enabled = false)]
        public string CardName { get; set; }

        [XafDisplayName("FullName")]
        [VisibleInLookupListView(true), VisibleInListView(true), VisibleInDetailView(true)]
        [Appearance("FullName", Enabled = false)]
        public string FullName { get; set; }

        [XafDisplayName("GroupName")]
        [VisibleInLookupListView(true), VisibleInListView(true), VisibleInDetailView(true)]
        [Appearance("GroupName", Enabled = false)]
        public string GroupName { get; set; }

        [XafDisplayName("GroupCode")]
        [VisibleInLookupListView(true), VisibleInListView(true), VisibleInDetailView(true)]
        [Appearance("GroupCode", Enabled = false)]
        public string GroupCode { get; set; }
    }
}