using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WpfApp.Tools.Utils
{

    public static class CheckTrue
    {
        /// <summary>
        /// 正则验证身份证
        /// </summary>
        /// <param name="strIdCard"></param>
        /// <returns></returns>
        public static bool checkIdCard(string strIdCard)
        {
            //闰年出生日期的合法性正则表达式 || 平年出生日期的合法性正则表达式 
            if (!Regex.IsMatch(strIdCard, @"(^[1-9][0-9]{5}(19|20)[0-9]{2}((01|03|05|07|08|10|12)(0[1-9]|[1-2][0-9]|3[0-1])|(04|06|09|11)(0[1-9]|[1-2][0-9]|30)|02(0[1-9]|[1-2][0-9]))[0-9]{3}[0-9Xx]$)") || !Regex.IsMatch(strIdCard, @"(^[1-9][0-9]{5}(19|20)[0-9]{2}((01|03|05|07|08|10|12)(0[1-9]|[1-2][0-9]|3[0-1])|(04|06|09|11)(0[1-9]|[1-2][0-9]|30)|02(0[1-9]|1[0-9]|2[0-8]))[0-9]{3}[0-9Xx]$)"))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 正则验证电话号码
        /// </summary>
        /// <param name="strPhoneNumber"></param>
        /// <returns></returns>
        public static bool checkPhone(string strPhoneNumber)
        {
            ////使用正则表达式判断是否匹配               
            if (!Regex.IsMatch(strPhoneNumber, @"^0?(13[0-9]|14[5-9]|15[012356789]|166|17[0-8]|18[0-9]|19[89])[0-9]{8}$"))
            {
                return false;
            }
            return true;
        }
    }
}
