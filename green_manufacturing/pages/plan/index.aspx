<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="TDM.pages.plan.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="maximum-scale=1.0,minimum-scale=1.0,user-scalable=0,width=device-width,initial-scale=1.0" />
    <meta name="format-detection" content="telephone=no,email=no,date=no,address=no" />
    <title>TDM</title>
    <script src="../../js/jquery-1.11.0.min.js"></script>
    <script src="../../js/aui-popup-new.js"></script>
    <script src="../../js/dingtalk.js"></script>
    <script src="../../js/api.js"></script>
    <script src="../../js/aui-tab.js"></script>
    <script src="../../js/aui-dialog.js"></script>
    <link href="../../css/aui.css" rel="stylesheet" />
    <link href="../../css/iconfont.css" rel="stylesheet" />
    <link href="../../css/complanation.css" rel="stylesheet" />
    <link href="../../css/table.css" rel="stylesheet" />
    <script type="text/javascript">
        var _config = {
            appId: '<%=appId%>',
            corpId: '<%=corpId%>',
            timeStamp: '<%=timestamp%>',
            nonce: '<%=nonceStr%>',
            signature: '<%=signature%>'
        };
        var userid = "";
        dd.config({
            appId: _config.appId,
            corpId: _config.corpId,
            timeStamp: _config.timeStamp,
            nonceStr: _config.nonce,
            signature: _config.signature,
            jsApiList: ['biz.user.get']
        });
        dd.ready(function () {
            dd.biz.user.get({
                onSuccess: function (info) {
                    userid = info.emplId;
                    var mydate = new Date();
                    var date = (mydate.getMonth() + 1);
                    list(date);
                },
                onFail: function (err) {
                    logger.e('userGet fail: ' + JSON.stringify(err));
                }
            });
            function show() {
                var mydate = new Date();
                var str = (mydate.getMonth() + 1) + "月";
                return str;
            }
            function s_show()
            {
                var mydate = new Date();
                var str = (mydate.getMonth() - 1);
                if (str < 1)
                    str += 12;
                return str+"月";
            }
            function year()
            {
                var mydate = new Date();
                var str = mydate.getFullYear();
                return str + "年";
            }
            document.addEventListener('resume', function () {
                var mydate = new Date();
                var date = (mydate.getMonth());
                if (date == 0)
                    date = 12;
                a_list(date);
                list(mydate.getMonth()+1);
                hb_list()
            });
            $("#year").html(year());
            var lhk_date = show();
            lhk_date=lhk_date.substring(0,lhk_date.length-1)
            $("#start_m").html(lhk_date-1 + "月");
            
  

            $("#make_plan").click(function () {
                dd.ui.nav.preload({
                    pages: [ //需要预加载的页面数组
                      {
                          id: 'page_id1', //预加载的页面id
                          url: 'make_plan.aspx' //页面url
                      }
                    ],
                    onSuccess: function (data) { // 回调通知预加载结果
                        dd.ui.nav.go({
                            id: 'page_id1', //要跳转的页面id，一般是先调用```dd.ui.nav.preload```预加载成功后，再调用```dd.ui.nav.go```跳转
                            onSuccess: function (data) { },
                            onFail: function (err) { }
                        })
                    },
                    onFail: function (err) { } //回调通知预加载调用失败
                });
            })
            $("#date").html(lhk_date - 1 + "月");
            $("#date").click(function () {
                dd.biz.util.chosen({
                    source: [{ key: '1月', value: '1' }, { key: '2月', value: '2' }, { key: '3月', value: '3' }, { key: '4月', value: '4' }, { key: '5月', value: '5' }, { key: '6月', value: '6' }, { key: '7月', value: '7' }, { key: '8月', value: '8' }, { key: '9月', value: '9' }, { key: '10月', value: '10' }, { key: '11月', value: '11' }, { key: '12月', value: '12' }],
                    selectedKey: lhk_date - 1 + "月", // 默认选中的key
                    onSuccess: function (result) {
                        $("#date").html(result.key);
                        $("#table_title").html(result.key + "绩效详情");
                        a_list(result.value);
                    },
                    onFail: function (err) { }
                })
            })


        })
    </script>
