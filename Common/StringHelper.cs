using System;

namespace WeChat.Standard.Common
{
    internal class StringHelper
    {
        /// <summary>
        /// 获取N位随机字符串
        /// </summary>
        /// <param name="num">N位</param>
        /// <returns></returns>
        public static String getRandomString(int num)
        {
            string[] digits = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "_" };
            Random rnum = new Random(System.DateTime.Now.Millisecond);

            for (int i = 0; i < digits.Length; i++)
            {
                int index = Math.Abs(rnum.Next()) % 10;
                String tmpDigit = digits[index];
                digits[index] = digits[i];
                digits[i] = tmpDigit;
            }

            String returnStr = digits[0];
            for (int i = 1; i < num; i++)
            {
                returnStr = digits[i] + returnStr;
            }
            return returnStr;
        }

        /// <summary>
        /// datetime转换成unixtime
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static int convertDateTimeInt(System.DateTime time)
        {
            
            System.DateTime startTime = TimeZoneInfo.ConvertTimeToUtc(new System.DateTime(1970, 1, 1));
            return (int)(time - startTime).TotalSeconds;
        }
        /// <summary>
        /// unixtime->普通时间
        /// </summary>
        /// <param name="unixtime"></param>
        /// <returns></returns>
        public static String convertDateTimeNormal(long unixtime)
        {
            DateTime dt = new System.DateTime(1970, 1, 1);
            return dt.AddSeconds(unixtime).AddHours(8).ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
