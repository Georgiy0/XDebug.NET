namespace xdebugnet.lib.message;

using System.Xml.Serialization;

[XmlRoot("response", Namespace = "urn:debugger_protocol_v1")]
public class ResponseMessage
{
    [XmlAttribute("command")]
    public System.String Command;
    [XmlAttribute("transaction_id")]
    public System.String TransactionId;

    private System.String _rawResponse;
    public void SetRawResponse(System.String rawResponse) => _rawResponse = rawResponse;
    public System.String RawResponse => _rawResponse;
}