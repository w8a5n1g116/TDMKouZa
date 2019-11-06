/// <reference path="../api/public/user_info.aspx" />
var getUser;

dd.ready(function () {
    dd.biz.navigation.setLeft({
        control: true,//是否控制点击事件，true 控制，false 不控制， 默认false
        text: '关闭',//控制显示文本，空字符串表示显示默认文本
        onSuccess: function (result) {
            dd.biz.navigation.close({
                onSuccess: function (result) {

                },
                onFail: function (err) { }
            })
        },
        onFail: function (err) { }
    });
    document.addEventListener('backbutton', function (e) {
        e.preventDefault();
        dd.biz.navigation.close({
            onSuccess: function (result) {
            },
            onFail: function (err) { }
        })
    }, false);

    getUser = function (callback) {
        dd.biz.user.get({
            onSuccess: function (info) {
                $.ajax({
                    data: { userid: info.emplId },
                    type:"POST",
                    url: "../api/public/user_info.aspx",
                    async: false,
                    success: function (fd) {
                        info.phone = fd;
                        return;
                    }
                })
                callback(info);
                return;
            },
            onFail: function (err) {
                
            }
        });
    }

    ddalerts = function (message,title,button) {
        dd.device.notification.alert({
            message: message,
            title: title,//可传空
            buttonName: button,
            onSuccess: function () {
                //onSuccess将在点击button之后回调
                /*回调*/
            },
            onFail: function (err) { }
        });
    }
    datepickers = function (date,callback) {
        dd.biz.util.datepicker({
            format: 'yyyy-MM-dd',
            value: date, //默认显示日期
            onSuccess: function (result) {
                callback(result)
            },
            onFail: function (err) { }
        })
    }
    timepickers = function (time, callback) {
        dd.biz.util.timepicker({
            format: 'HH:mm',
            value: time, //默认显示时间  0.0.3
            onSuccess: function (result) {
                callback(result)
            },
            onFail: function (err) { }
        })
    }
    datetimepickers = function (datetime, callback) {
        dd.biz.util.datetimepicker({
            format: 'yyyy-MM-dd HH:mm',
            value: datetime, //默认显示
            onSuccess: function (result) {
                callback(result)
            },
            onFail: function (err) { }
        })
    }
    location_get = function (callback) {
        dd.device.geolocation.get({
            targetAccuracy: 200,
            coordinate: 1,
            withReGeocode: false,
            useCache: true, //默认是true，如果需要频繁获取地理位置，请设置false
            onSuccess: function (result) {
               
                callback(result);
            },
            onFail: function (err) { }
        });
    }
    map_get = function (callback) {
        
        dd.biz.map.locate({
            latitude: 38.48263047960069, // 纬度
            longitude: 106.1127175564236, // 经度
            onSuccess: function (result) {
                /* result 结构 */
               callback(result)
            },
            onFail: function (err) {
            }
        });
    }
    scans = function (callback) {
        dd.biz.util.scan({
            type: "all", // type 为 all、qrCode、barCode，默认是all。
            onSuccess: function (data) {
               callback(data)
            },
            onFail: function (err) {
            }
        })
    }
    previewImages = function (url) {
        dd.biz.util.previewImage({
            urls: [url],//图片地址列表
            current: url,//当前显示的图片链接
            onSuccess: function (result) {
                /**/
            },
            onFail: function (err) { }
        })
    }
    uploadImageFromCameras = function (callback) {
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
    uploadImages = function (callback) {
        dd.biz.util.uploadImage({
            compression: true,//(是否压缩，默认为true)
            multiple: false, //是否多选，默认false
            max: 3, //最多可选个数
            quality: 50, // 图片压缩质量, 
            resize: 50, // 图片缩放率
            stickers: {   // 水印信息
            },
            onSuccess: function (result) {
                callback(result)
            },
            onFail: function (err) { }
        })
    }
})