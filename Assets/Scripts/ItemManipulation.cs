using UnityEngine;
using System.Collections;

public class ItemManipulation : MonoBehaviour {

    private bool m_bHolding;
    private bool m_bHasTarget;
    private RaycastHit m_rTarget;
    private Rigidbody m_tObject;
    private float m_fThrowDelayTime;
    private float m_fDelayTimer;
    private bool m_bStartDelay;
    private Vector3 m_vHoldPoint;
    private Vector3 m_vObjectPosition;
    private Vector3 m_vTranslationVector;
    private Vector3 m_vNonNormalTranslationVector;

    public Camera m_cCamera;
    public GameObject m_oHoldPosition;
    public LayerMask m_mLayerMask;
    public float m_fThrowForce;
    public float m_fPickupRange;
    public float m_fBreakingRange;
    public AudioClip m_aBreak;
    public float m_fTranslationSpeed;

	// Use this for initialization
	void Start () 
    {
        m_bHolding = false;
        m_bHasTarget = false;
        m_bStartDelay = false;
        m_fDelayTimer = 0.0f;
        m_fThrowDelayTime = 0.1f;
	}

    void FixedUpdate()
    {
        if (m_bHolding)
        {
            TranslateObject();
            CheckBreak();
        }
    }

	// Update is called once per frame
	void Update () 
    {
        //if (m_bHolding == false)
        //    RayCast();

        if (m_bStartDelay)
            DelayFire();

        if (m_bHolding)
        {
            InputDrop();
            CheckObject();
        }

        if (m_bHolding == false)
            InputPickup();

	}

    void RayCast()
    {
        RaycastHit hit;


        if (Physics.Raycast(m_cCamera.transform.position, m_cCamera.transform.forward, out hit, m_fPickupRange, m_mLayerMask))
        {
            m_bHasTarget = true;
            m_rTarget = hit;
        }
        else
            m_bHasTarget = false;
       

    }

    void InputPickup()
    {
        if (Input.GetButtonDown("Use"))
        {
            if (m_bHolding == false)
            {
                RayCast();

                if (m_bHasTarget == false)
                    return;

                m_tObject = m_rTarget.transform.GetComponent<Rigidbody>();
                if(m_tObject)
                {
                    //Debug.Log("Ray hit an object");
                    if (m_tObject.GetComponent<Prop_Basic>())
                    {
                        //Debug.Log("prop is prop hit by ray");
                        if (m_tObject.GetComponent<Prop_Basic>().isHeld() == false)
                        {
                           // Debug.Log("should be pickedup");
                            m_tObject.GetComponent<Prop_Basic>().Held();
                            m_bHolding = true;
                            //m_tObject.transform.parent = m_oHoldPosition.transform;


                            m_tObject.constraints = RigidbodyConstraints.FreezeRotation;
                            m_vObjectPosition = m_tObject.transform.position;
                        }
                        else
                        {
                            //do nothing atm maybe play disabled sound since someone else is carrying this object.
                            AudioSource.PlayClipAtPoint(m_aBreak, GetComponent<Transform>().position);
                        }
                    }
                }
            }
        }

    }

    void InputDrop()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (m_bHolding)
            {
                if (m_tObject.GetComponent<Prop_Basic>())
                    m_tObject.GetComponent<Prop_Basic>().Droped();
                m_bHolding = false;
                Throw();
                m_bStartDelay = true;
            }
        }

        if (Input.GetButtonDown("Fire2"))
        {
            if (m_bHolding)
            {
                if (m_tObject.GetComponent<Prop_Basic>())
                    m_tObject.GetComponent<Prop_Basic>().Droped();
                m_bHolding = false;
                Place();
                m_bStartDelay = true;
            }
        }
    }

    void TranslateObject()
    {
        //translate object to point.
        Vector3 test;
        test.x = 373;
        test.y = 10;
        test.z = 325;

        m_tObject.GetComponent<Rigidbody>().useGravity = false;

        m_vHoldPoint = m_cCamera.transform.position + (m_cCamera.transform.forward * m_fPickupRange);

        m_vTranslationVector = m_vHoldPoint - m_tObject.transform.position;
        //m_vTranslationVector = m_tObject.transform.position - m_vHoldPoint;

        m_vNonNormalTranslationVector = m_vTranslationVector;

        Debug.DrawRay(m_tObject.transform.position, m_vTranslationVector);
        

       m_vTranslationVector.Normalize();

        m_tObject.GetComponent<Rigidbody>().MovePosition(Vector3.MoveTowards(m_tObject.transform.position, m_vHoldPoint,m_fTranslationSpeed * Time.deltaTime));
       // m_tObject.GetComponent<Rigidbody>().transform.Translate(m_vTranslationVector * (m_fTranslationSpeed * Time.deltaTime));


        //looks good but can't use since it won't collide with anything after the condition is met.
        //if (Vector3.Distance(m_vHoldPoint, m_tObject.GetComponent<Rigidbody>().transform.position) < 2)
        //    m_tObject.GetComponent<Rigidbody>().transform.position = m_vHoldPoint;


    }

    void OnDrawGizmos()
    {
        if (m_tObject)
        {
            Gizmos.color = Color.blue;

            Gizmos.DrawSphere(m_vHoldPoint, 0.2f);
        }
    }


    void CheckBreak()
    {
        if (m_vNonNormalTranslationVector.magnitude > m_fBreakingRange)
        {
            Place();
            m_tObject.GetComponent<Prop_Basic>().Droped();
            m_bHolding = false;
            AudioSource.PlayClipAtPoint(m_aBreak, GetComponent<Transform>().position);
            m_tObject.GetComponent<Rigidbody>().useGravity = true;
        }
    }

    void Throw()
    {
        //m_tObject.transform.parent = null;

        if (m_tObject)
        {
            m_tObject.GetComponent<Rigidbody>().useGravity = true;
            m_tObject.constraints = RigidbodyConstraints.None;
            Vector3 force = m_cCamera.transform.forward * m_fThrowForce;
            m_tObject.AddForceAtPosition(force, m_tObject.position);
        }
        
    }

    void Place()
    {
        m_tObject.GetComponent<Rigidbody>().useGravity = true;
        m_tObject.constraints = RigidbodyConstraints.None;
        //m_tObject.transform.parent = null;
    }

    void CheckObject()
    {
        //make sure the object wasn't destroyed while you are holding it
        if (m_tObject == null)
            m_bHolding = false;
    }

    void DelayFire()
    {
        m_fDelayTimer += Time.deltaTime;
        if (m_fDelayTimer > m_fThrowDelayTime)
        {
            m_fDelayTimer = 0.0f;
            m_bStartDelay = false;
        }
    }

    public bool IsHoldingObject()
    {
        return m_bStartDelay;
    }
}
