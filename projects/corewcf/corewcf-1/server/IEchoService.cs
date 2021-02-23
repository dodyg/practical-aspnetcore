using CoreWCF;
using System.Runtime.Serialization;

namespace Contracts
{
    [ServiceContract]
    public interface IEchoService
    {
        [OperationContract]
        string Echo(string text);

        [OperationContract]
        string ComplexEcho(EchoMessage text);
    }

    [DataContract]
    public class EchoMessage
    {
        [DataMember]
        public string Text { get; set; }
    }
}