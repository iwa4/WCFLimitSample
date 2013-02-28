
class Limit
{
    private static Limit singleton = new Limit();
    private int runCounter;
    private object lockObject;

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
            token.IsLimitError = IsLimitError(runCounter);
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

    private bool IsLimitError(int runCounter)
    {
        var limitNum = 3;           //<= 同時実行数。実際はconfigなど外部に保持する。
        if (runCounter < limitNum)
        {
            //チェックOKの場合のみインクリメントを行う
            runCounter++;
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
