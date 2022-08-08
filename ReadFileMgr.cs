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

            ReadFileData();
        }

        public List<string> GetAllNoTranslateLines()
        {
            ReadFileBase fb = GetReadProgram(Setting.GetReadType());
            return fb.NoTranslationList;
        }

        private void ReadFileData()
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
