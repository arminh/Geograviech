// 
//  TestMap.cs
//  15.442552, 47.067243  
//  Author:
//       Jonathan Derrough <jonathan.derrough@gmail.com>
//  
//  Copyright (c) 2012 Jonathan Derrough
// 
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
// 
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Lesser General Public License for more details.
// 
//  You should have received a copy of the GNU Lesser General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using UnityEngine;

using System;

using UnitySlippyMap;
using ProjNet.CoordinateSystems;
using ProjNet.CoordinateSystems.Transformations;
using ProjNet.Converters.WellKnownText;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using Assets.Scripts.FightCharacters;
using Assets.Scripts.Utils;

public class TestMap : MonoBehaviour
{
	public Texture	LocationTexture;
	public Texture	MarkerAlrauneTexture;
	public Texture	MarkerGargoyleTexture;
	public Texture	MarkerImpTexture;
	public Texture	MarkerSireneTexture;
	public Texture	MarkerPantherTexture;
	public Texture	MarkerZerberWelpeTexture;


	private Map		map;

	private float	guiXScale;
	private float	guiYScale;
	private Rect	guiRect;
	
	private bool 	isPerspectiveView = false;
	private float	perspectiveAngle = 30.0f;
	private float	destinationAngle = 0.0f;
	private float	currentAngle = 0.0f;
	private float	animationDuration = 0.5f;
	private float	animationStartTime = 0.0f;
	
	private List<Layer> layers;
	private int     currentLayerIndex = 0;


	bool Toolbar(Map map)
	{
		GUI.matrix = Matrix4x4.Scale(new Vector3(guiXScale, guiXScale, 1.0f));
		
		GUILayout.BeginArea(guiRect);
		
		GUILayout.BeginHorizontal();
		
		//GUILayout.Label("Zoom: " + map.CurrentZoom);
		
		bool pressed = false;
		if (GUILayout.RepeatButton("+", GUILayout.ExpandHeight(true)))
		{
			map.Zoom(1.0f);
			pressed = true;
		}
		if (Event.current.type == EventType.Repaint)
		{
			Rect rect = GUILayoutUtility.GetLastRect();
			if (rect.Contains(Event.current.mousePosition))
				pressed = true;
		}
		
		if (GUILayout.Button("2D/3D", GUILayout.ExpandHeight(true)))
		{
			if (isPerspectiveView)
			{
				destinationAngle = -perspectiveAngle;
			}
			else
			{
				destinationAngle = perspectiveAngle;
			}
			
			animationStartTime = Time.time;
			
			isPerspectiveView = !isPerspectiveView;
		}
		if (Event.current.type == EventType.Repaint)
		{
			Rect rect = GUILayoutUtility.GetLastRect();
			if (rect.Contains(Event.current.mousePosition))
				pressed = true;
		}
		
		if (GUILayout.Button("Center", GUILayout.ExpandHeight(true)))
		{
			map.CenterOnLocation();
		}
		if (Event.current.type == EventType.Repaint)
		{
			Rect rect = GUILayoutUtility.GetLastRect();
			if (rect.Contains(Event.current.mousePosition))
				pressed = true;
		}
		
		string layerMessage = String.Empty;
		if (map.CurrentZoom > layers[currentLayerIndex].MaxZoom)
			layerMessage = "\nZoom out!";
		else if (map.CurrentZoom < layers[currentLayerIndex].MinZoom)
			layerMessage = "\nZoom in!";
		if (GUILayout.Button(((layers != null && currentLayerIndex < layers.Count) ? layers[currentLayerIndex].name + layerMessage : "Layer"), GUILayout.ExpandHeight(true)))
		{
			#if UNITY_3_0 || UNITY_3_1 || UNITY_3_2 || UNITY_3_3 || UNITY_3_4 || UNITY_3_5 || UNITY_3_6 || UNITY_3_7 || UNITY_3_8 || UNITY_3_9
			layers[currentLayerIndex].gameObject.SetActiveRecursively(false);
			#else
			layers[currentLayerIndex].gameObject.SetActive(false);
			#endif
			++currentLayerIndex;
			if (currentLayerIndex >= layers.Count)
				currentLayerIndex = 0;
			#if UNITY_3_0 || UNITY_3_1 || UNITY_3_2 || UNITY_3_3 || UNITY_3_4 || UNITY_3_5 || UNITY_3_6 || UNITY_3_7 || UNITY_3_8 || UNITY_3_9
			layers[currentLayerIndex].gameObject.SetActiveRecursively(true);
			#else
			layers[currentLayerIndex].gameObject.SetActive(true);
			#endif
			map.IsDirty = true;
		}
		
		if (GUILayout.RepeatButton("-", GUILayout.ExpandHeight(true)))
		{
			map.Zoom(-1.0f);
			pressed = true;
		}
		if (Event.current.type == EventType.Repaint)
		{
			Rect rect = GUILayoutUtility.GetLastRect();
			if (rect.Contains(Event.current.mousePosition))
				pressed = true;
		}
		
		GUILayout.EndHorizontal();
		
		GUILayout.EndArea();
		
		return pressed;
	}
	
