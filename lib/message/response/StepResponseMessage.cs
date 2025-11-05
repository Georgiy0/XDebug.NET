namespace xdebugnet.lib.message;

using System.Xml.Serialization;

[XmlRoot("message", Namespace = "https://xdebug.org/dbgp/xdebug")]
public class TraceMessage
{
    [XmlAttribute("filename")]
    public System.String FileUri;
    [XmlAttribute("lineno")]
    public System.Int32 LineNumber;
}

[XmlRoot("response", Namespace = "urn:debugger_protocol_v1")]
public class StepResponseMessage : ResponseMessage
{
    [XmlAttribute("status")]
    public System.String Status;
    [XmlAttribute("reason")]
    public System.String Reason;
    [XmlElement("message", Namespace = "https://xdebug.org/dbgp/xdebug")]
    public TraceMessage Message;

}