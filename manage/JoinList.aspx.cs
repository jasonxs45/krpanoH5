using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Text;
public partial class HousingTest2016_JoinList : System.Web.UI.Page
{
    string remark = "世贸光合教育";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //ControlBind();
            PageInit();
            //string number = SiteFun.objectToStr(DbHelperSQL.GetSingle("select count(1) from Event_wx_baoming_JoinList where Year =2017 and Remark = '银湖城报名活动2' and Age='2月25日'"));
            //this.number.Text = number;
            //string number1 = SiteFun.objectToStr(DbHelperSQL.GetSingle("select count(1) from Event_wx_baoming_JoinList where Year =2017 and Remark = '银湖城报名活动2' and Age='2月26日'"));
            //this.number1.Text = number1;s
        }
        this.title.Text = "数据列表";
    }
    protected void PageInit()
    {
        //string aa=Request.Form["page"];
        //string bb=Request.QueryString["page"];
        //string cc=Request["page"];
        //Response.Write(aa+"/"+bb+"/"+cc);
        //当前页
        int itPageIndex = 1;
        //总页数
        int itPageCount;
        //总条数
        int itDataCount;
        string strSqlWhere = "Remark = '" + remark+"'";
        string strSort = "id desc";
        if (Request["page"] != null && Request["page"] != "")
        {
            try { itPageIndex = int.Parse(Request["page"]); }
            catch { itPageIndex = 1; }
        }

        if (Request["value"] != null && Request["value"] != "")
        {
            strSqlWhere += "AND " + Request["type"] + " LIKE '%" + Request["value"] + "%'";
            hidQuery.Value = hidQuery.Value + "&type=" + Request["type"] + "&value=" + Server.UrlEncode(Request["value"]);
        }

        rptData.DataSource = pageView(
            "Event_2017_shimao360",
            "*",
            "Id",
            //每页多少条
            10,
            //当前页
            itPageIndex,
            strSqlWhere,
            strSort,
            out itDataCount,
            out itPageCount,
            out itPageIndex);
        rptData.DataBind();
        pageBind(itPageIndex, itPageCount, itDataCount);
    }


    private DataTable pageView(string tablename, string column, string primarykey, int pagesize, int pageindex, string where, string order, out int DataCount, out int PageCount, out int PageIndex_p)
    {
        string sql;
        string sqlwhere;
        string sqlwhere2;
        string sqlorder;
        DataCount = PageCount = PageIndex_p = 0;
        if (where != "")
            sqlwhere = "where " + where + " ";
        else
            sqlwhere = "";

        Object objtmp = DbHelperSQL.GetSingle("select count(1) from " + tablename + " " + sqlwhere);
        if (objtmp != null)
            DataCount = (int)objtmp;
        else
            DataCount = 0;
        //算出总页数
        if (DataCount % pagesize != 0)
        {
            PageCount = (DataCount / pagesize) + 1;
        }
        else
            PageCount = DataCount / pagesize;
        //判断pageindex当前页的临界值
        if (pageindex > PageCount)
        {
            if (PageCount == 0)
                PageIndex_p = 1;
            else
                PageIndex_p = PageCount;
        }
        else if (pageindex < 1)
        {
            PageIndex_p = 1;
        }
        else
        {
            PageIndex_p = pageindex;
        }

        if (!string.IsNullOrEmpty(where))
        {
            sqlwhere = "where " + where + " and ";
            sqlwhere2 = "where " + where;
        }
        else
        {
            sqlwhere = "where ";
            sqlwhere2 = " ";
        }
        if (!string.IsNullOrEmpty(order))
        {
            sqlorder = " order by " + order;
        }
        else
        {
            sqlorder = " ";
        }
        sql = "select top " + pagesize.ToString() + " " + column + " from " + tablename + " " + sqlwhere + primarykey + " not in (select top " + (pagesize * (PageIndex_p - 1)).ToString() + " " + primarykey + " from " + tablename + " " + sqlwhere2 + sqlorder + " )" + sqlorder;

        DataTable dt = DbHelperSQL.Query(sql).Tables[0];
        return dt;
    }

    protected void ControlBind()
    {
        btnDel.Attributes.Add("onmouseover", "this.style.background='url(But01_over.gif) no-repeat'");
        btnDel.Attributes.Add("onmouseout", "this.style.background='url(But01_out.gif) no-repeat'");

    }
    protected void pageBind(int parPageIndex, int parPageCount, int parDataCount)
    {
        lblPageSize.Text = "10";
        lblPageIndex.Text = parPageIndex.ToString();
        lblPageCount.Text = parPageCount.ToString();
        lblDataCount.Text = parDataCount.ToString();
        if (parDataCount < 1)
        {
            hlFirst.Enabled = false;
            hlPre.Enabled = false;
            hlNext.Enabled = false;
            hlLast.Enabled = false;
        }
        else if (parPageIndex == 1 && parPageCount > 1)
        {
            hlFirst.Enabled = false;
            hlPre.Enabled = false;
            hlNext.Enabled = true;
            hlLast.Enabled = true;
        }
        else if (parPageIndex == 1 && parPageCount == 1)
        {

            hlFirst.Enabled = false;
            hlPre.Enabled = false;
            hlNext.Enabled = false;
            hlLast.Enabled = false;
        }
        else if (parPageIndex == parPageCount)
        {
            hlFirst.Enabled = true;
            hlPre.Enabled = true;
            hlNext.Enabled = false;
            hlLast.Enabled = false;
        }
        hlFirst.NavigateUrl = Request.Url.AbsolutePath + "?page=" + "1" + hidQuery.Value;
        hlPre.NavigateUrl = Request.Url.AbsolutePath + "?page=" + (parPageIndex - 1) + hidQuery.Value;
        hlNext.NavigateUrl = Request.Url.AbsolutePath + "?page=" + (parPageIndex + 1) + hidQuery.Value;
        hlLast.NavigateUrl = Request.Url.AbsolutePath + "?page=" + parPageCount + hidQuery.Value;
    }

    /// <summary>
    /// 行编辑事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    //protected void rptData_RowCommand(object sender, RepeaterCommandEventArgs e)
    //{
    //    string sql = "";
    //    if (e.CommandName == "未领取")
    //    {
    //        int state = Convert.ToInt32(DbHelperSQL.GetSingle("SELECT [State] FROM [Event_2017_whwanda0112_joinlist] WHERE [ID] = " + e.CommandArgument.ToString()));
    //        sql = "UPDATE [Event_2017_bl0120_JoinList] SET [State] = " + (state == 1 ? "0" : "1") + "  where ID =" + e.CommandArgument.ToString();
    //        // Response.Write(sql);
    //        DbHelperSQL.ExecuteSql(sql);
    //    }
    //    else if (e.CommandName == "删除")
    //    {
    //        sql = "DELETE FROM Event_2017_whwanda0112_joinlist WHERE [ID] = " + e.CommandArgument.ToString();
    //    }
    //    DbHelperSQL.ExecuteSql(sql);
    //    PageInit();
    //}

    protected void btnDel_Click(object sender, EventArgs e)
    {
        string strNewsID = "0";
        for (int i = 0; i < rptData.Items.Count; i++)
        {
            if (((CheckBox)rptData.Items[i].FindControl("cbSelect")).Checked)
            {
                strNewsID += "," + ((HiddenField)rptData.Items[i].FindControl("hidID")).Value;
            }
        }

        DbHelperSQL.ExecuteSql("delete from [Event_2017_shimao360] WHERE remark='" + remark + "' and [Id] IN (" + strNewsID + ")");
        PageInit();

    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        string sql = @"select * from Event_2017_shimao360 where remark='" + remark + "'";

        StringBuilder sb = new StringBuilder();
        sb.Append("参与编号,OpenID,微信昵称,微信头像,姓名,手机号码,是否业主,城市,课程,活动名称,参与时间");

        DataTable dt = DbHelperSQL.Query(sql).Tables[0];
        for (int i = 0, j = dt.Rows.Count; i < j; i++)
        {
            sb.Append(System.Environment.NewLine);
            for (int m = 0, n = dt.Columns.Count; m < n; m++)
            {
                if (dt.Columns[m].ColumnName == "AddTime")
                {
                    if (dt.Rows[i][m].ToString() != "")
                    {
                        sb.Append("\"" + DateTime.Parse(dt.Rows[i][m].ToString()).ToString("yyyy-MM-dd HH:mm:ss") + "\",");
                    }
                    else
                    {
                        sb.Append(",");
                    }
                }
                else
                {
                    sb.Append(dt.Rows[i][m] + ",");
                }
            }
        }

        Page.Response.Clear();
        Page.Response.Buffer = true;
        Page.Response.ContentEncoding = Encoding.GetEncoding("gb2312");
        Response.Charset = "gb2312";
        Page.Response.AddHeader("Content-Disposition", "attachment;filename=" + DateTime.Now.ToString("yyMMdd_hhmmss") + ".csv");
        Page.Response.ContentEncoding = Encoding.GetEncoding("gb2312");//设置输出流为简体中文
        Page.Response.ContentType = "text/txt";//设置输出文件类型为excel文件。
        Page.Response.Write(sb.ToString());
        Page.Response.End();
    }


}
