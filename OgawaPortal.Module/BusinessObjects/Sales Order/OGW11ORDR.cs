using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using OgawaPortal.Module.BusinessObjects.View;
using System;
using System.ComponentModel;

namespace OgawaPortal.Module.BusinessObjects.Sales_Order
{
    [XafDisplayName("Sales Details")]
    //[Appearance("HideNew", AppearanceItemType.Action, "True", TargetItems = "New", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Context = "Any")]
    [Appearance("LinkDoc", AppearanceItemType = "Action", TargetItems = "Link", Context = "ListView", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
    [Appearance("UnlinkDoc", AppearanceItemType = "Action", TargetItems = "Unlink", Context = "ListView", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
    [Appearance("HideDelete", AppearanceItemType.Action, "True", TargetItems = "Delete", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Context = "Any")]

    public class OGW11ORDR : XPObject
    { 
        public OGW11ORDR(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            CreateUser = Session.GetObjectByKey<ApplicationUser>(SecuritySystem.CurrentUserId).UserName;
            CreateDate = DateTime.Now;
        }

        private OGW10ORDR _DocEntry;
        [Association("OGW10ORDR-OGW11ORDR")]
        [Index(0), VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        [Appearance("DocEntry", Enabled = false)]
        public OGW10ORDR DocEntry
        {
            get { return _DocEntry; }
            set { SetPropertyValue("DocEntry", ref _DocEntry, value); }
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

        private string _Class;
        [XafDisplayName("Class")]
        [Appearance("Class", Enabled = false)]
        [Index(10), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        public string Class
        {
            get { return _Class; }
            set
            {
                SetPropertyValue("Class", ref _Class, value);
            }
        }

        private vwItemMasters _ItemCode;
        [NoForeignKey]
        [XafDisplayName("Item Code")]
        [Appearance("ItemCode", Enabled = false, Criteria = "Not IsNew")]
        [Index(13), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        public vwItemMasters ItemCode
        {
            get { return _ItemCode; }
            set
            {
                SetPropertyValue("ItemCode", ref _ItemCode, value);
                if (!IsLoading && value != null)
                {
                    ItemName = ItemCode.ItemName;
                    Class = ItemCode.Class;
                }
                else if (!IsLoading && value == null)
                {
                    ItemName = null;
                    Class = null;
                }
            }
        }

        private string _ItemFather;
        [XafDisplayName("Item Father")]
        [Appearance("ItemFather", Enabled = false)]
        [Index(15), VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public string ItemFather
        {
            get { return _ItemFather; }
            set
            {
                SetPropertyValue("ItemFather", ref _ItemFather, value);
            }
        }

        private string _ItemName;
        [XafDisplayName("Item Name")]
        [Appearance("ItemName", Enabled = false)]
        [Index(18), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        public string ItemName
        {
            get { return _ItemName; }
            set
            {
                SetPropertyValue("ItemName", ref _ItemName, value);
            }
        }

        private decimal _RevisedSellingPrice;
        [ImmediatePostData]
        [DbType("numeric(19,6)")]
        [ModelDefault("DisplayFormat", "n2")]
        [ModelDefault("EditMask", "n2")]
        [Index(20), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        [XafDisplayName("Revised Selling Price")]
        public decimal RevisedSellingPrice
        {
            get { return _RevisedSellingPrice; }
            set
            {
                SetPropertyValue("RevisedSellingPrice", ref _RevisedSellingPrice, value);
            }
        }

        private decimal _UnitPrice;
        [ImmediatePostData]
        [DbType("numeric(19,6)")]
        [ModelDefault("DisplayFormat", "n2")]
        [ModelDefault("EditMask", "n2")]
        [Index(23), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        [XafDisplayName("Unit Price")]
        public decimal UnitPrice
        {
            get { return _UnitPrice; }
            set
            {
                SetPropertyValue("UnitPrice", ref _UnitPrice, value);
                if (!IsLoading && value != 0)
                {
                    Amount = UnitPrice * Order;
                }
            }
        }

        private decimal _Order;
        [ImmediatePostData]
        [Index(25), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        [DbType("numeric(19,6)")]
        [ModelDefault("DisplayFormat", "n2")]
        [ModelDefault("EditMask", "n2")]
        [XafDisplayName("Order")]
        public decimal Order
        {
            get { return _Order; }
            set
            {
                SetPropertyValue("Order", ref _Order, value);
                if (!IsLoading && value != 0)
                {
                    Amount = UnitPrice * Order;
                }
            }
        }

        private decimal _Taken;
        [ImmediatePostData]
        [Index(28), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        [DbType("numeric(19,6)")]
        [ModelDefault("DisplayFormat", "n2")]
        [ModelDefault("EditMask", "n2")]
        [XafDisplayName("Taken")]
        public decimal Taken
        {
            get { return _Taken; }
            set
            {
                SetPropertyValue("Taken", ref _Taken, value);
                if (!IsLoading && value != 0)
                {
                    BackOrder = Order - Taken;
                }
            }
        }

        private decimal _BackOrder;
        [ImmediatePostData]
        [Index(30), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        [DbType("numeric(19,6)")]
        [ModelDefault("DisplayFormat", "n2")]
        [ModelDefault("EditMask", "n2")]
        [XafDisplayName("Back Order")]
        public decimal BackOrder
        {
            get { return _BackOrder; }
            set
            {
                SetPropertyValue("BackOrder", ref _BackOrder, value);
            }
        }

        private decimal _Amount;
        [ImmediatePostData]
        [DbType("numeric(19,6)")]
        [ModelDefault("DisplayFormat", "n2")]
        [ModelDefault("EditMask", "n2")]
        [Appearance("Amount", Enabled = false)]
        [Index(33), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        [XafDisplayName("Amount")]
        public decimal Amount
        {
            get { return _Amount; }
            set
            {
                SetPropertyValue("Amount", ref _Amount, value);
            }
        }

        private string _SeriesNumber;
        [XafDisplayName("Series Number")]
        [Index(35), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        public string SeriesNumber
        {
            get { return _SeriesNumber; }
            set
            {
                SetPropertyValue("SeriesNumber", ref _SeriesNumber, value);
            }
        }

        private string _Remarks;
        [XafDisplayName("Remarks")]
        [Index(38), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        public string Remarks
        {
            get { return _Remarks; }
            set
            {
                SetPropertyValue("Remarks", ref _Remarks, value);
            }
        }

        private string _FatherKey;
        [XafDisplayName("FatherKey")]
        [Appearance("FatherKey", Enabled = false)]
        [Index(80), VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public string FatherKey
        {
            get { return _FatherKey; }
            set
            {
                SetPropertyValue("FatherKey", ref _FatherKey, value);
            }
        }

        [Browsable(false)]
        public bool IsNew
        {
            get
            { return Session.IsNewObject(this); }
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

                if (Session.IsNewObject(this))
                {

                }
            }
        }
    }
}