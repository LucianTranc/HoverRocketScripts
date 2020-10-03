using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public PlayerController player;
    public Camera cam;
    // Start is called before the first frame update
    void Start() {
        cam.orthographicSize = DataController.instance.data.cameraZoom;
    }

    // Update is called once per frame
    void LateUpdate() {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);   
    }

    public void cameraZoom (float newZoom) {

        cam.orthographicSize = newZoom;
        DataController.instance.data.cameraZoom = newZoom;
        DataController.instance.SavePlayerData();
    }

}
