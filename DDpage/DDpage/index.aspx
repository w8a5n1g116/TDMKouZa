 <%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="DDpage.page.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="maximum-scale=1.0,minimum-scale=1.0,user-scalable=0,width=device-width,initial-scale=1.0" />
    <meta name="format-detection" content="telephone=no,email=no,date=no,address=no" />
    <meta name="viewport" content="width=device-width,initial-scale=1,user-scalable=no" />
    <title></title>
    <link href="../css/Styles.css" rel="stylesheet" />
    <link href="../css/aui.css" rel="stylesheet" />
    <link href="../css/mobileSelect.css" rel="stylesheet" />
    
    <style type="text/css">
        .aui-toast {
            padding:0 1em;
            margin:-4.75em;
        }
        .mobileSelect .content {
            bottom: 0;
        }
    </style>
</head>
<body>
    <div id="month"></div>
    <div class="page_load">
        <div class='base'>
            <div class='cube'></div>
            <div class='cube'></div>
            <div class='cube'></div>
            <div class='cube'></div>
            <div class='cube'></div>
            <div class='cube'></div>
            <div class='cube'></div>
            <div class='cube'></div>
            <div class='cube'></div>
        </div>
    </div>
    <div class="main_page" style="display:none"></div>
</body>
<script src="../js/jquery-1.11.0.min.js"></script>
<script src="../js/dingtalk.js"></script>
<script src="../js/ddjs.js"></script>
<script src="../js/mobileSelect.js"></script>
<script src="../js/api.js"></script>
<script src="../js/aui-toast.js"></script>
<script type="text/javascript">
    var _config = {
        appId: '<%=appId%>',
        corpId: '<%=corpId%>',
        timeStamp: '<%=timestamp%>',
        nonce: '<%=nonceStr%>',
        signature: '<%=signature%>',
        url: '<%=url%>',
        title:'<%=title%>'
    };
    var get;
    var toast = new auiToast();
    dd.config({
        appId: _config.appId,
        corpId: _config.corpId,
        timeStamp: _config.timeStamp,
        nonceStr: _config.nonce,
        signature: _config.signature,
        jsApiList: ['biz.user.get', 'device.geolocation.get', 'biz.map.locate', 'biz.util.uploadImageFromCamera', 'biz.util.uploadImage']
    });

    alert("111111");
    window.location.href = "../KmrStorage/content.aspx" 
    //$(function () {
    //    $(document).attr("title", _config.title);
    //    setTimeout(function () {
    //        //getUserInfo(function (re) {
    //        //    
    //        //});
    //        $(".main_page").html('<iframe src="' + _config.url + '" id="myiframe"></iframe>');
    //            var frm = document.getElementById('myiframe');
    //            $(frm).load(function () {                             //  等iframe加载完毕  
    //                $(".page_load").css("display", "none");
    //                $(".main_page").css("display", "block");
    //            });
    //    }, 1000);
    //})



    function getUserInfo(callback) {
        getUser(function (re) {
            callback(re)
        });
    }
    function ddalert(message, title, button) {
        ddalerts(message,title,button);
    }

    function page_load() {
        toast.loading({
            title: "加载中",
            duration: 2000
        });
    }

    function load_hide() {
        toast.hide();
    }
    function page_success(title) {
        toast.success({
            title: title,
            duration: 2000
        });
    }
    function page_fail(title) {
        toast.fail({
            title: title,
            duration: 2000
        });
    }
    function dddatepicker(date,callback) {
        datepickers(date, function (re) {
            callback(re)
        });
    }
    function ddtimepicker(time, callback) {
        timepickers(time, function (re) {
            callback(re)
        });
    }
    function dddatetimepicker(datetime, callback) {
        datetimepickers(datetime, function (re) {
            callback(re)
        });
    }
    function monthpicker(callback) {
        var mydate = new Date();
        var month =[];
        var year =[];
        for (var i = 5; i > -5; i--) {
            year.push(mydate.getFullYear() - i);
        }
        for (var i = 1; i <= 12; i++) {
            month.push(i);
        }
        var mobileSelect = new MobileSelect({
            trigger: '#month',
            title: '日期选择',
            wheels: [
                        { data: year },
                        { data: month }
            ],
            position: [5, mydate.getMonth()],
            transitionEnd: function (indexArr, data) {
                
            },
            callback: function (indexArr, data) {
                $(".mobileSelect").removeClass("mobileSelect-show");
                callback({value:data[0] + '-' + data[1]});
            }
        });
        $(".mobileSelect").addClass("mobileSelect-show");
    }
    function location_gets(callback) {
        location_get(function (re) {
            callback(re);
        })
    }
    function map_gets(callback) {
        map_get(function (re) {
            callback(re);
        });
    }
    function scan(callback) {
        scans(function (re) {
            callback(re);
        });
    }

    function previewImage(url) {
        previewImages(url);
    }

    function uploadImageFromCamera(callback) {
        uploadImageFromCameras(function (re) {
            callback(re)
        });
    }
    function uploadImage(callback) {
        uploadImages(function (re) {
            callback(re);
        });
    }
</script>

</html>
