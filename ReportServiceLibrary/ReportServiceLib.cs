using System;
using System.IO;
using System.ServiceModel;

[ServiceContract]
public class ReportServiceLib
{
    [OperationContract]
    [FaultContract(typeof(LimitError))]
    public Stream ReadReport(RequestParameter requestParameter)
    {
        Token token = null;
        try
        {
            token = Limit.GetInstance().GetToken();
            if (token.IsLimitError)
                throw new FaultException<LimitError>(new LimitError(), "");

            using (var fs = new FileStream(@"E:\develop\WCFtest\report\JavaScript.pdf", FileMode.Open))
            {
                var bs = new byte[fs.Length];
                fs.Read(bs, 0, bs.Length);

                var report = new MemoryStream(bs);
                //通信前に Position を必ずリセット（リセットしなければ取得側で正常に読み取れない）
                if (report != null)
                    report.Position = 0;

                return report;
            }
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            Limit.GetInstance().ReleaseToken(token);
        }
    }
}

