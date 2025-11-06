# Sample

Sample tools build on top of `xdebugnet.lib`.

# FileUriTransactionServer

A tool that maps relations between HTTP request URL and PHP file URI that acutally handles the request.

E.g. it may map HTTP URL and PHP file URI discrepencies caused via url rewriting (e.g. via Apache httpd, Nginx or other proxy capable software).

## Usage:
./xdebugnet.runner [TCP-port to listen]

Saves result to `out.csv` file on ctrl+c. Example:
```
FileUri,Uri,Correlator
file:///usr/local/www/some_php_file.php,/actual_url.php?query_string_param=1&__correlator=sample_correlator_123,sample_correlator_123
...
```

# TracerTransactionServer

A tool that collects request processing trace via `step_into` command. Request should include `?__correlator=__do_trace` parameter in order to trigger trace collection.

It also demonstates the usage of `EvalCommand.GetPhpFileLineEval` for reading current PHP code line that corresponds to trace location.

E.g. execution trace may be used in VS code to crossreference with application codebase.

_Note:_ loop iterations with body length exceeding 1 code line are not dedublicated from execution trace (i.e. execution trace may be bloated with loop iterations).

## Usage:
./xdebugnet.runner [TCP-port to listen]

Saves result to `trace.log` on request processing completion. Example:
```
1: file:///some_php_file.php:30
2: file:///some_inc_file.inc:35
3: file:///some_inc_file.inc:39
4: file:///some_inc_file_2.inc:33
5: file:///some_inc_file_3.inc:30
```