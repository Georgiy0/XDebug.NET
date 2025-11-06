namespace xdebugnet.lib.message;

using System.Xml.Serialization;

[XmlRoot("stack")]
public class StackLevel
{
    [XmlAttribute("where")]
    public System.String Where;
    [XmlAttribute("level")]
    public System.Int32 Level;
    [XmlAttribute("type")]
    public System.String Type;
    [XmlAttribute("filename")]
    public System.String FileUri;
    [XmlAttribute("lineno")]
    public System.Int32 LineNumber;
}

[XmlRoot("response", Namespace = "urn:debugger_protocol_v1")]
public class StackResponseMessage : ResponseMessage
{
    [XmlElement("stack")]
    public List<StackLevel> StackLevels;
    public IEnumerable<StackLevel> SortedStackLevels => StackLevels.OrderBy(e => e.Level).AsEnumerable();

}