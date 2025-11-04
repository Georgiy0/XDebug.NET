# XDebug.NET

.NET implementation of XDebug API for PHP debugging. Provides API for automating PHP debug session for data collection and DAST tooling implementation.

## [lib - xdebugnet.lib](./lib/README.md)

Implements XDebug.NET server and automation API

## [runner](./runner/README.md)

Sample DAST tool that uses XDebug.NET API

# TODO

- [ ] Implement support for most useful commands: setting breakpoints, getting stack trace, coniguring debug session parameters
- [ ] Implement support for notification messages (notify)