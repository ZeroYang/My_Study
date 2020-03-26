# -*- coding: UTF-8 -*-
import requests
import threading
import re
import time
import os

all_urls = []
all_img_urls = []
g_lock = threading.Lock()
pic_links = []

class Spider():
    def __init__(self, target_url, headers):
        self.target_url = target_url
        self.headers = headers

    def getUrls(self, start_page, page_num):
        global all_urls
        for i in range(start_page, page_num+1):
            url = self.target_url % i
            all_urls.append(url)

class Consumer(threading.Thread):
    def run(self):
        headers = {
            'User-Agent': 'Mozilla/5.0 (X11; Linux x86_64; rv:52.0) Gecko/20100101 Firefox/52.0',
            'HOST': 'www.meizitu.com'
        }
        global all_img_urls
        print("%s is running " % threading.current_thread())
        while len(all_img_urls) > 0 :
            g_lock.acquire()
            img_url = all_img_urls.pop()
            g_lock.release()
            try:
                response = requests.get(img_url, headers = headers)
                response.encoding = 'gb2312'
                title = re.search('<title>(.*?) | 妹子图</title>',response.text).group(1)
                all_pic_src = re.findall('<img alt=.*?src="(.*?)" /><br />',response.text,re.S)
                pic_dict = {title:all_pic_src} #字典
                global pic_links
                g_lock.acquire()
                pic_links.append(pic_dict)
                print(title + "获取成功")
                g_lock.release()

            except:
                pass
            time.sleep(0.5)


class DownPic(threading.Thread):
    def run(self):
        headers = {
            'User-Agent': 'Mozilla/5.0 (X11; Linux x86_64; rv:52.0) Gecko/20100101 Firefox/52.0',
            'HOST': 'mm.chinasareview.com'
        }
        while True:
            global pic_links
            g_lock.acquire()
            if len(pic_links) == 0 :
                g_lock.release()
                continue
            else:
                pic = pic_links.pop()
                g_lock.release()
                for key,values in pic.items():
                    path = key.rstrip("\\") #删除指定字符
                    is_exists = os.path.exists(path)
                    if not is_exists:
                        os.makedirs(path)
                        print(path+'目录创建成功')
                    else:
                        print(path + '目录已存在')
                    for pic in values:
                        filename = path+"/"+pic.split('/')[-1]
                        if os.path.exists(filename):
                            continue
                        else:
                            # response = requests.get(pic, headers = headers)
                            # with open(filename, 'wb') as f:
                            #     f.write(response.content)
                            #     f.close
                            try:
                                response = requests.get(pic, headers=headers)
                                with open(filename, 'wb') as f:
                                f.write(response.content)
                                f.close
                            except Exception as e:
                                print(e)
                                pass

class Producer(threading.Thread):
    def run(self):
        headers = {
            'User-Agent': 'Mozilla/5.0 (X11; Linux x86_64; rv:52.0) Gecko/20100101 Firefox/52.0',
            'HOST': 'www.meizitu.com'
        }
        global all_urls
        print("%s is running " % threading.current_thread)
        while len(all_urls) > 0 :
            g_lock.acquire()
            page_url = all_urls.pop()
            g_lock.release()
            try:
                print("分析"+page_url)
                response = requests.get(page_url, headers=headers, timeout=3)
                all_pic_link = re.findall('<a target=\'_blank\' href="(.*?)">',response.text,re.S)
                global all_img_urls
                g_lock.acquire()
                all_img_urls += all_pic_link
                print(all_img_urls)
                g_lock.release()
                time.sleep(0.5)
            except:
                pass

if __name__ == "__main__":
# if __name__ == "__main__":
    headers = {
        'User-Agent': 'Mozilla/5.0 (X11; Linux x86_64; rv:52.0) Gecko/20100101 Firefox/52.0',
        'HOST': 'www.meizitu.com'
    }
    target_url = 'http://www.meizitu.com/a/pure_%d.html'  # 图片集和列表规则

    spider = Spider(target_url, headers)
    spider.getUrls(1, 16)
    #print(all_urls)

    threads = []
    for x in range(2):
        t = Producer()
        t.start()
        threads.append(t)

    for tt in threads:
        tt.join()

    print("进行到我这里了")

    for x in range(10):
        ta = Consumer()
        ta.start()

    for x in range(10):
        down = DownPic()
        down.start()