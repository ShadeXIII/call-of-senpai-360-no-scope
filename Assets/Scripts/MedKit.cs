using UnityEngine;
using System.Collections;

public class MedKit : MonoBehaviour {

    public float m_fHealAmmount;
    public AudioClip m_aHealSound;

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    //void OnCollisionEnter(Collision col)
    //{
    //    if (col.gameObject.tag == "Player")
    //    {
    //        Health hp = col.gameObject.GetComponent<Health>();
    //        if (hp.IsAtFullHealth() == false)
    //        {
    //            AudioSource.PlayClipAtPoint(m_aHealSound, col.gameObject.transform.position);
    //            hp.Heal(m_fHealAmmount);
    //            Destroy(gameObject);
    //        }
    //    }
    //}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            Health hp = col.gameObject.GetComponent<Health>();
            if (hp.IsAtFullHealth() == false)
            {
                AudioSource.PlayClipAtPoint(m_aHealSound, col.gameObject.transform.position);
                hp.Heal(m_fHealAmmount);
                Destroy(gameObject);
            }
        }
    }
}
