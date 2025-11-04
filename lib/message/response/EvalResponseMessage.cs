namespace xdebugnet.lib.message;

using System.Text;
using System.Xml.Serialization;

[XmlRoot("property")]
public class EvalProperty
{
    [XmlAttribute("type")]
    public System.String Type;
    [XmlAttribute("size")]
    public System.Int32 Size;
    [XmlAttribute("encoding")]
    public System.String DataEncoding;
    [XmlText]
    public System.String RawData;

    public System.String? GetData()
    {
        if (Type == "null")
        {
            return null;
        }
        if (Type == "string" && DataEncoding == "base64")
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(RawData));
        }
        throw new NotImplementedException($"type and endoding not supported ({Type}, {DataEncoding})");
    }
}

[XmlRoot("response", Namespace = "urn:debugger_protocol_v1")]
public class EvalResponseMessage : ResponseMessage
{
    [XmlElement("property")]
    public EvalProperty Property;
}