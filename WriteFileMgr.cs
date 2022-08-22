using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FindNoTranslation
{
    public sealed class WriteFileMgr
    {
        private static WriteFileMgr _instance;

        public static WriteFileMgr GetInstance()
        {
            if (_instance == null)
                _instance = new WriteFileMgr();
            return _instance;
        }

        public void SaveFile()
        {
            try
            {
                Dictionary<string,string> allNoTran = ReadFileMgr.GetInstance().GetAllNoTranslateLines();
                string extension = Setting.DEFAULT_SAVE_NAME + "." + Setting.GetOutputType().ToString();
                string savePath = (Setting.GetFileDirectorPath() + @"\" + extension).Replace(@"\", "/");
                if (File.Exists(savePath))
                {
                    File.Delete(savePath);
                }
                FileStream fs = new FileStream(savePath, FileMode.Create, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
                string result = "Key,Value";
                sw.WriteLine(result);
                foreach (var item in allNoTran)
                {
                    if (Setting.GetOutputType() == FileEnum.csv)
                    {
                        result = string.Format("\"{0}\",\"{1}\"", item.Key, item.Value);
                    }
                    else
                    {
                        result = string.Format("{0},{1}", item.Key, item.Value);
                    }
                    sw.WriteLine(result);
                }
                
                sw.Flush();
                sw.Close();
                fs.Close();
                Logger.LogFormat(LogEnum.SaveFile, savePath);
            }
            catch (Exception e)
            {
                Logger.LogError(e);
            }
        }

    }
}
