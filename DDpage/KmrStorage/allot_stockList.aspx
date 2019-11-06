<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="allot_stockList.aspx.cs" Inherits="DDpage.KmrStorage.allot_stockList" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8">
    <title>库存调拨</title>
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
						<label >公司</label>
                        <div id="showCompany" class="div_fix">
                            <span id="queryCompany" class="font_fix2"></span>
                        </div>
					</div>
                    <div class="mui-input-row">
						<label>调出库房</label>
						<div id="ShowOutStock" class="div_fix">
                            <span id="outStock" class="font_fix2">请选择...</span>
                            <span id="outStockCode" class="font_fix2"></span>
						</div>
					</div>
                    <div class="mui-input-row">
						<label>调入库房</label>
						<div id="showInStock" class="div_fix">
                            <span id="inStock" class="font_fix2">请选择...</span>
                            <span id="inStockCode" class="font_fix2"></span>
						</div>
					</div>
                    <div class="mui-input-row">
						<label>物料名称</label>
						<input id="matName" type="text" class="mui-input-clear font_fix" placeholder="请输入物料名称">
					</div>
            <div class="mui-button-row" id="query">
						<button type="button" class="mui-btn mui-btn-danger" id="queryBtn">查询</button>
					</div>
        </form>
            </div>

         

        <div id="slider" class="mui-slider" >
            <div id="sliderSegmentedControl" class="mui-slider-indicator mui-segmented-control mui-segmented-control-inverted mui-segmented-control-negative">
                <a class="mui-control-item" href="#item1mobile">
                    库存明细
                </a>
                <a class="mui-control-item" href="#item2mobile">
                    调拨查询
                </a>
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
                <div id="item2mobile" class="mui-slider-item mui-control-content " style="height:35em">    
                   
                    <div id="scroll2" class="mui-scroll-wrapper">
                        <div class="mui-scroll">                     
                            <ul class="mui-table-view mui-table-view-striped mui-table-view-condensed" id="list2" >
                                
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

    


    <%--<nav class="mui-bar mui-bar-tab " id="nav-submit">
        <a class="mui-tab-item mui-active checkSubmit" id="submit">
            <span class="mui-icon iconfont icon-jiaogongyanshou"></span>
            <span class="mui-tab-label">提交</span>
        </a>
    </nav>--%>

    <nav class="mui-bar mui-bar-tab">
        <a class="mui-tab-item " href="#middlePopover" id="middlePopoverhref">
            <span class="mui-icon iconfont icon-icon_jiarulingliaoche"><span class="mui-badge" >0</span></span>
            <span class="mui-tab-label">已选</span>
        </a>
    </nav>


    <script src="../js/jquery-1.11.0.min.js"></script>
   <%-- <script src="../js/dingtalk.js"></script>--%>
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
            jsApiList: ['biz.user.get', 'device.geolocation.get', 'biz.map.locate', 'biz.util.uploadImageFromCamera', 'biz.util.uploadImage']
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
        var strJson = "";
        var ajaxUrl = "";
        var arrayCompany = Array();
        var arrayFac = Array();
        var arrayStock = Array();
        var showWait;
        var hiddWait;
        var toastInfo;

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


        var objectArray = new Array();
        var selectedObjectArray = new Array();



        (function ($) {
            $('.mui-scroll-wrapper').scroll({
                indicators: true //是否显示滚动条
            });
            //mui("#queryCompany")[0].innerHTML = _company;
            //mui("#PDept")[0].innerHTML = _dept;
            // mui("#PVenture")[0].innerHTML = _venture;

        })(mui);


        //发料仓库下拉列表
        (function ($, doc) {
            $.init();
            $.ready(function () {
                var _getParam = function (obj, param) {
                    return obj[param] || '';
                };

                getOutStockList();
                getInStockList();

                function getOutStockList() {
                    var strCom = _company;

                    ajaxUrl = "../api/KmrStorage/GetCheckInFacStock_BL.aspx";
                    mui.ajax(ajaxUrl, {
                        data: { com: strCom  },
                        async: true,
                        crossDomain: true,
                        //dataType: 'json',//服务器返回json格式数据
                        type: 'post',//HTTP请求类型
                        //timeout: 10000,//超时时间设置为10秒；
                        // headers: { 'Content-Type': 'text/json' },
                        success: function (data) {
                            //服务器返回响应，根据响应结果，分析是否登录成功；
                            var js = JSON.parse(data).data;
                            arrayStock.splice(0, arrayStock.length);
                            for (var i = 0; i < js.length; i++) {
                                arrayStock.push({ value: js[i].stockCode, text: js[i].stockName });
                            }

                        },
                        error: function (xhr, type, errorThrown) {
                            //异常处理；
                            alert(errorThrown);
                            console.log(type);
                        }
                    });

                    var showDataPicekerButtonStock = doc.getElementById('ShowOutStock');

                    showDataPicekerButtonStock.addEventListener('tap', popPickerOutStock, false);
                }


                function popPickerOutStock() {

                    //更新下拉菜单数据
                    var dataPicekerStock = new $.PopPicker();
                    dataPicekerStock.setData(arrayStock);
                    //var showDataPicekerButton02 = doc.getElementById('showTypeTwo');
                    var ResultStock = doc.getElementById('outStock');

                    dataPicekerStock.show(function (items) {
                        ResultStock.innerText = items[0].text;
                        mui("#outStockCode")[0].innerText = items[0].value;
                    }
                    )
                }

                function getInStockList() {
                    var strCom = _company;

                    ajaxUrl = "../api/KmrStorage/GetCheckInFacStock_BL.aspx";
                    mui.ajax(ajaxUrl, {
                        data: { com: strCom },
                        async: true,
                        crossDomain: true,
                        //dataType: 'json',//服务器返回json格式数据
                        type: 'post',//HTTP请求类型
                        //timeout: 10000,//超时时间设置为10秒；
                        // headers: { 'Content-Type': 'text/json' },
                        success: function (data) {
                            //服务器返回响应，根据响应结果，分析是否登录成功；
                            var js = JSON.parse(data).data;
                            arrayStock.splice(0, arrayStock.length);
                            for (var i = 0; i < js.length; i++) {
                                arrayStock.push({ value: js[i].stockCode, text: js[i].stockName });
                            }

                        },
                        error: function (xhr, type, errorThrown) {
                            //异常处理；
                            alert(errorThrown);
                            console.log(type);
                        }
                    });

                    var showDataPicekerButtonStock = doc.getElementById('showInStock');

                    showDataPicekerButtonStock.addEventListener('tap', popPickerInStock, false);
                }


                function popPickerInStock() {

                    //更新下拉菜单数据
                    var dataPicekerStock = new $.PopPicker();
                    dataPicekerStock.setData(arrayStock);
                    //var showDataPicekerButton02 = doc.getElementById('showTypeTwo');
                    var ResultStock = doc.getElementById('inStock');

                    dataPicekerStock.show(function (items) {
                        ResultStock.innerText = items[0].text;
                        mui("#inStockCode")[0].innerText = items[0].value;
                    }
                    )
                }


            });

        })(mui, document);
        //发料仓库下拉列表


        //查询按钮loading处理,查询数据
        var strCompany = "";
        var strOutStock = "";
        var strOutStockCode = "";
        var strInStock = "";
        var strInStockCode = "";
        var strMatName = "";
        mui("#queryCompany")[0].innerText = _company;
        mui("#query").on('tap', '.mui-btn', function () {
            mui(this).button('loading');
            //查询物料信息
            strCompany = mui("#queryCompany")[0].innerText;
            if (strCompany == "请选择...") {
                strCompany = "";
            }
            strOutStock = mui("#outStock")[0].innerText;
            if (strOutStock == "请选择...") {
                strOutStock = "";
            }
            strInStock = mui("#inStock")[0].innerText;
            if (strInStock == "请选择...") {
                strInStock = "";
            }

            strMatName = mui("#matName")[0].value;

            objectArray = new Array();

            getFList(this);
            getSList(this);


            //后台获取查询物料信息

            mui(this).button('reset');
        });
        //查询按钮loading处理,库存查询数据
        function getFList(ob) {
            var fragment = document.createDocumentFragment();
            ajaxUrl = "../api/KmrStorage/GetQueryInventoryList_BL.aspx";
            mui.ajax(ajaxUrl, {
                data: { company: strCompany, stock: strOutStock, matName: strMatName },
                async: false,
                crossDomain: true,
                type: 'post',//HTTP请求类型
                success: function (data) {
                    //服务器返回响应，根据响应结果，分析是否登录成功；
                    var js = JSON.parse(data).data;
                    var li;

                    objectArray = js;

                    //alert(objectArray[0].matID);
                    
                    var strHtml = "";

                    fragment = document.createDocumentFragment();

                    if (js.length > 0) {


                        for (var i = 0; i < js.length; i++) {
                            strHtml += '<li id="' + 'one' + js[i].matID + '" class="mui-table-view-cell picked" title="one">';
                            strHtml += "<div class=\"list_text\">";
                            strHtml += "<div class=\"list_info\"><div>物料名称：<span class=\"wlmcone" + js[i].matID + "\">" + js[i].matName + "</span></div><div class=\"mui-switch mui-switch-danger  one" + js[i].matID + "\" name=\"one" + js[i].matID + "\"  onclick=\"btn_switch(this)\"><div class=\"mui-switch-handle\"></div></div></div>";
                            strHtml += "<div class=\"list_info\"><div >代码：<span class=\"codeone" + js[i].matID + "\">" + js[i].matCode + "</span><span class=\"faccodeone" + js[i].matID + "\" style=\"display:none\">" + js[i].matFacCode + "</span><span class=\"stockcodeone" + js[i].matID + "\" style=\"display:none\">" + js[i].matStockCode + "</span><span class=\"groupone" + js[i].matID + "\" style=\"display:none\">" + js[i].matGroup + "</span><span class=\"groupcodeone" + js[i].matID + "\" style=\"display:none\">" + js[i].matGroupCode + "</span></div></div>";
                            strHtml += "<div class=\"list_info\"><div>批次号：<span class=\"noone" + js[i].matID + "\">" + js[i].matNo + "</div><div>单位：<span class=\"unitone" + js[i].matID + "\">" + js[i].matWeight + "</div></div>";
                            strHtml += "<div class=\"list_info\"><div>工厂：<span class=\"facone" + js[i].matID + "\" >" + js[i].matFac + "</span></div><div>库存地点：<span class=\"stockone" + js[i].matID + "\">" + js[i].matStock + "</div></div>";
                            strHtml += "<div class=\"list_info\"><div>库存：<span class=\"numone" + js[i].matID + "\">" + js[i].matNum + "</div><div>调拨数量:<input type=\"text\" style=\"width: 5em; height: 1em; margin: 0; font - size:0.5em\" class=\"pnumone" + js[i].matID + "\" value=\"\"></div></div>";
                            strHtml += "</div>";
                            strHtml += '</li>'
                            //li = document.createElement('li');
                            //li.id = "one" + js[i].matID;
                            //li.title = "one";
                            //li.className = 'mui-table-view-cell picked';
                            //li.innerHTML = strHtml;
                            //fragment.appendChild(li);
                        };


                        for (var i = 0; i < selectedObjectArray.length; i++) {
                            strHtml += '<li id="' + 'one' + selectedObjectArray[i].matID + '" class="mui-table-view-cell picked" title="one">';
                            strHtml += "<div class=\"list_text\">";
                            strHtml += "<div class=\"list_info\"><div>物料名称：<span class=\"wlmcone" + selectedObjectArray[i].matID + "\">" + selectedObjectArray[i].matName + "</span></div><div class=\"mui-switch mui-switch-danger mui-active  one" + selectedObjectArray[i].matID + "\" name=\"one" + selectedObjectArray[i].matID + "\"  onclick=\"btn_switch(this)\"><div class=\"mui-switch-handle\"></div></div></div>";
                            strHtml += "<div class=\"list_info\"><div >代码：<span class=\"codeone" + selectedObjectArray[i].matID + "\">" + selectedObjectArray[i].matCode + "</span><span class=\"faccodeone" + selectedObjectArray[i].matID + "\" style=\"display:none\">" + selectedObjectArray[i].matFacCode + "</span><span class=\"stockcodeone" + selectedObjectArray[i].matID + "\" style=\"display:none\">" + selectedObjectArray[i].matStockCode + "</span><span class=\"groupone" + selectedObjectArray[i].matID + "\" style=\"display:none\">" + selectedObjectArray[i].matGroup + "</span><span class=\"groupcodeone" + selectedObjectArray[i].matID + "\" style=\"display:none\">" + selectedObjectArray[i].matGroupCode + "</span></div></div>";
                            strHtml += "<div class=\"list_info\"><div>批次号：<span class=\"noone" + selectedObjectArray[i].matID + "\">" + selectedObjectArray[i].matNo + "</div><div>单位：<span class=\"unitone" + selectedObjectArray[i].matID + "\">" + selectedObjectArray[i].matWeight + "</div></div>";
                            strHtml += "<div class=\"list_info\"><div>工厂：<span class=\"facone" + selectedObjectArray[i].matID + "\" >" + selectedObjectArray[i].matFac + "</span></div><div>库存地点：<span class=\"stockone" + selectedObjectArray[i].matID + "\">" + selectedObjectArray[i].matStock + "</div></div>";
                            strHtml += "<div class=\"list_info\"><div>库存：<span class=\"numone" + selectedObjectArray[i].matID + "\">" + selectedObjectArray[i].matNum + "</div><div>调拨数量:<input type=\"text\" style=\"width: 5em; height: 1em; margin: 0; font - size:0.5em\" class=\"pnumone" + selectedObjectArray[i].matID + "\" value=\"\"></div></div>";
                            strHtml += "</div>";
                            strHtml += '</li>'
                            //li = document.createElement('li');
                            //li.id = "one" + selectedObjectArray[i].matID;
                            //li.title = "one";
                            //li.className = 'mui-table-view-cell picked';
                            //li.innerHTML = strHtml;
                            //fragment.appendChild(li);
                        };


                        //li = document.createElement('li');
                        //li.className = 'mui-table-view-cell';
                        //li.innerHTML = "<div class=\"list_text\" style=\"text-align:center;margin-bottom:4em\">已经到底了！！！</div>";
                        //fragment.appendChild(li);

                        strHtml += '<li class="mui-table-view-cell"><div class="list_text" style="text-align:center;margin-bottom:4em">已经到底了！！！</div></li>'


                        $("#list1 li").remove();
                        $("#list1").append(strHtml);
                    }
                    else {

                        for (var i = 0; i < selectedObjectArray.length; i++) {
                            strHtml += '<li id="' + 'one' + selectedObjectArray[i].matID + '" class="mui-table-view-cell picked" title="one">';
                            strHtml += "<div class=\"list_text\">";
                            strHtml += "<div class=\"list_info\"><div>物料名称：<span class=\"wlmcone" + selectedObjectArray[i].matID + "\">" + selectedObjectArray[i].matName + "</span></div><div class=\"mui-switch mui-switch-danger mui-active  one" + selectedObjectArray[i].matID + "\" name=\"one" + selectedObjectArray[i].matID + "\"  onclick=\"btn_switch(this)\"><div class=\"mui-switch-handle\"></div></div></div>";
                            strHtml += "<div class=\"list_info\"><div >代码：<span class=\"codeone" + selectedObjectArray[i].matID + "\">" + selectedObjectArray[i].matCode + "</span><span class=\"faccodeone" + selectedObjectArray[i].matID + "\" style=\"display:none\">" + selectedObjectArray[i].matFacCode + "</span><span class=\"stockcodeone" + selectedObjectArray[i].matID + "\" style=\"display:none\">" + selectedObjectArray[i].matStockCode + "</span><span class=\"groupone" + selectedObjectArray[i].matID + "\" style=\"display:none\">" + selectedObjectArray[i].matGroup + "</span><span class=\"groupcodeone" + selectedObjectArray[i].matID + "\" style=\"display:none\">" + selectedObjectArray[i].matGroupCode + "</span></div></div>";
                            strHtml += "<div class=\"list_info\"><div>批次号：<span class=\"noone" + selectedObjectArray[i].matID + "\">" + selectedObjectArray[i].matNo + "</div><div>单位：<span class=\"unitone" + selectedObjectArray[i].matID + "\">" + selectedObjectArray[i].matWeight + "</div></div>";
                            strHtml += "<div class=\"list_info\"><div>工厂：<span class=\"facone" + selectedObjectArray[i].matID + "\" >" + selectedObjectArray[i].matFac + "</span></div><div>库存地点：<span class=\"stockone" + selectedObjectArray[i].matID + "\">" + selectedObjectArray[i].matStock + "</div></div>";
                            strHtml += "<div class=\"list_info\"><div>库存：<span class=\"numone" + selectedObjectArray[i].matID + "\">" + selectedObjectArray[i].matNum + "</div><div>调拨数量:<input type=\"text\" style=\"width: 5em; height: 1em; margin: 0; font - size:0.5em\" class=\"pnumone" + selectedObjectArray[i].matID + "\" value=\"\"></div></div>";
                            strHtml += "</div>";
                            strHtml += '</li>'
                            //li = document.createElement('li');
                            //li.id = "one" + selectedObjectArray[i].matID;
                            //li.title = "one";
                            //li.className = 'mui-table-view-cell picked';
                            //li.innerHTML = strHtml;
                            //fragment.appendChild(li);
                        };

                        //li = document.createElement('li');
                        //li.className = 'mui-table-view-cell';
                        //li.innerHTML = "<div class=\"list_text\" style=\"text-align:center;margin-bottom:4em\">已经到底了！！！</div>";
                        //fragment.appendChild(li);

                        strHtml += '<li class="mui-table-view-cell"><div class="list_text" style="text-align:center;margin-bottom:4em">已经到底了！！！</div></li>'

                        $("#list1 li").remove();
                        $("#list1").append(strHtml);

                    }
                },
                error: function (xhr, type, errorThrown) {
                    //异常处理；
                    alert(type);
                    console.log(type);
                }
            });

        }
        //库存查询数据

        //已调拨数据查询
        function getSList(ob) {
            var fragment = document.createDocumentFragment();
            ajaxUrl = "../api/KmrStorage/GetAllotQueryInfo_BL.aspx";
            mui.ajax(ajaxUrl, {
                data: { Company: _company, Dept: _dept, Ven: _venture, OutStock: strOutStock, InStock: strInStock, Name: _name },
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
                        strHtml += "<div class=\"list_info\"><div>申请单号：<span class=\"pid\">" + js[i].PID + "</span></div></div>";
                        strHtml += "<div class=\"list_info\"><div>申请时间：<span class=\"ptime\">" + js[i].PTime + "</span></div></div>";
                        strHtml += "<div class=\"list_info\"><div>申请公司：<span class=\"pcompany\">" + js[i].PCom + "</span></div><div>申请部门：<span class=\"pdept\">" + js[i].PDept + "</span></div></div>";
                        strHtml += "<div class=\"list_info\"><div>申请经营体：<span class=\"pven\">" + js[i].PVen + "</span></div><div><span class=\"linkid\" style=\"display:none\">" + js[i].PLID + "</span></div><div>申请人：<span class=\"pname\">" + js[i].PName + "</span></div></div>";
                        strHtml += "</a>";
                        strHtml += "</div>";
                        li = document.createElement('li');
                        li.id = js[i].matID;
                        li.title = "";
                        li.className = 'mui-table-view-cell listQuery';
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

        //已调拨数据查询

        //list2 中li元素添加监听事件
        mui(".mui-table-view").on('tap', '.listQuery', function () {

            $(this).find("span").addClass("on");


            //alert($("span.pcompany.on").text());

            //获取当前li的linkid
            var strLinkID = $("span.linkid.on").text();
            
            //修改blist中的li
            var fragment = document.createDocumentFragment();
            ajaxUrl = "../api/KmrStorage/GetAllotQueryDetail_BL.aspx";
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
                        strHtml += "<div class=\"list_info\"><div>物料名称：<span>" + js[i].MatName + "</span></div></div>";
                        strHtml += "<div class=\"list_info\"><div >代码：<span>" + js[i].MCode + "</span></div></div>";
                        strHtml += "<div class=\"list_info\"><div >工厂：<span>" + js[i].MFac + "</span></div><div>单位：<span>" + js[i].MUnit + "</span></div></div>";
                        strHtml += "<div class=\"list_info\"><div>调出库房：<span>" + js[i].MOutStock + "</span></div><div>调入库房：<span>" + js[i].MInStock + "</span></div></div>";
                        strHtml += "<div class=\"list_info\"><div>库存：<span>" + js[i].MInven + "</div><div>调拨数量:<span>" + js[i].PNum + "</div></div>";
                        //if (js[i].pickInfo.length > 0) {
                        //    strHtml += "<div class=\"list_info\"><div>SAP：<span class=\"ifsap\">" + js[i].ifSap + "</span></div><div>SAP类型：<span class=\"saptype\">" + js[i].pickItemType + "</span></div></div>";
                        //    strHtml += "<div class=\"list_info\"><div>SAP信息：<span class=\"sappickinfo\">" + js[i].pickInfo + "</span></div></div>";
                        //}

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

        function btn_switch(obj) {
            var nameid = $(obj).attr("name");
            if ($(obj).hasClass('mui-active')) {
                $(obj).removeClass("mui-active");

                var num = $("span.mui-badge").text()
                num--;
                $("span.mui-badge").text(num);

                //$("li.picked" + nameid + "").remove();


                $("li#" + nameid + "").removeClass("picked");

                var matId = nameid.substring(3);
                $.each(selectedObjectArray, function (i, n) {
                    if (n.matID == matId) {
                        selectedObjectArray.pop(n);
                    }
                });
            }
            else {
                $(obj).addClass("mui-active");
                $(obj).addClass("onthselected");
                //$("li#" + nameid + "").addClass("picked");


                var num = $("span.mui-badge").text();
                num++;
                $("span.mui-badge").text(num);


                //获取领料数量
                var inputNum = $("li#" + nameid + "").find("input").val();
                //获取领料数量

                var innerhtml = '';
                innerhtml += '<li class="mui-table-view-cell picked' + nameid + '" id="picked' + nameid + '">'
                innerhtml += $("li#" + nameid + "").html();
                innerhtml += '</li>'

                $("#blist").append(innerhtml);
                $(obj).removeClass("onthselected");
                $(".onthselected").attr("onclick", "btn_Thpickswitch(this)");

                //设置领料数量

                $("li#picked" + nameid).find("input").val(inputNum);

                //设置领料数量

                var matId = nameid.substring(3);
                //alert(matId);
                $.each(objectArray, function (i, n) {
                    if (n.matID == matId) {
                        selectedObjectArray.push(n);
                    }
                });
            }
        }

        $("#clear").click(function () {
            if (confirm("删除全部物料？")) {
                $("#blist li").remove();
                $("span.mui-badge").text("0");
                mui('#middlePopover').popover('hide');
                var btn = document.getElementById("queryBtn");
                $("#list1").html('');

                selectedObjectArray = new Array();

                mui.trigger(btn, 'tap');

                
                //location.reload();
                // $(".pick-submit-clear").hide();
                // $("#middlePopover").removeClass("mui-active");
                // $("#middlePopoverhref").addClass("mui-active");
                // $(".mui-backdrop").remove();
            }
        });

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


        $("#submit").click(function () {

            strOutStock = mui("#outStock")[0].innerText;
            if (strOutStock == "请选择...") {
                strOutStock = "";
                strOutStockCode = "";
            }
            strInStock = mui("#inStock")[0].innerText;
            if (strInStock == "请选择...") {
                strInStock = "";
                strInStockCode = "";
            }

            if (strOutStock.length == 0) {
                mui.toast("未选择调出库房，请选择！");
                return
            }
            strOutStockCode = mui("#outStockCode")[0].innerText;
            if (strOutStockCode.length == 0) {
                mui.toast("未获取调出库房代码，请重新选择！");
                return
            }

            strInStockCode = mui("#inStockCode")[0].innerText;
            if (strInStockCode.length == 0) {
                mui.toast("未获取调入库房代码，请重新选择！");
                return
            }

            if ($("li.picked").length > 0) {
                if (confirm("确认提交？")) {
                    //showWait();
                    postData();

                }
            }
            else {

                alert("无领料信息，请返回继续添加！")
            }

        });


        function postData() {


            var couldPost = true;

            var strOneJson = "";

            strOneJson += "[";
            var oneNum = 0;

            var tmp = "";
            $("li.picked").each(function () {
                //alert($(this).html());
                //alert($(this).attr("id"));

                tmp = $(this).attr("id");

                $(this).find("span").addClass("on");
                $(this).find("input").addClass("on");

                oneNum += 1;
                strOneJson += "{\"MStock\":\"" + $("span.stock" + tmp + ".on").text() + "\",";
                strOneJson += "\"MStockCode\":\"" + $("span.stockcode" + tmp + ".on").text() + "\",";
                strOneJson += "\"MFactory\":\"" + $("span.fac" + tmp + ".on").text() + "\",";
                strOneJson += "\"MFactoryCode\":\"" + $("span.faccode" + tmp + ".on").text() + "\",";
                strOneJson += "\"MGroup\":\"" + strtojs($("span.group" + tmp + ".on").text()) + "\",";
                strOneJson += "\"MGroupCode\":\"" + $("span.groupcode" + tmp + ".on").text() + "\",";
                strOneJson += "\"Material\":\"" + strtojs($("span.wlmc" + tmp + ".on").text()) + "\",";
                strOneJson += "\"MCode\":\"" + $("span.code" + tmp + ".on").text() + "\",";
                strOneJson += "\"MBatch\":\"" + $("span.no" + tmp + ".on").text() + "\",";
                strOneJson += "\"MUnit\":\"" + $("span.unit" + tmp + ".on").text() + "\",";
                strOneJson += "\"MInventory\":\"" + $("span.num" + tmp + ".on").text() + "\",";
                strOneJson += "\"PickInventory\":\"" + $("input.pnum" + tmp + ".on").val() + "\",";
                strOneJson += "\"PDStat\":\"1\"},";

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

            })


            if (!couldPost) {
                return;
            }

            strOneJson = strOneJson.substr(0, strOneJson.length - 1);
            strOneJson += "]";


            console.log(strOneJson);

            if (oneNum == 0) {
                strOneJson = "";
            }
            //strJson += "]}";
            //ajax后台数据处理

            $.post("../api/KmrStorage/Post_AllotSubmit_BL.aspx",
                { PCompany: _company, PDept: _dept, PVenture: _venture, SapVenId: _venSapId, PName: _name, PCode: _cardId, PCostCode: _costCode, InStock: strInStock, InStockCode: strInStockCode, OutStock: strOutStock, OutStockCode: strOutStockCode, dataListOne: strOneJson, OneNum: oneNum },
                function (data, status) {
                    var js = JSON.parse(data);
                    //var jskeeper = JSON.parse(data).ddInfo;                   
                    hiddWait();
                    toastInfo(js.info);
                    //mui.toast(js.info, { duration: 'long', type: 'div' })
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


        //照相图标添加点击事件

        dd.ready(function () {

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


            upImgFromCameras = function (callback) {
                dd.biz.util.uploadImageFromCamera({
                    compression: true,//(是否压缩，默认为true)
                    quality: 50, // 图片压缩质量, 
                    resize: 50, // 图片缩放率
                    stickers: {   // 水印信息

                    },
                    onSuccess: function (result) {
                        callback(result)
                    },
                    onFail: function (err) { }
                });
            }

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

        })


        mui(".deshow").on('tap', '.photo', function () {
            var strDeid = $(this).attr("name");
            //alert(strDeid);
            upImgFromCameras(function (re) {
                $("span.photourl" + strDeid + "").text(re[0]);
                //alert($("span.photourl" + strDeid + "").text());
                preImages(re[0]);
                //alert(re[0]);

            })
            $(this).toggleClass("icon_fix");//照相图片变红
            //alert($(this).hasClass("icon_fix"));
        })

      

        //页面初始化js
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
        
        //页面初始化js

        //初始化查询
        //mui.ready(function () {
        //    //mui.trigger(btn, 'tap');
        //    var btn = document.getElementById("queryBtn");
        //    mui.trigger(btn, 'tap');
        //});
    </script>

    </body>
</html>