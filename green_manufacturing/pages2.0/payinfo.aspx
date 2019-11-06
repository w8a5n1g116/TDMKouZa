<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="payinfo.aspx.cs" Inherits="TDM.pages2._0.payinfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="maximum-scale=1.0,minimum-scale=1.0,user-scalable=0,width=device-width,initial-scale=1.0" />
    <meta name="format-detection" content="telephone=no,email=no,date=no,address=no" />
    <meta name="viewport" content="width=device-width,initial-scale=1,user-scalable=no"/>
    <title>TDM</title>
    <link href="../css/aui.css" rel="stylesheet" />
    <link href="../css/complanation.css" rel="stylesheet" />
    <link href="css/font/iconfont.css" rel="stylesheet" />
    <link href="css/mobileSelect.css" rel="stylesheet" />
    <link href="css/public.css" rel="stylesheet" />
    <style type="text/css">
        td {
            font-size:0.9em;
            text-align:center;
            margin-top:0.3em;
        }
        .complanation_date_box {
            font-size:1.2em;
        }
    </style>
</head>
<body style="font-family:微软雅黑">
    <div>
        <div class="input_box" style="padding-bottom: 0.5em;">
            <div class="complanation_date_box" style="margin: 0; font-size: 1em;align-items:center;height:1.5em;"></div>
            <div class="line_text1" style="padding: 0 0.5em; margin: 1em 1em 0.5em;align-items:center;">
                <i class="icon iconfont icon-service" style="font-size:1.6em;color:#DA251C;margin-top:0.1em;"></i>
                <div style="font-size:1.2em;">卡上余额：<span id="money" style="font-size:1.2em;"></span></div>
            </div>
            <div style="font-size:1em;margin-left:2em;">本月消费总额：<span id="payfor"></span>元</div>
        </div>

        <div class="input_box" style="padding:0 0 0.5em 0;">
            <div class="complanation_date_box" style="margin:0;">
                    <i class="icon iconfont aui-iconfont icon-activity_fill" style="font-size: 1.2em;"></i>
                    <div style="margin-left: 0.5em;" id="date">

                    </div>
                </div>
            <table  style="width:90%;margin-top:1em;" id="list">
                 
            </table>
        </div>
    </div>
</body>
<script src="../js/jquery-1.11.0.min.js"></script>
<script src="js/mobileSelect.js"></script>
<script type="text/javascript">
    var _config = {
        userid:'<%=userid%>'
    }
    function show() {
        var mydate = new Date();
        var str = (mydate.getMonth() + 1);
        a(mydate.getFullYear(),str);
        str = mydate.getFullYear() + "年" + str;
        return str + "月";
    }
    $("#date").html(show());
    var year = [];
    var month = [];
    var now_year, now_month;
    date_list();
    function date_list() {
        var mydate = new Date();
        now_year = mydate.getFullYear();
        now_month = mydate.getMonth();
        for (var i = 5; i >-5; i--) {
            year.push(mydate.getFullYear() - i);
        }
        
        console.log(year);

        for (var i = 1; i <=12; i++) {
            month.push(i);
        }
        console.log(month)
    }
    var mobileSelect2 = new MobileSelect({
        trigger: '#date',
        title: '日期选择',
        wheels: [
                    { data: year },
                    { data: month }
        ],
        position: [5, now_month],
        transitionEnd: function (indexArr, data) {
            console.log(data);
        },
        callback: function (indexArr, data) {
            console.log("123" + data);
            $("#date").html(data[0] + "年" + data[1] + "月");
            a(data[0], data[1]);
        }
    });
    
    function a(dyear,dmonth) {
        $.post("../api/pay/pay_list.aspx", { userid: _config.userid, month: dmonth,year:dyear }, function (fd) {
            if(fd!="none"){
                var data = JSON.parse(fd);
                var text = "<tr><th>日期</th><th>余额</th><th>消费</th><th>机号</th></tr>";
                $("#money").html(data.list[0].less);
                var payfor = 0.00;
                for (var i = 0; i < data.list.length; i++) {
                    text += '<tr><td>' + data.list[i].date + '</td><td>' + data.list[i].less + '</td><td>' + data.list[i].pay + '</td><td>' + data.list[i].code + '</td></tr> ';
                    payfor += parseFloat(data.list[i].pay);
                }
                $("#list").html(text);
                $("#payfor").html(payfor);
            }
            else{
                
            }
        })
    }
</script>
</html>
