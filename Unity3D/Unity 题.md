#Unity3D试题

##一：什么是协同程序?

在主线程运行的同时开启另一段逻辑处理，来协助当前程序的执行，协程很像多线程，但是不是多线程，Unity的协程实在每帧结束之后去检测yield的条件是否满足。

##二：Unity3d中的碰撞器和触发器的区别?

碰撞器是触发器的载体，而触发器只是碰撞器身上的一个属性。当Is Trigger=false时，碰撞器根据物理引擎引发碰撞，产生碰撞的效果，可以调用OnCollisionEnter/Stay/Exit函数；当Is Trigger=true时，碰撞器被物理引擎所忽略，没有碰撞效果，可以调用OnTriggerEnter/Stay/Exit函数。如果既要检测到物体的接触又不想让碰撞检测影响物体移动或要检测一个物件是否经过空间中的某个区域这时就可以用到触发器

##三：物体发生碰撞的必要条件

两个物体都必须带有碰撞器（Collider），其中一个物体还必须带有Rigidbody刚体，而且必须是运动的物体带有Rigidbody脚本才能检测到碰撞。

##四：请简述ArrayList和List的主要区别

性能 ArrayList更消耗性能

将值类型的1转为object(int型转为object型数据，即为装箱)

使用区别
ArrayList的每个item默认是Object的类型
由于List使用了泛型，需要我们指定了item的类型

##五：请简述GC(垃圾回收)产生的原因，并描述如何避免?

GC回收堆上的内存
避免：
 
1）减少new产生对象的次数
2）使用公用的对象（静态成员）
3）将String换为StringBuilder

##六：反射的实现原理?
反射的概述
反射的定义：审查元数据并收集关于它的类型信息的能力。元数据（编译以后的最基本数据单元）就是一大堆的表，当编译程序集或者模块时，编译器会创建一个类定义表，一个字段定义表，和一个方法定义表等，。System.reflection命名空间包含的几个类，允许你反射（解析）这些元数据表的代码
和反射相关的命名空间（我们就是通过这几个命名空间访问反射信息）：
System.Reflection.MemberInfo
System.Reflection.EventInfo
System.Reflection.FieldInfo
System.Reflection.MethodBase
System.Reflection.ConstructorInfo
System.Reflection.MethodInfo
System.Reflection.PropertyInfo
System.Type
System.Reflection.Assembly
反射的作用：
1． 可以使用反射动态地创建类型的实例，将类型绑定到现有对象，或从现 有对象中获取类型
2． 应用程序需要在运行时从某个特定的程序集中载入一个特定的类型，以便实现某个任务时可以用到反射。
3． 反射主要应用与类库，这些类库需要知道一个类型的定义，以便提供更多的功能。
应用要点：
1． 现实应用程序中很少有应用程序需要使用反射类型
2． 使用反射动态绑定需要牺牲性能
3． 有些元数据信息是不能通过反射获取的
4． 某些反射类型是专门为那些clr 开发编译器的开发使用的，所以你要意识到不是所有的反射类型都是适合每个人的。
反射appDomain 的程序集

##七：简述四元数Quaternion的作用，四元数对欧拉角的优点?


##八：如何安全的在不同工程间安全地迁移asset数据?三种方法

1.将Assets目录和Library目录一起迁移
2.导出包，export Package
3.用unity自带的assets Server功能

##九：OnEnable、Awake、Start运行时的发生顺序?哪些可能在同一个对象周期中反复的发生?

执行先后顺序为：Awake、OnEnable、Start

Awake函数仅执行一次；如果游戏对象（即gameObject）的初始状态为关闭状态，那么运行程序，Awake函数不会执行；

OnEnable会在GameObject的Activity状态由false切换为true时响应
OnDisable

Update：当MonoBehaviour启用时,其Update在每一帧被调用；

LateUpdate：当Behaviour启用时，其LateUpdate在每一帧被调用

