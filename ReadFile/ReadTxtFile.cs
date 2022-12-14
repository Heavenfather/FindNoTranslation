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
                //txt格式还需要根据实际情况修改，现在这里先简单的就读一整行
                for (int i = 0; i < files.Length; i++)
                {
                    Logger.LogFormat(LogEnum.ReadingFile, files[i].Name);
                    int index = 0;
                    string name = files[i].Name.Split(".")[0];
                    allLines = File.ReadAllLines(files[i].FullName);
                    for (int j = 0; j < allLines.Length; j++)
                    {
                        if (FindNoTranslateMgr.GetInstance().IsMatch(allLines[j]) && IsSkip())
                        {
                            ReadFileMgr.GetInstance().AddContent(name + "_" + index, allLines[j]);
                        }
                        index++;
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
