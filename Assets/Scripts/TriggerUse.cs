using UnityEngine;
using System.Collections;

public class TriggerUse : MonoBehaviour {

    [SerializeField]
    bool m_bOneUse;

    [SerializeField]
    bool m_bUseButton;

    [SerializeField]
    float m_fUseTime;

    [SerializeField]
    GameObject[] m_tTarget;

    private bool m_bUsed;

    private float m_fTimer;

	// Use this for initialization
	void Start () 
    {
        m_bUsed = false;
        //set timer to usetime so you can use it immediatly
        m_fTimer = m_fUseTime;
	}
	
	// Update is called once per frame
	void Update () 
    {
        m_fTimer += Time.deltaTime;
	}

    void OnDrawGizmos()
    {
        for (int i = 0; i < m_tTarget.Length; i++)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(this.gameObject.transform.position, m_tTarget[i].transform.position);
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        //don't need to worry about use timer if we only use it once.
        if (m_bOneUse && m_bUsed == false)//could technically just destroy the trigger since it will only be used once anyways.
        {
            if (m_bUseButton)
            {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        Used();
                    }
            }
            else
            {
                Used();
            }
        }
        else if(m_bOneUse == false)
        {
            if (m_bUseButton)
            {
                if (m_fTimer >= m_fUseTime)
                {
                    if (Input.GetKeyDown(KeyCode.E))
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

    void OnTriggerStay(Collider collision)
    {
        if (m_bOneUse && m_bUsed == false)//could technically just destroy the trigger since it will only be used once anyways.
        {
            if (m_bUseButton)
            {
                if (Input.GetKeyDown(KeyCode.E))
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
                    if (Input.GetKeyDown(KeyCode.E))
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
        if (m_tTarget != null)
        {
            for (int i = 0; i < m_tTarget.Length; i++)
            {
                m_tTarget[i].SendMessage("Use");
            }
            m_bUsed = true;
            m_fTimer = 0.0f;
        }
    }

}
