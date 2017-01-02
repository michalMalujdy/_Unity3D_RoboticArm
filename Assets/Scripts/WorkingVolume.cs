using UnityEngine;
using System.Collections;

public class WorkingVolume : MonoBehaviour {

    public float ThetaScale = 0.01f;
    private float radiusMax = 1.65f + 0.95f + 0.348f;
    private int Size;
    private LineRenderer LineDrawer;
    private float Theta = 0f;
    private float z = 1.95f;



    //new mode
    public Transform cylinderTemplate;
    private Vector3 zAxis = new Vector3(0.0f, 0.0f, 1.0f);
    bool isWorkingVolumeShown = false;

    void Start()
    {
        LineDrawer = GetComponent<LineRenderer>();

        Create();

        gameObject.SetActive(false);
    }

    void Update()
    {

    }

    void DrawCircle()
    {
        Theta = 0f;
        Size = (int)((1f / ThetaScale) + 1f);
        LineDrawer.SetVertexCount(Size);
        for (int i = 0; i < Size; i++)
        {
            Theta += (2.0f * Mathf.PI * ThetaScale);
            float x = radiusMax * Mathf.Cos(Theta);
            float y = radiusMax * Mathf.Sin(Theta);
            LineDrawer.SetPosition(i, new Vector3(x, y, z));
        }
    }

    void Create()
    {
        for (int i = 1; i <= 360; i+=2)
        {
            /*GameObject newCylinder = new GameObject();
            newCylinder = cylinderTemplate.gameObject;
            newCylinder.transform.parent = cylinderTemplate.parent;*/

            Transform newCylinder = (Transform) Instantiate(cylinderTemplate);
            newCylinder.parent = cylinderTemplate.parent;

            newCylinder.Rotate(zAxis, i);

            Debug.Log("a");
        }
    }

    public void ToggleShowWorkingVolume()
    {
        isWorkingVolumeShown = !isWorkingVolumeShown;
        gameObject.SetActive(isWorkingVolumeShown);
    }
}
