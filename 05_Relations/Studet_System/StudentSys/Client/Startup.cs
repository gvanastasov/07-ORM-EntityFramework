namespace Client
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Data;

    class Startup
    {
        static void Main(string[] args)
        {
            var ctx = new StudentSystemContext();
            ctx.Database.Initialize(true);

        }
    }
}
