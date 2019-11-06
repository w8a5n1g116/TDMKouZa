<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="DDpage.DD_example.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="maximum-scale=1.0,minimum-scale=1.0,user-scalable=0,width=device-width,initial-scale=1.0" />
    <meta name="format-detection" content="telephone=no,email=no,date=no,address=no" />
   
    <title></title>
    <link href="../css/Styles.css" rel="stylesheet" />
    <link href="../css/font/iconfont.css" rel="stylesheet" />
    <style type="text/css">
        @keyframes fade-in {
            0% {
                opacity: 0;
            }
            /*初始状态 透明度为0*/
            40% {
                opacity: 0;
            }
            /*过渡状态 透明度为0*/
            100% {
                opacity: 1;
            }
            /*结束状态 透明度为1*/
        }

        @-webkit-keyframes fade-in { /*针对webkit内核*/
            0% {
                opacity: 0;
            }

            40% {
                opacity: 0;
            }

            100% {
                opacity: 1;
            }
        }

        .logo {
            width: auto;
            height: 2.5em;
            margin-top: 1em;
            margin-left: 1em;
        }

        .page_text {
            padding: 1em 2.5em;
            line-height: 1.5em;
            color: #848283;
            font-size: 1.1em;
        }

        .line {
            display:flex;
            flex-direction:column;
            margin: 1em;
        }

        .box {
            display: flex;
            flex-direction: row;
            align-items: center;
            justify-content: space-between;
            background: #fff;
            padding: 1em;
            font-size: 1.2em;
            color: #393532;
        }

        .box_text {
            height:auto;
            background:#EAEAEA;
            padding: 1em;
            display:none;
            word-break:break-all;
        }

        .button {
            padding:0.5em 1em;
            display:flex;
            justify-content:center;
            align-items:center;
            background:#EF9A85;
            color:#fff;
            min-width:6em;
            max-width:8em;
            margin-top:2em;
        }
    </style>
