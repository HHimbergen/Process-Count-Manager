using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Processcounter
{
    public class LogMessage
    {
        // Getter and setter for message
        private string _message;
        public string message {
            get
            {
                return _message;
            }

            set
            {
                _message = value;
            }
        }

        // Getter and setter for datetime-object
        private DateTime _datetime;
        public DateTime datetime
        {
            get
            {
                return _datetime;
            }

            set
            {
                _datetime = value;
            }
        }

        // Set to true if log-message is printed to the logfile
        private bool _isPrinted;
        public bool isPrinted {
            get
            {
                return _isPrinted;
            }

            set
            {
                _isPrinted = value;
            }
        }
    }
}
