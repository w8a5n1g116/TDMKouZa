/**
 * aui-dialog.js
 * @author 流浪男
 * @todo more things to abstract, e.g. Loading css etc.
 * Licensed under the MIT license.
 * http://www.opensource.org/licenses/mit-license.php
 */
(function( window, undefined ) {
    "use strict";
    var auiDialog = function() {
    };
    var isShow = false;
    auiDialog.prototype = {
        params: {
            title:'',
            msg:'',
            buttons: ['取消','确定'],
            input:false
        },
        create: function(params,callback) {
        	var self = this;
            var dialogHtml = '';
            var buttonsHtml = '';
            var headerHtml = params.title ? '<div class="aui-dialog-header">' + params.title + '</div>' : '<div class="aui-dialog-header">' + self.params.title + '</div>';
            if(params.input){
                params.text = params.text ? params.text : '';

                var tt = '<select id="wc" style="border: 1px solid #dddddd;height: 1.8rem;line-height: 1.8rem;min-height: 1.8rem;padding-left: 0.25rem;padding-right: 0.25rem;" ><option value="计划类别">点击选择评价结果</option><option value="优">优</option><option value="良">良</option><option value="中">中</option><option value="差">差</option></select>';
                var td = '<select id="jg" style="border: 1px solid #dddddd;height: 1.8rem;line-height: 1.8rem;min-height: 1.8rem;padding-left: 0.25rem;padding-right: 0.25rem;"><option value="计划类别">点击选择完成判断</option><option value="Yes">Yes</option><option value="Doing">Doing</option><option value="No">No</option></select>'
                var msgHtml = '<div class="aui-dialog-body"><input type="text" id="kh_name" placeholder="' + params.text1 + '"></div><div class="aui-dialog-body"><input type="number" id="kh_money" placeholder="' + params.text2 + '"></div><div class="aui-dialog-body">' + tt + '</div><div class="aui-dialog-body">' + td + '</div>';
            }else{
                var msgHtml = params.msg ? '<div class="aui-dialog-body">' + params.msg + '</div>' : '<div class="aui-dialog-body">' + self.params.msg + '</div>';
            }
            var buttons = params.buttons ? params.buttons : self.params.buttons;
            if (buttons && buttons.length > 0) {
                
                for (var i = 0; i < buttons.length; i++) {
                    buttonsHtml += '<div class="aui-dialog-btn" tapmode button-index="'+i+'">'+buttons[i]+'</div>';
                }
                
            }
            var footerHtml = '<div class="aui-dialog-footer">'+buttonsHtml+'</div>';
            dialogHtml = '<div class="aui-dialog">' + headerHtml + msgHtml + footerHtml + '</div></div>';
            console.log(dialogHtml);
            document.body.insertAdjacentHTML('BeforeEnd', dialogHtml);
            console.log("2");
            // listen buttons click
            var dialogButtons = document.querySelectorAll(".aui-dialog-btn");
            if(dialogButtons && dialogButtons.length > 0){
                for(var ii = 0; ii < dialogButtons.length; ii++){
                    dialogButtons[ii].onclick = function(){
                        if(callback){
                            if(params.input){
                                callback({
                                    buttonIndex: parseInt(this.getAttribute("button-index"))+1,
                                    text1: document.querySelector("#kh_name").value,
                                    text2: document.querySelector("#kh_money").value,
                                    text3: $("#wc").find("option:selected").text(),
                                    text4: $("#jg").find("option:selected").text(),
                                });
                            }else{
                                callback({
                                    buttonIndex: parseInt(this.getAttribute("button-index"))+1
                                });
                            }
                        };
                        self.close();
                        return;
                    }
                }
            }
            self.open();
        },
        creates: function(params,callback) {
            var self = this;
            var dialogHtml = '';
            var buttonsHtml = '';
            var headerHtml = params.title ? '<div class="aui-dialog-header">' + params.title + '</div>' : '<div class="aui-dialog-header">' + self.params.title + '</div>';
            if(params.input){
                params.text = params.text ? params.text : '';
                var msgHtml = '<div class="aui-dialog-body"><input type="text" id="text1" placeholder="' + params.text1 + '"></div><div class="aui-dialog-body"><input type="number" id="text2" placeholder="' + params.text2 + '"></div>';
               }else{
                var msgHtml = params.msg ? '<div class="aui-dialog-body">' + params.msg + '</div>' : '<div class="aui-dialog-body">' + self.params.msg + '</div>';
            }
            var buttons = params.buttons ? params.buttons : self.params.buttons;
            if (buttons && buttons.length > 0) {
                
                for (var i = 0; i < buttons.length; i++) {
                    buttonsHtml += '<div class="aui-dialog-btn" tapmode button-index="'+i+'">'+buttons[i]+'</div>';
                }
                
            }
            var footerHtml = '<div class="aui-dialog-footer">'+buttonsHtml+'</div>';
            dialogHtml = '<div class="aui-dialog">' + headerHtml + msgHtml + footerHtml + '</div></div>';
            console.log(dialogHtml);
            document.body.insertAdjacentHTML('BeforeEnd', dialogHtml);
            console.log("2");
            // listen buttons click
            var dialogButtons = document.querySelectorAll(".aui-dialog-btn");
            if(dialogButtons && dialogButtons.length > 0){
                for(var ii = 0; ii < dialogButtons.length; ii++){
                    dialogButtons[ii].onclick = function(){
                        if(callback){
                            if(params.input){
                                callback({
                                    buttonIndex: parseInt(this.getAttribute("button-index"))+1,
                                    text1: document.querySelector("#text1").value,
                                    text2: document.querySelector("#text2").value,
                                });
                            }else{
                                callback({
                                    buttonIndex: parseInt(this.getAttribute("button-index"))+1
                                });
                            }
                        };
                        self.close();
                        return;
                    }
                }
            }
            self.open();
        },
        open: function(){
            if(!document.querySelector(".aui-dialog"))return;
            var self = this;
            document.querySelector(".aui-dialog").style.marginTop =  "-"+Math.round(document.querySelector(".aui-dialog").offsetHeight/2)+"px";
            if(!document.querySelector(".aui-mask")){
                var maskHtml = '<div class="aui-mask"></div>';
                document.body.insertAdjacentHTML('beforeend', maskHtml);
            }
            // document.querySelector(".aui-dialog").style.display = "block";
            setTimeout(function(){
                document.querySelector(".aui-dialog").classList.add("aui-dialog-in");
                document.querySelector(".aui-mask").classList.add("aui-mask-in");
                document.querySelector(".aui-dialog").classList.add("aui-dialog-in");
            }, 10)
            document.querySelector(".aui-mask").addEventListener("touchmove", function(e){
                e.preventDefault();
            })
            document.querySelector(".aui-dialog").addEventListener("touchmove", function(e){
                e.preventDefault();
            })
            return;
        },
        close: function(){
            var self = this;
            document.querySelector(".aui-mask").classList.remove("aui-mask-in");
            document.querySelector(".aui-dialog").classList.remove("aui-dialog-in");
            document.querySelector(".aui-dialog").classList.add("aui-dialog-out");
            if (document.querySelector(".aui-dialog:not(.aui-dialog-out)")) {
                setTimeout(function(){
                    if(document.querySelector(".aui-dialog"))document.querySelector(".aui-dialog").parentNode.removeChild(document.querySelector(".aui-dialog"));
                    self.open();
                    return true;
                },200)
            }else{
                document.querySelector(".aui-mask").classList.add("aui-mask-out");
                document.querySelector(".aui-dialog").addEventListener("webkitTransitionEnd", function(){
                    self.remove();
                })
                document.querySelector(".aui-dialog").addEventListener("transitionend", function(){
                    self.remove();
                })
            }
        },
        remove: function(){
            if(document.querySelector(".aui-dialog"))document.querySelector(".aui-dialog").parentNode.removeChild(document.querySelector(".aui-dialog"));
            if(document.querySelector(".aui-mask")){
                document.querySelector(".aui-mask").classList.remove("aui-mask-out");
            }
            return true;
        },
        alert: function(params,callback){
        	var self = this;
            return self.create(params,callback);
        },
        prompt: function (params, callback) {
           
            var self = this;
            params.input = true;
            return self.create(params,callback);
        },
        prompts: function (params, callback) {
           
            var self = this;
            params.input = true;
            return self.creates(params,callback);
        }
    };
	window.auiDialog = auiDialog;
})(window);