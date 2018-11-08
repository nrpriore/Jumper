using UnityEngine;

// Static class that stores constants that govern game behavior
public static class Config {

	public const float COUNTDOWN_TIME		= 3f;
	public const float LEVEL_TIME 			= 45f;
	public const float GRAVITY				= -60f;
	public const float BASE_MULT			= .8f;
	public const float MULT_RANGE			= 1.2f;
	public const float PERCENT_TO_MAX_MULT	= 2f/3f; // Max mult at 15s left
	public const float STAGE_MULT			= 0.01f;

	public static float PLATFORM_STARTX;
	//---------------------------------------------------------------
	// Initializes calculated config
	public static void SetConfig() {
		PLATFORM_STARTX = 10f * Mathf.Tan(Camera.main.fieldOfView * 0.5f * Mathf.Deg2Rad) * (Camera.main.aspect * 1.5f);
		Physics.gravity = new Vector3(0,GRAVITY,0);
	}

}
