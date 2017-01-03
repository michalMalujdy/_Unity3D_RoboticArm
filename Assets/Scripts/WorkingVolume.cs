using UnityEngine;
using System.Collections;

public class WorkingVolume : MonoBehaviour {

    //new mode
    public Transform cylinderTemplate;
    private Vector3 zAxis = new Vector3(0.0f, 0.0f, 1.0f);
    bool isWorkingVolumeShown = false;

    void Start()
    {        
        Create();
        gameObject.SetActive(false);
    }

    void Update()
    {

    }

    void Create()
    {
        for (int i = 1; i <= 360; i+=1)
        {
            Transform newCylinder = (Transform) Instantiate(cylinderTemplate);
            newCylinder.parent = cylinderTemplate.parent;

            newCylinder.Rotate(zAxis, i);
        }
    }

    public void ToggleShowWorkingVolume()
    {
        isWorkingVolumeShown = !isWorkingVolumeShown;
        gameObject.SetActive(isWorkingVolumeShown);
    }
}
