using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_intro
{
    public class MathUtil
    {
        public static float Sum(float a, float b)
        {
            return a + b;
        }

        public static float Subtract(float a, float b)
        {
            return a - b;
        }

        public static float Multiply(float a, float b)
        {
            return a * b;
        }

        public static float Divide(float a, float b)
        {
            return a / b;
        }

        public static float Percentage(float a, float b)
        {
            return (a * b) / 100;
        }
    }
}
