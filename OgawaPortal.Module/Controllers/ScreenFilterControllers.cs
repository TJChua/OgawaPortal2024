using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using OgawaPortal.Module.BusinessObjects;
using OgawaPortal.Module.BusinessObjects.Sales_Order;

namespace OgawaPortal.Module.Controllers
{
    public partial class ScreenFilterControllers : ViewController
    {
        public ScreenFilterControllers()
        {
            InitializeComponent();
        }
        protected override void OnActivated()
        {
            base.OnActivated();

            ApplicationUser user = (ApplicationUser)SecuritySystem.CurrentUser;

            if (View.ObjectTypeInfo.Type == typeof(OGW10ORDR))
            {
                if (View.Id == "OGW10ORDR_ListView_RsmOrder")
                {
                    ((ListView)View).CollectionSource.Criteria["Filter1"] = CriteriaOperator.Parse("Status.Code IN (?, ?)",
                        "OPEN", "REOPEN");
                }

                if (View.Id == "OGW10ORDR_ListView_EditOrder")
                {
                    ((ListView)View).CollectionSource.Criteria["Filter1"] = CriteriaOperator.Parse("EditAndCancel = 1");
                }
            }

        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
        }
        protected override void OnDeactivated()
        {
            base.OnDeactivated();
        }
    }
}