FixedUpdate:这个函数会在每个固定的物理时间片被调用一次.这是放置游戏基本物理行为代码的地方。UPDATE之后调用。

##十：MeshRender中material和sharedmaterial的区别?
sharedMaterial是公用的Material，所有用到这个材质的MeshRendered都会引用这个Material。改变sharedMaterial的属性也会改变mat文件。

material是独立的Material，改变material的属性不会影响到其他对象，也不会影响mat文件。

当只修改材质的参数的时候，使用material属性，确保其他对象不会受影响。

当需要修改材质的时候，直接赋值给sharedMaterial，否则赋值给material会产生内存泄露。

*赋值给material在销毁物体的时候需要我们手动去销毁该material*


##十一：transform.localposition和transform.position有什么区别?

transform.localposition是自身坐标 ,transform.position是世界坐标
TransformPoint
function TransformPoint (position : Vector3) : Vector3
function TransformPoint (x : float, y : float, z : float) : Vector3
把一个点从自身相对坐标转换到世界坐标

##十二：TCP/IP协议栈各个层次及分别的功能

##十三：Unity提供了几种光源，分别是什么
1.平行光(Directional Light)，因为所有的光线都有着同一个方向；它会独立于光源的位置。因为所有的光线都是平行的，对于场景中的每个物体光的方向都保持一致，物体和光源的位置保持怎样的关系都无所谓。由于光的方向向量保持一致，光照计算会和场景中的其他物体相似。
2.点光源是一个在时间里有位置的光源，它向所有方向发光，光线随距离增加逐渐变暗。想象灯泡和火炬作为投光物，它们可以扮演点光的角色。
3.区域光源：Area Light
4.聚光灯(Spotlight)是一种位于环境中某处的光源，它不是向所有方向照射，而是只朝某个方向照射。结果是只有一个聚光照射方向的确定半径内的物体才会被照亮，其他的都保持黑暗。聚光的好例子是路灯或手电筒。

##十四：简述一下对象池，你觉得在FPS里哪些东西适合使用对象池?

对象池就存放需要被反复调用资源的一个空间，当一个对象回大量生成的时候如果每次都销毁创建会很费时间，通过对象池把暂时不用的对象放到一个池中（也就是一个集合），当下次要重新生成这个对象的时候先去池中查找一下是否有可用的对象，如果有的话就直接拿出来使用，不需要再创建，如果池中没有可用的对象，才需要重新创建，利用空间换时间来达到游戏的高速运行效果，在FPS游戏中要常被大量复制的对象包括子弹，敌人，粒子等

##十五：CharacterController和Rigidbody的区别?

Rigidbody具有完全真实物理的特性，Unity中物理系统最基本的一个组件，包含了常用的物理特性，而CharacterController可以说是受限的的Rigidbody，具有一定的物理效果但不是完全真实的，是Unity为了使开发者能方便的开发第一人称视角的游戏而封装的一个组件

##十六：移动相机动作在哪个函数里，为什么在这个函数里?
LateUpdate，在每帧执行完毕调用，它是在所有Update结束后才调，比较适合用于命令脚本的执行。官网上例子是摄像机的跟随，都是在所有Update操作完才跟进摄像机，不然就有可能出现摄像机已经推进了，但是视角里还未有角色的空帧出现。

##十七：简述prefab的用处

在游戏运行时实例化，prefab相当于一个模板，对你已经有的素材、脚本、参数做一个默认的配置，以便于以后的修改，同时prefab打包的内容简化了导出的操作，便于团队的交流。

##十八：请简述sealed关键字用在类声明时与函数声明时的作用。

sealed修饰的类为密封类，类声明时可防止其他类继承此类，在方法中声明则可防止派生类重写此方法。

##十九：请简述private，public，protected，internal的区别。

public：对任何类和成员都公开，无限制访问
private：仅对该类公开
protected：对该类和其派生类公开
internal：只能在包含该类的程序集中访问该类


##二十：简述SkinnedMesh的实现原理