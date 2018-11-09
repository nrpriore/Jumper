using UnityEngine;
using UnityEngine.UI;

// Governs the countdown timer and UI
public class TimeLeft : MonoBehaviour {
	
	private static TimeLeft _timeLeft;
	private float _timer;
	public static float Timer {
		get{return _timeLeft._timer;}
	}

	private GameObject _container;
	private Text _timeLeftText;

	//---------------------------------------------------------------
	// Runs on scene startup
	void Start () {
		_timeLeft = this;
		_container = transform.Find("Container").gameObject;
		_timeLeftText = _container.transform.Find("TimeLeftText").GetComponent<Text>();

		_timer = 0f;
	}
	
	//---------------------------------------------------------------
	// Runs every frame
	void Update () {
		if(_container.activeSelf && Game.Active) {
			_timer += Time.deltaTime;
			_timeLeftText.text = Mathf.Max(Mathf.CeilToInt(Config.LEVEL_TIME - _timer), 0).ToString();

			if(_timer >= Config.LEVEL_TIME-0.5f && !Game.TriggerFinalPlatform) {
				Game.SetTriggerFinalPlatform();
			}
			else if(_timer >= Config.LEVEL_TIME) {
				//EndTimeLeft();
			}
		}
	}

	//---------------------------------------------------------------
	// Starts time left
	public static void StartTimeLeft() {
		_timeLeft._timer = 0f;
		_timeLeft._container.SetActive(true);
	}

	//---------------------------------------------------------------
	// Ends time left
	public static void EndTimeLeft() {
		_timeLeft._container.SetActive(false);
	}
}
