using UnityEngine;
using UnityEngine.SceneManagement;

// Governs game progression, stats, and mechanics
public class Game : MonoBehaviour {

	// Gameplay variables -------------------------------------------
	// If the game is actively running
	private static bool _active;
	public static bool Active {
		get{return _active;}
	}
	// Tells the newest platform to spawn the final platform
	private static bool _triggerFinalPlatform;
	public static bool TriggerFinalPlatform {
		get{return _triggerFinalPlatform;}
	}
	// The player's gameobject
	private static GameObject _currPlayer;
	public static GameObject CurrPlayer {
		get{return _currPlayer;}
	}

	// Stats
	private int _numPlatforms;


#region // Functions
	// Returns % of max game speed (0 - 1)
	public static float TimeRatio {
		get{return Mathf.Min(TimeLeft.Timer / (Config.PERCENT_TO_MAX_MULT * Config.LEVEL_TIME),1f);}
	}
	// Alters speed variable into a value usable by the game
	public static float SpeedMult {
		get{return (Config.BASE_MULT + (TimeRatio * Config.MULT_RANGE)) / 8f;}
	}
	// Returns the size of the gap between platforms based on game speed
	public static float GapSize {
		get{return 0.83f * Player.JumpDistance;}
	}
#endregion

	//---------------------------------------------------------------
	// Runs on app startup
	void Start() {
		// Initialize data
		Prefabs.LoadPrefabs();
		Config.SetConfig();

		// Initialize starting vars
		_active = false;
		_triggerFinalPlatform = false;

		// Initialize starting objects
		CreateStartingScene();
		StartCountdown();
	}

	//---------------------------------------------------------------
	// Runs every frame
	void Update() {

	}

	//---------------------------------------------------------------
	// Creates the starting platform
	private void CreateStartingScene() {
		Platform.Create(12, 1);
		_currPlayer = Instantiate<GameObject>(Prefabs.Player);
	}

	//---------------------------------------------------------------
	// Starts countdown to begin game, runs on 'Start' button click
	public static void StartCountdown() {
		Countdown.StartCountdown();
	}

	//---------------------------------------------------------------
	// Starts game, runs when countdown ends
	public static void StartGame() {
		_active = true;
		TimeLeft.StartTimeLeft();
	}

	//---------------------------------------------------------------
	// Triggers final platform creation, runs when timer hits 1s left so you land on final platform around 0s left
	public static void SetTriggerFinalPlatform() {
		_triggerFinalPlatform = true;
	}

	//---------------------------------------------------------------
	// Runs when player lands on final platform
	public static void WinGame() {
		_active = false;
	}

	//---------------------------------------------------------------
	// Runs if player falls off screen
	public static void LoseGame() {
		_active = false;
	}

	//---------------------------------------------------------------
	// Restarts the game scene
	public void Restart() {
		SceneManager.LoadSceneAsync("Game", LoadSceneMode.Single);
	}

}
