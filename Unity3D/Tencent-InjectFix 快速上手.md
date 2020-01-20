# Tencent/InjectFix 快速上手

[https://github.com/Tencent/InjectFix.git](https://github.com/Tencent/InjectFix.git)

可用于Unity业务的bug修复，支持Unity全系列，全平台。

## 安装InjexctFix
 先clone工程， 进入Source\VSProj目录，修改脚本 build_for_unity.bat ，设置正确的UNITY_HOME，其他脚本类似

    @set UNITY_HOME=C:\Program Files (x86)\Unity4.7.2f1

执行脚本。 执行脚本后unity工程下多了几个文件

..\UnityProj\Assets\Plugins\IFix.Core.dll  	//IFix的dll

..\UnityProj\IFixToolKit					//IFix工具	

## 体验Unity InjexctFix
Source\UnityProj 是默认提供的Unity Demo，用Unity打开UnityProj，打开Helloworld场景，体验demo，发现add方法错误，

### Fix
 修复错误方法，打开Fix属性

        [Patch]
        public int Add(int a, int b)
        {
            return a + b;
        }

Unity窗口菜单中 执行InjectFix/Fix。 生成如下文件
output: Assembly-CSharp.patch.bytes
output: Assembly-CSharp-firstpass.patch.bytes

将Assembly-CSharp.patch.bytes、Assembly-CSharp-firstpass.patch.bytes拷贝到UnityProj\Assets\Resources目录下，

实际项目中，因为需要我们热更出去，将文件拷贝到Application.persistentDataPath目录下

        if (File.Exists(Application.streamingAssetsPath + "/Assembly-CSharp.patch.bytes"))
        {
            //var patchData = new FileStream(Application.persistentDataPath + "/Assembly-CSharp.patch.bytes", FileMode.Open);
            //if (patch != null)
            //{
                UnityEngine.Debug.Log("loading Assembly-CSharp.patch ...");
                var sw = Stopwatch.StartNew();
                //PatchManager.Load(new MemoryStream(patchData.));
                PatchManager.Load(new FileStream( Application.streamingAssetsPath + "/Assembly-CSharp.patch.bytes", FileMode.Open));
                UnityEngine.Debug.Log("patch Assembly-CSharp.patch, using " + sw.ElapsedMilliseconds + " ms");
            //}
        }

### Injexct

还有Fix修改代码，Unity窗口菜单中 执行InjectFix/Inject，从新运行，发现Add方法输出正确。

##注意事项

一次代码修改执行一次Fix操作，或者Reimport任意一个代码文件，等Unity编译后在进行Fix操作。

##iOS 体验