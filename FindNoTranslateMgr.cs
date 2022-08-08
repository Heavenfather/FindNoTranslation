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
            result = Regex.IsMatch(content, @"[\u4e00-\u9fbb]");
            //繁体中文
            if (!result)
            {
                if (Regex.IsMatch(content, @"[\u4e00-\u9fbb]"))
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
            }
            return result;
        }

        public bool IsEnglish(string content)
        {
            return Regex.IsMatch(content, @"^[A-Za-z]+$");
        }

    }
}
