using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UsingReflaction.TestEntities;

namespace UsingReflaction.Entities
{
    public class MyConstructorInfo
    {
        public MemberTypes MemberType { get; set; }
        public ConstructorInfo ConstructorInfo { get; set; }
        public string ConstructorName { get; set; }
        public List<MyParameterInfo> Parameters { get; set; }
        public Object SelectedObject { get; set; }

        public string ParametersTypeName
        {
            get
            {
                string parametersTypeName = null;
                foreach (MyParameterInfo parameter in Parameters)
                {
                    parametersTypeName += parameter.ParameterType + " " + parameter.ParameterName;
                    if (Parameters.IndexOf(parameter) < Parameters.Count - 1)
                    {
                        parametersTypeName += ", ";
                    }
                }

                return parametersTypeName;
            }
        }

        public MyConstructorInfo(MemberTypes memberType, ConstructorInfo constructorInfo, string constructorName, List<MyParameterInfo> parameters, Object selectedObject)
        {
            MemberType = memberType;
            ConstructorInfo = constructorInfo;
            ConstructorName = constructorName;
            Parameters = parameters;
            SelectedObject = selectedObject;
        }    
    }
}
