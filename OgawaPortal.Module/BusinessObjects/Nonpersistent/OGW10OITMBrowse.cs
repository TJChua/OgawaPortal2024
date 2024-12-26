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

namespace OgawaPortal.Module.BusinessObjects.Nonpersistent
{
    [DomainComponent]
    [NonPersistent]
    [Appearance("HideNew", AppearanceItemType.Action, "True", TargetItems = "New", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Context = "Any")]
    [Appearance("HideDelete", AppearanceItemType.Action, "True", TargetItems = "Delete", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Context = "Any")]
    [XafDisplayName("Item List")]
    public class ItemCodes
    {
        [Browsable(false), DevExpress.ExpressApp.Data.Key]
        public int Id;

        [XafDisplayName("Class")]
        [Index(0), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        [Appearance("Class", Enabled = false)]
        public string Class { get; set; }

        [XafDisplayName("Item Code")]
        [Index(1), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        [Appearance("ItemCode", Enabled = false)]
        public string ItemCode { get; set; }

        [XafDisplayName("Item Name")]
        [Index(2), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        [Appearance("ItemName", Enabled = false)]
        public string ItemName { get; set; }

        [XafDisplayName("New/Demo")]
        [Index(3), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        [Appearance("NewOrDemo", Enabled = false)]
        public string NewOrDemo { get; set; }

        [XafDisplayName("Price")]
        [Index(4), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        [Appearance("Price", Enabled = false)]
        [DbType("numeric(19,6)")]
        [ModelDefault("DisplayFormat", "n2")]
        [ModelDefault("EditMask", "n2")]
        public decimal Price { get; set; }
    }

    [DomainComponent]
    [NonPersistent]
    [Appearance("HideNew", AppearanceItemType.Action, "True", TargetItems = "New", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Context = "Any")]
    [XafDisplayName("Carts")]
    public class ItemCarts
    {
        [Browsable(false), DevExpress.ExpressApp.Data.Key]
        public int Id;

        [XafDisplayName("Class")]
        [Index(0), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        [Appearance("Class", Enabled = false)]
        public string Class { get; set; }

        [XafDisplayName("Item Code")]
        [Index(1), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        [Appearance("ItemCode", Enabled = false)]
        public string ItemCode { get; set; }

        [XafDisplayName("Item Name")]
        [Index(2), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        [Appearance("ItemName", Enabled = false)]
        public string ItemName { get; set; }

        [XafDisplayName("New/Demo")]
        [Index(3), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        [Appearance("NewOrDemo", Enabled = false)]
        public string NewOrDemo { get; set; }

        [XafDisplayName("Price")]
        [Index(4), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        [Appearance("Price", Enabled = false)]
        [DbType("numeric(19,6)")]
        [ModelDefault("DisplayFormat", "n2")]
        [ModelDefault("EditMask", "n2")]
        public decimal Price { get; set; }

        [XafDisplayName("Quantity")]
        [Index(5), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        [DbType("numeric(19,6)")]
        [ModelDefault("DisplayFormat", "n2")]
        [ModelDefault("EditMask", "n2")]
        public decimal Quantity { get; set; }
    }

    [DomainComponent]
    [NonPersistent]
    [Appearance("HideNew", AppearanceItemType.Action, "True", TargetItems = "New", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Context = "Any")]
    [Appearance("HideDelete", AppearanceItemType.Action, "True", TargetItems = "Delete", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Context = "Any")]
    [XafDisplayName("Item Browser")]
    public class ItemBrowser
    {
        private BindingList<ItemCodes> _ItemCodes;
        private BindingList<ItemCarts> _ItemCarts;
        public ItemBrowser()
        {
            _ItemCodes = new BindingList<ItemCodes>();
            _ItemCarts = new BindingList<ItemCarts>();
        }

        [XafDisplayName("Item List")]
        public BindingList<ItemCodes> itemcodes { get { return _ItemCodes; } }

        [XafDisplayName("Carts")]
        public BindingList<ItemCarts> itemcarts { get { return _ItemCarts; } }
    }
}