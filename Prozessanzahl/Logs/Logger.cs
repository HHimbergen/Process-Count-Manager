using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prozessanzahl
{
    public class Logger
    {
        public List<LogMessage> logs = new List<LogMessage>();

        public void addLog(string logmessage) {
            LogMessage entry = new LogMessage();
            entry.datetime = DateTime.Now;
            entry.message = logmessage;
            logs.Add(entry);
        }

        public List<LogMessage> getLog()
        {
            return logs;
        }

        public void printLog()
        {
            using (System.IO.StreamWriter w = System.IO.File.AppendText("log.txt"))
            {
                foreach (LogMessage l in getLog())
                {
                    w.WriteLine("[" + l.datetime.ToString("o") + "]: " + l.message);
                }

                logs.Clear();
            }
        }
    }
}
