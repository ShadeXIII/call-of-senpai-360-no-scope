﻿using UnityEngine;
using System.Collections;

public class TriggerHurt : MonoBehaviour, IUseinterface
{

    
    public float m_fDamage;
    
    public float m_fDamageRate;

    public bool m_bEnabled;

    private float m_fTime;

    //private int test;

    // Use this for initialization
    void Start()
    {
        m_fTime = 0.0f;
        test = 0;
    }

    void OnDrawGizmos()
    {
        if (m_bEnabled)
            Gizmos.color = Color.green;
        else
            Gizmos.color = Color.red;

        Gizmos.DrawSphere(this.gameObject.transform.position, 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        m_fTime += Time.deltaTime;
    }

    void OnTriggerEnter(Collider collision)
    {
        Trigger(collision);
    }

    void OnTriggerStay(Collider collision)
    {
        Trigger(collision);
    }

    private void Trigger(Collider collision)
    {
        if (m_bEnabled)
        {
            if (m_fTime > m_fDamageRate)
            {
                //do the dmg
                m_fTime = 0.0f;
                //Debug.Log(m_fDamage);
                collision.gameObject.GetComponent<Health>().TakeDamage(m_fDamage);
            }
        }
    }

    void IUseinterface.Use()
    {
        //test++;
        m_bEnabled = !m_bEnabled;
        //Debug.Log("toggled " + test);
    }
}
