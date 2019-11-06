<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="DDpage.KmrStorage.test_index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1,maximum-scale=1,user-scalable=no" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black" />
    <link href="../css/iconfont.css" rel="stylesheet" />
    <link href="../css/kmStorageExtra/iconfont.css" rel="stylesheet" />
    <link rel="stylesheet" type="text/css" href="../css/app.css"/>
    <!--标准mui.css-->
    <link rel="stylesheet" href="../css/mui.min.css" />
    <style>
			
			.title{
				margin: 20px 15px 10px;
				color: #6d6d72;
				font-size: 15px;
			}
			
			.oa-contact-cell.mui-table .mui-table-cell {
				padding: 11px 0;
				vertical-align: middle;
			}
			
			.oa-contact-cell {
				position: relative;
				margin: -11px 0;
			}
	
			.oa-contact-avatar {
				width: 75px;
			}
			.oa-contact-avatar img {
				border-radius: 50%;
			}
			.oa-contact-content {
				width: 100%;
			}
			.oa-contact-name {
				margin-right: 20px;
			}
			.oa-contact-name, oa-contact-position {
				float: left;
			}

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
   
       .mui-bar-tab .mui-tab-item.mui-active {
    color: #00bcd4;
}

       .mui-table-view.mui-grid-view .mui-table-view-cell .mui-media-body {
    font-size: 13px;
}

		</style>
	</head>

	<body>
		
		<nav class="mui-bar mui-bar-tab">
			<a class="mui-tab-item mui-active" href="#tabbar">
				<span class="mui-icon mui-icon-home "></span>
				<span class="mui-tab-label">领料扣杂</span>
			</a>
			<%--<a class="mui-tab-item" href="#tabbar-with-chat">
				<span class="mui-icon iconfont icon-wuliaolingyong "></span>
				<span class="mui-tab-label">跨公司</span>
			</a>--%>
			<%--<a class="mui-tab-item" href="#tabbar-with-contact">
				<span class="mui-icon mui-icon-contact"></span>
				<span class="mui-tab-label">领料扣杂</span>
			</a>--%>
		</nav>
		<div class="mui-content">
			<div id="tabbar" class="mui-control-content mui-active">
				<ul class="mui-table-view mui-grid-view mui-grid-9">  
                    
                    <li class="mui-table-view-cell mui-media mui-col-xs-4 mui-col-sm-3" id="checkIn_tablist1900.aspx">
                        <a href="javascript:void(0)">
                            <span class="mui-icon iconfont icon-rukuyanshou fix_icon"></span>
                            <div class="mui-media-body fix_font">扣杂验收</div>
                        </a>
                    </li>    
                    <li class="mui-table-view-cell mui-media mui-col-xs-4 mui-col-sm-3" id="checkIn_tablist1900_submit.aspx">
                        <a href="javascript:void(0)">
                            <span class="mui-icon iconfont icon-rukuyanshou fix_icon"></span>
                            <div class="mui-media-body fix_font">验收入库</div>
                        </a>
                    </li>
                    <%--<li class="mui-table-view-cell mui-media mui-col-xs-4 mui-col-sm-3 mui-hidden" id="pick_tablist.aspx">
                        <a href="javascript:void(0)">
                            <span class="mui-icon iconfont icon-wuliaolingyong fix_icon"></span>
                            <div class="mui-media-body">公司内领料</div>
                        </a>
                    </li>
                    <li class="mui-table-view-cell mui-media mui-col-xs-4 mui-col-sm-3 " id="pickInCom_InPlanlist.aspx">
                        <a href="javascript:void(0)">
                            <span class="mui-icon iconfont icon-wuliaolingyong fix_icon"></span>
                            <div class="mui-media-body">计划内领料</div>
                        </a>
                    </li>
                    <li class="mui-table-view-cell mui-media mui-col-xs-4 mui-col-sm-3 " id="pickInCom_OutPlanlist.aspx">
                        <a href="javascript:void(0)">
                            <span class="mui-icon iconfont icon-wuliaolingyong fix_icon"></span>
                            <div class="mui-media-body">计划外领料</div>
                        </a>
                    </li>
                    <li class="mui-table-view-cell mui-media mui-col-xs-4 mui-col-sm-3 " id="pickInCom_CostCenterlist.aspx">
                        <a href="javascript:void(0)">
                            <span class="mui-icon iconfont icon-wuliaolingyong fix_icon"></span>
                            <div class="mui-media-body">成本中心领料</div>
                        </a>
                    </li>
                    <li class="mui-table-view-cell mui-media mui-col-xs-4 mui-col-sm-3 " id="pickInCom_Researchlist.aspx">
                        <a href="javascript:void(0)">
                            <span class="mui-icon iconfont icon-wuliaolingyong fix_icon"></span>
                            <div class="mui-media-body">研发类领料</div>
                        </a>
                    </li>
                    <li class="mui-table-view-cell mui-media mui-col-xs-4 mui-col-sm-3 mui-hidden" id="project_Picklist.aspx">
                        <a href="javascript:void(0)">
                            <span class="mui-icon iconfont icon-wuliaolingyong fix_icon"></span>
                            <div class="mui-media-body">工程类领料</div>
                        </a>
                    </li>


                     <li class="mui-table-view-cell mui-media mui-col-xs-4 mui-col-sm-3 mui-hidden" id="query_stockList.aspx">
                        <a href="javascript:void(0)">
                            <span class="mui-icon iconfont icon-kucunchaxun fix_icon"></span>
                            <div class="mui-media-body">库存查询</div>
                        </a>
                    </li>
                    <li class="mui-table-view-cell mui-media mui-col-xs-4 mui-col-sm-3 mui-hidden" id="allot_stockList.aspx">
                        <a href="javascript:void(0)">
                            <span class="mui-icon iconfont icon-tiaobodan fix_icon"></span>
                            <div class="mui-media-body">仓库调拨</div>
                        </a>
                    </li>--%>
                     
                </ul>
			</div>
			<%--<div id="tabbar-with-chat" class="mui-control-content">
				<ul class="mui-table-view mui-grid-view mui-grid-9">
				     <li class="mui-table-view-cell mui-media mui-col-xs-4 mui-col-sm-3 mui-hidden" id="pick_saplist.aspx">
                        <a href="javascript:void(0)">
                            <span class="mui-icon iconfont icon-wuliaolingyong fix_icon"></span>
                            <div class="mui-media-body">跨公司领料</div>
                        </a>
                    </li>
                    <li class="mui-table-view-cell mui-media mui-col-xs-4 mui-col-sm-3 mui-hidden" id="pickOutCom_InPlanlist.aspx">
                        <a href="javascript:void(0)">
                            <span class="mui-icon iconfont icon-wuliaolingyong fix_icon"></span>
                            <div class="mui-media-body">计划内领料</div>
                        </a>
                    </li>
                    <li class="mui-table-view-cell mui-media mui-col-xs-4 mui-col-sm-3 mui-hidden" id="pickOutCom_OutPlanlist.aspx">
                        <a href="javascript:void(0)">
                            <span class="mui-icon iconfont icon-wuliaolingyong fix_icon"></span>
                            <div class="mui-media-body">计划外领料</div>
                        </a>
                    </li>
                    <li class="mui-table-view-cell mui-media mui-col-xs-4 mui-col-sm-3 mui-hidden" id="pickOutCom_CostCenterlist.aspx">
                        <a href="javascript:void(0)">
                            <span class="mui-icon iconfont icon-wuliaolingyong fix_icon"></span>
                            <div class="mui-media-body">成本中心领料</div>
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
				</ul>
			</div>--%>
			<%--<div id="tabbar-with-contact" class="mui-control-content">
                <ul class="mui-table-view mui-grid-view mui-grid-9">
				    <li class="mui-table-view-cell mui-media mui-col-xs-4 mui-col-sm-3 mui-hidden" id="checkIn_tablist1900.aspx">
                        <a href="javascript:void(0)">
                            <span class="mui-icon iconfont icon-rukuyanshou fix_icon"></span>
                            <div class="mui-media-body fix_font">南车扣杂验收入库</div>
                        </a>
                    </li>    
                    <li class="mui-table-view-cell mui-media mui-col-xs-4 mui-col-sm-3 mui-hidden" id="checkIn_tablist.aspx">
                        <a href="javascript:void(0)">
                            <span class="mui-icon iconfont icon-rukuyanshou fix_icon"></span>
                            <div class="mui-media-body fix_font">验收入库</div>
                        </a>
                    </li>
                                     
                    <li class="mui-table-view-cell mui-media mui-col-xs-4 mui-col-sm-3 mui-hidden" id="allot_stockList.aspx">
                        <a href="javascript:void(0)">
                            <span class="mui-icon iconfont icon-tiaobodan fix_icon"></span>
                            <div class="mui-media-body">仓库调拨</div>
                        </a>
                    </li>
                    <li class="mui-table-view-cell mui-media mui-col-xs-4 mui-col-sm-3 mui-hidden" id="query_stockList.aspx">
                        <a href="javascript:void(0)">
                            <span class="mui-icon iconfont icon-kucunchaxun fix_icon"></span>
                            <div class="mui-media-body">库存查询</div>
                        </a>
                    </li>
                    <li class="mui-table-view-cell mui-media mui-col-xs-4 mui-col-sm-3 mui-hidden" id="query_declarePlanList.aspx">
                        <a href="javascript:void(0)">
                            <span class="mui-icon iconfont icon-kucunchaxun fix_icon"></span>
                            <div class="mui-media-body">申报计划查询</div>
                        </a>
                    </li>
                    <li class="mui-table-view-cell mui-media mui-col-xs-4 mui-col-sm-3 mui-hidden" id="working.aspx">
                        <a href="javascript:void(0)">
                            <span class="mui-icon iconfont icon-dibang2 fix_icon"></span>
                            <div class="mui-media-body">地磅检斤</div>
                        </a>
                    </li>
                    <li class="mui-table-view-cell mui-media mui-col-xs-4 mui-col-sm-3 " id="confirm_tablist.aspx">
                        <a href="javascript:void(0)">
                            <span class="mui-icon iconfont icon-bumenlingliaochuku fix_icon"></span>
                            <div class="mui-media-body">领料确认</div>
                        </a>
                    </li>
                    <li class="mui-table-view-cell mui-media mui-col-xs-4 mui-col-sm-3 " id="costCenter_batchPicklist.aspx" title="成本中心批量领料">
                        <a href="javascript:void(0)">
                            <span class="mui-icon iconfont icon-piliang fix_icon"></span>
                            <div class="mui-media-body">批量-成本中心</div>
                        </a>
                    </li>
                     <li class="mui-table-view-cell mui-media mui-col-xs-4 mui-col-sm-3 " id="outPlan_batchPicklist.aspx" title="计划外批量领料">
                        <a href="javascript:void(0)">
                            <span class="mui-icon iconfont icon-piliang fix_icon"></span>
                            <div class="mui-media-body">批量-计划外</div>
                        </a>
                    </li>
				</ul>

			</div>--%>
			
		</div>
	</body>
