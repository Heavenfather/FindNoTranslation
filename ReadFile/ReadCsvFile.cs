using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

namespace FindNoTranslation.ReadFile
{
    public class ReadCsvFile : ReadFileBase
    {
        public override string[] FilePattern { get { return new string[1] { "*.csv" }; } }

        public override void Read()
        {
            try
            {
                FileInfo[] files = GetFileInfos();
                DataSet data = null;
                for (int i = 0; i < files.Length; i++)
                {
                    if (files[i].Name.StartsWith("~$"))
                    {
                        continue;
                    }
                    Logger.LogFormat(LogEnum.ReadingFile, files[i].Name);
                    data = GetCsvData(files[i].FullName.Replace("\\", "/"));
                    string name = files[i].Name.Split(".")[0];
                    ReadCsv(name, data);
                }
            }
            catch (Exception e)
            {
                Logger.LogError(e);
            }

        }

        private DataSet GetCsvData(string path)
        {
            DataSet data = null;
            FileStream fs = null;
            IExcelDataReader reader = null;

            try
            {
                fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                reader = ExcelReaderFactory.CreateCsvReader(fs);
                data = reader.AsDataSet();
                reader.Close();
                fs.Close();
            }
            catch (Exception e)
            {
                Logger.LogError(e);
            }

            return data;
        }

        private void ReadCsv(string name,DataSet data)
        {
            int columnCount = data.Tables[0].Columns.Count;
            int rowCount = data.Tables[0].Rows.Count;
            string result = "";
            int skipCount = Setting.GetStartRow();
            for (int i = 0; i < rowCount; i++)
            {
                if (i < skipCount)
                {
                    continue;
                }
                for (int j = 0; j < columnCount; j++)
                {
                    result = data.Tables[0].Rows[i][j].ToString();
                    if (FindNoTranslateMgr.GetInstance().IsMatch(result) && IsSkip())
                    {
                        ReadFileMgr.GetInstance().AddContent(name + "_" + i + "_" + j, result);
                    }
                }
            }
        }

    }
}
