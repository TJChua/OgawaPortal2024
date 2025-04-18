﻿using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Web.SystemModule;
using OgawaPortal.Module.BusinessObjects;
using OgawaPortal.Module.BusinessObjects.Logistic;
using OgawaPortal.Module.BusinessObjects.Maintenance;
using OgawaPortal.Module.BusinessObjects.POS___Exchange;
using OgawaPortal.Module.BusinessObjects.POS___Logistic;
using OgawaPortal.Module.BusinessObjects.Sales_Order;
using OgawaPortal.Module.Controllers;
using System;

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
            Frame.GetController<ModificationsController>().SaveAndNewAction.Active.SetItemValue("Enabled", false);
            Frame.GetController<ModificationsController>().SaveAndCloseAction.Active.SetItemValue("Enabled", false);
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
                        if (View.Id == "OGW10ORDR_DetailView_EditOrder")
                        {
                            ORDR.EditAndCancel = true;
                        }

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
            #region OGW10ORDN
            else if (View.ObjectTypeInfo.Type == typeof(OGW10ORDN))
            {
                foreach (OGW10ORDN selectedObject in args.SelectedObjects)
                {
                    OGW10ORDN ORDN = (OGW10ORDN)selectedObject;

                    if (ORDN.IsNew == true)
                    {
                        ObjectSpace.CommitChanges();
                        base.Save(args);
                        ObjectSpace.Refresh();
                    }
                    else
                    {
                        ORDN.UpdateUser = user.UserName.ToString();
                        ORDN.UpdateDate = DateTime.Now;
                        ObjectSpace.CommitChanges();
                        base.Save(args);
                        ObjectSpace.Refresh();
                        ((DetailView)View).ViewEditMode = ViewEditMode.View;
                        View.BreakLinksToControls();
                        View.CreateControls();
                    }

                    /* Generate Document Number */
                    if (string.IsNullOrEmpty(ORDN.DocNum))
                    {
                        IObjectSpace os = Application.CreateObjectSpace();
                        OGW10NNM1 code = os.FindObject<OGW10NNM1>(CriteriaOperator.Parse("DocType.Code = ? AND IsActive = 'True'", ORDN.ObjType.Code));

                        if (code != null)
                        {
                            genCon.executeNonQuery("EXEC FTS_sp_GenAutoNumbering '" + ORDN.ObjType.Code + "','" + ORDN.Oid + "','" + code.Oid + "', '" + ORDN.ReturnDate.ToString("yyyy-MM-dd") + "'");
                        }
                    }
                }
            }
            #endregion
            #region OGW10EXCO
            else if (View.ObjectTypeInfo.Type == typeof(OGW10EXCO))
            {
                foreach (OGW10EXCO selectedObject in args.SelectedObjects)
                {
                    OGW10EXCO EXCO = (OGW10EXCO)selectedObject;

                    if (EXCO.IsNew == true)
                    {
                        ObjectSpace.CommitChanges();
                        base.Save(args);
                        ObjectSpace.Refresh();
                    }
                    else
                    {
                        EXCO.UpdateUser = user.UserName.ToString();
                        EXCO.UpdateDate = DateTime.Now;
                        ObjectSpace.CommitChanges();
                        base.Save(args);
                        ObjectSpace.Refresh();
                        ((DetailView)View).ViewEditMode = ViewEditMode.View;
                        View.BreakLinksToControls();
                        View.CreateControls();
                    }

                    /* Generate Document Number */
                    if (string.IsNullOrEmpty(EXCO.DocNum))
                    {
                        IObjectSpace os = Application.CreateObjectSpace();
                        OGW10NNM1 code = os.FindObject<OGW10NNM1>(CriteriaOperator.Parse("DocType.Code = ? AND IsActive = 'True'", EXCO.ObjType.Code));

                        if (code != null)
                        {
                            genCon.executeNonQuery("EXEC FTS_sp_GenAutoNumbering '" + EXCO.ObjType.Code + "','" + EXCO.Oid + "','" + code.Oid + "', '" + EXCO.ReturnDate.ToString("yyyy-MM-dd") + "'");
                        }
                    }
                }
            }
            #endregion
            #region OGW10EXCO
            else if (View.ObjectTypeInfo.Type == typeof(OGW10DREQ))
            {
                foreach (OGW10DREQ selectedObject in args.SelectedObjects)
                {
                    OGW10DREQ DREQ = (OGW10DREQ)selectedObject;

                    if (DREQ.IsNew == true)
                    {
                        ObjectSpace.CommitChanges();
                        base.Save(args);
                        ObjectSpace.Refresh();
                    }
                    else
                    {
                        DREQ.UpdateUser = user.UserName.ToString();
                        DREQ.UpdateDate = DateTime.Now;
                        ObjectSpace.CommitChanges();
                        base.Save(args);
                        ObjectSpace.Refresh();
                        ((DetailView)View).ViewEditMode = ViewEditMode.View;
                        View.BreakLinksToControls();
                        View.CreateControls();
                    }

                    /* Generate Document Number */
                    if (string.IsNullOrEmpty(DREQ.DocNum))
                    {
                        IObjectSpace os = Application.CreateObjectSpace();
                        OGW10NNM1 code = os.FindObject<OGW10NNM1>(CriteriaOperator.Parse("DocType.Code = ? AND IsActive = 'True'", DREQ.ObjType.Code));

                        if (code != null)
                        {
                            genCon.executeNonQuery("EXEC FTS_sp_GenAutoNumbering '" + DREQ.ObjType.Code + "','" + DREQ.Oid + "','" + code.Oid + "', '" + DREQ.DeliveryReqDate.ToString("yyyy-MM-dd") + "'");
                        }
                    }
                }
            }
            #endregion
            #region OGW10DREX
            else if (View.ObjectTypeInfo.Type == typeof(OGW10DREX))
            {
                foreach (OGW10DREX selectedObject in args.SelectedObjects)
                {
                    OGW10DREX DREX = (OGW10DREX)selectedObject;

                    if (DREX.IsNew == true)
                    {
                        ObjectSpace.CommitChanges();
                        base.Save(args);
                        ObjectSpace.Refresh();
                    }
                    else
                    {
                        DREX.UpdateUser = user.UserName.ToString();
                        DREX.UpdateDate = DateTime.Now;
                        ObjectSpace.CommitChanges();
                        base.Save(args);
                        ObjectSpace.Refresh();
                        ((DetailView)View).ViewEditMode = ViewEditMode.View;
                        View.BreakLinksToControls();
                        View.CreateControls();
                    }

                    /* Generate Document Number */
                    if (string.IsNullOrEmpty(DREX.DocNum))
                    {
                        IObjectSpace os = Application.CreateObjectSpace();
                        OGW10NNM1 code = os.FindObject<OGW10NNM1>(CriteriaOperator.Parse("DocType.Code = ? AND IsActive = 'True'", DREX.ObjType.Code));

                        if (code != null)
                        {
                            genCon.executeNonQuery("EXEC FTS_sp_GenAutoNumbering '" + DREX.ObjType.Code + "','" + DREX.Oid + "','" + code.Oid + "', '" + DREX.DeliveryRequestDate.ToString("yyyy-MM-dd") + "'");
                        }
                    }
                }
            }
            #endregion
            #region OGW10OPKL
            else if (View.ObjectTypeInfo.Type == typeof(OGW10OPKL))
            {
                foreach (OGW10OPKL selectedObject in args.SelectedObjects)
                {
                    OGW10OPKL OPKL = (OGW10OPKL)selectedObject;

                    if (OPKL.IsNew == true)
                    {
                        ObjectSpace.CommitChanges();
                        base.Save(args);
                        ObjectSpace.Refresh();
                    }
                    else
                    {
                        OPKL.UpdateUser = user.UserName.ToString();
                        OPKL.UpdateDate = DateTime.Now;
                        ObjectSpace.CommitChanges();
                        base.Save(args);
                        ObjectSpace.Refresh();
                        ((DetailView)View).ViewEditMode = ViewEditMode.View;
                        View.BreakLinksToControls();
                        View.CreateControls();
                    }

                    /* Generate Document Number */
                    if (string.IsNullOrEmpty(OPKL.DocNum))
                    {
                        IObjectSpace os = Application.CreateObjectSpace();
                        OGW10NNM1 code = os.FindObject<OGW10NNM1>(CriteriaOperator.Parse("DocType.Code = ? AND IsActive = 'True'", OPKL.ObjType.Code));

                        if (code != null)
                        {
                            genCon.executeNonQuery("EXEC FTS_sp_GenAutoNumbering '" + OPKL.ObjType.Code + "','" + OPKL.Oid + "','" + code.Oid + "', '" + OPKL.DocDate.ToString("yyyy-MM-dd") + "'");
                        }
                    }
                }
            }
            #endregion
            #region OGW10PLEX
            else if (View.ObjectTypeInfo.Type == typeof(OGW10PLEX))
            {
                foreach (OGW10PLEX selectedObject in args.SelectedObjects)
                {
                    OGW10PLEX PLEX = (OGW10PLEX)selectedObject;

                    if (PLEX.IsNew == true)
                    {
                        ObjectSpace.CommitChanges();
                        base.Save(args);
                        ObjectSpace.Refresh();
                    }
                    else
                    {
                        PLEX.UpdateUser = user.UserName.ToString();
                        PLEX.UpdateDate = DateTime.Now;
                        ObjectSpace.CommitChanges();
                        base.Save(args);
                        ObjectSpace.Refresh();
                        ((DetailView)View).ViewEditMode = ViewEditMode.View;
                        View.BreakLinksToControls();
                        View.CreateControls();
                    }

                    /* Generate Document Number */
                    if (string.IsNullOrEmpty(PLEX.DocNum))
                    {
                        IObjectSpace os = Application.CreateObjectSpace();
                        OGW10NNM1 code = os.FindObject<OGW10NNM1>(CriteriaOperator.Parse("DocType.Code = ? AND IsActive = 'True'", PLEX.ObjType.Code));

                        if (code != null)
                        {
                            genCon.executeNonQuery("EXEC FTS_sp_GenAutoNumbering '" + PLEX.ObjType.Code + "','" + PLEX.Oid + "','" + code.Oid + "', '" + PLEX.DocDate.ToString("yyyy-MM-dd") + "'");
                        }
                    }
                }
            }
            #endregion
            else
            {
                base.Save(args);
                ((DetailView)View).ViewEditMode = ViewEditMode.View;
                View.BreakLinksToControls();
                View.CreateControls();
            }

            ObjectSpace.Refresh();
        }
    }
}
