using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UsingReflaction.Entities
{
    public class MyPropertyInfo
    {
        public string PropertyName { get; set; }
        public string PropertyType { get; set; }
        public PropertyInfo PropertyInfo { get; set; }
        public MemberTypes MemberType { get; set; }

        public MyPropertyInfo(PropertyInfo propertyInfo, MemberTypes memberType, string propertyType, string propertyName)
        {
            PropertyInfo = propertyInfo;
            MemberType = memberType;
            PropertyName = propertyName;
            PropertyType = propertyType;
        }
    }
}
