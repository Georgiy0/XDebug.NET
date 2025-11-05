# Supported XDebug commands

Classes in `xdebugnet.lib.command` namespace implement XDebug commands such as:

|#|Class|Command|Description|
|-|-|-|-|
|1|`EvalCommand`|`eval`|execute PHP command in interpreter request handling session|
|2|`RunCommand`|`run`|continue PHP execution|
|3|`StopCommand`|`stop`|terminate debugging session|
|4|`StepOverCommand`|`step_over`|tracing command (step over)|
|5|`StepIntoCommand`|`step_into`|tracing command (step into)|