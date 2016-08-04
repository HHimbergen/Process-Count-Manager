using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Prozessanzahl
{
    public partial class Prozessmanager : Form
    {
        public Logger processLogger = new Logger();

        public Prozessmanager(string[] args)
        {
            // Initialisiere Designer Support
            InitializeComponent();
            // Initialisiere Prozessmanager
            InitProzessmanager();
        }

        /// <summary>
        /// Initialisiert den Prozessmanager
        /// </summary>
        public void InitProzessmanager()
        {
            try
            {
                int maximaleAusfuehrungen = Properties.Settings.Default.maximaleAusfuehrungen;
                int checkInterval = Properties.Settings.Default.checkInterval;
                string prozessname = Properties.Settings.Default.processName;
                string errormessage = Properties.Settings.Default.errorMessage;
                StartProzessmanager(prozessname, maximaleAusfuehrungen, checkInterval, errormessage);
            }
            catch (Exception)
            {
                processLogger.addLog("Es fehlen Einstellungen zum Programmaufruf");
                ShowMessageBox("Die Prozessüberwachung erfordert Parameter. Prüfen Sie die Einstellungen!");
            }
        }

        /// <summary>
        /// Startet den Prozessmanager als ausgekapselte Methode
        /// </summary>
        /// <param name="prozessname"></param>
        /// <param name="maximaleAusfuehrungen"></param>
        /// <param name="errormessage"></param>
        public void StartProzessmanager(string prozessname, int maximaleAusfuehrungen, int checkInterval,  string errormessage = "Die maximale Programmanzahl wurde überschritten")
        {
            processLogger.addLog("Prozessmanager ist gestartet");
            while (true)
            {
                Process[] ps = Process.GetProcessesByName(prozessname);

                if (ps.Length > maximaleAusfuehrungen)
                {
                    int lastNum = ps.Length - maximaleAusfuehrungen;
                    var last = (from p in ps orderby p.StartTime descending select p).Take(lastNum);

                    foreach (Process p in last)
                    {
                        try
                        {
                            p.Kill();
                            processLogger.addLog("Der Prozess " + p.ProcessName + " mit der Prozess-ID " + p.Id + " wurde beendet");
                        }
                        catch (Exception e)
                        {
                            processLogger.addLog("Der Prozess " + p.ProcessName + " mit der Prozess-ID " + p.Id + " konnte nicht beendet werden. Fehler: "+e.Message.ToString());
                        }
                        
                        
                    }

                    ShowMessageBox(errormessage);
                }

                var threadLogger = new Thread(
                    () =>
                    {
                        processLogger.printLog();
                    });

                threadLogger.Start();
                
                System.Threading.Thread.Sleep(checkInterval);
            }
        }

        /// <summary>
        /// Methode zum Anzeigen einer Messagebox.
        /// Wenn das Attribut asThread "true" ist, wird die
        /// MessageBox als Thread gestartet um den 
        /// Code nicht zu stoppen.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="asThread"></param>
        public void ShowMessageBox(string message, bool asThread = false)
        {
            if (asThread == true)
            {
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
