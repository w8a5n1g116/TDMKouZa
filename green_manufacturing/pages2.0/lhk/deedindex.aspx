<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="deedindex.aspx.cs" Inherits="TDM.pages2._0.lhk.deedindex" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="maximum-scale=1.0,minimum-scale=1.0,user-scalable=0,width=device-width,initial-scale=1.0" />
    <meta name="format-detection" content="telephone=no,email=no,date=no,address=no" />
    <title>TDM</title>
    <link href="../css/font/iconfont.css" rel="stylesheet" />
    <link href="../../css/complanation.css" rel="stylesheet" />
        <link href="../css/mobileSelect.css" rel="stylesheet" />
    <link href="../js/pop/pop.css" rel="stylesheet" />
    <link href="../css/page_plan.css" rel="stylesheet" />

    <style type="text/css">
        .complanation_date_box {
            margin:0;
            margin-top:3em;
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
            width:inherit;
            height:inherit;
            border:0;
            margin-top:0.5em;
            overflow-y:auto;
        }
    </style>
</head>

<body style="margin:0;overflow-y:hidden">
    <header class="menu_box" style="width:auto">
        <div class="type_box">
            <div class="active">全部</div>
            <div>我的事例</div>
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
        function show() {
            var mydate = new Date();
            var str = (mydate.getMonth() + 1);
            str = mydate.getFullYear() + "年" + str;
            var mydate = new Date();
            now_year = mydate.getFullYear();
            now_month = mydate.getMonth();
            nowdate = [now_year, now_month + 1];
            $(".iframe_box").html(' <iframe src="deed.aspx?userid=' + userid + '&nowdate=' + nowdate + '" id="bb"></iframe>');
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

                console.log(data);
            },
            callback: function (indexArr, data) {
                console.log("123" + data);
                $("#date").html(data[0] + "年" + data[1] + "月");
                 $("#bb")[0].contentWindow.gr_list(data);
                //type_change();
            }
        });

        $(function () {
            //plan_load();
            $("#add").bind("click", plan_add);
            $(".type_box div").bind("click", type_change);
        })
        function type_change() {
            $(".type_box div").each(function () {
                $(this).removeAttr("class");
            })
            $(this).attr("class", "active");
            var html = "";
            var fd = $("#bb")[0].contentWindow.get_data();
            
            if ($(this).html() == "我的事例") {
                
                for (var i = 0; i < fd.list.length; i++) {

                    if (fd.username == fd.list[i].emname || fd.username == fd.list[i].inputname) {
                        html += '<div class="list" style="padding:0;background: #f9f9f9"><div class="list_line1" style="margin:0;background: #DDDDDD;justify-content: space-around;width:100%"><div>个人事例</div><div>常规典型</div><div>提出人：' + fd.list[i].inputname + '</div></div><div style="padding:0.5em;text-indent:2em;justify-content:flex-start;">' + fd.list[i].text + '</div><div class="complanation_date_box" style="margin:0;justify-content: space-between;width:100%;background:#f9f9f9;color:#000;"><div>自评金额：' + fd.list[i].zp + '</div><div>审核金额：' + fd.list[i].sh + '</div></div><div class="complanation_date_box" style="margin:0;justify-content: flex-start;width:100%;background:#f9f9f9;color:#000;"><div>责任人：' + fd.list[i].emname + '</div></div></div>';
                       
                    }
                }
            }
            else {
                for (var i = 0; i < fd.list.length; i++) {
                    html += '<div class="list" style="padding:0;background: #f9f9f9"><div class="list_line1" style="margin:0;background: #DDDDDD;justify-content: space-around;width:100%"><div>个人事例</div><div>常规典型</div><div>提出人：' + fd.list[i].inputname + '</div></div><div style="padding:0.5em;text-indent:2em;justify-content:flex-start;">' + fd.list[i].text + '</div><div class="complanation_date_box" style="margin:0;justify-content: space-between;width:100%;background:#f9f9f9;color:#000;"><div>自评金额：' + fd.list[i].zp + '</div><div>审核金额：' + fd.list[i].sh + '</div></div><div class="complanation_date_box" style="margin:0;justify-content: flex-start;width:100%;background:#f9f9f9;color:#000;"><div>责任人：' + fd.list[i].emname + '</div></div></div>';
                }
            }
            //$("#deeds").html(html);
            $("#bb")[0].contentWindow.input_data(html);
        }
        function plan_add() {

            parent.pops_show({ title: ["新增"], text: "plan_new", type: "plan_add" });
        }

        </script>
</html>
