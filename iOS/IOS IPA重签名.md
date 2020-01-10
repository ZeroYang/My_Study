#ios IPA重签名

> 站在巨人的肩膀上

参考链接

[iOS ipa包重签名（sigh）](https://www.jianshu.com/p/4d90d4630f11)

[iOS 超级签名详解](https://www.cnblogs.com/huadeng/p/11557679.html)

## 环境准备
安装brew
    
    xcode-select --install
    
    ruby -e "$(curl -fsSL https://raw.githubusercontent.com/Homebrew/install/master/install)"


安装ruby

    brew install ruby

安装sigh

    sudo gem install -n /usr/local/bin sigh

## 问题记录

xcode-select --install

    xcode-select --install
    xcode-select: error: command line tools are already installed, use "Software Update" to install updates


解决方法，删除原有的重新安装
    sudo rm -rf /Library/Developer/CommandLineTools


sudo gem install -n /usr/local/bin sigh

    zwwxdeiMac:~ zwwx$ sudo gem install -n /usr/local/bin sigh
    Building native extensions.  This could take a while...
    ERROR:  Error installing sigh:
    	ERROR: Failed to build gem native extension.
    
    current directory: /Library/Ruby/Gems/2.3.0/gems/unf_ext-0.0.7.6/ext/unf_ext
    /System/Library/Frameworks/Ruby.framework/Versions/2.3/usr/bin/ruby -r ./siteconf20200110-60502-bav8nr.rb extconf.rb
    mkmf.rb can't find header files for ruby at /System/Library/Frameworks/Ruby.framework/Versions/2.3/usr/lib/ruby/include/ruby.h
    
    extconf failed, exit code 1
    
    Gem files will remain installed in /Library/Ruby/Gems/2.3.0/gems/unf_ext-0.0.7.6 for inspection.
    Results logged to /Library/Ruby/Gems/2.3.0/extensions/universal-darwin-18/2.3.0/unf_ext-0.0.7.6/gem_make.out


我的Mac原本已经装了ruby，直接跳过，进行sigh的安装。上面错误断定是ruby环境的问题，重新安装ruby,按提示设置ruby的环境变量解决问题

    zwwxdeiMac:/ zwwx$ brew install ruby
    Updating Homebrew...
    Warning: ruby 2.6.5 is already installed and up-to-date
    To reinstall 2.6.5, run `brew reinstall ruby`
    zwwxdeiMac:/ zwwx$ brew reinstall ruby


# 演示

1. 准备重签名的ipa 注意IPA文件名不能太长包含空格等，测试发现会导致读取IPA文件失败
2. 准备Provisioning Profile

...
	zwwxdeiMac:~ zwwx$ sigh resign

	[WARNING] You are calling sigh directly. Usage of the tool name without the `fastlane` prefix is deprecated in 	fastlane 2.0
	Please update your scripts to use `fastlane sigh resign` instead.
	[15:24:55]: Path to ipa file: /Users/zwwx/Documents/work_my/moyu.ipa 

	[15:25:07]: Available identities: 

	iPhone Distribution: yaru gao (*)
		BF8FB9B3C5C2DECE9B2D8B15D71D870517DA2CA8
	iPhone Developer: chunguang dong (*)
		FA2A605F843A75B2DD21536BCFC877FAF847DCA1
	iPhone Developer: Nathaniel Kier (*)
		11F29F0759D54566DFA204E74BEDE04B11056F13
	iPhone Developer: Amelia Lewis (*)
		C3EB8872821160351A1756E10C9BAF184305B3DD
	iPhone Developer: yaru gao (*)
		13E4EAAD6AF9066CE483778AC10F3A5F2CBF89A6
	iPhone Developer: I- WEN HAN (*)
		5314F5D017EBA34A4125D99AD12E64B79856EEFE
	iPhone Distribution: I- WEN HAN (*)
		B3BAA5D5B9AE03C2CE84659F46BFBE3AB37B575E
	iPhone Developer: jiakang shi (*)
		C33B697219104740BF79517A56EF1C4375C76AB5
	iPhone Developer: luoxing liu (*)
		F9CDB0030C6E3A2495736B992965D686550AAB44
	iPhone Distribution: Skyline Matrix Korea (*)
		517DF9755FD6940625F4BDDC6E9ECD6B4F649F7F
	iPhone Developer: Hao Li (*)
		1FB93756E1A0FA4E2B9A0AFB61EEDC5DA1D5E5FB
	Apple Development: Zero Yang (*)
		A678A9A60B088DB8D1A53DA3682A70850F0F3B24
		653CF2B603871C46CBC07B3E1000372B8E72B8D4

    [15:25:07]: Signing Identity: iPhone Developer: Amelia Lewis (U5CZMVVUY3)
    [15:25:23]: Path to provisioning file: /Users/zwwx/Documents/work_ssss/sdxy.mobileprovision
    /usr/local/lib/ruby/gems/2.6.0/gems/fastlane-2.140.0/sigh/lib/assets/resign.sh /Users/zwwx/Documents/work_my/moyu.ipa C3EB8872821160351A1756E10C9BAF184305B3DD -p /Users/zwwx/Documents/work_ssss/sdxy.mobileprovision  /Users/zwwx/Documents/work_my/moyu.ipa
    _floatsignTemp/Payload/moyu.app: replacing existing signature

