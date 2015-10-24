using UnityEngine;
using System.Collections;

public class Func_Door : MonoBehaviour
{
    #region SerializedFields
    [SerializeField]
    bool m_bOpenPosYAxis;

    [SerializeField]
    bool m_bOpenNegYAxis;

    [SerializeField]
    bool m_bOpenPosXAxis;

    [SerializeField]
    bool m_bOpenNegXAxis;

    [SerializeField]
    bool m_bOpenPosZAxis;

    [SerializeField]
    bool m_bOpenNegZAxis;

    [SerializeField]
    bool m_bStayOpen;

    [SerializeField]
    float m_fSpeed;

    [SerializeField]
    float m_fLip;
    
    [SerializeField]
    float m_fTimeOpen;

    [SerializeField]
    AudioClip m_tStart;

    [SerializeField]
    AudioClip m_tMoving;

    [SerializeField]
    AudioClip m_tClose;
    #endregion

    private Vector3 m_tExtents;
    private Vector3 m_tOriginalPosition;
    private float m_fOpenTimer;
    private float m_fPosYAxisExtent;
    private float m_fNegYAxisExtent;
    private float m_fPosZAxisExtent;
    private float m_fNegZAxisExtent;
    private float m_fPosXAxisExtent;
    private float m_fNegXAxisExtent;
    private int m_iDoorState;
    private enum DoorState {Closed, Open, MovingToOpen, MovingToClose};
    private bool m_bInCycle;
    //used to keep the door from activating again if stay open is true
    //not exactly necessary if people use trigger_use properly but this is a fail safe
    private bool m_bUsedOnce;


    // Use this for initialization
    void Start()
    {
        m_bInCycle = false;
        m_bUsedOnce = false;
        m_iDoorState = (int)DoorState.Closed;
        m_fOpenTimer = 0;
        if (GetComponent<Renderer>() != null)
        {
            m_tExtents = GetComponent<Renderer>().bounds.extents;
            m_tOriginalPosition = GetComponent<Rigidbody>().transform.position;

            m_fPosYAxisExtent = GetComponent<Rigidbody>().transform.position.y + (m_tExtents.y * 2) - m_fLip;
            m_fNegYAxisExtent = GetComponent<Rigidbody>().transform.position.y + (-m_tExtents.y * 2) + m_fLip; 

            m_fPosZAxisExtent = GetComponent<Rigidbody>().transform.position.z + (m_tExtents.z * 2) - m_fLip;
            m_fNegZAxisExtent = GetComponent<Rigidbody>().transform.position.z + (-m_tExtents.z * 2) + m_fLip;

            m_fPosXAxisExtent = GetComponent<Rigidbody>().transform.position.x + (m_tExtents.x * 2) - m_fLip;
            m_fNegXAxisExtent = GetComponent<Rigidbody>().transform.position.x + (-m_tExtents.x * 2) + m_fLip;
        }
    }

    void Update()
    {
        if (m_bInCycle)
        {
            if(m_bOpenPosYAxis)
                PositiveYAxis();

            if(m_bOpenNegYAxis)
                NegativeYAxis();

            if (m_bOpenPosZAxis)
                PositiveZAxis();

            if (m_bOpenNegZAxis)
                NegativeZAxis();

            if (m_bOpenPosXAxis)
                PositiveXAxis();

            if (m_bOpenNegXAxis)
                NegativeXAxis();

            if (m_iDoorState == (int)DoorState.Open)
            {
                m_fOpenTimer += Time.deltaTime;
            }
        }
    }

    void PositiveYAxis()
    {
        switch (m_iDoorState)
        {
            case (int)DoorState.Open:
                {
                    //play close clip
                    PlayClose();
                    if (m_fOpenTimer >= m_fTimeOpen)
                    {
                        m_fOpenTimer = 0;
                        m_iDoorState = (int)DoorState.MovingToClose;
                    }
                    break;
                }
            case (int)DoorState.MovingToOpen:
                {
                    //play moving clip
                    PlayMoving();
                    GetComponent<Rigidbody>().transform.Translate(GetComponent<Rigidbody>().transform.up.normalized * m_fSpeed * Time.deltaTime);
                    if (GetComponent<Rigidbody>().transform.position.y >= m_fPosYAxisExtent)
                    {
                        if (m_bStayOpen)
                        {
                            m_bInCycle = false;
                            m_bUsedOnce = true;
                            return;
                        }
                        m_iDoorState = (int)DoorState.Open;
                    }
                    break;
                }
            case (int)DoorState.MovingToClose:
                {
                    //play moving clip
                    PlayMoving();
                    GetComponent<Rigidbody>().transform.Translate(-(GetComponent<Rigidbody>().transform.up.normalized) * m_fSpeed * Time.deltaTime);
                    if (GetComponent<Rigidbody>().transform.position.y <= m_tOriginalPosition.y)
                    {
                        m_iDoorState = (int)DoorState.Closed;
                        GetComponent<Rigidbody>().transform.position = m_tOriginalPosition;
                    }
                    break;
                }
            case (int)DoorState.Closed:
                {
                    //play close clip
                    PlayClose();
                    m_bInCycle = false;
                    break;
                }
            default:
                {
                    Debug.Log("Something went horribly wrong with this door!!!");
                    m_iDoorState = (int)DoorState.Closed;
                    break;
                }
        }
    }

