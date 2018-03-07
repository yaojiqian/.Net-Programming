using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttributeTest
{
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Method|AttributeTargets.Property)]
    class SimpleDescriptionAttribute : Attribute
    {
        private string description;
        private log4net.ILog logger = log4net.LogManager.GetLogger("SimpleDescriptionAttribute");

        /// <summary>
        /// The default constructor.
        /// </summary>
        public SimpleDescriptionAttribute() : base()
        {
            logger.Debug("Enter default constructor.");
        }
        
        public SimpleDescriptionAttribute(string desc)
        {
            logger.Debug("Enter string constructor: " + desc);
            description = desc;
        }

        public string Descripton
        {
            get
            {
                logger.Debug("Enter Description property.");
                return description;
            }
        }

        ~SimpleDescriptionAttribute()
        {
            logger.Debug("Enter ~SimpleDescriptionAttribute.");
        }
        
    }
}
