using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;

public class DisableMirror : MonoBehaviour {

	// Use this for initialization
	void Start () {
        VRSettings.showDeviceView = false;
	}
}
