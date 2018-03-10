var isOnline = 0;
var curUrl = window.location.href.toLowerCase();


//活动信息
var year = "2017";  //活动年份
var Remark = "世贸光合教育";  //活动年份
var subimitType = "1";
//=========================================================授权=================================================
/*定义服务号ID*/
var authOpenID = "gh_db98c1853c28";
var shareOpenID = "gh_43cc8fd12f5a";

/*微信强制授权*/
function getWechatAuth() {
    //    window.sessionStorage.yhc0207_OpenID = "ovVKYjkXnG4I8VsEZ7tRmQh98pQI";
    if (window.sessionStorage.shimao360_OpenID == null || window.sessionStorage.shimao360_OpenID == "" || window.sessionStorage.shimao360_OpenID == "undefined") {
        wx_auth.config({
            state: authOpenID   //授权公众号id
            , debug: false        //是否测试，默认true有弹窗消息
            , auth: true         //是否强制授权，默认false
        });
        //授权配置，配置成功后自动开始授权跳转，可以默认什么参数都不写，就是用公司的授权
        wx_auth.ready(function () {
            //wx_contact可用属性：city; comopenid; country; headimgurl; language; nickname; openid; province; sex; subscribe; subscribe_time; unionid
            window.sessionStorage.shimao360_OpenID = wx_contact.openid;
            window.sessionStorage.shimao360_NickName = wx_contact.nickname;
            window.sessionStorage.shimao360_HeadImg = wx_contact.headimgurl;
            //alert(window.sessionStorage.baoming_OpenID)
            globalInit();
        });
    }
    else {
        globalInit();
    }
}
/*全局加载*/
function globalInit() {
    //alert("本次活动已结束，感谢您的关注！");
    //alert(window.sessionStorage.yhc0207_OpenID);
    saveWechatInfo(window.sessionStorage.shimao360_OpenID, window.sessionStorage.shimao360_NickName, window.sessionStorage.shimao360_HeadImg);
    
}

function alertTips() {
    alert("本活动处于测试阶段，\r\n请勿转发朋友圈！");
}

$(document).ready(function () {
    if (isOnline == 0) {
        alertTips();
    }
    getWechatAuth();


});


//分享的成功后调用的方法
wx_share.success(function () {
    //    alert('分享成功！');
});
//分享的失败后调用的方法
wx_share.cancel(function () {
    //    alert('分享取消！');
});
//分享配置方法
if (curUrl.indexOf("tour.html") > -1) {
    wx_share.config({
        state: 'gh_e89dba73fb6f'            //分享的公众号id
    , debug: false                         //测试弹窗信息，默认true
        //, config_debug: false               //微软官方的js-sdk配置是否开启，分享失败的时候可以尝试开启调试
        //, domain: 'weixin.juzhen.com'      //分享程序所在的域名，默认不修改
    , img: 'http://shimao.juzhen01.com/2017/shimao360/fx1.jpg'
    , url: 'http://shimao.juzhen01.com/2017/shimao360/share.shtml'
    , title: '有了它，什么家委会竞选都不怕！'
    , desc: '世茂集团邀您共同焕新光合教育2.0！'
    });
} else {
    wx_share.config({
        state: 'gh_e89dba73fb6f'            //分享的公众号id
    , debug: false                         //测试弹窗信息，默认true
        //, config_debug: false               //微软官方的js-sdk配置是否开启，分享失败的时候可以尝试开启调试
        //, domain: 'weixin.juzhen.com'      //分享程序所在的域名，默认不修改
    , img: 'http://shimao.juzhen01.com/2017/shimao360/fx.jpg'
    , url: 'http://shimao.juzhen01.com/2017/shimao360/share.shtml'
    , title: '这一次，我们重新定义“光合作用”。'
    , desc: '每颗种子，都值得期待。'
    });
}



/*保存微信信息到目标服务器*/
function saveWechatInfo(openid, nickname, headimg) {
    $.ajax({
        type: "Post",
        url: "Form/formAjax.aspx",
        data: {
            "Type": "saveweixin",
            "OpenID": openid,
            "NickName": nickname,
            "HeadImg": headimg,
            "Remark": Remark,
            "Random": Math.random() * 10000
        },
        dataType: "json",
        async: false,
        cache: false,
        beforeSend: function () { },
        success: function (result) {
            //alert(result.ErrorCode);
            if (result.ErrorCode == 1 || result.ErrorCode == 2) {
                //微信信息保存完成
                return;
            }
            else if (result.ErrorCode == 3) {
                return;
            }
            else if (result.ErrorCode == 8) {
                alert("本次活动已结束，感谢您的关注");
                return;
            }
            else if (result.ErrorCode == 0) {
                alert("请使用微信登陆");
            }
            else {

                return;
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            //alert(XMLHttpRequest.responseText);
        }
    });
}