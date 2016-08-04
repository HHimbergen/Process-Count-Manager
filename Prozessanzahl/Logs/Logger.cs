using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Processcounter
{
    public class Logger
    {
        private List<LogMessage> logs = new List<LogMessage>();

        /// <summary>
        /// Add a log entry to the logger-component
        /// </summary>
        /// <param name="logmessage"></param>
        public void addLog(string logmessage) {
            // Creates a new log-entry object
            LogMessage entry = new LogMessage();
            // Fill log-entry object with useful data
            DateTime now = DateTime.Now;
            entry.datetime = now;
            entry.message = logmessage;
            // Add log-object 
            logs.Add(entry);
        }

        /// <summary>
        /// Get the List<LogMessage> Object with all added Log-Entrys
        /// </summary>
        /// <returns></returns>
        public List<LogMessage> getLog(bool allWhereNotPrinted = false)
        {
            if (allWhereNotPrinted)
            {
                // Return logs from component
                return logs;
            } else
            {
                // Get last logs wich are not printed yet
                return (from log in logs where log.isPrinted = false select log).ToList();
            }
        }

        /// <summary>
        /// Write log entrys into a log-file
        /// </summary>
        public void printLog()
        {
            using (System.IO.StreamWriter w = System.IO.File.AppendText("log.txt"))
            {
                // Get all Log-Messages and write them in the log file
                foreach (LogMessage log in getLog(true))
                {
                    // [Date and Time]: Message
                    w.WriteLine("[" + log.datetime.ToString("o") + "]: " + log.message);
                    log.isPrinted = true;
                }
            }
        }
    }
}
