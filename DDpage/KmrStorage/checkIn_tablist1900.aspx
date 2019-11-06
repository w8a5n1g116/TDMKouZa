<%@ Page Language="C#"  AutoEventWireup="true" CodeBehind="checkIn_tablist1900.aspx.cs" Inherits="DDpage.KmrStorage.checkIn_tablist1900" %>



<!DOCTYPE html>

<html>
<head>
    <meta charset="utf-8">
    <title>掌上仓储-南车扣杂验收入库</title>
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
         color: #aab2bd;
         /* placeholder字体大小  */
         font-size: 14px;
         /* placeholder位置  */
         text-align: left;
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
        .mui-switch-blue.mui-active {
                border: 2px solid #dd524d;
                background-color: #dd524d;
        }
        .mui-bar-tab .mui-tab-item.mui-active {
                color: #dd524d;
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
            .dateOne {
                 font-size: 1em;
                color:#808080;    
            }
            .dateTwo {
                 font-size: 1em;
                color:#808080;    
            }


            input {
    font-family: 'Helvetica Neue',Helvetica,sans-serif;
    font-size: 12px;
    -webkit-tap-highlight-color: transparent;
    -webkit-tap-highlight-color: transparent;
}
                        /*.mui-icon-extra
{
    font-size: 16px  !important;
}*/

    </style>
    <!--<header class="mui-bar mui-bar-nav">
        <a class="mui-action-back mui-icon mui-icon-left-nav mui-pull-left"></a>
        <h1 class="mui-title">顶部选项卡-可左右拖动(div)</h1>
    </header>-->
    <div class="mui-content">
        <div class="mui-content-padded" style="margin: 5px;">
        <form class="mui-input-group font_fix">
					<div class="mui-input-row">
						<label>*采购订单</label>
						<input type="text" class="mui-input-clear font_fix" placeholder="请输入订单号" id="OddNo" style="width:55%">
                        <span class="mui-icon-extra mui-icon-extra-sweep fodd"></span>
					</div>
                    <div class="mui-input-row">
						<label>*内向交货单</label>
						<input type="text" class="mui-input-clear font_fix" placeholder="请输入内向交货号" id="InNo" style="width:55%">
                        <span class="mui-icon-extra mui-icon-extra-sweep sodd"></span>
					</div>
                    <div class="mui-input-row">
						<label>供应商名称</label>
						<input type="text" class="mui-input-clear font_fix" placeholder="请输入供应商" id="Supply">
					</div>
                    <div class="mui-input-row">
                        <label>车牌</label>
                        <div id="showCarNo">
                            <span id="CarNo">请选择</span>
                        </div>                      
                    </div>
                    <div class="mui-input-row">
                        <label>手机号</label>
                        <div id="showPhone">
                            <span id="Phone"></span>
                        </div>                      
                    </div>
                    <div class="mui-input-row">
						<label>收料单位</label>
						<div id="showFac">
                            <span id="PFac" class="font_fix2">宁夏共享商务有限公司</span>
                        </div>  
					</div>
                    <div class="mui-input-row">
						<label>入库库房</label>
						<div id="showStock">
                            <span id="PStock" class="font_fix2">请选择</span>
                        </div>  
					</div>
                    <div class="mui-input-row">
						<label>物料名称</label>
						<input type="text" class="mui-input-clear font_fix" placeholder="请输入物料名称" id="matName" style="width:55%">
                        <span class="mui-icon-extra mui-icon-extra-sweep spanScanName"></span>
					</div>
                    
                    <div class="mui-input-row mui-hidden">
						<label>扣杂系数</label>
						<input type="text" class="mui-input-clear font_fix" placeholder="请输扣杂系数" id="KouZa">
					</div>
                    
            <div class="mui-button-row" id="query">
						<button type="button" class="mui-btn mui-btn-danger" id="queryBtn">查询</button>
					</div>
        </form>
            </div>

         

        <div id="slider" class="mui-slider" >
            <div id="sliderSegmentedControl" class="mui-slider-indicator mui-segmented-control mui-segmented-control-inverted mui-segmented-control-negative">
                <a class="mui-control-item" href="#item1mobile">
                    扣杂提交
                </a>
                <a class="mui-control-item" href="#item2mobile">
                    提交查询
                </a>
            </div>
            <div id="sliderProgressBar" class="mui-slider-progress-bar mui-col-xs-6" style="color:red"></div>
            
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
            </div>
        </div>

    </div>

    


    <nav class="mui-bar mui-bar-tab" id="nav-submit">
        <a class="mui-tab-item mui-active checkInSubmit"  id="submit">
            <span class="mui-icon iconfont icon-icon_jiarulingliaoche"><span class="mui-badge" >0</span></span>
            <span class="mui-tab-label">提交</span>
        </a>
    </nav>
    <script src="../js/jquery-1.11.0.min.js"></script>
    <script src="../js/dingtalk.js"></script>
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
            jsApiList: ['biz.user.get', 'device.geolocation.get', 'biz.map.locate', 'biz.util.uploadImageFromCamera', 'biz.util.uploadImage', 'dd.biz.ding.post']
        });

        mui.init({
            swipeBack: false
        });


        var CarNoList = new Array();
        var StdNo = '';
        var F_Net = 0;
        var F_Gross = 0;
        var F_Tare = 0;

        var _phone = '<%=strPhone%>';
        var _company = "";
        var _dept = "";
        var _name = "";
        var _venture = "";
        var _unit = "";
        var _cardId = "";
        var _facName = "商务-铸造工厂";
        var _facCode = "1690";
        var _stockName = "";
        var _stockCode = "";
        var strJson = "";
        var ajaxUrl = "";
        var dataFac = Array();
        var dataStock = Array();
        var arrayFac = Array();     
        var arrayStock = Array();
        var arrayCharge = Array();
        
        var showWait;
        var hiddWait;
        var toastInfo;
        var OddScan;
        var InScan;
        var MatIdScan;
        var _tmpId = "";
        var _strId = "";
        _listOneStat = 0;
        _listTwoStat = 0;


        var JsonUserInfo = '<%=user_info%>';

        var strArray = new Array();
        strArray = JsonUserInfo.split(",")
        _cardId = strArray[0];
        _company = '共享商务';
        _dept = strArray[2];
        _name = strArray[3];
        _venture = strArray[4];
        _unit = strArray[5];

        (function ($, doc) {


            var showCarNoBtn = doc.getElementById('showCarNo');

            showCarNoBtn.addEventListener('tap', function () {
                jQuery.post("../api/KmrStorage/Get1900CarCode.aspx", { StdNo: StdNo }, function (data) {
                    var js = JSON.parse(data).data;

                    CarNoList = new Array();

                    for (var i = 0; i < js.length; i++) {
                        CarNoList.push({ value: js[i].F_StdNo, text: js[i].F_CarNo, phone: js[i].F_YlStr04});

                    }

                    var CarNopicker = new mui.PopPicker();
                    CarNopicker.setData(CarNoList);
                    CarNopicker.show(function (SelectedItem) {
                        console.log(SelectedItem);
                        var ResultCarNo = doc.getElementById('CarNo');
                        ResultCarNo.innerText = SelectedItem[0].text;
                        var ResultPhone = doc.getElementById('Phone');
                        ResultPhone.innerText = (SelectedItem[0].phone == undefined ? "" : SelectedItem[0].phone );
                        StdNo = SelectedItem[0].value;

                        jQuery.post("../api/KmrStorage/Get1900CarLots.aspx", { StdNo: StdNo }, function (data) {
                            var js = JSON.parse(data).data;
                            F_Net = js[0].F_Net;
                            F_Gross = js[0].F_Gross;
                            F_Tare = js[0].F_Tare;
                        });
                    })
                }).error(function () { alert("地磅服务器未开机！"); });                            
                
            }, false);

            
            getStockList();
            getSList();

            function getStockList() {

                ajaxUrl = "../api/KmrStorage/GetCheckInFacStock.aspx";
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
                            dataStock.push({ value: js[i].stockCode, text: js[i].stockName });
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


        (function ($) {
            $('.mui-scroll-wrapper').scroll({
                indicators: true //是否显示滚动条
            });
        })(mui);

        //查询按钮loading处理,查询数据
        var strOrder = "";
        var strInOrder = "";
        var strSupply = "";
        var strMatName = "";
        var strStock = "";

        mui(".mui-input-row").on('tap', '.fodd', function () {
            OddScan();
        });

        mui(".mui-input-row").on('tap', '.sodd', function () {
            InScan();
        });

        mui(".mui-input-row").on('tap', '.spanScanName', function () {
            MatIdScan();
        });
        

        mui("#query").on('tap', '.mui-btn', function () {

            _listOneStat = 0;
            _listTwoStat = 0;
            mui(this).button('loading');
            //查询物料信息

            strOrder = mui("#OddNo")[0].value;
            strInOrder = mui("#InNo")[0].value;
            strSupply = mui("#Supply")[0].value;
            strMatName = mui("#matName")[0].value;
            
            //strStock = mui("#PickStock")[0].innerText;
            //if (strStock == "请选择...") {
            //    strStock = "";
            //}
            getFList(this);
            getSList(this);

            //后台获取查询物料信息


        });

        //查询按钮还原
        function resetBtn() {
            if ( _listOneStat == 1 && _listTwoStat == 1) {

                mui("#queryBtn").button('reset');
            }

        }
        //查询按钮还原


        //查询按钮loading处理,查询数据
        //采购订单查询函数
        function getFList(ob) {
            var fragment = document.createDocumentFragment();
            ajaxUrl = "../api/KmrStorage/GetPurchaseInfo.aspx";
            mui.ajax(ajaxUrl, {
                data: { Company: _company, Dept: _dept, Ven: _venture, Name: _name, Order: strOrder, Supply: strSupply, matName: strMatName, InOrder: strInOrder, facCode: _facCode, facName: _facName, stockName: _stockName, stockCode: _stockCode},
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
                        //$("span.mui-badge").text(js.length);
                        for (var i = 0; i < js.length; i++) {
                            var strId = js[i].purchaseOrder + js[i].matCode;
                            strHtml = "";
                            strHtml += "<div class=\"list_text\">";
                            strHtml += "<div class=\"list_info\"><div>采购单号：<span class=\"orderId" + strId + "\">" + js[i].purchaseOrder + "</span></div><div class=\"mui-switch mui-switch-blue   " + strId + "\" name=\"" + strId + "\"  onclick=\"btn_switch(this)\"><div class=\"mui-switch-handle\"></div></div></div>";
                            strHtml += "<div class=\"list_info\"><div>物料：<span class=\"matName" + strId + "\">" + strtojs(js[i].matName) + "</span><span class=\"itemOrder" + strId + "\" style=\"display:none\">" + js[i].itemOrder + "</span><span class=\"unit" + strId + "\" style=\"display:none\">" + js[i].matUnit + "</span></div></div>";
                            strHtml += "<div class=\"list_info\"><div>供应商：<span class=\"supplyName" + strId + "\">" + js[i].supplyName + "</span></div></div>";
                            strHtml += "<div class=\"list_info mui-hidden\"><div>已入数量：<span  class=\"checkNum" + strId + "\">" + js[i].checkNum + "</span><span style=\"display:none\" class=\"matCode" + strId + "\">" + js[i].matCode + "</span></div><div>申请日期：<span class=\"adate" + strId + "\">" + js[i].pDate + "</span></div></div>";
                            strHtml += "<div class=\"list_info\" style=\"display:none\"><div>供应商：<span class=\"supplyName" + strId + "\">" + js[i].supplyName + "</span><span class=\"supplyCode" + strId + "\" style=\"display:none\">" + js[i].supplyCode + "</span></div></div>";
                            strHtml += "<div class=\"list_info\" style=\"display:none\"><div>采购公司：<span class=\"companyName" + strId + "\">" + js[i].companyName + "</span><span class=\"companyShortName" + strId + "\">" + js[i].companyShortName + "</span><span class=\"companyCode" + strId + "\"  style=\"display:none\">" + js[i].companyCode + "</span></div></div>";
                            strHtml += "<div class=\"list_info\"><div>净重：<span class=\"jingzhong" + strId + "\">" + (F_Gross - F_Tare)  + "</span></div></div>";
                            //strHtml += "<div class=\"list_info\"><div>批号：<input type=\"text\" style=\"width: 10em; height: 1em; margin: 0; font - size:0.5em\" name=\"pcharge" + strId + "\" id=\"pcharge" + strId + "\"  class=\"pcharge" + strId + "\"  value=\"0\"><span class=\"mui-icon mui-icon-plus ChargeList\" id=\"showCharge" + strId + "\" onclick=\"purchaseChargeList('" + js[i].purchaseOrder + "','" + js[i].matCode + "','" + strId + "')\"></span></div></div>";
                            strHtml += "<div class=\"list_info\"><div>毛重：<span class=\"pmz" + strId + "\"  id=\"pmz" + strId + "\"  \">" + F_Gross + "</span></div><div>皮重：<span class=\"ppz" + strId + "\"  id=\"ppz" + strId + "\"  \">" + F_Tare + "</span></div></div>"
                            strHtml += "<div class=\"list_info\"><div>扣杂：<input type=\"text\" style=\"width: 10em; height: 1em; margin: 0; font - size:0.5em\" name=\"pkz" + strId + "\" id=\"pkz" + strId + "\"  class=\"pkz" + strId + "\"  value=\"\"></div><div>扣包装：<input type=\"text\" style=\"width: 10em; height: 1em; margin: 0; font - size:0.5em\" name=\"pkbz" + strId + "\" id=\"pkbz" + strId + "\"  class=\"pkbz" + strId + "\"  value=\"0\"></div></div>"
                            strHtml += "<div class=\"list_info mui-hidden\"><div name=\"" + strId + "\">入库工厂：<span class=\"facName" + strId + " facListName\"  id=\"facName" + strId + "\">" + _facName + "</span><span class=\"facCode" + strId + " facListCode\" style=\"display:none\" id=\"facCode" + strId + "\">" + _facCode + "</span></div><div></div><div onclick=\"stockPickList(this)\" name=\"" + strId + "\">入库库房：<span class=\"stockName" + strId + " stockListName\"  id=\"stockName" + strId + "\" >" + _stockName + "</span><span class=\"stockCode" + strId + " stockListCode\" style=\"display:none\" id=\"stockCode" + strId + "\">" + _stockCode + "</span></div></div>";
                          
                            strHtml += "</div>";
                            li = document.createElement('li');
                            li.id = strId;
                            li.title = "";
                            li.className = 'mui-table-view-cell listOne ';
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


        //查询函数
        function getSList() {
            var fragment = document.createDocumentFragment();
            ajaxUrl = "../api/KmrStorage/GetCheckInList.aspx";
            mui.ajax(ajaxUrl, {
                data: { Company: _company, Dept: _dept, Ven: _venture, Name: _name, Order: strOrder, Supply: strSupply, matName: strMatName, StName: strStock },
                async: true,
                crossDomain: true,
                type: 'post',//HTTP请求类型
                success: function (data) {
                    //服务器返回响应，根据响应结果，分析是否登录成功；
                    
                    var js = JSON.parse(data).data;

                   
                    if (js.length > 0) {
                        var li;

                        var strHtml;

                        for (var i = 0; i < js.length; i++) {
                            strHtml = "";
                            strHtml += "<div class=\"list_text\">";
                            strHtml += "<div class=\"list_info\"><div>采购单号：<span >" + js[i].purchaseOrder + "</span></div></div>";
                            strHtml += "<div class=\"list_info\"><div>物料：<span >" + strtojs(js[i].matName) + "</span></div></div>";
                            strHtml += "<div class=\"list_info\"><div>代码：<span >" + js[i].matCode + "</span></div></div>";
                            strHtml += "<div class=\"list_info\"><div>供应商：<span >" + js[i].supplyName + "</span></div></div>";
                            strHtml += "<div class=\"list_info\"><div>采购公司：<span >" + js[i].companyName + "</span></div></div>";
                            strHtml += "<div class=\"list_info\"><div>订单数量：<span >" + js[i].purchaseNum + "</span></div></div>";
                            strHtml += "<div class=\"list_info\"><div>毛重：<span >" + (js[i].maozhong === undefined || js[i].maozhong === null ? js[i].maozhong : 0) + "</span></div></div>";
                            strHtml += "<div class=\"list_info\"><div>皮重：<span >" + (js[i].pizhong === undefined || js[i].pizhong === null ? js[i].pizhong : 0) + "</span></div></div>";
                            strHtml += "<div class=\"list_info\"><div>扣杂：<span >" + (js[i].kouza === undefined || js[i].kouza === null ? js[i].kouza : 0)+ "</span></div></div>";
                            strHtml += "<div class=\"list_info\"><div>扣包装：<span >" + (js[i].koubaozhuang === undefined || js[i].koubaozhuang === null ? js[i].koubaozhuang : 0)+ "</span></div></div>";
                            strHtml += "<div class=\"list_info\"><div>净重：<span >" + ((js[i].maozhong === undefined || js[i].maozhong === null ? js[i].maozhong : 0) - (js[i].pizhong === undefined || js[i].pizhong === null ? js[i].pizhong : 0))  + "</span></div></div>";
                            strHtml += "<div class=\"list_info\"><div>入库工厂：<span >" + js[i].facName + "</span></div><div >入库库房：<span>" + js[i].stockName + "</span></div></div>";
                            strHtml += "<div class=\"list_info\"><div>入库数量：<span>" + (js[i].checkNum === undefined || js[i].checkNum === null ? js[i].checkNum : 0) + "</span></div></div>";
                            
                            strHtml += "</div>";
                            li = document.createElement('li');
                            li.id = "";
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

                        _listTwoStat = 1;
                        resetBtn();
                    }
                    else {
                        _listTwoStat = 1;
                        resetBtn();
                    }

                   
                },
                error: function (xhr, type, errorThrown) {
                    //异常处理；
                    alert(type);
                    console.log(type);
                }
            });
        }
     


        //工厂,库房点击函数

        ajaxUrl = "../api/KmrStorage/GetCheckInFac.aspx";
        mui.ajax(ajaxUrl, {
            data: { comName: _company },
            async: false,
            crossDomain: true,
            //dataType: 'json',//服务器返回json格式数据
            type: 'post',//HTTP请求类型
            //timeout: 10000,//超时时间设置为10秒；
            // headers: { 'Content-Type': 'text/json' },
            success: function (data) {
                //服务器返回响应，根据响应结果，分析是否登录成功；
                var js = JSON.parse(data).data;
                arrayFac.splice(0, dataStock.length);
                for (var i = 0; i < js.length; i++) {
                    arrayFac.push({ value: js[i].facCode, text: js[i].facName });
                }

            },
            error: function (xhr, type, errorThrown) {
                //异常处理；
                alert(errorThrown);
                console.log(type);
            }
        });


        ajaxUrl = "../api/KmrStorage/GetCheckInStock.aspx";
        mui.ajax(ajaxUrl, {
            data: { comName: _company },
            async: false,
            crossDomain: true,
            //dataType: 'json',//服务器返回json格式数据
            type: 'post',//HTTP请求类型
            //timeout: 10000,//超时时间设置为10秒；
            // headers: { 'Content-Type': 'text/json' },
            success: function (data) {
                //服务器返回响应，根据响应结果，分析是否登录成功；
                var js = JSON.parse(data).data;
                arrayStock.splice(0, dataStock.length);
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

        function facPickList(ob) {
            var strid = $(ob).attr("name");
            //alert(ob.className);
            var dataPicekerFac = new mui.PopPicker();
            dataPicekerFac.setData(arrayFac);

            var ResultName = document.getElementById("facName" + strid + "");
            var ResultCode = document.getElementById("facCode" + strid + "");

            dataPicekerFac.show(function (items) {
                ResultName.innerText = items[0].text;
                ResultCode.innerText = items[0].value;
            }
            )


        }

        function purchaseChargeList(purchaseOrder,matCode,strId) {

            var strOrder = purchaseOrder;
            var strMatCode = matCode;

            ajaxUrl = "../api/KmrStorage/GetChargeList.aspx";
            mui.ajax(ajaxUrl, {
                data: { strOrder: strOrder, strMatCode: strMatCode },
                async: false,
                crossDomain: true,
                //dataType: 'json',//服务器返回json格式数据
                type: 'post',//HTTP请求类型
                //timeout: 10000,//超时时间设置为10秒；
                // headers: { 'Content-Type': 'text/json' },
                success: function (data) {
                    //服务器返回响应，根据响应结果，分析是否登录成功；
                    var js = JSON.parse(data).data;
                    console.log(js);
                    arrayCharge.splice(0, arrayCharge.length);
                    for (var i = 0; i < js.length; i++) {
                        arrayCharge.push({ value: js[i].charg, text: js[i].charg });
                    }

                },
                error: function (xhr, type, errorThrown) {
                    //异常处理；
                    alert(errorThrown);
                    console.log(type);
                }
            });

            //var showDataPicekerButtonCharge = document.getElementById("showCharge" + strId + "");

            //showDataPicekerButtonCharge.addEventListener('tap', popPickerCharge, false);
            //_strId = strId

            var dataPicekerChargeNo = new mui.PopPicker();
            dataPicekerChargeNo.setData(arrayCharge);
            //var showDataPicekerButton02 = doc.getElementById('showTypeTwo');
            var ResultCharge = document.getElementById("pcharge" + strId + "");

            dataPicekerChargeNo.show(function (items) {
                if (arrayCharge.length > 0) {
                    ResultCharge.value = items[0].value;
                }

            }
            )

        }


        

        function stockPickList(ob) {
            var strid = $(ob).attr("name");
            //alert(ob.className);
            var dataPicekerStock = new mui.PopPicker();
            dataPicekerStock.setData(arrayStock);
            

            var ResultName = document.getElementById("stockName" + strid + "");
            var ResultCode = document.getElementById("stockCode" + strid + "");

            dataPicekerStock.show(function (items) {
                ResultName.innerText = items[0].text;
                ResultCode.innerText = items[0].value;
            }
            )

        }

        
       

        //页面初始化js

        //数据提交
        mui("#nav-submit").on("tap", ".checkInSubmit", function () {
            if ($("li.listOne.picked").length > 0) {
                if (confirm("确认提交？")) {
                    showWait();
                }
            }
            else {

                alert("无验收入库信息，点击取消返回继续修改！")
            }
        });

        //数据处理函数
        function postData() {

            var carno = $('#CarNo').html();
            var carphone = $('#Phone').html();

            var strJson = "";
            strJson += "[";
            var tmp = "";
            $("li.picked").each(function () {                      
                tmp = $(this).attr("id");
                       

                strJson += "{\"purOrder\":\"" + $("span.orderId" + tmp + "").text() + "\",";
                strJson += "\"itemOrder\":\"" + $("span.itemOrder" + tmp + "").text() + "\",";
                strJson += "\"matName\":\"" + $("span.matName" + tmp + "").text() + "\",";
                strJson += "\"matCode\":\"" + $("span.matCode" + tmp + "").text() + "\",";
                strJson += "\"matUnit\":\"" + $("span.unit" + tmp + "").text() + "\",";
                strJson += "\"supplyName\":\"" + $("span.supplyName" + tmp + "").text() + "\",";
                strJson += "\"supplyCode\":\"" + $("span.supplyCode" + tmp + "").text() + "\",";
                strJson += "\"companyName\":\"" + $("span.companyName" + tmp + "").text() + "\",";
                strJson += "\"companyShortName\":\"" + $("span.companyShortName" + tmp + "").text() + "\",";
                strJson += "\"companyCode\":\"" + $("span.companyCode" + tmp + "").text() + "\",";
                strJson += "\"purNum\":\"" + $("span.purNum" + tmp + "").text() + "\",";
                strJson += "\"adate\":\"" + $("span.adate" + tmp + "").text() + "\",";
                strJson += "\"pdate\":\"" + $("span.pdate" + tmp + "").text() + "\",";
                strJson += "\"facName\":\"" + $("span.facName" + tmp + "").text() + "\",";
                strJson += "\"facCode\":\"" + $("span.facCode" + tmp + "").text() + "\",";
                strJson += "\"stockName\":\"" + $("span.stockName" + tmp + "").text() + "\",";
                strJson += "\"stockCode\":\"" + $("span.stockCode" + tmp + "").text() + "\",";
                strJson += "\"checkNum\":\"" + $("span.checkNum" + tmp + "").text() + "\",";
                strJson += "\"pnum\":\"" + $("input.pnum" + tmp + "").val() + "\",";
                strJson += "\"pmz\":\"" + $("input.pmz" + tmp + "").val() + "\",";
                strJson += "\"ppz\":\"" + $("input.ppz" + tmp + "").val() + "\",";
                //strJson += "\"pjz\":\"" + $("input.pjz" + tmp + "").val() + "\",";
                strJson += "\"pkz\":\"" + $("input.pkz" + tmp + "").val() + "\",";
                strJson += "\"pkbz\":\"" + $("input.pkbz" + tmp + "").val() + "\",";
                strJson += "\"stat\":\"0\"},";
            })
            strJson = strJson

            strJson = strJson.substr(0, strJson.length - 1);
            strJson += "]";
            //strJson += "]}";
            //ajax后台数据处理
            //alert(strJson);
            console.log(strJson);
                    //alert(strJson);

            $.post("../api/KmrStorage/checkInList_toDiBang1900.aspx",
                { PCompany: _company, PDept: _dept, PVenture: _venture, PName: _name, PCode: _cardId, PTime: getNowFormatDate(), CarNo: carno, CarPhone: carphone, StdNo: StdNo,dataList: strJson },
            function (data, status) {
                hiddWait();
                toastInfo(data);
                           
                //数据提交后页面处理
                //$("#stockList li").remove();
                //mui('#middlePopover').popover('hide');
                //$("span.mui-badge").text("0");
                            
                //location.reload();
                //数据提交后页面处理
            });
        }


        //数据处理函数
        //数据提交


        //按钮动态变化
        function btn_switch(obj) {
            var nameid = $(obj).attr("name");
            if ($(obj).hasClass('mui-active')) {
                $(obj).removeClass("mui-active");
                var num = $("span.mui-badge").text()
                num--;
                $("span.mui-badge").text(num);
                $("li#" + nameid + "").removeClass("picked");
            }
            else {
                $(obj).addClass("mui-active");
                var num = $("span.mui-badge").text();
                num++;
                $("span.mui-badge").text(num);
                $("li#" + nameid + "").addClass("picked");
            }

        }

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
            var currentdate = year + seperator1 + month + seperator1 + strDate;
               
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


            OddScan = function () {

                dd.biz.util.scan({
                    type: "all", // type 为 all、qrCode、barCode，默认是all。
                    tips: "请对准条形码/二维码",  //进入扫码页面显示的自定义文案
                    onSuccess: function (data) {
                        //onSuccess将在扫码成功之后回调
                        /* data结构
                          { 'text': String}
                        */
                        $("#OddNo").val(data.text);
                    },
                    onFail: function (err) {
                    }
                })
            }

            InScan = function () {
                dd.biz.util.scan({
                    type: "all", // type 为 all、qrCode、barCode，默认是all。
                    tips: "请对准条形码/二维码",  //进入扫码页面显示的自定义文案
                    onSuccess: function (data) {
                        //onSuccess将在扫码成功之后回调
                        /* data结构
                          { 'text': String}
                        */
                        $("#InNo").val(data.text);
                        
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
                        var js = data.text;
                        var js = js.replace(/\\/g, '\\\\');
                        var jsObj = JSON.parse(js);
                        $("#matName").val(jsObj[0].ID);

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