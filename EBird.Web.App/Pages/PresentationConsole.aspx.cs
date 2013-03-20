using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EBird.Web.App.Pages
{
    public partial class PresentationConsole : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetFilesAndFolders();
            }
        }

        //void gvDoc_RowCommand(Object sender, GridViewCommandEventArgs e)
        //{
        //    if (e.CommandName == "Doc")
        //    {
        //        string docName = Convert.ToString(e.CommandArgument);

        //    }
        //}


        public void GetFilesAndFolders()
        {
            DirectoryInfo dirInfo = new DirectoryInfo(Server.MapPath("~/PrensentationDocs/11231"));
            FileInfo[] fileInfo = dirInfo.GetFiles("*.*", SearchOption.AllDirectories);
            gvDoc.DataSource = fileInfo;
            gvDoc.DataBind();
        }

        protected void gvDoc_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Doc")
            {
                string docName = Convert.ToString(e.CommandArgument);
                lblFileName.Text = docName;
            }
        }
    }
}