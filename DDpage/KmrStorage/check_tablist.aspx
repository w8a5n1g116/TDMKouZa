<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="check_tablist.aspx.cs" Inherits="DDpage.KmrStorage.check_tablist" %>

<!DOCTYPE html>

<html>
<head>
    <meta charset="utf-8">
    <title>掌上仓储-工厂验收</title>
    <meta name="viewport" content="width=device-width, initial-scale=1,maximum-scale=1,user-scalable=no">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="apple-mobile-web-app-status-bar-style" content="black">
    <!--标准mui.css-->
    <link rel="stylesheet" href="../css/mui.min.css">
    <link rel="stylesheet" type="text/css" href="../css/app.css" />
	<link href="../css/mui.picker.css" rel="stylesheet" />
	<link href="../css/mui.poppicker.css" rel="stylesheet" />
    <link href="../css/iconfont.css" rel="stylesheet" />
    <!--App自定义的css-->
    <!--<link rel="stylesheet" type="text/css" href="../css/app.css" />-->
</head>
<body>
    <style>
        .mui-control-content {
            background-color: white;
            min-height: 215px;
        }

            .mui-control-content .mui-loading {
                margin-top: 50px;
            }
      
            .font_fix{
                font-size:14px; 

            }
            
        .mui-input-row {
            display:flex;
            flex-direction:row;
            align-items:center;
            justify-content:flex-start;
        }
            .mui-input-row label {
                padding:0;
                padding-left:0.5em;
            }
        .ui-alert {
				text-align: center;
				padding: 20px 10px;
				font-size: 14px;
			}
        .mui-btn{
				font-size:14px;
				
			}
        .font_fix2{

             /* placeholder颜色  */
10         color: #aab2bd;
11         /* placeholder字体大小  */
12         font-size: 14px;
13         /* placeholder位置  */
14         text-align: left;
        }

        a:link {
        color: #aab2bd;
        }
        a:visited {
                color: #aab2bd;
        }
        a:hover {
                color: #aab2bd;
        }
        a:active {
                color: #aab2bd;
        }

        .mui-slider-indicator.mui-segmented-control {
				background-color: #fff;
			}
        .mui-slider .mui-segmented-control.mui-segmented-control-inverted~.mui-slider-group .mui-slider-item{
				border:none;
			}

        .mui-table-view-cell{
				padding:0;
				margin-bottom:0.5em;
				 -moz-box-shadow:2px 3px 12px #ADADAD; -webkit-box-shadow:2px 3px 12px #ADADAD; box-shadow:2px 3px 12px #ADADAD;
			}

			.list_info{
				width:100%;
				display:flex;
    			flex-direction:row;
    			justify-content:space-between;
    			padding:0.5em 0;
    			font-size:1em;
                
                align-items:center;
                border-bottom:1px solid #EFEFEF; 
			}
			.list_text{
				margin-top:1em;
				padding:0 0.5em;
				display:flex;
    			flex-direction:column;
                font-size: 1em;
                color:#808080;    
			}
            .mui-popover {
				height: 90%;
                width:100%;
			}
            .icon_fix{
                color:red;
            }
    </style>
    <!--<header class="mui-bar mui-bar-nav">
        <a class="mui-action-back mui-icon mui-icon-left-nav mui-pull-left"></a>
        <h1 class="mui-title">顶部选项卡-可左右拖动(div)</h1>
    </header>-->
    <div class="mui-content">
        <div class="mui-content-padded" style="margin: 5px;">
        <form class="mui-input-group font_fix">
					<div class="mui-input-row">
						<label >领料公司</label>
                        <div id="PCompany" class="div_fix"></div>
					</div>
                     <div class="mui-input-row">
						<label>领料部门</label>
						<div id="PDept" class="div_fix"></div>
					</div>
                    <div class="mui-input-row">
						<label>领料经营体</label>
						<div id="PVenture" class="div_fix"></div>
					</div>
					<div class="mui-input-row">
						<label>领料单号</label>
						<input type="text" class="mui-input-clear font_fix" placeholder="请输入订单号" id="OddNo">
					</div>
                    <div class="mui-input-row">
						<label>发料仓库</label>
                        <div id="PStock">
                            <span id="PickStock" class="font_fix2">请选择...</span>
                        </div>
					</div>
                    <div class="mui-input-row">
						<label>物料名称</label>
						<input id="PMName" type="text" class="mui-input-clear font_fix" placeholder="请输入物料名称">
					</div>
            <div class="mui-button-row" id="query">
						<button type="button" class="mui-btn mui-btn-danger" id="queryBtn">查询</button>
					</div>
        </form>
            </div>

         

        <div id="slider" class="mui-slider" >
            <div id="sliderSegmentedControl" class="mui-slider-indicator mui-segmented-control mui-segmented-control-inverted mui-segmented-control-negative">
                <a class="mui-control-item" href="#item1mobile">
                    待验收
                </a>
                <a class="mui-control-item" href="#item2mobile">
                    验收查询
                </a>
                <%--<a class="mui-control-item" href="#item3mobile">
                    后续添加
                </a>--%>
            </div>
            <div id="sliderProgressBar" class="mui-slider-progress-bar mui-col-xs-6"></div>
            
            <div class="mui-slider-group">
                 
                <div id="item1mobile" class="mui-slider-item mui-control-content mui-active" style="height:35em">    
                   
                    <div id="scroll1" class="mui-scroll-wrapper">
                        <div class="mui-scroll">                     
                            <ul class="mui-table-view mui-table-view-striped mui-table-view-condensed" id="list1" >
                                
                            </ul>
                        </div>
                    </div>
                </div>
                <div id="item2mobile" class="mui-slider-item mui-control-content"  style="height:35em">
                    <div id="scroll2" class="mui-scroll-wrapper">
                        <div class="mui-scroll">
                            <ul class="mui-table-view mui-table-view-striped mui-table-view-condensed" id="list2" >

                            </ul>
                        </div>
                    </div>
                </div>
                <%--<div id="item3mobile" class="mui-slider-item mui-control-content"  style="height:25em">
                    <div id="scroll3" class="mui-scroll-wrapper">
                        <div class="mui-scroll">
                            <ul class="mui-table-view mui-table-view-striped mui-table-view-condensed" id="list3">

                            </ul>                          
                        </div>
                    </div>
                </div>--%>
            </div>
        </div>


        <%--<div id="middlePopover" class="mui-popover font_fix" style="margin-left:0px;" >
            
			<div class="mui-popover-arrow list_text"  ></div>
			<div class="mui-scroll-wrapper" style="margin:0;height:90%;">
				<div class="mui-scroll" >
					<ul class="mui-table-view deshow" id="checkList" >
						
					</ul>
                    
				</div>
               
			</div>
             <div class="mui-button-row pick-submit-clear" style="height:10%;position:absolute;bottom:0;text-align:center;width:100%;">
						<button type="button" class="mui-btn mui-btn-primary" id="submit">确认</button>
                   &nbsp;&nbsp;
                        <button type="button" class="mui-btn mui-btn-primary" id="clear">删除</button>
				</div>
		</div>--%>
        <div id="reportPopover" class="mui-popover font_fix" style="margin-left:0px;" >
            
			<div class="mui-popover-arrow list_text"  ></div>
			<div class="mui-scroll-wrapper" style="margin:0;height:90%;">
				<div class="mui-scroll" >
					<div class="list_text">
                    <div class="list_info"><div >物料名称：<span  class="lims_wlmc"></span></div></div>
                    <div class="list_info"><div >物料代码：<span class="lims_wldm"></span></div></div>
                    <div class="list_info"><div >供应商：<span class="lims_supply"></span></div></div>
                    </div>
                    <ul class="mui-table-view limsShow" id="LimsList" >
						
					</ul>   
				</div>
               
			</div>
            
		</div>



        <div id="queryPopover" class="mui-popover font_fix" style="margin-left:0px;" >
            
			<div class="mui-popover-arrow list_text"  ></div>
			<div class="mui-scroll-wrapper" style="margin:0;height:90%;">
				<div class="mui-scroll" >
					<ul class="mui-table-view" id="queryList" >
						
					</ul>
                    
				</div>
               
			</div>
            
		</div>

    </div>

    <nav class="mui-bar mui-bar-tab " id="nav-submit">
        <a class="mui-tab-item mui-active checkSubmit" id="submit">
            <span class="mui-icon iconfont icon-jiaogongyanshou"><span class="mui-badge" >0</span></span>
            <span class="mui-tab-label">提交</span>
        </a>
    </nav>

