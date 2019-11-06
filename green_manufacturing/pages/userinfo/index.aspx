<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="TDM.pages.userinfo.index" %>

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
                    $.post("../../api/userinfo/info.aspx", { userid: userid }, function (fd) {
                        var data = JSON.parse(fd);
                        Object.keys(data).forEach(function (trait) {
                                $("#" + trait).html(data[trait]);
                        });
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
            $("#sub").click(function () {
                dd.device.notification.confirm({
                    message: "此功能会重置电脑端桌面TDM的密码，初始密码为“1”。",
                    title: "请注意",
                    buttonLabels: ['确定', '取消'],
                    onSuccess: function (result) {
                        if (result.buttonIndex == 0)
                        {
                            $.post("../../api/userinfo/pwd.aspx", { userid: userid }, function () {
                                dd.device.notification.toast({
                                    icon: 'success', //icon样式，有success和error，默认为空 0.0.2
                                    text: "重置成果", //提示信息
                                    duration: 2, //显示持续时间，单位秒，默认按系统规范[android只有两种(<=2s >2s)]
                                    delay: 0, //延迟显示，单位秒，默认0
                                    onSuccess: function (result) {
                                        /*{}*/
                                    },
                                    onFail: function (err) { }
                                })
                            })
                        }
                        //onSuccess将在点击button之后回调
                        /*
                        {
                            buttonIndex: 0 //被点击按钮的索引值，Number类型，从0开始
                        }
                        */
                    },
                    onFail: function (err) { }
                });
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
    <div class="input_box" style="background:#009688;padding:1.2em 0.5em;display:flex;color:#fff;;flex-direction:column;justify-content:center;align-items:center;">
        <img src="../../image/user_jc.png" style="width:5em;height:5em;"/>
        <div style="margin-top:1em;" id="name">姓名</div>
    </div>
    <div class="input_box" style="padding:1em 0.8em;">
        <div class="line_text1">公司：<span id="cpy"></span></div>
        <div class="line_text1">部门：<span id="bm"></span></div>
        <div class="line_text1">经营体：<span id="jyt"></span></div>
        <div class="line_text1">岗位：<span id="gw"></span></div>
        <div class="line_text1">岗位类别：<span id="gwlb"></span></div>
        <div class="line_text1">性别：<span id="sex"></span></div>
        <div class="line_text1">电话：<span id="phone"></span></div>
        <div class="line_text1">婚姻状况：<span id="marry"></span></div>
        <div class="line_text1">籍贯：<span id="home"></span></div>
        <div class="line_text1">出生日期：<span id="br_day"></span></div>
        <div class="line_text1">身份证所在地：<span id="sc_card"></span></div>
        <div class="line_text1">户口所在地：<span id="hk_ad"></span></div>
        <div class="line_text1">人事档案所在地：<span id="rsda"></span></div>
        <div class="line_text1">职称：<span id="zc"></span></div>
        <div class="line_text1">职称授予时间：<span id="zc_date"></span></div>
        <div class="line_text1">技能级别：<span id="jn"></span></div>
        <div class="line_text1">健康级别：<span id="jk"></span></div>
        <div class="line_text1">品质级别：<span id="pz"></span></div>
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
                                a("../pay_info/index.aspx");
                                break;
                            }
                        case 4:
                            {
                                //go(ret.buttonIndex, "../userid/index.aspx");
                                a("index.aspx");
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
