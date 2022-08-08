using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FindNoTranslation.ReadFile
{
    public class ReadCsvFile : ReadFileBase
    {
        public override string[] FilePattern { get { return new string[1] { "*.csv" }; } }

        /// <summary>
        /// csv也是一个文件流形式，可以用读取txt方式读取
        /// </summary>
        public override void Read()
        {
            try
            {
                FileInfo[] files = GetFileInfos();
                string[] allLines;
                for (int i = 0; i < files.Length; i++)
                {
                    Logger.LogFormat(LogEnum.ReadingFile, files[i].Name);
                    allLines = File.ReadAllLines(files[i].FullName);
                    for (int j = 0; j < allLines.Length; j++)
                    {
                        if (IsSkip())
                        {
                            foreach (var item in allLines[j])
                            {
                                if (FindNoTranslateMgr.GetInstance().IsMatch(item.ToString()))
                                {
                                    AddContent(allLines[j]);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        }

    }
}
