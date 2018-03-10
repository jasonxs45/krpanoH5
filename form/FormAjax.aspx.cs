using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class _2017_FormAjax : System.Web.UI.Page
{
    string returnMsg = "";
    private System.Diagnostics.PerformanceCounter performanceCounter1;
    DateTime endtime = new DateTime(2017, 11, 29, 23, 59, 59);
    string tablename = "Event_2017_shimao360";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.RequestType.ToLower() == "post" && Request.UrlReferrer != null)
        {
            string functionName = Request.Form["Type"].Trim().ToLower();
            switch (functionName)
            {
                case "formsubimit":
                    returnMsg = FormSubimit();
                    break;
                case "saveweixin":
                    returnMsg = SaveWechatInfo();
                    break;
            };
            Response.Write(returnMsg);
            return;
        }
        else
        {
            Response.Write("非法操作，拒绝访问");
            return;
        }
    }

    //用户提交表单
    private string FormSubimit()
    {
        string i = "";
        //if (DateTime.Now > endtime)
        //{
        //    i = "{\"ErrorCode\":8,\"MessageText\":\"本次活动已过期\"}";
        //    return i;
        //}
        int people;
        int sql;
        string openid = SiteFun.cutechar(Request.Form["OpenID"]);
        string name = SiteFun.cutechar(Request.Form["Name"]);
        string telephone = SiteFun.cutechar(Request.Form["Tel"]);
        string Costumer = SiteFun.cutechar(Request.Form["Costumer"]);
        string City = SiteFun.cutechar(Request.Form["City"]);
        string Favors = SiteFun.cutechar(Request.Form["Favors"]).TrimEnd('|');
        string Remark = SiteFun.cutechar(Request.Form["Remark"]);
        int count = SiteFun.objectToInt(DbHelperSQL.GetSingle("select count(telephone) from " + tablename + " where openid='" + openid + "'"), 0);
       
        if (count > 0)
        {
            i = "{\"ErrorCode\":4,\"MessageText\":\"重复报名\"}";
            return i;
        }
        people = SiteFun.objectToInt(DbHelperSQL.GetSingle("select count(telephone) from " + tablename), 0);
        //if (people >= 100)
        //{
        //    i = "{\"ErrorCode\":3,\"MessageText\":\"报名人数已满\"}";
        //}
        //else
        //{
            sql = SiteFun.objectToInt(DbHelperSQL.GetSingle("select count(1) from " + tablename + " where TelePhone='" + telephone + "' and remark='" + Remark+"'"), 0);
            //判断手机是否被使用过
            if (sql > 0)
            {
                i = "{\"ErrorCode\":2,\"MessageText\":\"手机已被报名\"}";
            }
            else
            {
                DbHelperSQL.ExecuteSql("update " + tablename + " set name='" + name + "',telephone='" + telephone + "',Costumer='" + Costumer + "',City='" + City + "',Project='" + Favors + "' where openid='" + openid + "' and remark='" + Remark + "'");
                //DataTable dt = DbHelperSQL.Query("select * from " + tablename + " where OpenId = '" + openid + "' and Remark ='" + Remark + "'").Tables[0];
                i = "{\"ErrorCode\":1,\"MessageText\":\"已成功报名\"}";
            }
        //}

        return i;
    }
    //微信用户授权验证
    private string SaveWechatInfo()
    {
        string i = "";
        if (DateTime.Now > endtime)
        {
            i = "{\"ErrorCode\":8,\"MessageText\":\"本次活动已过期\"}";
            return i;
        }
        if (string.IsNullOrEmpty(Request.Form["OpenID"]))
        {
            i = "{\"ErrorCode\":-1,\"MessageText\":\"授权失败，未获取用户微信基本信息\"}";
        }
        else
        {
            string openID = SiteFun.cutechar(Request.Form["OpenID"]);
            string nickName = SiteFun.cutechar(Request.Form["NickName"]);
            string headImg = Request.Form["HeadImg"];
            string Remark = Request.Form["Remark"];
            i = SaveWechatInfo(openID, nickName, headImg, Remark);

        }
        return i;
    }

    //验证用户是否报名方法
    public string SaveWechatInfo(string openid, string nickname, string headimg, string Remark)
    {
        int sql = SiteFun.objectToInt(DbHelperSQL.GetSingle("SELECT COUNT(1) FROM " + tablename + " WHERE  OpenId ='" + openid + "' and Remark ='" + Remark + "'"), 0);
        if (sql > 0)
        {
            string myName = SiteFun.objectToStr(DbHelperSQL.GetSingle("SELECT Name FROM " + tablename + " WHERE OpenId='" + openid + "' and Remark ='" + Remark + "'"));
            if (myName == "" || myName == null)
            {
                return "{\"ErrorCode\":2,\"MessageText\":\"已保存微信信息\"}";
            }
            else
            {
                DataTable dt = DbHelperSQL.Query("select * from " + tablename + " where OpenId = '" + openid + "' and Remark ='" + Remark + "'").Tables[0];
                return "{\"ErrorCode\":3,\"MessageText\":\"已提交用户信息，并且返回用户信息\",\"rows\":" + SiteFun.DataTable3Json(dt, "str") + "}";
            }
        }
        else
        {
            DbHelperSQL.ExecuteSql("INSERT INTO " + tablename + "([OpenId],[NickName],[HeadImg],[Remark],[AddTime]) VALUES ('" + openid + "','" + nickname + "','" + headimg + "','" + Remark + "',GETDATE())");
            return "{\"ErrorCode\":1,\"MessageText\":\"添加微信信息完成\"}";
        }
    }


}