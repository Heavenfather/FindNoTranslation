using FindNoTranslation.ReadFile;
using System;
using System.Collections.Generic;
using System.Text;

namespace FindNoTranslation
{
    public sealed class ReadFileMgr
    {
        private static ReadFileMgr _instance;
        private ReadFileBase[] _readFileBases = new ReadFileBase[(int)FileEnum.csv + 1];

        private Dictionary<string, string> _noTranslationDic = new Dictionary<string, string>();
        public Dictionary<string, string> NoTranslationDic
        {
            get
            {
                return _noTranslationDic;
            }
        }

        public static ReadFileMgr GetInstance()
        {
            if (_instance == null)
                _instance = new ReadFileMgr();
            return _instance;
        }

        private ReadFileMgr()
        {
            _readFileBases[(int)FileEnum.txt] = new ReadTxtFile();
            _readFileBases[(int)FileEnum.excel] = new ReadExcelFile();
            _readFileBases[(int)FileEnum.csv] = new ReadCsvFile();
        }

        public Dictionary<string,string> GetAllNoTranslateLines()
        {
            return this.NoTranslationDic;
        }

        /// <summary>
        /// 添加未翻译内容
        /// </summary>
        /// <param name="content"></param>
        public void AddContent(string key, string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                return;
            }
            if (_noTranslationDic.ContainsKey(key))
            {
                return;
            }
            content = content.Replace("\r", "");
            content = content.Replace("\n", "");
            _noTranslationDic.Add(key, content);
        }

        public void ReadFileData()
        {
            FileEnum fe = Setting.GetReadType();
            Logger.LogFormat(LogEnum.ReadFile, fe, Setting.GetFindLanguage());
            ReadFileBase fb = GetReadProgram(fe);
            fb.Read();
        }

        private ReadFileBase GetReadProgram(FileEnum fe)
        {
            return _readFileBases[(int)fe];
        }


    }
}
