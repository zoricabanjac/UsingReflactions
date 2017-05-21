using System.Collections.Generic;
using System.Reflection;

namespace UsingReflaction.Entities
{
    public class MyConstructorInfo
    {
        public MemberTypes MemberType { get; set; }
        public ConstructorInfo ConstructorInfo { get; set; }
        public string ConstructorName { get; set; }
        public List<MyParameterInfo> Parameters { get; set; }

        private string parametersTypeName;
        public string ParametersTypeName
        {
            get
            {
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

            set
            {
                parametersTypeName = value;
            }
        }

        public MyConstructorInfo(MemberTypes memberType, ConstructorInfo constructorInfo, string constructorName, List<MyParameterInfo> parameters)
        {
            MemberType = memberType;
            ConstructorInfo = constructorInfo;
            ConstructorName = constructorName;
            Parameters = parameters;
        }    
    }
}
