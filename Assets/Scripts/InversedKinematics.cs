using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InversedKinematics : MonoBehaviour {

    public InputField xPositionInpuField;
    public InputField yPositionInpuField;
    public InputField zPositionInpuField;

    public Text IK_Text;

    //Desired rotations for every of the joints
    private float theta1;
    private float theta2;
    private float theta3;

    //Turn off text timer
    bool isTimerOn = false;
    float duration = 3.0f;
    float passedTime = 0.0f;

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        if (isTimerOn)
        {
            passedTime += Time.deltaTime;
            if(passedTime >= duration)
            {
                IK_Text.gameObject.SetActive(false);
                passedTime = 0.0f;
                isTimerOn = false;
            }
        }
	}

    public void CalculateInversedKinematics()
    {
        if (!CheckIfPointInWorkingVolume())
        {
            IK_Text.gameObject.SetActive(true);
            passedTime = 0.0f;
            isTimerOn = true;
        }
        Debug.Log("OK!");
    }

    bool CheckIfPointInWorkingVolume()
    {
        return false;
    }

    void CalculateIK()
    {
        float r; //temporary variable helping calculating thetas. It is a vector between the Cylinder1 and the Grabber on YX plane.

        //Desired position of the Grabber
        float px;
        float py;
        float pz;

        //Reading desired position of the Grabber from input fields in menu
        //px = 
    }
}
