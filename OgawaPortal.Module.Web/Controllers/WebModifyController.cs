using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.ExpressApp.Web.SystemModule;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using OgawaPortal.Module.BusinessObjects;
using OgawaPortal.Module.BusinessObjects.Maintenance;
using OgawaPortal.Module.BusinessObjects.Sales_Order;
using OgawaPortal.Module.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OgawaPortal.Module.Web.Controllers
{
    public partial class WebModifyController : WebModificationsController
    {
        GeneralControllers genCon;
        public WebModifyController()
        {
            InitializeComponent();
        }
        protected override void OnActivated()
        {
            base.OnActivated();
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();

            genCon = Frame.GetController<GeneralControllers>();
        }
        protected override void OnDeactivated()
        {
            base.OnDeactivated();
        }

        protected override void Save(SimpleActionExecuteEventArgs args)
        {
            string query = "";
            ApplicationUser user = (ApplicationUser)SecuritySystem.CurrentUser;

            #region OGW10ORDR
            if (View.ObjectTypeInfo.Type == typeof(OGW10ORDR))
            {
                foreach (OGW10ORDR selectedObject in args.SelectedObjects)
                {
                    OGW10ORDR ORDR = (OGW10ORDR)selectedObject;

                    if (ORDR.IsNew == true)
                    {
                        ObjectSpace.CommitChanges();
                        base.Save(args);
                        ObjectSpace.Refresh();
                    }
                    else
                    {
                        ORDR.UpdateUser = user.UserName.ToString();
                        ORDR.UpdateDate = DateTime.Now;
                        ObjectSpace.CommitChanges();
                        base.Save(args);
                        ObjectSpace.Refresh();
                        ((DetailView)View).ViewEditMode = ViewEditMode.View;
                        View.BreakLinksToControls();
                        View.CreateControls();
                    }

                    /* Generate Document Number */
                    if (string.IsNullOrEmpty(ORDR.DocNum))
                    {
                        IObjectSpace os = Application.CreateObjectSpace();
                        OGW10NNM1 code = os.FindObject<OGW10NNM1>(CriteriaOperator.Parse("DocType.Code = ? AND IsActive = 'True'", ORDR.ObjType.Code));

                        if (code != null)
                        {
                            genCon.executeNonQuery("EXEC FTS_sp_GenAutoNumbering '" + ORDR.ObjType.Code + "','" + ORDR.Oid + "','" + code.Oid + "', '" + ORDR.SalesOrderDate.ToString("yyyy-MM-dd") + "'");
                        }
                    }
                }
            }
            #endregion

            ObjectSpace.Refresh();
        }
    }
}
