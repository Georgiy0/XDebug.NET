# XDebug.NET

.NET implementation of XDebug API for PHP debugging. Provides API for automating PHP debug session for data collection and DAST tooling implementation.

## [lib - xdebugnet.lib](./lib/README.md)

Implements XDebug.NET server and automation API

## [runner](./runner/README.md)

Sample DAST tool that uses XDebug.NET API

# TODO

- [ ] Implement support for most useful commands: setting breakpoints, getting stack trace, coniguring debug session parameters
- [ ] Implement support for notification messages (notify)

# Usage

[XDebug extension](https://xdebug.org/) should be configured in order to connect to XDebug.NET server TCP-port.

E.g., this configuration segment may be used in php.ini:

```ini
zend_extension = "...path_to/xdebug.{so.dll}"

[XDebug]
xdebug.mode = debug
xdebug.start_with_request = yes
xdebug.client_host = <XDebug.NET IP address / hostname>
xdebug.client_port = <XDdebug.NET port>
```