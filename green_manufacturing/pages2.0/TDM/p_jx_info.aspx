<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="p_jx_info.aspx.cs" Inherits="TDM.pages2._0.TDM.p_jx_info" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="maximum-scale=1.0,minimum-scale=1.0,user-scalable=0,width=device-width,initial-scale=1.0" />
    <meta name="format-detection" content="telephone=no,email=no,date=no,address=no" />
    <title>TDM</title>
    <link href="../../css/table.css" rel="stylesheet" />
    <link href="../css/font/iconfont.css" rel="stylesheet" />
    <link href="../css/mobileSelect.css" rel="stylesheet" />
    <link href="../css/public.css" rel="stylesheet" />
    <style type="text/css">
        .tables {
            display:flex;
            flex-direction:row;
            justify-content:center;
            align-items:center;
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
    </style>
</head>
<body>
    <div class="complanation_date_box">
                <i class="icon iconfont aui-iconfont icon-activity_fill" style="font-size: 1.2em;"></i>
                <div style="margin-left: 0.5em;" id="date"></div>
            </div>
    <div class="tables">
         <div id="none_box"></div>
        <table class="zebra" style="width: 90vw; font-size: 0.8em;" id="table_boxs">
                    <thead>
                        <tr>
                            <th></th>
                            <th id="table_title"></th>
                            <th>金额</th>

                        </tr>
                    </thead>

                    <tr>

                        <td>1</td>
                        <td>绩效工资</td>
                        <td id="jxgz"></td>
                    </tr>
                    <tr>
                        <td>2</td>
                        <td>超额净收益</td>
                        <td id="cejsy"></td>

                    </tr>
                    <tr>
                        <td>3</td>
                        <td>岗位指标</td>
                        <td id="gwzb"></td>
                    </tr>
                    <tr>
                        <td>4</td>
                        <td>岗位职责</td>
                        <td id="gwzz"></td>
                    </tr>
                    <tr>
                        <td>5</td>
                        <td>计划任务</td>
                        <td id="jhrw"></td>
                    </tr>

                    <tr>
                        <td>6</td>
                        <td>常规典型</td>
                        <td id="cgdx"></td>
                    </tr>
                    <tr>
                        <td>7</td>
                        <td>6s管理</td>
                        <td id="sgl"></td>
                    </tr>
                    <tr>
                        <td>8</td>
                        <td>班组建设</td>
                        <td id="bzjs"></td>
                    </tr>
                    <tr>
                        <td>9</td>
                        <td>培训</td>
                        <td id="px"></td>
                    </tr>
                    <tr>
                        <td>合计</td>
                        <td id="hj" colspan="2" style="text-align:center;"></td>
                    </tr>
                </table>
    </div>
   

</body>
<script src="../../js/jquery-1.11.0.min.js"></script>
<script src="../js/mobileSelect.js"></script>
<script type="text/javascript">
    var _config = {
        userid:'<%=userid%>'
    }
    $(function () {
        $("#date").html(show());
    })
    function show() {
        var mydate = new Date();
        var str = mydate.getMonth();
        title(str);
        var y = mydate.getFullYear();
        if (str == 0) {
            y = mydate.getFullYear() - 1;
            str == 12;
        } 
        jx_info(y, str);
        str =y + "年" + str;
        return str + "月";
    }
    var year = [];
    var month = [];
    var now_month;
    date_list();
    function date_list() {
        var mydate = new Date();
        now_month = mydate.getMonth();
        console.log(now_month)
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
            console.log(data);
        },
        callback: function (indexArr, data) {
            console.log( data);
            $("#date").html(data[0] + "年" + data[1] + "月");
            jx_info(data[0], data[1]);
            title(data[1]);
        }
    });

    function jx_info(year, month) {
        $.post("../../api/plan/list.aspx", { userid: _config.userid, year: year,month:month }, function (fd) {

            if (fd=="none") {
                $("#none_box").html("未查找到数据");
                $("#none_box").css("display", "block");
                $("#table_boxs").css("display", "none");
            }
            else {
                $("#none_box").css("display", "none");
                $("#table_boxs").css("display", "table");
                var data = JSON.parse(fd);
                var hj = 0;
                console.log(data);
                Object.keys(data).forEach(function (trait) {
                    $("#" + trait).html(data[trait]);
                    
                    if (trait != "name") {
                        if (trait != "jxgz") {
                            hj += parseFloat(data[trait]);
                            console.log(trait);
                        } 
                    }     
                });
                $("#hj").html(hj);
            }
        })
    }
    function title(month) {
        $("#table_title").html(month+"月绩效详情")
    }
</script>
</html>
