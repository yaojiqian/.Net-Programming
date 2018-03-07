using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace AttributeTest
{
    class Program
    {
        private static log4net.ILog logger = log4net.LogManager.GetLogger("Program");

        [Obsolete("Don't use this method.")]
        [SimpleDescription("The old mothod.")]
        public static void Old() { Console.WriteLine("Hi Old"); }

        [SimpleDescription("The new method.")]
        public static void New() { Console.WriteLine("hi, new"); }

        [SimpleDescription("the AMethod1")]
        public void AMethod1()
        {
            Console.WriteLine("a method1");
        }

        [SimpleDescription("This is the property Prop")]
        public string Prop
        {
            get;
            set;
        }

        static void Main(string[] args)
        {
            logger.Debug("Hello log4net!");
            Old();
            New();


            Program pp = new Program();

            pp.Prop = "Hi Prop!";
            Console.WriteLine(pp.Prop);

            //Program at = new Program();
            Type type = typeof(Program);

            // Iterate through all the methods of the class.
            foreach (MethodInfo mInfo in type.GetMethods())
            {
                // Iterate through all the Attributes for each method.
                foreach (Attribute attr in Attribute.GetCustomAttributes(mInfo))
                {
                    // Check for the AnimalType attribute.
                    if (attr.GetType() == typeof(SimpleDescriptionAttribute))
                        Console.WriteLine( String.Format(
                            "Method {0} has a {1} attribute.",
                            mInfo.Name, ((SimpleDescriptionAttribute)attr).Descripton));
                }

            }

            foreach (PropertyInfo pInfo in type.GetProperties())
            {
                foreach (Attribute attr in Attribute.GetCustomAttributes(pInfo))
                {
                    // 
                    if (attr.GetType() == typeof(SimpleDescriptionAttribute))
                    {
                        Console.WriteLine(String.Format("Property {0} has a {1} attribute.",
                            pInfo.Name, ((SimpleDescriptionAttribute)attr).Descripton));
                    }
                }
            }


            Console.ReadLine();

        }
    }
}
