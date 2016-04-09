using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {

    public float m_fReloadTime;
    public ParticleSystem m_pMuzzleFlash;
    public Transform m_tFlashLocation;
    //public GameObject m_gImpact;
    public AudioClip m_aGunShot;
    public AudioClip m_aReload;
    public AudioClip m_aGetAmmo;
    public GameObject m_oImpact;


    private int m_iMaxMagazine;
    private int m_iMagazine;
    private int m_iAmmo;
    private bool m_bShooting;

    private GameObject[] impacts;
    private int m_iCurrentImpact = 0;
    private int m_iMaxImpacts = 5;

    private float m_fReloadTimer;

	// Use this for initialization
	void Start () 
    {
        m_iAmmo = 220;
        m_iMagazine = 30;
        m_iMaxMagazine = 30;
        m_bShooting = false;

        impacts = new GameObject[m_iMaxImpacts];
        for (int i = 0; i < m_iMaxImpacts; i++)
        {
            impacts[i] = (GameObject)Instantiate(m_oImpact);
        }
	}

    // Update is called once per frame
    void Update() 
    {
        ButtonInput();
	}

    void FixedUpdate()
    {
        if (m_bShooting)
        {
            m_bShooting = false;

            RaycastHit hit;


            if (Physics.Raycast(transform.position, transform.forward, out hit, 50.0f))
            {
                impacts[m_iCurrentImpact].transform.position = hit.point;
                //impacts[m_iCurrentImpact].GetComponent<ParticleSystem>().Play();
                if (++m_iCurrentImpact >= m_iMaxImpacts)
                {
                    m_iCurrentImpact = 0;
                }
                if (hit.transform.gameObject.GetComponent<Health>())
                {
                    hit.transform.gameObject.GetComponent<Health>().TakeDamage(12.0f);
                }
                else if (hit.transform.gameObject.GetComponent<prop_health>())
                {
                    hit.transform.gameObject.GetComponent<prop_health>().TakeDamage(12.0f);
                    //hit.transform.gameObject.GetComponent<Rigidbody>().AddForce(hit.transform.position);
                }
                //else
                //{
                //    //hit.transform.gameObject.GetComponent<Rigidbody>().AddForce(hit.transform.position);
                //}
            }
        }
    }

    private void ButtonInput()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }

        if (Input.GetButtonDown("Reload"))
        {
            Reload();
        }
    }

    private void Shoot()
    {
        //if (m_iAmmo > 0)
        //{
            m_iMagazine -= 1;
           // m_pMuzzleFlash.Play();
            m_bShooting = true;
            AudioSource.PlayClipAtPoint(m_aGunShot, GetComponent<Transform>().position);
       // }
    }

    private void Reload()
    {
        int difference = m_iMaxMagazine - m_iMagazine;
        m_iAmmo -= difference;
        m_iMagazine = m_iMaxMagazine;
    }

    public void GetAmmo(int ammo)
    {
        m_iAmmo += ammo;
    }
}
