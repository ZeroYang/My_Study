#Unity使用Obb扩展包的正确姿势

由于Google Play上不能上传大于100M的包，所以需要将应用进行Obb分包，资源文件打包到Obb中，在Apk启动的时候再从Obb扩展文件中加载资源。

##如何生成Obb扩展资源文件
Unity可以自动为你进行分包操作，只需要你在发布安卓版本的时候进行简单的设置，当然也可以自己根据需求通过以下命令进行分包

	// jobb 命令在sdk\tools目录下
	jobb -pn <package> -pv <versioncode> -d \资源 -o G:\输出包名(如main.1.com.google.obb)
	obb扩展文件的命名规则为：
	main文件：main.<expansion-version>.<package-name>.obb
	patch文件：patch.<expansion-version>.<package-name>.obb

按照Unity分包的规则，主APK文件主要包括Java、Native代码、游戏脚本、插件以及第一个场景包含的所有资源。Obb包主要是资源文件，在Unity打包Apk过程中，会把所有的资源文件（包括 streaming Assets）打包到Assets目录下，而Obb分包后会将第一个场景以外的资源都打包到Obb目录中，在Apk启动后，会根据相应命名规则从Obb中加载资源文件。而在Unity里面为了安全性还封装了一些校验规则，下面会提取出相关的校验规则供我们下载校验（这只针对通过Unity直接打包会生成相关的校验规则，如果你是导出工程然后再进行分包、打包那么Unity这套规则并不直接适用于你，为了安全性你可以自己实现一套类似的规则）。

##如何使用Obb扩展资源文件
大多数情况下，当用户从Google Play上下载应用时，Google Play会自动将APK文件和扩展文件同时下载下来，至于具体是哪些cases下Google Play无法下载扩展文件并没有说明，此外即使Google Play正确的下载了扩展文件，但是由于扩展文件存放的目录是可以被用户和其他应用访问的。但是Google Play并不总是保证一定会下载扩展文件，一般情况下我们需要将生成的apk以及obb下载下来的扩展文件有可能会被用户或其他应用删除。
其次，我们的安装包除了在Google Play平台，也会在其他渠道上架，所以为了保证用户下载简洁可靠，我们需要在应用中自行实现扩展文件完整性检查和下载的机制。

###如何手动下载Obb资源扩展文件
1.如果你的Obb扩展文件上传到Google平台，那么你可以使用Android中提供的APK扩展文件下载库Downloader Library来简化扩展文件检查和下载的逻辑，具体可以参考以及Google Play APK扩展文件机制及开发流程详解，然而这种方式限制多多，需要支持google框架，不能应用于其他渠道等等...
2.将扩展文件上传到自己的服务器，原理上就可以适用于所有的渠道，需要的就是实现一个网络下载器。

手动校验Obb是否已经下载完成
UnityPlayer是一个UI场景类，在UnityPlayerActivity会初始化该类，在进入游戏前，这个类里面会读取本地Obb文件生成校验码并与打包Apk时，配置在setting.xml中的校验表对比，如果校验失败，则不会进入游戏场景，配置表如下：

Assets/bin/Data/Setting.xml

	<?xml version="1.0" encoding="UTF-8"?>
	<settings>
	  <integer name="splash_mode">0</integer>
	  <bool name="useObb">True</bool>
	  <bool name="9f6f9912e7e5c791037078042be85f73">True</bool>
	</settings>
splash_mode:应该是定义启动模式

useObb：是否使用Obb，如果没有使用Unity进行Obb分包，那么该选项始终是False。

`9f6f9912e7e5c791037078042be85f73`：表示加密算法生成的校验码。

项目中需要做的是在进入游戏后去进行一次Obb校验，防止用户重复下载Obb，如果校验失败就需要我们在游戏中自动去下载Obb包，我们把Unity中校验Obb的步骤拎出来，一共三部。

检测Obb文件是否存在
根据Obb文件生成校验码
读取setting.xml文件，并与校验码做对比
下面的具体的一些代码,主要规则来源于UnityPlayer。

获取Obb文件

    /**
     * 获取应用obb位置
     * @param paramContext
     * @return
     */
    private static String[] getObbPath(Context paramContext) {
        String str1 = paramContext.getPackageName();
        Vector<String> localVector = new Vector<String>();
        try {
            int i1 = paramContext.getPackageManager().getPackageInfo(str1, 0).versionCode;
            if (Environment.getExternalStorageState().equals("mounted")) {
                File localFile1 = Environment.getExternalStorageDirectory();
                File localFile2 = new File(localFile1.toString()
                        + "/Android/obb/" + str1);
                if (localFile2.exists()) {
                    if (i1 > 0) {
                        String str3 = localFile2 + File.separator + "main."
                                + i1 + "." + str1 + ".obb";
                        if (new File(str3).isFile()) {
                            localVector.add(str3);
                        }
                    }
                    if (i1 > 0) {
                        String str2 = localFile2 + File.separator + "patch."
                                + i1 + "." + str1 + ".obb";
                        if (new File(str2).isFile()) {
                            localVector.add(str2);
                        }
                    }
                }
            }
            String[] arrayOfString = new String[localVector.size()];
            localVector.toArray(arrayOfString);
            return arrayOfString;
        } catch (PackageManager.NameNotFoundException localNameNotFoundException) {
        }
        return new String[0];
    }

