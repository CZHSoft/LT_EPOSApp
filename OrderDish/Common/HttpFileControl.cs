using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Net;
using System.Web;
using System.Text.RegularExpressions;

namespace OrderDish.Common
{
    public class HttpFileControl
    {
        public static bool DownLoadFile(string fileURL, string filePath)
        {
            HttpWebRequest req = null;
            HttpWebResponse resp = null;
            Stream respStream = null;
            FileStream wrtr = null;
            bool flag = false;

            try
            {
                req = (HttpWebRequest)WebRequest.Create(fileURL);
                resp = (HttpWebResponse)req.GetResponse();

                respStream = resp.GetResponseStream();
                wrtr = new FileStream(filePath, FileMode.Create);

                byte[] inData = new byte[4096];

                int bytesRead = respStream.Read(inData, 0, inData.Length);
                while (bytesRead > 0)
                {
                    wrtr.Write(inData, 0, bytesRead);
                    bytesRead = respStream.Read(inData, 0, inData.Length);
                }
                flag = true;
            }
            catch
            {
                flag = false;
                //MessageBox.Show(ep.Message);
            }
            finally
            {
                try
                {
                    if (wrtr != null)
                    {
                        wrtr.Close();
                    }
                    if (respStream != null)
                    {
                        respStream.Close();
                    }
                    if (resp != null)
                    {
                        resp.Close();
                    }
                    if (req != null)
                    {
                        req.Abort();
                        req = null;
                    }
                }
                catch
                {

                }

            }
            return flag;
        }

        public static string[] GetPathFile(string dirPath)
        {
            //string dirPath = HttpContext.Current.Server.MapPath(url);

            

            if (Directory.Exists(dirPath))
            {
                //获得目录信息 
                DirectoryInfo dir = new DirectoryInfo(dirPath);

                //获得目录文件列表 
                FileInfo[] files = dir.GetFiles("*.*");
                string[] fileNames = new string[files.Length];
                int i = 0;

                foreach (FileInfo fileInfo in files)
                {
                    fileNames[i] = fileInfo.Name;
                    i++;
                }
                return fileNames;

            }
            else
                return null;
        }

        public static List<string> GetPathFileList(string dirPath)
        {
            List<string> strList = new List<string>();
            strList.Clear();

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(dirPath);

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream(),Encoding.Default))
                {
                    string html = reader.ReadToEnd();
                    Regex regex = new Regex("<a(?:\\s+.+?)*?\\s+href=\"([^\"]*?)\".*?>(.*?)</a>", RegexOptions.IgnoreCase);
                    MatchCollection matches = regex.Matches(html);
                    if (matches.Count > 0)
                    {
                        foreach (Match match in matches)
                        {
                            if (match.Success)
                            {
                                string[] tempArray=match.Groups[2].Value.ToString().Split('.');
                                if(tempArray.Length==2)
                                {
                                    if (tempArray[1] == "wmv")
                                    {
                                        strList.Add(match.Groups[2].Value.ToString());
                                    }
                                }
                                
                            }
                        }
                    }

                }
            }

            return strList;
        }
    }

}
