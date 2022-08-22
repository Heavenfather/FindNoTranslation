using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

namespace FindNoTranslation.ReadFile
{
    public class ReadExcelFile : ReadFileBase
    {
        public override string[] FilePattern { get { return new string[2] { "*.xlsx", "*.xls" }; } }

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
                    data = GetExcelData(files[i].FullName.Replace("\\","/"));
                    ReadExcel(data);
                }
            }
            catch (Exception e)
            {
                Logger.LogError(e);
            }
        }

        private DataSet GetExcelData(string path)
        {
            DataSet data = null;
            FileStream fs = null;
            IExcelDataReader reader = null;

            try
            {
                fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
                reader = ExcelReaderFactory.CreateReader(fs);                
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

        private void ReadExcel(DataSet data)
        {            
            int columnCount = data.Tables[0].Columns.Count;
            int rowCount = data.Tables[0].Rows.Count;
            string name = data.Tables[0].TableName;
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
