<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="team.aspx.cs" Inherits="TDM.pages2._0.lhk.team" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="maximum-scale=1.0,minimum-scale=1.0,user-scalable=0,width=device-width,initial-scale=1.0" />
    <meta name="format-detection" content="telephone=no,email=no,date=no,address=no" />
    <meta name="viewport" content="width=device-width,initial-scale=1,user-scalable=no" />
    <title>TDM</title>
    <link href="../css/font/iconfont.css" rel="stylesheet" />
    <link href="../css/mobileSelect.css" rel="stylesheet" />
    <link href="../css/public.css" rel="stylesheet" />
    <link href="../../css/table.css" rel="stylesheet" />
    <link href="../css/page_plan.css" rel="stylesheet" />
    <style type="text/css">
        .tables {
            display:flex;
            flex-direction:row;
            justify-content:center;
            align-items:center;
            
        }
        .zebra th {
            text-align:center;
        }
        .complanation_date_box {
             display:flex;
            flex-direction:row;
            align-items:center;
            justify-content:flex-start;
            padding:0.5em 1.5em;
            font-size:1.2em;
            color:#fff;
            background:#E1553E;
            margin:1em 0;
        }
        #none {
            display:flex;
            flex-direction:row;
            align-items:center;
            justify-content:center;
        }
        .zebra td, .zebra th {
            text-align:center;
        }
    </style>
</head>
<body>
   <div class="complanation_date_box">
                <div class="add_box" style="color:#fff;margin-right:50vw;display:none" id="back">
                    <i class="icon iconfont icon-undo"></i>
                    <div style="float:right;">返回</div>
                </div>
                <div id="type_data" style="display:flex;flex-direction: row;align-items: center;">
                    <i class="icon iconfont aui-iconfont icon-activity_fill" style="font-size: 1.2em;"></i>
                <div style="margin-left: 0.5em;" id="date"></div> 
                </div>
            </div>
    <div class="input_box" style="padding: 1em 0.8em;">
        <table class="zebra" style="width: 100%; font-size: 0.8em;" id="table_boxs">
            <thead>
                <tr><th colspan="5" id="team"></th></tr>
                <tr>
                    <th>姓名</th>
                    <th>岗位指标</th>
                    <th>超额净收益</th>
                    <th>其他</th>
                    <th>总合</th>
                </tr>
            </thead>
            <tbody id="datas">

            </tbody>
        </table>
      
    </div>
    <div class="leader_box" style="display:none;"></div>
    <div id="none" style="display:none"></div>
