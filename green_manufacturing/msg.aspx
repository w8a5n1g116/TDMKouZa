
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="msg.aspx.cs" Inherits="TDM.msg" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script src="js/jquery-1.11.0.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#btn").click(function () {
                $.post("http://192.168.1.189:1850/api.aspx", {
                    appid: "95106966",
                    users: "642222199402162418",
                    title: "测试标题",
                    text: "测试测试测试测试测试接口",
                    url: "https://www.baidu.com",
                    name:"DT中心"
                }, function (fd) { })
            })
        });
    </script>
</head>
<body style="font-family:微软雅黑">

    <div>
        <div style="width:10em;height:2.5em;padding:0.5em;margin:1em;background:#ff6a00;color:#fff;display:flex;flex-direction:row;justify-content:center;align-items:center;" id="btn">点击发送钉钉消息</div>
    </div>
</body>
</html>
