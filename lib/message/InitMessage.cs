namespace xdebugnet.lib.message;

using System.Xml.Serialization;

[XmlRoot("engine")]
public class EngineInfo
{
    [XmlAttribute("version")]
    public System.String Version;
    [XmlText]
    public System.String Engine;
}

[XmlRoot("init", Namespace = "urn:debugger_protocol_v1")]
public class InitMessage
{
    [XmlAttribute("fileuri")]
    public System.String FileUri;
    [XmlAttribute("language")]
    public System.String Language;
    [XmlAttribute("language_version", Namespace = "https://xdebug.org/dbgp/xdebug")]
    public System.String LanguageVersion;
    [XmlAttribute("protocol_version")]
    public System.String ProtocolVersion;
    [XmlAttribute("appid")]
    public System.String AppId;
    [XmlElement("engine")]
    public EngineInfo Engine;
    [XmlElement("author")]
    public System.String Author;
    [XmlElement("url")]
    public System.String Url;
    [XmlElement("copyright")]
    public System.String Copyright;
}