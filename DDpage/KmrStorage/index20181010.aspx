<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index20181010.aspx.cs" Inherits="DDpage.KmrStorage.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1,maximum-scale=1,user-scalable=no" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black" />
    <link href="../css/iconfont.css" rel="stylesheet" />

    <!--标准mui.css-->
    <link rel="stylesheet" href="../css/mui.min.css" />
    <link rel="stylesheet" type="text/css" href="../css/app.css"/>
    <style type="text/css">
        .fix_icon {
            background-color: #00bcd4;
            color: #fff;
            border-radius: 10px
        }

        /*.fix_font{
            font-size:10px !important;
        }*/
       .fix_slider{
           height:12em;
       }
   
    </style>
</head>
<body>
    <div class="mui-content">
        <div id="slider" class="mui-slider fix_slider" >
			<div class="mui-slider-group mui-slider-loop">
				<!-- 额外增加的一个节点(循环轮播：第一个节点是最后一张轮播) -->
				<div class="mui-slider-item mui-slider-item-duplicate">
					<a href="#">
						<img src="../images/01.jpg" class="fix_slider">
					</a>
				</div>
				<!-- 第一张 -->
				<div class="mui-slider-item">
					<a href="#">
						<img src="../images/05.jpg" class="fix_slider"/>
					</a>
				</div>
				<!-- 第二张 -->
				<div class="mui-slider-item">
					<a href="#">
						<img src="../images/01.jpg" class="fix_slider"/>
					</a>
				</div>
				<!-- 额外增加的一个节点(循环轮播：最后一个节点是第一张轮播) -->
				<div class="mui-slider-item mui-slider-item-duplicate">
					<a href="#">
						<img src="../images/05.jpg" class="fix_slider"/>
					</a>
				</div>
			</div>
			<div class="mui-slider-indicator">
				<div class="mui-indicator mui-active"></div>
				<div class="mui-indicator"></div>
			</div>
		</div>




        <ul class="mui-table-view mui-grid-view mui-grid-9">
            
            <li class="mui-table-view-cell mui-media mui-col-xs-4 mui-col-sm-3 mui-hidden" id="pick_tablist.aspx">
                <a href="javascript:void(0)">
                    <span class="mui-icon iconfont icon-wuliaolingyong fix_icon"></span>
                    <div class="mui-media-body">公司内领料</div>
                </a>
            </li>
             <li class="mui-table-view-cell mui-media mui-col-xs-4 mui-col-sm-3 mui-hidden" id="confirm_tablist.aspx">
                <a href="javascript:void(0)">
                    <span class="mui-icon iconfont icon-bumenlingliaochuku fix_icon"></span>
                    <div class="mui-media-body">领料确认</div>
                </a>
            </li>
            <li class="mui-table-view-cell mui-media mui-col-xs-4 mui-col-sm-3 mui-hidden" id="checkIn_tablist.aspx">
                <a href="javascript:void(0)">
                    <span class="mui-icon iconfont icon-rukuyanshou fix_icon"></span>
                    <div class="mui-media-body fix_font">验收入库</div>
                </a>
            </li>                     
            <li class="mui-table-view-cell mui-media mui-col-xs-4 mui-col-sm-3 mui-hidden" id="MaterialStockMoveList.aspx">
                <a href="javascript:void(0)">
                    <span class="mui-icon iconfont icon-tiaobodan fix_icon"></span>
                    <div class="mui-media-body">仓库调拨</div>
                </a>
            </li>
            <li class="mui-table-view-cell mui-media mui-col-xs-4 mui-col-sm-3 mui-hidden" id="MaterialListSel.aspx">
                <a href="javascript:void(0)">
                    <span class="mui-icon iconfont icon-kucunchaxun fix_icon"></span>
                    <div class="mui-media-body">库存查询</div>
                </a>
            </li>
            <li class="mui-table-view-cell mui-media mui-col-xs-4 mui-col-sm-3 mui-hidden" id="working.aspx">
                <a href="javascript:void(0)">
                    <span class="mui-icon iconfont icon-dibang2 fix_icon"></span>
                    <div class="mui-media-body">地磅检斤</div>
                </a>
            </li>
            <li class="mui-table-view-cell mui-media mui-col-xs-4 mui-col-sm-3 mui-hidden" id="pick_saplist.aspx">
                <a href="javascript:void(0)">
                    <span class="mui-icon iconfont icon-wuliaolingyong fix_icon"></span>
                    <div class="mui-media-body">跨公司领料</div>
                </a>
            </li>
            <li class="mui-table-view-cell mui-media mui-col-xs-4 mui-col-sm-3 mui-hidden" id="stock_tablist.aspx">
                <a href="javascript:void(0)">
                    <span class="mui-icon iconfont icon-icon1122 fix_icon"></span>
                    <div class="mui-media-body">领料备餐</div>
                </a>
            </li>
            <li class="mui-table-view-cell mui-media mui-col-xs-4 mui-col-sm-3 mui-hidden" id="de_tablist.aspx">
                <a href="javascript:void(0)">
                    <span class="mui-icon iconfont icon-peisong fix_icon"></span>
                    <div class="mui-media-body">配送发货</div>
                </a>
            </li>
            <li class="mui-table-view-cell mui-media mui-col-xs-4 mui-col-sm-3 mui-hidden" id="check_tablist.aspx">
                <a href="javascript:void(0)">
                    <span class="mui-icon iconfont icon-jiaogongyanshou fix_icon"></span>
                    <div class="mui-media-body">工厂验收</div>
                </a>
            </li>
            
           
            <li class="mui-table-view-cell mui-media mui-col-xs-4 mui-col-sm-3 mui-hidden" id="close_account.aspx">
                <a href="javascript:void(0)">
                    <span class="mui-icon mui-icon-gear fix_icon"></span>
                    <div class="mui-media-body">财务关账</div>
                </a>
            </li>

        </ul>
    </div>
