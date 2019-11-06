<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="pick_saplist.aspx.cs" Inherits="DDpage.KmrStorage.pick_saplist" %>

<!DOCTYPE html>

<html>
<head>
    <meta charset="utf-8">
    <title>掌上仓储-跨公司领料</title>
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
                        <div id="PCompany" class="div_fix" ></div>
					</div>
                     <div class="mui-input-row">
						<label>领料部门</label>
						<div id="PDept" class="div_fix"></div>
					</div>
                    <div class="mui-input-row">
						<label >领料经营体</label>
						<div id="PVenture" class="div_fix"></div>
					</div>
                    <div class="mui-input-row">
                        <label title="点击选择领料库房" >*领入库房</label>
                         <div id="showPSt">
                            <span id="PStCode" class="font_fix2">请选择...</span>
                        </div>                      	
					</div>
					<div class="mui-input-row">
						<label title="点击选择相关订单号" >*订单号</label>     
                            <input type="text" class="font_fix" placeholder="请输入订单号" id="OddNo" style="width:55%">
                            <span class="mui-icon mui-icon-plus OddList" id="showOddNo"></span>						
					</div>
                    
                    <div class="mui-input-row">
						<label>*工序号</label>
						<input type="text" class="mui-input-clear font_fix" placeholder="请输入" id="proNo" >
					</div>
                    <div class="mui-input-row">
						<label>物料名称</label>
                      
						<input type="text" class="mui-input-clear font_fix" placeholder="请输入" id="matName" style="width:55%">
                        <span class="mui-icon-extra mui-icon-extra-sweep matScanName"></span>
					</div>
                    
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
                <a class="mui-control-item" href="#item2mobile">
                    计划外
                </a>
                <a class="mui-control-item" href="#item3mobile">
                    成本中心
                </a>
                <a class="mui-control-item" href="#item4mobile">
                    查询
                </a>
            </div>
            <div id="sliderProgressBar" class="mui-slider-progress-bar mui-col-xs-3"></div>
            
            <div class="mui-slider-group">
                 
                <div id="item1mobile" class="mui-slider-item mui-control-content mui-active" style="height:25em">    
                   
                    <div id="scroll1" class="mui-scroll-wrapper">
                        <div class="mui-scroll">                     
                            <ul class="mui-table-view mui-table-view-striped mui-table-view-condensed" id="list1" >
                                
                            </ul>
                        </div>
                    </div>
                </div>
                <div id="item2mobile" class="mui-slider-item mui-control-content"  style="height:25em">
                    <div id="scroll2" class="mui-scroll-wrapper">
                        <div class="mui-scroll">
                            <ul class="mui-table-view mui-table-view-striped mui-table-view-condensed" id="list2" >

                            </ul>
                           
                        </div>
                    </div>
                </div>
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
    <script src="../js/dingtalk.js"></script>
    <script src="../js/mui.min.js"></script>
    <script src="../js/mui.picker.js"></script>
	<script src="../js/mui.poppicker.js"></script>
    <script>

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
        var _sapVenId = "";
        var _oneStat = "";
        var _twoStat = "";
        var strJson = "";
        var ajaxUrl = "";
        var showWait;
        var hiddWait;
        var toastInfo;
        var matScan;
        var dataStr = Array();
        var dataStStr = Array();

        var JsonUserInfo = '<%=user_info%>';
        
        var strArray = new Array();
        strArray = JsonUserInfo.split(",")
        _cardId = strArray[0];
        _company = strArray[1];
        _dept = strArray[2];
        _name = strArray[3];
        _venture = strArray[4];
        _unit = strArray[5];
        _sapVenId = strArray[6];
        _costCode = strArray[7];

        _oneStat = 0;
        _twoStat = 0;
        _thStat = 0;
        _listOneStat = 0;
        _listTwoStat = 0;
        _listThStat = 0;
        _listFourStat = 0;

        // if (_costCode.length==0) {
        //    _costCode = '1100F08001';
        //}
        //测试数据
        //_cardId = "640103198106051843";
        //_company = "直属事业部";
        //_dept = "第二铸造工";
        //_venture = "二铸生产服务经营体";
        //_name = "汪玮";
        //_sapVenId = "0145";
        //_costCode = "";
        //测试数据=



        var listhtml = "";

        (function ($) {
            $('.mui-scroll-wrapper').scroll({
                indicators: true //是否显示滚动条
            });

            mui("#PCompany")[0].innerHTML = _company;
            mui("#PDept")[0].innerHTML = _dept;
            mui("#PVenture")[0].innerHTML = _venture;

        })(mui);
        
        //查询按钮loading处理,查询数据
        var strOddNo = "";
        var strRsNo = "";
        var strdataNum = 10;
        var strMatName = "";

        mui("#query").on('tap', '.mui-btn', function () {

            _listOneStat = 0;
            _listTwoStat = 0;
            _listThStat = 0;
            _listFourStat = 0;
            mui(this).button('loading');
            getFList(this);            
            getSList(this);
            getThList(this);
            //getFourList(this);
            //后台获取查询物料信息

            //mui(this).button('reset');
        });
        //查询按钮loading处理,查询数据

        //查询经营体名下生产订单
        (function ($, doc) {
            $.init();
            $.ready(function () {
                var _getParam = function (obj, param) {
                    return obj[param] || '';
                };

                getOddNolist();
                getStCodeList();
            });

            function getOddNolist() {
                var strOddInput = document.getElementById("OddNo").value;
                ajaxUrl = "../api/KmrStorage/GetMesOddNo.aspx";
                mui.ajax(ajaxUrl, {
                    data: { strCom: _company, strDept: _dept, StrVen: _venture, strOdd: strOddInput },
                    async: true,
                    crossDomain: true,
                    //dataType: 'json',//服务器返回json格式数据
                    type: 'post',//HTTP请求类型
                    //timeout: 10000,//超时时间设置为10秒；
                    // headers: { 'Content-Type': 'text/json' },
                    success: function (data) {
                        //服务器返回响应，根据响应结果，分析是否登录成功；
                        var js = JSON.parse(data).data;
                        dataStr.splice(0, dataStr.length);
                        for (var i = 0; i < js.length; i++) {
                            dataStr.push({ value: js[i].strOdd, text: js[i].strTxt });
                        }

                    },
                    error: function (xhr, type, errorThrown) {
                        //异常处理；
                        alert(errorThrown);
                        console.log(type);
                    }
                });

                var showDataPicekerButtonOdd = doc.getElementById('showOddNo');

                showDataPicekerButtonOdd.addEventListener('tap', popPickerOddNo, false);
            }

            function getStCodeList() {
                ajaxUrl = "../api/KmrStorage/GetComStCode.aspx";
                mui.ajax(ajaxUrl, {
                    data: { strCom: _company},
                    async: true,
                    crossDomain: true,
                    //dataType: 'json',//服务器返回json格式数据
                    type: 'post',//HTTP请求类型
                    //timeout: 10000,//超时时间设置为10秒；
                    // headers: { 'Content-Type': 'text/json' },
                    success: function (data) {
                        //服务器返回响应，根据响应结果，分析是否登录成功；
                        var js = JSON.parse(data).data;
                        dataStStr.splice(0, dataStStr.length);
                        for (var i = 0; i < js.length; i++) {
                            dataStStr.push({ value: js[i].strStCode, text: js[i].strStName });
                        }

                    },
                    error: function (xhr, type, errorThrown) {
                        //异常处理；
                        alert(errorThrown);
                        console.log(type);
                    }
                });

                var showDataPicekerButtonSt = doc.getElementById('showPSt');

                showDataPicekerButtonSt.addEventListener('tap', popPickerStCode, false);
            }

            function popPickerStCode() {

                var dataPicekerSt = new $.PopPicker();
                dataPicekerSt.setData(dataStStr);
                //var showDataPicekerButton02 = doc.getElementById('showTypeTwo');
                var ResultSt = doc.getElementById('PStCode');

                dataPicekerSt.show(function (items) {
                    ResultSt.innerText = items[0].value;
                }
                )
            }


            function popPickerOddNo() {

                //更新下拉菜单数据
                var strOddInput = document.getElementById("OddNo").value;
                //console.log(strOddInput);
                if (strOddInput.length > 0) {
                    updateOddNoList();
                }
                //更新下拉菜单数据

                var dataPicekerOddNo = new $.PopPicker();
                dataPicekerOddNo.setData(dataStr);
                //var showDataPicekerButton02 = doc.getElementById('showTypeTwo');
                var ResultOddNo = doc.getElementById('OddNo');

                dataPicekerOddNo.show(function (items) {
                    ResultOddNo.value = items[0].value;
                }
                )
            }

            function updateOddNoList() {
                //console.log("22222");
                var strOddInput = document.getElementById("OddNo").value;
                ajaxUrl = "../api/KmrStorage/GetMesOddNo.aspx";
                mui.ajax(ajaxUrl, {
                    data: { strCom: _company, strDept: _dept, StrVen: _venture, strOdd: strOddInput },
                    async: true,
                    crossDomain: true,
                    //dataType: 'json',//服务器返回json格式数据
                    type: 'post',//HTTP请求类型
                    //timeout: 10000,//超时时间设置为10秒；
                    // headers: { 'Content-Type': 'text/json' },
                    success: function (data) {
                        //服务器返回响应，根据响应结果，分析是否登录成功；
                        var js = JSON.parse(data).data;
                        dataStr.splice(0, dataStr.length);
                        for (var i = 0; i < js.length; i++) {
                            dataStr.push({ value: js[i].strOdd, text: js[i].strTxt });
                        }

                    },
                    error: function (xhr, type, errorThrown) {
                        //异常处理；
                        alert(errorThrown);
                        console.log(type);
                    }
                });
            }


        })(mui, document);
        //查询经营体名下生产订单

        //点击扫码
        mui(".mui-input-row").on('tap', '.matScanName', function () {
            matScan();
        });
        //点击扫码

        //查询按钮还原
        function resetBtn() {
            if (_listOneStat == 1 && _listTwoStat == 1 && _listThStat == 1) {
                mui("#queryBtn").button('reset');
            }

        }
        //查询按钮还原

        function getFList(ob) {
            //获取编号信息
            $("#list1 li").remove();
            strOddNo = mui("#OddNo")[0].value;
            //strRsNo = mui("#RsNo")[0].value;

            var fragment = document.createDocumentFragment();
            ajaxUrl = "../api/KmrStorage/GetSapPlanList.aspx";
            mui.ajax(ajaxUrl, {
                data: { OddNo: strOddNo, RsNo: strRsNo },
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
                            
                            var strId = js[i].matID + js[i].stock;
                            strHtml = "";
                            strHtml += "<div class=\"list_text\">";
                            strHtml += "<div class=\"list_info\"><div>物料代码：<span class=\"codeone" + strId + "\">" + js[i].matnr + "</span></div><div class=\"mui-switch mui-switch-danger  one" + strId + "\" name=\"one" + strId + "\" onclick=\"btn_switch(this)\"><div class=\"mui-switch-handle\"></div></div></div>";
                            strHtml += "<div class=\"list_info\"><div >物料名称：<span class=\"wlmcone" + strId + "\">" + js[i].rmaktx + "</span><span class=\"faccodeone" + strId + "\" style=\"display:none\">" + js[i].factory + "</span><span class=\"stockcodeone" + strId + "\" style=\"display:none\">" + js[i].stock + "</span><span class=\"groupcodeone" + strId + "\" style=\"display:none\">" + js[i].mGroup + "</span><span class=\"gxcodeone" + strId + "\" style=\"display:none\">" + js[i].vornr + "</span></div></div>";
                            strHtml += "<div class=\"list_info\"><div>单位：<span class=\"unitone" + strId + "\">" + js[i].unit + "</div><div>领料类型：<span class=\"typeone" + js[i].matID + "\">计划内领料</div></div>";
                            strHtml += "<div class=\"list_info\"><div>计划数量：<span class=\"numone" + strId + "\">" + js[i].menge + "</span></div><div>领料数量：<input type=\"text\" style=\"width: 5em; height: 1em; margin: 0; font - size:0.5em\" class=\"pnumone" + strId + "\" value=\"" + js[i].menge +"\"></div></div>";
                            strHtml += "</div>";
                            li = document.createElement('li');
                            li.id = "one" + strId;
                            li.title = "one";
                            li.className = 'mui-table-view-cell';
                            li.innerHTML = strHtml;
                            fragment.appendChild(li);
                           
                        };

                        if (js.length > 2) {
                            li = document.createElement('li');
                            li.className = 'mui-table-view-cell';
                            li.innerHTML = "<div class=\"list_text\" style=\"text-align:center;margin-bottom:4em\">已经到底了！！！</div>";
                            fragment.appendChild(li);
                        }
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
        function getSList(ob) {

            $("#list2 li").remove();
            strMatName = mui("#matName")[0].value;

            var fragment = document.createDocumentFragment();
            ajaxUrl = "../api/KmrStorage/GetInventoryList.aspx";
            mui.ajax(ajaxUrl, {
                data: { comName: _company, matName: strMatName, dataNum: strdataNum },
               
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
                            strHtml += "<div class=\"list_info\"><div>物料名称：<span class=\"wlmcsec" + js[i].matID + "\">" + js[i].matName + "</span></div><div class=\"mui-switch mui-switch-danger  sec" + js[i].matID + "\" name=\"sec" + js[i].matID+"\"  onclick=\"btn_secswitch(this)\"><div class=\"mui-switch-handle\"></div></div></div>";
                            strHtml += "<div class=\"list_info\"><div >代码：<span class=\"codesec" + js[i].matID + "\">" + js[i].matCode + "</span><span class=\"facsec" + js[i].matID + "\" style=\"display:none\">" + js[i].matFac + "</span><span class=\"faccodesec" + js[i].matID + "\" style=\"display:none\">" + js[i].matFacCode + "</span><span class=\"stockcodesec" + js[i].matID + "\" style=\"display:none\">" + js[i].matStockCode + "</span><span class=\"groupsec" + js[i].matID + "\" style=\"display:none\">" + js[i].matGroup + "</span><span class=\"groupcodesec" + js[i].matID + "\" style=\"display:none\">" + js[i].matGroupCode + "</span></div></div>";
                            strHtml += "<div class=\"list_info\"><div>批次号：<span class=\"nosec" + js[i].matID + "\">" + js[i].matNo + "</div><div>单位：<span class=\"unitsec" + js[i].matID+"\">" + js[i].matWeight + "</div></div>";
                            strHtml += "<div class=\"list_info\"><div>库存地点：<span class=\"stocksec" + js[i].matID + "\">" + js[i].matStock + "</div><div>领料类型：<span class=\"typesec" + js[i].matID+"\">计划外领料</div></div>";
                            strHtml += "<div class=\"list_info\"><div>库存：<span class=\"numsec" + js[i].matID + "\">" + js[i].matNum + "</div><div>领料数量:<input type=\"text\" style=\"width: 5em; height: 1em; margin: 0; font - size:0.5em\" class=\"pnumsec" + js[i].matID + "\" value=\"0\"></div></div>";
                            strHtml += "</div>";
                            li = document.createElement('li');
                            li.id = "sec" + js[i].matID;
                            li.title = "sec";
                            li.className = 'mui-table-view-cell';
                            li.innerHTML = strHtml;
                            fragment.appendChild(li);
                        };
                        if (js.length > 2) {
                            li = document.createElement('li');
                            li.className = 'mui-table-view-cell';
                            li.innerHTML = "<div class=\"list_text\" style=\"text-align:center;margin-bottom:4em\">已经到底了！！！</div>";
                            fragment.appendChild(li);
                        }
                    }
                    else {

                        li = document.createElement('li');
                        li.className = 'mui-table-view-cell';
                        li.innerHTML = "<div class=\"list_text\" style=\"text-align:center;margin-bottom:4em\">查询无数据！</div>";
                        fragment.appendChild(li);

                    }

                    $("#list2 li").remove();
                    $("#list2").append(fragment);

                    _listTwoStat = 1;
                    resetBtn();
                   // mui("#query").button('reset');

                },
                error: function (xhr, type, errorThrown) {
                    //异常处理；
                    alert(type);
                    console.log(type);
                }
            });
        }

        function getThList(ob) {

            $("#list3 li").remove();

            strMatName = mui("#matName")[0].value;
            var fragment = document.createDocumentFragment();
            ajaxUrl = "../api/KmrStorage/GetInventoryList.aspx";
            mui.ajax(ajaxUrl, {
                data: { comName: _company, matName: strMatName, dataNum: strdataNum },

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
                            strHtml += "<div class=\"list_info\"><div>物料名称：<span class=\"wlmcth" + js[i].matID + "\">" + js[i].matName + "</span></div><div class=\"mui-switch mui-switch-danger  th" + js[i].matID + "\" name=\"th" + js[i].matID + "\"  onclick=\"btn_thswitch(this)\"><div class=\"mui-switch-handle\"></div></div></div>";
                            strHtml += "<div class=\"list_info\"><div >代码：<span class=\"codeth" + js[i].matID + "\">" + js[i].matCode + "</span><span class=\"facth" + js[i].matID + "\" style=\"display:none\">" + js[i].matFac + "</span><span class=\"faccodeth" + js[i].matID + "\" style=\"display:none\">" + js[i].matFacCode + "</span><span class=\"stockcodeth" + js[i].matID + "\" style=\"display:none\">" + js[i].matStockCode + "</span><span class=\"groupth" + js[i].matID + "\" style=\"display:none\">" + js[i].matGroup + "</span><span class=\"groupcodeth" + js[i].matID + "\" style=\"display:none\">" + js[i].matGroupCode + "</span></div></div>";
                            strHtml += "<div class=\"list_info\"><div>批次号：<span class=\"noth" + js[i].matID + "\">" + js[i].matNo + "</div><div>单位：<span class=\"unitth" + js[i].matID + "\">" + js[i].matWeight + "</div></div>";
                            strHtml += "<div class=\"list_info\"><div>库存地点：<span class=\"stockth" + js[i].matID + "\">" + js[i].matStock + "</div><div>领料类型：<span class=\"typeth" + js[i].matID + "\">成本中心领料</div></div>";
                            strHtml += "<div class=\"list_info\"><div>库存：<span class=\"numth" + js[i].matID + "\">" + js[i].matNum + "</div><div>领料数量:<input type=\"text\" style=\"width: 5em; height: 1em; margin: 0; font - size:0.5em\" class=\"pnumth" + js[i].matID + "\" value=\"0\"></div></div>";
                            strHtml += "</div>";
                            li = document.createElement('li');
                            li.id = "th" + js[i].matID;
                            li.title = "th";
                            li.className = 'mui-table-view-cell';
                            li.innerHTML = strHtml;
                            fragment.appendChild(li);
                        };
                        if (js.length > 2) {
                            li = document.createElement('li');
                            li.className = 'mui-table-view-cell';
                            li.innerHTML = "<div class=\"list_text\" style=\"text-align:center;margin-bottom:4em\">已经到底了！！！</div>";
                            fragment.appendChild(li);
                        }
                    }
                    else {

                        li = document.createElement('li');
                        li.className = 'mui-table-view-cell';
                        li.innerHTML = "<div class=\"list_text\" style=\"text-align:center;margin-bottom:4em\">查询无数据！</div>";
                        fragment.appendChild(li);

                    }

                    $("#list3 li").remove();
                    $("#list3").append(fragment);

                    _listThStat = 1;
                    resetBtn();
                    // mui("#query").button('reset');

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

            $("#list4 li").remove();

            if (strMatName.length > 0 || strStock.length > 0) {
                strdataNum = 10;
            }
            var fragment = document.createDocumentFragment();
            ajaxUrl = "../api/KmrStorage/GetSapPickedInfo.aspx";
            mui.ajax(ajaxUrl, {
                data: { matName: strMatName, stockName: strStock, dataNum: strdataNum, Company: _company, Dept: _dept, Ven: _venture, Name: _name },
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
                            li.className = 'mui-table-view-cell listFour';
                            li.innerHTML = strHtml;
                            fragment.appendChild(li);
                        };
                        if (js.length > 2) {
                            li = document.createElement('li');
                            li.className = 'mui-table-view-cell';
                            li.innerHTML = "<div class=\"list_text\" style=\"text-align:center;margin-bottom:4em\">已经到底了！！！</div>";
                            fragment.appendChild(li);
                        }
                    }
                    else {

                        li = document.createElement('li');
                        li.className = 'mui-table-view-cell';
                        li.innerHTML = "<div class=\"list_text\" style=\"text-align:center;margin-bottom:4em\">查询无数据！</div>";
                        fragment.appendChild(li);

                    }

                    _listFourStat = 1;
                    resetBtn();

                    $("#list4 li").remove();
                    $("#list4").append(fragment);

                },
                error: function (xhr, type, errorThrown) {
                    //异常处理；
                    alert(type);
                    console.log(type);
                }
            });
        }
        //查询tab数据

        //list3 中li元素添加监听事件
        mui(".mui-table-view").on('tap', '.listFour', function () {

            $(this).find("span").addClass("on");


            //alert($("span.pcompany.on").text());

            //获取当前li的linkid
            var strLinkID = $("span.linkid.on").text();

            //修改blist中的li
            var fragment = document.createDocumentFragment();
            ajaxUrl = "../api/KmrStorage/GetInQueryDetail.aspx";
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
                        strHtml += "<div class=\"list_info\"><div>申请数量：<span>" + js[i].PickInventory + "</div></div>";
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
                $("span.mui-badge").text(num);
                $("li." + nameid + "").remove();
                _oneStat--;

            }
            else {
                $(obj).addClass("mui-active");
                $(obj).addClass("onselected");
                 //获取领料数量
                var inputNum = $("li#" + nameid + "").find("input").val();
                 //获取领料数量
                var num = $("span.mui-badge").text();
                num++;
                $("span.mui-badge").text(num);

                var fragment = document.createDocumentFragment();
                var li;
                li = document.createElement('li');
                li.className = 'mui-table-view-cell picked '+nameid+'';
                li.id = nameid;
                li.innerHTML = $("li#" + nameid + "").html();

                fragment.appendChild(li);
                $("#blist").append(fragment);
                $(obj).removeClass("onselected");
                $(".onselected").attr("onclick", "btn_pickswitch(this)");

                //设置领料数量

                $("li#" + nameid + ".picked").find("input").val(inputNum);

                //设置领料数量
                //设置领料状态
                _oneStat++;
            }
        }


        function btn_secswitch(obj) {
            var nameid = $(obj).attr("name");
            
            if ($(obj).hasClass('mui-active')) {
                $(obj).removeClass("mui-active");
                var num = $("span.mui-badge").text()
                num--;
                $("span.mui-badge").text(num);
                $("li." + nameid + "").remove();
                _twoStat--;

            }
            else {
                $(obj).addClass("mui-active");
                $(obj).addClass("onsecselected");

                //获取领料数量
                var inputNum = $("li#" + nameid + "").find("input").val();
                 //获取领料数量

                var num = $("span.mui-badge").text();
                num++;
                $("span.mui-badge").text(num);

                var fragment = document.createDocumentFragment();
                var li;
                li = document.createElement('li');
                li.className = 'mui-table-view-cell picked '+ nameid + '';
                li.id = nameid;
                li.innerHTML = $("li#" + nameid + "").html();

                fragment.appendChild(li);
                $("#blist").append(fragment);
                $(obj).removeClass("onsecselected");
                $(".onsecselected").attr("onclick", "btn_Secpickswitch(this)");

                //设置领料数量

                $("li#" + nameid + ".picked").find("input").val(inputNum);

                //设置领料数量

                _twoStat++;

            }

        }


        function btn_thswitch(obj) {
            var nameid = $(obj).attr("name");

            if ($(obj).hasClass('mui-active')) {
                $(obj).removeClass("mui-active");
                var num = $("span.mui-badge").text()
                num--;
                $("span.mui-badge").text(num);
                $("li." + nameid + "").remove();
                _thStat--;

            }
            else {
                $(obj).addClass("mui-active");
                $(obj).addClass("onthselected");

                //获取领料数量
                var inputNum = $("li#" + nameid + "").find("input").val();
                //获取领料数量

                var num = $("span.mui-badge").text();
                num++;
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

                _thStat++;

            }

        }


        function btn_pickswitch(obj) {
            var nameclass = $(obj).attr("name");
            if ($(obj).hasClass('mui-active')) {
                $(obj).removeClass("mui-active");
                $("li." + nameclass + "").removeClass("picked");
                _oneStat--;
            }
            else {
                $(obj).addClass("mui-active");               
                $("li." + nameclass + "").addClass("picked");
                _oneStat++;
            }
        }

        function btn_Secpickswitch(obj) {
            var nameclass = $(obj).attr("name");
            if ($(obj).hasClass('mui-active')) {
                $(obj).removeClass("mui-active");
                $("li." + nameclass + "").removeClass("picked");
                _twoStat--;
            }
            else {
                $(obj).addClass("mui-active");
                $("li." + nameclass + "").addClass("picked");
                _twoStat++;
            }
        }

        function btn_Thpickswitch(obj) {
            var nameclass = $(obj).attr("name");
            if ($(obj).hasClass('mui-active')) {
                $(obj).removeClass("mui-active");
                $("li." + nameclass + "").removeClass("picked");
                _thStat--;
            }
            else {
                $(obj).addClass("mui-active");
                $("li." + nameclass + "").addClass("picked");
                _thStat++;
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
            var pStCode = $("#PStCode").text();
            if (orderNum == "请输入") {
                orderNum = "";
            }
            if (proNum == "请输入") {
                proNum = "";
            }
            if (pStCode == "请选择...") {
                pStCode = "";
            }

            //计划内领料需要输入订单号，计划外领料需要输入订单号，工序号
            if (_oneStat > 0) {
                if (orderNum.length == 0) {
                    mui.toast("存在SAP计划内领料，请输入订单号！");
                    return;
                }
            }

            if (_twoStat > 0) {
                if (orderNum.length == 0) {
                    mui.toast("存在SAP计划外领料，请输入订单号！");
                    return
                }
                if (proNum.length == 0) {
                    mui.toast("存在SAP计划外领料，请输入工序号！");
                    return;
                }
                if (pStCode.length == 0) {
                    mui.toast("存在SAP计划外领料，请选择领料库房！");
                    return;
                }
            }

            if (_thStat > 0) {
                if (_costCode.length == 0) {
                    mui.toast("存在成本中心领料，EHR中未维护成本中心！");
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

            var orderNum = $("#OddNo").val();
            var proNum = $("#proNo").val();
            var pStCode = $("#PStCode").text();
            if (orderNum == "请输入") {
                orderNum = "";
            }
            if (proNum == "请输入") {
                proNum = "";
            }
            if (pStCode == "请选择...") {
                pStCode = "";
            }

            var matName = $("#matName").val();

            var strOneJson = "";
            var strTwoJson = "";
            var strThJson = "";
            var strType = "";
            //strJson += "{\"PCompany\":\"" + _company + "\",\"PDept\":\"" + _dept + "\",\"PVenture\":\"" + _venture + "\",\"PName\":\"" + _name + "\",\"PCode\":\"" + _cardId + "\",";
            //strJson += "\"PTime\":\"" + getNowFormatDate() + "\",\"OddNo\":\"" + orderNum + "\",\"PickStat\":\"待备餐\",\"data\":[";
            strOneJson += "[";
            strTwoJson += "[";
            strThJson += "[";
            var oneNum = 0;
            var twoNum = 0;
            var thNum = 0;

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
                    strOneJson += "\"MUnit\":\"" + $("span.unit" + tmp + ".on").text() + "\",";
                    strOneJson += "\"PType\":\"" + $("span.type" + tmp + ".on").text() + "\",";
                    strOneJson += "\"MInventory\":\"" + $("span.num" + tmp + ".on").text() + "\",";
                    strOneJson += "\"PickInventory\":\"" + $("input.pnum" + tmp + ".on").val() + "\",";
                    strOneJson += "\"PDStat\":\"1\"},";
                }


                if (strType == "计划外领料") {
                    twoNum += 1;
                    strTwoJson += "{\"MStock\":\"" + $("span.stock" + tmp + ".on").text() + "\",";
                    strTwoJson += "\"MStockCode\":\"" + $("span.stockcode" + tmp + ".on").text() + "\",";
                    strTwoJson += "\"MFactory\":\"" + $("span.fac" + tmp + ".on").text() + "\",";
                    strTwoJson += "\"MFactoryCode\":\"" + $("span.faccode" + tmp + ".on").text() + "\",";
                    strTwoJson += "\"MGroup\":\"" + $("span.group" + tmp + ".on").text() + "\",";
                    strTwoJson += "\"MGroupCode\":\"" + $("span.groupcode" + tmp + ".on").text() + "\",";
                    strTwoJson += "\"Material\":\"" + strtojs($("span.wlmc" + tmp + ".on").text()) + "\",";
                    strTwoJson += "\"MCode\":\"" + $("span.code" + tmp + ".on").text() + "\",";
                    strTwoJson += "\"MBatch\":\"" + $("span.no" + tmp + ".on").text() + "\",";
                    strTwoJson += "\"MUnit\":\"" + $("span.unit" + tmp + ".on").text() + "\",";
                    strTwoJson += "\"MInventory\":\"" + $("span.num" + tmp + ".on").text() + "\",";
                    strTwoJson += "\"PickInventory\":\"" + $("input.pnum" + tmp + ".on").val() + "\",";
                    strTwoJson += "\"PType\":\"" + $("span.type" + tmp + ".on").text() + "\",";
                    strTwoJson += "\"PDStat\":\"1\"},";
                }

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
                    strThJson += "\"PDStat\":\"1\"},";
                }

            })

            strOneJson = strOneJson.substr(0, strOneJson.length - 1);
            strOneJson += "]";

            strTwoJson = strTwoJson.substr(0, strTwoJson.length - 1);
            strTwoJson += "]";

            strThJson = strThJson.substr(0, strThJson.length - 1);
            strThJson += "]";

            if (oneNum == 0) {
                strOneJson = "";
            }

            if (twoNum == 0) {
                strTwoJson = "";
            }

            if (thNum == 0) {
                strThJson = "";
            }
                //alert(strOneJson);
                //alert(strTwoJson);

                //console.log(strOneJson);
                //console.log(strTwoJson);

                //strJson += "]}";


            //ajax后台数据处理
            //获取订单号、工序号
                $.post("../api/KmrStorage/sapList_sumbit.aspx",
                    { PCompany: _company, PDept: _dept, PVenture: _venture, sapVentureCode: _sapVenId, PName: _name, PCode: _cardId, PTime: getNowFormatDate(), OddNo: orderNum, ProNo: proNum, PStNo: pStCode, PickStat: "1", dataListOne: strOneJson, dataListTwo: strTwoJson, dataListTh: strThJson },
                function (data, status) {


                    hiddWait();
                    toastInfo(data);
                    mui.toast(data, { duration: 'long', type: 'div' })

                   
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


        function strtojsFix(str) {
            console.log(str)
            var jst = "";
            for (var i = 0; i < str.length; i++) {
                var c = str[i];
                switch (c) {
                   
                    case '\\':
                        jst += "\\\\";
                        break;
                    default: {
                        jst += c;
                        break;
                    }

                }
            }
            return jst;
        }


       


        //初始化查询
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

            matScan = function () {
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
                        //alert(js);

                        //js = strtojs(js);
                        //var strScan = '[{\"ID\":\"1073575\",\"TY\":\"#Q10\",\"NM\":\"生铁\\\\#Q10\"}]';
                        //for (var i = 0; i < js.length; i++) {
                        //    alert(js.substr(i, 1) + "|||"+i+"|||" + strScan.substr(i, 1));
                        //}
                        //alert(js.length + "::::" + strScan.length);
                        //alert(strScan);
                        var jsObj = JSON.parse(js);
                        //alert(jsObj[0].NM);

                        $("#matName").val(jsObj[0].ID);

                    },
                    onFail: function (err) {
                        alert("scan fail:" + JSON.stringify(err));
                    }
                })
            }

        });

    </script>
</body>
</html>