namespace OgawaPortal.Module.Controllers
{
    partial class NonpersistentControllers
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
            this.AddCart = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.ClearCart = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // AddCart
            // 
            this.AddCart.ActionMeaning = DevExpress.ExpressApp.Actions.ActionMeaning.Accept;
            this.AddCart.Caption = "Add To Cart";
            this.AddCart.Category = "ListView";
            this.AddCart.ConfirmationMessage = null;
            this.AddCart.Id = "AddCart";
            this.AddCart.ToolTip = null;
            this.AddCart.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.AddCart_Execute);
            // 
            // ClearCart
            // 
            this.ClearCart.Caption = "Clear Cart";
            this.ClearCart.Category = "ListView";
            this.ClearCart.ConfirmationMessage = "Are you sure you want to clear the cart?";
            this.ClearCart.Id = "ClearCart";
            this.ClearCart.ToolTip = null;
            this.ClearCart.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.ClearCart_Execute);
            // 
            // NonpersistentControllers
            // 
            this.Actions.Add(this.AddCart);
            this.Actions.Add(this.ClearCart);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction AddCart;
        private DevExpress.ExpressApp.Actions.SimpleAction ClearCart;
    }
}
