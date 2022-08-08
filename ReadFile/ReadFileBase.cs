using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FindNoTranslation.ReadFile
{
    public abstract class ReadFileBase
    {
        private List<string> _noTranslationList = new List<string>();

        public List<string> NoTranslationList
        {
            get
            {
                return _noTranslationList;
            }
        }

        /// <summary>
        /// 读取文件
        /// </summary>
        public abstract void Read();
        
        /// <summary>
        /// 检索的文件后缀 
        /// </summary>
        public abstract string[] FilePattern { get; }

        /// <summary>
        /// 跳过检索文本的条件
        /// </summary>
        /// <returns></returns>
        public virtual bool IsSkip()
        {
            return true;
        }

        /// <summary>
        /// 获取所有的检索文件
        /// </summary>
        /// <returns></returns>
        protected FileInfo[] GetFileInfos()
        {
            List<FileInfo> infos = new List<FileInfo>();
            DirectoryInfo info = new DirectoryInfo(Setting.GetFileDirectorPath());
            for (int i = 0; i < FilePattern.Length; i++)
            {
                FileInfo[] files = info.GetFiles(FilePattern[i], SearchOption.AllDirectories);
                for (int j = 0; j < files.Length; j++)
                {
                    infos.Add(files[j]);
                }
            }
            return infos.ToArray();
        }

        /// <summary>
        /// 添加未翻译内容
        /// </summary>
        /// <param name="content"></param>
        protected void AddContent(string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                return;
            }
            content = content.Replace("\r", "");
            content = content.Replace("\n", "");
            _noTranslationList.Add(content);
        }

    }
}
