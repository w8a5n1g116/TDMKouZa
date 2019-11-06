﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="pickInCom_InPlanlist.aspx.cs" Inherits="DDpage.KmrStorage.pickInCom_InPlanlist" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8">
    <title>计划内领料</title>
    <meta name="viewport" content="width=device-width, initial-scale=1,maximum-scale=1,user-scalable=no">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="apple-mobile-web-app-status-bar-style" content="black">
    <!--标准mui.css-->
    <link rel="stylesheet" href="../css/mui.min.css">
    <link rel="stylesheet" type="text/css" href="../css/app.css" />
	<link href="../css/mui.picker.css" rel="stylesheet" />
	<link href="../css/mui.poppicker.css" rel="stylesheet" />
    <link href="../css/iconfont.css" rel="stylesheet" />
    <link rel="stylesheet" type="text/css" href="../css/icons-extra.css" />
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

         .mui-bar-tab .mui-tab-item.mui-active {
        color: #dd524d;
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

            input, select, textarea {
            font-family: 'Helvetica Neue',Helvetica,sans-serif;
            font-size: 14px;
            -webkit-tap-highlight-color: transparent;
            -webkit-tap-highlight-color: transparent;
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
						<label >领料公司1071</label>
                        <div id="PCompany" class="div_fix"></div>
					</div>
                     <div class="mui-input-row">
						<label>领料部门</label>
						<div id="PDept" class="div_fix"></div>
					</div>
                    <div class="mui-input-row mui-hidden">
						<label>领料部门</label>
						<div id="ShowVenture" class="div_fix">
                            <span id="PVenture" class="font_fix2"></span>
						</div>
                        &nbsp;
                        <div id="PSapVenId" class="mui-hidden"></div>
                        &nbsp;
                        <div id="PCostId" ></div>
					</div>
                    <%--<div class="mui-input-row">
						<label>领料库房</label>
						<div id="showStock">
                            <span id="PStock" class="font_fix2">请选择</span>
                        </div>  
					</div>--%>
					<div class="mui-input-row">
						<label title="点击选择相关订单号">*订单号</label>
						<input type="text" class="mui-input-clear font_fix" placeholder="请输入订单号" id="OddNo" style="width:47%">
                        <%--<span class="mui-icon mui-icon-plus OddList" id="showOddNo"></span>--%>
                        &nbsp;&nbsp;
                        <span class="mui-icon-extra mui-icon-extra-sweep oddScanNo"></span>
					</div>
                    <div class="mui-input-row">
						<label>*工序号</label>
						<input type="text" class="mui-input-clear font_fix" placeholder="请输入工序号" id="proNo" >
					</div>
                    <%--<div class="mui-input-row">
						<label>物料名称</label>
						<input id="PMName" type="text" class="mui-input-clear font_fix" placeholder="请输入物料名称"  style="width:55%">
                        <span class="mui-icon-extra mui-icon-extra-sweep matScanName"></span>
					</div>--%>
            <div class="mui-button-row" id="query">
						<button type="button" class="mui-btn mui-btn-danger" id="queryBtn">查询</button>
					</div>
        </form>
            </div>

         

        <div id="slider" class="mui-slider" >
            <div id="sliderSegmentedControl" class="mui-slider-indicator mui-segmented-control mui-segmented-control-inverted mui-segmented-control-negative">
                <a class="mui-control-item" href="#item1mobile">
                    计划内
                </a>               
                <a class="mui-control-item" href="#item4mobile">
                    查询
                </a>
            </div>
            <div id="sliderProgressBar" class="mui-slider-progress-bar mui-col-xs-6"></div>
            
            <div class="mui-slider-group">
                 
                <div id="item1mobile" class="mui-slider-item mui-control-content mui-active" style="height:25em">    
                   
                    <div id="scroll1" class="mui-scroll-wrapper">
                        <div class="mui-scroll">                     
                            <ul class="mui-table-view mui-table-view-striped mui-table-view-condensed" id="list1" >
                                
                            </ul>
                        </div>
                    </div>
                </div>    
                <div id="item4mobile" class="mui-slider-item mui-control-content"  style="height:25em">
                    <div id="scroll4" class="mui-scroll-wrapper">
                        <div class="mui-scroll">
                            <ul class="mui-table-view mui-table-view-striped mui-table-view-condensed" id="list4">

                            </ul>
                          
                        </div>
                    </div>
                </div>
            </div>
        </div>


        <div id="middlePopover" class="mui-popover font_fix" style="margin-left:5px;" >
            
			<div class="mui-popover-arrow list_text"  ></div>
			<div class="mui-scroll-wrapper" style="margin:0;height:90%;">
				<div class="mui-scroll" >
					<ul class="mui-table-view" id="blist" >
						
					</ul>
                    
				</div>
               
			</div>
             <div class="mui-button-row pick-submit-clear" style="height:10%;position:absolute;bottom:0;text-align:center;width:100%;">
						<button type="button" class="mui-btn mui-btn-danger" id="submit">确认</button>
                   &nbsp;&nbsp;
                        <button type="button" class="mui-btn mui-btn-danger" id="clear">删除</button>
				</div>
		</div>

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

    

    <nav class="mui-bar mui-bar-tab">
        <a class="mui-tab-item " href="#middlePopover" id="middlePopoverhref">
            <span class="mui-icon iconfont icon-icon_jiarulingliaoche"><span class="mui-badge" >0</span></span>
            <span class="mui-tab-label">已选</span>
        </a>
    </nav>
    <script src="../js/jquery-1.11.0.min.js"></script>
    <script src="//g.alicdn.com/dingding/dingtalk-jsapi/2.6.41/dingtalk.open.js"></script>
    <%--<script src="../js/dingtalk.js"></script>--%>
    <script src="../js/mui.min.js"></script>
    <script src="../js/mui.picker.js"></script>
	<script src="../js/mui.poppicker.js"></script>
    <script>

        var _config = {
            appId: '<%=appId%>',
            corpId: '<%=corpId%>',
            timeStamp: '<%=timestamp%>',
            nonce: '<%=nonceStr%>',
            signature: '<%=signature%>',
            title: '<%=title%>'
        };

        dd.config({
            agentId: _config.appId,
            corpId: _config.corpId,
            timeStamp: _config.timeStamp,
            nonceStr: _config.nonce,
            signature: _config.signature,
            jsApiList: ['biz.util.ut']
        });

        dd.error(function (error) {
            alert('dd error: ' + JSON.stringify(error));
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
        var _venSapId = "";
        var _costCode = "";
        var _matCode = "";
        var strJson = "";
        var ajaxUrl = "";
        var dataStr = Array();
        var dataStockStr = Array();
        var dataVenture = Array();
        var dataStock = Array();
        var arrayPlan = Array();
        var queryWait;
        var showLimsWait;
        var showWait;
        var hiddWait;
        var toastInfo;
        var MatIdScan;

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
        _oneStat = 0;
        _twoStat = 0;
        _thStat = 0;
        _fiveStat = 0;
        _listOneStat = 0;
        _listTwoStat = 0;
        _listThStat = 0;
        _listFourStat = 0;
        _listFiveStat = 0;

        //if (_costCode.length==0) {
        //    _costCode = '1100F08001';
        //}
        //测试数据
        //_company = "直属事业部";
        //_dept = "熔炼工厂";
        //_venture = "熔炼重型电炉经营体";
        //测试数据


        var listhtml = "";

        (function ($) {
            $('.mui-scroll-wrapper').scroll({
                indicators: true //是否显示滚动条
            });



            mui("#PCompany")[0].innerHTML = _company;
            mui("#PDept")[0].innerHTML = _dept;
            mui("#PVenture")[0].innerHTML = _venture;
            mui("#PSapVenId")[0].innerHTML = _venSapId;
            mui("#PCostId")[0].innerHTML = _costCode;



        })(mui);

        //查询按钮loading处理,查询数据

        mui("#query").on('tap', '.mui-btn', function () {


            var strOddNo = mui("#OddNo")[0].value;
            //strRsNo = mui("#RsNo")[0].value;
            if (strOddNo.length == 0) {
                mui.toast("计划内领料查询，必须输入订单号！");
               
                return;
            }

            queryWait();

            //查询物料信息
            //_listOneStat = 0;
            //_listFourStat = 0;
            //mui(this).button('loading');
            //getFList(this);
            //getFourList(this);
            //后台获取查询物料信息

            //mui(this).button('reset');
        });

        function QueryInfo() {



            //查询物料信息
            _listOneStat = 0;
            _listFourStat = 0;

            getFList(this);
            getFourList(this);

            hiddWait();

        }

        //查询按钮loading处理,查询数据
        //查询经营体名下生产订单
        (function ($, doc) {
            $.init();
            $.ready(function () {
                var _getParam = function (obj, param) {
                    return obj[param] || '';
                };

                
            });


        })(mui, document);

        //点击更新订单号下拉菜单
        //mui(".mui-input-row").on('tap', '.OddList', function () {
        //    getOddNolist();
        //});
        //点击更新订单号下拉菜单


        //查询按钮还原
        function resetBtn() {
            if (_listOneStat == 1  && _listFourStat == 1 ) {
                mui("#queryBtn").button('reset');
            }

        }
        //查询按钮还原


        //点击扫码

        mui(".mui-input-row").on('tap', '.oddScanNo', function () {
            OddIdScan();
        });

        mui(".mui-input-row").on('tap', '.matScanName', function () {
            MatIdScan();
        });
        //点击扫码

        //查询经营体名下生产订单

        function getFList(ob) {
            //获取编号信息
            $("#list1 li").remove();
            

            var fragment = document.createDocumentFragment();
            ajaxUrl = "../api/KmrStorage/GetSapPlanList_BL.aspx";
            mui.ajax(ajaxUrl, {
                data: { OddNo: mui("#OddNo")[0].value, company: _company, RsNo: mui("#proNo")[0].value},
                async: true,
                crossDomain: true,
                type: 'post',//HTTP请求类型
                success: function (data) {
                    //服务器返回响应，根据响应结果，分析是否登录成功；
                    var js = JSON.parse(data).data;
                    var stat = JSON.parse(data).stat;
                    var li;

                    var strHtml;

                    if (js.length > 0) {

                        for (var i = 0; i < js.length; i++) {
                            //alert(js[i].matnr + "-" + js[i].kdauf + "-" + js[i].kdpos);
                            //
                            strHtml = "";
                            strHtml += "<div class=\"list_text\">";
                            strHtml += "<div class=\"list_info\"><div>物料代码：<span class=\"codeone" + js[i].matID + "\">" + js[i].matnr + "</span></div><div class=\"mui-switch mui-switch-danger  one" + js[i].matID + "\" name=\"one" + js[i].matID + "\" onclick=\"btn_switch(this)\"><div class=\"mui-switch-handle\"></div></div></div>";
                            strHtml += "<div class=\"list_info\"><div>物料名称：<span class=\"wlmcone" + js[i].matID + "\">" + js[i].rmaktx + "</span><span class=\"faccodeone" + js[i].matID + "\" style=\"display:none\">" + js[i].factory + "</span><span class=\"groupcodeone" + js[i].matID + "\" style=\"display:none\">" + js[i].mGroup + "</span><span class=\"gxcodeone" + js[i].matID + "\" style=\"display:none\">" + js[i].vornr + "</span><span class=\"rsNoone" + js[i].matID + "\" style=\"display:none\">" + js[i].RsNo + "</span><span class=\"rsPosone" + js[i].matID + "\" style=\"display:none\">" + js[i].RsPos + "</span></div></div>";
                            strHtml += "<div class=\"list_info\"><div>单位：<span class=\"unitone" + js[i].matID + "\">" + js[i].unit + "</div><div>领料类型：<span class=\"typeone" + js[i].matID + "\">计划内领料</div></div>";
                            strHtml += "<div class=\"list_info\"><div onclick=\"planPickList(this)\" name=\"" + js[i].matID + "\">序列号：<span class=\"planNameone" + js[i].matID + " planListName\"  id=\"planNameone" + js[i].matID + "\">请选择</span></div></div>";
                            strHtml += "<div class=\"list_info\"><div>工序号：<span class=\"vornrone" + js[i].matID + "\">" + js[i].vornr + "</div><div>库存：<span class=\"inventoryone" + js[i].matID + "\">" + js[i].inventory + "</div><span style=\"display:none;\" class=\"kdaufone" + js[i].matID + "\">" + js[i].kdauf + "</span><span style=\"display:none;\" class=\"kdposone" + js[i].matID + "\">" + js[i].kdpos + "</span></div>";
                            if (js[i].stock.length > 0) {
                                strHtml += "<div class=\"list_info\"><div>计划数量：<span class=\"numone" + js[i].matID + "\">" + js[i].menge + "</span><span class=\"stockcodeone" + js[i].matID + "\" style=\"display:none\">" + js[i].stock + "</span></div>";
                                strHtml += "<div>领料数量：<input type=\"text\" style=\"width: 5em; height: 1em; margin: 0; font - size:0.5em\" class=\"pnumone" + js[i].matID + "\" value=\"" + js[i].menge2 + "\"></div></div>";
                            }
                            else {
                                var stocklist = js.stockList;
                                strHtml += "<div class=\"list_info\"><div>计划数量：<span class=\"numone" + js[i].matID + "\">" + js[i].menge + "</span></div>";
                                strHtml += "<div>领料数量：<input type=\"text\" style=\"width: 5em; height: 1em; margin: 0; font - size:0.5em\" class=\"pnumone" + js[i].matID + "\" value=\"" + js[i].menge2 + "\"></div></div>";
                                strHtml += "<div class=\"list_info\"><div onclick=\"stockPickList(this)\" name=\"" + js[i].matID + "\">库存地点：<span class=\"stocknameone" + js[i].matID + " stockListName\"  id=\"stocknameone" + js[i].matID + "\"></span><span class=\"stockcodeone" + js[i].matID + " stockListCode\" style=\"display:none\" id=\"stockcodeone" + js[i].matID + "\"></span></div>";
                                strHtml += "</div>"
                            }

                            li = document.createElement('li');
                            li.id = "one" + js[i].matID;
                            li.title = "one";
                            li.className = 'mui-table-view-cell';
                            li.innerHTML = strHtml;
                            fragment.appendChild(li);

                        };

                        li = document.createElement('li');
                        li.className = 'mui-table-view-cell';
                        li.innerHTML = "<div class=\"list_text\" style=\"text-align:center;margin-bottom:4em\">已经到底了！！！</div>";
                        fragment.appendChild(li);
                    }
                    else {

                        li = document.createElement('li');
                        li.className = 'mui-table-view-cell';
                        li.innerHTML = "<div class=\"list_text\" style=\"text-align:center;margin-bottom:4em\">" + stat + "</div>";
                        fragment.appendChild(li);

                    }
                    $("#list1 li").remove();
                    $("#list1").append(fragment);

                    _listOneStat = 1;
                    resetBtn();

                },
                error: function (xhr, type, errorThrown) {
                    //异常处理；
                    alert(type);
                    console.log(type);
                }
            });

        }

        function stockPickList(ob) {
            var strid = $(ob).attr("name");
            var strMatCode = $("span.codeone" + strid + "").text()
            var strMatNum = $("span.numone" + strid + "").text()

            //获取库存地点数据

            ajaxUrl = "../api/KmrStorage/GetComPlanStock_BL.aspx";
            mui.ajax(ajaxUrl, {
                data: { strCom: _company, strMatCode: strMatCode, strMatNum: strMatNum },
                async: false,
                crossDomain: true,
                //dataType: 'json',//服务器返回json格式数据
                type: 'post',//HTTP请求类型
                //timeout: 10000,//超时时间设置为10秒；
                // headers: { 'Content-Type': 'text/json' },
                success: function (data) {
                    //服务器返回响应，根据响应结果，分析是否登录成功；
                    var js = JSON.parse(data).data;
                    dataStockStr.splice(0, dataStockStr.length);
                    for (var i = 0; i < js.length; i++) {
                        dataStockStr.push({ value: js[i].code, text: js[i].name });
                    }

                },
                error: function (xhr, type, errorThrown) {
                    //异常处理；
                    alert(errorThrown);
                    console.log(type);
                }
            });

            //获取库存地点数据

            //console.log(dataStockStr)

            var dataPicekerStock = new mui.PopPicker();
            dataPicekerStock.setData(dataStockStr);


            var ResultName = document.getElementById("stocknameone" + strid + "");
            var ResultCode = document.getElementById("stockcodeone" + strid + "");

            dataPicekerStock.show(function (items) {

                ResultName.innerText = items[0].text;
                ResultCode.innerText = items[0].value;
            }
            )

        }


        function planPickList(ob) {
            var strid = $(ob).attr("name");
            //alert(ob.className);
            var strMatCode = $("span.codeone" + strid + "").text()
            var strSaleNum = $("span.kdaufone" + strid + "").text()
            var strSaleNo = $("span.kdposone" + strid + "").text()

            ajaxUrl = "../api/KmrStorage/GetSeriarsNumber.aspx";
            mui.ajax(ajaxUrl, {
                data: { strSaleNum: strSaleNum, strSaleNo: strSaleNo, strMatCode: strMatCode },
                async: false,
                crossDomain: true,
                //dataType: 'json',//服务器返回json格式数据
                type: 'post',//HTTP请求类型
                //timeout: 10000,//超时时间设置为10秒；
                // headers: { 'Content-Type': 'text/json' },
                success: function (data) {
                    //服务器返回响应，根据响应结果，分析是否登录成功；
                    var js = JSON.parse(data).data;
                    arrayPlan.splice(0, arrayPlan.length);
                    for (var i = 0; i < js.length; i++) {
                        arrayPlan.push({ value: js[i].name, text: js[i].name });
                    }

                },
                error: function (xhr, type, errorThrown) {
                    //异常处理；
                    alert(errorThrown);
                    console.log(type);
                }
            });


            var dataPicekerPlan = new mui.PopPicker();
            dataPicekerPlan.setData(arrayPlan);

            var ResultName = document.getElementById("planNameone" + strid + "");

            dataPicekerPlan.show(function (items) {
                    ResultName.innerText = items[0].text;
                }
            )


        }


        //查询tab数据
        function getFourList() {
            var strMatName = '';
            var strdataNum = 0;
            if (strMatName.length > 0) {
                strdataNum = 10;
            }
            var fragment = document.createDocumentFragment();
            ajaxUrl = "../api/KmrStorage/GetComPickInfo_BL.aspx";
            mui.ajax(ajaxUrl, {
                data: { matName: strMatName, Company: _company, Dept: _dept, Ven: _venture, Name: _name, strPstat:'待确认'},
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
                        strHtml += "<div class=\"list_info\"><div>申请单号：<span class=\"pid\">" + js[i].PID + "</span><span class=\"linkid\" style=\"display:none\">" + js[i].PLID + "</span></div><div>申请状态：<span class=\"pstat\">" + js[i].PStat + "</span></div></div>";
                        strHtml += "<div class=\"list_info\"><div>申请公司：<span class=\"pcompany\">" + js[i].PCom + "</span></div><div>申请部门：<span class=\"pdept\">" + js[i].PDept + "</span></div></div>";
                        strHtml += "<div class=\"list_info\"><div>申请经营体：<span class=\"pven\">" + js[i].PVen + "</span></div><div>申请人：<span class=\"pname\">" + js[i].PName + "</span></div></div>";
                        strHtml += "<div class=\"list_info\"><div>申请时间：<span class=\"ptime\">" + js[i].PTime + "</span></div></div>";
                        strHtml += "</a>";
                        strHtml += "</div>";
                        li = document.createElement('li');
                        li.id = js[i].matID;
                        li.title = "";
                        li.className = 'mui-table-view-cell listFour';
                        li.innerHTML = strHtml;
                        fragment.appendChild(li);
                    };

                    li = document.createElement('li');
                    li.className = 'mui-table-view-cell';
                    li.innerHTML = "<div class=\"list_text\" style=\"text-align:center;margin-bottom:4em\">已经到底了！！！</div>";
                    fragment.appendChild(li);

                    $("#list4 li").remove();
                    $("#list4").append(fragment);

                    _listFourStat = 1;
                    resetBtn();

                },
                error: function (xhr, type, errorThrown) {
                    //异常处理；
                    alert(type);
                    console.log(type);
                }
            });
        }
        //查询tab数据

        //list4 中li元素添加监听事件
        mui(".mui-table-view").on('tap', '.listFour', function () {

            $(this).find("span").addClass("on");


            //alert($("span.pcompany.on").text());

            //获取当前li的linkid
            var strID = $("span.linkid.on").text();

            //修改blist中的li
            var fragment = document.createDocumentFragment();
            ajaxUrl = "../api/KmrStorage/GetComQueryDetail_BL.aspx";
            mui.ajax(ajaxUrl, {
                data: { strID: strID },
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
                        strHtml += "<div class=\"list_info\"><div>物料名称：<span>" + js[i].matName + "</span></div></div>";
                        strHtml += "<div class=\"list_info\"><div >代码：<span>" + js[i].MCode + "</span></div><div>物料状态：<span>" + js[i].PDStat + "</span></div></div>";
                        strHtml += "<div class=\"list_info\"><div>发料库房：<span>" + js[i].mStock + "</span></div><div>单位：<span>" + js[i].mUnit + "</span></div></div>";
                        strHtml += "<div class=\"list_info\"><div>申请数量：<span>" + js[i].Pnum + "</div><div>领料类型：<span>" + js[i].Ptype + "</div></div>";
                        if (js[i].pickInfo.length > 0) {
                            strHtml += "<div class=\"list_info\"><div>SAP：<span class=\"ifsap\">" + js[i].ifSap + "</span></div><div>SAP类型：<span class=\"saptype\">" + js[i].pickItemType + "</span></div></div>";
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

        //list3 中li元素添加监听事件

        function btn_switch(obj) {
            var nameid = $(obj).attr("name");
            if ($(obj).hasClass('mui-active')) {
                $(obj).removeClass("mui-active");
                var num = $("span.mui-badge").text()
                num--;
                _oneStat--;
                $("span.mui-badge").text(num);
                $("li." + nameid + "").remove();

            }
            else {
                $(obj).addClass("mui-active");
                $(obj).addClass("onselected");
                //获取领料数量
                var inputNum = $("li#" + nameid + "").find("input").val();
                //获取领料数量
                var num = $("span.mui-badge").text();
                num++;
                _oneStat++;
                $("span.mui-badge").text(num);

                var fragment = document.createDocumentFragment();
                var li;
                li = document.createElement('li');
                li.className = 'mui-table-view-cell picked ' + nameid + '';
                li.id = nameid;
                li.innerHTML = $("li#" + nameid + "").html();

                fragment.appendChild(li);
                $("#blist").append(fragment);
                $(obj).removeClass("onselected");
                $(".onselected").attr("onclick", "btn_pickswitch(this)");

                //设置领料数量

                $("li#" + nameid + ".picked").find("input").val(inputNum);

                //设置领料数量

            }
        }

        function btn_pickswitch(obj) {
            var nameclass = $(obj).attr("name");
            if ($(obj).hasClass('mui-active')) {
                $(obj).removeClass("mui-active");
                $("li." + nameclass + "").removeClass("picked");
            }
            else {
                $(obj).addClass("mui-active");
                $("li." + nameclass + "").addClass("picked");
            }
        }

        

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


        $("#clear").click(function () {
            if (confirm("删除全部物料？")) {
                $("#blist li").remove();
                $("span.mui-badge").text("0");
                mui('#middlePopover').popover('hide');
                var btn = document.getElementById("queryBtn");
                mui.trigger(btn, 'tap');
                //location.reload();
                // $(".pick-submit-clear").hide();
                // $("#middlePopover").removeClass("mui-active");
                // $("#middlePopoverhref").addClass("mui-active");
                // $(".mui-backdrop").remove();
            }
        });

        $("#submit").click(function () {

            var orderNum = $("#OddNo").val();
            var proNum = $("#proNo").val();

            //alert(orderNum);
            //alert(proNum);
            if (orderNum == "请输入订单号" || orderNum.length==0) {
                orderNum = "";
            }
            if (proNum == "请输入工序号" || proNum.length == 0) {
                proNum = "";
            }

            //计划内领料需要输入订单号，计划外领料需要输入订单号，工序号
            //alert(_oneStat);
            if (_oneStat > 0) {
                if (orderNum.length == 0) {
                    mui.toast("存在计划内领料，请输入订单号！");
                    return;
                }
            }

            if ($("li.picked").length > 0) {
                if (confirm("确认提交？")) {
                    showWait();

                }
            }
            else {

                alert("无领料信息，请返回继续添加！")
            }
        })


        function postData() {


            var orderNum = $("#OddNo").val();
            var proNum = $("#proNo").val();
            if (orderNum == "请输入") {
                orderNum = "";
            }
            if (proNum == "请输入") {
                proNum = "";
            }


            var strOneJson = "";
            var strTwoJson = "";
            var strThJson = "";
            var strFourJson = "";
            var strType = "";

            strOneJson += "[";
           
            var oneNum = 0;
            var twoNum = 0;
            var thNum = 0;
            var fourNum = 0;

            var tmp = "";
            $("li.picked").each(function () {
                //alert($(this).html());
                //alert($(this).attr("id"));

                tmp = $(this).attr("id");

                $(this).find("span").addClass("on");
                $(this).find("input").addClass("on");

                strType = $("span.type" + tmp + ".on").text();

                if (strType == "计划内领料") {
                    oneNum += 1;
                    strOneJson += "{\"MatName\":\"" + strtojs($("span.wlmc" + tmp + ".on").text()) + "\",";
                    strOneJson += "\"MCode\":\"" + $("span.code" + tmp + ".on").text() + "\",";
                    strOneJson += "\"MStockCode\":\"" + $("span.stockcode" + tmp + ".on").text() + "\",";
                    strOneJson += "\"MFactoryCode\":\"" + $("span.faccode" + tmp + ".on").text() + "\",";
                    strOneJson += "\"MGroupCode\":\"" + $("span.groupcode" + tmp + ".on").text() + "\",";
                    strOneJson += "\"MGxCode\":\"" + strtojs($("span.gxcode" + tmp + ".on").text()) + "\",";
                    strOneJson += "\"MRsNo\":\"" + $("span.rsNo" + tmp + ".on").text() + "\",";
                    strOneJson += "\"MRsPos\":\"" + $("span.rsPos" + tmp + ".on").text() + "\",";
                    strOneJson += "\"MUnit\":\"" + $("span.unit" + tmp + ".on").text() + "\",";
                    strOneJson += "\"PType\":\"" + $("span.type" + tmp + ".on").text() + "\",";
                    strOneJson += "\"MInventory\":\"" + $("span.num" + tmp + ".on").text() + "\",";
                    strOneJson += "\"Vornr\":\"" + $("span.vornr" + tmp + ".on").text() + "\",";
                    strOneJson += "\"Sernr\":\"" + ($("span.planName" + tmp + ".on").text() == "请选择" ? "" : $("span.planName" + tmp + ".on").text()) + "\",";
                    strOneJson += "\"PickInventory\":\"" + $("input.pnum" + tmp + ".on").val() + "\",";
                    strOneJson += "\"PDStat\":\"1\"},";
                }
 

            })


            strOneJson = strOneJson.substr(0, strOneJson.length - 1);
            strOneJson += "]";
        

            if (oneNum == 0) {
                strOneJson = "";
            }

            
            //strJson += "]}";
            //ajax后台数据处理

            $.post("../api/KmrStorage/pickTabList_sumbit_BL.aspx",
                { PCompany: _company, PDept: _dept, PVenture: _venture, SapVenId: _venSapId, PName: _name, PCode: _cardId, PCostCode: _costCode, OddNo: orderNum, ProNo: proNum, dataListOne: strOneJson, dataListTwo: strTwoJson, dataListTh: strThJson, dataListFour: strFourJson, OneNum: oneNum, TwoNum: twoNum, ThNum: thNum, FourNum: fourNum },
                function (data, status) {
                    var js = JSON.parse(data);
                    //var jskeeper = JSON.parse(data).ddInfo;                   
                    hiddWait();
                    toastInfo(js.info);
                    mui.toast(js.info, { duration: 'long', type: 'div' })
                    alert("领料单号:" + js.PDID);
                    //数据提交后页面处理
                    //$("#blist li").remove();
                    //$("span.mui-badge").text("0");
                    //mui('#middlePopover').popover('hide');
                    //var btn = document.getElementById("queryBtn");
                    //mui.trigger(btn, 'tap');

                    //location.reload();
                    //数据提交后页面处理
                });



            //ajax后台数据处理
            //console.log(strJson);

        }


        function strtojs(str) {
            console.log(str)
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

        function RndNum(n) {
            var rnd = "";
            for (var i = 0; i < n; i++)
                rnd += Math.floor(Math.random() * 10);
            return rnd;
        }


        //初始化查询
        mui.ready(function () {
            //mui.trigger(btn, 'tap');
            //var btn = document.getElementById("queryBtn");
            //mui.trigger(btn, 'tap');
        });


        dd.ready(function () {

            queryWait = function (callback) {
                dd.device.notification.showPreloader({
                    text: "数据查询中..", //loading显示的字符，空表示不显示文字
                    showIcon: true, //是否显示icon，默认true
                    onSuccess: function (result) {
                        QueryInfo();
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


            showWait = function (callback) {
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


            OddIdScan = function () {
                dd.biz.util.scan({
                    type: "qrCode", // type 为 all、qrCode、barCode，默认是all。
                    tips: "请对准条形码/二维码",  //进入扫码页面显示的自定义文案
                    onSuccess: function (data) {
                        //onSuccess将在扫码成功之后回调
                        /* data结构
                          { 'text': String}
                        */

                        var js = data.text;
                        var js = js.replace(/\\/g, '\\\\');
                        var jsObj = JSON.parse(js);
                        $("#OddNo").val(jsObj[0].ID);

                    },
                    onFail: function (err) {
                        alert("scan fail:" + JSON.stringify(err));
                    }
                })
            }

            MatIdScan = function () {
                dd.biz.util.scan({
                    type: "all", // type 为 all、qrCode、barCode，默认是all。
                    tips: "请对准条形码/二维码",  //进入扫码页面显示的自定义文案
                    onSuccess: function (data) {
                        //onSuccess将在扫码成功之后回调
                        /* data结构
                          { 'text': String}
                        */
                        alert(data);
                        var js = data.text;
                        var js = js.replace(/\\/g, '\\\\');
                        var jsObj = JSON.parse(js);
                        $("#PMName").val(jsObj[0].ID);

                    },
                    onFail: function (err) {
                        alert("scan fail:" + JSON.stringify(err));
                    }
                })
            }

        })



    </script>
</body>
</html>
