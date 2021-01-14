# sqlite语句整理

* 查看DB的所有表
    ```
    .table
    ```
    or
    ```
    select * from sqlite_master where type="table";
    ```

* 查看具体一张表的表结构
    ```
    select * from sqlite_master where type="table" and name="system";
    ```
## 参考链接
https://www.runoob.com/sqlite/sqlite-syntax.html