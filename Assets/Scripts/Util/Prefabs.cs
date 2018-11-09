using UnityEngine;

// Stores the game prefabs loaded on app startup
public static class Prefabs {
	
	// Public references --------------------------------------------
	public static GameObject Player {
		get{return _player;}
	}
	public static GameObject Platform {
		get{return _platform;}
	}
	public static GameObject PLeft {
		get{return _pLeft;}
	}
	public static GameObject PMid {
		get{return _pMid;}
	}
	public static GameObject PRight {
		get{return _pRight;}
	}
	public static Material PLightMat {
		get{return _pLightMat;}
	}
	//---------------------------------------------------------------

	// Gameplay components
	private static GameObject _player;		// The player
	private static GameObject _platform;	// The parent platform prefab
	private static GameObject _pLeft;		// The left component of the platform
	private static GameObject _pMid;		// The middle component of the platform
	private static GameObject _pRight;		// The right component of the platform

	// UI components
	private static Material _pLightMat;		// A light material for testing to show platform is moving
	//--
	//--

	public static void LoadPrefabs() {
		_player 	= Resources.Load<GameObject>("Prefabs/Player");
		_platform 	= Resources.Load<GameObject>("Prefabs/Platform");
		_pLeft 		= Resources.Load<GameObject>("Components/PLeft");
		_pMid 		= Resources.Load<GameObject>("Components/PMid");
		_pRight		= Resources.Load<GameObject>("Components/PRight");
		
		_pLightMat 	= Resources.Load<Material>("Materials/PLight");
	}

}
