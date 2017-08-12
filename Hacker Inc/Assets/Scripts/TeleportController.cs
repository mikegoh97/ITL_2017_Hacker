using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportController : MonoBehaviour {
    [SerializeField] private float maxLineDistance;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject playSpace;

    private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
    public bool triggerButtonDown = false;

    private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
    private SteamVR_TrackedObject trackedObj;

    void Start()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    void Update()
    {
        triggerButtonDown = controller.GetPressDown(triggerButton);

        Ray ray = new Ray(this.transform.position, this.transform.forward);
        RaycastHit hitInfo;

        DrawLine(this.transform.position, this.transform.position + this.transform.forward * maxLineDistance, Color.red, 0.01f);

        print(playSpace.transform.position - player.transform.position);

        if (Physics.Raycast(ray, out hitInfo))
        {
            if (triggerButtonDown && hitInfo.collider.tag == "Floor")
            {
                Vector3 playSpacePosition = playSpace.transform.position;
                Vector3 playerPosition = player.transform.position;
                Vector3 playerOffset = (new Vector3(playSpacePosition.x, 0f, playSpacePosition.z)) - (new Vector3(playerPosition.x, 0f, playerPosition.z));

                StartCoroutine(Teleport(playSpace, hitInfo.point + playerOffset));
            }
        }
    }

    IEnumerator Teleport(GameObject objectToTeleport, Vector3 destination)
    {
        yield return new WaitForSeconds(0.5f);
        objectToTeleport.transform.position = new Vector3(destination.x, objectToTeleport.transform.position.y, destination.z);
    }

    void DrawLine(Vector3 start, Vector3 end, Color color, float duration)
    {
        GameObject myLine = new GameObject();
        myLine.transform.position = start;
        myLine.AddComponent<LineRenderer>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();

        lr.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
        lr.startColor = color;
        lr.endColor = color;

        float width = 0.01f;

        lr.startWidth = width;
        lr.endWidth = width;
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);

        Destroy(myLine, duration);
    }
}
