using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Web.Editors.ASPx;
using DevExpress.Web;
using OgawaPortal.Module.BusinessObjects;
using OgawaPortal.Module.BusinessObjects.Nonpersistent;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OgawaPortal.Module.Controllers
{
    public partial class NonpersistentControllers : ViewController
    {
        GeneralControllers genCon;
        public NonpersistentControllers()
        {
            InitializeComponent();
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            this.AddCart.Active.SetItemValue("Enabled", false);
            this.ClearCart.Active.SetItemValue("Enabled", false);

            ApplicationUser user = (ApplicationUser)SecuritySystem.CurrentUser;
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();

            genCon = Frame.GetController<GeneralControllers>();

            ApplicationUser user = (ApplicationUser)SecuritySystem.CurrentUser;

            /* ItemCodes Browser */
            if (View.ObjectTypeInfo.Type == typeof(ItemBrowser))
            {
                if (View.Id == "ItemBrowser_DetailView")
                {
                    if (((DetailView)View).ViewEditMode == ViewEditMode.Edit)
                    {
                        this.AddCart.Active.SetItemValue("Enabled", true);
                        this.ClearCart.Active.SetItemValue("Enabled", true);
                    }
                    else
                    {
                        this.AddCart.Active.SetItemValue("Enabled", false);
                        this.ClearCart.Active.SetItemValue("Enabled", false);
                    }
                }
            }
            else
            {
                this.AddCart.Active.SetItemValue("Enabled", false);
                this.ClearCart.Active.SetItemValue("Enabled", false);
            }
        }
        protected override void OnDeactivated()
        {
            base.OnDeactivated();
        }

        private void AddCart_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            #region ItemCodes Browser
            if (View.ObjectTypeInfo.Type == typeof(ItemBrowser))
            {
                ItemBrowser browser = (ItemBrowser)View.CurrentObject;
                ListPropertyEditor details = null;
                IEnumerable<object> list = null;

                details = ((DetailView)View).FindItem("itemcodes") as ListPropertyEditor;

                list = details.ListView.SelectedObjects.Count > 0 ? details.ListView.SelectedObjects.Cast<object>() : details.ListView.CollectionSource.List.Cast<object>();

                foreach (ItemCodes dtl in list)
                {
                    ItemCarts carts = ObjectSpace.CreateObject<ItemCarts>();
                    carts.Id = browser.itemcarts.Count == 0 ? 1 : browser.itemcarts.Max(pp => pp.Id) + 1;
                    carts.Class = dtl.Class;
                    carts.ItemCode = dtl.ItemCode;
                    carts.ItemName = dtl.ItemName;
                    carts.NewOrDemo = dtl.NewOrDemo;
                    carts.Price = dtl.Price;
                    carts.Quantity = 1;

                    browser.itemcarts.Add(carts);
                }

                ObjectSpace.CommitChanges();
                ObjectSpace.Refresh();

                if (details != null)
                {
                    ((IFrameContainer)details).InitializeFrame();
                    if (details.ListView != null && details.ListView.Editor != null)
                    {
                        ASPxGridListEditor editor = details.ListView.Editor as ASPxGridListEditor;
                        if (editor != null)
                        {
                            if (editor.Control != null)
                            {
                                genCon.ResetGridSelectionCore(editor.Control);
                            }
                        }
                    }
                }
            }
            #endregion
        }

        private void ClearCart_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            #region ItemCodes Browser
            if (View.ObjectTypeInfo.Type == typeof(ItemBrowser))
            {
                ItemBrowser browser = (ItemBrowser)View.CurrentObject;

                browser.itemcarts.Clear();

                ObjectSpace.CommitChanges();
                ObjectSpace.Refresh();
            }
            #endregion
        }
    }
}
