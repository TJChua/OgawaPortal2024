namespace OgawaPortal.Module.Controllers
{
    partial class SalesControllers
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
            this.DeleteORDRLine = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.CopyFrmSO = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.ResumeDoc = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.CopyFrmDR = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.CopyFrmDREX = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            // 
            // DeleteORDRLine
            // 
            this.DeleteORDRLine.Caption = "Delete";
            this.DeleteORDRLine.Category = "Edit";
            this.DeleteORDRLine.ConfirmationMessage = "Are you sure want to proceed?";
            this.DeleteORDRLine.Id = "DeleteORDRLine";
            this.DeleteORDRLine.ImageName = "Action_Delete";
            this.DeleteORDRLine.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireMultipleObjects;
            this.DeleteORDRLine.ToolTip = null;
            this.DeleteORDRLine.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.DeleteORDRLine_Execute);
            // 
            // CopyFrmSO
            // 
            this.CopyFrmSO.AcceptButtonCaption = null;
            this.CopyFrmSO.CancelButtonCaption = null;
            this.CopyFrmSO.Caption = "Copy Frm SO";
            this.CopyFrmSO.Category = "ListView";
            this.CopyFrmSO.ConfirmationMessage = null;
            this.CopyFrmSO.Id = "CopyFrmSO";
            this.CopyFrmSO.ToolTip = null;
            this.CopyFrmSO.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.CopyFrmSO_CustomizePopupWindowParams);
            this.CopyFrmSO.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.CopyFrmSO_Execute);
            // 
            // ResumeDoc
            // 
            this.ResumeDoc.Caption = "Resume";
            this.ResumeDoc.Category = "ObjectsCreation";
            this.ResumeDoc.ConfirmationMessage = null;
            this.ResumeDoc.Id = "ResumeDoc";
            this.ResumeDoc.ToolTip = null;
            this.ResumeDoc.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.ResumeDoc_Execute);
            // 
            // CopyFrmDR
            // 
            this.CopyFrmDR.AcceptButtonCaption = null;
            this.CopyFrmDR.CancelButtonCaption = null;
            this.CopyFrmDR.Caption = "Copy Frm DR";
            this.CopyFrmDR.Category = "ListView";
            this.CopyFrmDR.ConfirmationMessage = null;
            this.CopyFrmDR.Id = "CopyFrmDR";
            this.CopyFrmDR.ToolTip = null;
            this.CopyFrmDR.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.CopyFrmDR_CustomizePopupWindowParams);
            this.CopyFrmDR.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.CopyFrmDR_Execute);
            // 
            // CopyFrmDREX
            // 
            this.CopyFrmDREX.AcceptButtonCaption = null;
            this.CopyFrmDREX.CancelButtonCaption = null;
            this.CopyFrmDREX.Caption = "Copy Frm DREX";
            this.CopyFrmDREX.Category = "ListView";
            this.CopyFrmDREX.ConfirmationMessage = null;
            this.CopyFrmDREX.Id = "CopyFrmDREX";
            this.CopyFrmDREX.ToolTip = null;
            this.CopyFrmDREX.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.CopyFrmDREX_CustomizePopupWindowParams);
            this.CopyFrmDREX.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.CopyFrmDREX_Execute);
            // 
            // SalesControllers
            // 
            this.Actions.Add(this.DeleteORDRLine);
            this.Actions.Add(this.CopyFrmSO);
            this.Actions.Add(this.ResumeDoc);
            this.Actions.Add(this.CopyFrmDR);
            this.Actions.Add(this.CopyFrmDREX);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction DeleteORDRLine;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction CopyFrmSO;
        private DevExpress.ExpressApp.Actions.SimpleAction ResumeDoc;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction CopyFrmDR;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction CopyFrmDREX;
    }
}