</body>
<script src="../../js/jquery-1.11.0.min.js"></script>
<script src="../js/mobileSelect.js"></script>
<script type="text/javascript">
    var _config = {
        userid: '<%=userid%>'
    }
    var ys = "";
    var ms = "";
    var status = "";
    var bm = "";
    $(function () {
        $("#date").html(show());
        $("#back").bind("click",backpage)
    })
    function show() {
        var mydate = new Date();
        var str = mydate.getMonth();
        var y = mydate.getFullYear();
        if (str == 0) {
            y = mydate.getFullYear() - 1;
            str == 12;
        }
        ys = y;
        ms = str;
        str = y + "年" + str;
        data_load()
        return str + "月";
    }
    var year = [];
    var month = [];
    var now_month;
    date_list();
    function date_list() {
        var mydate = new Date();
        now_month = mydate.getMonth();
     //   console.log(now_month)
        for (var i = 5; i > -5; i--) {
            year.push(mydate.getFullYear() - i);
        }
        for (var i = 1; i <= 12; i++) {
            month.push(i);
        }

    }
    var mobileSelect = new MobileSelect({
        trigger: '#date',
        title: '日期选择',
        wheels: [
                    { data: year },
                    { data: month }
        ],
        position: [5, now_month],
        transitionEnd: function (indexArr, data) {
          //  console.log(data);
        },
        callback: function (indexArr, data) {
           // console.log(data);
            ys = data[0];
            ms = data[1];
            data_load()
            $("#date").html(data[0] + "年" + data[1] + "月");
        }
    });
    function data_load() {
        $.post("../../api/userinfo/team.aspx", { userid: _config.userid, year: ys, month: ms }, function (fd) {
           // console.log(JSON.parse(fd));
            if (fd == "none") {
                $("table").css("display", "none");
                $(".leader_box").css("display", "none");
                $("#none").css("display", "flex");
                $("#none").html("未查找到数据！");
            }
            else if (fd == "stop") {
                $("table").css("display", "none");
                $("#none").css("display", "flex");
                $("#none").html("您没有查看权限！");
            }
            else {
                if (JSON.parse(fd).status == "集团领导") {
                    status = "集团领导";
                    var data_list = JSON.parse(fd);
                    $(".input_box").css("display", "none");
                    var html = "";
                    for (var i = 0; i < data_list.data.length; i++) {
                        html += '<div class="plan_box" onclick="bm_click(this)" name="' + data_list.data[i].bm + '"><div class="info" style="padding:0.5em;"><div class="leader_text">' + data_list.data[i].bm + '</div><div class="ts">超额净收益' + data_list.data[i].zh + '</div></div><div class="stage"><div>查看详情</div></div></div>';
                    }
                    $(".leader_box").css("display", "flex");
                    $(".leader_box").html(html);
                }
                else {
                    $("table").css("display", "table");
                    $("#none").css("display", "none");
                    var html = "";
                    var js = JSON.parse(fd).data;
                    for (var i = 0; i < js.length; i++) {
                        var qt = js[i].gshj - js[i].cejsy - js[i].gwzb;
                        qt = qt.toFixed(2);
                        var hj = js[i].gshj;
                        hj = parseFloat(hj).toFixed(2);
                        html += '<tr><td>' + js[i].name + '</td><td>' + js[i].gwzb + '</td><td>' + js[i].cejsy + '</td><td>' + qt + '</td><td>' + hj + '</td></tr>';
                    }
                    $("#datas").html(html);
                    $("#team").html(JSON.parse(fd).team + " " + ms + "月绩效");
                }
            }
        });
    }

    function bm_click(obj) {
        //console.log($(obj).attr("name"))
        bm = $(obj).attr("name");
        bm_data();
    }

    function bm_data() {
        $.post("../../api/userinfo/team_info.aspx", { userid: _config.userid, year: ys, month: ms, bm: bm }, function (fd) {
            //console.log(fd);
            if (fd == "none") {
                $("table").css("display", "none");
                $(".leader_box").css("display", "none");
                $("#none").css("display", "flex");
                $("#none").html("未查找到数据！");
            }
            else {
                $("#type_data").css("display", "none");
                $("#back").css("display", "block");
                $(".input_box").css("display", "flex");
                $("#back").css("display", "flex");
                $(".leader_box").css("display", "none");
                $("table").css("display", "table");
                $("#none").css("display", "none");
                var html = "";
                var js = JSON.parse(fd).data;
                for (var i = 0; i < js.length; i++) {
                    var qt = js[i].gshj - js[i].cejsy - js[i].gwzb;
                    qt = qt.toFixed(2);
                    var hj = js[i].gshj;
                    hj = parseFloat(hj).toFixed(2);
                    html += '<tr><td>' + js[i].name + '</td><td>' + js[i].gwzb + '</td><td>' + js[i].cejsy + '</td><td>' + qt + '</td><td>' + hj + '</td></tr>';
                }
                $("#datas").html(html);
                $("#team").html(JSON.parse(fd).team + " " + ms + "月绩效");
            }
        });
    }
    function backpage() {
        $("#type_data").css("display", "flex");
        $("#back").css("display", "none");
        $(".input_box").css("display", "none");
        $(".leader_box").css("display", "flex");
        $("table").css("display", "none");
        $("#none").css("display", "none");
    }
</script>
</html>
