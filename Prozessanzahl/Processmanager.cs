using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Processcounter
{
    public partial class Processcounter : Form
    {
        private Logger processLogger = new Logger();

        /// <summary>
        /// Class Construction of Process-Manager
        /// </summary>
        /// <param name="args"></param>
        public Processcounter(string[] args)
        {
            // Initi Processmanager
            InitProzessmanager();
        }

        /// <summary>
        /// Initalizing the process manager
        /// </summary>
        public void InitProzessmanager()
        {
            try
            {
                // Set runtime settings
                int maximaleAusfuehrungen = Properties.Settings.Default.maxProcesses; // Maximum count of same Processes
                int checkInterval = Properties.Settings.Default.checkInterval; // Check Interval contains the interval wich controls the time between the process-proofs
                string prozessname = Properties.Settings.Default.processName; // Contains the process-name without extension (e.g "notepad.exe" => "notepad")
                string errormessage = Properties.Settings.Default.errorMessage; // Contains the errorMessage if Process-Count is reached
                bool allowZeroProcesses = Properties.Settings.Default.allowZeroProcess; // Allow to set max. Process-Count to zero
                StartProcessmanager(prozessname, maximaleAusfuehrungen, checkInterval, allowZeroProcesses, errormessage);
            }
            catch (Exception e)
            {
                processLogger.addLog("Loading the Settings failed. Error: " + e.Message);
                ShowMessageBox(e.Message);
            }
        }

        /// <summary>
        /// Starts the process manager
        /// </summary>
        /// <param name="processname"></param>
        /// <param name="maxProcessesCount"></param>
        /// <param name="checkInterval"></param>
        /// <param name="allowZeroProcess"></param>
        /// <param name="errormessage"></param>
        public void StartProcessmanager(string processname, int maxProcessesCount, int checkInterval,  bool allowZeroProcess, string errormessage)
        {
            processLogger.addLog("Processmanager started");
            while (true)
            {
                Process[] ps = Process.GetProcessesByName(processname);

                if (ps.Length > maxProcessesCount && (maxProcessesCount > 0 || allowZeroProcess))
                {
                    int lastNum = ps.Length - maxProcessesCount;
                    var lastProcesses = (from p in ps orderby p.StartTime descending select p).Take(lastNum);

                    foreach (Process p in lastProcesses)
                    {
                        try
                        {
                            // Kill the process
                            p.Kill();
                            processLogger.addLog("The process " + p.ProcessName + " with the PID " + p.Id + " was killed");
                        }
                        catch (Exception e)
                        {
                            processLogger.addLog("The process " + p.ProcessName + " with the PID " + p.Id + " can not be killed. Error: "+ e.Message.ToString());
                        }
                    }

                    // Show a error that the maximum process count is reached
                    ShowMessageBox(errormessage);
                }

                // Thread to log events in log-file
                var threadLogger = new Thread(
                    () =>
                    {
                        processLogger.printLog();
                    });
                threadLogger.Start();
                
                // Inteval to not over-use the internal memory
                System.Threading.Thread.Sleep(checkInterval);
            }
        }

        /// <summary>
        /// Method to show a message box.
        /// If "asThread" is true it will starts the MessageBox
        /// as Thread. A Thread is needed to don't stop the code 
        /// during the process-check is running.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="asThread"></param>
        public void ShowMessageBox(string message, bool asThread = false)
        {
            // Check if message box have to loaded as thread
            if (asThread == true)
            {
                // Start a thread
                var thread = new Thread(
                    () =>
                    {
                        MessageBox.Show(message);
                    });
                thread.Start();
            }
            else
            {
                MessageBox.Show(message);
            }
        }
    }
}
