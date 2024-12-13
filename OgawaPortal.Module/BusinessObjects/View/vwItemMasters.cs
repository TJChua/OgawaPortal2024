using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System.ComponentModel;

namespace OgawaPortal.Module.BusinessObjects.View
{
    [NavigationItem("SAP")]
    [XafDisplayName("Item")]
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
    public class vwItemMasters : XPLiteObject
    { 
        public vwItemMasters(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        [Key]
        [Browsable(true)]
        [XafDisplayName("Item Code")]
        [VisibleInLookupListView(true), VisibleInListView(true), VisibleInDetailView(true)]
        [Appearance("ItemCode", Enabled = false)]
        public string ItemCode { get; set; }

        [XafDisplayName("Item Name")]
        [VisibleInLookupListView(true), VisibleInListView(true), VisibleInDetailView(true)]
        [Appearance("ItemName", Enabled = false)]
        public string ItemName { get; set; }

        [XafDisplayName("FullName")]
        [VisibleInLookupListView(true), VisibleInListView(true), VisibleInDetailView(true)]
        [Appearance("FullName", Enabled = false)]
        public string FullName { get; set; }

        [XafDisplayName("Class")]
        [VisibleInLookupListView(true), VisibleInListView(true), VisibleInDetailView(true)]
        [Appearance("Class", Enabled = false)]
        public string Class { get; set; }

        [XafDisplayName("NewDemo")]
        [VisibleInLookupListView(true), VisibleInListView(true), VisibleInDetailView(true)]
        [Appearance("NewDemo", Enabled = false)]
        public string NewDemo { get; set; }

        [XafDisplayName("frozenFor")]
        [VisibleInLookupListView(true), VisibleInListView(true), VisibleInDetailView(true)]
        [Appearance("frozenFor", Enabled = false)]
        public string frozenFor { get; set; }
    }
}