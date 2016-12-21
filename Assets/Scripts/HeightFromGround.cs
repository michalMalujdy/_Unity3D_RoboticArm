using UnityEngine;
using System.Collections;

public class HeightFromGround : MonoBehaviour {

	// Use this for initialization
	void Start () {
        float height = transform.position.z;
        Debug.Log(gameObject.name + ": " + height);
	}
}
