<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="user_info.aspx.cs" Inherits="TDM.pages2._0.user_info" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="maximum-scale=1.0,minimum-scale=1.0,user-scalable=0,width=device-width,initial-scale=1.0" />
    <meta name="format-detection" content="telephone=no,email=no,date=no,address=no" />
    <meta name="viewport" content="width=device-width,initial-scale=1,user-scalable=no"/>
    <title>TDM</title>
    <link href="../css/complanation.css" rel="stylesheet" />
    <style type="text/css">
        body {
            background:#F9F9F9;
        }
        .line_text1 {
            color:#383838;
        }
    </style>
</head>
<body>
    <div class="input_box" style="background:#E1553E;padding:1em 0.5em;display:flex;color:#fff;flex-direction:column;justify-content:center;align-items:center;">
        <img src="../image/user_jc.png" style="width:3em;height:3em;"/>
        <div style="margin-top:0.5em;" id="name">姓名</div>
    </div>
    <div class="input_box" style="padding:1em 0.8em;">
        <div class="line_text1">公司：<span id="cpy"></span></div>
        <div class="line_text1">部门：<span id="bm"></span></div>
        <div class="line_text1">经营体：<span id="jyt"></span></div>
        <div class="line_text1">岗位：<span id="gw"></span></div>
        <div class="line_text1">岗位类别：<span id="gwlb"></span></div>
        <div class="line_text1">性别：<span id="sex"></span></div>
        <div class="line_text1">电话：<span id="phone"></span></div>
        <div class="line_text1">婚姻状况：<span id="marry"></span></div>
        <div class="line_text1">籍贯：<span id="home"></span></div>
        <div class="line_text1">出生日期：<span id="br_day"></span></div>
        <div class="line_text1">身份证所在地：<span id="sc_card"></span></div>
        <div class="line_text1">户口所在地：<span id="hk_ad"></span></div>
        <div class="line_text1">人事档案所在地：<span id="rsda"></span></div>
        <div class="line_text1">职称：<span id="zc"></span></div>
        <div class="line_text1">职称授予时间：<span id="zc_date"></span></div>
        <div class="line_text1">技能级别：<span id="jn"></span></div>
        <div class="line_text1">健康级别：<span id="jk"></span></div>
        <div class="line_text1">品质级别：<span id="pz"></span></div>
    </div>
</body>
<script src="../js/jquery-1.11.0.min.js"></script>
<script type="text/javascript">
    var _config = {
        userid:'<%=userid%>'
    }
    
    $.post("../api/userinfo/info.aspx", { userid: _config.userid }, function (fd) {
        var data = JSON.parse(fd);
        Object.keys(data).forEach(function (trait) {
            $("#" + trait).html(data[trait]);
        });
    })
</script>
</html>
