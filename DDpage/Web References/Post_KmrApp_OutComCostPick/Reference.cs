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

namespace DDpage.Post_KmrApp_OutComCostPick {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2558.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="SI_ZPP_GOODSMVT_201Binding", Namespace="http://www.kocel.com/lims/zpp_goodsmvt_201")]
    public partial class SI_ZPP_GOODSMVT_201Service : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback SI_ZPP_GOODSMVT_201OperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public SI_ZPP_GOODSMVT_201Service() {
            this.Url = global::DDpage.Properties.Settings.Default.DDpage_Post_KmrApp_OutComCostPick_SI_ZPP_GOODSMVT_201Service;
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
        public event SI_ZPP_GOODSMVT_201CompletedEventHandler SI_ZPP_GOODSMVT_201Completed;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://sap.com/xi/WebService/soap1.1", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Bare)]
        [return: System.Xml.Serialization.XmlElementAttribute("ZPP_GOODSMVT_201Response", Namespace="urn:sap-com:document:sap:rfc:functions")]
        public ZPP_GOODSMVT_201Response SI_ZPP_GOODSMVT_201([System.Xml.Serialization.XmlElementAttribute(Namespace="urn:sap-com:document:sap:rfc:functions")] ZPP_GOODSMVT_201 ZPP_GOODSMVT_201) {
            object[] results = this.Invoke("SI_ZPP_GOODSMVT_201", new object[] {
                        ZPP_GOODSMVT_201});
            return ((ZPP_GOODSMVT_201Response)(results[0]));
        }
        
        /// <remarks/>
        public void SI_ZPP_GOODSMVT_201Async(ZPP_GOODSMVT_201 ZPP_GOODSMVT_201) {
            this.SI_ZPP_GOODSMVT_201Async(ZPP_GOODSMVT_201, null);
        }
        
        /// <remarks/>
        public void SI_ZPP_GOODSMVT_201Async(ZPP_GOODSMVT_201 ZPP_GOODSMVT_201, object userState) {
            if ((this.SI_ZPP_GOODSMVT_201OperationCompleted == null)) {
                this.SI_ZPP_GOODSMVT_201OperationCompleted = new System.Threading.SendOrPostCallback(this.OnSI_ZPP_GOODSMVT_201OperationCompleted);
            }
            this.InvokeAsync("SI_ZPP_GOODSMVT_201", new object[] {
                        ZPP_GOODSMVT_201}, this.SI_ZPP_GOODSMVT_201OperationCompleted, userState);
        }
        
        private void OnSI_ZPP_GOODSMVT_201OperationCompleted(object arg) {
            if ((this.SI_ZPP_GOODSMVT_201Completed != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SI_ZPP_GOODSMVT_201Completed(this, new SI_ZPP_GOODSMVT_201CompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.2634.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="urn:sap-com:document:sap:rfc:functions")]
    public partial class ZPP_GOODSMVT_201 {
        
        private ZSD_GOODSMVTMSG_201[] eT_MSGField;
        
        private BAPIRET2[] eT_RETURNField;
        
        private ZSD_GOODSMVTHEADER_201 iS_HEADField;
        
        private ZSD_GOODSMVTITEM_201[] iT_ITEMField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("item", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=false)]
        public ZSD_GOODSMVTMSG_201[] ET_MSG {
            get {
                return this.eT_MSGField;
            }
            set {
                this.eT_MSGField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("item", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=false)]
        public BAPIRET2[] ET_RETURN {
            get {
                return this.eT_RETURNField;
            }
            set {
                this.eT_RETURNField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public ZSD_GOODSMVTHEADER_201 IS_HEAD {
            get {
                return this.iS_HEADField;
            }
            set {
                this.iS_HEADField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("item", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=false)]
        public ZSD_GOODSMVTITEM_201[] IT_ITEM {
            get {
                return this.iT_ITEMField;
            }
            set {
                this.iT_ITEMField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.2634.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:sap-com:document:sap:rfc:functions")]
    public partial class ZSD_GOODSMVTMSG_201 {
        
        private string wERKSField;
        
        private string lGORTField;
        
        private string mATNRField;
        
        private string rSNUMField;
        
        private string gSBERField;
        
        private decimal zNEEDField;
        
        private decimal zOKField;
        
        private decimal zNOTField;
        
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
        public string GSBER {
            get {
                return this.gSBERField;
            }
            set {
                this.gSBERField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal ZNEED {
            get {
                return this.zNEEDField;
            }
            set {
                this.zNEEDField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal ZOK {
            get {
                return this.zOKField;
            }
            set {
                this.zOKField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public decimal ZNOT {
            get {
                return this.zNOTField;
            }
            set {
                this.zNOTField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.2634.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:sap-com:document:sap:rfc:functions")]
    public partial class ZSD_GOODSMVTITEM_201 {
        
        private string lGORTField;
        
        private string bATCHField;
        
        private string rSNUMField;
        
        private string rSPOSField;
        
        private decimal mENGEField;
        
        private string eNTRY_UOMField;
        
        private string pLANTField;
        
        private string mATNRField;
        
        private string cOSTCENTERField;
        
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
        public string BATCH {
            get {
                return this.bATCHField;
            }
            set {
                this.bATCHField = value;
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
        public decimal MENGE {
            get {
                return this.mENGEField;
            }
            set {
                this.mENGEField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ENTRY_UOM {
            get {
                return this.eNTRY_UOMField;
            }
            set {
                this.eNTRY_UOMField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string PLANT {
            get {
                return this.pLANTField;
            }
            set {
                this.pLANTField = value;
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
        public string COSTCENTER {
            get {
                return this.cOSTCENTERField;
            }
            set {
                this.cOSTCENTERField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.2634.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:sap-com:document:sap:rfc:functions")]
    public partial class ZSD_GOODSMVTHEADER_201 {
        
        private string rSNUMField;
        
        private string rSPOSField;
        
        private string lGORTField;
        
        private string gSBERField;
        
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
        public string GSBER {
            get {
                return this.gSBERField;
            }
            set {
                this.gSBERField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.2634.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:sap-com:document:sap:rfc:functions")]
    public partial class BAPIRET2 {
        
        private string tYPEField;
        
        private string idField;
        
        private string nUMBERField;
        
        private string mESSAGEField;
        
        private string lOG_NOField;
        
        private string lOG_MSG_NOField;
        
        private string mESSAGE_V1Field;
        
        private string mESSAGE_V2Field;
        
        private string mESSAGE_V3Field;
        
        private string mESSAGE_V4Field;
        
        private string pARAMETERField;
        
        private int rOWField;
        
        private string fIELDField;
        
        private string sYSTEMField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string TYPE {
            get {
                return this.tYPEField;
            }
            set {
                this.tYPEField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ID {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string NUMBER {
            get {
                return this.nUMBERField;
            }
            set {
                this.nUMBERField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string MESSAGE {
            get {
                return this.mESSAGEField;
            }
            set {
                this.mESSAGEField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string LOG_NO {
            get {
                return this.lOG_NOField;
            }
            set {
                this.lOG_NOField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string LOG_MSG_NO {
            get {
                return this.lOG_MSG_NOField;
            }
            set {
                this.lOG_MSG_NOField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string MESSAGE_V1 {
            get {
                return this.mESSAGE_V1Field;
            }
            set {
                this.mESSAGE_V1Field = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string MESSAGE_V2 {
            get {
                return this.mESSAGE_V2Field;
            }
            set {
                this.mESSAGE_V2Field = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string MESSAGE_V3 {
            get {
                return this.mESSAGE_V3Field;
            }
            set {
                this.mESSAGE_V3Field = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string MESSAGE_V4 {
            get {
                return this.mESSAGE_V4Field;
            }
            set {
                this.mESSAGE_V4Field = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string PARAMETER {
            get {
                return this.pARAMETERField;
            }
            set {
                this.pARAMETERField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public int ROW {
            get {
                return this.rOWField;
            }
            set {
                this.rOWField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string FIELD {
            get {
                return this.fIELDField;
            }
            set {
                this.fIELDField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string SYSTEM {
            get {
                return this.sYSTEMField;
            }
            set {
                this.sYSTEMField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.2634.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="urn:sap-com:document:sap:rfc:functions")]
    public partial class ZPP_GOODSMVT_201Response {
        
        private ZSD_GOODSMVTMSG_201[] eT_MSGField;
        
        private BAPIRET2[] eT_RETURNField;
        
        private string e_MSGTXField;
        
        private string e_MSGTYField;
        
        private ZSD_GOODSMVTITEM_201[] iT_ITEMField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("item", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=false)]
        public ZSD_GOODSMVTMSG_201[] ET_MSG {
            get {
                return this.eT_MSGField;
            }
            set {
                this.eT_MSGField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("item", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=false)]
        public BAPIRET2[] ET_RETURN {
            get {
                return this.eT_RETURNField;
            }
            set {
                this.eT_RETURNField = value;
            }
        }
        
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
        public ZSD_GOODSMVTITEM_201[] IT_ITEM {
            get {
                return this.iT_ITEMField;
            }
            set {
                this.iT_ITEMField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2558.0")]
    public delegate void SI_ZPP_GOODSMVT_201CompletedEventHandler(object sender, SI_ZPP_GOODSMVT_201CompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2558.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class SI_ZPP_GOODSMVT_201CompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal SI_ZPP_GOODSMVT_201CompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public ZPP_GOODSMVT_201Response Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((ZPP_GOODSMVT_201Response)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591