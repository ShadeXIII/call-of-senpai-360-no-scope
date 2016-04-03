using UnityEngine;
using System.Collections;

public class TriggerUse : MonoBehaviour, IUseinterface 
{

    [SerializeField]
    bool m_bOneUse;

    [SerializeField]
    bool m_bUseButton;

    [SerializeField]
    bool m_bEnabled;

    [SerializeField]
    float m_fUseTime;

    [SerializeField]
    AudioClip m_tUse;

    [SerializeField]
    AudioClip m_tDisabled;

    [SerializeField]
    GameObject[] m_tTarget;

    private bool m_bUsed;

    private float m_fTimer;

	// Use this for initialization
	void Start () 
    {
        m_bUsed = false;
        //set timer to usetime so you can use it immediatly
        if (m_fUseTime == 0)
            m_fUseTime = 0.1f;
        m_fTimer = m_fUseTime;
	}
	
	// Update is called once per frame
	void Update () 
    {
        m_fTimer += Time.deltaTime;
	}

    void OnDrawGizmos()
    {
        if (m_bEnabled)
            Gizmos.color = Color.green;
        else
            Gizmos.color = Color.red;
        
        Gizmos.DrawSphere(this.gameObject.transform.position, 0.2f);

        for (int i = 0; i < m_tTarget.Length; i++)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(this.gameObject.transform.position, m_tTarget[i].transform.position);
        }
    }

    void PlayUseClip()
    {
        if (m_tUse != null)
            AudioSource.PlayClipAtPoint(m_tUse, GetComponent<Transform>().position);
    }

    void PlayDisabledClip()
    {
        if (m_tDisabled != null)
            AudioSource.PlayClipAtPoint(m_tDisabled, GetComponent<Transform>().position);
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player") 
            Trigger(collision);
    }

    void OnTriggerStay(Collider collision)
    {
        if(collision.tag == "Player")
            Trigger(collision);   
    }

    void Trigger(Collider collision)
    {
        //don't need to worry about use timer if we only use it once.
        if (m_bOneUse && m_bUsed == false)//could technically just destroy the trigger since it will only be used once anyways.
        {
            if (m_bUseButton)
            {
                if (Input.GetButtonDown("Use"))
                {
                    Used();
                }
            }
            else
            {
                Used();
            }
        }
        else if (m_bOneUse == false)
        {
            if (m_bUseButton)
            {
                if (m_fTimer >= m_fUseTime)
                {
                    if (Input.GetButtonDown("Use"))
                    {
                        Used();
                    }
                }
            }
            else
            {
                if (m_fTimer >= m_fUseTime)
                {
                    Used();
                }
            }
        }
        
    }

    void Used()
    {
        if (m_bEnabled)
            PlayUseClip();
        else
            PlayDisabledClip();


        if (m_bEnabled)
        {
            if (m_tTarget != null)
            {
                for (int i = 0; i < m_tTarget.Length; i++)
                {
                    IUseinterface canuse = m_tTarget[i].gameObject.GetComponent<IUseinterface>();

                    if (canuse != null)
                    {
                        //m_tTarget[i].SendMessage("IUseinterface.Use");
                        canuse.Use();
                    }
                    Debug.Log("used");
                }
                m_bUsed = true;
                m_fTimer = 0.0f;
            }
        }
    }

    void IUseinterface.Use()
    {
        m_bEnabled = !m_bEnabled;
    }


}
