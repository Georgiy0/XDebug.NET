namespace xdebugnet.lib.message;

using System.Xml.Serialization;

public static class MessageSerializeHelper
{
    private static MT? DeserializeMessageOfType<MT>(System.String data)
    {
        MT? message;
        var messageSerializer = new XmlSerializer(typeof(MT));
        using (StringReader reader = new StringReader(data))
        {
            message = (MT?)messageSerializer.Deserialize(reader);
        }
        return message;
    }
    public static MT Deserialize<MT>(System.String data)
    {
        var result = DeserializeMessageOfType<MT>(data);
        return result
            ?? throw new NotImplementedException($"failed to deserialize {typeof(MT).Name}");
    }
    public static MT Reserialize<MT>(this ResponseMessage responseMessage)
        => Deserialize<MT>(responseMessage.RawResponse);
}