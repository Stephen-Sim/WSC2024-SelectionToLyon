using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FreshApi.Helpers
{
    public class Calculator
    {
        public static int Calculate(string s)
        {
            int result = 0;
            s = s.Replace(" ", "");

            int num = 0;
            int sign = 1;

            for (int i = 0; i < s.Length; i++)
            {
                char c = s[i];

                if (char.IsDigit(c))
                {
                    num = num * 10 + (c - '0');
                }
                else
                {
                    result += sign * num;
                    num = 0;
                    sign = (c == '+') ? 1 : -1;
                }
            }

            result += sign * num;
            return result;
        }
    }
}