加密生成校验码算法：

    /**
     * 通过obb文件获取加密MD5
     * @param paramString
     * @return
     */
    private static String getMd5(String paramString) {
        try {
            Log.d("WARX", "path = " + paramString);
            MessageDigest localMessageDigest = MessageDigest.getInstance("MD5");
            FileInputStream localFileInputStream = new FileInputStream(
                    paramString);
            long lenght = new File(paramString).length();
            localFileInputStream.skip(lenght - Math.min(lenght, 65558L));
            byte[] arrayOfByte = new byte[1024];
            for (int i2 = 0; i2 != -1; i2 = localFileInputStream
                    .read(arrayOfByte)) {
                localMessageDigest.update(arrayOfByte, 0, i2);
            }
            BigInteger bi = new BigInteger(1, localMessageDigest.digest());
            Log.d("WARX", "md5 = " + bi.toString(16));
            return bi.toString(16);
        } catch (FileNotFoundException localFileNotFoundException) {
        } catch (IOException localIOException) {
        } catch (NoSuchAlgorithmException localNoSuchAlgorithmException) {

        }
        return null;
    }

这里主要是根据文件的长度生成的一个md校验码。

解析XML算法：

	private static Bundle getXml(Context context) {
        Bundle bundle = new Bundle();
        XmlPullParser localXmlPullParser;
        // int i1;
        String str;
        try {
            File localFile = new File(context.getPackageCodePath(),
                    "assets/bin/Data/settings.xml");
            Object localObject1;
            if (localFile.exists())

                localObject1 = new FileInputStream(localFile);
            else
                localObject1 = context.getAssets()
                        .open("bin/Data/settings.xml");

            XmlPullParserFactory localXmlPullParserFactory = XmlPullParserFactory
                    .newInstance();
            localXmlPullParserFactory.setNamespaceAware(true);
            localXmlPullParser = localXmlPullParserFactory.newPullParser();
            localXmlPullParser.setInput((InputStream) localObject1,null);
            int type = localXmlPullParser.getEventType();
            Object localObject2 = null;
            str = null;
            while (type!=1) {
                switch (type) {
                case 2:
                    if (localXmlPullParser.getAttributeCount()==0) {
                        type = localXmlPullParser.next();
                        continue;
                    }
                    str = localXmlPullParser.getName();
                    localObject2 = localXmlPullParser.getAttributeName(0);
                    if (!localXmlPullParser.getAttributeName(0).equals("name")){
                        type = localXmlPullParser.next();
                        continue;
                        }
                    localObject2 = localXmlPullParser.getAttributeValue(0);
                    if (str.equalsIgnoreCase("integer")) {
                        bundle.putInt((String) localObject2,
                                Integer.parseInt(localXmlPullParser.nextText()));
                    } else if (str.equalsIgnoreCase("string")) {
                        bundle.putString((String) localObject2,
                                localXmlPullParser.nextText());
                    } else if (str.equalsIgnoreCase("bool")) {
                        bundle.putBoolean((String) localObject2, Boolean
                                .parseBoolean(localXmlPullParser.nextText()));
                    } else if (str.equalsIgnoreCase("float")) {
                        bundle.putFloat((String) localObject2,
                                Float.parseFloat(localXmlPullParser.nextText()));
                    }
                    break;
                default:
                    break;
                }
                type = localXmlPullParser.next();
            }

        } catch (Exception localException) {
            localException.printStackTrace();
        }
        return bundle;
    }

这里将xml中的数据读取到一个Bundle中进行保存，Bundle内部实现是Map。最后我们可以将生成的校验码与setting.xml中获取的校验码进行对比，如果校验失败就可以启动下载流程了，下载完成后重启Activity，重新读取Obb文件并加载资源。

    /**
     * 重启Activity
     * @param context
     */
    public static void restartApplication(Activity context) {
        PackageManager packageManager = context.getPackageManager();
        Intent intent = packageManager.getLaunchIntentForPackage(context.getPackageName());
        ComponentName componentName = intent.getComponent();
        Intent mainIntent = IntentCompat.makeRestartActivityTask(componentName);
        mainIntent.addFlags(Intent.FLAG_ACTIVITY_NO_ANIMATION);
        context.startActivity(mainIntent);
        System.exit(0);
    }

##关于使用obb所涉及到的权限问题
最近需要把应用所用的权限最小化，那么获取obb是否需要权限，这是一个非常坑的东西，先看看官方的文档。
image.png

从文档上可以看出来，是android 6.0需要权限，除了6.0都无需权限，但是使用我们手里的6.0设备去尝试没有权限都可以下载obb正常进行游戏，但是使用google play下载之后部分6.0机型读取bugly上报访问obb路径被拒绝了，使用测试机也发现是偶发现象，下载了多次游戏，第一次的时候出现了访问路径拒绝，这就非常的蛋疼了。加上权限是肯定不会出问题的，我们剔除权限前游戏从未上报过这个问题，目前我们是增加了用户读取内存权限解决问题。


##小结

1. unity obb分包  obb文件会进行校验，如果obb有变化只能重新生成apk+obb。
2. unity会自动load obb。 Application.streamingAssetsPath的路径会自动切换 比如
	obb 模式： jar:file:///storage/emulated/0/Android/obb/com.linyou.ssss/main.218.com.linyou.ssss.obb!/assets/
	非Obb模式：jar:file:///data/app/package name-1/base.apk!/assets 

MARK资料链接：
[https://www.jianshu.com/p/af3f8e8f2a96](https://www.jianshu.com/p/af3f8e8f2a96)

[https://docs.unity3d.com/2017.4/Documentation/Manual/android-OBBsupport.html](https://docs.unity3d.com/2017.4/Documentation/Manual/android-OBBsupport.html)
