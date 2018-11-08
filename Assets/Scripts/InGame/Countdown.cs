using UnityEngine;
using UnityEngine.UI;

// Governs the countdown timer and UI
public class Countdown : MonoBehaviour {

	private static Countdown _countdown;
	private float _timer;

	private GameObject _container;
	private Text _countdownText;

	//---------------------------------------------------------------
	// Runs on scene startup
	void Start () {
		_countdown = this;
		_container = transform.Find("Container").gameObject;
		_countdownText = _container.transform.Find("CountdownText").GetComponent<Text>();

		_timer = 0f;
	}
	
	//---------------------------------------------------------------
	// Runs every frame
	void Update () {
		if(_container.activeSelf) {
			_timer += Time.deltaTime;
			_countdownText.text = Mathf.CeilToInt(Config.COUNTDOWN_TIME - _timer).ToString();

			if(_timer >= Config.COUNTDOWN_TIME) {
				Game.StartGame();
				_container.SetActive(false);
			}
		}
	}

	//---------------------------------------------------------------
	// Starts countdown
	public static void StartCountdown() {
		_countdown._timer = 0f;
		_countdown._container.SetActive(true);
	}
}
