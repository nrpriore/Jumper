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

		// Set transform variables. Move right edge of platform to x = 0 (relative to parent) for easier spawning
		// Right --
		Transform pRight = Instantiate(Prefabs.PRight, pt).GetComponent<Transform>();
		float pRightX = -pRight.GetComponent<MeshRenderer>().bounds.size.x / 2f;
		pRight.localPosition += Vector3.right * pRightX;
		// Middle slices --
		Transform pMid;
		float pMidWidth = Prefabs.PMid.GetComponent<MeshRenderer>().bounds.size.x;
		float pMidX = (2f * pRightX) + (pMidWidth / 2f);
		for(int i = 0; i < size; i++) {
			pMid = Instantiate(Prefabs.PMid , pt).GetComponent<Transform>();
			pMidX -= pMidWidth;
			pMid.localPosition += Vector3.right * pMidX;
			pMid.name = "Platform";

			if(i % 2 == 0) {
				pMid.GetComponent<MeshRenderer>().material = Prefabs.PLightMat;
			}
		}
		// Left --
		Transform pLeft = Instantiate(Prefabs.PLeft, pt).GetComponent<Transform>();
		float pLeftX = pMidX - (pMidWidth / 2f) + pRightX;
		pLeft.localPosition += Vector3.right * pLeftX;

		p._pWidth = - pLeftX - pRightX;

		float spawnX = (typeID == 1)? Config.PLATFORM_STARTX : _newestPlatform.transform.localPosition.x + p._pWidth + Game.GapSize;
		pt.localPosition = new Vector3(spawnX, -1f, 0);

		// Set names for collision check
		pLeft.name = pRight.name = "Platform";
		p._landed = (typeID == 1)? true : false;
		_newestPlatform = p;
	}

	public void Land() {
		if(!_landed) {
			_landed = true;
		}
	}
}
