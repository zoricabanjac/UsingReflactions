using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UsingReflaction.Entities
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
