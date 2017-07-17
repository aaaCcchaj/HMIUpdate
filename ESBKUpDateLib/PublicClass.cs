using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Net.NetworkInformation;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace ESBKUpDateLib
{
    /// <summary>
    /// 公共类，提供各种工具和方法
    /// </summary>
    public class PublicClass
    {
        private Dictionary<string, string> mProjectFilePath=new Dictionary<string, string>();
        private string appPath = System.Environment.CurrentDirectory;
        private string mConfigureFileName = "configure.xml";
        XmlDocument mXmlDocument = new XmlDocument();

        public PublicClass()
        {
            string configureFile = Path.Combine(appPath, mConfigureFileName);
            mXmlDocument.Load(configureFile);
            GetProjectFilePath();
        }

        private void GetProjectFilePath()
        {
            try
            {
                XmlNamespaceManager xnm = new XmlNamespaceManager(mXmlDocument.NameTable);
                XmlNode node = mXmlDocument.SelectSingleNode("/configuration/objectpath/HMI", xnm);
                 mProjectFilePath.Add("HMI",node.InnerText);
            }
            catch (Exception ex)
            {
                Console.WriteLine("获取HMI路径出错," + ex.Message);
            }
        }

        /// <summary>
        /// 获取项目的文件路径
        /// </summary>
        /// <param name="projectName"></param>
        /// <returns></returns>
        public string FilePath(string projectName)
        {
            if (mProjectFilePath.ContainsKey(projectName))
            {
                return mProjectFilePath[projectName];
            }
            return string.Empty;
        }

        /// <summary>
        /// 获取项目的版本号
        /// </summary>
        /// <param name="projectName"></param>
        /// <returns></returns>
        public string GetVersion(string projectName)
        {
            string mVersion = string.Empty;
            try
            {
                XmlNodeList versionnodelist = mXmlDocument.GetElementsByTagName("version");
                foreach (XmlNode node in versionnodelist)
                {
                    if (node.Name == "version")
                    {
                        mVersion = node.InnerText;
                        Console.WriteLine("获取到版本号" + mVersion);
                        break;
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("获取版本号出错," + ex.Message);
            }
            return mVersion;
        }

        public int GetBindPort()
        {
            int mBindPort = 7777;
            try
            {
                XmlNodeList versionnodelist = mXmlDocument.GetElementsByTagName("port");
                foreach (XmlNode node in versionnodelist)
                {
                    if (node.Name == "port")
                    {
                        mBindPort = int.Parse(node.InnerText);
                        break;
                    }
                }
            }
            catch { }
            return mBindPort;
        }
    }
}
