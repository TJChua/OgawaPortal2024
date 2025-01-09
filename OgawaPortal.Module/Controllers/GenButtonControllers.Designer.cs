namespace OgawaPortal.Module.Controllers
{
    partial class GenButtonControllers
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AddItem = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.ChangeOutlet = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.SubmitDoc = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.CancelDoc = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.CloseDoc = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.ViewDoc = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            // 
            // AddItem
            // 
            this.AddItem.AcceptButtonCaption = null;
            this.AddItem.CancelButtonCaption = null;
            this.AddItem.Caption = "Add Item";
            this.AddItem.Category = "ListView";
            this.AddItem.ConfirmationMessage = null;
            this.AddItem.Id = "AddItem";
            this.AddItem.ToolTip = null;
            this.AddItem.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.AddItem_CustomizePopupWindowParams);
            this.AddItem.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.AddItem_Execute);
            // 
            // ChangeOutlet
            // 
            this.ChangeOutlet.Caption = "Change";
            this.ChangeOutlet.Category = "ObjectsCreation";
            this.ChangeOutlet.ConfirmationMessage = "Are you sure want to change the outlet?";
            this.ChangeOutlet.Id = "ChangeOutlet";
            this.ChangeOutlet.ToolTip = null;
            this.ChangeOutlet.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.ChangeOutlet_Execute);
            // 
            // SubmitDoc
            // 
            this.SubmitDoc.Caption = "Submit";
            this.SubmitDoc.Category = "ObjectsCreation";
            this.SubmitDoc.ConfirmationMessage = null;
            this.SubmitDoc.Id = "SubmitDoc";
            this.SubmitDoc.ToolTip = null;
            this.SubmitDoc.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.SubmitDoc_Execute);
            // 
            // CancelDoc
            // 
            this.CancelDoc.Caption = "Cancel";
            this.CancelDoc.Category = "ObjectsCreation";
            this.CancelDoc.ConfirmationMessage = null;
            this.CancelDoc.Id = "CancelDoc";
            this.CancelDoc.ToolTip = null;
            this.CancelDoc.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.CancelDoc_Execute);
            // 
            // CloseDoc
            // 
            this.CloseDoc.Caption = "Close";
            this.CloseDoc.Category = "ObjectsCreation";
            this.CloseDoc.ConfirmationMessage = null;
            this.CloseDoc.Id = "CloseDoc";
            this.CloseDoc.ToolTip = null;
            this.CloseDoc.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.CloseDoc_Execute);
            // 
            // ViewDoc
            // 
            this.ViewDoc.AcceptButtonCaption = null;
            this.ViewDoc.CancelButtonCaption = null;
            this.ViewDoc.Caption = "View";
            this.ViewDoc.Category = "ListView";
            this.ViewDoc.ConfirmationMessage = null;
            this.ViewDoc.Id = "ViewDoc";
            this.ViewDoc.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.ViewDoc.ToolTip = null;
            this.ViewDoc.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.ViewDoc_CustomizePopupWindowParams);
            this.ViewDoc.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.ViewDoc_Execute);
            // 
            // GenButtonControllers
            // 
            this.Actions.Add(this.AddItem);
            this.Actions.Add(this.ChangeOutlet);
            this.Actions.Add(this.SubmitDoc);
            this.Actions.Add(this.CancelDoc);
            this.Actions.Add(this.CloseDoc);
            this.Actions.Add(this.ViewDoc);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.PopupWindowShowAction AddItem;
        private DevExpress.ExpressApp.Actions.SimpleAction ChangeOutlet;
        private DevExpress.ExpressApp.Actions.SimpleAction SubmitDoc;
        private DevExpress.ExpressApp.Actions.SimpleAction CancelDoc;
        private DevExpress.ExpressApp.Actions.SimpleAction CloseDoc;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction ViewDoc;
    }
}
