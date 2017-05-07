using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsingReflaction.Entities
{
    public class MyNestedTypeInfo
    {
        public string NestedTypeName { get; set; }
        
        public MyNestedTypeInfo(string typeName)
        {
            NestedTypeName = typeName;
        }
    }
}
