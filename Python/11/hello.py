# print("hello");
import urllib.request
import requests
import random
for i in range(5):
    print(random.randint(1,100))

# a = 'sunday'
# print('today is %s' % a)
# print("today is %r" % a)
#
# print( 'hello, {}, {}'.format('python', 2020))
# print('hello, {name}, {year}'.format(name='python', year=2020))
# print('hello, {:s}, {:d}'.format('python', 2020))
#
# list1 = [2020, 'python', 2017, 'python3', 1999, 'hello']
# print(list1[::2])
# list1.clear()
# print(list1)

# 三元表达式
y=5
x=-1 if y < 0 else 1
print(x)

# for else
for i in [1, 2, 3, 4]:
    if i > 2 :
        print(i)
        #break  #拆散for else
else:
    print(i, "我是else")

#列表推导式
a = [1, 2, 3, 4, 5]
result = [i*i for i in a]
print(result)

# response = urllib.request.urlopen('https://blog.csdn.net/weixin_43499626')
# print(response.read().decode('utf-8'))

def run():
    # response = requests.get('http://www.baidu.com')
    # print(response.text)
    headers = {
        "Host": "www.newsimg.cn",
        "User-Agent": "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/63.0.3239.26 Safari/537.36 Core/1.63.5383.400 QQBrowser/10.0.1313.400"
    }
    # response = requests.get("http://www.newsimg.cn/big201710leaderreports/xibdj20171030.jpg")
    # with open("xijinping.jpg", "wb") as f:
    #     f.write(response.content)
    #     f.close

# if __name__ == "__main__":
#     run()

if __name__ == "__main__":
    run()