</head>
<body style="font-family: 微软雅黑">
    <header class="aui-bar aui-bar-nav" style="background: #fff">
        <div class="aui-pull-left aui-btn aui-btn-outlined" tapmode onclick="showPopup('top-left')">
            <span class="aui-iconfont aui-icon-menu" style="color: #009688;"></span>
            <span style="margin-left: 0.5em; color: #009688;">菜单</span>
        </div>
    </header>

    <div id="page" style="margin-bottom: 5em;">

        <div id="make_plan_page" style="display: none">
            <div class="complanation_date_box" style="margin: 0;">
                <i class="icon iconfont aui-iconfont icon-activity_fill" style="font-size: 1.2em;"></i>
                <div style="margin-left: 0.5em;" id="date"></div>
            </div>

            <div class="input_box" style="padding: 1em 0.8em;" id="bbox">
                <!--<div class="line_text1">绩效工资：<span ></span></div>
                <div class="line_text1">超额净收益：<span id="cejsy"></span></div>
                <div class="line_text1">岗位指标：<span ></span></div>
                <div class="line_text1">岗位职责：<span ></span></div>
                <div class="line_text1">计划任务：<span ></span></div>
                <div class="line_text1">常规典型：<span ></span></div>
                <div class="line_text1">自主管理：<span id="zugl"></span></div>
                <div class="line_text1">6s管理：<span id="sgl"></span></div>
                <div class="line_text1">班组建设：<span id="bzjs"></span></div>
                <div class="line_text1">培训：<span id="px"></span></div>-->
                <div id="none_box"></div>
                <table class="zebra" style="width: 90vw; font-size: 0.9em;" id="table_boxs">
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

                </table>
            </div>
        </div>

        <div id="report_page" style="display: none">

            <div class="list_box">
                <div class="complanation_date_box" style="margin: 0; width: 95vw; padding: 0.8em 1em;">
                    <div style="margin-left: 0.5em;" id="year"></div>
                    <div style="margin-left: 0.5em;margin-right:1em;" id="start_m"></div>
                    指标/目标
                </div>

                <div id="hb_list">
            <!--        <div class="list" style="padding-bottom: 0;" data-id="' + data.list[i].id + '" data-user="' + data.list[i].user + '" onclick="' + data.list[i].fun + '">
                        <div class="list_line1" style="justify-content: space-around; padding: 0 0.5em">
                            <div class="project">' + data.list[i].type + '</div>
                            <div class="name">' + data.list[i].item + '</div>
                        </div>
                        <hr />
                        <div class="list_line2" style="background: #DDDDDD; font-size: 0.9em; flex-direction: column; align-items: flex-start; padding: 0.5em; color: ' + data.list[i].color + '">
                            <div class="line_text1">
                                <div>指标/目标：</div>
                                <div>' + data.list[i].text + '</div>
                            </div>
                            <div class="line_text1">
                                <div>评价标准：</div>
                                <div>' + data.list[i].pj_text + '</div>
                            </div>
                            <div class="line_text2">
                                <div><span>权重：</span><span>' + data.list[i].qz + '</span></div>
                                <div><span>金额：</span><span>' + data.list[i].money + '</span></div>
                            </div>
                            <div class="line_text2">
                                <div>
                                    <div>责任人：</div>
                                    <div>' + data.list[i].zr_name + '</div>
                                </div>
                                <div>
                                    <div>责任班：</div>
                                    <div>' + data.list[i].zr_f + '</div>
                                </div>
                            </div>
                            <div class="line_text1">
                                <div>自评金额：</div>
                                <div>' + data.list[i].zp + '</div>
                            </div>
                            <div class="line_text3">
                                <span>完成情况说明：</span><div>' + data.list[i].wc + '</div>
                            </div>
                            <div class="line_text1">
                                <div>审核金额：</div>
                                <div>' + data.list[i].sh + '</div>
                            </div>
                            <div class="line_text3">
                                <span>审核评语：</span><div>' + data.list[i].sh_text + '</div>
                            </div>
                        </div>
                    </div>-->
                </div>
            </div>
        </div>

        <div id="plan_list_page">
            <div class="complanation_box" id="make_plan">
                    <img src="../../image/add.png" />
                    <div>添加个人计划</div>
                </div>
            <div class="list_box" id="list">
            </div>
        </div>
    </div>

    <footer class="aui-bar aui-bar-tab" id="footer">
        <div class="aui-bar-tab-item" tapmode>
            <i class="icon iconfont icon-createtask aui-iconfont"></i>
            <div class="aui-bar-tab-label">计划任务</div>
        </div>
        <div class="aui-bar-tab-item" tapmode>
            <i class="aui-iconfont aui-icon-comment"></i>
            <div class="aui-bar-tab-label">指标职责</div>
        </div>
        <div class="aui-bar-tab-item aui-active" tapmode>
            <i class="aui-iconfont aui-icon-edit"></i>
            <div class="aui-bar-tab-label">绩效信息</div>
        </div>
    </footer>



