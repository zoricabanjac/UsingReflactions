namespace UsingReflection.Entities
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
