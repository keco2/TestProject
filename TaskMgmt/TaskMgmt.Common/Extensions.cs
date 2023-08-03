using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskMgmt.Common
{
    public static class Extensions
    {
        public static T ToEnum<T>(this string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        public static string TakeLine(this string text, int linesToTake)
        {
            var sb = new StringBuilder();
            int i = 0;

            using (StringReader sr = new StringReader(text))
            {
                string line;
                while ((line = sr.ReadLine()) != null && i < linesToTake)
                {
                    sb.AppendLine(line);
                    i++;
                }
            }

            return sb.ToString();
        }
    }
}
