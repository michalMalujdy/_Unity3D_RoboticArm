using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RotateBySlider : MonoBehaviour
{
    public Slider slider;
    public RotationAxis rotationAxis;

    public enum RotationAxis
    {
        X = 1,
        Y = 2,
        Z = 3
    }

    // Use this for initialization
    void Start()
    {
        slider.onValueChanged.AddListener(delegate { Rotate(); });
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Rotate()
    {
        if (rotationAxis == RotationAxis.X)
        {
            transform.localRotation = Quaternion.Euler(slider.value, 0.0f, 0.0f);
        }
        if (rotationAxis == RotationAxis.Y)
        {
            transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x, slider.value, transform.localRotation.eulerAngles.z);
        }
        if (rotationAxis == RotationAxis.Z)
        {
            transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x, transform.localRotation.eulerAngles.y, slider.value);
        }
    }
}