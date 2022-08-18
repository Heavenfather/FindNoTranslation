using System;

namespace FindNoTranslation
{
    class Program
    {
        static void Main(string[] args)
        {
            RegisterEncoding();
            Setting.InitConfig();

            ReadFileMgr.GetInstance().ReadFileData();
            WriteFileMgr.GetInstance().SaveFile();

            Logger.Log(LogEnum.PressAnyKey);
            Console.ReadKey();
        }

        /// <summary>
        /// 使用ExcelDataReader程序包后，需要主动注册Endcoding....
        /// </summary>
        private static void RegisterEncoding()
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        }

    }
}
