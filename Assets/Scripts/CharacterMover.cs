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
        m_tDirMod.x = 0.0f;
        m_tDirMod.y = 0.0f;
        m_tDirMod.z = 0.0f;
        m_OPlayerController = GetComponent<CharacterController>();
        m_OPlayerCamera = GetComponent<Camera>();
        m_tMovedir.y = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //get input
        PlayerInput();
        //apply gravity
        m_tMovedir.y = m_tMovedir.y + (m_fGravity * Time.deltaTime);
        //apply movement
        m_OPlayerController.Move(m_tMovedir * Time.deltaTime);

        m_tMovedir.x = 0.0f;
        //Debug.Log("update.");
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
        m_tMovedir = new Vector3(0, m_tMovedir.y, 0);
        m_tMovedir = transform.TransformDirection(m_tMovedir);
        if (m_OPlayerController.isGrounded)
            m_tMovedir.x = m_fSpeed;
        else
            m_tMovedir.x = m_fSpeed * 0.3f;
    }

    private void MoveLeft()
    {
        m_tMovedir = new Vector3(0, m_tMovedir.y, 0);
        m_tMovedir = transform.TransformDirection(m_tMovedir);
        if (m_OPlayerController.isGrounded)
            m_tMovedir.x = -m_fSpeed;
        else
            m_tMovedir.x = -m_fSpeed * 0.3f;
    }

    private void MoveForward()
    {
        m_tMovedir = transform.TransformDirection(m_OPlayerCamera.transform.forward);
        m_tMovedir.x =+ m_fSpeed;
    }

    private void MoveBackward()
    {
        m_tMovedir = transform.TransformDirection(m_OPlayerCamera.transform.forward);
        m_tMovedir.x =- m_fSpeed;
    }

    private void Jump()
    {
        if (m_OPlayerController.isGrounded)
        {
            m_tMovedir = new Vector3(m_tMovedir.x, 0, 0);
            m_tMovedir = transform.TransformDirection(m_tMovedir);
            m_tMovedir.y = m_fJmpstr;
        }
    }

  
}

