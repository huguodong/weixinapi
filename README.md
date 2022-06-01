<h1>一、前言</h1>
<p style="margin-left: 30px;"><span style="font-size: 16px;">因为工作需要，开始接触微信公众号开发，一开始根据官网的教程和百度的教程，勉强把官网教程的服务器配置完成。后来听说有专门的微信公众号开发<a href="https://weixin.senparc.com/" target="_blank" rel="noopener">盛派SDK</a>，于是就去研究了一下这个SDK，可能是历史原因，网上都说这个SDK非常的臃肿，而且想下载个demo运行一下都非常的麻烦，好不容易demo下载下来运行起来，结果有些东西跟预想的有些差距，比如接口中获取PostModel,demo中并没有加[FromQuery]特性导致根本拿不到这个参数,并且demo没有说明文档，新手看起来比较乱。。。。。。所以我还是决定通过自己搭建项目加使用盛派SDK的一些封装好的功能模块结合起来开发比较好，正好.NET6也出来了，网上好像也很少.NET6开发公众号的文章，所以决定继续把博客园捡起来，希望能把公众号系列写完，当然如果有人能根据我的项目结构速度比我快替我写也行~~。</span></p>
<h1>二、章节导航</h1>
<p><span style="font-size: 18pt;"><a href="https://www.cnblogs.com/huguodong/p/16287223.html" target="_blank" rel="noopener">&nbsp;开发微信公众号系列之二：搭建项目</a></span></p>
<p><span style="font-size: 18pt;"><a href="https://www.cnblogs.com/huguodong/p/16288767.html" target="_blank" rel="noopener">&nbsp;开发微信公众号系列之三：接入公众号</a></span></p>
<p><span style="font-size: 18pt;"><a href="https://www.cnblogs.com/huguodong/p/16304377.html" target="_blank" rel="noopener">&nbsp;开发微信公众号系列之四：自动回复</a></span></p>
<p><span style="font-size: 18pt;"><a href="https://www.cnblogs.com/huguodong/p/16305579.html" target="_blank" rel="noopener">&nbsp;开发微信公众号系列之五：自定义MessageService来处理消息</a></span></p>
<p><span style="font-size: 18pt;"><a title="开发微信公众号系列之六：其他消息处理" href="https://www.cnblogs.com/huguodong/p/16308142.html" target="_blank" rel="noopener">开发微信公众号系列之六：普通消息处理</a></span></p>
<p><span style="font-size: 18pt;"><a class="postTitle2 vertical-middle" href="https://www.cnblogs.com/huguodong/p/16330508.html">开发微信公众号系列之七：生成带参数的二维码</a></span></p>
<p><span style="font-size: 24px;"><a class="postTitle2 vertical-middle" href="https://www.cnblogs.com/huguodong/p/16316790.html">开发微信公众号系列之八：自定义菜单</a></span></p>
<p><span style="font-size: 24px;"><a class="postTitle2 vertical-middle" href="https://www.cnblogs.com/huguodong/p/16327311.html">开发微信公众号系列之九：事件推送</a></span></p>
<p><span style="font-size: 24px;"><a class="postTitle2 vertical-middle" href="https://www.cnblogs.com/huguodong/p/16334346.html">开发微信公众号系列之十：模板消息</a></span></p>
<h1>三、Git地址</h1>
<p><span style="font-size: 18pt;"><a href="https://gitee.com/huguodong520/weixinapi" target="_blank" rel="noopener">https://gitee.com/huguodong520/weixinapi</a></span></p>
<h1>四、友情链接</h1>
<p><span style="font-size: 18pt;"><a href="https://sdk.weixin.senparc.com/" target="_blank" rel="noopener">https://sdk.weixin.senparc.com/</a></span></p>
<p><span style="font-size: 18pt;"><a href="https://developers.weixin.qq.com/doc/offiaccount/Getting_Started/Overview.html" target="_blank" rel="noopener">https://developers.weixin.qq.com/doc/offiaccount/Getting_Started/Overview.html</a></span></p>
<p><span style="font-size: 18pt;"><a href="https://dotnetchina.gitee.io/furion/docs/" target="_blank" rel="noopener">https://dotnetchina.gitee.io/furion/docs/</a></span></p>
<p><span style="font-size: 18pt;"><a href="https://www.cnblogs.com/szw/archive/2013/05/14/weixin-course-index.html" target="_blank" rel="noopener">https://www.cnblogs.com/szw/archive/2013/05/14/weixin-course-index.html</a></span></p>
<p><a href="https://www.donet5.com/Doc/1/1180" target="_blank" rel="noopener"><span style="font-size: 18pt;">https://www.donet5.com/Doc/1/1180</span></a></p>
<p><a href="https://dotnetchina.gitee.io/furion/" target="_blank" rel="noopener"><span style="font-size: 18pt;">https://dotnetchina.gitee.io/furion/</span></a></p>
