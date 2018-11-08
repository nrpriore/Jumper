using UnityEngine;

// Governs the creation and movement of platforms
public class Platform : MonoBehaviour {

	private static Platform _newestPlatform;
	private bool _landed;
	private float _pWidth;
	private int _typeID; // 0 normal, 1 first platform, 2 final platform

	//---------------------------------------------------------------
	// Runs on platform creation
	void Start() {

	}

	// Runs every frame
	void Update () {
		if(Game.Active) {
			transform.localPosition -= Vector3.right * Game.SpeedMult;

			if(_newestPlatform == this && transform.localPosition.x <= Config.PLATFORM_STARTX - Game.GapSize) {
				if(Game.TriggerFinalPlatform) {
					Platform.Create(1000);
				}
				else {
					int size = (int)(Random.value * (1 - Game.TimeRatio) * 8f) + (int)(Random.value * 8f);
					Platform.Create(size);
				}
			}
		}
		if(gameObject.transform.localPosition.x <= -2f * Config.PLATFORM_STARTX) {
			Destroy(gameObject);
		}
	}

	// Sets the transform properties of the new platform
	public static void Create(int size, int typeID = 0) {
		// Initialize the parent platform and child components
		Platform p = Instantiate<GameObject>(Prefabs.Platform).GetComponent<Platform>();
		Transform pt = p.transform;
		p._typeID = typeID;

		Transform pLeft = Instantiate(Prefabs.PLeft, pt).GetComponent<Transform>();
		Transform pMid = Instantiate(Prefabs.PMid , pt).GetComponent<Transform>();
		Transform pRight = Instantiate(Prefabs.PRight, pt).GetComponent<Transform>();

		// Set transform variables. Move right edge of platform to x = 0 (relative to parent) for easier spawning
		pMid.localScale = new Vector3(pMid.localScale.x * size, pMid.localScale.y, pMid.localScale.z);
		float pRightX = -pRight.GetComponent<MeshRenderer>().bounds.size.x / 2f;
		float pMidX = (2f * pRightX) - (pMid.GetComponent<MeshRenderer>().bounds.size.x / 2f);
		float pLeftX = (2f * pMidX) - pRightX;
		p._pWidth = - pMidX * 2f;

		pLeft.localPosition += Vector3.right * pLeftX;
		pMid.localPosition += Vector3.right * pMidX;
		pRight.localPosition += Vector3.right * pRightX;

		float spawnX = (typeID == 1)? Config.PLATFORM_STARTX : _newestPlatform.transform.localPosition.x + p._pWidth + Game.GapSize;
		pt.localPosition = new Vector3(spawnX, -1f, 0);

		// Set names for collision check
		pLeft.name = pMid.name = pRight.name = "Platform";

		p._landed = (typeID == 1)? true : false;
		_newestPlatform = p;
	}

	public void Land() {
		if(!_landed) {
			_landed = true;
		}
	}
}
