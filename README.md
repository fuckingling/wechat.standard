# wechat.standard 多用户微信公众号开发
Wechat Api For .net core
包含 微信订阅号、服务号开发，小程序开发、微信支付等集成
由于是多用户 所以每个接口 使用前需调用Init方法   如果是单用户场景可以自行注入：
services.AddOptions().Configure<WeChatInfo>(Configuration.GetSection("WeChatInfo"));
## 调用方法：
message send info 自行注入 </br>
关注事件回复图文消息 例如：</br>
message.init(info.Value);</br>
send.init(info.Value);</br>
message.subscribeMsg((msg) =></br>
{</br>
  var list =new List<ResponseItem>();</br>
  var item = new ResponseItem {</br>
  Description="xxxxx",</br>
  PicUrl="https://xxxx.xx.xx",</br>
  Title="欢迎关注xxx"</br>
  };</br>
  list.Add(item);</br>
  send.newsMsg(msg.FromUserName, list);</br>
});</br>