<script src="../js/jquery-1.11.0.min.js"></script>
    <%--<script src="../js/dingtalk.js"></script>--%>
    <script src="//g.alicdn.com/dingding/dingtalk-jsapi/2.6.41/dingtalk.open.js"></script>
    <%--<script src="../js/ddjs.js"></script>--%>
    <script src="../js/mui.min.js"></script>
    <script src="../js/mui.picker.js"></script>
	<script src="../js/mui.poppicker.js"></script>
    <script>
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
        var preImages;
        var upImgFromCameras;

        dd.config({
            agentId: _config.agentId,
            corpId: _config.corpId,
            timeStamp: _config.timeStamp,
            nonceStr: _config.nonce,
            signature: _config.signature,
            jsApiList: ['biz.user.get', 'device.geolocation.get', 'biz.map.locate', 'biz.util.uploadImageFromCamera', 'biz.util.uploadImage', 'dd.device.notification.showPreloader', 'dd.device.notification.hidePreloader','dd.device.notification.toast']
        });


        mui.init({
            swipeBack: false
        });

        var _phone = '<%=strPhone%>';
        var _company = "";
        var _dept = "";
        var _name = "";
        var _venture = "";
        var _unit = "";
        var _cardId = "";
        var strJson = "";
        var ajaxUrl = "";
        var toastInfo;
        var showSubWait;
        var showLimsWait;
        var hiddWait;
        var _matCode = "";
        var arrayStock = Array();
        var dataStr = Array();

        var JsonUserInfo = '<%=user_info%>';

        var strArray = new Array();
        strArray = JsonUserInfo.split(",")
        _cardId = strArray[0];
        _company = strArray[1];
        _dept = strArray[2];
        _name = strArray[3];
        _venture = strArray[4];
        _unit = strArray[5];
        _venSapId = strArray[6];
        _costCode = strArray[7];


        (function ($) {
            $('.mui-scroll-wrapper').scroll({
                indicators: true //是否显示滚动条
            });



            mui("#PCompany")[0].innerHTML = _company;
            mui("#PDept")[0].innerHTML = _dept;
            mui("#PVenture")[0].innerHTML = _venture;

        })(mui);


        (function ($) {
            $('.mui-scroll-wrapper').scroll({
                indicators: true //是否显示滚动条
            });
            //mui("#PCompany")[0].innerHTML = _company;
            //mui("#PDept")[0].innerHTML = _dept;
            // mui("#PVenture")[0].innerHTML = _venture;

        })(mui);

        //查询按钮loading处理,查询数据
        var strCompany = "";
        var strDept = "";
        var strVenture = "";
        var strOrder = "";
        var strStock = "";
        var strMatName = "";

        mui("#query").on('tap', '.mui-btn', function () {
            mui(this).button('loading');
            //查询物料信息
            

            strStock = mui("#PickStock")[0].innerText;
            if (strStock == "请选择...") {
                strStock = "";
            }

            strOrder = mui("#OddNo")[0].value;
            strMatName = mui("#PMName")[0].value;

            getFList(this);
            getSList(this);

            //后台获取查询物料信息

            mui(this).button('reset');
        });
        //查询按钮loading处理,查询数据

        //待配送查询函数
        function getFList(ob) {
            var fragment = document.createDocumentFragment();
            ajaxUrl = "../api/KmrStorage/GetChDetail.aspx";
            mui.ajax(ajaxUrl, {
                data: { chCompany: strCompany, chDept: strDept, chVen: strVenture, chStName: strStock, chPName: _name, chOrder: strOrder, chMatName: strMatName },
                async: false,
                crossDomain: true,
                type: 'post',//HTTP请求类型
                success: function (data) {
                    //服务器返回响应，根据响应结果，分析是否登录成功；
                    var js = JSON.parse(data).data;
                    var li;
                    var subNum = js.length;
                    var strHtml;

                    for (var i = 0; i < js.length; i++) {

                        strHtml = "";
                        strHtml += "<div class=\"list_text\">";
                        strHtml += "<div class=\"list_info\"><div>物料名称：<span class=\"wlmc" + js[i].PDID + "\">" + js[i].Material + "</span></div><div class=\"mui-switch mui-switch-danger  mui-active " + js[i].PDID + "\" name=\"" + js[i].PDID + "\" onclick=\"btn_switch(this)\"><div class=\"mui-switch-handle\"></div></div></div>";
                        strHtml += "<div class=\"list_info\"><div >代码：<span class=\"code" + js[i].PDID + "\">" + js[i].MCode + "</span><span class=\"linkid" + js[i].PDID + "\" style=\"display:none\">" + js[i].PLinkID + "</span><span class=\"checkid" + js[i].PDID + "\" style=\"display:none\">" + js[i].CheckID + "</span><span class=\"pdid" + js[i].PDID + "\" style=\"display:none\">" + js[i].PDID + "</span></div><div>坑号:<input type=\"text\" style=\"width: 3em; height: 1em; margin: 0; font - size:0.5em\" class=\"kh" + js[i].PDID + "\" value=\"0\"></div></div>";
                        if (js[i].Photo.replace(/(^s*)|(s*$)/g, "").length == 0){
                            strHtml += "<div class=\"list_info\"><div>批次号：<span class=\"no" + js[i].PDID + "\">" + js[i].MBatch + "</div><div><span class=\"mui-icon iconfont icon-peiyanshouhege  report\" name=\"" + js[i].MCode + "\"></span></div></div>";
                        }
                        else {
                            strHtml += "<div class=\"list_info\"><div>批次号：<span class=\"no" + js[i].PDID + "\">" + js[i].MBatch + "</div><div><span class=\"mui-icon mui-icon-camera photo\" name=\"" + js[i].Photo + "\"></span>&nbsp;&nbsp;&nbsp;&nbsp;<span class=\"mui-icon iconfont icon-peiyanshouhege  report\" name=\"" + js[i].MCode + "\"></span></div></div>";
                        }
                        strHtml += "<div class=\"list_info\"><div>库存地点：<span class=\"stock" + js[i].PDID + "\">" + js[i].MStock + "</div><div>单位：<span class=\"unit" + js[i].PDID + "\">" + js[i].MUnit + "</div></div>";
                        strHtml += "<div class=\"list_info\"><div>申请时间：<span class=\"ptime" + js[i].PDID + "\">" + js[i].PTime + "</div><div>申请人：<span class=\"pname" + js[i].PDID + "\">" + js[i].PName + "</span></div></div>";
                        strHtml += "<div class=\"list_info\"><div>申请数量：<span class=\"num" + js[i].PDID + "\">" + js[i].PickInventory + "</span></div><div>领料数量:<span class=\"num" + js[i].PDID + "\">" + js[i].StockInventory + "</span></div></div>";
                        strHtml += "</div>";
                        li = document.createElement('li');
                        li.id = js[i].PDID;
                        li.title = "";
                        li.className = 'mui-table-view-cell listOne picked';
                        li.innerHTML = strHtml;
                        fragment.appendChild(li);
                    };

                    li = document.createElement('li');
                    li.className = 'mui-table-view-cell';
                    li.innerHTML = "<div class=\"list_text\" style=\"text-align:center;margin-bottom:4em\">已经到底了！！！</div>";
                    fragment.appendChild(li);

                    $("#list1 li").remove();
                    $("#list1").append(fragment);

                    $("span.mui-badge").text(subNum);
                },
                error: function (xhr, type, errorThrown) {
                    //异常处理；
                    alert(type);
                    console.log(type);
                }
            });
        }


        //待配送查询函数

        //配送查询函数
        function getSList(ob) {
            var fragment = document.createDocumentFragment();
            ajaxUrl = "../api/KmrStorage/GetChQueryInfo.aspx";
            mui.ajax(ajaxUrl, {
                data: { chCompany: strCompany, chDept: strDept, chVen: strVenture, chStName: strStock, chName: _name },
                async: false,
                crossDomain: true,
                type: 'post',//HTTP请求类型
                success: function (data) {
                    //服务器返回响应，根据响应结果，分析是否登录成功；
                    var js = JSON.parse(data).data;
                    var li;

                    var strHtml;

                    for (var i = 0; i < js.length; i++) {

                        strHtml = "";
                        strHtml += "<div class=\"list_text\"><a  href=\"#queryPopover\" >";
                        strHtml += "<div class=\"list_info\"><div>申请单号：<span class=\"pid\">" + js[i].PickID + "</span></div><div>申请状态：<span class=\"pstat\">" + js[i].PStat + "</span></div></div>";
                        strHtml += "<div class=\"list_info\"><div>申请公司：<span class=\"pcompany\">" + js[i].PCompany + "</span></div><div>申请部门：<span class=\"pdept\">" + js[i].PDept + "</span></div></div>";
                        strHtml += "<div class=\"list_info\"><div>申请经营体：<span class=\"pven\">" + js[i].PVenture + "</span></div><div><span class=\"linkid\" style=\"display:none\">" + js[i].PLinkID + "</span></div><div>申请人：<span class=\"pname\">" + js[i].PName + "</span></div></div>";
                        strHtml += "<div class=\"list_info\"><div>申请时间：<span class=\"ptime\">" + js[i].PTime + "</span></div><div>物料种类:<span class=\"pnum\">" + js[i].PNum + "</span></div></div>";
                        strHtml += "</a>";
                        strHtml += "</div>";
                        li = document.createElement('li');
                        li.id = js[i].matID;
                        li.title = "";
                        li.className = 'mui-table-view-cell listTwo';
                        li.innerHTML = strHtml;
                        fragment.appendChild(li);
                    };

                    li = document.createElement('li');
                    li.className = 'mui-table-view-cell';
                    li.innerHTML = "<div class=\"list_text\" style=\"text-align:center;margin-bottom:4em\">已经到底了！！！</div>";
                    fragment.appendChild(li);

                    $("#list2 li").remove();
                    $("#list2").append(fragment);
                },
                error: function (xhr, type, errorThrown) {
                    //异常处理；
                    alert(type);
                    console.log(type);
                }
            });

        }
        //配送查询函数

        //list2 中li元素添加监听事件
        mui(".mui-table-view").on('tap', '.listTwo', function () {

            $(this).find("span").addClass("on");


            //alert($("span.pcompany.on").text());

            //获取当前li的linkid
            var strLinkID = $("span.linkid.on").text();

            //修改blist中的li
            var fragment = document.createDocumentFragment();
            ajaxUrl = "../api/KmrStorage/GetChQueryDetail.aspx";
            mui.ajax(ajaxUrl, {
                data: { LinkID: strLinkID },
                async: false,
                crossDomain: true,
                type: 'post',//HTTP请求类型
                success: function (data) {
                    //服务器返回响应，根据响应结果，分析是否登录成功；
                    var js = JSON.parse(data).data;
                    var li;

                    var strHtml;

                    for (var i = 0; i < js.length; i++) {

                        strHtml = "";
                        strHtml += "<div class=\"list_text\">";
                        strHtml += "<div class=\"list_info\"><div>物料名称：<span>" + js[i].Material + "</span></div></div>";
                        strHtml += "<div class=\"list_info\"><div >代码：<span>" + js[i].MCode + "</span></div><div>物料状态：<span>" + js[i].PDStat + "</span></div></div>";
                        strHtml += "<div class=\"list_info\"><div>发料库房：<span>" + js[i].MStock + "</span></div><div>单位：<span>" + js[i].MUnit + "</span></div></div>";
                        strHtml += "<div class=\"list_info\"><div>申请时间：<span>" + js[i].PTime + "</div><div>申请人：<span>" + js[i].PName + "</div></div>";
                        strHtml += "<div class=\"list_info\"><div>申请数量：<span>" + js[i].PickInventory + "</div><div>备料数量:<span>" + js[i].StockInventory + "</div></div>";
                        if (js[i].pickInfo.length>0) {
                            strHtml += "<div class=\"list_info\"><div>SAP：<span class=\"ifsap\">" + js[i].ifSap + "</span></div><div>SAP类型：<span class=\"saptype\">" + js[i].pickItemType +  "</span></div></div>";
                            strHtml += "<div class=\"list_info\"><div>SAP信息：<span class=\"sappickinfo\">" + js[i].pickInfo + "</span></div></div>";
                        }
                        
                        strHtml += "</div>";
                        li = document.createElement('li');
                        li.id = js[i].stPDID;
                        //li.title = "sec";
                        li.className = 'mui-table-view-cell ';
                        li.innerHTML = strHtml;
                        fragment.appendChild(li);
                    };


                    $("#queryList li").remove();
                    $("#queryList").append(fragment);

                },
                error: function (xhr, type, errorThrown) {
                    //异常处理；
                    alert(type);
                    console.log(type);
                }
            });

            $(this).find("span").removeClass("on");

        })
            //list2 中li元素添加监听事件


        //lims检验报告函数

        mui("#list1").on('tap', '.report', function () {
            //显示lims报告提取
            _matCode = $(this).attr("name");
            //alert(_matCode7);
            showLimsWait();

            //显示lims报告提取
            //获取物料代码


            
            
        })


        function showLimsDetail() {
            var matCode = _matCode;
            //获取物料代码
            //依据物料代码获取lims检验报告
            var fragment = document.createDocumentFragment();
            ajaxUrl = "../api/KmrStorage/GetlimsInfo.aspx";
            mui.ajax(ajaxUrl, {
                data: { mCode: matCode },
                async: false,
                crossDomain: true,
                type: 'post',//HTTP请求类型
                success: function (data) {
                    //服务器返回响应，根据响应结果，分析是否登录成功；
                    var js = JSON.parse(data).data;
                    if (js.length > 0) {

                        var li;
                        //lims报告表头设置

                        $("span.lims_wlmc").text(js[0].maktx);
                        $("span.lims_wldm").text(js[0].matnr);
                        $("span.lims_supply").text(js[0].supplier);
                        $("span.lims_charge").text(js[0].limsCharge);
                        

                        //lims报告表头设置
                        var strHtml;

                        for (var i = 0; i < js.length; i++) {

                            strHtml = "";
                            strHtml += "<div class=\"list_text\">";
                            strHtml += "<div class=\"list_info\"><div>检查项：<span>" + js[i].checktiem + "</span></div></div>";
                            strHtml += "<div class=\"list_info\"><div>标准值：<span>" + js[i].standardvalue + "</span></div><div>实测值：<span>" + js[i].realvalue + "</span></div></div>";
                            strHtml += "<div class=\"list_info\"><div>检验批号：<span>" + js[i].limsCharge + "</span></div><div>批号:<span >" + js[i].charge + "</span></div></div>";
                            strHtml += "<div class=\"list_info\"><div>检测时间：<span>" + js[i].edate + "</span></div><div>结果:<span >" + js[i].single + "</span></div></div>";
                            strHtml += "</div>";
                            li = document.createElement('li');
                            li.id = js[i].matID;
                            li.title = "";
                            li.className = 'mui-table-view-cell listTwo';
                            li.innerHTML = strHtml;
                            fragment.appendChild(li);
                        };
                        $("#LimsList li").remove();
                        $("#LimsList").append(fragment);
                        hiddWait();
                        mui("#reportPopover").popover('toggle', document.getElementById("list1"));
                    }
                    else {
                        hiddWait();
                        alert("无LIMS检测数据！");
                    }
                    

                },
                error: function (xhr, type, errorThrown) {
                    //异常处理；
                    hiddWait();
                    alert(type);
                    console.log(type);
                }
            });
            //依据物料代码获取lims检验报告
            //显示lims该物料代码下最新的lims检验报告
        }

        //lims检验报告函数
       

        //确认按钮提交

        mui("#nav-submit").on("tap", ".checkSubmit", function () {
            if ($("li.picked").length > 0) {
                if (confirm("确认提交？")) {
                    showSubWait();

                }
            }
            else{

                alert("无验收信息，请返回继续修改！")
            }
            });

        function postData() {
            var strJson = "";
            //strJson += "{\"PCompany\":\"" + _company + "\",\"PDept\":\"" + _dept + "\",\"PVenture\":\"" + _venture + "\",\"PName\":\"" + _name + "\",\"PCode\":\"" + _cardId + "\",";
            //strJson += "\"PTime\":\"" + getNowFormatDate() + "\",\"OddNo\":\"" + orderNum + "\",\"PickStat\":\"待备餐\",\"data\":[";
            strJson += "[";
            var tmp = "";
            $("li.picked").each(function () {
                //alert($(this).html());
                //alert($(this).attr("id"));
                tmp = $(this).attr("id");
                $(this).find("span").addClass("on");

                strJson += "{\"linkid\":\"" + $("span.linkid" + tmp + ".on").text() + "\",";
                strJson += "\"pdid\":\"" + $("span.pdid" + tmp + ".on").text() + "\",";
                strJson += "\"checkid\":\"" + $("span.checkid" + tmp + ".on").text() + "\",";
                strJson += "\"deid\":\"" + $("span.deid" + tmp + ".on").text() + "\",";
                strJson += "\"kh\":\"" + $("span.kh" + tmp + ".on").text() + "\",";
                strJson += "\"cstat\":\"1\"},";
            })
            strJson = strJson

            strJson = strJson.substr(0, strJson.length - 1);
            strJson += "]";
            //strJson += "]}";
            //ajax后台数据处理
            //alert(strJson);
            //console.log(strJson);

            $.post("../api/KmrStorage/checkListUpdate_sumbit.aspx",
                { PCompany: _company, PDept: _dept, PVenture: _venture, PName: _name, PCode: _cardId, sapCode: _venSapId, costCode: _costCode, PTime: getNowFormatDate(), dataList: strJson },
                function (data, status) {

                    hiddWait();
                    toastInfo(data);
                    mui.toast(data, { duration: 'long', type: 'div' })
                    //location.reload();
                    //数据提交后页面处理
                });
        }



        //按钮动态变化
        function btn_switch(obj) {
            var nameid = $(obj).attr("name");
            if ($(obj).hasClass('mui-active')) {
                $(obj).removeClass("mui-active");
                $("li#" + nameid + "").removeClass("picked");
                var num = $("span.mui-badge").text()
                num--;
                $("span.mui-badge").text(num);
            }
            else {
                $(obj).addClass("mui-active");
                $("li#" + nameid + "").addClass("picked");
                var num = $("span.mui-badge").text();
                num++;
                $("span.mui-badge").text(num);

            }

        }

        //发料仓库下拉列表
        (function ($, doc) {
            $.init();
            $.ready(function () {
                var _getParam = function (obj, param) {
                    return obj[param] || '';
                };

                ajaxUrl = "../api/KmrStorage/GetInventoryStock.aspx";
                mui.ajax(ajaxUrl, {
                    data: {},
                    async: false,
                    crossDomain: true,
                    //dataType: 'json',//服务器返回json格式数据
                    type: 'post',//HTTP请求类型
                    //timeout: 10000,//超时时间设置为10秒；
                    // headers: { 'Content-Type': 'text/json' },
                    success: function (data) {
                        //服务器返回响应，根据响应结果，分析是否登录成功；
                        var js = JSON.parse(data).data;

                        for (var i = 0; i < js.length; i++) {
                            arrayStock.push({ value: js[i], text: js[i] });
                        }

                    },
                    error: function (xhr, type, errorThrown) {
                        //异常处理；
                        alert(errorThrown);
                        console.log(type);
                    }
                });

                var dataPicekerStock = new $.PopPicker();
                dataPicekerStock.setData(arrayStock);

                var showStockButton = doc.getElementById('PickStock');
                var stockResult = doc.getElementById('PStock');

                showStockButton.addEventListener('tap', function (event) {
                    dataPicekerStock.show(function (items) {
                        showStockButton.innerText = items[0].text;
                    });
                }, false);
            });

        })(mui, document);
        //发料仓库下拉列表

        //时间函数
        function getNowFormatDate() {
            var date = new Date();
            var seperator1 = "-";
            var seperator2 = ":";
            var year = date.getFullYear();
            var month = date.getMonth() + 1;
            var strDate = date.getDate();
            if (month >= 1 && month <= 9) {
                month = "0" + month;
            }
            if (strDate >= 0 && strDate <= 9) {
                strDate = "0" + strDate;
            }
            var currentdate = year + seperator1 + month + seperator1 + strDate
                + " " + date.getHours() + seperator2 + date.getMinutes()
                + seperator2 + date.getSeconds();
            return currentdate;
        }

        //时间函数
        //前台json字符串处理函数
        function strtojs(str) {
            var jst = "";
            for (var i = 0; i < str.length; i++) {
                var c = str[i];
                switch (c) {
                    case '\"':
                        jst += "\\\"";
                        break;
                    case '\\':
                        jst += "\\\\";
                        break;
                    case '/':
                        jst += "\\/";
                        break;
                    case '\b':
                        jst += "\\b";
                        break;
                    case '\f':
                        jst += "\\f";
                        break;
                    case '\n':
                        jst += "\\n";
                        break;
                    case '\r':
                        jst += "\\r";
                        break;
                    case '\t':
                        jst += "\\t";
                        break;
                    default: {
                        jst += c;
                        break;
                    }

                }
            }
            return jst;
        }



        //前台json字符串处理函数

        //初始化查询
        mui.ready(function () {
            //mui.trigger(btn, 'tap');
            var btn = document.getElementById("queryBtn");
            mui.trigger(btn, 'tap');
        });



        //dd函数
        dd.ready(function () {

            preImages = function (url) {
                dd.biz.util.previewImage({
                    urls: [url],//图片地址列表
                    current: url,//当前显示的图片链接
                    onSuccess: function (result) {
                        /**/
                    },
                    onFail: function (err) { }
                })
            }

            showLimsWait = function (callback) {
                dd.device.notification.showPreloader({
                    text: "Lims报告提取中..", //loading显示的字符，空表示不显示文字
                    showIcon: true, //是否显示icon，默认true
                    onSuccess: function (result) {
                        showLimsDetail();
                    },
                    onFail: function (err) { }
                })
            }



            showSubWait = function (callback) {
                dd.device.notification.showPreloader({
                    text: "数据提交中..", //loading显示的字符，空表示不显示文字
                    showIcon: true, //是否显示icon，默认true
                    onSuccess: function (result) {
                        postData();
                    },
                    onFail: function (err) { }
                })
            }

            hiddWait = function (callback) {
                dd.device.notification.hidePreloader({
                    onSuccess: function (result) {
                        /*{}*/
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
                        location.reload();
                    },
                    onFail: function (err) { }
                })

            }


        })


        //dd函数
        //图片预览
        mui("#list1").on('tap', '.photo', function () {
            var strUrl = $(this).attr("name");
            preImages(strUrl);
        })

        //图片预览

</script>


    </body>
</html>