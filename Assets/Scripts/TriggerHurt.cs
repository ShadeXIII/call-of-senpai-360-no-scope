using UnityEngine;
using System.Collections;

public class TriggerHurt : MonoBehaviour, IUseinterface
{

    [SerializeField]
    float m_fDamage;
    [SerializeField]
    float m_fROD;
    [SerializeField]
    bool m_bEnabled;


    private bool m_bDoDamage;
    private float m_fTime;

    // Use this for initialization
    void Start()
    {
        m_fTime = 0.0f;
        m_bDoDamage = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_bEnabled)
        {
            if (!m_bDoDamage)
            {
                m_fTime += Time.deltaTime;
                if (m_fTime > m_fROD)
                {
                    m_fTime = 0.0f;
                    m_bDoDamage = true;
                }
            }
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        collision.gameObject.GetComponent<Health>().TakeDamage(m_fDamage);
        //Debug.Log("col enter");
        m_bDoDamage = false;
    }

    void OnTriggerStay(Collider collision)
    {
        if (m_bDoDamage)
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(m_fDamage);
            //Debug.Log("col stay");
            m_bDoDamage = false;
        }
    }

    void IUseinterface.Use()
    {
        m_bEnabled = !m_bEnabled;
    }
}
