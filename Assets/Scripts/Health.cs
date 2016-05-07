using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Health : NetworkBehaviour {

    [SerializeField]
    float m_fMaxHealth;

    [SerializeField]
    bool m_bNoMax;

    [SerializeField]
    public Slider m_UHealthBar;

    [SerializeField]
    public Text m_UDebugHealth;

    [SerializeField]
    public Image m_UHealthBarColor;

    [SerializeField]
    Color m_CFullHealth;

    [SerializeField]
    Color m_CNoHealth;

    [SerializeField]
    AudioClip m_aPlayerHurt;

    [SyncVar]
    float m_fCurrentHealth;

	// Use this for initialization
	void Start () 
    {
        m_UHealthBar.maxValue = m_fMaxHealth;
        m_UHealthBar.value = m_fMaxHealth;
        m_fCurrentHealth = m_fMaxHealth;
        SetHealthColor();
        m_UDebugHealth.text = m_UHealthBar.value.ToString();
	}
	
	// Update is called once per frame
	void Update () 
    {
        
	}

    public void TakeDamage(float damage)
    {
        m_UHealthBar.value -= damage;
        m_fCurrentHealth -= damage;
        AudioSource.PlayClipAtPoint(m_aPlayerHurt, GetComponent<Transform>().position);
        if(m_UHealthBar.value <= 0)
        {
            //do something because this thing is dead.
            //possibly make a death script that is called...or something not sure yet.
            GetComponent<Rigidbody>().AddExplosionForce(150.0f, GetComponent<Rigidbody>().transform.position, 1.0f);
        }
        SetHealthColor();

        m_UDebugHealth.text = m_UHealthBar.value.ToString();
    }


    public void Heal(float heal)
    {
        if (m_bNoMax)
        {
            m_UHealthBar.value += heal;
            m_fCurrentHealth += heal;
            if (m_UHealthBar.maxValue > m_fMaxHealth)
            {
                m_UHealthBar.maxValue += heal;
                //m_fMaxHealth += heal;
                m_UHealthBar.value += heal;
                m_fCurrentHealth += heal;
            }
            SetHealthColor();
        }
        else
        {
            m_UHealthBar.value += heal;
            m_fCurrentHealth += heal;
            if (m_UHealthBar.maxValue > m_fMaxHealth)
            {
                m_UHealthBar.value = m_fMaxHealth;
                m_fCurrentHealth = m_fMaxHealth;
            }
            SetHealthColor();
        }


        m_UDebugHealth.text = m_UHealthBar.value.ToString();
    }

    public bool IsAtFullHealth()
    {
        if (m_fMaxHealth == m_UHealthBar.value && m_bNoMax == false)
            return true;
        else if (m_UHealthBar.value < m_fMaxHealth)
            return false;
        else if (m_bNoMax)
            return false;
        else
            return false;
    }

    private void SetHealthColor()
    {
        m_UHealthBarColor.color = Color.Lerp(m_CNoHealth, m_CFullHealth, m_UHealthBar.value / m_fMaxHealth);
    }
}
