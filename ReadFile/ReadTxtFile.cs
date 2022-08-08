using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FindNoTranslation.ReadFile
{
    public class ReadTxtFile : ReadFileBase
    {
        /// <summary>
        /// 文件流形式的文件
        /// </summary>
        public override string[] FilePattern { get { return new string[1] { "*.txt" }; } }

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
                Logger.LogError(e);
            }

        }

        public override bool IsSkip()
        {
            return base.IsSkip();
        }

    }
}
