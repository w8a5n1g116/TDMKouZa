<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="text.aspx.cs" Inherits="TDM.pages2._0.lhk.text" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="maximum-scale=1.0,minimum-scale=1.0,user-scalable=0,width=device-width,initial-scale=1.0" />
    <meta name="format-detection" content="telephone=no,email=no,date=no,address=no" />
    <meta name="viewport" content="width=device-width,initial-scale=1,user-scalable=no" />
    <title>TDM</title>
    <link href="../css/font/iconfont.css" rel="stylesheet" />
    <link href="../../css/complanation.css" rel="stylesheet" />
    <link href="../css/mobileSelect.css" rel="stylesheet" />
    <link href="../js/pop/pop.css" rel="stylesheet" />
    <link href="../css/page_plan.css" rel="stylesheet" />
    <style type="text/css">
        .complanation_date_box {
            margin:0;
            
            padding:0.5em 1em;
            height:3vh;
        }
        .iframe_box {
           width:100vw;
            height:87vh;
            -webkit-overflow-scrolling:touch; overflow-y: scroll;
            overflow-x:hidden;
        }
        iframe {
            width:100vw;
            border:0;
            margin-top:0.5em;
            overflow-y:auto;
            height: inherit;
        }
        .menu_box {
         width:auto;   
           
        }
            
    </style>
</head>

<body style="margin:0;overflow-y:hidden">
    <header class="menu_box" style="position: inherit;padding-bottom:2vh">
        <div class="type_box">
            <div class="active">全部</div>
            <div>已下达</div>
            <div>已汇报</div>
            <div>已审批</div>
        </div>
    </header>
    <div class="complanation_date_box" >
        <i class="icon iconfont aui-iconfont icon-activity_fill" style="font-size: 1.2em;"></i>
        <div style="margin-left: 0.5em;" id="date">
        </div>
    </div>
    <div class="iframe_box">     
    </div>
</body>
 <script src="../../js/jquery-1.11.0.min.js"></script>
<script src="../js/mobileSelect.js"></script>
    <script type="text/javascript">
        var _config = {
            userid: '<%=userid%>'
        }
        var userid = _config.userid;
        $(function () {
            $(".type_box div").bind("click", type_change);
        })
        function show() {
            var mydate = new Date();
            var str = (mydate.getMonth() + 1);
            str = mydate.getFullYear() + "年" + str;
            var mydate = new Date();
            now_year = mydate.getFullYear();
            now_month = mydate.getMonth();
            nowdate = [now_year, now_month + 1];
             $(".iframe_box").html(' <iframe src="index.aspx?userid=' + userid + '&nowdate=' + nowdate + '" id="aa"></iframe>');
            return str + "月";    
        }

        $("#date").html(show());
        var year = [];
        var month = [];
        var now_year, now_month;
        var mydate = new Date();
        now_year = mydate.getFullYear();
        now_month = mydate.getMonth();
        date_list();
        function date_list() {
            var mydate = new Date();

            for (var i = 5; i > -5; i--) {
                year.push(mydate.getFullYear() - i);
            }

            console.log(year);

            for (var i = 1; i <= 12; i++) {
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

            },
            callback: function (indexArr, data) {
                $("#date").html(data[0] + "年" + data[1] + "月");
                $("#aa")[0].contentWindow.lhk_list(data);
            }
        });
        function type_change() {
            $(".type_box div").each(function () {
                $(this).removeAttr("class");
            })
            $(this).attr("class", "active");
            $("#aa")[0].contentWindow.type_change($(this).html());
        }
        function pops_shows(data) {
            parent.pops_show(data)
        }
        function jl_pj(fd, id) {
            $("#aa")[0].contentWindow.jl_pj(fd,id);
        }
        function hb(fd, id) {
            $("#aa")[0].contentWindow.hb(fd,id);
        }
        </script>
</html>
