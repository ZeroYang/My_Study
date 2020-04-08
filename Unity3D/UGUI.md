#UGUI

[https://www.cnblogs.com/muxiaomo/p/4509245.html](https://www.cnblogs.com/muxiaomo/p/4509245.html)

###UGUI的核心元素：
* Anchor(锚点)：每个控件都有一个Anchor属性，控件的4个顶点，分别与Anchor的4个点保持不变的距离，不受屏幕分辨率变化的影响。

                      系统默认设置控件的Anchor位置在其父物体的中心处，且不能离开父物体的范围。

                      将Anchor设置在父物体的左侧，可以实现左对齐的效果。

                      将Anchor设置在父物体的4个顶点上，子物体将随父物体同步缩放。

* Pivot(轴)：控件的中心点（或称为轴），控件围绕Pivot发生旋转（若想控件围绕某个顶点旋转，改变Pivot位置即可）

              Rec Transform中的PosX、PosY、PosZ指的是Pivot与Anchor间的相对位置

###UGUI的基本控件：
* Canvas(画布)：所有UI控件必须在Canvas上面绘制，也可以看做所有UI控件的父物体。

* Panel(面板)：主要的功能就是一个容器，可以放置其他控件，使其进行整体移动、旋转、缩放等。一个功能完备的UI界面，往往会使用多个Panel容器，甚至使用Panel嵌套。

* Text(文本)：富文本功能类似HTML中的标签。

* Image(图像)：图像源为2D Sprite格式。等比例调节图像大小，需要按住Shift键进行调节。Image Type的Sliced选项，需要对Sprite进行“九宫格”处理。

* Raw Image(原始图像)：图像源为Texture格式。

最后补充一个基本组件：

* Mask(遮罩)：遮罩并不是GUI的控件。它以父物体的范围约束子物体的显示，如果子物体过大，将只显示在父物体中的一部分

###UGUI的复合控件：
* Button(按钮)：由两个控件组成：1.添加了Button组件的Image控件，2.Text控件。Image控件是Text控件的父对象。

                    鉴于UGUI的高度自由，也可以理解为添加了Button组件、Image组件的空对象和添加了Text组件的空对象。

                    通过Transition对按钮的三态进行设置。

* InputField(输入框)：由三个控件组成：1.添加了Input Field组件的Image控件，2.Text控件（用作显示提示内容），3.Text控件（接收输入内容）。Image控件是两个Text控件的父对象。

                           Content Type对输入的字符类型进行预处理功能。

* Toggle(开关)：即可以做单选框又可以做复选框，系统默认为复选框。

                    由四个控件组成：1.添加了Toggle组件的空对象，2.Image控件（显示状态框的背景图），3.Image控件（显示当前状态），4.Text控件（用作显示选项内容）。

                    如何制作单选框：创建一个空对象，添加Toggle Group组件。在空对象下创建若干个Toggle控件，设置Group，并保持其中一个Toggle控件的Is On开关为true，其余为false。

* Slider(滑动条)：由6个控件组成：1.添加了Slider组件的空对象，2.Image控件（显示背景图像），3.空对象（控制填充区域），4.Image控件（显示填充图像），5.空对象（控制滑块移动区域），6.Image控件（显示滑块）

                     滑动条通过滑块驱动，在minValue和maxValue区间运动，根据当前的Value值，不断改变背景图像和填充图像的显示范围。

                     滑动条既可以用作音量控制等输入控件，去掉滑块后，也可以用作血量、进度等显示控件。

* ScrollBar(滚动条)：由3个控件组成：1.添加了Scrollbar组件的Image对象，2.空对象（控制滑块移动区域）,3.Image控件（显示滑块）

                          滚动条与滑动条的原理类似，对比而言，滚动条背景色单一，数值范围固定为0到1，偏重于单步数值的设置

　　                     滚动条既可以用作垂直滚动文本（背包），也可以用作水平滚动时间轴，还可以垂直+水平进行图像的缩放。