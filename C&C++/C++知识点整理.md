## 关键字

### typedef 用法

1. 定义一种类型的别名，而不只是简单的宏替换。可以用作同时声明指针型的多个对象。比如：
    ```
    char* pa, pb; // 这多数不符合我们的意图，它只声明了一个指向字符变量的指针，和一个字符变量；
    ```

    以下则可行：
    ```
        typedef char* PCHAR;

        PCHAR pa, pb;  
    ```
2. 用在旧的C代码中，帮助struct。以前的代码中，声明struct新对象时，必须要带上struct，即形式为： struct 结构名对象名，如：
    ```
    struct tagPOINT1
    {
        int x;
        int y;
    };
    struct tagPOINT1 p1;
    ```
    而在C++中，则可以直接写：结构名对象名，即：tagPOINT1 p1;
    ```
    typedef struct tagPOINT
    {
        int x;
        int y;
    }POINT;
    POINT p1; // 这样就比原来的方式少写了一个struct，比较省事，
    ```

3. 用typedef来定义与平台无关的类型。
    比如定义一个叫 REAL 的浮点类型，在目标平台一上，让它表示最高精度的类型为：
    ```
    typedef long double REAL;
    //在不支持 long double 的平台二上，改为：
    typedef double REAL;
    //在连 double 都不支持的平台三上，改为：
    typedef float REAL;
    ```
    也就是说，当跨平台时，只要改下 typedef 本身就行，不用对其他源码做任何修改。
    标准库就广泛使用了这个技巧，比如size_t。另外，因为typedef是定义了一种类型的新别名，不是简单的字符串替换，所以它比宏来得稳健。 

4. 为复杂的声明定义一个新的简单的别名。方法是：在原来的声明里逐步用别名替换一部
分复杂声明，如此循环，把带变量名的部分留到最后替换，得到的就是原声明的最简化
版。举例： 
```
    //原声明：
    void (*b[10]) (void (*)());
    //变量名为b，先替换右边部分括号里的，pFunParam为别名一：
    typedef void (*pFunParam)();
    //再替换左边的变量b，pFunx为别名二：
    typedef void (*pFunx)(pFunParam);
    //原声明的最简化版：
    pFunx b[10];
```
## STL

### std::string

* std::string 、 char*、 char[] 之间的转换
1. string 转 char*
    1. data()方法，如：
    ```
    string str = "hello";
    const char* p = str.data();//加const  或者用char * p=(char*)str.data();的形式
    ```

    2. c_str()方法，如：
    ```
    string str=“world”;
    const char *p = str.c_str();//同上，要加const或者等号右边用char*
    ```
    3. copy()方法，如：
    ```
    string str="hmmm";
    char p[50];
    str.copy(p, 5, 0);//这里5代表复制几个字符，0代表复制的位置，
    *(p+5)=‘\0’;//注意手动加结束符！！！  
    ```

2. char* 转 string
    可以直接赋值
    ```
    string s;
    char *p = "hello";//直接赋值
    s = p;
    ```

3. string转char[]

    这个由于我们知道string的长度，可以根据length()函数得到，又可以根据下标直接访问，所以用一个循环就可以赋值了。
    ```
    string pp = "dagah";
    char p[8];
    int i;
    for( i=0;i<pp.length();i++)
        p[i] = pp[i];
    p[i] = '\0';
    printf("%s\n",p);
    cout<<p;
    ```
4. char[]转string
    ```
    char a[] = "abc";
    std::string b = std::string(a);
    ```
## 结构体

### 定义
```
struct name
{

}
```

### 继承

### 访问


### C++ 中struct和class的区别：
* 默认的继承访问权限。struct是public的，class是private的。
* struct作为数据结构的实现体，它默认的数据访问控制是public的，而class作为对象的实现体，它默认的成员变量访问控制是private的。

## 类

### 继承

默认是private

### 虚函数

    virtual void foo(){

    }
### 纯虚函数 

纯虚函数是在基类中声明的虚函数，它在基类中没有定义，但要求任何派生类都要定义自己的实现方法。在基类中实现纯虚函数的方法是在函数原型后加 =0:

    virtual void foo() = 0;

### 虚函数和纯虚函数的区别
* 定义一个函数为虚函数，不代表函数为不被实现的函数。
* 定义他为虚函数是为了允许用基类的指针来调用子类的这个函数。
* 定义一个函数为纯虚函数，才代表函数没有被实现。
* 定义纯虚函数是为了实现一个接口，起到一个规范的作用，规范继承这个类的程序员必须实现这个函数。
