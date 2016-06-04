using UnityEngine;
using System.Collections;

public class WeaponPickup : MonoBehaviour {


    public int m_iAmmo;
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
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<WeaponsManager>().WeaponPickedUp(m_iWeaponID);
            AudioSource.PlayClipAtPoint(m_aGunGet, transform.position);
            Destroy(gameObject);
        }
    }

}