    void NegativeYAxis()
    {
        switch (m_iDoorState)
        {
            case (int)DoorState.Open:
                {
                    //play close clip
                    PlayClose();
                    if (m_fOpenTimer >= m_fTimeOpen)
                    {
                        m_fOpenTimer = 0;
                        m_iDoorState = (int)DoorState.MovingToClose;
                    }
                    break;
                }
            case (int)DoorState.MovingToOpen:
                {
                    //play moving clip
                    PlayMoving();
                    GetComponent<Rigidbody>().transform.Translate(-(GetComponent<Rigidbody>().transform.up.normalized) * m_fSpeed * Time.deltaTime);
                    if (GetComponent<Rigidbody>().transform.position.y <= m_fNegYAxisExtent)
                    {
                        if (m_bStayOpen)
                        {
                            m_bInCycle = false;
                            m_bUsedOnce = true;
                            return;
                        }
                        m_iDoorState = (int)DoorState.Open;
                    }
                    break;
                }
            case (int)DoorState.MovingToClose:
                {
                    //play moving clip
                    PlayMoving();
                    GetComponent<Rigidbody>().transform.Translate(GetComponent<Rigidbody>().transform.up.normalized * m_fSpeed * Time.deltaTime);
                    if (GetComponent<Rigidbody>().transform.position.y >= m_tOriginalPosition.y)
                    {
                        m_iDoorState = (int)DoorState.Closed;
                        GetComponent<Rigidbody>().transform.position = m_tOriginalPosition;
                    }
                    break;
                }
            case (int)DoorState.Closed:
                {
                    //play close clip
                    PlayClose();
                    m_bInCycle = false;
                    break;
                }
            default:
                {
                    Debug.Log("Something went horribly wrong with this door!!!");
                    m_iDoorState = (int)DoorState.Closed;
                    break;
                }
        }
    }

    void PositiveZAxis()
    {
        switch (m_iDoorState)
        {
            case (int)DoorState.Open:
                {
                    //play close clip
                    PlayClose();
                    if (m_fOpenTimer >= m_fTimeOpen)
                    {
                        m_fOpenTimer = 0;
                        m_iDoorState = (int)DoorState.MovingToClose;
                    }
                    break;
                }
            case (int)DoorState.MovingToOpen:
                {
                    //play moving clip
                    PlayMoving();
                    GetComponent<Rigidbody>().transform.Translate(GetComponent<Rigidbody>().transform.forward.normalized * m_fSpeed * Time.deltaTime);
                    if (GetComponent<Rigidbody>().transform.position.z >= m_fPosZAxisExtent)
                    {
                        if (m_bStayOpen)
                        {
                            m_bInCycle = false;
                            m_bUsedOnce = true;
                            return;
                        }
                        m_iDoorState = (int)DoorState.Open;
                    }
                    break;
                }
            case (int)DoorState.MovingToClose:
                {
                    //play moving clip
                    PlayMoving();
                    GetComponent<Rigidbody>().transform.Translate(-(GetComponent<Rigidbody>().transform.forward.normalized) * m_fSpeed * Time.deltaTime);
                    if (GetComponent<Rigidbody>().transform.position.z <= m_tOriginalPosition.z)
                    {
                        m_iDoorState = (int)DoorState.Closed;
                        GetComponent<Rigidbody>().transform.position = m_tOriginalPosition;
                    }
                    break;
                }
            case (int)DoorState.Closed:
                {
                    //play close clip
                    PlayClose();
                    m_bInCycle = false;
                    break;
                }
            default:
                {
                    Debug.Log("Something went horribly wrong with this door!!!");
                    m_iDoorState = (int)DoorState.Closed;
                    break;
                }
        }
    }

    void NegativeZAxis()
    {
        switch (m_iDoorState)
        {
            case (int)DoorState.Open:
                {
                    //play close clip
                    PlayClose();
                    if (m_fOpenTimer >= m_fTimeOpen)
                    {
                        m_fOpenTimer = 0;
                        m_iDoorState = (int)DoorState.MovingToClose;
                    }
                    break;
                }
            case (int)DoorState.MovingToOpen:
                {
                    //play moving clip
                    PlayMoving();
                    GetComponent<Rigidbody>().transform.Translate(-(GetComponent<Rigidbody>().transform.forward.normalized) * m_fSpeed * Time.deltaTime);
                    if (GetComponent<Rigidbody>().transform.position.z <= m_fNegZAxisExtent)
                    {
                        if (m_bStayOpen)
                        {
                            m_bInCycle = false;
                            m_bUsedOnce = true;
                            return;
                        }
                        m_iDoorState = (int)DoorState.Open;
                    }
                    break;
                }
            case (int)DoorState.MovingToClose:
                {
                    //play moving clip
                    PlayMoving();
                    GetComponent<Rigidbody>().transform.Translate(GetComponent<Rigidbody>().transform.forward.normalized * m_fSpeed * Time.deltaTime);
                    if (GetComponent<Rigidbody>().transform.position.z >= m_tOriginalPosition.z)
                    {
                        m_iDoorState = (int)DoorState.Closed;
                        GetComponent<Rigidbody>().transform.position = m_tOriginalPosition;
                    }
                    break;
                }
            case (int)DoorState.Closed:
                {
                    //play close clip
                    PlayClose();
                    m_bInCycle = false;
                    break;
                }
            default:
                {
                    Debug.Log("Something went horribly wrong with this door!!!");
                    m_iDoorState = (int)DoorState.Closed;
                    break;
                }
        }
    }