	private
		#if !UNITY_WEBPLAYER
		IEnumerator
			#else
			void
			#endif
			Start()
	{

		//gps localization from: https://northrush.wordpress.com/2014/01/13/access-gps-data-on-unity-3d/

		// First, check if user has location service enabled
		if (!Input.location.isEnabledByUser)
		{
			print ("GPS not enabled!");
			// remind user to enable GPS
			// As far as I know, there is no way to forward user to GPS setting menu in Unity
		}
		
		// Start service before querying location
		Input.location.Start();
		
		// Wait until service initializes
		int maxWait = 20;
		while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0) {
			yield return new WaitForSeconds(1);
			maxWait--;
		}
		
		// Service didn't initialize in 20 seconds
		if (maxWait < 1) {
			print("Timed out");
		}
		
		// Connection has failed
		if (Input.location.status == LocationServiceStatus.Failed) {
			print("Unable to determine device location");
		}
		
		// Access granted and location value could be retrieved
		else
			print("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " +       Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " +  Input.location.lastData.timestamp);
		
		// Stop service if there is no need to query location updates continuously
		Input.location.Stop();

		//----------------------

		GameManager.Instance.init();
		
		// setup the gui scale according to the screen resolution
		guiXScale = (Screen.orientation == ScreenOrientation.Landscape ? Screen.width : Screen.height) / 480.0f;
		guiYScale = (Screen.orientation == ScreenOrientation.Landscape ? Screen.height : Screen.width) / 640.0f;
		// setup the gui area
		guiRect = new Rect(16.0f * guiXScale, 4.0f * guiXScale, Screen.width / guiXScale - 32.0f * guiXScale, 32.0f * guiYScale);
		
		// create the map singleton
		map = Map.Instance;
		map.CurrentCamera = Camera.main;
		map.InputDelegate += UnitySlippyMap.Input.MapInput.BasicTouchAndKeyboard;
		map.CurrentZoom = 17.0f;
		// 9 rue Gentil, Lyon
		//map.CenterWGS84 = new double[2] { 15.442552, 47.067243 };
		//WORKS!
		map.CenterWGS84 = new double[2] { Input.location.lastData.latitude, Input.location.lastData.longitude };

		////Set-up Android SDK path to make Android remote work
	
		map.UseLocation = true;
		map.InputsEnabled = true;
		map.ShowGUIControls = false;
		
		map.GUIDelegate += Toolbar;


		layers = new List<Layer>();


		// create an OSM tile layer
		OSMTileLayer osmLayer = map.CreateLayer<OSMTileLayer>("OSM");
		osmLayer.BaseURL = "http://a.tile.openstreetmap.org/";
		
		layers.Add(osmLayer);
		
		// create a WMS tile layer
		//WMSTileLayer wmsLayer = map.CreateLayer<WMSTileLayer>("WMS");
		//wmsLayer.BaseURL = "http://129.206.228.72/cached/osm?"; // http://www.osm-wms.de : seems to be of very limited use
		//wmsLayer.Layers = "osm_auto:all";
		//wmsLayer.BaseURL = "http://vmap0.tiles.osgeo.org/wms/vmap0";
		//wmsLayer.Layers = "basic";
		//#if UNITY_3_0 || UNITY_3_1 || UNITY_3_2 || UNITY_3_3 || UNITY_3_4 || UNITY_3_5 || UNITY_3_6 || UNITY_3_7 || UNITY_3_8 || UNITY_3_9
		//wmsLayer.gameObject.SetActiveRecursively(false);
		//#else
		//wmsLayer.gameObject.SetActive(false);
		//#endif
		
		//layers.Add(wmsLayer);
		//wmsLayer.gameObject.SetActive (true);



		
		#if !UNITY_WEBPLAYER // FIXME: SQLite won't work in webplayer except if I find a full .NET 2.0 implementation (for free)
		// create an MBTiles tile layer
		bool error = false;
		// on iOS, you need to add the db file to the Xcode project using a directory reference
		string mbTilesDir = "MBTiles/";
		string filename = "UnitySlippyMap_World_0_8.mbtiles";
		string filepath = null;
		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			filepath = Application.streamingAssetsPath + "/" + mbTilesDir + filename;
		}
		else if (Application.platform == RuntimePlatform.Android)
		{
			// Note: Android is a bit tricky, Unity produces APK files and those are never unzip on the device.
			// Place your MBTiles file in the StreamingAssets folder (http://docs.unity3d.com/Documentation/Manual/StreamingAssets.html).
			// Then you need to access the APK on the device with WWW and copy the file to persitentDataPath
			// to that it can be read by SqliteDatabase as an individual file
			string newfilepath = Application.temporaryCachePath + "/" + filename;
			if (File.Exists(newfilepath) == false)
			{
				Debug.Log("DEBUG: file doesn't exist: " + newfilepath);
				filepath = Application.streamingAssetsPath + "/" + mbTilesDir + filename;
				// TODO: read the file with WWW and write it to persitentDataPath
				WWW loader = new WWW(filepath);
				yield return loader;
				if (loader.error != null)
				{
					Debug.LogError("ERROR: " + loader.error);
					error = true;
				}
				else
				{
					Debug.Log("DEBUG: will write: '" + filepath + "' to: '" + newfilepath + "'");
					File.WriteAllBytes(newfilepath, loader.bytes);
				}
			}
			else
				Debug.Log("DEBUG: exists: " + newfilepath);
			filepath = newfilepath;
		}
		else
		{
			filepath = Application.streamingAssetsPath + "/" + mbTilesDir + filename;
		}
		
