using UnityEngine;
using System.Collections;

public class fpscontroller : MonoBehaviour {

    [SerializeField]
    float sensitivity = 1.0f;

    [SerializeField]
    float miny;

    [SerializeField]
    float maxy;

	Camera m_playerCamera;

    private float roty;
    private float rotx;

	void Start() {
		m_playerCamera = gameObject.GetComponentInChildren<Camera> ();
	}

	// Update is called once per frame
	void Update ()
    {
		// We can put the rotation in the normal update, it shouldnt have any physics impact
		HandleRotation ();

	}

	private void HandleRotation() {

		// Calculate vertical rotation
		roty += Input.GetAxis("Mouse Y") * sensitivity;
		roty = Mathf.Clamp(roty, miny, maxy);

		// Apply vertial rotation
		m_playerCamera.transform.localRotation = Quaternion.Euler(-roty, 0, 0);

		// Calculate horizontal rotation
		rotx += Input.GetAxis("Mouse X") * sensitivity;

		// Apply horizontal rotation
		transform.localRotation = Quaternion.Euler(0, rotx, 0);
	}
}
