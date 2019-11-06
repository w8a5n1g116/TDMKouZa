<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="TDM.pages.lhk.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="maximum-scale=1.0,minimum-scale=1.0,user-scalable=0,width=device-width,initial-scale=1.0" />
    <title>TDM</title>
    <script src="../../js/jquery-1.11.0.min.js"></script>
    <script src="../../js/aui-popup-new.js"></script>
    <script src="../../js/dingtalk.js"></script>
    <script src="../../js/api.js"></script>
    <script src="../../js/aui-tab.js"></script>
    <link href="../../css/aui.css" rel="stylesheet" />
    <link href="../../css/iconfont.css" rel="stylesheet" />
    <link href="../../css/complanation.css" rel="stylesheet" />
    <script src="../../js/dialog.js"></script>
    <script type="text/javascript">
        var _config = {
            appId: '<%=appId%>',
            corpId: '<%=corpId%>',
            timeStamp: '<%=timestamp%>',
            nonce: '<%=nonceStr%>',
            signature: '<%=signature%>'
        };
        var userid = "";
        var page = 1;
        var datemonth = "";
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
                    var str = (mydate.getMonth() + 1);
                    lhk_list(parseInt(str)-1);
                    pj_list(str);
                    gr_list(str);
                    sl_list(str);
                    $.post("../../api/public/user_info.aspx", { userid: userid }, function (fd) {
                        if (fd == "主管" || fd == "经理")
                            $("#lhpj").css("display", "table-cell");
                    })
                },
                onFail: function (err) {
                    logger.e('userGet fail: ' + JSON.stringify(err));
                }
            });
            dd.biz.navigation.setLeft({
                show: true,//控制按钮显示， true 显示， false 隐藏， 默认true
                control: true,//是否控制点击事件，true 控制，false 不控制， 默认false
                showIcon: true,//是否显示icon，true 显示， false 不显示，默认true； 注：具体UI以客户端为准
                text: '关闭',//控制显示文本，空字符串表示显示默认文本
                onSuccess: function (result) {
                    dd.biz.navigation.close({
                        onSuccess: function (result) {

                        },
                        onFail: function (err) { }
                    })
                },
                onFail: function (err) { }
            });
            show();
            $("#date").html(datemonth+"月");
            $("#pj_date").html(show());
            $("#date2").html(show());
            $("#date3").html(show());
            $("#date").click(function () {
                dd.biz.util.chosen({
                    source: [{
                        key: '1月', //显示文本
                        value: '1' //值，
                    }, {
                        key: '2月',
                        value: '2'
                    }
                    , {
                        key: '3月',
                        value: '3'
                    }
                    , {
                        key: '4月',
                        value: '4'
                    }
                    , {
                        key: '5月',
                        value: '5'
                    }
                    , {
                        key: '6月',
                        value: '6'
                    }
                    , {
                        key: '7月',
                        value: '7'
                    }
                    , {
                        key: '8月',
                        value: '8'
                    }
                    , {
                        key: '9月',
                        value: '9'
                    }
                    , {
                        key: '10月',
                        value: '10'
                    }
                    , {
                        key: '11月',
                        value: '11'
                    }, {
                        key: '12月',
                        value: '12'
                    }],
                    selectedKey: show(), // 默认选中的key
                    onSuccess: function (result) {
                        $("#date").html((parseInt(result.value) - 1)+"月");
                        datemonth = parseInt(result.value) - 1;
                        if ((parseInt(result.value) - 1) == 0)
                            datemonth = 12;
                        lhk_list(result.value)
                        //onSuccess将在点击完成之后回调
                        /*
                        {
                            key: '选项2',
                            value: '234'
                        }
                        */
                    },
                    onFail: function (err) { }
                })
            })
            $("#pj_date").click(function () {
                dd.biz.util.chosen({
                    source: [{
                        key: '1月', //显示文本
                        value: '1' //值，
                    }, {
                        key: '2月',
                        value: '2'
                    }
                    , {
                        key: '3月',
                        value: '3'
                    }
                    , {
                        key: '4月',
                        value: '4'
                    }
                    , {
                        key: '5月',
                        value: '5'
                    }
                    , {
                        key: '6月',
                        value: '6'
                    }
                    , {
                        key: '7月',
                        value: '7'
                    }
                    , {
                        key: '8月',
                        value: '8'
                    }
                    , {
                        key: '9月',
                        value: '9'
                    }
                    , {
                        key: '10月',
                        value: '10'
                    }
                    , {
                        key: '11月',
                        value: '11'
                    }, {
                        key: '12月',
                        value: '12'
                    }],
                    selectedKey: show(), // 默认选中的key
                    onSuccess: function (result) {
                        $("#pj_date").html(result.key);

                        //onSuccess将在点击完成之后回调
                        /*
                        {
                            key: '选项2',
                            value: '234'
                        }
                        */
                    },
                    onFail: function (err) { }
                })
            })
            $("#date2").click(function () {
                dd.biz.util.chosen({
                    source: [{
                        key: '1月', //显示文本
                        value: '1' //值，
                    }, {
                        key: '2月',
                        value: '2'
                    }
                    , {
                        key: '3月',
                        value: '3'
                    }
                    , {
                        key: '4月',
                        value: '4'
                    }
                    , {
                        key: '5月',
                        value: '5'
                    }
                    , {
                        key: '6月',
                        value: '6'
                    }
                    , {
                        key: '7月',
                        value: '7'
                    }
                    , {
                        key: '8月',
                        value: '8'
                    }
                    , {
                        key: '9月',
                        value: '9'
                    }
                    , {
                        key: '10月',
                        value: '10'
                    }
                    , {
                        key: '11月',
                        value: '11'
                    }, {
                        key: '12月',
                        value: '12'
                    }],
                    selectedKey: show(), // 默认选中的key
                    onSuccess: function (result) {
                        $("#date2").html(result.key);
                        gr_list(result.value);
                        //onSuccess将在点击完成之后回调
                        /*
                        {
                            key: '选项2',
                            value: '234'
                        }
                        */
                    },
                    onFail: function (err) { }
                })
            })
            $("#date3").click(function () {
                dd.biz.util.chosen({
                    source: [{
                        key: '1月', //显示文本
                        value: '1' //值，
                    }, {
                        key: '2月',
                        value: '2'
                    }
                    , {
                        key: '3月',
                        value: '3'
                    }
                    , {
                        key: '4月',
                        value: '4'
                    }
                    , {
                        key: '5月',
                        value: '5'
                    }
                    , {
                        key: '6月',
                        value: '6'
                    }
                    , {
                        key: '7月',
                        value: '7'
                    }
                    , {
                        key: '8月',
                        value: '8'
                    }
                    , {
                        key: '9月',
                        value: '9'
                    }
                    , {
                        key: '10月',
                        value: '10'
                    }
                    , {
                        key: '11月',
                        value: '11'
                    }, {
                        key: '12月',
                        value: '12'
                    }],
                    selectedKey: show(), // 默认选中的key
                    onSuccess: function (result) {
                        $("#date3").html(result.key);
                        sl_list(str);
                        //onSuccess将在点击完成之后回调
                        /*
                        {
                            key: '选项2',
                            value: '234'
                        }
                        */
                    },
                    onFail: function (err) { }
                })
            })
            function show() {
                var mydate = new Date();
                var str = (mydate.getMonth() + 1) + "月";
                if (mydate.getMonth() == "0")
                    datemonth = 12;
                else
                    datemonth = mydate.getMonth();
                return str;
            }
            document.addEventListener('resume', function () {
                var mydate = new Date();
                var str = (mydate.getMonth() + 1);
                lhk_list(str);
            });
            function lhk_list(date) {
                dd.device.notification.showPreloader({

                })
                $.post("../../api/lhk/list.aspx", { userid: userid, date: datemonth ,page:page}, function (fd) {
                    if (fd.length > 0) {
                        var data = JSON.parse(fd);
                        var text = "";
                        var color = "#000";
                        for (var i = 0; i < data.list.length; i++) {
                            if (data.list[i].sh_text.length > 0)
                                color = "#0088F5";
                            text += '<div class="list" style="padding-bottom: 0;" data-id="' + data.list[i].id + '" data-user="' + data.list[i].user + '" onclick="' + data.list[i].fun + '"><div class="list_line1" style="justify-content: space-around; padding: 0 0.5em"><div class="list_type" style="margin: 0;background:#009688">量化卡</div><div class="project">' + data.list[i].type + '</div><div class="name">' + data.list[i].item + '</div><div class="date">' + data.list[i].date + '</div></div><hr />';

                            text += '<div class="list_line2" style="background: #DDDDDD; font-size: 0.9em; flex-direction: column; align-items: flex-start; padding: 0.5em;color:' + data.list[i].color + '"><div class="line_text1"><div>指标/目标：</div><div>' + data.list[i].text + '</div></div><div class="line_text1"><div>评价标准：</div><div>' + data.list[i].pj_text + '</div></div><div class="line_text2"><div><span>权重：</span><span>' + data.list[i].qz + '</span></div><div><span>金额：</span><span>' + data.list[i].money + '</span></div></div><div class="line_text2"><div><div>责任人：</div><div>' + data.list[i].zr_name + '</div></div><div><div>责任班：</div><div>' + data.list[i].zr_f + '</div></div></div>';

                            if (data.list[i].wc.length > 0) {
                                text += '<div class="line_text1"><div>自评金额：</div><div>' + data.list[i].zp + '</div></div><div class="line_text3"><span>完成情况说明：</span><div>' + data.list[i].wc + '</div></div>';
                            }
                            if (data.list[i].sh_text.length > 0) {
                                text += '<div class="line_text1"><div>审核金额：</div><div>' + data.list[i].sh + '</div></div><div class="line_text3"><span>审核评语：</span><div>' + data.list[i].sh_text + '</div></div>';
                            }
                            text += '</div></div>';

                        }
                        $("#lhk").html(text);
                       
                        page += 1;
                        $.getScript('../../api/public/list.js', function () {
                        });
                        
                    }
                    else
                    {
                        if(page==1)
                            $("#lhk").html("没有查找到量化卡数据");
                    }
                    dd.device.notification.hidePreloader({
                        onSuccess: function (result) {
                            /*{}*/
                        },
                        onFail: function (err) { }
                    })
                })
            }
            function pj_list(date) {
                $.post("../../api/lhk/pj_list.aspx", { userid: userid, date: date }, function (fd) {
                    var data = JSON.parse(fd);
                    var text = '<div class="complanation_date_box" style="margin: 0;">绩效详情</div>';
                    for (var i = 0; i < data.list.length; i++) {
                        text += '<div class="table"><div class="name" style="min-width:4em;">' + data.list[i].name + '</div><div class="lines"><div class="line_info"><div class="info"><div>绩效工资</div><div>' + data.list[i].jxgz + '</div></div><div class="info"><div>超额净收益</div><div>' + data.list[i].cejsy + '</div></div><div class="info"><div>岗位指标</div><div>' + data.list[i].gwzb + '</div></div><div class="info"><div>岗位职责</div><div>' + data.list[i].gwzz + '</div></div><div class="info"><div>计划任务</div><div>' + data.list[i].jhrw + '</div></div></div><div class="line_info"><div class="info"><div>常规典型</div><div>' + data.list[i].cgdx + '</div></div><div class="info"><div>6s管理</div><div>' + data.list[i].sgl + '</div></div><div class="info"><div>班组建设</div><div>' + data.list[i].bzjs + '</div></div><div class="info"><div>培训</div><div>' + data.list[i].px + '</div></div></div></div></div>';
                    }
                    $("#table").html(text);

                })
            }
            $("#make_sl").click(function () {
                dd.ui.nav.preload({
                    pages: [ //需要预加载的页面数组
                      {
                          id: 'page_id1', //预加载的页面id
                          url: 'add.aspx' //页面url
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
            function gr_list(date) {
                $.post("../../api/lhk/gr_list.aspx", { userid: userid, date: date }, function (fd) {
                    if (fd.length > 0) {
                        var data = JSON.parse(fd);
                        var text = '';
                        for (var i = 0; i < data.list.length; i++) {
                            text += '<div class="list" style="padding:0;"><div class="complanation_date_box" style="margin:0;justify-content: space-around;width:100%;"><div>个人事例</div><div>常规典型</div><div>提出人：' + data.list[i].name + '</div></div><div style="padding:0.5em;text-indent:2em;justify-content:flex-start;">' + data.list[i].text + '</div><div class="complanation_date_box" style="margin:0;justify-content: space-between;width:100%;background:#DDDDDD;color:#000;"><div>自评金额：' + data.list[i].zp + '</div><div>审核金额：' + data.list[i].sh + '</div></div><div class="complanation_date_box" style="margin:0;justify-content: flex-start;width:100%;background:#DDDDDD;color:#000;"><div>责任人：' + data.list[i].name + '</div></div></div>';
                        }
                        $("#gr_list").html(text);
                    }
                    else
                        $("#gr_list").html("未找到个人事例");
                })
            }
            function sl_list(date) {
                $.post("../../api/lhk/sl_list.aspx", { userid: userid, date: date }, function (fd) {
                    
                    if (fd.length > 0) {
                        var data =JSON.parse(fd);
                        var text = '';
                        for (var i = 0; i < data.list.length; i++) {
                            text += '<div class="list" style="padding:0;"><div class="complanation_date_box" style="margin:0;justify-content: space-around;width:100%;"><div>个人事例</div><div>常规典型</div><div>提出人：' + data.list[i].name + '</div></div><div style="padding:0.5em;text-indent:2em;justify-content:flex-start;">' + data.list[i].text + '</div><div class="complanation_date_box" style="margin:0;justify-content: space-between;width:100%;background:#DDDDDD;color:#000;"><div>自评金额：' + data.list[i].zp + '</div><div>审核金额：' + data.list[i].sh + '</div></div><div class="complanation_date_box" style="margin:0;justify-content: flex-start;width:100%;background:#DDDDDD;color:#000;"><div>责任人：' + data.list[i].name + '</div></div></div>';
                        }
                        $("#sl_list").html(text);
                    }
                    else
                        $("#sl_list").html("未能查找到事例信息");
                })
            }
        });
    </script>
</head>
<body style="font-family: 微软雅黑">
    <header class="aui-bar aui-bar-nav" style="background: #fff">
        <div class="aui-pull-left aui-btn aui-btn-outlined" tapmode onclick="showPopup('top-left')">
            <span class="aui-iconfont aui-icon-menu" style="color: #009688;"></span>
            <span style="margin-left: 0.5em; color: #009688;">菜单</span>
        </div>
    </header>
    <div></div>
    <div id="page" style="margin-bottom: 5em;">

        <div id="make_plan_page">

            <div class="complanation_date_box">
                <i class="icon iconfont aui-iconfont icon-activity_fill" style="font-size: 1.2em;"></i>
                <div style="margin-left: 0.5em;" id="date"></div>
            </div>
            <div class="list_box" id="lhk">
            </div>
        </div>

        <div id="report_page" style="display: none">
            <!--<div class="input_box">
                    <div class="complanation_date_box" style="margin: 0;">共享集团--DT中心</div>
                    <div class="line_text1" style="padding: 0 0.5em; align-items: center;">
                        <i class="icon iconfont aui-iconfont icon-activity_fill" style="font-size: 1.2em;"></i>
                        <div style="margin-left: 0.5em;" id="pj_date">7月</div>
                    </div>
                    <div class="line_text1" style="padding: 0 0.5em;">
                        <div>人数：<span>27</span></div>
                    </div>
                    <div class="line_text1" style="padding: 0 0.5em; justify-content: space-around; width: 100%; font-size: 0.9em">
                        <div class="ctext">
                            <div>标准绩效</div>
                            <div>1000</div>
                        </div>
                        <div class="ctext">
                            <div>实际绩效</div>
                            <div>1000</div>
                        </div>
                        <div class="ctext">
                            <div>实际比例</div>
                            <div>1000</div>
                        </div>
                        <div class="ctext">
                            <div>人均绩效</div>
                            <div>1000</div>
                        </div>
                    </div>
                </div>-->

            <div class="input_box" id="table">
                <div class="complanation_date_box" style="margin: 0;">绩效详情</div>

            </div>
        </div>

        <div id="plan_list_page" style="display: none">
            <div class="aui-tab" id="tab">
                <div class="aui-tab-item aui-active">个人事例</div>
                <div class="aui-tab-item">事例查看</div>
            </div>
            <div id="gr">
                <div class="complanation_box" id="make_sl">
                    <img src="../../image/add.png" />
                    <div>添加个人事例</div>
                </div>
                <div class="complanation_date_box">
                    <i class="icon iconfont aui-iconfont icon-activity_fill" style="font-size: 1.2em;"></i>
                    <div style="margin-left: 0.5em;" id="date2"></div>
                </div>
                <div class="list_box" id="gr_list">
                    
                </div>
            </div>

            <div id="list" style="display: none">
                <div class="complanation_date_box">
                    <i class="icon iconfont aui-iconfont icon-activity_fill" style="font-size: 1.2em;"></i>
                    <div style="margin-left: 0.5em;" id="date3"></div>
                </div>
                <div class="list_box" id="sl_list">
                    

                </div>
            </div>
        </div>
    </div>
    <footer class="aui-bar aui-bar-tab" id="footer">
        <div class="aui-bar-tab-item aui-active" tapmode>
            <i class="aui-iconfont aui-icon-edit"></i>
            <div class="aui-bar-tab-label">量化卡</div>
        </div>
        <div class="aui-bar-tab-item" tapmode id="lhpj" style="display: none">
            <i class="aui-iconfont aui-icon-comment"></i>
            <div class="aui-bar-tab-label">量化评价</div>
        </div>
        <div class="aui-bar-tab-item" tapmode>
            <i class="icon iconfont icon-createtask aui-iconfont"></i>
            <div class="aui-bar-tab-label">典型事例</div>
        </div>
    </footer>
</body>
<script type="text/javascript">
    apiready = function () {
        api.parseTapmode();
    }
    var tab1 = new auiTab({
        element: document.getElementById("tab"),
    }, function (ret) {
        switch (ret.index) {
            case 1:
                {
                    $("#gr").css('display', "block");
                    $("#list").css('display', "none");

                    break;
                }
            case 2:
                {
                    $("#gr").css('display', "none");
                    $("#list").css('display', "block");
                    break;
                }
        }
    });
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
                            a("../plan/index.aspx")
                            break;
                        }
                    case 2:
                        {
                            //go(ret.buttonIndex, "../lhk/index.aspx");
                            a("index.aspx");
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
        console.log(ret);
        if (ret) {
            switch (ret.index) {
                case 1:
                    {

                        $("#make_plan_page").css('display', "block");
                        $("#report_page").css('display', "none");
                        $("#plan_list_page").css('display', "none");
                        break;
                    }
                case 2:
                    {
                        $("#make_plan_page").css('display', "none");
                        $("#report_page").css('display', "block");
                        $("#plan_list_page").css('display', "none");

                        break;
                    }
                case 3:
                    {
                        $("#make_plan_page").css('display', "none");
                        $("#report_page").css('display', "none");
                        $("#plan_list_page").css('display', "block");
                        break;
                    }
            }
        }
    });
</script>
</html>
