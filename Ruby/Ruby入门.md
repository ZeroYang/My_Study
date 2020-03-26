#Ruby 入门

##Ruby 环境

Mac 系统自带Ruby环境。

只需要在命令提示符中键入 irb，一个交互式 Ruby Session 将会开始，如下所示：

    zwwxdeiMac:~ zwwx$ irb
    irb(main):001:0> def hello
    irb(main):002:1> out = "hello world"
    irb(main):003:1> puts out
    irb(main):004:1> end
    => :hello
    irb(main):005:0> hello
    hello world
    => nil
    irb(main):006:0> exit
	zwwxdeiMac:~ zwwx$ 

* Ruby 版本

    zwwxdeiMac:~ zwwx$ ruby -v
    ruby 2.6.5p114 (2019-10-01 revision 67812) [x86_64-darwin18]
    zwwxdeiMac:~ zwwx$ 

* Ruby 文件执行

    zwwxdeiMac:ruby_study zwwx$ ruby test.rb 
    Hello, Ruby!
    zwwxdeiMac:ruby_study zwwx$ 

* Ruby 中执行shell脚本

    system './copy.sh'

##简单用例
    
* 注释&多行注释

	    #!/usr/bin/ruby -w
	     
	    #puts "Hello, Ruby!"
     
	    =begin
	    这是一个多行注释。
	    可扩展至任意数量的行。
	    但 =begin 和 =end 只能出现在第一行和最后一行。 
	    =end

* if-else

		#!/usr/bin/ruby
		# -*- coding: UTF-8 -*-
		 
		x=1
		if x > 2
		   puts "x 大于 2"
		elsif x <= 2 and x!=0
		   puts "x 是 1"
		else
		   puts "无法得知 x 的值"
		end

* for

	    #!/usr/bin/ruby
	    # -*- coding: UTF-8 -*-
	     
	    for i in 0..5
	       puts "局部变量的值为 #{i}"
	    end


## RUBY获取当前的执行文件的路径和目录

获取当前的文件名称

    puts   __FILE__  
获取当前文件的目录名称

    puts File.dirname(__FILE__)
获取当前文件的完整名称
当要获取完整的路径时需要`require` `'pathname'`,代码如下：

    require ‘pathname’
    puts Pathname.new(__FILE__).realpath    
获取当前文件的完整目录

    require ‘pathname’
    puts Pathname.new(File.dirname(__FILE__)).realpath

## Ruby执行shell脚本

