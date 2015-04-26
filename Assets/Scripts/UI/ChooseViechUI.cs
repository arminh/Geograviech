using UnityEngine;
using System.Collections;

public class ChooseViechUI : MonoBehaviour {

	 private GUISkin skin;
 
     void Start()
     {
         // Load a skin for the buttons
         skin = Resources.Load("GUISkin3") as GUISkin;
     }

     void OnGUI()
     {

         // Constrain all drawing to be within a 800x600 pixel area centered on the screen.
         GUI.BeginGroup(new Rect(Screen.width / 2 - 200, Screen.height / 2 - 200, 400, 400));

         // Draw a box in the new coordinate space defined by the BeginGroup.
         // Notice how (0,0) has now been moved on-screen
         GUI.Button(new Rect(0, 0, 200, 200), "Button");
         GUI.Button(new Rect(0, 200, 200, 200), "Button");
         GUI.Button(new Rect(200, 0, 200, 200), "Button");
         GUI.Button(new Rect(200, 200, 200, 200), "Button");

         // We need to match all BeginGroup calls with an EndGroup
         GUI.EndGroup();
     }

}
