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

namespace OgawaPortal.Module.BusinessObjects.POS___Exchange
{
    [XafDisplayName("Exchange Out Details")]
    [Appearance("HideNew", AppearanceItemType.Action, "True", TargetItems = "New", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Context = "Any")]
    [Appearance("LinkDoc", AppearanceItemType = "Action", TargetItems = "Link", Context = "ListView", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
    [Appearance("UnlinkDoc", AppearanceItemType = "Action", TargetItems = "Unlink", Context = "ListView", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
    [Appearance("HideDelete", AppearanceItemType.Action, "True", TargetItems = "Delete", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Context = "Any")]
    public class OGW11EXCO : XPObject
    { 
        public OGW11EXCO(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            CreateUser = Session.GetObjectByKey<ApplicationUser>(SecuritySystem.CurrentUserId).UserName;
            CreateDate = DateTime.Now;
        }

        private OGW10ORDN _DocEntry;
        [Association("OGW10ORDN-OGW11ORDN")]
        [Index(0), VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        [Appearance("DocEntry", Enabled = false)]
        public OGW10ORDN DocEntry
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

        private int? _BaseEntry;
        [XafDisplayName("Base Entry"), ToolTip("Base Entry")]
        [Index(991), VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        [ModelDefault("DisplayFormat", "{0:d0}")]
        [Appearance("BaseEntry", Enabled = false)]
        [DbType("int")]
        public int? BaseEntry
        {
            get { return _BaseEntry; }
            set { SetPropertyValue("BaseEntry", ref _BaseEntry, value); }
        }

        private int? _BaseOid;
        [XafDisplayName("Base Oid"), ToolTip("Base Oid")]
        [Index(992), VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        [ModelDefault("DisplayFormat", "{0:d0}")]
        [Appearance("BaseOid", Enabled = false)]
        [DbType("int")]
        public int? BaseOid
        {
            get { return _BaseOid; }
            set { SetPropertyValue("BaseOid", ref _BaseOid, value); }
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