</html>

<script src="../js/jquery-1.11.0.min.js"></script>
<%--<script src="../js/dingtalk.js"></script>--%>
<script src="//g.alicdn.com/dingding/dingtalk-jsapi/2.6.41/dingtalk.open.js"></script>
<%--<script src="../js/ddjs.js"></script>--%>
<script src="../js/mobileSelect.js"></script>
<script src="../js/api.js"></script>
<script src="../js/aui-toast.js"></script>
<script src="../js/mui.min.js"></script>
<script type="text/javascript">
    var _config = {
        agentId: '<%=appId%>',
        corpId: '<%=corpId%>',
        timeStamp: '<%=timestamp%>',
        nonce: '<%=nonceStr%>',
        signature: '<%=signature%>',
        title: '<%=title%>'
    };
    var userinfo = "";
    var OpenLink;
    var toastInfo;

    dd.config({
        agentId: _config.agentId,
        corpId: _config.corpId,
        timeStamp: _config.timeStamp,
        nonceStr: _config.nonce,
        signature: _config.signature,
        jsApiList: ['biz.user.get', 'biz.util.scan', 'device.geolocation.get', 'biz.map.locate', 'biz.util.uploadImageFromCamera', 'biz.util.uploadImage']
    });

    dd.error(function (error) {
        alert('dd error: ' + JSON.stringify(error));
    });

    mui.init({
        swipeBack: true //启用右滑关闭功能
    });

    var slider = mui("#slider");
    slider.slider({
        interval: 5000
    });




    dd.ready(function () {

        dd.runtime.permission.requestAuthCode({
            corpId: _config.corpId, // 企业id
            onSuccess: function (info) {
                code = info.code // 通过该免登授权码可以获取用户身份
            }
        });

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


                    }
                })
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
                duration: 3, //显示持续时间，单位秒，默认按系统规范[android只有两种(<=2s >2s)]
                delay: 0, //延迟显示，单位秒，默认0
                onSuccess: function (result) {
                    //goBack();
                },
                onFail: function (err) { }
            })

        }


        OpenLink = function (ob) {
            dd.biz.util.openLink({
                url: "http://192.168.43.241:7647/KmrStorage/" + ob + "",//要打开链接的地址
                //url: "http://25347aq284.qicp.vip/KmrStorage/" + ob + "",//要打开链接的地址 钉钉发布用此代码
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
        var info = "欢迎进入掌上仓储！";
        var myDate = new Date();
        //if (myDate.getDate() >= 27) {
        //    info = "财务关账期间，SAP服务暂停使用！";
        //}
       

        toastInfo(info);

        var list = { "datalist": ["验收入库","南车扣杂验收入库", "工厂验收", "仓库调拨", "公司内领料", "位置", "领料确认", "领料备餐", "配送发货", "库存查询", "跨公司领料", "工程类领料", "研发类领料", "计划内领料", "计划外领料", "成本中心领料", "申报计划查询"] };

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

