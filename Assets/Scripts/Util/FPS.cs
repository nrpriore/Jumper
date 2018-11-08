using System.Linq;
using System.Collections.Generic;
using UnityEngine;

// Utility class that returns the FPS
public class FPS : MonoBehaviour {
	private static FPS _fps;

	private const int MAX_LIST_LENGTH = 30;
	private List<float> _fpsList;

	//---------------------------------------------------------------
	// Returns the average FPS
	public static float AvgFPS {
		get{return _fps._fpsList.Average();}
	}
	
	
	//---------------------------------------------------------------
	// Runs on scene startup
	void Start () {
		_fps = this;
		_fpsList = new List<float>();
	}
	
	// Runs every frame
	void Update () {
		if(_fpsList.Count == MAX_LIST_LENGTH) {
			_fpsList.RemoveAt(0);
		}
		_fpsList.Add(1f / Time.deltaTime);
	}
}
