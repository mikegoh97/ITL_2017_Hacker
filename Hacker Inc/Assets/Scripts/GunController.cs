using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {
    private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
	public bool triggerButtonDown = false;

    private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input ((int)trackedObj.index);  } }
	private SteamVR_TrackedObject trackedObj;

	void Start() {
		trackedObj = GetComponent<SteamVR_TrackedObject> ();
	}

	void Update() {
		triggerButtonDown = controller.GetPressDown (triggerButton);

        Ray ray = new Ray (this.transform.position, -this.transform.up);
		RaycastHit hitInfo;

		if (triggerButtonDown && Physics.Raycast(ray, out hitInfo)) {
            if(hitInfo.collider.tag == "Target")
            {
                Destroy(hitInfo.transform.root.gameObject);
            }
        }
	}
}
