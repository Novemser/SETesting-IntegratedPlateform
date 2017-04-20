using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareTestingWPF.Base
{
    public class DateParser
    {
        public static List<object> ParseDate(string dateStr)
        {
            List<object> result = new List<object>();
            string resultText = "";
            try
            {
                DateTime time = DateTime.Parse(dateStr);
                time = time.AddDays(1);
                resultText = time.ToLongDateString();
                result.Add(true);
            }
            catch (Exception e)
            {
                resultText = "输入格式错误:" + e.Message;
                result.Add(false);
            }
            finally
            {
                result.Add(resultText);
            }

            return result;
        }
    }
}
