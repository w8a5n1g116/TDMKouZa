<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="pickInCom_CostCenterlist.aspx.cs" Inherits="DDpage.KmrStorage.pickInCom_CostCenterlist" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8">
    <title>成本中心领料</title>
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
						<label >领料公司</label>
                        <div id="PCompany" class="div_fix"></div>
					</div>
                     <div class="mui-input-row">
						<label>领料部门</label>
						<div id="PDept" class="div_fix"></div>
					</div>
                    <div class="mui-input-row">
						<label>领料成本中心</label>
						<div id="ShowVenture" class="div_fix">
                            <span id="PVenture" class="font_fix2">请选择</span>
						</div>
                        &nbsp;
                        <div id="PSapVenId" class="mui-hidden"></div>
                        &nbsp;
                        <div id="PCostId" ></div>
					</div>
                    <div class="mui-input-row">
						<label>领料工厂</label>
						<div id="showFac">
                            <span id="PFac" class="font_fix2">冰轮铸造工厂</span>
                        </div>  
					</div>
                    <div class="mui-input-row">
						<label>领料库房</label>
						<div id="showStock">
                            <span id="PStock" class="font_fix2">请选择</span>
                        </div>  
					</div>
                    <div class="mui-input-row">
						<label>物料名称</label>
						<input id="PMName" type="text" class="mui-input-clear font_fix" placeholder="请输入物料名称"  style="width:55%">
                        <span class="mui-icon-extra mui-icon-extra-sweep matScanName"></span>
					</div>
            <div class="mui-button-row" id="query">
						<button type="button" class="mui-btn mui-btn-danger" id="queryBtn">查询</button>
					</div>
        </form>
            </div>

         

        <div id="slider" class="mui-slider" >
            <div id="sliderSegmentedControl" class="mui-slider-indicator mui-segmented-control mui-segmented-control-inverted mui-segmented-control-negative">
                <a class="mui-control-item" href="#item3mobile">
                    成本中心
                </a>
                <a class="mui-control-item" href="#item4mobile">
                    查询
                </a>
            </div>
            <div id="sliderProgressBar" class="mui-slider-progress-bar mui-col-xs-6"></div>
            
            <div class="mui-slider-group">
                 
               
                <div id="item3mobile" class="mui-slider-item mui-control-content"  style="height:25em">
                    <div id="scroll3" class="mui-scroll-wrapper">
                        <div class="mui-scroll">
                            <ul class="mui-table-view mui-table-view-striped mui-table-view-condensed" id="list3">

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
    <%--<script src="../js/dingtalk.js"></script>--%>
    <script src="//g.alicdn.com/dingding/dingtalk-jsapi/2.6.41/dingtalk.open.js"></script>
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
        var _facName = "冰轮铸造工厂";
        var _facCode = "1071";
        var _stockName = "";
        var _stockCode = "";
        var strJson = "";
        var ajaxUrl = "";
        var dataStr = Array();
        var dataStockStr = Array();
        var dataVenture = Array();
        var dataFac = Array();
        var dataStock = Array();
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


        var objectArray = new Array();
        var selectedObjectArray = new Array();

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
            //mui("#PVenture")[0].innerHTML = _venture;
            mui("#PSapVenId")[0].innerHTML = _venSapId;
            mui("#PCostId")[0].innerHTML = _costCode;



        })(mui);

        //查询按钮loading处理,查询数据

        mui("#query").on('tap', '.mui-btn', function () {


            var strMatName = mui("#PMName")[0].value;;

            if (strMatName.length == 0) {
                mui.toast("存在及时库存调用，请输入物料名称/代码进行查询！");
                mui("#queryBtn").button('reset');
                return;
            }

            queryWait();

            objectArray = new Array();
            //getThList(this);

            //mui(this).button('loading');
            //var strMatName = mui("#PMName")[0].value;;

            //if (strMatName.length == 0) {
            //    mui.toast("存在及时库存调用，请输入物料名称/代码进行查询！");
            //    mui("#queryBtn").button('reset');
            //    return;
            //}

            ////查询物料信息
            //_listThStat = 0;
            //_listFourStat = 0;
            
            //getThList(this);
            //getFourList(this);
            ////后台获取查询物料信息

            //mui(this).button('reset');
        });

        function QueryInfo() {
            
            

            //查询物料信息
            _listThStat = 0;
            _listFourStat = 0;

            getThList(this);
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

                getVentureList();
                getFacList();
                getStockList();
            });

            //经营体选择
            function getVentureList() {

                ajaxUrl = "../api/KmrStorage/GetCoastCenter.aspx";
                mui.ajax(ajaxUrl, {
                    data: {},
                    async: true,
                    crossDomain: true,
                    //dataType: 'json',//服务器返回json格式数据
                    type: 'post',//HTTP请求类型
                    //timeout: 10000,//超时时间设置为10秒；
                    // headers: { 'Content-Type': 'text/json' },
                    success: function (data) {
                        //服务器返回响应，根据响应结果，分析是否登录成功；
                        var js = JSON.parse(data).data;
                        dataVenture.splice(0, dataVenture.length);
                        for (var i = 0; i < js.length; i++) {
                            dataVenture.push({ value: js[i].CoastCenterID, text: js[i].CoastCenterName});
                        }

                    },
                    error: function (xhr, type, errorThrown) {
                        //异常处理；
                        alert(errorThrown);
                        console.log(type);
                    }
                });

                var showDataPicekerButtonVen = doc.getElementById('ShowVenture');

                showDataPicekerButtonVen.addEventListener('tap', popPickerVen, false);
            }

            function popPickerVen() {

                //更新下拉菜单数据
                var dataPicekerVen = new $.PopPicker();
                dataPicekerVen.setData(dataVenture);
                //var showDataPicekerButton02 = doc.getElementById('showTypeTwo');
                var ResultVen = doc.getElementById('PVenture');

                dataPicekerVen.show(function (items) {
                    ResultVen.innerText = items[0].text;
                    mui("#PSapVenId")[0].innerHTML = items[0].value;
                    mui("#PCostId")[0].innerHTML = items[0].value;
                    _venture = items[0].text;
                    _venSapId = items[0].value;
                    _costCode = items[0].value;
                }
                )

            }

            //工厂选择
            //库房选择
            function getFacList() {

                ajaxUrl = "../api/KmrStorage/GetCheckInFac_BL.aspx";
                mui.ajax(ajaxUrl, {
                    data: { comName: _company },
                    async: true,
                    crossDomain: true,
                    //dataType: 'json',//服务器返回json格式数据
                    type: 'post',//HTTP请求类型
                    //timeout: 10000,//超时时间设置为10秒；
                    // headers: { 'Content-Type': 'text/json' },
                    success: function (data) {
                        //服务器返回响应，根据响应结果，分析是否登录成功；
                        var js = JSON.parse(data).data;
                        dataFac.splice(0, dataFac.length);
                        for (var i = 0; i < js.length; i++) {
                            dataFac.push({ value: js[i].facCode, text: js[i].facName });
                        }

                    },
                    error: function (xhr, type, errorThrown) {
                        //异常处理；
                        alert(errorThrown);
                        console.log(type);
                    }
                });

                var showDataPicekerButtonFac = doc.getElementById('showFac');

                showDataPicekerButtonFac.addEventListener('tap', popPickerFac, false);

            }

            function popPickerFac() {

                //更新下拉菜单数据
                var dataPicekerFac = new $.PopPicker();
                dataPicekerFac.setData(dataFac);
                //var showDataPicekerButton02 = doc.getElementById('showTypeTwo');
                var ResultFac = doc.getElementById('PFac');

                dataPicekerFac.show(function (items) {
                    ResultFac.innerText = items[0].text;
                    _facName = items[0].text;
                    _facCode = items[0].value;

                    //getStockList();
                }
                )

            }



            //库房选择
            function getStockList() {

                ajaxUrl = "../api/KmrStorage/GetCheckInFacStock_BL.aspx";
                mui.ajax(ajaxUrl, {
                    data: { comName: _company, facName: _facName },
                    async: true,
                    crossDomain: true,
                    //dataType: 'json',//服务器返回json格式数据
                    type: 'post',//HTTP请求类型
                    //timeout: 10000,//超时时间设置为10秒；
                    // headers: { 'Content-Type': 'text/json' },
                    success: function (data) {
                        //服务器返回响应，根据响应结果，分析是否登录成功；
                        var js = JSON.parse(data).data;
                        dataStock.splice(0, dataStock.length);
                        for (var i = 0; i < js.length; i++) {
                            dataStock.push({ value: js[i].stockCode, text: js[i].stockName});
                        }

                    },
                    error: function (xhr, type, errorThrown) {
                        //异常处理；
                        alert(errorThrown);
                        console.log(type);
                    }
                });

                var showDataPicekerButtonStock = doc.getElementById('showStock');

                showDataPicekerButtonStock.addEventListener('tap', popPickerStock, false);

            }

            function popPickerStock() {

                //更新下拉菜单数据
                var dataPicekerStock = new $.PopPicker();
                dataPicekerStock.setData(dataStock);
                //var showDataPicekerButton02 = doc.getElementById('showTypeTwo');
                var ResultStock = doc.getElementById('PStock');

                dataPicekerStock.show(function (items) {
                    ResultStock.innerText = items[0].text;
                    _stockName = items[0].text;
                    _stockCode = items[0].value;
                }
                )

            }

        })(mui, document);

        //点击更新订单号下拉菜单
        //mui(".mui-input-row").on('tap', '.OddList', function () {
        //    getOddNolist();
        //});
        //点击更新订单号下拉菜单


        //查询按钮还原
        function resetBtn() {
            if ( _listThStat == 1 && _listFourStat == 1) {
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

        

        function stockPickList(ob) {
            var strid = $(ob).attr("name");
            var strMatCode = $("span.codeone" + strid + "").text()
            var strMatNum = $("span.numone" + strid + "").text()

            //获取库存地点数据

            ajaxUrl = "../api/KmrStorage/GetComPlanStock.aspx";
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

        
        //成本中心领料列表
        function getThList() {
            var strMatName = mui("#PMName")[0].value;;
            var strdataNum = 0;
            if (strMatName.length > 0) {
                strdataNum = 10;
            }
            var strStockName = $("#PStock").text();
            if (strStockName == "请选择") {
                strStockName = "";
            }

            //var strOddNo = mui("#OddNo")[0].value;


            var fragment = document.createDocumentFragment();
            ajaxUrl = "../api/KmrStorage/GetComInventoryListUpdate_BL.aspx";
            mui.ajax(ajaxUrl, {
                data: { comName: _company, matName: strMatName,facCode:_facCode, stockCode: _stockCode, stockName: strStockName, oddNo: '', dataNum: strdataNum },
                async: false,
                crossDomain: true,
                type: 'post',//HTTP请求类型
                success: function (data) {
                    //服务器返回响应，根据响应结果，分析是否登录成功；
                    var js = JSON.parse(data).data;
                    var li;

                    objectArray = js;

                    var strHtml = "";

                    fragment = document.createDocumentFragment();

                    for (var i = 0; i < js.length; i++) {
                        strHtml += '<li class="mui-table-view-cell" id="' + 'th' + js[i].matID + '" title="th">'
                        strHtml += "<div class=\"list_text\">";
                        strHtml += "<div class=\"list_info\"><div>物料名称：<span class=\"wlmcth" + js[i].matID + "\">" + js[i].matName + "</span></div><div class=\"mui-switch mui-switch-danger  th" + js[i].matID + "\" name=\"th" + js[i].matID + "\"  onclick=\"btn_thswitch(this)\"><div class=\"mui-switch-handle\"></div></div></div>";
                        strHtml += "<div class=\"list_info\"><div >代码：<span class=\"codeth" + js[i].matID + "\">" + js[i].matCode + "</span><span class=\"facth" + js[i].matID + "\" style=\"display:none\">" + js[i].matFac + "</span><span class=\"faccodeth" + js[i].matID + "\" style=\"display:none\">" + js[i].matFacCode + "</span><span class=\"stockcodeth" + js[i].matID + "\" style=\"display:none\">" + js[i].matStockCode + "</span><span class=\"groupth" + js[i].matID + "\" style=\"display:none\">" + js[i].matGroup + "</span><span class=\"groupcodeth" + js[i].matID + "\" style=\"display:none\">" + js[i].matGroupCode + "</span></div><div><span class=\"mui-icon iconfont icon-peiyanshouhege  report\" name=\"" + js[i].MCode + "\"></span></div></div>";
                        strHtml += "<div class=\"list_info\"><div>批次号：<span class=\"noth" + js[i].matID + "\">" + js[i].matNo + "</div><div>单位：<span class=\"unitth" + js[i].matID + "\">" + js[i].matWeight + "</div></div>";
                        strHtml += "<div class=\"list_info\"><div>库存地点：<span class=\"stockth" + js[i].matID + "\">" + js[i].matStock + "</div><div>领料类型：<span class=\"typeth" + js[i].matID + "\">成本中心领料</div></div>";
                        strHtml += "<div class=\"list_info\"><div>库存：<span class=\"numth" + js[i].matID + "\">" + js[i].matNum + "</div><div>领料数量:<input type=\"text\" style=\"width: 5em; height: 1em; margin: 0; font - size:0.5em\" class=\"pnumth" + js[i].matID + "\" value=\"\"></div></div>";
                        strHtml += "<div class=\"list_info\">备注：<input class=\"remark" + js[i].matID + "\"   type=\"text\" style=\"width: 85%; height: 1em; margin: 0; font - size:0.5em\"></div>";
                        strHtml += "</div>";
                        strHtml += '</li>'
                        //li = document.createElement('li');
                        //li.id = "th" + js[i].matID;
                        //li.title = "th";
                        //li.className = 'mui-table-view-cell';
                        //li.innerHTML = strHtml;
                        //fragment.appendChild(li);
                    };

                    //alert(JSON.stringify(selectedObjectArray));

                    for (var i = 0; i < selectedObjectArray.length; i++) {
                        strHtml += '<li class="mui-table-view-cell" id="' + 'th' + selectedObjectArray[i].matID + '" title="th">'
                        strHtml += "<div class=\"list_text\">";
                        strHtml += "<div class=\"list_info\"><div>物料名称：<span class=\"wlmcth" + selectedObjectArray[i].matID + "\">" + selectedObjectArray[i].matName + "</span></div><div class=\"mui-switch mui-switch-danger mui-active  th" + selectedObjectArray[i].matID + "\" name=\"th" + selectedObjectArray[i].matID + "\"  onclick=\"btn_thswitch(this)\"><div class=\"mui-switch-handle\"></div></div></div>";
                        strHtml += "<div class=\"list_info\"><div >代码：<span class=\"codeth" + selectedObjectArray[i].matID + "\">" + selectedObjectArray[i].matCode + "</span><span class=\"facth" + selectedObjectArray[i].matID + "\" style=\"display:none\">" + selectedObjectArray[i].matFac + "</span><span class=\"faccodeth" + selectedObjectArray[i].matID + "\" style=\"display:none\">" + selectedObjectArray[i].matFacCode + "</span><span class=\"stockcodeth" + selectedObjectArray[i].matID + "\" style=\"display:none\">" + selectedObjectArray[i].matStockCode + "</span><span class=\"groupth" + selectedObjectArray[i].matID + "\" style=\"display:none\">" + selectedObjectArray[i].matGroup + "</span><span class=\"groupcodeth" + selectedObjectArray[i].matID + "\" style=\"display:none\">" + selectedObjectArray[i].matGroupCode + "</span></div><div><span class=\"mui-icon iconfont icon-peiyanshouhege  report\" name=\"" + selectedObjectArray[i].MCode + "\"></span></div></div>";
                        strHtml += "<div class=\"list_info\"><div>批次号：<span class=\"noth" + selectedObjectArray[i].matID + "\">" + selectedObjectArray[i].matNo + "</div><div>单位：<span class=\"unitth" + selectedObjectArray[i].matID + "\">" + selectedObjectArray[i].matWeight + "</div></div>";
                        strHtml += "<div class=\"list_info\"><div>库存地点：<span class=\"stockth" + selectedObjectArray[i].matID + "\">" + selectedObjectArray[i].matStock + "</div><div>领料类型：<span class=\"typeth" + selectedObjectArray[i].matID + "\">成本中心领料</div></div>";
                        strHtml += "<div class=\"list_info\"><div>库存：<span class=\"numth" + selectedObjectArray[i].matID + "\">" + selectedObjectArray[i].matNum + "</div><div>领料数量:<input type=\"text\" style=\"width: 5em; height: 1em; margin: 0; font - size:0.5em\" class=\"pnumth" + selectedObjectArray[i].matID + "\" value=\"\"></div></div>";
                        strHtml += "<div class=\"list_info\">备注：<input class=\"remark" + selectedObjectArray[i].matID + "\"   type=\"text\" style=\"width: 85%; height: 1em; margin: 0; font - size:0.5em\"></div>";
                        strHtml += "</div>";
                        strHtml += '</li>'
                        //li = document.createElement('li');
                        //li.id = "th" + selectedObjectArray[i].matID;
                        //li.title = "th";
                        //li.className = 'mui-table-view-cell';
                        //li.innerHTML = strHtml;
                        //fragment.appendChild(li);
                    };

                    //li = document.createElement('li');
                    //li.className = 'mui-table-view-cell';
                    //li.innerHTML = "<div class=\"list_text\" style=\"text-align:center;margin-bottom:4em\">已经到底了！！！</div>";
                    //fragment.appendChild(li);
                    strHtml += '<li class="mui-table-view-cell"><div class="list_text" style="text-align:center;margin-bottom:4em">已经到底了！！！</div></li>'

                    $("#list3 li").remove();
                    $("#list3").append(strHtml);

                    _listThStat = 1;
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
        function getFourList() {
            var strMatName = mui("#PMName")[0].value;
            var strdataNum = 0;
            if (strMatName.length > 0) {
                strdataNum = 10;
            }
            var fragment = document.createDocumentFragment();
            ajaxUrl = "../api/KmrStorage/GetComPickInfo_BL.aspx";
            mui.ajax(ajaxUrl, {
                data: { matName: strMatName, Company: _company, Dept: _dept, Ven: _venture, Name: _name },
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
            ajaxUrl = "../api/KmrStorage/GetComQueryDetail.aspx";
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

       


        function btn_thswitch(obj) {
            var nameid = $(obj).attr("name");            

            if ($(obj).hasClass('mui-active')) {
                $(obj).removeClass("mui-active");
                var num = $("span.mui-badge").text()
                num--;
                _thStat--;
                $("span.mui-badge").text(num);
                $("li." + nameid + "").remove();

                var matId = nameid.substring(2);
                $.each(selectedObjectArray, function (i, n) {
                    if (n.matID == matId) {
                        selectedObjectArray.pop(n);
                    }
                });

            }
            else {
                $(obj).addClass("mui-active");
                $(obj).addClass("onthselected");

                //获取领料数量
                var inputNum = $("li#" + nameid + "").find("input").val();
                //获取领料数量

                var num = $("span.mui-badge").text();
                num++;
                _thStat++;
                $("span.mui-badge").text(num);

                var fragment = document.createDocumentFragment();
                var li;
                li = document.createElement('li');
                li.className = 'mui-table-view-cell picked ' + nameid + '';
                li.id = nameid;
                li.innerHTML = $("li#" + nameid + "").html();

                fragment.appendChild(li);
                $("#blist").append(fragment);
                $(obj).removeClass("onthselected");
                $(".onthselected").attr("onclick", "btn_Thpickswitch(this)");

                //设置领料数量

                $("li#" + nameid + ".picked").find("input").val(inputNum);

                //设置领料数量

                var matId = nameid.substring(2);
                $.each(objectArray, function (i, n) {
                    if (n.matID == matId) {
                        selectedObjectArray.push(n);
                    }
                });

            }

            //alert(JSON.stringify(selectedObjectArray));

        }


        function btn_Thpickswitch(obj) {
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


        //lims检验报告函数

      

        mui("#list3").on('tap', '.report', function () {
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
            if (orderNum == "请输入") {
                orderNum = "";
            }
            if (proNum == "请输入") {
                proNum = "";
            }

            //计划内领料需要输入订单号，计划外领料需要输入订单号，工序号
            

            if (_thStat > 0 ) {
                if (_costCode.length == 0) {
                    mui.toast("存在成本中心/研发类领料，EHR中未维护成本中心！");
                    return
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

            var couldPost = true;


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

            strThJson += "[";

  
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
                
                if (strType == "成本中心领料") {
                    thNum += 1;
                    strThJson += "{\"MStock\":\"" + $("span.stock" + tmp + ".on").text() + "\",";
                    strThJson += "\"MStockCode\":\"" + $("span.stockcode" + tmp + ".on").text() + "\",";
                    strThJson += "\"MFactory\":\"" + $("span.fac" + tmp + ".on").text() + "\",";
                    strThJson += "\"MFactoryCode\":\"" + $("span.faccode" + tmp + ".on").text() + "\",";
                    strThJson += "\"MGroup\":\"" + $("span.group" + tmp + ".on").text() + "\",";
                    strThJson += "\"MGroupCode\":\"" + $("span.groupcode" + tmp + ".on").text() + "\",";
                    strThJson += "\"Material\":\"" + strtojs($("span.wlmc" + tmp + ".on").text()) + "\",";
                    strThJson += "\"MCode\":\"" + $("span.code" + tmp + ".on").text() + "\",";
                    strThJson += "\"MBatch\":\"" + $("span.no" + tmp + ".on").text() + "\",";
                    strThJson += "\"MUnit\":\"" + $("span.unit" + tmp + ".on").text() + "\",";
                    strThJson += "\"MInventory\":\"" + $("span.num" + tmp + ".on").text() + "\",";
                    strThJson += "\"PickInventory\":\"" + $("input.pnum" + tmp + ".on").val() + "\",";
                    strThJson += "\"PType\":\"" + $("span.type" + tmp + ".on").text() + "\",";
                    strThJson += "\"PRemark\":\"" + $("input.remark" + tmp + ".on").val() + "\",";
                    strThJson += "\"PDStat\":\"1\"},";

                    var inventory = Number($("span.num" + tmp + ".on").text());
                    var pick = Number($("input.pnum" + tmp + ".on").val());

                    if (pick > inventory) {
                        alert("领料数量不能大于库存！");
                        couldPost = false;
                        return;
                    }

                    if (0 > pick) {
                        alert("领料数量不能小于0！");
                        couldPost = false;
                        return;
                    }
                }
                

            })

            if (!couldPost) {
                return;
            }

            strThJson = strThJson.substr(0, strThJson.length - 1);
            strThJson += "]";


            if (thNum == 0) {
                strThJson = "";
            }

            //strJson += "]}";
            //ajax后台数据处理

            $.post("../api/KmrStorage/pickTabList_sumbit_BL.aspx",
                {
                    PCompany: _company,
                    PDept: _dept,
                    PVenture: _venture,
                    SapVenId: _venSapId,
                    PName: _name,
                    PCode: _cardId,
                    PCostCode: _costCode,
                    OddNo: '',
                    ProNo: '',
                    dataListOne: strOneJson,
                    dataListTwo: strTwoJson,
                    dataListTh: strThJson,
                    dataListFour: strFourJson,
                    OneNum: oneNum,
                    TwoNum: twoNum,
                    ThNum: thNum,
                    FourNum: fourNum
                },
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
