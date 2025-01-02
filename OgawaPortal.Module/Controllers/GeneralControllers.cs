using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Editors;
using DevExpress.Web;
using DevExpress.Xpo.DB.Helpers;
using System;
using System.Data;
using System.Data.SqlClient;

namespace OgawaPortal.Module.Controllers
{
    public partial class GeneralControllers : ViewController
    {
        public GeneralControllers()
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
        }
        protected override void OnDeactivated()
        {
            base.OnDeactivated();
        }

        public string getConnectionString()
        {
            string connectionString = "";

            ConnectionStringParser helper = new ConnectionStringParser(Application.ConnectionString);
            helper.RemovePartByName("xpodatastorepool");
            connectionString = string.Format(helper.GetConnectionString());

            return connectionString;
        }

        public void openNewView(IObjectSpace os, object target, ViewEditMode viewmode)
        {
            ShowViewParameters svp = new ShowViewParameters();
            DetailView dv = Application.CreateDetailView(os, target);
            dv.ViewEditMode = viewmode;
            dv.IsRoot = true;
            svp.CreatedView = dv;

            Application.ShowViewStrategy.ShowView(svp, new ShowViewSource(null, null));

        }
        public void showMsg(string caption, string msg, InformationType msgtype)
        {
            MessageOptions options = new MessageOptions();
            options.Duration = 3000;
            //options.Message = string.Format("{0} task(s) have been successfully updated!", e.SelectedObjects.Count);
            options.Message = string.Format("{0}", msg);
            options.Type = msgtype;
            options.Web.Position = InformationPosition.Right;
            options.Win.Caption = caption;
            options.Win.Type = WinMessageType.Flyout;
            Application.ShowViewStrategy.ShowMessage(options);
        }

        // Remove selected row
        public void ResetGridSelectionCore(object control)
        {
            ASPxGridView gridView = control as ASPxGridView;
            if (gridView != null)
            {
                gridView.Selection.UnselectAll();
            }
        }

        public void executeNonQuery(string query)
        {
            #region Connect to xaf DB

            string PopUpMsg = "";
            string connectionString = getConnectionString();

            SqlConnection conn = new SqlConnection(connectionString);

            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            #endregion

            #region ExecuteNonQuery
            try
            {
                conn.Open();

                SqlCommand cmdsql = new SqlCommand(query, conn);
                cmdsql.CommandTimeout = 0;
                cmdsql.ExecuteNonQuery();
                cmdsql.Dispose();

                conn.Close();
            }
            catch (Exception ex)
            {
                PopUpMsg = ex.Message;
                showMsg("Error", PopUpMsg, InformationType.Error);
                conn.Close();
            }
            #endregion
        }
    }
}
