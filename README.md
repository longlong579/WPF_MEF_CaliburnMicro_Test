# WPF_MEF_CaliburnMicro_Test
WPF程序 =》MEF +Caliburation 
内置常用的Log4Net配置，自己使用时，请注意修log4net.config中的环境变量MEF_EXPORTED_HOM
配置了在WPF中使用Console，输出信息到命令窗口，当然用log4net也可以（.config文件中已经配置完毕）
参考以下3片文档即可了解大部分知识点

参考：1》https://docs.microsoft.com/zh-cn/archive/msdn-magazine/2010/february/managed-extensibility-framework-building-composable-apps-in-net-4-with-the-managed-extensibility-framework#%E7%A8%B3%E5%AE%9A%E7%9A%84%E7%BB%84%E5%90%88%E6%8B%92%E7%BB%9D%E5%92%8C%E8%AF%8A%E6%96%AD

2》https://www.cnblogs.com/pszw/category/335788.html

3》https://www.cnblogs.com/landeanfen/p/4848885.html

#注意点
MEF_Test 和MEF_Exported相对应，一个是界面，功能利用接口完成，另一个是服务的实际提供者。从而实现解耦。部署项目时，只要接口不变，则可以只替换MEF_Exported.dll,不必重新编译整个软件

1.Imort必须被CompositionContainer管理
2.懒加载Lazy<T>/Lazy<T,V>实现需要时才加载
3.元素组使用特性修饰，在多个导出时
4.属性注入和构造函数注入的区别与应用
