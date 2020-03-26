import requests

# 模拟成浏览器访问的头
headers = {'User-Agent':'Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/55.0.2883.87 Safari/537.36'}
response = requests.get('http://www.baidu.com', headers = headers)
# print(response.status_code)
# print(type(response.text))
# print(response.text)
# print(response.cookies)
print(response.headers)

