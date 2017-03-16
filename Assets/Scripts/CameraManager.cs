using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

    [SerializeField] GameObject player;
    private Vector3 lastPlayerPosition, temp;

	// Use this for initialization
	void Start () {
        lastPlayerPosition = player.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        temp = player.transform.position;
        gameObject.transform.Translate((temp - lastPlayerPosition), Space.World);
        lastPlayerPosition = temp;
	}
}
