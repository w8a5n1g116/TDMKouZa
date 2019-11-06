<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="confirm_tablist.aspx.cs" Inherits="DDpage.KmrStorage.confirm_tablist" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>掌上仓储-领料确认</title>
    <meta name="viewport" content="width=device-width, initial-scale=1,maximum-scale=1,user-scalable=no"/>
    <meta name="apple-mobile-web-app-capable" content="yes"/>
    <meta name="apple-mobile-web-app-status-bar-style" content="black"/>
    <!--标准mui.css-->
    <link rel="stylesheet" href="../css/mui.min.css"/>
    <link rel="stylesheet" type="text/css" href="../css/app.css" />
	<link href="../css/mui.picker.css" rel="stylesheet" />
	<link href="../css/mui.poppicker.css" rel="stylesheet" />
    <link href="../css/iconfont.css" rel="stylesheet" />
    <!--App自定义的css-->
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
    <div class="mui-content">
        <div class="mui-content-padded" style="margin: 5px;">
        <form class="mui-input-group font_fix">
					 <div class="mui-input-row">
						<label style="width:35%">责任公司</label>
						<div id="showCom" style="width:65%">
                            <span id="PCompany" class="font_fix2">请选择</span>
                        </div>  
					</div>
                    <div class="mui-input-row">
						<label style="width:35%">责任部门</label>
						<div id="showDept" style="width:65%">
                            <span id="PDept" class="font_fix2">请选择</span>
                        </div>  
					</div>
                    <div class="mui-input-row mui-hidden">
						<label style="width:35%">责任经营体</label>
						<div id="showVen" style="width:65%">
                            <span id="PVen" class="font_fix2">请选择...</span>
                        </div>  
					</div>
					<div class="mui-input-row">
						<label style="width:35%">领料类型</label>
						<div id="showPType" style="width:65%">
                            <span id="PType" class="font_fix2">请选择</span>
                        </div>  
					</div>
                    <div class="mui-input-row">
						<label>领料单号</label>
						<input id="PDID" type="text" class="mui-input-clear font_fix" placeholder="请输入领料单号"/>
					</div>
                    <div class="mui-input-row">
						<label>物料名称</label>
						<input id="PMName" type="text" class="mui-input-clear font_fix" placeholder="请输入物料名称"/>
					</div>
            <div class="mui-button-row" id="query">
						<button type="button" class="mui-btn mui-btn-danger" id="queryBtn">查询</button>
					</div>
        </form>
            </div>

         

        <div id="slider" class="mui-slider" >
            <div id="sliderSegmentedControl" class="mui-slider-indicator mui-segmented-control mui-segmented-control-inverted mui-segmented-control-negative">
                <a class="mui-control-item" href="#item1mobile">
                    待确认
                </a>
                <a class="mui-control-item" href="#item2mobile">
                    确认查询
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
    <%--<script src="../js/ddjs.js"></script>--%>
    <script src="//g.alicdn.com/dingding/dingtalk-jsapi/2.6.41/dingtalk.open.js"></script>
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
        var strJson = "";
        var ajaxUrl = "";
        var toastInfo;
        var showSubWait;
        var showLimsWait;
        var hiddWait;
        var _matCode = "";
        var dataStrCom = Array();
        var dataStrDept = Array();
        var dataStrVen = Array();
        var dataStrPType = Array();

        var stockhtml = "";

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
        _listOneStat = 0;
        _listTwoStat = 0;

        //初始化查询
        mui.ready(function () {
            //mui.trigger(btn, 'tap');
            ajaxUrl = "../api/KmrStorage/GetCheckInFacStock_BL.aspx";
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
                    stockhtml = "";
                    for (var i = 0; i < js.length; i++) {
                        stockhtml += '<option value ="' + js[i].stockName + '">' + js[i].stockName + '</option>';
                    }

                },
                error: function (xhr, type, errorThrown) {
                    //异常处理；
                    alert(errorThrown);
                    console.log(type);
                }
            });


            var btn = document.getElementById("queryBtn");
            mui.trigger(btn, 'tap');
        });


        (function ($) {
            $('.mui-scroll-wrapper').scroll({
                indicators: true //是否显示滚动条
            });
            mui("#PCompany")[0].innerHTML = _company;
            //mui("#PDept")[0].innerHTML = _dept;
            // mui("#PVenture")[0].innerHTML = _venture;

            getPDept();
            getPType();

            var _getParam = function (obj, param) {
                return obj[param] || '';
            };

            function getPDept() {

                ajaxUrl = "../api/KmrStorage/GetDept_BL.aspx";
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
                        dataStrDept.splice(0, dataStrDept.length);
                        for (var i = 0; i < js.length; i++) {
                            dataStrDept.push({ value: js[i].dept, text: js[i].dept });
                        }

                    },
                    error: function (xhr, type, errorThrown) {
                        //异常处理；
                        alert(errorThrown);
                        console.log(type);
                    }
                });

                var showDataPicekerButtonDept = document.getElementById('showDept');
                showDataPicekerButtonDept.addEventListener('tap', popPickerDept, false);

            }

            function popPickerDept() {
                var dataPicekerDept = new $.PopPicker();
                dataPicekerDept.setData(dataStrDept);
                //var showDataPicekerButton02 = doc.getElementById('showTypeTwo');
                var ResultDept = document.getElementById('PDept');

                dataPicekerDept.show(function (items) {
                    ResultDept.innerText = items[0].text;
                }
                )
            }

            function getPType() {

                dataStrPType.splice(0, dataStrPType.length);
                dataStrPType.push({ value: '计划内领料', text: '计划内领料' });
                dataStrPType.push({ value: '计划外领料', text: '计划外领料' });
                dataStrPType.push({ value: '成本中心领料', text: '成本中心领料' });
                dataStrPType.push({ value: '研发类领料', text: '研发类领料' });
                dataStrPType.push({ value: '工程类领料', text: '工程类领料' });

                var showDataPicekerButtonPType = document.getElementById('showPType');
                showDataPicekerButtonPType.addEventListener('tap', popPickerPType, false);

            }

            function popPickerPType() {
                var dataPicekerPType = new $.PopPicker();
                dataPicekerPType.setData(dataStrPType);
                //var showDataPicekerButton02 = doc.getElementById('showTypeTwo');
                var ResultPType = document.getElementById('PType');

                dataPicekerPType.show(function (items) {
                    ResultPType.innerText = items[0].text;
                }
                )
            }

            

        })(mui);

        //查询按钮loading处理,查询数据
        var strCompany = "";
        var strDept = "";
        var strVenture = "";
        var strMatName = "";
        var strPType = "";
        var strPDID = "";

        mui("#query").on('tap', '.mui-btn', function () {
            mui(this).button('loading');
            //查询物料信息
            strCompany = mui("#PCompany")[0].innerText;
            if (strCompany == "请选择") {
                strCompany = "";
            }
            strDept = mui("#PDept")[0].innerText;
            if (strDept == "请选择") {
                strDept = "";
            }
            strVenture = mui("#PVen")[0].innerText;
            if (strVenture == "请选择") {
                strVenture = "";
            }

            strPType = mui("#PType")[0].innerText;
            if (strPType == "请选择") {
                strPType = "";
            }

            strPDID = mui("#PDID")[0].innerText;

            _listOneStat = 0;
            _listTwoStat = 0;

            mui(this).button('loading');

            getFList(this);
            getSList(this);

            //后台获取查询物料信息

            mui(this).button('reset');
        });
        //查询按钮loading处理,查询数据

        //查询按钮还原
        function resetBtn() {
            if (_listOneStat == 1 && _listTwoStat == 1 ) {
                mui("#queryBtn").button('reset');
            }

        }
        //查询按钮还原

        //待配送查询函数
        function getFList(ob) {
            var fragment = document.createDocumentFragment();
            ajaxUrl = "../api/KmrStorage/GetComConfDetail_BL.aspx";
            mui.ajax(ajaxUrl, {
                data: { Company: strCompany, Dept: strDept, Ven: strVenture, MatName: strMatName, Name: _name, pType: strPType, PDID: strPDID},
                async: false,
                crossDomain: true,
                type: 'post',//HTTP请求类型
                success: function (data) {
                    //服务器返回响应，根据响应结果，分析是否登录成功；
                    var js = JSON.parse(data).data;
                    var li;
                    var strHtml;
                    var subNum = js.length;
                    for (var i = 0; i < js.length; i++) {

                        strHtml = "";
                        strHtml += "<div class=\"list_text\">";
                        strHtml += "<div class=\"list_info\"><div>物料名称：<span class=\"wlmc" + js[i].PDID + "\">" + js[i].PMatName + "</span></div><div class=\"mui-switch mui-switch-danger  mui-active " + js[i].PDID + "\" name=\"" + js[i].PDID + "\" onclick=\"btn_switch(this)\"><div class=\"mui-switch-handle\"></div></div></div>";
                        strHtml += "<div class=\"list_info\"><div>物料代码：<span class=\"wldm" + js[i].PDID + "\">" + js[i].MCode + "</span></div></div>";
                        strHtml += "<div class=\"list_info\"><div>序列号：<span class=\"rsno" + js[i].PDID + "\">" + js[i].MRsNo + "</span></div></div>";
                        strHtml += "<div class=\"list_info\"><div>公司：<span class=\"com" + js[i].PDID + "\">" + js[i].PCompany + "</div><div>部门：<span class=\"dept" + js[i].PDID + "\">" + js[i].PDept + "</div></div>";
                        strHtml += "<div class=\"list_info\"><div>经营体：<span class=\"ven" + js[i].PDID + "\">" + js[i].PVen + "<span class=\"pdid\" style=\"display:none\">" + js[i].PDID + "</span></div></div>";
                        strHtml += "<div class=\"list_info\"><div>申请数量：<input class=\"num" + js[i].PDID + "\" name=\"num" + js[i].PDID + "\" value=\"" + js[i].PNum + "\"/></div><div>类型：<span class=\"type" + js[i].PDID + "\">" + js[i].PType + "</span></div></div>";
                        strHtml += "<div class=\"list_info\"><div>申请时间：<span class=\"ptime" + js[i].PDID + "\">" + js[i].PTime + "</div><div>申请人：<span class=\"person" + js[i].PDID + "\">" + js[i].PName + "</div></div>"; 
                        strHtml += "<div class=\"list_info\"><div>申请仓库：<select class=\"PStock" + js[i].PDID + "\"><option value =\"" + js[i].PStock + "\">" + js[i].PStock + "</option>" + stockhtml + "</select></div></div>"; //<span class=\"PStock" + js[i].PDID + "\">" + js[i].PStock + "</span>
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
                    li.innerHTML = "<div class=\"list_text\" style=\"text-align:center;margin-bottom:8em\">已经到底了！！！</div>";
                    fragment.appendChild(li);

                    $("#list1 li").remove();
                    $("#list1").append(fragment);

                    $("span.mui-badge").text(subNum);

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


        //待配送查询函数

        //配送查询函数
        function getSList(ob) {

            var strPtype = $("#PType").html();
            if (strPtype == '请选择')
                strPtype = '';
            var fragment = document.createDocumentFragment();
            ajaxUrl = "../api/KmrStorage/GetComPickInfo_BL_KuGuan.aspx";
            mui.ajax(ajaxUrl, {
                data: { matName: strMatName, Company: strCompany, Dept: strDept, Ven: strVenture, Name: _name, pType: strPtype },
                async: false,
                crossDomain: true,
                type: 'post',//HTTP请求类型
                success: function (data) {
                    //服务器返回响应，根据响应结果，分析是否登录成功；
                    var js = JSON.parse(data).data;
                    var li;

                    var strHtml;


                    var strHtml = '';

                    for (var i = 0; i < js.length; i++) {
                        strHtml += '<li class="mui-table-view-cell" id="' + js[i].stPDID + '">';
                        strHtml += "<div class=\"list_text\">";
                        strHtml += "<div class=\"list_info\"><div>申请单号：<span class=\"pid\">" + js[i].PID + "</span><span class=\"linkid\" style=\"display:none\">" + js[i].PLID + "</span></div><div>申请状态：<span class=\"pstat\">" + js[i].PStat + "</span></div></div>";
                        strHtml += "<div class=\"list_info\"><div>申请公司：<span class=\"pcompany\">" + js[i].PCom + "</span></div><div>申请部门：<span class=\"pdept\">" + js[i].PDept + "</span></div></div>";
                        strHtml += "<div class=\"list_info\"><div>申请经营体：<span class=\"pven\">" + js[i].PVen + "</span></div><div>申请人：<span class=\"pname\">" + js[i].PName + "</span></div></div>";
                        strHtml += "<div class=\"list_info\"><div>申请时间：<span class=\"ptime\">" + js[i].PTime + "</span></div></div>"
                        strHtml += "<div class=\"list_info\"><div>物料名称：<span>" + js[i].matName + "</span></div></div>";
                        strHtml += "<div class=\"list_info\"><div >代码：<span>" + js[i].MCode + "</span></div><div>物料状态：<span>" + js[i].PDStat + "</span></div></div>";
                        strHtml += "<div class=\"list_info\"><div>发料库房：<span>" + js[i].mStock + "</span></div><div>单位：<span>" + js[i].mUnit + "</span></div></div>";
                        strHtml += "<div class=\"list_info\"><div>申请数量：<span>" + js[i].Pnum + "</div><div>领料类型：<span>" + js[i].Ptype + "</div></div>";
                        if (js[i].pickInfo.length > 0) {
                            strHtml += "<div class=\"list_info\"><div>SAP：<span class=\"ifsap\">" + js[i].ifSap + "</span></div><div>SAP类型：<span class=\"saptype\">" + js[i].pickItemType + "</span></div></div>";
                            strHtml += "<div class=\"list_info\"><div>SAP信息：<span class=\"sappickinfo\">" + js[i].pickInfo + "</span></div></div>";
                        }
                        strHtml += "</div>";
                        strHtml += '</li>';
                    };

                    //for (var i = 0; i < js.length; i++) {

                    //    strHtml = "";
                    //    strHtml += "<div class=\"list_text\"><a  href=\"#queryPopover\" >";
                    //    strHtml += "<div class=\"list_info\"><div>申请单号：<span class=\"pid\">" + js[i].PID + "</span><span class=\"linkid\" style=\"display:none\">" + js[i].PLID + "</span></div><div>申请状态：<span class=\"pstat\">" + js[i].PStat + "</span></div></div>";
                    //    strHtml += "<div class=\"list_info\"><div>申请公司：<span class=\"pcompany\">" + js[i].PCom + "</span></div><div>申请部门：<span class=\"pdept\">" + js[i].PDept + "</span></div></div>";
                    //    strHtml += "<div class=\"list_info\"><div>申请经营体：<span class=\"pven\">" + js[i].PVen + "</span></div><div>申请人：<span class=\"pname\">" + js[i].PName + "</span></div></div>";
                    //    strHtml += "<div class=\"list_info\"><div>申请时间：<span class=\"ptime\">" + js[i].PTime + "</span></div><div>申请类型：<span class=\"ptype\">" + js[i].PType + "</span></div></div>";
                    //    strHtml += "</a>";
                    //    strHtml += "</div>";
                    //    li = document.createElement('li');
                    //    li.id = js[i].matID;
                    //    li.title = "";
                    //    li.className = 'mui-table-view-cell listTwo';
                    //    li.innerHTML = strHtml;
                    //    fragment.appendChild(li);
                    //};

                    strHtml += '<li class="mui-table-view-cell">'
                    strHtml += '<div class=\"list_text\" style=\"text-align:center;margin-bottom:8em\">已经到底了！！！</div>'
                    strHtml += '</li>'

                    //li = document.createElement('li');
                    //li.className = 'mui-table-view-cell';
                    //li.innerHTML = "<div class=\"list_text\" style=\"text-align:center;margin-bottom:8em\">已经到底了！！！</div>";
                    //fragment.appendChild(li);

                    $("#list2 li").remove();
                    $("#list2").append(strHtml);

                    _listTwoStat = 1;
                    resetBtn();
                },
                error: function (xhr, type, errorThrown) {
                    //异常处理；
                    alert(type);
                    console.log(type);
                }
            });

        }
        //配送查询函数

        mui(".mui-table-view").on('tap', '.listTwo', function () {

            $(this).find("span").addClass("on");


            //获取当前li的linkid
            var strID = $("span.linkid.on").text();

            var strPtype = $("span.ptype.on").text();

            if (strPtype == '仓库调拨') {
                $.ajaxSettings.async = false;
                $.post('../api/KmrStorage/GetAllotQueryDetail_BL.aspx', { LinkID: strID  }, function (data) {
                    var js = JSON.parse(data).data;

                    var strHtml = "";

                    for (var i = 0; i < js.length; i++) {
                        strHtml += '<li class="mui-table-view-cell" id="' + js[i].stPDID + '">';
                        strHtml += "<div class=\"list_text\">";
                        strHtml += "<div class=\"list_info\"><div>物料名称：<span>" + js[i].MatName + "</span></div></div>";
                        strHtml += "<div class=\"list_info\"><div >代码：<span>" + js[i].MCode + "</span></div></div>";
                        strHtml += "<div class=\"list_info\"><div >工厂：<span>" + js[i].MFac + "</span></div><div>单位：<span>" + js[i].MUnit + "</span></div></div>";
                        strHtml += "<div class=\"list_info\"><div>调出库房：<span>" + js[i].MOutStock + "</span></div><div>调入库房：<span>" + js[i].MInStock + "</span></div></div>";
                        strHtml += "<div class=\"list_info\"><div>库存：<span>" + js[i].MInven + "</div><div>调拨数量:<span>" + js[i].PNum + "</div></div>";
                    }

                    $("#queryList li").remove();
                    $("#queryList").append(strHtml);
                })
                $.ajaxSettings.async = true;
            } else {
                $.ajaxSettings.async = false;
                $.post('../api/KmrStorage/GetComQueryDetail_BL.aspx', { strID: strID }, function (data) {
                    var js = JSON.parse(data).data;

                    var strHtml = '';

                    for (var i = 0; i < js.length; i++) {
                        strHtml += '<li class="mui-table-view-cell" id="' + js[i].stPDID + '">';
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
                        strHtml += '</li>';
                    };


                    $("#queryList li").remove();
                    $("#queryList").append(strHtml);
                })
                $.ajaxSettings.async = true;
            }

            $(this).find("span").removeClass("on");

            ////alert(strID);
            ////修改blist中的li
            //var fragment = document.createDocumentFragment();
            //ajaxUrl = "../api/KmrStorage/GetComQueryDetail_BL.aspx";
            //mui.ajax(ajaxUrl, {
            //    data: { strID: strID },
            //    async: false,
            //    crossDomain: true,
            //    type: 'post',//HTTP请求类型
            //    success: function (data) {
            //        //服务器返回响应，根据响应结果，分析是否登录成功；
            //        var js = JSON.parse(data).data;
            //        var li;

            //        var strHtml;

            //        for (var i = 0; i < js.length; i++) {

            //            strHtml = "";
            //            strHtml += "<div class=\"list_text\">";
            //            strHtml += "<div class=\"list_info\"><div>物料名称：<span>" + js[i].matName + "</span></div></div>";
            //            strHtml += "<div class=\"list_info\"><div >代码：<span>" + js[i].MCode + "</span></div><div>物料状态：<span>" + js[i].PDStat + "</span></div></div>";
            //            strHtml += "<div class=\"list_info\"><div>发料库房：<span>" + js[i].mStock + "</span></div><div>单位：<span>" + js[i].mUnit + "</span></div></div>";
            //            strHtml += "<div class=\"list_info\"><div>申请数量：<span>" + js[i].Pnum + "</div><div>领料类型：<span>" + js[i].Ptype + "</div></div>";
            //            if (js[i].pickInfo.length > 0) {
            //                strHtml += "<div class=\"list_info\"><div>SAP：<span class=\"ifsap\">" + js[i].ifSap + "</span></div><div>SAP类型：<span class=\"saptype\">" + js[i].pickItemType + "</span></div></div>";
            //                strHtml += "<div class=\"list_info\"><div>SAP信息：<span class=\"sappickinfo\">" + js[i].pickInfo + "</span></div></div>";
            //            }
            //            strHtml += "</div>";
            //            li = document.createElement('li');
            //            li.id = js[i].stPDID;
            //            //li.title = "sec";
            //            li.className = 'mui-table-view-cell ';
            //            li.innerHTML = strHtml;
            //            fragment.appendChild(li);
            //        };


            //        $("#queryList li").remove();
            //        $("#queryList").append(fragment);

            //    },
            //    error: function (xhr, type, errorThrown) {
            //        //异常处理；
            //        alert(type);
            //        console.log(type);
            //    }
            //});

            
        })


        //确认按钮提交

        mui("#nav-submit").on("tap", ".checkSubmit", function () {
            if ($("li.picked").length > 0) {
                if (confirm("确认提交？")) {
                    showSubWait();

                }
            }
            else{

                alert("无确认信息，请返回继续修改！")
            }
            });

        function postData() {
            var strJson = "";
            //strJson += "{\"PCompany\":\"" + _company + "\",\"PDept\":\"" + _dept + "\",\"PVenture\":\"" + _venture + "\",\"PName\":\"" + _name + "\",\"PCode\":\"" + _cardId + "\",";
            //strJson += "\"PTime\":\"" + getNowFormatDate() + "\",\"OddNo\":\"" + orderNum + "\",\"PickStat\":\"待备餐\",\"data\":[";
            strJson += "[";
            var tmp = "";
            $("li.picked").each(function () {

                tmp = $(this).attr("id");

                strJson += "{\"pdid\":\"" + tmp + "\",\"MStock\":\"" + $("select.PStock" + tmp).val() + "\" , \"Pnum\":\"" + $("input.num" + tmp).val() + "\"},";
            })

            

            strJson = strJson.substr(0, strJson.length - 1);
            strJson += "]";
            //strJson += "]}";
            //ajax后台数据处理
            //console.log(strJson);

            //alert(strJson);

            $.post("../api/KmrStorage/confirm_sumbit_BL.aspx",
            { PCompany: _company, PDept: _dept, PVenture: _venture, PName: _name, PCode: _cardId, sapCode: _venSapId,  dataList: strJson },
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
        

</script>


    </body>
</html>
