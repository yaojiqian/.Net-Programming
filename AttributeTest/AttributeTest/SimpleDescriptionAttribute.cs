using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttributeTest
{

    enum PropertyType
    {
        Type1,
        Type2,
        Type3
    }

    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Method|AttributeTargets.Property)]
    class SimpleDescriptionAttribute : Attribute
    {
        private string description;
        private PropertyType type = PropertyType.Type1;
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

        public SimpleDescriptionAttribute(PropertyType t)
        {
            type = t;
        }

        public SimpleDescriptionAttribute(string desc, PropertyType t)
        {
            description = desc;
            type = t;
        }

        public string Descripton
        {
            get
            {
                logger.Debug("Enter Description property.");
                return description;
            }
            set
            {
                description = value;
            }
        }

        public PropertyType Type
        {
            get { return type; }
            set { type = value; }
        }

        ~SimpleDescriptionAttribute()
        {
            logger.Debug("Enter ~SimpleDescriptionAttribute.");
        }
        
    }
}