</head>
<body>
    <div style="padding: 0.5em">
        <img src="../img/logo.png" class="logo" />
    </div>
    <div class="page_text">
        本页面为钉钉内移动应用开发组件集成展示，各功能事例与样式仅供参考。引用事例请查看对应说明，界面样式请按公司要求参照相关文档。
    </div>
    <div class="line">
        <div class="box" onclick="box_click(this)">
            <div>引用</div>
            <div><i class="icon iconfont icon-unfold"></i></div>
        </div>
        <div class="box_text">
            引用方法：<br/>
            http://xxx.xxx.xxx/DDpage/index?title=xxx&appid=xxxxxxx&url=http://xxx.xxx.xxx
            <br/>
            参数说明:<br/>
            title 应用标题<br/>
            appid 钉钉中应用appid<br/>
            http 应用实际网址<br/>
        </div>
    </div>
    <div class="line">
        <div class="box" onclick="box_click(this)">
            <div>获取用户信息</div>
            <div><i class="icon iconfont icon-unfold"></i></div>
        </div>
        <div class="box_text">
            调用方法：<br/>
            parent.getUserInfo(function(re){})<br/>
            re为用户信息，具体参数如下：<br/>
            {<br/>
                emplId 人员编号<br/>
                avatar 人员头像<br/>
                corpId 企业ID<br/>
                id 个人id<br/>
                isAuth 用户可信<br/>
                isManager 经理<br/>
                nickName 用户昵称<br/>
                phone 用户手机号<br/>
            }<br/>
            <div class="button" name="userinfo">获取人员信息</div>
        </div>
    </div>

    <div class="line">
        <div class="box" onclick="box_click(this)">
            <div>消息提示</div>
            <div><i class="icon iconfont icon-unfold"></i></div>
        </div>
        <div class="box_text">
            调用方法：<br/>
            parent.ddalert(message,title,button)<br/>
            参数说明：<br/>
                message 提示内容<br/>
                title 消息标题<br/>
                button 按钮文字<br/>
              事例： parent.ddalert("事例消息","事例标题","好的")<br/>
            <div class="button" name="alert">发送提示</div>
        </div>
    </div>

    <div class="line">
        <div class="box" onclick="box_click(this)">
            <div>加载Toast与取消Toast</div>
            <div><i class="icon iconfont icon-unfold"></i></div>
        </div>
        <div class="box_text">
            调用方法：<br/>
            显示加载中：parent.page_load()<br/>
            取消显示：parent.load_hide()<br/>
            事例说明：<br/>
            显示加载2秒后，结束显示。<br/>
            <div class="button" name="load">弹出加载框</div>
        </div>
    </div>

    <div class="line">
        <div class="box" onclick="box_click(this)">
            <div>成功Toast与失败Toast</div>
            <div><i class="icon iconfont icon-unfold"></i></div>
        </div>
        <div class="box_text">
            调用方法：<br/>
            成功提示：parent.page_success(title)<br/>
            失败显示：parent.fail(title)<br/>
            参数说明：<br/>
            title 提示信息<br/>
            事例：<br/>
            parent.page_success("成功")<br/>
            parent.fail("失败")<br/>
            <div class="button" name="success">弹出成功框</div>
             <div class="button" name="fail">弹出失败框</div>
        </div>
    </div>

    <div class="line">
        <div class="box" onclick="box_click(this)">
            <div>日期选择器</div>
            <div><i class="icon iconfont icon-unfold"></i></div>
        </div>
        <div class="box_text">
            调用方法：<br/>
            parent.dddatepicker(date,function(re){})<br/>
            参数说明：<br/>
            date 默认显示日期，格式"yyyy-MM-dd"<br/>
            re 返回值<br/>
            事例：parent.dddatepicker("2017-06-06",function(re){alert(JSON.stringify(re))})
            <div class="button" name="datepicker">弹出选项框</div>
        </div>
    </div>
    <div class="line">
        <div class="box" onclick="box_click(this)">
            <div>时间选择器</div>
            <div><i class="icon iconfont icon-unfold"></i></div>
        </div>
        <div class="box_text">
            调用方法：<br/>
            parent.ddtimepicker(date,function(re){})<br/>
            参数说明：<br/>
            date 默认显示日期，格式"HH:mm"<br/>
            re 返回值<br/>
            事例：parent.ddtimepicker("08:11",function(re){alert(JSON.stringify(re))})
            <div class="button" name="timepicker">弹出选项框</div>
        </div>
    </div>
    <div class="line">
        <div class="box" onclick="box_click(this)">
            <div>日期时间选择器</div>
            <div><i class="icon iconfont icon-unfold"></i></div>
        </div>
        <div class="box_text">
            调用方法：<br/>
            parent.dddatetimepicker（datetime,function(re){})<br/>
            参数说明：<br/>
            datetime 默认显示日期，格式"yyyy-MM-dd HH:mm"<br/>
            re 返回值<br/>
            事例：parent.dddatetimepicker("2017-06-06 08:11",function(re){alert(JSON.stringify(re))})
            <div class="button" name="datetimepicker">弹出选项框</div>
        </div>
    </div>
    <div class="line">
        <div class="box" onclick="box_click(this)">
            <div>月份选择器</div>
            <div><i class="icon iconfont icon-unfold"></i></div>
        </div>
        <div class="box_text">
            调用方法：<br/>
            parent.monthpicker（function(re){})<br/>
            参数说明：<br/>
            re 返回值<br/>
            事例：parent.monthpicker(function(re){alert(JSON.stringify(re))})
            <div class="button" name="monthpicker">弹出选项框</div>
        </div>
    </div>
    <div class="line">
        <div class="box" onclick="box_click(this)">
            <div>扫二维码</div>
            <div><i class="icon iconfont icon-unfold"></i></div>
        </div>
        <div class="box_text">
            调用方法：<br/>
            parent.scan（function(re){})<br/>
            参数说明：<br/>
            re 二维码文本<br/>
            事例：parent.scan(function(re){alert(JSON.stringify(re))})
            <div class="button" name="scan">打开“扫一扫”</div>
        </div>
    </div>

    <div class="line">
        <div class="box" onclick="box_click(this)">
            <div>获取地理位置</div>
            <div><i class="icon iconfont icon-unfold"></i></div>
        </div>
        <div class="box_text">
            调用方法：<br/>
            parent.location_gets（function(re){})<br/>
            参数说明：<br/>
            *返回的地理坐标为高德地图坐标<br/>
            re 地理信息<br/>
            事例：parent.location_gets(function(re){alert(JSON.stringify(re))})
            <div class="button" name="location">获取地理位置</div>
        </div>
    </div>

    <div class="line">
        <div class="box" onclick="box_click(this)">
            <div>图片预览</div>
            <div><i class="icon iconfont icon-unfold"></i></div>
        </div>
        <div class="box_text">
            调用方法：<br/>
            parent.previewImage（ImgUrl)<br/>
            参数说明：<br/>
            ImgUrl 图片地址<br/>
            事例：parent.previewImage("http://www.kocelcloud.com/file/image/20171017/1508210304055027190.jpg")
            <div class="button" name="previewimage">预览图片</div>
        </div>
    </div>

    <div class="line">
        <div class="box" onclick="box_click(this)">
            <div>拍照并上传图片</div>
            <div><i class="icon iconfont icon-unfold"></i></div>
        </div>
        <div class="box_text">
            调用方法：<br/>
            parent.uploadImageFromCamera(function(re){})<br/>
            参数说明：<br/>
            re 返回图片网址<br/>
            事例：parent.uploadImageFromCamera(function(re){
                        alert(re[0])
                        parent.previewImage(re[0]);
            })
            <div class="button" name="camera">拍照上传图片</div>
        </div>
    </div>

    <div class="line">
        <div class="box" onclick="box_click(this)">
            <div>上传图片</div>
            <div><i class="icon iconfont icon-unfold"></i></div>
        </div>
        <div class="box_text">
            调用方法：<br/>
            parent.uploadImages(function(re){})<br/>
            参数说明：<br/>
            re 返回图片网址<br/>
            事例：parent.uploadImages(function(re){
                        alert(re[0])
                        parent.previewImage(re[0]);
            })
            <div class="button" name="upimg">上传图片</div>
        </div>
    </div>
