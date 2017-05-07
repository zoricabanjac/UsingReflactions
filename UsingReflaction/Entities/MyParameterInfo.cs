using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsingReflaction.Entities
{
    public class MyParameterInfo
    {
        public string ParameterName { get; set; }
        public string ParameterType { get; set; }

        public MyParameterInfo(string parameterType, string parameterName)
        {
            ParameterType = parameterType;
            ParameterName = parameterName;
        }
    }
}
