using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UsingReflaction.Entities
{
    public class MyFieldInfo
    {
        public FieldInfo FieldFullName { get; set; }
        public string FieldName { get; set; }
        public string FieldType { get; set; }
        public MemberTypes MemberType { get; set; }

        public MyFieldInfo(FieldInfo fieldFullName, string fieldName, string fieldType, MemberTypes memberType)
        {
            FieldFullName = fieldFullName;
            FieldName = fieldName;
            FieldType = fieldType;
            MemberType = memberType;
        }
    }
}
