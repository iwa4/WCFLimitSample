﻿using ReportServiceReference;
using System;
using System.IO;
using System.ServiceModel;

public partial class _Default : System.Web.UI.Page
{
    public string hoge;
    protected void Page_Load(object sender, EventArgs e)
    {
        var request = new DoSomethingRequest();
        request.FirstName = "Alice";
        request.LastName = "Wonderland";
        request.FilePath = @"C:\Temp\JavaScript.pdf";

        try
        {
            var proxy = new DoSomethingServiceProxy();
            var returnBytes = proxy.DoSomething(request);
            Session.Add("getKey", returnBytes);
        }
        catch (FaultException<LimitError>)
        {
            //実行制限OVERのエラー。アプリケーションエラーとして扱う。
            System.Diagnostics.Debug.WriteLine("実行数OVER");
        }
        catch (Exception)
        {
            //その他エラー。システムエラーとして扱う。
            System.Diagnostics.Debug.WriteLine("WCF通信エラー");
        }
        hoge = null;
    }
}

public class DoSomethingServiceProxy
{
    public Byte[] DoSomething(DoSomethingRequest request)
    {
        var proxy = new ReportServiceLibClient();
        try
        {
            Byte[] returnBytes;
            using (var returnStream = proxy.DoSomething(request))
            using (var tempStream = new MemoryStream())
            {
                returnStream.CopyTo(tempStream);
                returnBytes = tempStream.ToArray();
            }
            proxy.Close();
            return returnBytes;
        }
        catch
        {
            proxy.Abort();
            throw;
        }
    }
}
