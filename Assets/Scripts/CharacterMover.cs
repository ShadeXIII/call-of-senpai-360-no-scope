using UnityEngine;
using System.Collections;


public class CharacterMover : MonoBehaviour
{
    //public ==============================
    public float m_fGravity = -180.3f;//-9.8f;
    public float m_fSpeed;
    public float m_fJmpstr;
    public Vector3 m_tDirMod;
    //======================================
    //private===============================
    private CharacterController m_OPlayerController;
    private Camera m_OPlayerCamera;
    private Vector3 m_tMovedir;
    private float m_fNormSpeed = 0.0f;
    //=======================================

    // Use this for initialization
    void Start()
    {
        m_OPlayerController = GetComponent<CharacterController>();
        m_OPlayerCamera = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        //get input
        PlayerInput();
        //GetComponent<MeshRenderer>().transform.rotation.SetLookRotation(m_OPlayerCamera.transform.forward, new Vector3(0, 1, 0));
    }

    private void PlayerInput()
    {
        if (Input.GetKey(KeyCode.Space) && m_OPlayerController.isGrounded)
        {
            Jump();
        }
        if (Input.GetKey(KeyCode.D))
        {
            //move right	
            MoveRight();
        }
        if (Input.GetKey(KeyCode.A))
        {
            //move left	
            MoveLeft();
        }
        if (Input.GetKey(KeyCode.W))
        {
            MoveForward();
        }
        if (Input.GetKey(KeyCode.S))
        {

        }


        if (Input.GetKey(KeyCode.Mouse0))
        {
            Debug.Log("Fired");
        }


    }

    private void MoveRight()
    {
        
    }

    private void MoveLeft()
    {
       
    }

    private void MoveForward()
    {
        transform.Translate(m_OPlayerCamera.transform.forward.normalized * m_fSpeed * Time.deltaTime);
    }

    private void MoveBackward()
    {
       
    }

    private void Jump()
    {
       
    }

  
}

