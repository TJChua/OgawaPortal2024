using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.SystemModule;
using OgawaPortal.Module.BusinessObjects;
using OgawaPortal.Module.BusinessObjects.Nonpersistent;
using OgawaPortal.Module.BusinessObjects.View;
using OgawaPortal.Module.BusinessObjects.Maintenance;

namespace OgawaPortal.Module.Controllers
{
    public partial class NavigationControllers : WindowController
    {
        public NavigationControllers()
        {
            InitializeComponent();
        }
        protected override void OnActivated()
        {
            base.OnActivated();

            ShowNavigationItemController showNavigationItemController = Frame.GetController<ShowNavigationItemController>();
            showNavigationItemController.CustomShowNavigationItem += showNavigationItemController_CustomShowNavigationItem;
        }
        protected override void OnDeactivated()
        {
            base.OnDeactivated();
        }

        void showNavigationItemController_CustomShowNavigationItem(object sender, CustomShowNavigationItemEventArgs e)
        {
            if (e.ActionArguments.SelectedChoiceActionItem.Id == "Discount_ListView")
            {
                IObjectSpace objectSpace = Application.CreateObjectSpace(typeof(Discount));
                Discount screen = objectSpace.FindObject<Discount>(new BinaryOperator("Oid", 1));

                if (screen != null)
                {
                    DetailView detailView = Application.CreateDetailView(objectSpace, screen);
                    detailView.ViewEditMode = DevExpress.ExpressApp.Editors.ViewEditMode.Edit;
                    e.ActionArguments.ShowViewParameters.CreatedView = detailView;
                }
                else
                {
                    Discount newrecord = objectSpace.CreateObject<Discount>();

                    DetailView detailView = Application.CreateDetailView(objectSpace, newrecord);
                    detailView.ViewEditMode = DevExpress.ExpressApp.Editors.ViewEditMode.Edit;
                    e.ActionArguments.ShowViewParameters.CreatedView = detailView;

                    e.Handled = true;
                }
            }

            if (e.ActionArguments.SelectedChoiceActionItem.Id == "DeliveryDateControl_ListView")
            {
                IObjectSpace objectSpace = Application.CreateObjectSpace(typeof(DeliveryDateControl));
                DeliveryDateControl screen = objectSpace.FindObject<DeliveryDateControl>(new BinaryOperator("Oid", 1));

                if (screen != null)
                {
                    DetailView detailView = Application.CreateDetailView(objectSpace, screen);
                    detailView.ViewEditMode = DevExpress.ExpressApp.Editors.ViewEditMode.Edit;
                    e.ActionArguments.ShowViewParameters.CreatedView = detailView;
                }
                else
                {
                    DeliveryDateControl newrecord = objectSpace.CreateObject<DeliveryDateControl>();

                    DetailView detailView = Application.CreateDetailView(objectSpace, newrecord);
                    detailView.ViewEditMode = DevExpress.ExpressApp.Editors.ViewEditMode.Edit;
                    e.ActionArguments.ShowViewParameters.CreatedView = detailView;

                    e.Handled = true;
                }
            }

            if (e.ActionArguments.SelectedChoiceActionItem.Id == "GSTDates_ListView")
            {
                IObjectSpace objectSpace = Application.CreateObjectSpace(typeof(GSTDates));
                GSTDates screen = objectSpace.FindObject<GSTDates>(new BinaryOperator("Oid", 1));

                if (screen != null)
                {
                    DetailView detailView = Application.CreateDetailView(objectSpace, screen);
                    detailView.ViewEditMode = DevExpress.ExpressApp.Editors.ViewEditMode.Edit;
                    e.ActionArguments.ShowViewParameters.CreatedView = detailView;
                }
                else
                {
                    GSTDates newrecord = objectSpace.CreateObject<GSTDates>();

                    DetailView detailView = Application.CreateDetailView(objectSpace, newrecord);
                    detailView.ViewEditMode = DevExpress.ExpressApp.Editors.ViewEditMode.Edit;
                    e.ActionArguments.ShowViewParameters.CreatedView = detailView;

                    e.Handled = true;
                }
            }

            if (e.ActionArguments.SelectedChoiceActionItem.Id == "OGW10ChgOutlet_ListView")
            {
                IObjectSpace objectSpace = Application.CreateObjectSpace();
                OGW10ChgOutlet screen = objectSpace.CreateObject<OGW10ChgOutlet>();

                ApplicationUser user = (ApplicationUser)SecuritySystem.CurrentUser;

                DetailView detailView = Application.CreateDetailView(objectSpace, screen);
                detailView.ViewEditMode = DevExpress.ExpressApp.Editors.ViewEditMode.Edit;

                if (user.Outlet != null)
                {
                    ((OGW10ChgOutlet)detailView.CurrentObject).Outlet = ((OGW10ChgOutlet)detailView.CurrentObject).Session.GetObjectByKey<vwOutlets>
                        (user.Outlet.CardCode);
                }

                e.ActionArguments.ShowViewParameters.CreatedView = detailView;

                e.Handled = true;
            }
        }
    }
}
