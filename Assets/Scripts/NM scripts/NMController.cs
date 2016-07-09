using UnityEngine;
using System.Collections;

public class NMController : MonoBehaviour {
    //Camera Mouse Movement==============================
    [SerializeField]
    float m_fSensitivity = 1.0f;

    [SerializeField]
    float m_fMiny;

    [SerializeField]
    float m_fMaxy;

    [SerializeField]
    Camera m_cCamera;

    private float roty;
    private float rotx;
    //===================================================
    //Standard Movement =================================
    [SerializeField]
    float m_fAcceleration;

    [SerializeField]
    float m_fMaxSpeed;

    private Vector3 m_vecCurrMoveDir;
    //===================================================

    

	// Use this for initialization
	void Start () 
    {
	
	}

    void Update()
    {
        // We can put the rotation in the normal update, it shouldnt have any physics impact
        HandleRotation();

    }

    private void HandleRotation()
    {

        // Calculate vertical rotation
        roty += Input.GetAxis("Mouse Y") * m_fSensitivity;
        roty = Mathf.Clamp(roty, m_fMiny, m_fMaxy);

        // Apply vertial rotation
        m_cCamera.transform.localRotation = Quaternion.Euler(-roty, 0, 0);

        // Calculate horizontal rotation
        rotx += Input.GetAxis("Mouse X") * m_fSensitivity;

        // Apply horizontal rotation
        transform.localRotation = Quaternion.Euler(0, rotx, 0);
    }
}
