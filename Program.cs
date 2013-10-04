using Microsoft.Diagnostics.Tracing;
using Microsoft.Diagnostics.Tracing.Session;

namespace ChromeTrace
{
    internal class Program
    {
        private static void Main()
        {
            var session = new TraceEventSession("CHROMETRACE");
            var source = new ETWTraceEventSource("CHROMETRACE", TraceEventSourceType.Session);
            session.EnableProvider(ChromeEventParser.ProviderGuid, TraceEventLevel.Informational);
            using (new ChromeEventParser(source))
            {
                source.Process();
            }
        }
    }
}