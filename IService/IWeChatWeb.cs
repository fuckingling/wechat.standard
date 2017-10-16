using System;
using System.Collections.Generic;
using System.Text;
using WeChat.Standard.Model;
using WeChat.Standard.Model.Share;

namespace WeChat.Standard.IService
{
    public interface IWeChatWeb
    {
        /// <summary>
        /// 初始化
        /// </summary>
        IWeChatWeb init(WeChatInfo info);
        /// <summary>
        /// 网页授权
        /// </summary>
        /// <param name="url">授权回跳地址</param>
        /// <param name="stata">授权回传参数</param>
        /// <param name="agree">是否弹出用户提示框</param>
        void auth(string url, string stata = "", bool agree = true);
        /// <summary>
        /// 根据URL算出wx.config的几个参数
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        JsShare getJsShareConfig(string url);
        /// <summary>
        /// 带参数二维码图片地址
        /// </summary>
        /// <param name="scene_id"></param>
        /// <param name="expire_seconds"></param>
        /// <returns></returns>
        string getQRcodeImgSrc(int scene_id, int expire_seconds);
        /// <summary>
        /// 远程图片转发 解决COCOS 跨域
        /// </summary>
        /// <param name="url">图片地址</param>
        /// <returns></returns>
        byte[] getImg(string url);
        
        /// <summary>
        /// 授权仅仅获取用户openid
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        string getOpenidOnly(string code);
    }
}
