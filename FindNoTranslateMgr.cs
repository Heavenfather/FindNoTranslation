using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace FindNoTranslation
{
    public sealed class FindNoTranslateMgr
    {
        private static FindNoTranslateMgr _instance;

        public static FindNoTranslateMgr GetInstance()
        {
            if (_instance == null)
                _instance = new FindNoTranslateMgr();
            return _instance;
        }

        public bool IsMatch(string content)
        {
            FindLanguage findEnume = Setting.GetFindLanguage();
            if (findEnume == FindLanguage.en)
            {
                return IsEnglish(content);
            }else if(findEnume == FindLanguage.cn)
            {
                return IsChinese(content);
            }
            return false;
        }

        public bool IsChinese(string content)
        {
            bool result = false;
            //简体中文
            for (int i = 0; i < content.Length; i++)
            {
                result = Regex.IsMatch(content[i].ToString(), @"[\u4e00-\u9fbb]");
                if (result)
                    break;
            }
            //繁体中文
            if (!result)
            {
                foreach (var item in content)
                {
                    char[] temp = Encoding.GetEncoding("big5").GetChars(Encoding.GetEncoding("big5").GetBytes(new char[] { item }));
                    if (temp.Length != 1 || !item.Equals(temp[0]))
                    {
                        result = true;
                        break;
                    }
                }
            }
            return result;
        }

        public bool IsEnglish(string content)
        {
            bool result = false;
            for (int i = 0; i < content.Length; i++)
            {
                result = Regex.IsMatch(content[i].ToString(), @"^[A-Za-z]+$");
                if (result)
                    break;
            }
            if (result)
            {
                var bytes = Encoding.UTF8.GetBytes(content);
                result = bytes.Length == content.Length;
            }
            return result;
        }

    }
}
