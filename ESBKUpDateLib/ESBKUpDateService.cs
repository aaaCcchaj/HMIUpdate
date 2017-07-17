using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ESBKUpDateInterface;

namespace ESBKUpDateLib
{
    public class ESBKUpDateService : IServiceInterface
    {
        PublicClass mPublicClass = new PublicClass();

        /// <summary>
        /// 获取项目的版本号
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string GetVersion(string item)
        {
            string version = mPublicClass.GetVersion(item);
            return version;
        }

        /// <summary>
        /// 获取项目的所有更新文件
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string[] GetUpDateFileNames(string item)
        {
            if (item == string.Empty)
                return null;
            if (!item.Contains('\\'))
            {
                string itemPath = mPublicClass.FilePath(item);
                if (itemPath != string.Empty && Directory.Exists(itemPath))
                {
                    string[] fileNames = Directory.GetFiles(itemPath);
                    return fileNames;
                }
                return null;
            }
            else
            {
                if (Directory.Exists(item))
                {
                    string[] fileNames = Directory.GetFiles(item);
                    return fileNames;
                }
                return null;
            }
        }

        public string[] GetUpDateDirectorys(string pathName)
        {
            if (pathName == string.Empty)
                return null;
            if (!pathName.Contains('\\'))
            {
                string itemPath = mPublicClass.FilePath(pathName);
                if (itemPath != string.Empty && Directory.Exists(itemPath))
                {
                    string[] dirNames = Directory.GetDirectories(itemPath);
                    return dirNames;
                }
                return null;
            }
            else
            {
                if (Directory.Exists(pathName))
                {
                    string[] dirNames = Directory.GetDirectories(pathName);
                    return dirNames;
                }
                return null;
            }
        }


        /// <summary>
        /// 获取文件的二进制数据流
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public byte[] GetFileBinary(string fileName)
        {
            FileStream fileStream = File.OpenRead(fileName);
            MemoryStream memoryStream = new MemoryStream();
            int temp;
            while ((temp = fileStream.ReadByte()) != -1)
            {
                memoryStream.WriteByte((byte)temp);
            }
            byte[] fileByte = memoryStream.ToArray();
            fileStream.Dispose();
            memoryStream.Dispose();
            return fileByte;
        }

        /// <summary>
        /// 获取文件的字节流
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public DownFileResult GetFileStream(DownFile downFileName)
        {
            DownFileResult mDownFileResult = new DownFileResult();
            using (Stream mMemoryStream = new MemoryStream())
            {
                using (FileStream fs = new FileStream(downFileName.FileName, FileMode.Open, FileAccess.Read))
                {
                    fs.CopyTo(mMemoryStream);
                    mMemoryStream.Position = 0;
                    mDownFileResult.FileSize = mMemoryStream.Length;
                    mDownFileResult.FileStream = mMemoryStream;
                    fs.Flush();
                    fs.Close();
                }
            }
            return mDownFileResult;
        }
    }
}
