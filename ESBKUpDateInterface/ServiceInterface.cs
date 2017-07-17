using System.IO;
using System.ServiceModel;

namespace ESBKUpDateInterface
{
    
    /// <summary>
    /// 公用方法接口
    /// </summary>
    [ServiceContract(Name = "ESBKUpDateService", Namespace = "http://www.artech.com/")]
    public interface IServiceInterface
    {
        /// <summary>
        /// 获取服务器中项目的版本号
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [OperationContract]
        string GetVersion(string itemName);

        /// <summary>
        /// 获取所有更新的文件名
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        string[] GetUpDateFileNames(string itemName);

        /// <summary>
        /// 获取路径上的文件夹名称
        /// </summary>
        /// <param name="itemName"></param>
        /// <returns></returns>
        [OperationContract]
        string[] GetUpDateDirectorys(string itemName);

        /// <summary>
        /// 获取文件的二进制数据流
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        [OperationContract]
        byte[] GetFileBinary(string fileName);

        /// <summary>
        /// 获取文件的数据流
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        [OperationContract]
        DownFileResult GetFileStream(DownFile fileResult);

    }

    [MessageContract]
    public class DownFileResult
    {
        [MessageHeader]
        public long FileSize { get; set; }
        [MessageHeader]
        public bool IsSuccess { get; set; }
        [MessageHeader]
        public string Message { get; set; }
        [MessageBodyMember]
        public Stream FileStream { get; set; }
    }

    [MessageContract]
    public class DownFile
    {
        [MessageHeader]
        public string FileName { get; set; }
    }

}
