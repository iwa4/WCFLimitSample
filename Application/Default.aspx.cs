using ReportServiceReference;
using System;
using System.IO;
using System.ServiceModel;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        var rp = new RequestParameter();
        rp.param1 = "hoge";
        rp.param2 = "fuga";

        var proxy = new ReportServiceProxy();
        try
        {
            var report = proxy.ReadReport(rp);
            Session.Add("getKey", report);
        }
        catch (FaultException<LimitError>)
        {
            //実行制限OVERのエラー
        }
        catch (Exception)
        {
            //WCF通信のエラー
        }
    }
}

public class ReportServiceProxy
{
    public Stream ReadReport(RequestParameter rp)
    {
        var proxy = new ReportServiceLibClient();
        try
        {
            Stream report = null;
            using (var stream = proxy.ReadReport(rp))
            {
                report = ConvertMessageToStream(stream);
            }
            proxy.Close();
            return report;
        }
        catch (FaultException<LimitError>)
        {
            proxy.Abort();
            throw;
        }
        catch (Exception)
        {
            proxy.Abort();
            throw;
        }
    }

    private Stream ConvertMessageToStream(Stream input)
    {
        const int chunkSize = 4096;
        var buffer = new Byte[chunkSize];
        var output = new MemoryStream();
        while (true)
        {
            var bytesRead = input.Read(buffer, 0, chunkSize);
            if (bytesRead == 0) break;
            output.Write(buffer, 0, bytesRead);
        }
        return output;
    }
}
