<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="query_declarePlanList.aspx.cs" Inherits="DDpage.KmrStorage.query_declarePlanList" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8">
    <title>申报计划查询</title>
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
						<label>起始日期</label>
						<span id="firDate" class="dateOne">点击选择日期</span>
					</div>		
                    <div class="mui-input-row">
						<label>结束日期</label>
						<span id="secDate" class="dateTwo">点击选择日期</span>
					</div>
					<div class="mui-input-row">
						<label >公司</label>
                        <div id="showCompany" class="div_fix">
                            <span id="queryCompany" class="font_fix2">请选择...</span>
                        </div>
					</div>
                     <div class="mui-input-row">
						<label>工厂</label>
						<div id="showFac" class="div_fix">
                             <span id="queryFac" class="font_fix2">请选择...</span>
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
                    计划明细
                </a>
            </div>
            <div id="sliderProgressBar" class="mui-slider-progress-bar mui-col-xs-12"></div>
            
            <div class="mui-slider-group">
                 
                <div id="item1mobile" class="mui-slider-item mui-control-content mui-active" style="height:35em">    
                   
                    <div id="scroll1" class="mui-scroll-wrapper">
                        <div class="mui-scroll">                     
                            <ul class="mui-table-view mui-table-view-striped mui-table-view-condensed" id="list1" >
                                
                            </ul>
                        </div>
                    </div>
                </div>
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

    


    <%--<nav class="mui-bar mui-bar-tab">
        <a class="mui-tab-item mui-active" href="#middlePopover" id="middlePopoverhref">
            <span class="mui-icon iconfont icon-icon_jiarulingliaoche"><span class="mui-badge" >0</span></span>
            <span class="mui-tab-label">已选</span>
        </a>
    </nav>--%>
    <script src="../js/jquery-1.11.0.min.js"></script>
    <script src="../js/dingtalk.js"></script>
    <%--<script src="../js/ddjs.js"></script>--%>
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
        var userinfo = "";
        var OpenLink;
        var preImages;
        var upImgFromCameras;

        dd.config({
            appId: _config.appId,
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
        var _matCode = "";
        var strJson = "";
        var ajaxUrl = "";
        var arrayCompany = Array();
        var arrayFac = Array();
        var arrayStock = Array();
        var showLimsWait;
        var hiddWait;
        var OpenLinkOne;
        var OpenLinkTwo;
        var queryWait;

        var JsonUserInfo = '<%=user_info%>';

        var strArray = new Array();
        strArray = JsonUserInfo.split(",")
        _cardId = strArray[0];
        _company = strArray[1];
        _dept = strArray[2];
        _name = strArray[3];
        _venture = strArray[4];
        _unit = strArray[5];






        (function ($) {
            $('.mui-scroll-wrapper').scroll({
                indicators: true //是否显示滚动条
            });
            //mui("#PCompany")[0].innerHTML = _company;
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

                getCompanyList();
                getFacList(_company);
                //getStockList("");

                function getCompanyList() {

                    ajaxUrl = "../api/KmrStorage/GetCompany.aspx";
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
                            arrayCompany.splice(0, arrayCompany.length);
                            for (var i = 0; i < js.length; i++) {
                                arrayCompany.push({ value: js[i].com, text: js[i].com });
                            }

                        },
                        error: function (xhr, type, errorThrown) {
                            //异常处理；
                            alert(errorThrown);
                            console.log(type);
                        }
                    });

                    var showDataPicekerButtonCom = doc.getElementById('showCompany');

                    showDataPicekerButtonCom.addEventListener('tap', popPickerCom, false);
                }

                function popPickerCom() {

                    //更新下拉菜单数据
                    var dataPicekerCom = new $.PopPicker();
                    dataPicekerCom.setData(arrayCompany);
                    //var showDataPicekerButton02 = doc.getElementById('showTypeTwo');
                    var ResultCom = doc.getElementById('queryCompany');

                    dataPicekerCom.show(function (items) {
                        ResultCom.innerText = items[0].text;
                        getFacList(items[0].text);
                    }
                    )
                }


                function getFacList(ob) {

                    var strCom = ob;
                    if (strCom == "" || strCom == "请选择...") {
                        strCom = "";
                    }

                    ajaxUrl = "../api/KmrStorage/GetQueryFac.aspx";
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
                            arrayFac.splice(0, arrayFac.length);
                            for (var i = 0; i < js.length; i++) {
                                arrayFac.push({ value: js[i].fac, text: js[i].fac });
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
                    dataPicekerFac.setData(arrayFac);
                    //var showDataPicekerButton02 = doc.getElementById('showTypeTwo');
                    var ResultFac = doc.getElementById('queryFac');

                    dataPicekerFac.show(function (items) {
                        ResultFac.innerText = items[0].text;
                    }
                    )
                }

            });

        })(mui, document);
        //发料仓库下拉列表


        //查询按钮loading处理,查询数据
        var strCompany = "";
        var strFac = "";
        var strSDate = "";
        var strEDate = "";
        var strMatName = "";
        mui("#queryCompany")[0].innerText = _company;


        mui("#query").on('tap', '.mui-btn', function () {
            
            //查询物料信息
            strCompany = mui("#queryCompany")[0].innerText;
            if (strCompany == "请选择...") {
                strCompany = "";
            }
            strFac = mui("#queryFac")[0].innerText;
            if (strFac == "请选择...") {
                strFac = "";
            }

            strSDate = mui("#firDate")[0].innerText;
            if (strSDate == "点击选择日期") {
                strSDate = "";
            }

            strEDate = mui("#secDate")[0].innerText;
            if (strEDate == "点击选择日期") {
                strEDate = "";
            }

            strMatName = mui("#matName")[0].value;


            queryWait();

            //getQueryList();

            //后台获取查询物料信息
        });
        //查询按钮loading处理,查询数据
        function getQueryList() {
            var fragment = document.createDocumentFragment();
            ajaxUrl = "../api/KmrStorage/GetQueryDeclarePlanList.aspx";
            mui.ajax(ajaxUrl, {
                data: { company: strCompany, sDate: strSDate, eDate: strEDate, fac: strFac, matName: strMatName },
                async: false,
                crossDomain: true,
                type: 'post',//HTTP请求类型
                success: function (data) {
                    //服务器返回响应，根据响应结果，分析是否登录成功；
                    var js = JSON.parse(data).data;
                    var li;


                    var strHtml;

                    if (js.length > 0) {

                        for (var i = 0; i < js.length; i++) {

                            strHtml = "";
                            strHtml += "<div class=\"list_text\">";
                            strHtml += "<div class=\"list_info\"><div>物料代码：<span class=\"matCode\">" + js[i].matCode + "</span></div></div>";
                            strHtml += "<div class=\"list_info\"><div>物料名称：<span class=\"matName\">" + js[i].matName + "</span></div></div>";
                            strHtml += "<div class=\"list_info\"><div>业务部门：<span class=\"purGroupName\">" + js[i].venDeptDesc + "</span></div></div>";
                            strHtml += "<div class=\"list_info\"><div>申请日期：<span class=\"badat\">" + js[i].badat + "</span></div><div>工厂：<span class=\"facName\">" + js[i].facName + "</span></div></div>";
                            strHtml += "<div class=\"list_info\"><div>计划数量：<span class=\"menge\">" + js[i].menge + "</span></div><div>单位：<span class=\"msehl\">" + js[i].msehl + "</span></div></div>";
                            strHtml += "<div class=\"list_info\"><div>采购凭证：<span class=\"purOrder\">" + js[i].purOrder + "</span></div></div>";
                            strHtml += "<div class=\"list_info\"><div>供应商：<span class=\"suppName\">" + js[i].suppName + "</span></div></div>";
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

                        $("#list1 li").remove();
                        $("#list1").append(fragment);
                        hiddWait();
                    }
                    else {
                        li = document.createElement('li');
                        li.className = 'mui-table-view-cell';
                        li.innerHTML = "<div class=\"list_text\" style=\"text-align:center;margin-bottom:4em\">已经到底了！！！</div>";
                        fragment.appendChild(li);

                        $("#list1 li").remove();
                        $("#list1").append(fragment);
                        hiddWait();
                    }
                },
                error: function (xhr, type, errorThrown) {
                    //异常处理；
                    alert(type);
                    console.log(type);
                }
            });

        }
        //配送查询函数


        mui("#list1").on('tap', '.report', function () {
            //显示lims报告提取
            _matCode = $(this).attr("name");
            //alert(_matCode7);
            showLimsWait();

            //显示lims报告提取
            //获取物料代码


        })

        mui(".mui-input-row").on('tap', '.dateOne', function () {
            datePickOne();
        });

        mui(".mui-input-row").on('tap', '.dateTwo', function () {
            datePickTwo();
        });



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

        //照相图标添加点击事件

        dd.ready(function () {


            queryWait = function (callback) {
                dd.device.notification.showPreloader({
                    text: "数据查询中..", //loading显示的字符，空表示不显示文字
                    showIcon: true, //是否显示icon，默认true
                    onSuccess: function (result) {
                        getQueryList();
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

            hiddWait = function (callback) {
                dd.device.notification.hidePreloader({
                    onSuccess: function (result) {
                        /*{}*/
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

            datePickOne = function (callback) {
                dd.biz.util.datepicker({
                    format: 'yyyy-MM-dd',
                    value: getNowFormatDate(), //默认显示日期
                    onSuccess: function (result) {
                        $("#firDate").html(result.value);
                    },
                    onFail: function (err) { }
                });
            }

            datePickTwo = function (callback) {
                dd.biz.util.datepicker({
                    format: 'yyyy-MM-dd',
                    value: getNowFormatDate(), //默认显示日期
                    onSuccess: function (result) {
                        $("#secDate").html(result.value);
                    },
                    onFail: function (err) { }
                });
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
