using System;
using Microsoft.Diagnostics.Tracing;

namespace ChromeTrace
{
    public sealed class ChromeEventParser : TraceEventParser, IDisposable
    {
        public static readonly Guid ProviderGuid = Guid.Parse("7FE69228-633E-4F06-80C1-527FEA23E3A7");
        private bool _disposed;

        public ChromeEventParser(TraceEventSource source)
            : base(source)
        {
            base.source.RegisterEventTemplate(new ChromeTraceEvent(OnTraceEvent, 10, 0, null, Guid.Empty, 10, "10",
                ProviderGuid, ProviderName));
        }

        public new static string ProviderName
        {
            get { return "CHROME"; }
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                source.UnregisterEventTemplate(new Action<ChromeTraceEvent>(OnTraceEvent), 10, ProviderGuid);
                _disposed = true;
            }
        }

        private void OnTraceEvent(ChromeTraceEvent @event)
        {
            Console.WriteLine(@event.Message);
        }
    }
}