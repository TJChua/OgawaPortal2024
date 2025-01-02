using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;

namespace OgawaPortal.Module.BusinessObjects.Maintenance
{
    [XafDisplayName("Numbering by Date")]
    [Appearance("HideLink", AppearanceItemType.Action, "True", TargetItems = "Link", Visibility = ViewItemVisibility.Hide, Context = "Any")]
    [Appearance("HideUnlink", AppearanceItemType.Action, "True", TargetItems = "Unlink", Visibility = ViewItemVisibility.Hide, Context = "Any")]
    [Appearance("HideSave", AppearanceItemType.Action, "True", TargetItems = "Save", Visibility = ViewItemVisibility.Hide, Context = "Any")]
    [Appearance("HideSave&New", AppearanceItemType.Action, "True", TargetItems = "SaveAndNew", Visibility = ViewItemVisibility.Hide, Context = "Any")]
    [Appearance("HideValidate", AppearanceItemType.Action, "True", TargetItems = "ShowAllContexts", Visibility = ViewItemVisibility.Hide, Context = "Any")]
    public class OGW11NNM1 : XPObject
    { 
        public OGW11NNM1(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        private OGW10NNM1 _DocEntry;
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        [Association("OGW10NNM1-OGW11NNM1", typeof(OGW10NNM1))]
        public OGW10NNM1 DocEntry
        {
            get { return _DocEntry; }
            set { SetPropertyValue("DocEntry", ref _DocEntry, value); }
        }

        private int _DocYear;
        [XafDisplayName("Year"), ToolTip("Year")]
        [Index(10), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        [ModelDefault("DisplayFormat", "{0:d}")]
        [RuleRequiredField(DefaultContexts.Save)]
        public int DocYear
        {
            get { return _DocYear; }
            set { SetPropertyValue("DocYear", ref _DocYear, value); }
        }

        private int _DocMonth;
        [XafDisplayName("Month"), ToolTip("Month")]
        [Index(20), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        [ModelDefault("DisplayFormat", "{0:d}")]
        [RuleRequiredField(DefaultContexts.Save)]
        public int DocMonth
        {
            get { return _DocMonth; }
            set { SetPropertyValue("DocMonth", ref _DocMonth, value); }
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
    }
}