<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ddInfo.aspx.cs" Inherits="DDpage.KmrStorage.ddInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1,maximum-scale=1,user-scalable=no" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black" />


    <!--标准mui.css-->
    <link rel="stylesheet" href="../css/mui.min.css" />
    <style type="text/css">
        .fix_icon {
            background-color: #00bcd4;
            color: #fff;
            border-radius: 10px
        }

        /*.fix_font{
            font-size:10px !important;
        }*/
       .fix_slider{
           height:12em;
       }
   
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>
    </form>
</body>
</html>
<script src="../js/jquery-1.11.0.min.js"></script>
<script src="../js/dingtalk.js"></script>
<%--<script src="../js/ddjs.js"></script>--%>
<script src="../js/mobileSelect.js"></script>
<script src="../js/api.js"></script>
<script src="../js/aui-toast.js"></script>
<script src="../js/mui.min.js"></script>
<script type="text/javascript">

    var strtext = "您有新的领料信息需要处理! 审核时间：" + DateTime.Now.ToString() + "";
    alert("1111");
    $.post("http://122.112.213.22/Message/api.aspx", { appid: "150465644", users: "15109509909", title: "领料确认通知", text: strtext, url: "www.baidu.com", name: "掌上仓储" }, function (data, status) {
        if (data = "success") {
            mui.toast("审批成功！", { duration: 'short', type: 'div' });

            //mui.toast("报修单提交成功！", { duration: 'short', type: 'div' });
            //location.reload();
        }
        else {


        }
    });
                                //发送钉消息提醒

   

</script>