<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="make_plan.aspx.cs" Inherits="TDM.pages.plan.make_plan" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="maximum-scale=1.0,minimum-scale=1.0,user-scalable=0,width=device-width,initial-scale=1.0" />
    <title>TDM</title>
    <script src="../../js/jquery-1.11.0.min.js"></script>
    <script src="../../js/dingtalk.js"></script>
    <script src="../../js/api.js"></script>
    <script src="../../js/aui-tab.js"></script>
    <link href="../../css/aui.css" rel="stylesheet" />
    <link href="../../css/iconfont.css" rel="stylesheet" />
    <link href="../../css/complanation.css" rel="stylesheet" />
    <script src="../../js/aui-popup-new.js"></script>
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
                },
                onFail: function (err) {
                    logger.e('userGet fail: ' + JSON.stringify(err));
                }
            });
            function show() {
                var mydate = new Date();
                var str = "" + mydate.getFullYear() + "-";
                str += (mydate.getMonth() + 1) + "-";
                str += mydate.getDate();
                return str;
            }
            $("#zg_date").html(show());
            $("#zg_date").click(function () {
                var date = show();
                dd.biz.util.datepicker({
                    format: 'yyyy-MM-dd',
                    value: date, //默认显示日期
                    onSuccess: function (result) {
                        $("#zg_date").html(result.value)
                    },
                    onFail: function (err) { }
                })
            })
            $("#sub").click(function () {
                var flag = false;
                var title = "您有未填写项";
                if ($("#type").find("option:selected").text() == "点击选择计划类别") {
                    flag = true;
                    message = "请选择计划类别";
                }
                else if ($("#text").val().length < 1) {
                    flag = true;
                    message = "请填写工作内容"
                }
                else if ($("#complete").find("option:selected").text() == "点击选择完成方式") {
                    flag = true;
                    message = "请选择完成方式"
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
                    
                    var money=0;
                    if($("#money").text().length>0)
                    {
                        money=$("#money").text();
                    }
                    $.post("../../api/plan/make_plan.aspx", {
                        userid: userid,
                        type: $("#type").find("option:selected").text(),
                        text:$("#text").val(),
                        date:$("#zg_date").text(),
                        complete:$("#complete").find("option:selected").text(),
                        money:money,
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
                                else
                                {
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
        })
    </script>
</head>
<body style="font-family: 微软雅黑">
    <header class="aui-bar aui-bar-nav" style="background: #fff" >
        <div class="aui-pull-left aui-btn aui-btn-outlined" tapmode onclick="showPopup('top-left')">
            <span class="aui-iconfont aui-icon-menu" style="color: #009688;"></span>
            <span style="margin-left: 0.5em; color: #009688;">菜单</span>
        </div>
    </header>
    <div class="input_box">
        <div class="complanation_date_box" style="margin: 0">制定计划</div>
        <div class="font_title">计划类别</div>
        <div>
            <select id="type" style="margin: 0.5em 0 1em 1em; width: 90%; border: 1px solid #009688; border-radius: 5px; height: auto; padding: 0.2em 0.5em;">
                <option value="计划类别">点击选择计划类别</option>
                <option value="基础管理">基础管理</option>
                <option value="TDM建设">TDM建设</option>
                <option value="IT事务">IT事务</option>
                <option value="项目管理">项目管理</option>
                <option value="其他">其他</option>
            </select>
        </div>
        <div class="font_title">工作内容</div>
        <div>
            <textarea rows="5" placeholder="请输入具体工作内容" id="text" style="border:1px solid #009688;border-radius:5px;margin:0.5em 1em 1em;height:6em;width:90%;font-size:0.9em;padding:0.5em;"></textarea>
        </div>
        <div class="font_title">完成期限</div>
        <div style="border:1px solid #009688;border-radius:5px;margin:0.5em 1em 1em;width:90%;font-size:0.9em;padding:0.5em;" id="zg_date"></div>
        <div class="font_title">完成方式</div>
        <div>
            <select id="complete" style="margin: 0.5em 0 1em 1em; width: 90%; border: 1px solid #009688; border-radius: 5px; height: auto; padding: 0.2em 0.5em;">
                <option value="完成方式">点击选择完成方式</option>
                <option value="岗位指标">岗位指标</option>
                <option value="岗位职责">岗位职责</option>
                <option value="计划任务">计划任务</option>
                <option value="常规典型">常规典型</option>
                <option value="自主管理">自主管理</option>
            </select>
        </div>
        <div class="font_title">金额</div>
        <div>
            <input type="number" placeholder="0.0元" value="0" id="money" style="border:1px solid #009688;border-radius:5px;margin:0.5em 1em 1em;width:90%;padding:0.5em;height:auto;" />
        </div>
    </div>

    <div class="aui-btn aui-btn-block aui-btn-success" style="margin:0.5em;width:auto;" id="sub">确认</div>
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
                },  {
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
