<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="TDM.pages.kq.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
</head>
<body style="font-family:微软雅黑">
    <header class="aui-bar aui-bar-nav" style="background: #fff" >
        <div class="aui-pull-left aui-btn aui-btn-outlined" tapmode onclick="showPopup('top-left')">
            <span class="aui-iconfont aui-icon-menu" style="color: #009688;"></span>
            <span style="margin-left: 0.5em; color: #009688;">菜单</span>
        </div>
    </header>
    <div>
        <div class="complanation_date_box">
            <i class="icon iconfont aui-iconfont icon-activity_fill" style="font-size: 1.2em;"></i>
            <div style="margin-left: 0.5em;" id="date"></div>
        </div>

        <div class="input_box" style="padding:0.5em 0;">
            <table align="center" style="width:100%">
                <tr>
                    <th>姓名</th>
                    <th>经营体</th>
                    <th>应到</th>
                    <th>实到</th>
                    <th>请假</th>
                </tr>  
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
                    image: '../../image/task.png',
                    text: '考勤明细',
                    value: 'kq'
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
                                //go(ret.buttonIndex, "../kq/index.aspx");
                                a("index.aspx");
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
