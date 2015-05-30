using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Assets.Scripts.Utils
{
    

    public class Log
    {
        private static Log instance = null;

        private Dictionary<Enums.LogBookEntryType, string> logBook;

        Log()
        {
            logBook = new Dictionary<Enums.LogBookEntryType, string>();
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


        private void makeEntry(Enums.LogBookEntryType type, string message, Exception e)
        {
            var exMessage = "";

            if (e != null)
            {
                exMessage = string.Format("{0}\n", e.ToString());
            }

            var entry = string.Format("{0}: {1}\n{2}\n", Enum.GetName(typeof(Enums.LogBookEntryType), type), message, exMessage);

            logBook.Add(type, entry);
        }


        public void Info(string message, Exception e = null)
        {
            makeEntry(Enums.LogBookEntryType.INFO, message, e);
        }

        public void Error(string message, Exception e = null)
        {
            makeEntry(Enums.LogBookEntryType.ERROR, message, e);
        }

        public Dictionary<Enums.LogBookEntryType, string> getLogBook()
        {
            return logBook;
        }

    }
}
