using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;


namespace DVLD_DataAccessLayer
{
    public static class clsEventViewer
    {
        public static void SendEventLogApplication(string Message, EventLogEntryType Type)
        {
            string sourceApp = "DVLD";

            if (!EventLog.SourceExists(sourceApp))
            {
                EventLog.CreateEventSource(sourceApp, "Application");
            }
            EventLog.WriteEntry(sourceApp, Message, Type);
        }

        public static void SendEventLogSecurity(string Message, EventLogEntryType Type)
        {
            string sourceApp = "DVLD";

            if (!EventLog.SourceExists(sourceApp))
            {
                EventLog.CreateEventSource(sourceApp, "Security");
            }
            EventLog.WriteEntry(sourceApp, Message, Type);
        }

        
    }
}
