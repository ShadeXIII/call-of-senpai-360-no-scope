using UnityEngine;
using System.Collections;


public class AmmoPickup : MonoBehaviour {

    public int m_iWeaponID;
    public int m_iAmmo;
    public AudioClip m_aAmmoGet;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            //check if ammo is full for weapon or not.
            if (col.gameObject.GetComponent<WeaponsManager>().HasWeapon(m_iWeaponID))
            {
                if (col.gameObject.GetComponent<WeaponsManager>().GetIsWeaponAmmoFull(m_iWeaponID))
                    return;
                else
                {
                    col.gameObject.GetComponent<WeaponsManager>().AmmoPickedUp(m_iWeaponID, m_iAmmo, 0);
                    AudioSource.PlayClipAtPoint(m_aAmmoGet, transform.position);
                    Destroy(gameObject);
                }
            }

           

            //Gun gun = col.gameObject.GetComponentInChildren<Gun>();

            //if (gun.IsAmmoFull() == false)
            //{
            //    gun.GiveAmmo(m_iAmmo);
            //    AudioSource.PlayClipAtPoint(m_aAmmoGet, transform.position);
            //    Destroy(gameObject);
            //}
        }
    }
}
