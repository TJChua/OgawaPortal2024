using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Editors;
using DevExpress.Persistent.Base;
using OgawaPortal.Module.BusinessObjects.POS___Logistic;
using System.ComponentModel;

namespace OgawaPortal.Module.BusinessObjects.Copy_Screen
{
    [DomainComponent]
    [XafDisplayName("Copy From Delivery Request")]
    [DefaultProperty("Oid")]
    [Appearance("HideNew", AppearanceItemType.Action, "True", TargetItems = "New", Visibility = ViewItemVisibility.Hide, Context = "Any")]
    [Appearance("HideDelete", AppearanceItemType.Action, "True", TargetItems = "Delete", Visibility = ViewItemVisibility.Hide, Context = "Any")]
    [Appearance("HideEdit", AppearanceItemType.Action, "True", TargetItems = "SwitchToEditMode; Edit", Visibility = ViewItemVisibility.Hide, Context = "Any")]
    [Appearance("HideLink", AppearanceItemType.Action, "True", TargetItems = "Link", Visibility = ViewItemVisibility.Hide, Context = "Any")]
    [Appearance("HideUnlink", AppearanceItemType.Action, "True", TargetItems = "Unlink", Visibility = ViewItemVisibility.Hide, Context = "Any")]
    [Appearance("HideSave", AppearanceItemType.Action, "True", TargetItems = "Save", Visibility = ViewItemVisibility.Hide, Context = "Any")]
    [Appearance("HideSave&New", AppearanceItemType.Action, "True", TargetItems = "SaveAndNew", Visibility = ViewItemVisibility.Hide, Context = "Any")]
    [Appearance("HideValidate", AppearanceItemType.Action, "True", TargetItems = "ShowAllContexts", Visibility = ViewItemVisibility.Hide, Context = "Any")]
    public class CopyList_OGW11DREQ
    {
        [DevExpress.ExpressApp.Data.Key, Browsable(false)]
        public int Oid;

        [XafDisplayName("OGW10DREQ"), ToolTip("OGW10DREQ")]
        [Index(10), VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        [Appearance("OGW10DREQ", Enabled = false)]
        public OGW10DREQ Header { get; set; }

        [XafDisplayName("OGW11DREQ"), ToolTip("OGW11DREQ")]
        [Index(20), VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        [Appearance("OGW11DREQ", Enabled = false)]
        public OGW11DREQ Details { get; set; }
    }
}