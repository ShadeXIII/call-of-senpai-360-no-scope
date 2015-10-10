using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

    [SerializeField]
    float m_fHealth;

    [SerializeField]
    float m_fMaxHealth;

    [SerializeField]
    bool m_bNoMax;

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            Debug.Log(m_fHealth);
        }
	}

    public void TakeDamage(float damage)
    {
        m_fHealth -= damage;
        if(m_fHealth < 0)
        {
            //do something because this thing is dead.
            GetComponent<Rigidbody>().AddExplosionForce(150.0f, GetComponent<Rigidbody>().transform.position, 1.0f);
        }
    }


    public void Heal(float heal)
    {
        if (!m_bNoMax)
        {
            m_fHealth += heal;
            if (m_fHealth > m_fMaxHealth)
            {
                m_fHealth = m_fMaxHealth;
            }
        }
        else
        {
            m_fHealth += heal;
        }
       
    }

}