</body>
<script src="../js/jquery-1.11.0.min.js"></script>
<script type="text/javascript">
    $(function () {
        $("div[name]").click(function () {

            switch ($(this).attr("name")) {
                case "userinfo": {
                    parent.getUserInfo(function (re) {
                        alert(JSON.stringify(re));
                    })
                    break;
                }
                case "alert": {
                    parent.ddalert("事例消息", "事例标题", "好的");
                    break;
                }
                case "load": {
                    parent.page_load();
                    setTimeout(function () {
                        parent.load_hide();
                    }, 2000);
                    break;
                }
                case "success": {
                    parent.page_success("成功");
                    break;
                }
                case "fail": {
                    parent.page_fail("失败");
                    break;
                }
                case "fail": {
                    parent.page_fail("失败");
                    break;
                }
                case "datepicker": {
                    parent.dddatepicker("2017-06-06", function (re) { alert(JSON.stringify(re)) })
                    break;
                }
                case "timepicker": {
                    parent.ddtimepicker("08:11", function (re) { alert(JSON.stringify(re)) })
                    break;
                }
                case "datetimepicker": {
                    parent.dddatetimepicker("2017-06-06 08:11", function (re) { alert(JSON.stringify(re)) })
                    break;
                }
                case "monthpicker": {
                    parent.monthpicker(function (re) {
                        alert(JSON.stringify(re));
                    });
                    break;
                }
                case "scan": {
                    parent.scan(function (re) {
                        alert(JSON.stringify(re));
                    });
                    break;
                }
                case "location": {
                    parent.location_gets(function (re) { alert(JSON.stringify(re)) });
                    break;
                }
                case "previewimage": {
                    parent.previewImage("http://www.kocelcloud.com/file/image/20171017/1508210304055027190.jpg");
                    break;
                }
                case "camera": {
                    parent.uploadImageFromCamera(function (re) {
                        alert(re[0])
                        parent.previewImage(re[0]);
                    });
                    break;
                }
                case "upimg": {
                    parent.uploadImages(function (re) {
                        alert(re[0])
                        parent.previewImage(re[0]);
                    });
                    break;
                }
            }
        })
    })
    function box_click(obj) {
        if ($(obj).find('i').attr("class") == "icon iconfont icon-unfold") {
            $(obj).next().fadeIn();
            var h = $(document).scrollTop() + $(obj).next().height();
            $(document).scrollTop(h);
            $(obj).find('i').attr("class", "icon iconfont icon-packup");
        }
        else {
            $(obj).next().fadeOut();
            var h = $(document).scrollTop() - $(obj).next().height();
            $(document).scrollTop(h);
            $(obj).find('i').attr("class", "icon iconfont icon-unfold");
        }
        
    }
    
</script>
</html>
