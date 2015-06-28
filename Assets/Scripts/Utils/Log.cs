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

            if (e != null)
            {
                exMessage = string.Format("{0}\n", e.ToString());
            }

            var entry = string.Format("{0}: {1}\n{2}", Enum.GetName(typeof(Enums.LogBookEntryType), type), message, exMessage);

            logBook.Add(entry);
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
            GUI.Label(new Rect(Screen.width * (1.0f / 3.0f), Screen.height * (3.0f / 4.0f), Screen.width * (1.0f / 3.0f), Screen.height * (1.0f / 4.0f)), logBookAsText, GUI.skin.textArea);
            //GUILayout.EndScrollView();
            //GUI.EndGroup();



        }
    }
}
