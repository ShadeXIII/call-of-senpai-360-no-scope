using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

    [SerializeField]
    float m_fHealth;

    [SerializeField]
    float m_fMaxHealth;

    [SerializeField]
    bool m_bNoMax;

    [SerializeField]
    float m_fHealTime;

    [SerializeField]
    float m_fHealAmount;

    private float m_fHealTimer;

	// Use this for initialization
	void Start () 
    {
        m_fHealTimer = 0;
	}
	
	// Update is called once per frame
	void Update () 
    {
        GetInput();
	}

    void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            Debug.Log(m_fHealth);
        }

        if (Input.GetButton("Heal"))
        {
            m_fHealTimer += Time.deltaTime;
            Debug.Log(m_fHealTimer);
            if (m_fHealTimer > m_fHealTime)
            {
                Heal();
                m_fHealTimer = 0;
                //Debug.Log("Healed one");
            }
        }

        if (Input.GetButtonUp("Heal"))
        {
            m_fHealTimer = 0;
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


    public void Heal()
    {
        if (!m_bNoMax)
        {
            m_fHealth += m_fHealAmount;
            if (m_fHealth > m_fMaxHealth)
            {
                m_fHealth = m_fMaxHealth;
            }
        }
        else
        {
            m_fHealth += m_fHealAmount;
        }
       
    }

}
