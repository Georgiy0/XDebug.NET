# File URI collector

A sample tool build on top of `xdebugnet.lib` that maps relations between HTTP request URL and PHP file URI that acutally handles the request.

E.g. it may map HTTP URL and PHP file URI discrepencies caused via url rewriting (e.g. via Apache httpd, Nginx or other proxy capable software).

## Usage:
./xdebugnet.runner [TCP-port to listen]

Saves result to `out.csv` file on ctrl+c. Example:
```
FileUri,Uri,Correlator
file:///usr/local/www/some_php_file.php,/actual_url.php?query_string_param=1&__correlator=sample_correlator_123,sample_correlator_123
...
```