<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="TDM.pages.pay_info.index" %>

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
    <link href="../../css/aui.css" rel="stylesheet" />
    <link href="../../css/iconfont.css" rel="stylesheet" />
    <link href="../../css/complanation.css" rel="stylesheet" />
    <style type="text/css">
        td {
            font-size:0.8em;
            text-align:center;
        }
    </style>
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
                    var str = (mydate.getMonth() + 1);
                    a(str);
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
            function show() {
                var mydate = new Date();
                var str = (mydate.getMonth() + 1) + "月";
                return str;
            }
            $("#date").html(show());
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
                        $("#date").html(result.key);
                        a(result.value);
                    },
                    onFail: function (err) { }
                })
            })
            function a(date) {
                $.post("../../api/pay/pay_list.aspx", { userid: userid, date: date }, function (fd) {
                    var data = JSON.parse(fd);
                    var text = "<tr><th>日期</th><th>余额</th><th>消费</th><th>机号</th></tr>";
                    $("#money").html(data.list[0].less);
                    for (var i = 0; i < data.list.length; i++)
                    {
                        text += '<tr><td>'+data.list[i].date+'</td><td>'+data.list[i].less+'</td><td>'+data.list[i].pay+'</td><td>'+data.list[i].code+'</td></tr> ';
                    }
                    $("#list").html(text);
                })
            }
        });
    </script>
</head>
<body style="font-family:微软雅黑">
    <header class="aui-bar aui-bar-nav" style="background: #fff" >
        <div class="aui-pull-left aui-btn aui-btn-outlined" tapmode onclick="showPopup('top-left')">
            <span class="aui-iconfont aui-icon-menu" style="color: #009688;"></span>
            <span style="margin-left: 0.5em; color: #009688;">菜单</span>
        </div>
    </header>
    <div>
        <div class="input_box" style="padding-bottom: 0.5em;">
            <div class="complanation_date_box" style="margin: 0; font-size: 1.2em;">姓名</div>
            <div class="line_text1" style="padding: 0 0.5em; font-size: 1.3em; margin: 1em 1em 0.5em">
                <img src="../../image/money.png" style="width: 1.2em; height: 1.2em;" />
                <div>卡上余额：<span id="money"></span></div>
            </div>
        </div>

        <div class="input_box" style="padding:0 0 0.5em 0;">
            <div class="complanation_date_box" style="margin:0;">
                    <i class="icon iconfont aui-iconfont icon-activity_fill" style="font-size: 1.2em;"></i>
                    <div style="margin-left: 0.5em;" id="date"></div>
                </div>
            <table align="center" style="width:100%" id="list">
                  
            </table>
        </div>
    </div>
</body>
    <script type="text/javascript">
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
                                a("../lhk/index.aspx");
                                break;
                            }
                        case 3:
                            {
                               
                                //go(ret.buttonIndex, "../pay_info/index.aspx");
                                a("index.aspx");
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
    </script>
</html>
