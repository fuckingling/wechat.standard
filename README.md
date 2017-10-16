# wechat.standard 多用户微信公众号API开发
Wechat Api For .net core
包含 微信订阅号、服务号开发，小程序开发、微信支付等集成
由于是多用户 所以每个接口 使用前需调用Init方法   如果是单用户场景可以自行注入：
services.AddOptions().Configure<WeChatInfo>(Configuration.GetSection("WeChatInfo"));
## 调用方法：
我写在中间件中 例如：
message.init(info.Value);
                    send.init(info.Value);
                    //微信绑定
                    message.baseMsg((msg) =>
                    {
                        userservice.addWechatUser(msg.FromUserName);
                    });
                    message.subscribeMsg((msg) =>
                    {
                        var list =new List<ResponseItem>();
                        var item = new ResponseItem {
                            Description="xxxxx",
                            PicUrl="https://xxxx.xx.xx",
                            Title="欢迎关注xxx"
                        };
                        list.Add(item);
                        send.newsMsg(msg.FromUserName, list);
                    });
