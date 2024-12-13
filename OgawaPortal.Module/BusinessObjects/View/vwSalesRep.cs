using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System.ComponentModel;

namespace OgawaPortal.Module.BusinessObjects.View
{
    [NavigationItem("SAP")]
    [XafDisplayName("Sales Person")]
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
    public class vwSalesRep : XPLiteObject
    { 
        public vwSalesRep(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        [Key]
        [Browsable(true)]
        [XafDisplayName("No")]
        [VisibleInLookupListView(true), VisibleInListView(true), VisibleInDetailView(true)]
        [Appearance("No", Enabled = false)]
        public string No { get; set; }

        [XafDisplayName("Name")]
        [VisibleInLookupListView(true), VisibleInListView(true), VisibleInDetailView(true)]
        [Appearance("Name", Enabled = false)]
        public string Name { get; set; }

        [XafDisplayName("Code")]
        [VisibleInLookupListView(true), VisibleInListView(true), VisibleInDetailView(true)]
        [Appearance("Code", Enabled = false)]
        public string Code { get; set; }

        [XafDisplayName("FullName")]
        [VisibleInLookupListView(true), VisibleInListView(true), VisibleInDetailView(true)]
        [Appearance("FullName", Enabled = false)]
        public string FullName { get; set; }

        [XafDisplayName("U_ContactNo")]
        [VisibleInLookupListView(true), VisibleInListView(true), VisibleInDetailView(true)]
        [Appearance("U_ContactNo", Enabled = false)]
        public string U_ContactNo { get; set; }

        [XafDisplayName("U_SEDivision")]
        [VisibleInLookupListView(true), VisibleInListView(true), VisibleInDetailView(true)]
        [Appearance("U_SEDivision", Enabled = false)]
        public string U_SEDivision { get; set; }

        [XafDisplayName("Active")]
        [VisibleInLookupListView(true), VisibleInListView(true), VisibleInDetailView(true)]
        [Appearance("Active", Enabled = false)]
        public string Active { get; set; }
    }
}