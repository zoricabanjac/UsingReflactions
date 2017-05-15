using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UsingReflaction.TestEntities;

namespace UsingReflaction.Entities
{
    public class MyMethodInfo
    {
        public MethodInfo MethodInfo { get; set; }
        public string MethodName { get; set; }
        public string ReturnType { get; set; }
        public MemberTypes MemberType { get; set; }
        public Object SelectedObject { get; set; }

        public List<MyParameterInfo> Parameters { get; set; }
        
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

        public MyMethodInfo(MemberTypes memberType, MethodInfo methodInfo, string returnType, string methodName, List<MyParameterInfo> parameters, Object selectedObject)
        {
            MemberType = memberType;
            MethodInfo = methodInfo;
            MethodName = methodName;
            ReturnType = returnType;
            Parameters = parameters;
            SelectedObject = selectedObject;
        }
    }
}
