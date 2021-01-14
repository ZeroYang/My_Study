# Flask全局异常处理

在 Flask 程序中，使用 app.errorhandler() 装饰器可以注册错误处理函数，传入 HTTP 错误状态码或是特定的异常类：
```
@app.errorhandler(404)
def error_404(e):
    return '404 Error', 404
```
如果发生 404 错误，就会触发这个函数获取返回值作为响应主体。

通常我们会为不同的 HTTP 错误编写各自的的错误处理函数，以便返回不同的响应。如果你愿意的话，我们也可以编写一个统一的错误处理函数，这个函数会处理所有的 HTTP 错误和一般异常，只需要在装饰器内传入 Exception 类即可：
```
@app.errorhandler(Exception)
def all_exception_handler(e):
    return 'Error', 500
```
现在所有的 HTTP 错误都会触发这个函数。你也可以在函数中对错误进行分类处理：
```
@app.errorhandler(Exception)
def all_exception_handler(e):
    # 对于 HTTP 异常，返回自带的错误描述和状态码
    # 这些异常类在 Werkzeug 中定义，均继承 HTTPException 类
    if isinstance(e, HTTPException):
        return e.desciption, e.code
    return 'Error', 500  # 一般异常
```
如果你使用 Flask 0.12 版本，则需要参考这个 SO 回答重写相关方法。

附注一些关于错误处理的小知识：

对于一般的程序异常（比如 NameError），如果没有特定的异常处理函数，默认都会触发 500 错误处理函数。
开启调试模式的时候，500 错误会显示错误调试页面。
500 错误发生时传入错误处理函数的是真正的异常对象，不是 Werkzeug 内置的 HTTP 异常类。
内置的 HTTP 异常类的 description 和 code 属性分别返回错误描述和状态码。