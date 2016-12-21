using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InversedKinematics : MonoBehaviour {

    public InputField xPositionInpuField;
    public InputField yPositionInpuField;
    public InputField zPositionInpuField;

    public Text IK_Text;
    public Transform Grabber;

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

        CalculateIK();
        Debug.Log("T1=" + RadiansToDegrees(theta1) + ", T2=" + RadiansToDegrees(theta2));
    }

    bool CheckIfPointInWorkingVolume()
    {
        return true;
    }

    void CalculateIK()
    {
        float r; //temporary variable helping calculating thetas. It is a length of a vector between the Cylinder1 and the Grabber on the YX plane.

        //Desired position of the Grabber
        float px;
        float py;
        float pz;

        //Reading desired position of the Grabber from input fields in menu
        px = Grabber.position.x;
        py = Grabber.position.y;
        pz = Grabber.position.z;

        float l1 = 1.95f;
        float l2 = 1.65f;
        float l3 = 0.8f;
        float l4 = 0.95f + 0.348f;

        r = Mathf.Sqrt(Mathf.Pow(px, 2.0f) + Mathf.Pow(py, 2.0f));

        //Calculating theta2
        float sinTheta2 = Mathf.Sqrt(Mathf.Pow(r,2.0f) - Mathf.Pow(l2 + l4, 2.0f)) / l3;
        theta2 = Mathf.Atan2(sinTheta2, Mathf.Sqrt(1 - Mathf.Pow(sinTheta2, 2.0f)));
        //theta2 = Mathf.Asin(sinTheta2);

        //Calculating theta1
        float beta = Mathf.Atan2(py, px);
        float alpha = Mathf.Atan2(l3 * sinTheta2, r);
        theta1 = beta - alpha;
    }

    float RadiansToDegrees(float angle)
    {
        return angle * 180.0f / Mathf.PI;
    }
}
