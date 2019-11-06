<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="deed.aspx.cs" Inherits="TDM.pages2._0.lhk.deed" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="maximum-scale=1.0,minimum-scale=1.0,user-scalable=0,width=device-width,initial-scale=1.0" />
    <meta name="format-detection" content="telephone=no,email=no,date=no,address=no" />
    <title>TDM</title>
    <link href="../../css/aui.css" rel="stylesheet" />
    <link href="../../css/iconfont.css" rel="stylesheet" />
    <link href="../css/font/iconfont.css" rel="stylesheet" />
    <link href="../css/page_plan.css" rel="stylesheet" />
    <link href="../js/pop/pop.css" rel="stylesheet" />
    <link href="../css/mobileSelect.css" rel="stylesheet" />
    <link href="../../css/complanation.css" rel="stylesheet" /> 
    <style type="text/css">
       .none{
           margin-top: 3em;
       }
    </style>
</head> 
<body style="background:#fff;">
    <div class="page_body scroll" id="page_body">
           
            <div class="list_box" id="deeds" style="margin-top:0;margin-bottom:3em;">             
        </div>        
    </div>    
</body>
<script src="../../js/jquery-1.11.0.min.js"></script>
<script type="text/javascript">
    var _config = {
        userid: '<%=userid%>',
        nowdate: '<%=nowdate%>'
    }
    var list_data = "";
    var date = _config.nowdate.split(",");
    gr_list(date);
        function gr_list(date) {
            $.post("../../api/lhk/gr_list.aspx", { userid: _config.userid, yue: date[1], year: date[0] }, function (fd) {
               
                if (fd.length > 0) {
                     var data = JSON.parse(fd);
                     list_data=data;
                    console.log(data);
                    var text = '';
                    
                    for (var i = 0; i < data.list.length; i++) {
                        text += '<div class="list" style="padding:0;background: #f9f9f9"><div class="list_line1" style="margin:0;background: #DDDDDD;justify-content: space-around;width:100%"><div>个人事例</div><div>常规典型</div><div>提出人：' + data.list[i].inputname + '</div></div><div style="padding:0.5em;text-indent:2em;justify-content:flex-start;">' + data.list[i].text + '</div><div class="complanation_date_box" style="margin:0;justify-content: space-between;width:100%;background:#f9f9f9;color:#000;"><div>自评金额：' + data.list[i].zp + '</div><div>审核金额：' + data.list[i].sh + '</div></div><div class="complanation_date_box" style="margin:0;justify-content: flex-start;width:100%;background:#f9f9f9;color:#000;"><div>责任人：' + data.list[i].emname + '</div></div></div>';
                    }
                    $("#deeds").html(text);
              
                }
                else
                    $("#deeds").html("未找到个人事例");
            })
        }
        function get_data() {
            return list_data;
        }
        function input_data(html) {
            if (html.length > 0)
                $("#deeds").html(html);
            else
                $("#deeds").html("未找到个人事例");
        }
</script>
</html>
