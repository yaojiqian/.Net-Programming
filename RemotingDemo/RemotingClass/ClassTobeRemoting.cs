using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemotingClass
{
    public class ClassTobeRemoting : MarshalByRefObject
    {
        private DateTime createdTime = DateTime.MinValue;
        public ClassTobeRemoting()
        {
            createdTime = DateTime.Now;
            Console.WriteLine("{0} : ClassTobeRemoting is created!", createdTime);
        }

        ~ClassTobeRemoting()
        {
            Console.WriteLine("{0} : ClassTobeRemoting is gone!  Object: {1}", DateTime.Now, createdTime);
        }

        public DateTime GetServerTime()
        {
            Console.WriteLine("{0} : GetServerTime is call! Object: {1}", DateTime.Now, createdTime);
            return DateTime.Now;
        }

    }
}
