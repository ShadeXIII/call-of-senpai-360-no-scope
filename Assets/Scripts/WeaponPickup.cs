using UnityEngine;
using System.Collections;

public class WeaponPickup : MonoBehaviour {


    public int m_iAmmo;
    public int m_iMag;
    public int m_iWeaponID;
    public AudioClip m_aGunGet;

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    void OnTriggerEnter(Collider col)
    {
        Debug.Log("hit weapon pickup");
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<WeaponsManager>().WeaponPickedUp(m_iWeaponID, m_iAmmo, m_iMag);
            AudioSource.PlayClipAtPoint(m_aGunGet, transform.position);
            Destroy(gameObject.transform.parent.gameObject); //destroy the pickup
            Destroy(gameObject);
        }
    }

    public void AmmoForPickup(int ammo, int magammo)
    {
        //for when you drop a weapon.
        m_iAmmo = ammo;
        m_iMag = magammo;
    }

}
