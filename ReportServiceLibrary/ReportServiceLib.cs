using System.IO;
using System.ServiceModel;
using System.Threading;

[ServiceContract]
public class ReportServiceLib
{
    [OperationContract]
    [FaultContract(typeof(LimitError))]
    public Stream DoSomething(DoSomethingRequest request)
    {
        Token token = null;
        try
        {
            token = Limit.GetInstance().GetToken();
            if (token.IsLimitError)
                throw new FaultException<LimitError>(new LimitError(), "");

            //テスト用に待機（WCFで帳票生成に時間がかかったことを想定）
            Thread.Sleep(5000);

            using (var fs = new FileStream(request.FilePath, FileMode.Open))
            {
                var bs = new byte[fs.Length];
                fs.Read(bs, 0, bs.Length);

                var returnStream = new MemoryStream(bs);
                //通信前に Position を必ずリセット（リセットしなければ取得側で読み取れない）
                if (returnStream != null) returnStream.Position = 0;
                return returnStream;
            }
        }
        catch
        {
            throw;
        }
        finally
        {
            Limit.GetInstance().ReleaseToken(token);
        }
    }
}

