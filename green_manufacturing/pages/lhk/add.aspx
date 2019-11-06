<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="add.aspx.cs" Inherits="TDM.pages.lhk.add" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="maximum-scale=1.0,minimum-scale=1.0,user-scalable=0,width=device-width,initial-scale=1.0" />
    <title>TDM</title>
    <script src="../../js/jquery-1.11.0.min.js"></script>
    <script src="../../js/dingtalk.js"></script>
    <script src="../../js/api.js"></script>
    <script src="../../js/aui-tab.js"></script>
    <link href="../../css/aui.css" rel="stylesheet" />
    <link href="../../css/iconfont.css" rel="stylesheet" />
    <link href="../../css/complanation.css" rel="stylesheet" />
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
                    var sct = document.getElementById('complete');
                    sct.options.add(new Option(info.nickName, info.nickName));
                },
                onFail: function (err) {
                    logger.e('userGet fail: ' + JSON.stringify(err));
                }
            });

            $("#sub").click(function () {
                var flag = false;
                var title = "您有未填写项";
                if ($("#complete").find("option:selected").text() == "请选择责任人") {
                    flag = true;
                    message = "请选择责任人";
                }
                else if ($("#text").val().length < 1) {
                    flag = true;
                    message = "请填写事例描述"
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

                else {
                    dd.device.notification.showPreloader({
                        text: "正在提交..", //loading显示的字符，空表示不显示文字
                        showIcon: true, //是否显示icon，默认true
                        onSuccess: function (result) {
                        },
                        onFail: function (err) { }
                    })

                    var money = 0;
                    if ($("#money").text().length > 0) {
                        money = $("#money").text();
                    }
                    $.post("../../api/lhk/make_sl.aspx", {
                        userid: userid,
                        text: $("#text").val(),
                        zr_name: $("#complete").find("option:selected").text(),
                        money: money,
                    }, function (fd) {
                        dd.device.notification.hidePreloader({
                            onSuccess: function (result) {
                                if (fd.length > 0) {
                                    dd.device.notification.toast({
                                        icon: 'success', //icon样式，有success和error，默认为空 0.0.2
                                        text: '提交完成', //提示信息
                                        duration: 2, //显示持续时间，单位秒，默认按系统规范[android只有两种(<=2s >2s)]
                                        delay: 0, //延迟显示，单位秒，默认0
                                        onSuccess: function (result) {
                                            setTimeout(function () {
                                                dd.biz.navigation.goBack()
                                            }, 1500);
                                        },
                                        onFail: function (err) { }
                                    })
                                }
                                else {
                                    dd.device.notification.toast({
                                        icon: 'error', //icon样式，有success和error，默认为空 0.0.2
                                        text: '提交失败', //提示信息
                                        duration: 2, //显示持续时间，单位秒，默认按系统规范[android只有两种(<=2s >2s)]
                                        delay: 0, //延迟显示，单位秒，默认0
                                        onSuccess: function (result) {
                                            setTimeout(function () {
                                                dd.biz.navigation.goBack()
                                            }, 1500);
                                        },
                                        onFail: function (err) { }
                                    })
                                }
                            },
                            onFail: function (err) { }
                        })

                    })
                }
            })
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
    <div class="input_box">
        <div class="complanation_date_box" style="margin: 0">个人事例信息填写</div>
     
        <div class="font_title">事例描述</div>
        <div>
            <textarea rows="5" placeholder="请输入个人事例的文字描述" id="text" style="border:1px solid #009688;border-radius:5px;margin:0.5em 1em 1em;height:6em;width:90%;font-size:0.9em;padding:0.5em;"></textarea>
        </div>
        <div class="font_title">责任人</div>
        <div>
            <select id="complete" style="margin: 0.5em 0 1em 1em; width: 90%; border: 1px solid #009688; border-radius: 5px; height: auto; padding: 0.2em 0.5em;">
                <option value="完成方式">请选择责任人</option>
                
            </select>
        </div>
        <div class="font_title">自评金额</div>
        <div>
            <input type="number" id="money" placeholder="0.0元" value="0" style="border:1px solid #009688;border-radius:5px;margin:0.5em 1em 1em;width:90%;padding:0.5em;height:auto;" />
        </div>
    </div>

    <div class="aui-btn aui-btn-block aui-btn-success" style="margin:0.5em;width:auto;" id="sub">添加个人事例</div>
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
                                a("index.aspx");
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
