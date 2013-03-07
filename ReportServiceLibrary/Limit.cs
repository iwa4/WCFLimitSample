
class Limit
{
    private static Limit singleton = new Limit();
    private int runCounter;     //現在実行数を保持するカウンタ
    private object lockObject;  //カウンタをインクリメント or デクリメントする際の排他用オブジェクト

    private Limit()
    {
        runCounter = 0;
        lockObject = new object();
    }

    internal static Limit GetInstance()
    {
        return singleton;
    }

    internal Token GetToken()
    {
        lock (lockObject)
        {
            var token = new Token();
            token.IsLimitError = IsLimitError();
            return token;
        }
    }

    internal void ReleaseToken(Token token)
    {
        lock (lockObject)
        {
            if (!token.IsLimitError) runCounter--;
        }
    }

    private bool IsLimitError()
    {
        var limitNum = 1;   //<= 同時実行数。実際はconfigなど外部に保持する。
        if (runCounter < limitNum)
        {
            runCounter++;   //チェックOKの場合のみインクリメント
            return false;
        }
        else
        {
            return true;
        }
    }
}

class Token
{
    public bool IsLimitError;
}
