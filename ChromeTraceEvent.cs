using System;
using Microsoft.Diagnostics.Tracing;

namespace ChromeTrace
{
    public class ChromeTraceEvent : TraceEvent
    {
        private readonly Action<ChromeTraceEvent> _action;

        public ChromeTraceEvent(Action<ChromeTraceEvent> action, int eventId, int task, string taskName, Guid taskGuid,
            int opcode, string opcodeName, Guid providerGuid, string providerName)
            : base(eventId, task, taskName, taskGuid, opcode, opcodeName, providerGuid, providerName)
        {
            _action = action;
        }

        public string Message
        {
            get { return GetUTF8StringAt(0); }
        }

        public override string[] PayloadNames
        {
            get { return new string[0]; }
        }

        protected override void Dispatch()
        {
            _action(this);
        }

        public override object PayloadValue(int index)
        {
            return null;
        }
    }
}