using System;
using System.IO;
using System.Web;

public partial class Report : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            var key = Request.QueryString["key"];
            var report = (MemoryStream)Session[key];
            if (report != null)
            {
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "inline; filename=" + Request.QueryString["filename"]);
                Response.Cache.SetCacheability(HttpCacheability.Private);
                Response.BinaryWrite(report.ToArray());
            }
        }
        finally
        {
            Session.Clear();
        }
    }
}