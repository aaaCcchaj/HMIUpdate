using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Xml;
using ESBKUpDateClient.ESBKUpDateServer;

namespace ESBKUpDateClient
{
    public partial class Form1 : Form
    {
        ESBKUpDateService mESBKUpDateService=new ESBKUpDateServiceClient();
        private string mVersion;
        private string mObjectName;
        private string mStartFileName;
        private string appPath = System.Environment.CurrentDirectory;
        private string mConfigureFileName = "Client.config";
        XmlDocument mXmlDocument=new XmlDocument();

        int successNum = 0;
        int faieldNum = 0;

        public Form1()
        {
            InitializeComponent();
            GetSelfVersion();
            Action action = UpDateProject;

            action.BeginInvoke(null, null);
        }

        void UpDateProject()
        {
            
            string version = mESBKUpDateService.GetVersion(mObjectName);
            if (version != this.mVersion || !Directory.Exists(Path.Combine(appPath, mObjectName)))
            {
                NeedUpDate(version);
            }
            else
            {
                ShowMessage("当前已经是最新版本");
            }
            ShowMessage("更新结束");
            //Thread.Sleep(3000);
            //if (faieldNum==0)
            //    StartApplication();
        }

        private void NeedUpDate(string version)
        {
            //label_banben_new.Text = version;
            ShowMessage(string.Format("发现新版本 {0}，准备更新", version));
            try
            {
                string localPathName = Path.Combine(appPath, mObjectName);
                if (!Directory.Exists(localPathName))
                    Directory.CreateDirectory(localPathName);
                UpDateDirectorys(mObjectName, localPathName);
            }
            catch (Exception ex)
            {
                ShowMessage("更新出现异常" + ex.Message);
            }
           
            ShowMessage(string.Format("更新完成,成功更新{0}各文件，失败{1}", successNum, faieldNum));
            if (faieldNum == 0 && successNum>0)
                UpdateVersionInfo(version);
        }

        private void UpDateDirectorys(string dirPath,string localDirPath)
        {
            GetUpDateFiles(dirPath, localDirPath);

            string[] dirPaths = mESBKUpDateService.GetUpDateDirectorys(dirPath);
            foreach (var path in dirPaths)
            {
                string pathName = path.Split('\\').Last();
                string fullPathName = Path.Combine(localDirPath, pathName);
                if (!Directory.Exists(fullPathName))
                    Directory.CreateDirectory(fullPathName);
                UpDateDirectorys(path, fullPathName);
            }
        }

        private void GetUpDateFiles(string dirPath,string localDirPath)
        {
            string[] upDateFiles = mESBKUpDateService.GetUpDateFileNames(dirPath);
            if (upDateFiles.Any())
            {
                foreach (var filename in upDateFiles)
                {
                    UpDateFile(filename, localDirPath);
                }
            }
        }

        private void UpDateFile(string filename, string localDirPath)
        {
            string newFileName = filename.Split('\\').Last();
            string newFilePath = Path.Combine(localDirPath, newFileName);
            try
            {
                WriteUpDateFile(filename, newFilePath, newFileName);
            }
            catch (Exception ex)
            {
                faieldNum++;
                ShowMessage(string.Format("更新文件{0}失败，原因{1}", newFileName, ex.Message));
            }
        }

        private void WriteUpDateFile(string filename, string newFilePath, string newFileName)
        {
            File.Delete(newFilePath);

            DownFile mDownFile = new DownFile();
            mDownFile.FileName = filename;
            DownFileResult mDownFileResult = mESBKUpDateService.GetFileStream(mDownFile);
            Stream mMemoryStream = mDownFileResult.FileStream;
            FileStream fileStream = new FileStream(newFilePath, FileMode.Create, FileAccess.ReadWrite);
            int count = 0;
            byte[] buffer = new byte[mDownFileResult.FileSize];
            while ((count = mMemoryStream.Read(buffer, 0, buffer.Length)) > 0)
            {
                fileStream.Write(buffer, 0, count);
            }
            fileStream.Flush();
            fileStream.Close();

            successNum++;
            ShowMessage("已更新" + newFileName);
        }

        private void StartApplication()
        {
            try
            {
                System.Diagnostics.Process.Start(Path.Combine(appPath, mObjectName, mStartFileName));
                Application.Exit();
                this.Close();
                this.Dispose();
            }
            catch
            {
                ShowMessage("自动启动应用程序出错,请手动启动");
            }
        }

        private void GetSelfVersion()
        {
            string configureFile = Path.Combine(appPath, mConfigureFileName);
            try
            {
                mXmlDocument.Load(configureFile);
                XmlNodeList versionnodelist = mXmlDocument.GetElementsByTagName("version");
                foreach (XmlNode node in versionnodelist)
                {
                    if (node.Name == "version")
                    {
                        mVersion = node.InnerText;
                        label_banben_now.Text = mVersion;
                        break;
                    }
                }
                XmlNodeList applicationnamenodelist = mXmlDocument.GetElementsByTagName("applicationname");
                foreach (XmlNode node in applicationnamenodelist)
                {
                    if (node.Name == "applicationname")
                    {
                        mStartFileName = node.InnerText;
                        break;;
                    }
                }
                XmlNodeList objectnamenodelist = mXmlDocument.GetElementsByTagName("objectname");
                foreach (XmlNode node in objectnamenodelist)
                {
                    if (node.Name == "objectname")
                    {
                        mObjectName = node.InnerText;
                        objectName.Text = mObjectName + objectName.Text;
                        objectName.Location = new System.Drawing.Point(this.Width / 2 - objectName.Width / 2, 22);
                        break; 
                    }
                }
            }
            catch
            {
                ShowMessage("获取配置文件失败");
                mVersion = string.Empty;
                //创建新的xml文件
                CreateLoadFormXml(configureFile);
                mXmlDocument.Load(configureFile);
            }
        }

        void CreateLoadFormXml(string configureFile)
        {
            XmlWriter writer = XmlWriter.Create(configureFile);
            writer.WriteStartDocument();
            writer.WriteStartElement("configuration");
            writer.WriteElementString("version", "");
            writer.WriteElementString("applicationname", "");
            writer.WriteElementString("objectname", "");
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Flush();
            writer.Close();
        }

        bool UpdateVersionInfo(string version)
        {
            try
            {
                //读所有节点表
                XmlNamespaceManager xnm = new XmlNamespaceManager(mXmlDocument.NameTable);
                //读取节点值
                XmlNode node = mXmlDocument.SelectSingleNode("/configuration/version", xnm);
                //node.InnerText 就是读取出来的值
                //修改节点值
                node.InnerText = version;
                //配置文件路径
                string configureFile = Path.Combine(appPath, mConfigureFileName);
                //保存修改后的内容
                mXmlDocument.Save(configureFile);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void ShowMessage(string msg)
        {
            if (this.InvokeRequired)
            {
                Action<string> action = WriteMessageToBox;
                this.BeginInvoke(action, new object[] {msg});
            }
            else
            {
                WriteMessageToBox(msg);                
            }
        }

        private void WriteMessageToBox(string msg)
        {
            mMessageBox.AppendText(msg+"\r\n");
        }

        private void button_begin_Click(object sender, EventArgs e)
        {
            StartApplication();
        }

        private void button_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