</body>
<script type="text/javascript">
    apiready = function () {
        api.parseTapmode();
    }
    var popup = new auiPopup();
    function showPopup(location) {
        popup.init({
            frameBounces: true,//当前页面是否弹动，（主要针对安卓端）
            location: location,//位置，top(默认：顶部中间),top-left top-right,bottom,bottom-left,bottom-right
            buttons: [{
                image: '../../image/createtask_fill.png',
                text: '我的计划',
                value: 'plan'//可选
            }, {
                image: '../../image/barrage_fill.png',
                text: '我的量化',
                value: 'lhk'
            }, {
                image: '../../image/transaction_fill.png',
                text: '消费明细',
                value: 'xf'
            }, {
                image: '../../image/mine_fill.png',
                text: '个人信息',
                value: 'info'
            }],
        }, function (ret) {
            if (ret) {
                switch (ret.buttonIndex) {
                    case 1:
                        {

                            //alert(ret.buttonIndex)
                            a("index.aspx")
                            break;
                        }
                    case 2:
                        {
                            //go(ret.buttonIndex, "../lhk/index.aspx");
                            a("../lhk/index.aspx");
                            break;
                        }
                    case 3:
                        {
                            //go(ret.buttonIndex, "../pay_info/index.aspx");
                            a("../pay_info/index.aspx");
                            break;
                        }
                    case 4:
                        {
                            //go(ret.buttonIndex, "../userid/index.aspx");
                            a("../userinfo/index.aspx");
                            break;
                        }
                }
            }
        })
    }
    function a(url) {
        location.href = url;
    }

    var tab = new auiTab({
        element: document.getElementById("footer")
    }, function (ret) {
        if (ret) {
            var mydate = new Date();
            var date = (mydate.getMonth() + 1);
            switch (ret.index) {
                case 1:
                    {

                        $("#make_plan_page").css('display', "none");
                        $("#report_page").css('display', "none");
                        $("#plan_list_page").css('display', "block");
                        break;
                    }
                case 2:
                    {
                        $("#make_plan_page").css('display', "none");
                        $("#report_page").css('display', "block");
                        $("#plan_list_page").css('display', "none");
                        var mydate = new Date();
                        var end = (mydate.getMonth() + 1);
                        var start = (mydate.getMonth() + 1);
                        if (start < 1)
                            start += 12;
                        hb_list();
                        break;
                    }
                case 3:
                    {
                        $("#make_plan_page").css('display', "block");
                        $("#report_page").css('display', "none");
                        $("#plan_list_page").css('display', "none");
                        var mydate = new Date();
                        var date = (mydate.getMonth());
                        if (date == 0)
                            date = 12;
                        $("#table_title").html(date + "月绩效详情");
                        a_list(date);
                        break;
                    }
            }
        }
    });
    function hb_list() {
        $.post("../../api/plan/hb_list.aspx", { userid: userid}, function (fd) {
            if (fd.length > 0) {
                var data = JSON.parse(fd);
                var text = "";
                for (var i = 0; i < data.list.length; i++) {

                    text += '<div class="list" style="padding-bottom: 0;" data-id="' + data.list[i].id + '" data-user="' + data.list[i].user + '" onclick="' + data.list[i].fun + '"><div class="list_line1" style="justify-content: space-around; padding: 0 0.5em"><div class="project">' + data.list[i].type + '</div><div class="name">' + data.list[i].item + '</div><div class="date">' + data.list[i].date + '</div></div><hr />';

                    text += '<div class="list_line2" style="background: #DDDDDD; font-size: 0.9em; flex-direction: column; align-items: flex-start; padding: 0.5em;color:' + data.list[i].color + '"><div class="line_text1"><div>指标/目标：</div><div>' + data.list[i].text + '</div></div><div class="line_text1"><div>评价标准：</div><div>' + data.list[i].pj_text + '</div></div><div class="line_text2"><div><span>权重：</span><span>' + data.list[i].qz + '</span></div><div><span>金额：</span><span>' + data.list[i].money + '</span></div></div><div class="line_text2"><div><div>责任人：</div><div>' + data.list[i].zr_name + '</div></div><div><div>责任班：</div><div>' + data.list[i].zr_f + '</div></div></div>';

                    if (data.list[i].wc.length > 0) {
                        text += '<div class="line_text1"><div>自评金额：</div><div>' + data.list[i].zp + '</div></div><div class="line_text3"><span>完成情况说明：</span><div>' + data.list[i].wc + '</div></div>';
                    }
                    if (data.list[i].sh_text.length > 0) {
                        text += '<div class="line_text1"><div>审核金额：</div><div>' + data.list[i].sh + '</div></div><div class="line_text3"><span>审核评语：</span><div>' + data.list[i].sh_text + '</div></div>';
                    }
                    text += '</div></div>';
                }
                $("#hb_list").html(text);
            }
            else
                $("#hb_list").html('<div style="padding:1em">本月未有下达的计划/指标</div>');
        })
    }
    function a_list(da) {
        
        $.post("../../api/plan/list.aspx", { userid: userid, date: da }, function (fd) {
     
            if (fd.length < 1)
            {
                $("#none_box").html("未查找到数据");
                $("#none_box").css("display", "block");
                $("#table_boxs").css("display","none");
            }
                
            else {
                $("#none_box").css("display", "none");
                $("#table_boxs").css("display", "table");
                var data = JSON.parse(fd);
                Object.keys(data).forEach(function (trait) {
                    $("#" + trait).html(data[trait]);
                });
                
            }
        })
    }
    function list(date) {
        dd.device.notification.showPreloader({

        })
        $.post("../../api/plan/all_list.aspx", { userid: userid, date: date }, function (fd) {
            if (fd == "none") {
                $("#list").html('<div style="padding:1em">未能查找到员工信息，请与HR部门联系更新个人数据。</div>');
            }
            else if (fd.length > 0) {
                var data = JSON.parse(fd);
                var text = "";
                for (var i = 0; i < data.list.length; i++) {
                    text += '<div class="list" data-id="' + data.list[i].id + '" onclick="' + data.list[i].fun + '"><div class="list_line1"><div class="list_type" style="background:' + data.list[i].color + '">' + data.list[i].state + '</div><div class="project">' + data.list[i].type + '</div><div class="name">' + data.list[i].zr_name + '</div><div class="date">' + data.list[i].date + '</div></div><hr /><div class="list_line2"><div class="text">' + data.list[i].text + '</div></div></div>'
                }
                $("#list").html(text);
                $.getScript('../../api/public/list.js', function () {
                });
            }
            else {
                $("#list").html('<div style="padding:1em">本月未有待汇报的计划</div>');
            }
            dd.device.notification.hidePreloader({
                onSuccess: function (result) {
                    /*{}*/
                },
                onFail: function (err) { }
            })
        })
    }
</script>
</html>
