# SeatSearch
座位查询项目
用的是webapi使用
大致的思路是先webapi写一个查询数据库的程序，
再sqlite导入提前准备的表格，与程序进行交互无问题后，
进行网页编写(注意移动端界面部署)，把做好的界面放到webapi下，确保本地无误(无跨域问题)
然后再云服务上面打相关的环境和打开对应接口，用的windowserver服务,打开iis挂载应用
然后把链接生成二维码，给宾客扫描查询座位。
