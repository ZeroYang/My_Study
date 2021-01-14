# find命令详解

在 Linux 命令中，find用于在指定目录下查找文件。任何位于参数之前的字符串都将被视为欲查找的目录名，其支持按名称查找、按正则表达式查找、按文件大小查找、按文件权限查找等多种查询方式。如果在使用该命令时，不设置任何参数，则find命令将在当前目录下查找子目录与文件，并且将查找到的子目录和文件全部进行显示。

语法：find + 目标目录(路径) + <选项> + 参数

* 示例 1：查找当前目录及其子目录下所有文件和文件夹
    ```
    find .
    ```

* 示例 2：在/testLinux目录下查找以.txt结尾的文件名
    ```
    // 需要书写完整的路径
    find /tmp/cg/testLinux -name "*.txt"
    ```

* 示例 3：组合查找文件名以file1开头（与、或、非）file2开头的文件
    ```
    /**
    * 组合查找语法：
    * -a        与（取交集）
    * -o        或（取并集）
    * -not      非（同 ！）
    * !         非（同 not）
    */

    find . -name "file1*" -a -name "file2*"
    find . -name "file1*" -o -name "file2*"
    find . -name "file1*" -not -name "file2*"
    find . -name "file1*" ! -name "file2*"*
    ```

* 示例 4：根据文件类型进行搜索
    ```
    /**
    * 查找当前目录及所有子目录下的普通文件
    */

    find . -type f
    ```

* 示例 5：基于目录深度进行搜索
    ```
    /**
    * 限制最大深度为 3
    */

    find . -maxdepth 3 -type f

    /**
    * 限制最大深度为 2
    */

    find . -maxdepth 2 -type f
    ```

* 示例 6：基于文件权限进行搜索
    ```
    /**
    * 搜索权限为 777 的文件
    */

    find . -type f -perm 777

    /**
    * 搜索 .txt 格式且权限不为 777 的文件
    */

    find . -type f -name "*.txt" ! -perm 777
    ```


* 示例 7：借助-exec命令，将当前目录及子目录下所有.txt格式的文件以File:文件名的形式打印出来
    ```
    find . -type f -name "*.txt" -exec printf "File: %s\n" {} \;
    ```


* 示例 8：借助-exec命令，将当前目录及子目录下所有 3 天前的.txt格式的文件复制一份到old目录
    ```
    find . -type f -mtime +3 -name "*.txt" -exec cp {} old \;
    ```

## 参考链接

https://cloud.tencent.com/developer/article/1435559?from=information.detail.linux%20%E6%9F%A5%E6%89%BE%E5%A4%9A%E7%A7%8D%E6%96%87%E4%BB%B6%E5%90%8D