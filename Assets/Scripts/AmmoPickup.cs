using UnityEngine;
using System.Collections;


public class AmmoPickup : MonoBehaviour {

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

            Gun gun = col.gameObject.GetComponentInChildren<Gun>();

            if (gun.IsAmmoFull() == false)
            {
                gun.GiveAmmo(m_iAmmo);
                AudioSource.PlayClipAtPoint(m_aAmmoGet, transform.position);
                Destroy(gameObject);
            }
        }
    }
}