		if (error == false)
		{
			Debug.Log("DEBUG: using MBTiles file: " + filepath);
			MBTilesLayer mbTilesLayer = map.CreateLayer<MBTilesLayer>("MBTiles");
			mbTilesLayer.Filepath = filepath;
			#if UNITY_3_0 || UNITY_3_1 || UNITY_3_2 || UNITY_3_3 || UNITY_3_4 || UNITY_3_5 || UNITY_3_6 || UNITY_3_7 || UNITY_3_8 || UNITY_3_9
			mbTilesLayer.gameObject.SetActiveRecursively(false);
			#else
			mbTilesLayer.gameObject.SetActive(false);
			#endif
			
			layers.Add(mbTilesLayer);
		}
		else
			Debug.LogError("ERROR: MBTiles file not found!");
		
		#endif


		//Imp Marker
		GameObject go = Tile.CreateTileTemplate(Tile.AnchorPoint.BottomCenter).gameObject;
		go.GetComponent<Renderer>().material.mainTexture = MarkerImpTexture;
		go.GetComponent<Renderer>().material.renderQueue = 4001;
		go.transform.localScale = new Vector3(0.70588235294118f, 1.0f, 1.0f);
		go.transform.localScale /= 3.0f;
		go.AddComponent<CameraFacingBillboard>().Axis = Vector3.up;
		
		GameObject markerGO;
		markerGO = Instantiate(go) as GameObject;
		map.CreateMarker<Marker>("Imp", new double[2] { 15.442552, 47.067243}, markerGO);
		DestroyImmediate(go);

		//AlraunenMarker
		GameObject go2 = Tile.CreateTileTemplate(Tile.AnchorPoint.BottomCenter).gameObject;
		go2.GetComponent<Renderer>().material.mainTexture = MarkerAlrauneTexture;
		go2.GetComponent<Renderer>().material.renderQueue = 4001;
		go2.transform.localScale = new Vector3(0.70588235294118f, 1.0f, 1.0f);
		go2.transform.localScale /= 3.0f;
		go2.AddComponent<CameraFacingBillboard>().Axis = Vector3.up;
		
		GameObject markerGO2;
		markerGO2 = Instantiate(go2) as GameObject;
		map.CreateMarker<Marker>("Alraune", new double[2] { 15.443552, 47.068243}, markerGO2);

		DestroyImmediate(go2);


		//Gargoyle Marker
		GameObject go3 = Tile.CreateTileTemplate(Tile.AnchorPoint.BottomCenter).gameObject;
		go3.GetComponent<Renderer>().material.mainTexture = MarkerGargoyleTexture;
		go3.GetComponent<Renderer>().material.renderQueue = 4001;
		go3.transform.localScale = new Vector3(0.70588235294118f, 1.0f, 1.0f);
		go3.transform.localScale /= 3.0f;
		go3.AddComponent<CameraFacingBillboard>().Axis = Vector3.up;
		
