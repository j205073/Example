using System;
using System.Runtime.InteropServices;
using System.Text;

namespace ChsChtTranslate
{
    public class LanguageTool
    {
        public LanguageTool()
        {
        }

        /// <summary>
        /// 判斷是否為GB2312編碼
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        private bool IsGBCode(string word)
        {
            byte[] bytes = Encoding.GetEncoding("GB2312").GetBytes(word);
            // if there is only one byte, it is ASCII code or other code
            if (bytes.Length <= 1)
            {
                return false;
            }
            else
            {
                byte byte1 = bytes[0];
                byte byte2 = bytes[1];
                //判斷是否是GB2312
                if (byte1 >= 176 && byte1 <= 247 && byte2 >= 160 && byte2 <= 254)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        #region ---使用Kernel32 LCMapString轉換---

        private const int LOCALE_SYSTEM_DEFAULT = 0x0800;
        private const int LCMAP_SIMPLIFIED_CHINESE = 0x02000000;
        private const int LCMAP_TRADITIONAL_CHINESE = 0x04000000;

        [DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int LCMapString(int Locale, int dwMapFlags, string lpSrcStr, int cchSrc, [Out] string lpDestStr, int cchDest);

        public string ToSimplified(string source)
        {
            String target = new String(' ', source.Length);
            int ret = LCMapString(LOCALE_SYSTEM_DEFAULT, LCMAP_SIMPLIFIED_CHINESE, source, source.Length, target, source.Length);
            return target;
        }

        public string ToTraditional(string source)
        {
            String target = new String(' ', source.Length);
            int ret = LCMapString(LOCALE_SYSTEM_DEFAULT, LCMAP_TRADITIONAL_CHINESE, source, source.Length, target, source.Length);
            return target;
        }

        #endregion ---使用Kernel32 LCMapString轉換---
    }
}