using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Prozessanzahl
{
    public partial class Prozessmanager : Form
    {
        public Prozessmanager(string[] args)
        {
            InitializeComponent();

            if (args.Length > 3)
            {
                int maximaleAusfuehrungen = Convert.ToInt32(args[0]);
                int checkInterval = Convert.ToInt32(args[1]);
                string prozessname = args[2];
                string errormessage = args[3];

                while (true)
                {
                    Process[] ps = Process.GetProcessesByName(prozessname);

                    if (ps.Length > maximaleAusfuehrungen)
                    {
                        int lastNum = ps.Length - maximaleAusfuehrungen;
                        var last = (from p in ps orderby p.StartTime descending select p).Take(lastNum);

                        foreach (Process p in last)
                        {
                            p.Kill();
                        }

                        MessageBox.Show(errormessage);
                    }

                    System.Threading.Thread.Sleep(checkInterval);
                }
            }
            else
            {
                MessageBox.Show("Die Prozessüberwachung erfordert mehr Parameter");
            }
        }
    }
}
