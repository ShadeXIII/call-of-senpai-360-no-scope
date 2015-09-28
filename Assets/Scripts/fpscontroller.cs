using UnityEngine;
using System.Collections;

public class fpscontroller : MonoBehaviour {

    [SerializeField]
    float sensitivity = 1.0f;

    [SerializeField]
    float miny;

    [SerializeField]
    float maxy;

    private float roty;
    private float rotx;
	
	// Update is called once per frame
	void Update () 
    {
        rotx = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivity;

        roty += Input.GetAxis("Mouse Y") * sensitivity;
        roty = Mathf.Clamp(roty, miny, maxy);

        transform.localEulerAngles = new Vector3(-roty, rotx, 0);
	}
}
