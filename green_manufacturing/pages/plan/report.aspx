<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="report.aspx.cs" Inherits="TDM.pages.plan.report" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
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
        .hb_list {
            display:flex;
            flex-direction:row;
            justify-content:space-between;
            align-items:flex-start;
            width:90%;
        }
        .btn {
            display:flex;
            flex-direction:row;
            justify-content:space-between;
            align-items:center;
        }
        .line_text3 div {
            text-indent:0;
            font-size:0.9em;
        }
    </style>
    <script type="text/javascript">
        var _config = {
            appId: '<%=appId%>',
            corpId: '<%=corpId%>',
            timeStamp: '<%=timestamp%>',
            nonce: '<%=nonceStr%>',
            signature: '<%=signature%>',
            code:'<%=code%>'
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
                    $.post("../../api/plan/plan_info.aspx", { userid: userid, code: _config.code }, function (fd) {
                        if (fd != "none") {
                            var data = JSON.parse(fd);
                            $("#plan").html(data.plan);
                            if (data.user) {
                                $("#sub").css("display", "block");
                                $("#complete").css("display", "block");
                                $("#late").css("display", "block");
                            }
                            var text = "";
                            for (var i = 0; i < data.list.length; i++) {
                                text += '<div class="hb_list"><div style="min-width:6em;">' + data.list[i].date + '</div><div>' + data.list[i].text + '</div><div>' + data.list[i].state + '</div></div>'
                            }
                            $("#list").append(text);
                        }
                    })
                },
                onFail: function (err) {
                    logger.e('userGet fail: ' + JSON.stringify(err));
                }
            });

            $("#sub").click(function () {
                s("Doing");
            })
            $("#complete").click(function () {
                s("Yes");
            })
            $("#late").click(function () {
                s("No");
            })

            function s(type)
            {
                var flag=false;
                var title = "您有未填写项";
                if ($("#text").val().length < 1) {
                    flag = true;
                    message = "请填写汇报内容"
                }
                if (flag) {
                    dd.device.notification.alert({
                        message: message,
                        title: title,
                        buttonName: "知道了",
                        onSuccess: function () {
                            flag = false;
                        },
                        onFail: function (err) { }
                    });
                }
                else
                {
                    dd.device.notification.showPreloader({
                        text: "正在提交..", //loading显示的字符，空表示不显示文字
                        showIcon: true, //是否显示icon，默认true
                        onSuccess: function (result) {
                        },
                        onFail: function (err) { }
                    })
                    $.post("../../api/plan/hb.aspx", {
                        userid: userid,
                        code:_config.code,
                        type: type,
                        text:$("#text").val(),
                    }, function (fd) {
                        dd.device.notification.hidePreloader({
                            onSuccess: function (result) {
                                dd.device.notification.toast({
                                    icon: 'success', //icon样式，有success和error，默认为空 0.0.2
                                    text: "汇报完成", //提示信息
                                    duration: 2, //显示持续时间，单位秒，默认按系统规范[android只有两种(<=2s >2s)]
                                    delay: 0, //延迟显示，单位秒，默认0
                                    onSuccess: function (result) {
                                        $.post("../../api/plan/plan_info.aspx", { userid: userid, code: _config.code }, function (fd) {
                                            if (fd != "none") {
                                                var data = JSON.parse(fd);
                                                $("#plan").html(data.plan);
                                                if (data.user) {
                                                    $("#sub").css("display", "block");
                                                    $("#complete").css("display", "block");
                                                }
                                                var text = "";
                                                for (var i = 0; i < data.list.length; i++) {
                                                    text += '<div class="hb_list"><div>' + data.list[i].date + '</div><div>' + data.list[i].text + '</div><div>' + data.list[i].state + '</div></div>'
                                                }
                                                $("#list").html("");
                                                $("#list").append(text);
                                            }
                                        })
                                    },
                                    onFail: function (err) { }
                                })
                            }
                        })
                    })
                }
            }
        })
    </script>
</head>
<body>
    <header class="aui-bar aui-bar-nav" style="background: #fff" >
        <div class="aui-pull-left aui-btn aui-btn-outlined" tapmode onclick="showPopup('top-left')">
            <span class="aui-iconfont aui-icon-menu" style="color: #009688;"></span>
            <span style="margin-left: 0.5em; color: #009688;">菜单</span>
        </div>
    </header>
    <div class="input_box" style="padding:0.5em 1em;">
        <div class="line_text1" style="color:#009688">计划：<span id="plan"></span></div>
         <span style="color:#009688">汇报信息：</span>
        <div class="line_text3" id="list">
           

        </div>
    </div>
    <div class="input_box">
        <div class="font_title">汇报内容</div>
        <div>
            <textarea rows="5" placeholder="请输入汇报内容" id="text" style="border:1px solid #009688;border-radius:5px;margin:0.5em 1em 1em;height:6em;width:90%;font-size:0.9em;padding:0.5em;"></textarea>
        </div>
    </div>
    <div class="btn">
    <div class="aui-btn aui-btn-warning aui-btn-block" style="margin:0 0.5em;width:45%;background:#FF5722 !important;display:none;" id="sub">提交汇报</div>
    <div class="aui-btn aui-btn-success aui-btn-block" style="margin:0 0.5em;width:45%;display:none;" id="complete">完成计划</div>
        <div class="aui-btn aui-btn-danger aui-btn-block" style="margin:0 0.5em;width:45%;display:none;" id="late">计划推迟</div>
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
                                //go(ret.buttonIndex, "../kq/index.aspx");
                                a("../kq/index.aspx");
                                break;
                            }
                        case 4:
                            {
                                //go(ret.buttonIndex, "../pay_info/index.aspx");
                                a("../pay_info/index.aspx");
                                break;
                            }
                        case 5:
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