</body>
</html>
<script src="../js/jquery-1.11.0.min.js"></script>
<script src="../js/dingtalk.js"></script>
<%--<script src="../js/ddjs.js"></script>--%>
<script src="../js/mobileSelect.js"></script>
<script src="../js/api.js"></script>
<script src="../js/aui-toast.js"></script>
<script src="../js/mui.min.js"></script>
<script type="text/javascript">
    var _config = {
        appId: '<%=appId%>',
        corpId: '<%=corpId%>',
        timeStamp: '<%=timestamp%>',
        nonce: '<%=nonceStr%>',
        signature: '<%=signature%>',
        title: '<%=title%>'
    };
    var userinfo="";
    var OpenLink;
    dd.config({
        appId: _config.appId,
        corpId: _config.corpId,
        timeStamp: _config.timeStamp,
        nonceStr: _config.nonce,
        signature: _config.signature,
        jsApiList: ['biz.user.get', 'device.geolocation.get', 'biz.map.locate', 'biz.util.uploadImageFromCamera', 'biz.util.uploadImage','biz.util.scan']
    });

		mui.init({
			swipeBack:true //启用右滑关闭功能
    });

        var slider = mui("#slider");
            slider.slider({
                interval: 5000
            });
           
 


        dd.ready(function () {
            dd.biz.user.get({
                onSuccess: function (info) {
                    $.ajax({
                        data: { userid: info.emplId },
                        type: "POST",
                        url: "../api/public/user_info.aspx",
                        async: false,
                        success: function (fd) {
                            info.phone = fd;
                            userinfo = info;
                            //showPreloader();
                            status(info);
                           
                            
                            return;
                        }
                    })
                    return;
                },
                onFail: function (err) {

                }
            })

            showPreloader = function () {
                dd.device.notification.showPreloader({
                    text: "页面加载中..", //loading显示的字符，空表示不显示文字
                    showIcon: true, //是否显示icon，默认true
                    onSuccess: function (result) {
                        /*{}*/
                    },
                    onFail: function (err) { }
                })
            }

            hidePreloader = function () {
                dd.device.notification.hidePreloader({
                    onSuccess: function (result) {
                        /*{}*/
                    },
                    onFail: function (err) { }
                })
            }

            goBack = function () {
                dd.biz.navigation.goBack({
                    onSuccess: function (result) {
                        /*result结构
                        {}
                        */
                    },
                    onFail: function (err) { }
                })
            }


            toastInfo = function (info) {
                dd.device.notification.toast({
                    icon: '', //icon样式，有success和error，默认为空 0.0.2
                    text: info, //提示信息
                    duration: 2, //显示持续时间，单位秒，默认按系统规范[android只有两种(<=2s >2s)]
                    delay: 0, //延迟显示，单位秒，默认0
                    onSuccess: function (result) {
                        goBack();
                    },
                    onFail: function (err) { }
                })

            }


            OpenLink = function (ob) {
                dd.biz.util.openLink({
                    url: "http://192.168.120.83:7647/KmrStorage/" + ob + "",//要打开链接的地址
                    //url: "http://122.112.213.22/KmrStorage/KmrStorage/" + ob + "",//要打开链接的地址 钉钉发布用此代码
                        onSuccess: function (result) {
                                /**/
                        },
                        onFail: function (err) { }
                })
            }

        });

        function accStat(info) {
            //获取app当前使用状态，财务是否关账
            $.post("../api/KmrStorage/GetAppStat.aspx",
                {},
                function (data, status) {
                    hidePreloader();
                    var stat = JSON.parse(data).stat;
                    if (stat == "0") {

                        var btnArray = ['确定'];
                        mui.confirm('财务关账期间，APP暂停使用！', '提醒', btnArray, function (e) {

                            if (e.index == 0) {
                                goBack();
                            }
                        })
                    }
                    else {
                        status(info);
                    }

                    //数据提交后页面处理
                    //$("#blist li").remove();
                    //$("span.mui-badge").text("0");
                    //mui('#middlePopover').popover('hide');
                    //var btn = document.getElementById("queryBtn");
                    //mui.trigger(btn, 'tap');

                    //location.reload();
                    //数据提交后页面处理
                });

        }



        function status(re) {
            //mui(".mui-collapse-content").classList.add("mui-hidden");
            //显示元素mui(".mui-collapse-content").classList.remove("mui-hidden"); 
            //alert(JSON.stringify(userinfo));
            //var name = JSON.stringify(userinfo).name;
            //sessionStorage.setItem("name", name);

            //re是人员信息，获取后进行后台认证
            //$.post("../api/KmrStorage/GetAuth.aspx",
            //    {phone:},
            //    function (data, status) {
            //        hidePreloader();
            //        var stat = JSON.parse(data).stat;
            //        if (stat == "0") {

            //            var btnArray = ['确定'];
            //            mui.confirm('财务关账期间，APP暂停使用！', '提醒', btnArray, function (e) {

            //                if (e.index == 0) {
            //                    goBack();
            //                }
            //            })
            //        }
            //        else {
            //            status(info);
            //        }
            //        //数据提交后页面处理
            //    });

            //re是人员信息，获取后进行后台认证

          
            var list = { "datalist": ["验收入库", "工厂验收", "仓库调拨", "公司内领料","位置","领料确认", "领料备餐", "配送发货", "库存查询", "地磅检斤", "跨公司领料","财务关账"] };
           
            $("ul li").each(function () {
                for (var i = 0; i < list.datalist.length; i++) {
                    if ($(this).find('div').html() == list.datalist[i]) {

                        this.classList.remove("mui-hidden");  
                    }
                }
            });

            
        }


        mui(".mui-table-view").on('tap', '.mui-table-view-cell', function () {
            //获取id
            var lid = this.getAttribute("id");
            var url = lid + "?phone=" + userinfo.phone + ""
            //传值给详情页面，通知加载新数据
            //mui.fire(detail, 'getDetail', { id: id });
            //打开新闻详情
            OpenLink(url);
        }) 

</script>

