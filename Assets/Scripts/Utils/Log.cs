using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Assets.Scripts.Utils
{


    public class Log : MonoBehaviour
    {
        private static Log instance = null;

        private static List<string> logBook;

        private static string logBookAsText;

        private static bool logOnGui;


        public static Log Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = GameObject.FindObjectOfType<Log>();
                    logBook = new List<string>();
                    logOnGui = false;
                    //Tell unity not to destroy this object when loading a new scene!
                    //DontDestroyOnLoad(instance.gameObject);
                }
                return instance;
            }
        }


        private void makeEntry(Enums.LogBookEntryType type, string message, Exception e)
        {
            var exMessage = "";
            var error = "";

            if (e != null)
            {
                exMessage = string.Format("{0}\n", e.ToString());
            }

            if(type == Enums.LogBookEntryType.ERROR)
            {
                error = string.Format("{0}: ", Enum.GetName(typeof(Enums.LogBookEntryType), type));
            }
            var entry = string.Format("{0}{1}\n{2}", error, message, exMessage);

            logBook.Add(entry);

            if (logBook.Count > 10)
            {
                logBook.RemoveRange(0, 5);
            }
        }


        public void Info(string message, Exception e = null)
        {
            makeEntry(Enums.LogBookEntryType.INFO, message, e);
        }

        public void Error(string message, Exception e = null)
        {
            makeEntry(Enums.LogBookEntryType.ERROR, message, e);
        }

        public List<string> getLogBook()
        {
            return logBook;
        }

        public void print()
        {
            logBookAsText = string.Empty;

            foreach(var entry in logBook)
            {
                logBookAsText = string.Concat(string.Format("{0}", entry), logBookAsText);
            }
        }

        public void toggleLogOnGui()
        {
            logOnGui = !logOnGui;
        }


        void OnGUI()
        {
			Instance.print();
            //GUI.BeginGroup(new Rect(Screen.width/2, Screen.height/2, 110, 130));  //note the 250 width and 305 height compared to the scrollview size
            //GUILayout.BeginScrollView(new Vector2(0, 0), GUILayout.Width(100), GUILayout.Height(100));
            var style = GUI.skin.textArea;
            style.fontSize = 24;
            GUI.Label(new Rect(Screen.width * (1.0f / 4.0f), Screen.height * (11.0f / 12.0f), Screen.width * (1.0f / 2.0f), Screen.height * (1.0f / 12.0f)), logBookAsText, style);
            //GUILayout.EndScrollView();
            //GUI.EndGroup();



        }
    }
}
