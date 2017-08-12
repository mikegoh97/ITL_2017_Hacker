using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour {

	private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
	private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
	private SteamVR_TrackedObject trackedObj;


	// Use this for initialization
	void Start()
	{
		trackedObj = GetComponent<SteamVR_TrackedObject>();
		VRSettings.showDeviceView = false;
	}

	void Update(){
		if (Input.GetKey(KeyCode.Space) || controller.GetPress (triggerButton)) {
			SceneManager.LoadScene ("ComputerScreen");
		}
	}
}
