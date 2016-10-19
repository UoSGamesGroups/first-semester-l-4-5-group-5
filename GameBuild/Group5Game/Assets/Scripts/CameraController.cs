using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    GameObject player;
    PlayerController pc;

	// Use this for initialization
	void Start ()
    {
        player = GameObject.Find("player");
        pc = player.GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        CameraControl(pc.controlObject);
	}

    void CameraControl(GameObject obj)
    {
        transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y, -10);
    }
}
