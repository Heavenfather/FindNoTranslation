using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FindNoTranslation
{
    public static class Setting
    {
        public static readonly string DEFAULT_SAVE_NAME = "NoTranslation";
        private static readonly string FILE_NAME = "AppSetting.txt";

        private static ConfigData configData;

        public static void InitConfig()
        {
            Logger.Log(LogEnum.StartReadConfig);

            ReadSetting();
        }

        private static void ReadSetting()
        {
            string path = (AppDomain.CurrentDomain.BaseDirectory + FILE_NAME).Replace("\\", "/");
            if (File.Exists(path))
            {
                string[] content = File.ReadAllLines(path);
                configData = new ConfigData();
                for (int i = 0; i < content.Length; i++)
                {
                    if (content[i].StartsWith("//") || string.IsNullOrEmpty(content[i]))
                    {
                        continue;
                    }
                    string[] setStrs = content[i].Split(".");
                    configData.Set(int.Parse(setStrs[0]), setStrs[1]);
                }
                Logger.Log(LogEnum.EndReadConfig);
            }
            else
            {
                Logger.LogError("AppSetting.txt文件不存在");
            }
        }

        public static FindLanguage GetFindLanguage()
        {
            return configData.language;
        }

        public static string GetFileDirectorPath()
        {
            return configData.fileDirPath;
        }

        public static FileEnum GetOutputType()
        {
            return configData.outputType;
        }

        public static FileEnum GetReadType()
        {
            return configData.readType;
        }

        public static int GetStartRow()
        {
            return configData.startRow;
        }
        
    }

    public class ConfigData
    {
        public FindLanguage language;
        public FileEnum readType;
        public FileEnum outputType;
        public string fileDirPath;
        public int startRow = 0;

        public void Set(int index, string content)
        {
            try
            {
                string[] param = content.Split('=', 2, StringSplitOptions.RemoveEmptyEntries);
                if (index == 1)
                {
                    readType = (FileEnum)Enum.Parse(typeof(FileEnum), param[1].ToLower());
                }
                else if (index == 2)
                {
                    if (int.TryParse(param[1],out startRow))
                    {
                    }
                }
                else if (index == 3)
                {
                    fileDirPath = param[1].Replace(@"\", "/");
                }
                else if (index == 4)
                {
                    outputType = (FileEnum)Enum.Parse(typeof(FileEnum), param[1].ToLower());
                }
                else if (index == 5)
                {
                    language = (FindLanguage)Enum.Parse(typeof(FindLanguage), param[1].ToLower());
                }
            }
            catch (Exception e)
            {
                Logger.LogError(e);
            }
        }

    }

    public enum FindLanguage
    {
        en,
        cn
    }

    public enum FileEnum
    {
        txt,
        excel,
        csv
    }

}
