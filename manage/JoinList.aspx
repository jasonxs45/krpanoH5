<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JoinList.aspx.cs" Inherits="HousingTest2016_JoinList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="/manage/style/ManageStyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/manage/Scripts/Global.js"></script>
    <script language="javascript" type="text/javascript">
        function searchRes() {
            location.href = "JoinList.aspx?page=1&type=" + document.getElementById("ddlSort").value + "&value=" + escape(document.getElementById("tbSearch").value.Trim());
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField ID="hidQuery" runat="server" />
    <div class="GlobaSpace">
        <div class="PartSpace">
            <div class="DataList">
                <table class="TableNormal">
                    <tr>
                        <td style="text-align: center; color: Green; font-weight: bold; font-size: 20px;"
                            colspan="3">
                            <asp:Label ID="title" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding: 5px 0 3px 5px;">
                            <asp:Button ID="btnExcel" runat="server" Text="导出Excel" CssClass="ButtonLong" onmouseover="this.style.background='url(But02_over.gif) no-repeat'"
                                onmouseout="this.style.background='url(But02_out.gif) no-repeat'" OnClick="btnExcel_Click" />
                        </td>
                        <%--<td style="padding: 5px 0 3px 5px;">
                            <asp:Label ID="time1" runat="server" Text="Label">2月25日预约人数：</asp:Label>
                            <asp:Label ID="number" runat="server" Text="Label"></asp:Label>
                        </td>
                        <td style="padding: 5px 0 3px 5px;">
                            <asp:Label ID="time2" runat="server" Text="Label">2月26日预约人数：</asp:Label>
                            <asp:Label ID="number1" runat="server" Text="Label"></asp:Label>
                        </td>--%>
                    </tr>
                </table>
            </div>
        </div>
        <div class="PartSpace">
            <div class="PagingBar">
                <div style="margin-left: 10px;">
                    每页<asp:Label ID="lblPageSize" runat="server" Text=""></asp:Label>条 <span style="color: #cccccc;
                        padding: 0 3px 0 3px">|</span><asp:HyperLink ID="hlFirst" runat="server">首页</asp:HyperLink>
                    <asp:HyperLink ID="hlPre" runat="server">上一页</asp:HyperLink>
                    <asp:HyperLink ID="hlNext" runat="server">下一页</asp:HyperLink>
                    <asp:HyperLink ID="hlLast" runat="server">尾页</asp:HyperLink>
                    第<asp:Label ID="lblPageIndex" runat="server"></asp:Label>/<asp:Label ID="lblPageCount"
                        runat="server"></asp:Label>页 <span style="color: #cccccc; padding: 0 3px 0 3px">|</span>
                    共
                    <asp:Label ID="lblDataCount" runat="server" Text=""></asp:Label>
                    条记录 <span style="padding-left: 25px;">搜索：</span>
                    <asp:DropDownList ID="ddlSort" runat="server" CssClass="SelectNormal">
                        <asp:ListItem Value="NickName">微信昵称</asp:ListItem>
                        <asp:ListItem Value="Age">预约时间</asp:ListItem>
                    </asp:DropDownList>
                    <asp:TextBox ID="tbSearch" runat="server" CssClass="InputNormal" Width="80px"></asp:TextBox>
                    <a onclick="searchRes()" style="cursor: pointer;">搜索</a>
                </div>
            </div>
        </div>
        <div class="PartSpace">
            <div class="DataList">
                <table class="TableNormal">
                    <tr>
                        <th>
                            参与编号
                        </th>
                        <th>
                            微信昵称
                        </th>
                        <th>
                            微信头像
                        </th>
                        <th>
                            姓名
                        </th>
                        <th>
                            手机号
                        </th>
                        <th>
                            是否业主
                        </th>
                        <th>
                            城市
                        </th>
                        <th>
                            课程
                        </th>
                        <th>
                            参与时间
                        </th>
                    </tr>
                    <asp:Repeater ID="rptData" runat="server">
                        <ItemTemplate>
                            <tr onmouseover="overItem(this);" onmouseout="outItem(this)" align="center">
                                <td>
                                    <asp:CheckBox ID="cbSelect" runat="server" />
                                    <asp:HiddenField ID="hidID" Value='<%# Eval("Id") %>' runat="server" />
                                </td>
                                <%--  <td>
                                    <%# Eval("OpenID")%>
                                </td>--%>
                                <td>
                                    <%# Eval("NickName")%>
                                </td>
                                <td>
                                    <asp:Image ID="Image1" runat="server" ImageUrl='<%# Eval("HeadImg")%>' Width="50px" />
                                </td>
                                <td>
                                    <%# Eval("Name")%>
                                </td>
                                <td>
                                    <%# Eval("TelePhone")%>
                                </td>
                                <td>
                                    <%# Eval("Costumer")%>
                                </td>
                                <td>
                                    <%# Eval("City")%>
                                </td>
                                <td>
                                    <%# Eval("Project")%>
                                </td>
                                <td>
                                    <%# Eval("AddTime")%>
                                </td>
                            </tr>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </div>
        <div class="PartSpace">
            <div class="OperateBar">
                <input id="cbSelect" type="checkbox" onclick="selectItem(this)" /><a>反选</a>
                <asp:Button ID="btnDel" runat="server" Text="删 除" OnClick="btnDel_Click" CssClass="ButtonShort"
                    OnClientClick="return confirm('确认删除吗？')" />
            </div>
        </div>
    </div>
    </form>
</body>
</html>
