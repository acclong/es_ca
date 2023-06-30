﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.42000.
// 
#pragma warning disable 1591

namespace WebSignTest.CA_SignOnWeb {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    using System.Data;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="CA_SignOnWebSoap", Namespace="http://ca-nldc.vn/")]
    public partial class CA_SignOnWeb : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback LogInOperationCompleted;
        
        private System.Threading.SendOrPostCallback LogOutOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetInfoToSignOperationCompleted;
        
        private System.Threading.SendOrPostCallback SaveFileOperationCompleted;
        
        private System.Threading.SendOrPostCallback CreateAndSaveFileInDBOperationCompleted;
        
        private System.Threading.SendOrPostCallback CreateAndSaveFileInServerOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public CA_SignOnWeb() {
            this.Url = global::WebSignTest.Properties.Settings.Default.AppSign_CA_SignOnWeb_CA_SignOnWeb;
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
        public event LogInCompletedEventHandler LogInCompleted;
        
        /// <remarks/>
        public event LogOutCompletedEventHandler LogOutCompleted;
        
        /// <remarks/>
        public event GetInfoToSignCompletedEventHandler GetInfoToSignCompleted;
        
        /// <remarks/>
        public event SaveFileCompletedEventHandler SaveFileCompleted;
        
        /// <remarks/>
        public event CreateAndSaveFileInDBCompletedEventHandler CreateAndSaveFileInDBCompleted;
        
        /// <remarks/>
        public event CreateAndSaveFileInServerCompletedEventHandler CreateAndSaveFileInServerCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://ca-nldc.vn/LogIn", RequestNamespace="http://ca-nldc.vn/", ResponseNamespace="http://ca-nldc.vn/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool LogIn(string username, string password) {
            object[] results = this.Invoke("LogIn", new object[] {
                        username,
                        password});
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void LogInAsync(string username, string password) {
            this.LogInAsync(username, password, null);
        }
        
        /// <remarks/>
        public void LogInAsync(string username, string password, object userState) {
            if ((this.LogInOperationCompleted == null)) {
                this.LogInOperationCompleted = new System.Threading.SendOrPostCallback(this.OnLogInOperationCompleted);
            }
            this.InvokeAsync("LogIn", new object[] {
                        username,
                        password}, this.LogInOperationCompleted, userState);
        }
        
        private void OnLogInOperationCompleted(object arg) {
            if ((this.LogInCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.LogInCompleted(this, new LogInCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://ca-nldc.vn/LogOut", RequestNamespace="http://ca-nldc.vn/", ResponseNamespace="http://ca-nldc.vn/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool LogOut(string key) {
            object[] results = this.Invoke("LogOut", new object[] {
                        key});
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void LogOutAsync(string key) {
            this.LogOutAsync(key, null);
        }
        
        /// <remarks/>
        public void LogOutAsync(string key, object userState) {
            if ((this.LogOutOperationCompleted == null)) {
                this.LogOutOperationCompleted = new System.Threading.SendOrPostCallback(this.OnLogOutOperationCompleted);
            }
            this.InvokeAsync("LogOut", new object[] {
                        key}, this.LogOutOperationCompleted, userState);
        }
        
        private void OnLogOutOperationCompleted(object arg) {
            if ((this.LogOutCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.LogOutCompleted(this, new LogOutCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://ca-nldc.vn/GetInfoToSign", RequestNamespace="http://ca-nldc.vn/", ResponseNamespace="http://ca-nldc.vn/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool GetInfoToSign(string SignMessage, ref string strErr, out System.Data.DataTable dtResult) {
            object[] results = this.Invoke("GetInfoToSign", new object[] {
                        SignMessage,
                        strErr});
            strErr = ((string)(results[1]));
            dtResult = ((System.Data.DataTable)(results[2]));
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void GetInfoToSignAsync(string SignMessage, string strErr) {
            this.GetInfoToSignAsync(SignMessage, strErr, null);
        }
        
        /// <remarks/>
        public void GetInfoToSignAsync(string SignMessage, string strErr, object userState) {
            if ((this.GetInfoToSignOperationCompleted == null)) {
                this.GetInfoToSignOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetInfoToSignOperationCompleted);
            }
            this.InvokeAsync("GetInfoToSign", new object[] {
                        SignMessage,
                        strErr}, this.GetInfoToSignOperationCompleted, userState);
        }
        
        private void OnGetInfoToSignOperationCompleted(object arg) {
            if ((this.GetInfoToSignCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetInfoToSignCompleted(this, new GetInfoToSignCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://ca-nldc.vn/SaveFile", RequestNamespace="http://ca-nldc.vn/", ResponseNamespace="http://ca-nldc.vn/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool SaveFile(System.Data.DataTable dtInFo, string SignMessage, ref System.Data.DataTable dtResult, ref string strError) {
            object[] results = this.Invoke("SaveFile", new object[] {
                        dtInFo,
                        SignMessage,
                        dtResult,
                        strError});
            dtResult = ((System.Data.DataTable)(results[1]));
            strError = ((string)(results[2]));
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void SaveFileAsync(System.Data.DataTable dtInFo, string SignMessage, System.Data.DataTable dtResult, string strError) {
            this.SaveFileAsync(dtInFo, SignMessage, dtResult, strError, null);
        }
        
        /// <remarks/>
        public void SaveFileAsync(System.Data.DataTable dtInFo, string SignMessage, System.Data.DataTable dtResult, string strError, object userState) {
            if ((this.SaveFileOperationCompleted == null)) {
                this.SaveFileOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSaveFileOperationCompleted);
            }
            this.InvokeAsync("SaveFile", new object[] {
                        dtInFo,
                        SignMessage,
                        dtResult,
                        strError}, this.SaveFileOperationCompleted, userState);
        }
        
        private void OnSaveFileOperationCompleted(object arg) {
            if ((this.SaveFileCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SaveFileCompleted(this, new SaveFileCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://ca-nldc.vn/CreateAndSaveFileInDB", RequestNamespace="http://ca-nldc.vn/", ResponseNamespace="http://ca-nldc.vn/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool CreateAndSaveFileInDB(System.Data.DataTable dtInFo, string SignMessage, ref System.Data.DataTable dtResult, ref string strError) {
            object[] results = this.Invoke("CreateAndSaveFileInDB", new object[] {
                        dtInFo,
                        SignMessage,
                        dtResult,
                        strError});
            dtResult = ((System.Data.DataTable)(results[1]));
            strError = ((string)(results[2]));
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void CreateAndSaveFileInDBAsync(System.Data.DataTable dtInFo, string SignMessage, System.Data.DataTable dtResult, string strError) {
            this.CreateAndSaveFileInDBAsync(dtInFo, SignMessage, dtResult, strError, null);
        }
        
        /// <remarks/>
        public void CreateAndSaveFileInDBAsync(System.Data.DataTable dtInFo, string SignMessage, System.Data.DataTable dtResult, string strError, object userState) {
            if ((this.CreateAndSaveFileInDBOperationCompleted == null)) {
                this.CreateAndSaveFileInDBOperationCompleted = new System.Threading.SendOrPostCallback(this.OnCreateAndSaveFileInDBOperationCompleted);
            }
            this.InvokeAsync("CreateAndSaveFileInDB", new object[] {
                        dtInFo,
                        SignMessage,
                        dtResult,
                        strError}, this.CreateAndSaveFileInDBOperationCompleted, userState);
        }
        
        private void OnCreateAndSaveFileInDBOperationCompleted(object arg) {
            if ((this.CreateAndSaveFileInDBCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.CreateAndSaveFileInDBCompleted(this, new CreateAndSaveFileInDBCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://ca-nldc.vn/CreateAndSaveFileInServer", RequestNamespace="http://ca-nldc.vn/", ResponseNamespace="http://ca-nldc.vn/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool CreateAndSaveFileInServer(System.Data.DataTable dtInFo, string SignMessage, ref System.Data.DataTable dtResult, ref string strError) {
            object[] results = this.Invoke("CreateAndSaveFileInServer", new object[] {
                        dtInFo,
                        SignMessage,
                        dtResult,
                        strError});
            dtResult = ((System.Data.DataTable)(results[1]));
            strError = ((string)(results[2]));
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void CreateAndSaveFileInServerAsync(System.Data.DataTable dtInFo, string SignMessage, System.Data.DataTable dtResult, string strError) {
            this.CreateAndSaveFileInServerAsync(dtInFo, SignMessage, dtResult, strError, null);
        }
        
        /// <remarks/>
        public void CreateAndSaveFileInServerAsync(System.Data.DataTable dtInFo, string SignMessage, System.Data.DataTable dtResult, string strError, object userState) {
            if ((this.CreateAndSaveFileInServerOperationCompleted == null)) {
                this.CreateAndSaveFileInServerOperationCompleted = new System.Threading.SendOrPostCallback(this.OnCreateAndSaveFileInServerOperationCompleted);
            }
            this.InvokeAsync("CreateAndSaveFileInServer", new object[] {
                        dtInFo,
                        SignMessage,
                        dtResult,
                        strError}, this.CreateAndSaveFileInServerOperationCompleted, userState);
        }
        
        private void OnCreateAndSaveFileInServerOperationCompleted(object arg) {
            if ((this.CreateAndSaveFileInServerCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.CreateAndSaveFileInServerCompleted(this, new CreateAndSaveFileInServerCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
    public delegate void LogInCompletedEventHandler(object sender, LogInCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class LogInCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal LogInCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public bool Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
    public delegate void LogOutCompletedEventHandler(object sender, LogOutCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class LogOutCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal LogOutCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public bool Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
    public delegate void GetInfoToSignCompletedEventHandler(object sender, GetInfoToSignCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetInfoToSignCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetInfoToSignCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public bool Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
        
        /// <remarks/>
        public string strErr {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[1]));
            }
        }
        
        /// <remarks/>
        public System.Data.DataTable dtResult {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Data.DataTable)(this.results[2]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
    public delegate void SaveFileCompletedEventHandler(object sender, SaveFileCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class SaveFileCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal SaveFileCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public bool Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
        
        /// <remarks/>
        public System.Data.DataTable dtResult {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Data.DataTable)(this.results[1]));
            }
        }
        
        /// <remarks/>
        public string strError {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[2]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
    public delegate void CreateAndSaveFileInDBCompletedEventHandler(object sender, CreateAndSaveFileInDBCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class CreateAndSaveFileInDBCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal CreateAndSaveFileInDBCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public bool Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
        
        /// <remarks/>
        public System.Data.DataTable dtResult {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Data.DataTable)(this.results[1]));
            }
        }
        
        /// <remarks/>
        public string strError {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[2]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
    public delegate void CreateAndSaveFileInServerCompletedEventHandler(object sender, CreateAndSaveFileInServerCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1038.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class CreateAndSaveFileInServerCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal CreateAndSaveFileInServerCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public bool Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
        
        /// <remarks/>
        public System.Data.DataTable dtResult {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Data.DataTable)(this.results[1]));
            }
        }
        
        /// <remarks/>
        public string strError {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[2]));
            }
        }
    }
}

#pragma warning restore 1591