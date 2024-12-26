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
            // GenButtonControllers
            // 
            this.Actions.Add(this.AddItem);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.PopupWindowShowAction AddItem;
    }
}
