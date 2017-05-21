using System.Reflection;

namespace UsingReflaction.Entities
{
    public class MyPropertyInfo
    {
        public string PropertyName { get; set; }
        public string PropertyType { get; set; }
        public PropertyInfo PropertyInformation { get; set; }
        public MemberTypes MemberType { get; set; }
      
        public MyPropertyInfo(PropertyInfo propertyInformation, MemberTypes memberType, string propertyType, string propertyName)
        {
            PropertyInformation = propertyInformation;
            MemberType = memberType;
            PropertyName = propertyName;
            PropertyType = propertyType;
        }
    }
}
