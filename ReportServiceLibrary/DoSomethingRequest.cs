using System.IO;
using System.Runtime.Serialization;
using System.ServiceModel;

[DataContract]
//標準でシリアライズ出来ない型は、個別宣言が必要
//e.g. KnownType(typeof(string[]))
public class DoSomethingRequest
{
    [DataMember]
    public string LastName { get; set; }

    [DataMember]
    public string FirstName { get; set; }

    [DataMember]
    public string FilePath { get; set; }
}

[DataContract]
//クライアント側にエラーで戻った場合、実行制限エラーを識別するためのクラス
public class LimitError
{ }
