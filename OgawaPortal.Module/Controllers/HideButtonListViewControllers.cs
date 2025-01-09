using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Web.SystemModule;

namespace OgawaPortal.Module.Controllers
{
    public partial class HideButtonListViewControllers : ViewController<ListView>
    {
        ListViewController controller = null;
        string criteriaString = "";
        public HideButtonListViewControllers()
        {
            InitializeComponent();
        }
        protected override void OnActivated()
        {
            base.OnActivated();

            /* Disable Inline Edit button */
            switch (View.Id)
            {
                case "OGW10ORDR_ListView":
                case "OGW10ORDR_ListView_EditOrder":
                case "OGW10ORDR_ListView_RsmOrder":
                    controller = Frame.GetController<ListViewController>();
                    criteriaString = "Status.Code IN ('DRAFT','REOPEN')";
                    controller.EditAction.TargetObjectsCriteria = criteriaString;
                    controller.InlineEditAction.TargetObjectsCriteria = criteriaString;
                    break;
            }

        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
        }
        protected override void OnDeactivated()
        {
            controller = Frame.GetController<ListViewController>();
            controller.EditAction.TargetObjectsCriteria = null;
            controller.InlineEditAction.TargetObjectsCriteria = null;

            base.OnDeactivated();
        }
    }
}
