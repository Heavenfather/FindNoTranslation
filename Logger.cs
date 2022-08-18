using System;

namespace FindNoTranslation
{
    public static class Logger
    {
        public static void Log(LogEnum logEnum)
        {
            Console.WriteLine(GetContent(logEnum));
        }

        public static void Log(string content)
        {
            Console.WriteLine(content);
        }

        public static void LogError(object message)
        {
            Console.WriteLine(message);
            Log(LogEnum.ErrorCatch);
            Console.ReadKey();
            Environment.Exit(0);
        }

        public static void LogFormat(LogEnum logEnum, params object[] args)
        {
            string content = GetContent(logEnum);
            Console.WriteLine(string.Format(content, args));
        }

        private static string GetContent(LogEnum logEnum)
        {
            string content = "";
            switch (logEnum)
            {
                case LogEnum.StartReadConfig:
                    content = "开始读取配置...";
                    break;
                case LogEnum.EndReadConfig:
                    content = "配置读取完成!";
                    break;
                case LogEnum.ReadConfigError_NonePath:
                    content = "读取配置错误,不存在路径{0}";
                    break;
                case LogEnum.ReadFile:
                    content = "开始读取文件,读取类型:{0},查找类型:{1}";
                    break;
                case LogEnum.ReadingFile:
                    content = "正在读取:{0}";
                    break;
                case LogEnum.SaveFile:
                    content = "保存文件成功！查看NoTranslation.{0}";
                    break;
                case LogEnum.PressAnyKey:
                    content = "\n按任意键退出";
                    break;
                case LogEnum.ErrorCatch:
                    content = "\n程序已出错!!!按任意键退出!";
                    break;
                default:
                    break;
            }
            return content;
        }

    }

    public enum LogEnum
    {
        PressAnyKey,
        ErrorCatch,
        StartReadConfig,
        EndReadConfig,
        ReadConfigError_NonePath,
        ReadFile,
        ReadingFile,
        SaveFile,
    }
}
