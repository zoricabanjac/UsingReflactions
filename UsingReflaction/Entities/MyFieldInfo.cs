using System.Reflection;

namespace UsingReflaction.Entities
{
    public class MyFieldInfo
    {
        public FieldInfo FieldInformation { get; set; }
        public string FieldName { get; set; }
        public string FieldType { get; set; }
        public MemberTypes MemberType { get; set; }
        public FieldInfo FieldInfo { get; set; }

        public MyFieldInfo(FieldInfo fieldInformation, string fieldName, string fieldType, MemberTypes memberType)
        {
            FieldInformation = fieldInformation;
            FieldName = fieldName;
            FieldType = fieldType;
            MemberType = memberType;
        }
    }
}
