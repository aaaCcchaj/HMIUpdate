﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.18408
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace ESBKUpDateClient.ESBKUpDateServer {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://www.artech.com/", ConfigurationName="ESBKUpDateServer.ESBKUpDateService")]
    public interface ESBKUpDateService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.artech.com/ESBKUpDateService/GetVersion", ReplyAction="http://www.artech.com/ESBKUpDateService/GetVersionResponse")]
        string GetVersion(string itemName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.artech.com/ESBKUpDateService/GetUpDateFileNames", ReplyAction="http://www.artech.com/ESBKUpDateService/GetUpDateFileNamesResponse")]
        string[] GetUpDateFileNames(string itemName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.artech.com/ESBKUpDateService/GetUpDateDirectorys", ReplyAction="http://www.artech.com/ESBKUpDateService/GetUpDateDirectorysResponse")]
        string[] GetUpDateDirectorys(string itemName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.artech.com/ESBKUpDateService/GetFileBinary", ReplyAction="http://www.artech.com/ESBKUpDateService/GetFileBinaryResponse")]
        byte[] GetFileBinary(string fileName);
        
        // CODEGEN: 消息 DownFile 的包装名称(DownFile)以后生成的消息协定与默认值(GetFileStream)不匹配
        [System.ServiceModel.OperationContractAttribute(Action="http://www.artech.com/ESBKUpDateService/GetFileStream", ReplyAction="http://www.artech.com/ESBKUpDateService/GetFileStreamResponse")]
        ESBKUpDateClient.ESBKUpDateServer.DownFileResult GetFileStream(ESBKUpDateClient.ESBKUpDateServer.DownFile request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="DownFile", WrapperNamespace="http://www.artech.com/", IsWrapped=true)]
    public partial class DownFile {
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://www.artech.com/")]
        public string FileName;
        
        public DownFile() {
        }
        
        public DownFile(string FileName) {
            this.FileName = FileName;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="DownFileResult", WrapperNamespace="http://www.artech.com/", IsWrapped=true)]
    public partial class DownFileResult {
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://www.artech.com/")]
        public long FileSize;
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://www.artech.com/")]
        public bool IsSuccess;
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://www.artech.com/")]
        public string Message;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.artech.com/", Order=0)]
        public System.IO.Stream FileStream;
        
        public DownFileResult() {
        }
        
        public DownFileResult(long FileSize, bool IsSuccess, string Message, System.IO.Stream FileStream) {
            this.FileSize = FileSize;
            this.IsSuccess = IsSuccess;
            this.Message = Message;
            this.FileStream = FileStream;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ESBKUpDateServiceChannel : ESBKUpDateClient.ESBKUpDateServer.ESBKUpDateService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ESBKUpDateServiceClient : System.ServiceModel.ClientBase<ESBKUpDateClient.ESBKUpDateServer.ESBKUpDateService>, ESBKUpDateClient.ESBKUpDateServer.ESBKUpDateService {
        
        public ESBKUpDateServiceClient() {
        }
        
        public ESBKUpDateServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ESBKUpDateServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ESBKUpDateServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ESBKUpDateServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string GetVersion(string itemName) {
            return base.Channel.GetVersion(itemName);
        }
        
        public string[] GetUpDateFileNames(string itemName) {
            return base.Channel.GetUpDateFileNames(itemName);
        }
        
        public string[] GetUpDateDirectorys(string itemName) {
            return base.Channel.GetUpDateDirectorys(itemName);
        }
        
        public byte[] GetFileBinary(string fileName) {
            return base.Channel.GetFileBinary(fileName);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        ESBKUpDateClient.ESBKUpDateServer.DownFileResult ESBKUpDateClient.ESBKUpDateServer.ESBKUpDateService.GetFileStream(ESBKUpDateClient.ESBKUpDateServer.DownFile request) {
            return base.Channel.GetFileStream(request);
        }
        
        public long GetFileStream(string FileName, out bool IsSuccess, out string Message, out System.IO.Stream FileStream) {
            ESBKUpDateClient.ESBKUpDateServer.DownFile inValue = new ESBKUpDateClient.ESBKUpDateServer.DownFile();
            inValue.FileName = FileName;
            ESBKUpDateClient.ESBKUpDateServer.DownFileResult retVal = ((ESBKUpDateClient.ESBKUpDateServer.ESBKUpDateService)(this)).GetFileStream(inValue);
            IsSuccess = retVal.IsSuccess;
            Message = retVal.Message;
            FileStream = retVal.FileStream;
            return retVal.FileSize;
        }
    }
}