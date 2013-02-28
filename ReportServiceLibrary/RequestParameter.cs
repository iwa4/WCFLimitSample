using System.Runtime.Serialization;

[DataContract, KnownType(typeof(string[]))]
public class RequestParameter
{
    [DataMember]
    public string param1;

    [DataMember]
    public string param2;
}

[DataContract]
public class LimitError
{ }
