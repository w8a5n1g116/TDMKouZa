﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

// 
// 此源代码是由 Microsoft.VSDesigner 4.0.30319.42000 版自动生成。
// 
#pragma warning disable 1591

namespace DDpage.ProductOrderYuLiu {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    // CODEGEN: 未处理命名空间“http://schemas.xmlsoap.org/ws/2004/09/policy”中的可选 WSDL 扩展元素“Policy”。
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3056.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="ZZXPRODUCTRESB", Namespace="urn:sap-com:document:sap:rfc:functions")]
    public partial class ZZXPRODUCTRESB : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback ZPP_RESBOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public ZZXPRODUCTRESB() {
            this.Url = global::DDpage.Properties.Settings.Default.DDpage_ProductOrderYuLiu_ZZXPRODUCTRESB;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event ZPP_RESBCompletedEventHandler ZPP_RESBCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:sap-com:document:sap:rfc:functions:ZZXPRODUCTRESB:ZPP_RESBRequest", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Bare)]
        [return: System.Xml.Serialization.XmlElementAttribute("ZPP_RESBResponse", Namespace="urn:sap-com:document:sap:rfc:functions")]
        public ZPP_RESBResponse ZPP_RESB([System.Xml.Serialization.XmlElementAttribute("ZPP_RESB", Namespace="urn:sap-com:document:sap:rfc:functions")] ZPP_RESB1 ZPP_RESB1) {
            object[] results = this.Invoke("ZPP_RESB", new object[] {
                        ZPP_RESB1});
            return ((ZPP_RESBResponse)(results[0]));
        }
        
        /// <remarks/>
        public void ZPP_RESBAsync(ZPP_RESB1 ZPP_RESB1) {
            this.ZPP_RESBAsync(ZPP_RESB1, null);
        }
        
        /// <remarks/>
        public void ZPP_RESBAsync(ZPP_RESB1 ZPP_RESB1, object userState) {
            if ((this.ZPP_RESBOperationCompleted == null)) {
                this.ZPP_RESBOperationCompleted = new System.Threading.SendOrPostCallback(this.OnZPP_RESBOperationCompleted);
            }
            this.InvokeAsync("ZPP_RESB", new object[] {
                        ZPP_RESB1}, this.ZPP_RESBOperationCompleted, userState);
        }
        
        private void OnZPP_RESBOperationCompleted(object arg) {
            if ((this.ZPP_RESBCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ZPP_RESBCompleted(this, new ZPP_RESBCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.3056.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="urn:sap-com:document:sap:rfc:functions")]
    public partial class ZPP_RESB1 {
        
        private string z_AUFNRField;
        
        private ZPP_RESB[] z_RESBINFOField;
        
        private string z_RSNUMField;
        
        private string z_VORNRField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Z_AUFNR {
            get {
                return this.z_AUFNRField;
            }
            set {
                this.z_AUFNRField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("item", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=false)]
        public ZPP_RESB[] Z_RESBINFO {
            get {
                return this.z_RESBINFOField;
            }
            set {
                this.z_RESBINFOField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Z_RSNUM {
            get {
                return this.z_RSNUMField;
            }
            set {
                this.z_RSNUMField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Z_VORNR {
            get {
                return this.z_VORNRField;
            }
            set {
                this.z_VORNRField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.3056.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:sap-com:document:sap:rfc:functions")]
    public partial class ZPP_RESB {
        
        private string aUFNRField;
        
        private string bAUGRField;
        
        private string vORNRField;
        
        private string rSPOSField;
        
        private string pOSNRField;
        
        private string pRODUCE_MAKTXField;
        
        private decimal gAMNGField;
        
        private string gMEINField;
        
        private string aUFPLField;
        
        private string rSNUMField;
        
        private string mATNRField;
        
        private string rEQUIRE_MAKTXField;
        
        private decimal bDMNGField;
        
        private decimal eNMNGField;
        
        private string mEINSField;
        
        private string bDTERField;
        
        private string vAPLZField;
        
        private string wERKSField;
        
        private string lGORTField;
        
        private string mATKLField;
        
        private string kDAUFField;
        
        private string kDPOSField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string AUFNR {
            get {
                return this.aUFNRField;
            }
            set {
                this.aUFNRField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string BAUGR {
            get {
                return this.bAUGRField;
            }
            set {
                this.bAUGRField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string VORNR {
            get {
                return this.vORNRField;
            }
            set {
                this.vORNRField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string RSPOS {
            get {
                return this.rSPOSField;
            }
            set {
                this.rSPOSField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string POSNR {
            get {
                return this.pOSNRField;
            }
            set {
                this.pOSNRField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string PRODUCE_MAKTX {
            get {
                return this.pRODUCE_MAKTXField;
            }
            set {
                this.pRODUCE_MAKTXField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal GAMNG {
            get {
                return this.gAMNGField;
            }
            set {
                this.gAMNGField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string GMEIN {
            get {
                return this.gMEINField;
            }
            set {
                this.gMEINField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string AUFPL {
            get {
                return this.aUFPLField;
            }
            set {
                this.aUFPLField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string RSNUM {
            get {
                return this.rSNUMField;
            }
            set {
                this.rSNUMField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string MATNR {
            get {
                return this.mATNRField;
            }
            set {
                this.mATNRField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string REQUIRE_MAKTX {
            get {
                return this.rEQUIRE_MAKTXField;
            }
            set {
                this.rEQUIRE_MAKTXField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal BDMNG {
            get {
                return this.bDMNGField;
            }
            set {
                this.bDMNGField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal ENMNG {
            get {
                return this.eNMNGField;
            }
            set {
                this.eNMNGField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string MEINS {
            get {
                return this.mEINSField;
            }
            set {
                this.mEINSField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string BDTER {
            get {
                return this.bDTERField;
            }
            set {
                this.bDTERField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string VAPLZ {
            get {
                return this.vAPLZField;
            }
            set {
                this.vAPLZField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string WERKS {
            get {
                return this.wERKSField;
            }
            set {
                this.wERKSField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string LGORT {
            get {
                return this.lGORTField;
            }
            set {
                this.lGORTField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string MATKL {
            get {
                return this.mATKLField;
            }
            set {
                this.mATKLField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string KDAUF {
            get {
                return this.kDAUFField;
            }
            set {
                this.kDAUFField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string KDPOS {
            get {
                return this.kDPOSField;
            }
            set {
                this.kDPOSField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.3056.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="urn:sap-com:document:sap:rfc:functions")]
    public partial class ZPP_RESBResponse {
        
        private string e_MSGTXField;
        
        private string e_MSGTYField;
        
        private ZPP_RESB[] z_RESBINFOField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string E_MSGTX {
            get {
                return this.e_MSGTXField;
            }
            set {
                this.e_MSGTXField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string E_MSGTY {
            get {
                return this.e_MSGTYField;
            }
            set {
                this.e_MSGTYField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("item", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=false)]
        public ZPP_RESB[] Z_RESBINFO {
            get {
                return this.z_RESBINFOField;
            }
            set {
                this.z_RESBINFOField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3056.0")]
    public delegate void ZPP_RESBCompletedEventHandler(object sender, ZPP_RESBCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3056.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ZPP_RESBCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal ZPP_RESBCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public ZPP_RESBResponse Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((ZPP_RESBResponse)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591