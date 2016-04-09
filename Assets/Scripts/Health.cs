using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Health : MonoBehaviour {

    [SerializeField]
    float m_fMaxHealth;

    [SerializeField]
    bool m_bNoMax;

    [SerializeField]
    Slider m_UHealthBar;

    [SerializeField]
    Text m_UDebugHealth;

    [SerializeField]
    Image m_UHealthBarColor;

    [SerializeField]
    Color m_CFullHealth;

    [SerializeField]
    Color m_CNoHealth;

	// Use this for initialization
	void Start () 
    {
        m_UHealthBar.maxValue = m_fMaxHealth;
        m_UHealthBar.value = m_fMaxHealth;
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
            if (m_UHealthBar.maxValue > m_fMaxHealth)
            {
                m_UHealthBar.maxValue += heal;
                m_fMaxHealth += heal;
                m_UHealthBar.value += heal;
            }
            SetHealthColor();
        }
        else
        {
            m_UHealthBar.value += heal;
            if (m_UHealthBar.maxValue > m_fMaxHealth)
            {
                m_UHealthBar.value = m_fMaxHealth;
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