    void PositiveXAxis()
    {
        switch (m_iDoorState)
        {
            case (int)DoorState.Open:
                {
                    //play close clip
                    PlayClose();
                    if (m_fOpenTimer >= m_fTimeOpen)
                    {
                        m_fOpenTimer = 0;
                        m_iDoorState = (int)DoorState.MovingToClose;
                    }
                    break;
                }
            case (int)DoorState.MovingToOpen:
                {
                    //play moving clip
                    PlayMoving();
                    GetComponent<Rigidbody>().transform.Translate(GetComponent<Rigidbody>().transform.right.normalized * m_fSpeed * Time.deltaTime);
                    if (GetComponent<Rigidbody>().transform.position.x >= m_fPosXAxisExtent)
                    {
                        if (m_bStayOpen)
                        {
                            m_bInCycle = false;
                            m_bUsedOnce = true;
                            return;
                        }
                        m_iDoorState = (int)DoorState.Open;
                    }
                    break;
                }
            case (int)DoorState.MovingToClose:
                {
                    //play moving clip
                    PlayMoving();
                    GetComponent<Rigidbody>().transform.Translate(-(GetComponent<Rigidbody>().transform.right.normalized) * m_fSpeed * Time.deltaTime);
                    if (GetComponent<Rigidbody>().transform.position.x <= m_tOriginalPosition.x)
                    {
                        m_iDoorState = (int)DoorState.Closed;
                        GetComponent<Rigidbody>().transform.position = m_tOriginalPosition;
                    }
                    break;
                }
            case (int)DoorState.Closed:
                {
                    //play close clip
                    PlayClose();
                    m_bInCycle = false;
                    break;
                }
            default:
                {
                    Debug.Log("Something went horribly wrong with this door!!!");
                    m_iDoorState = (int)DoorState.Closed;
                    break;
                }
        }
    }

    void NegativeXAxis()
    {
        switch (m_iDoorState)
        {
            case (int)DoorState.Open:
                {
                    //play close clip
                    PlayClose();
                    if (m_fOpenTimer >= m_fTimeOpen)
                    {
                        m_fOpenTimer = 0;
                        m_iDoorState = (int)DoorState.MovingToClose;
                    }
                    break;
                }
            case (int)DoorState.MovingToOpen:
                {
                    //play moving clip
                    PlayMoving();
                    GetComponent<Rigidbody>().transform.Translate(-(GetComponent<Rigidbody>().transform.right.normalized) * m_fSpeed * Time.deltaTime);
                    if (GetComponent<Rigidbody>().transform.position.x <= m_fNegXAxisExtent)
                    {
                        if (m_bStayOpen)
                        {
                            m_bInCycle = false;
                            m_bUsedOnce = true;
                            return;
                        }
                        m_iDoorState = (int)DoorState.Open;
                    }
                    break;
                }
            case (int)DoorState.MovingToClose:
                {
                    //play moving clip
                    PlayMoving();
                    GetComponent<Rigidbody>().transform.Translate(GetComponent<Rigidbody>().transform.right.normalized * m_fSpeed * Time.deltaTime);
                    if (GetComponent<Rigidbody>().transform.position.x >= m_tOriginalPosition.x)
                    {
                        m_iDoorState = (int)DoorState.Closed;
                        GetComponent<Rigidbody>().transform.position = m_tOriginalPosition;
                    }
                    break;
                }
            case (int)DoorState.Closed:
                {
                    //play close clip
                    PlayClose();
                    m_bInCycle = false;
                    break;
                }
            default:
                {
                    Debug.Log("Something went horribly wrong with this door!!!");
                    m_iDoorState = (int)DoorState.Closed;
                    break;
                }
        }
    }

    void PlayStart()
    {
        if (m_tStart != null)
            AudioSource.PlayClipAtPoint(m_tStart, GetComponent<Rigidbody>().transform.position);
    }

    void PlayClose()
    {
        if (m_tClose != null)
            AudioSource.PlayClipAtPoint(m_tClose, GetComponent<Rigidbody>().transform.position);
    }

    void PlayMoving()
    {
        if (m_tMoving != null)
            AudioSource.PlayClipAtPoint(m_tMoving, GetComponent<Rigidbody>().transform.position);
    }

    void Use()
    {
        if (m_bUsedOnce == false)
        {
            m_bInCycle = true;
            m_iDoorState = (int)DoorState.MovingToOpen;
            PlayStart();
        }
    }

   
}
