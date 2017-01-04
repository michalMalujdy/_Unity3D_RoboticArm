using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InversedKinematics : MonoBehaviour {

    public InputField xPositionInpuField;
    public InputField yPositionInpuField;
    public InputField zPositionInpuField;

    //Boundary of a working volume. Necessary for checking whether indicated point is inside the working volume
    private float circleRadius;
    private float margin = 0.001f;
    private float zMin = 1.15f;
    private float zMax = 2.75f;

    public Text IK_Text;
    public Transform Grabber;

    //Desired rotations for every of the joints
    private float theta1;
    private float theta2;
    private float theta3;

    //Position from input fields
    float x;
    float y;
    float z;

    //Real grabber position for tests
    Vector3 grabberPosition;

    public Transform cylinder1;
    public Transform cylinder2;
    public Transform cylinder3;

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

            theta1 = 0.0f;
            theta2 = 0.0f;
        }
        else
        {
            //Debug.ClearDeveloperConsole();
            grabberPosition = Grabber.position;
            Debug.Log("Grabber position before IK:" + grabberPosition);

            CalculateIK();
            SetJointsRotations();

            grabberPosition = Grabber.position;
            Debug.Log("Grabber position after IK:" + grabberPosition);
            Debug.Log("T1=" + RadiansToDegrees(theta1) + ", T2=" + RadiansToDegrees(theta2));
        }       
    }

    bool CheckIfPointInWorkingVolume()
    {
        bool xResult = float.TryParse(xPositionInpuField.text, out x);
        bool yResult = float.TryParse(yPositionInpuField.text, out y);
        bool zResult = float.TryParse(zPositionInpuField.text, out z);

        if(!(xResult && yResult && zResult))
        {
            return false;
        }

        x = Mathf.Floor(x * 10000.0f) / 10000.0f;
        y = Mathf.Floor(y * 10000.0f) / 10000.0f;

        circleRadius = Mathf.Sqrt(Mathf.Pow(x, 2.0f) + Mathf.Pow(y, 2.0f));

        float leftSideCircleEquation = Mathf.Pow(x, 2.0f) + Mathf.Pow(y, 2.0f);
        float rightSideCircleEquation = Mathf.Pow(circleRadius, 2.0f);

        if (leftSideCircleEquation >= rightSideCircleEquation - margin && leftSideCircleEquation <= rightSideCircleEquation + margin)
        {
            if (z >= zMin && z <= zMax)
            {
                return true;
            }
        }
        return false;
    }

    void CalculateIK()
    {
        float r; //temporary variable helping calculating thetas. It is a length of a vector between the Cylinder1 and the Grabber on the YX plane.

        //Desired position of the Grabber
        float px;
        float py;
        float pz;

        //Reading desired position of the Grabber from input fields in menu
        px = x;
        py = y;
        pz = z;

        float l1 = 1.95f;
        float l2 = 1.65f;
        float l3 = 0.8f;
        float l4 = 0.95f + 0.348f;

        r = Mathf.Sqrt(Mathf.Pow(px, 2.0f) + Mathf.Pow(py, 2.0f));

        //Start calculations of theta2
        float sinTheta2;
        float temp = Mathf.Pow(r, 2.0f) - Mathf.Pow(l2 + l4, 2.0f);

        if (temp > 0) sinTheta2 = Mathf.Sqrt(temp) / l3;
        else sinTheta2 = 0.0f;

        //sign of a sinTheta2. Because sinus is positive only in I and II quarter there is a need to check in which quarter the grabber is desired to be. It is multiplied by square root of 1-(sinTheta2)^2 calculating theta2.
        int sign;

        if (pz < l1) sign = -1;
        else sign = 1;

        theta2 = Mathf.Atan2(sinTheta2, sign * Mathf.Sqrt(1 - Mathf.Pow(sinTheta2, 2.0f)));

        //Calculating theta1
        float beta = Mathf.Atan2(py, px);
        float alpha = Mathf.Atan2(l3 * sinTheta2, r);
        theta1 = beta - alpha;
    }

    void SetJointsRotations()
    {
        cylinder1.localEulerAngles = new Vector3(0.0f, RadiansToDegrees(theta1), 0.0f);
        cylinder2.localEulerAngles = new Vector3(0.0f, RadiansToDegrees(theta2), 0.0f);
    }

    float RadiansToDegrees(float angle)
    {
        return angle * 180.0f / Mathf.PI;
    }

    void CreateAnimation()
    {
        
    }
}
