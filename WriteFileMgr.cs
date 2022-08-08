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
                string[] allNoTran = ReadFileMgr.GetInstance().GetAllNoTranslateLines().ToArray();
                string extension = Setting.DEFAULT_SAVE_NAME + "." + Setting.GetOutputType().ToString();
                string savePath = (AppDomain.CurrentDomain.BaseDirectory + extension).Replace("\\", "/");
                if (File.Exists(savePath))
                {
                    File.Delete(savePath);
                }
                FileStream fs = new FileStream(savePath, FileMode.Create, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
                string result = "";
                for (int i = 0; i < allNoTran.Length; i++)
                {
                    result = allNoTran[i];
                    sw.WriteLine(result);
                }
                
                sw.Flush();
                sw.Close();
                fs.Close();
                Logger.Log(LogEnum.SaveFile);
            }
            catch (Exception e)
            {
                Logger.LogError(e);
            }
        }

    }
}
