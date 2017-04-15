using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_intro
{
    public static class Calculation
    {
        public const decimal Planck = 6.62606896e-34m;
        public const decimal PI = 3.14159m;

        public static decimal ReducedPlanckConstant()
        {
            return (Planck) / (2 * PI);
        }
    }
}
