<div align="center">
<br/><br/><br/>
<!-- <img style="height:60px" src="https://www.atmb.top/favicon.ico"> -->


# [秋之盒-AutumnBox](http://www.atmb.top)
免费,简单,易用的图形化ADB工具箱。   
<br/>

## 贡献者
![](https://opencollective.com/AutumnBox/contributors.svg?button=false)

<br/>



[![](https://github.com/zsh2401/AutumnBox/workflows/Build%20and%20Test/badge.svg)](https://github.com/zsh2401/AutumnBox/actions?query=workflow%3ABuild+and+Test)
[![](https://github.com/zsh2401/AutumnBox/workflows/Canary/badge.svg)](https://github.com/zsh2401/AutumnBox/releases)


![](https://img.shields.io/badge/C%23-8.0-brightgreen.svg)
![](https://img.shields.io/badge/GUI-WPF-blue.svg)
[![](https://img.shields.io/badge/开发者群-153424015-orange.svg)](https://jq.qq.com/?_wv=1027&k=M6X9BBCR)

 </div>

## 简介
秋之盒是一个对[谷歌安卓调试桥(Android Debug Bridge)](https://developer.android.com/studio/command-line/adb/?gclid=CjwKCAjw4MP5BRBtEiwASfwAL0zKSD761d9W05bQmMc4Q7AjJSNvnfQrmdg3rvmfzkixmXfCqFbCyxoCu3gQAvD_BwE&gclsrc=aw.ds)命令行工具进行图形化封装的微软视窗操作系统应用。   
“方便小白，帮助老鸟”
- 技术关键词: C# / .NET 5 / WPF / ADB
- 支持的操作系统: Windows 7 - Windows 10
- 运行环境/框架技术: `.Net 5` (发布时已自带运行时)

![](https://atmb.top/assets/img/1.dedcd74c.png)


## 能做什么?
* 为您的设备刷入第三方Recovery
* 向设备推送文件
* 一键激活`黑域`服务
* 一键激活`冰箱`
* 解锁System,获取完整root控制权
* 以拓展模块为中心的功能开发思想,将来将会支持越来越多的功能


## 进行AutumnBox拓展模块开发
如果您也是一位开发者,欢迎下载`SDK`开始开发秋之盒拓展模块。   
[**AutumnBox开发文档**](https://atmb.top/dev/docs/)
### 为什么要开发秋之盒模块，而不是写bat/sh/ps1脚本？
* 无需关心用户设备连接情况
* 快速达成优雅的图形化交互
* 你可以根据需要构建自己的”秋之盒发行版“，并向特定用户群发布

## 对秋之盒主体进行贡献
### 1.克隆到本地
```sh
git clone https://github.com/zsh2401/AutumnBox.git
```
### 2.初始化ADB环境
在DEBUG秋之盒的过程中使用的ADB环境来自于外部仓库。在开始你的开发前，需要手动配置环境。
请在仓库根目录，使用`powershell`执行代码:
```sh
./scripts/get_adb.ps1
```
### 3.打开项目
使用`Visual Studio 2019`打开`src/AutumnBox.sln`即可。
### 4.编译与运行
将启动项目设置为`AutumnBox.GUI`，然后按下`F5`即可以调试模式启动`AutumnBox`。   
标准拓展模块会被./src/AutumBox.GUI/build_ext.ps1构建并放置到合适的文件夹，如果修改了模块代码，请重新生成整个项目。
### 5.贡献
#### 5.1 分支结构
目前秋之盒有以下主要分支:
* `master`，稳定可编译的代码，可供新入开发者进行最基础的调试，源码阅读。
* `dev`，最新的，正在开发的代码，通常领先于`master`，并会定期合并到`master`。
* `X.X.X-LTS`，特定LTS的维护分支，不再添加新的特性，只是进行问题修复与安全更新。   

被以二进制形式大规模分发的稳定版本通常来自于`master`分支。而`canary`版本则是来自于`dev`分支。    
#### 5.2 提交代码
请在`dev`分支的基础上进行开发，`Pull Request`也请提交到`dev`分支。