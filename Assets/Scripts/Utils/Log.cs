using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Assets.Scripts.Utils
{
    public enum LogBookEntryType
    {
        INFO = 0,
        ERROR = 1
    }

    public class Log
    {
        private static Log instance = null;

        private Dictionary<LogBookEntryType, string> logBook;

        Log()
        {
            logBook = new Dictionary<LogBookEntryType, string>();
        }

        public static Log Instance
        {
            get
            {
                return instance ?? (instance = new Log());
            }
            private set
            {
                //do nothing
            }
        }


        private void makeEntry(LogBookEntryType type, string message, Exception e)
        {
            var exMessage = "";

            if (e != null)
            {
                exMessage = string.Format("{0}\n", e.ToString());
            }

            var entry = string.Format("{0}: {1}\n{2}\n", Enum.GetName(typeof(LogBookEntryType), type), message, exMessage);

            logBook.Add(type, entry);
        }


        public void Info(string message, Exception e = null)
        {
            makeEntry(LogBookEntryType.INFO, message, e);
        }

        public void Error(string message, Exception e = null)
        {
            makeEntry(LogBookEntryType.ERROR, message, e);
        }

        public Dictionary<LogBookEntryType, string> getLogBook()
        {
            return logBook;
        }

    }
}
