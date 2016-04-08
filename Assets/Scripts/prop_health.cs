using UnityEngine;
using System.Collections;

public class prop_health : MonoBehaviour {

    
     public float m_fMaxHealth;
     public float m_fCurrentHealth;
     public AudioClip m_aDeath;

	// Use this for initialization
	void Start () 
    {
	    
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    private void Death()
    {
        AudioSource.PlayClipAtPoint(m_aDeath, GetComponent<Transform>().position);
        GetComponent<Rigidbody>().AddExplosionForce(250.0f, GetComponent<Rigidbody>().transform.position, 1.0f);
    }

    public void TakeDamage(float damage)
    {
        m_fCurrentHealth -= damage;
        if (m_fCurrentHealth <= 0)
        {
            Death();
        }
    }
}
