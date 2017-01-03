using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShowGrabberPosition : MonoBehaviour {

    public InputField xResult;
    public InputField yResult;
    public InputField zResult;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ShowPosition()
    {
        float roundedX = Mathf.Floor(transform.position.x * 1000.0f) / 1000.0f;
        float roundedY = Mathf.Floor(transform.position.y * 1000.0f) / 1000.0f;
        float roundedZ = Mathf.Floor(transform.position.z * 1000.0f) / 1000.0f;

        xResult.text = roundedX.ToString();
        yResult.text = roundedY.ToString();
        zResult.text = roundedZ.ToString();
    }
}
