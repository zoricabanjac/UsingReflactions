using System.Reflection;

namespace UsingReflection.Entities
{
    public class MyEventInfo
    {
        public EventInfo EventInfo { get; set; }
        public string EventName { get; set; }
        public MemberTypes MemberType { get; set; }

        public MyEventInfo(MemberTypes memberType, EventInfo eventInfo, string eventName)
        {
            MemberType = memberType;
            EventInfo = eventInfo;
            EventName = eventName;
        }
    }
}
