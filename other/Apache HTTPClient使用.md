# Apache HTTPClient 使用

文档比较完善：[http://hc.apache.org/httpcomponents-client-ga/tutorial/html/](http://hc.apache.org/httpcomponents-client-ga/tutorial/html/)

http://hc.apache.org/httpcomponents-client-ga/httpclient/apidocs/org/apache/http/client/HttpClient.html


* 1、使用连接池

虽说http协议时无连接的，但毕竟是基于tcp的，底层还是需要和服务器建立连接的。对于需要从同一个站点抓取大量网页的程序，应该使用连接池，否则每次抓取都和Web站点建立连接、发送请求、获得响应、释放连接，一方面效率不高，另一方面稍不小心就会疏忽了某些资源的释放、导致站点拒绝连接（很多站点会拒绝同一个ip的大量连接、防止DOS攻击）。

游戏资源热更新  下载文件个数过多 可以考虑优化成 连接池

简单示例