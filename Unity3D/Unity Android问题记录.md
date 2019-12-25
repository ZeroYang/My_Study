#Unity Android问题记录

##Unity游戏在Android上运行按Home或锁屏键不能后台运行的解决方案

解决方法很简单，在android项目AndroidManifest.xml文件中的activity中添加如下内容：

    android:configChanges="fontScale|keyboard|keyboardHidden|locale|mnc|mcc|navigation|orientation|screenLayout|screenSize|smallestScreenSize|uiMode|touchscreen" 

这个设置是很全的一个，基本上保证了你随便折腾，都不会重新运行游戏。

	VALUE   DESCRIPTION
	"mcc"   国际移动用户识别码所属国家代号是改变了----- sim被侦测到了，去更新mccmcc是移动用户所属国家代号  
	"mnc"   国际移动用户识别码的移动网号码是改变了------ sim被侦测到了，去更新mncMNC是移动网号码，最多由两位数字组成，用于识别移动用户所归属的移动通信网  
	"locale"    地址改变了  
	"touchscreen"   触摸屏是改变了------通常是不会发生的  
	"keyboard"  键盘发生了改变----例如用户用了外部的键盘  
	"keyboardHidden"    键盘的可用性发生了改变  
	"navigation"    导航发生了变化-----通常也不会发生  
	"screenLayout"  屏幕的显示发生了变化------不同的显示被激活  
	"fontScale" 字体比例发生了变化----选择了不同的全局字体  
	"uiMode"    用户的模式发生了变化  
	"orientation"   屏幕方向改变了  
	"screenSize"    屏幕大小改变了  
	"smallestScreenSize" 屏幕的物理大小改变了，如：连接到一个外部的屏幕上
游戏重新启动，是因为上述原因发生变化后，导致activity的生命周期重新运行，执行了onCreate()方法。游戏中用不到这么多设置，需要哪个设置哪个就好了。

## Unity游戏在Android上运行监听不到Home键或Back键的解决办法

在extends UnityPlayerActivity的activity中直接Override onBackPressed方法 监听不到back按键

	  @Override
	  public void onBackPressed()
	  {
		exit();
	  }

解决办法，在onKeyDown方法中监听具体的按键可以监听到

	@Override
	public boolean onKeyDown(int keyCode, KeyEvent event)  {
	  if (keyCode == KeyEvent.KEYCODE_BACK) {
	      Log.d(TAG, "exitGame()");
	      exit();
	  return true;
	  }
	  return super.onKeyDown(keyCode, event);
	}