		GameObject markerGO3;
		markerGO3 = Instantiate(go3) as GameObject;
		map.CreateMarker<Marker>("Gargoyle", new double[2] { 15.444552, 47.068143}, markerGO3);
		
		DestroyImmediate(go3);


		//Sirene Marker
		GameObject go4 = Tile.CreateTileTemplate(Tile.AnchorPoint.BottomCenter).gameObject;
		go4.GetComponent<Renderer>().material.mainTexture = MarkerSireneTexture;
		go4.GetComponent<Renderer>().material.renderQueue = 4001;
		go4.transform.localScale = new Vector3(0.70588235294118f, 1.0f, 1.0f);
		go4.transform.localScale /= 3.0f;
		go4.AddComponent<CameraFacingBillboard>().Axis = Vector3.up;
		
		GameObject markerGO4;
		markerGO4 = Instantiate(go4) as GameObject;
		map.CreateMarker<Marker>("Sirene", new double[2] { 15.444632, 47.068443}, markerGO4);
		
		DestroyImmediate(go4);

		//Panther Marker
		GameObject go6 = Tile.CreateTileTemplate(Tile.AnchorPoint.BottomCenter).gameObject;
		go6.GetComponent<Renderer>().material.mainTexture = MarkerPantherTexture;
		go6.GetComponent<Renderer>().material.renderQueue = 4001;
		go6.transform.localScale = new Vector3(0.70588235294118f, 1.0f, 1.0f);
		go6.transform.localScale /= 3.0f;
		go6.AddComponent<CameraFacingBillboard>().Axis = Vector3.up;
		
		GameObject markerGO6;
		markerGO6 = Instantiate(go6) as GameObject;
		map.CreateMarker<Marker>("Panther", new double[2] { 15.443752, 47.067553}, markerGO6);
		
		DestroyImmediate(go6);

		//Zerber Marker
		GameObject go7 = Tile.CreateTileTemplate(Tile.AnchorPoint.BottomCenter).gameObject;
		go7.GetComponent<Renderer>().material.mainTexture = MarkerZerberWelpeTexture;
		go7.GetComponent<Renderer>().material.renderQueue = 4001;
		go7.transform.localScale = new Vector3(0.70588235294118f, 1.0f, 1.0f);
		go7.transform.localScale /= 3.0f;
		go7.AddComponent<CameraFacingBillboard>().Axis = Vector3.up;
		
		GameObject markerGO7;
		markerGO7 = Instantiate(go7) as GameObject;
		map.CreateMarker<Marker>("Zerber", new double[2] { 15.444963, 47.068573}, markerGO7);
		
		DestroyImmediate(go7);


		// create the location marker
		go = Tile.CreateTileTemplate().gameObject;
		go.GetComponent<Renderer>().material.mainTexture = LocationTexture;
		go.GetComponent<Renderer>().material.renderQueue = 4000;
		go.transform.localScale /= 3.0f;
		
		markerGO = Instantiate(go) as GameObject;

		//markerGO.AddComponent<BoxCollider>();

		map.SetLocationMarker<LocationMarker>(markerGO);
		
		DestroyImmediate(go);

	}
	
	void OnApplicationQuit()
	{
		map = null;
	}
	
	void Update()
	{
		if (destinationAngle != 0.0f)
		{
			Vector3 cameraLeft = Quaternion.AngleAxis(-90.0f, Camera.main.transform.up) * Camera.main.transform.forward;
			if ((Time.time - animationStartTime) < animationDuration)
			{
				float angle = Mathf.LerpAngle(0.0f, destinationAngle, (Time.time - animationStartTime) / animationDuration);
				Camera.main.transform.RotateAround(Vector3.zero, cameraLeft, angle - currentAngle);
				currentAngle= angle;
			}
			else
			{
				Camera.main.transform.RotateAround(Vector3.zero, cameraLeft, destinationAngle - currentAngle);
				destinationAngle = 0.0f;
				currentAngle = 0.0f;
				map.IsDirty = true;
			}
			
			map.HasMoved = true;
		}
	}
	
	#if DEBUG_PROFILE
	void LateUpdate()
	{
		Debug.Log("PROFILE:\n" + UnitySlippyMap.Profiler.Dump());
		UnitySlippyMap.Profiler.Reset();
	}
	#endif
}

