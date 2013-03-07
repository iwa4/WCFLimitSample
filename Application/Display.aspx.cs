using System;
using System.Web;

public partial class Display : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        var key = Request.QueryString["key"];
        try
        {
            var returnBytes = Session[key] as Byte[];
            if (returnBytes != null)
            {
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "inline; filename=" + Request.QueryString["filename"]);
                Response.Cache.SetCacheability(HttpCacheability.Private);
                Response.BinaryWrite(returnBytes);
            }
        }
        finally
        {
            Session.Remove(key);
        }
    }
}