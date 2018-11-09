using UnityEngine;

// Governs movement and physics of the Player object
public class Player : MonoBehaviour {

	private bool _onGround;			// Is the player on the ground?

	private Rigidbody _rb;


#region // Functions
	// Determine whether the player is eligible to jump
	private bool CanJump {
		get{return Game.Active && _onGround && gameObject.transform.localPosition.y >= -0.05f;}
	}
	// From the jump height (1.5f) and gravity we deduce the upwards speed for the character to reach at the apex
	private static float JumpSpeed {
		get{return Mathf.Sqrt(-2f * (1.5f) * Physics.gravity.y);}
	}
	private static float JumpTime {
		get{return 2f * -JumpSpeed / Physics.gravity.y;}
	}
	public static float JumpDistance {
		get{return JumpTime * Game.SpeedMult * FPS.AvgFPS;}
	}
#endregion
	

	// Runs when player is added to scene
	void Start() {
		_rb = gameObject.GetComponent<Rigidbody>();
	}

	// Runs every frame
	void Update () {
		if(CanJump) {
			if(Input.GetKeyDown("space") || Input.touchCount > 0) {
				Jump();
			}
		}
		else if(gameObject.transform.localPosition.y <= -15) {
			Game.LoseGame();
		}
	}

	// Makes the player jump
	private void Jump() {
		_rb.velocity = new Vector2(_rb.velocity.x, JumpSpeed);
		_onGround = false;
	}

	// Case switch the collision event to determine outcome
	void OnCollisionEnter(Collision col) {
		switch(col.gameObject.name) {
			case "Platform":
				_onGround = true;
				if(CanJump) {
					col.transform.parent.gameObject.GetComponent<Platform>().Land();
				}
				break;
		}
	}

	// Case switch the collision event to determine outcome
	void OnCollisionExit(Collision col) {
		switch(col.gameObject.name) {
			case "Platform":
				if(gameObject.transform.localPosition.y <= -0.05f) {
					_onGround = false;
				}
				break;
		}
	}

}
