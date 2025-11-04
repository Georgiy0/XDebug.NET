# XDebug XML messages

Classes from `xdebugnet.lib.message` namespace implement wrappers over XDebug XML command format via `System.Xml.Serialization`.

`xdebugnet.lib.message.MessageSerializeHelper` provides function for deserializing responses and custing responses to more concrete types (e.g. ResponseMessage -> EvalResponseMessage).