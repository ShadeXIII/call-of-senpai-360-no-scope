using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Gun : MonoBehaviour {

    public float m_fReloadTime;
    public ParticleSystem m_pMuzzleFlash;
    public Transform m_tFlashLocation;
    //public GameObject m_gImpact;
    public AudioClip m_aGunShot;
    public AudioClip m_aReload;
    public AudioClip m_aEmpty;
    public GameObject m_oImpact;
    public Camera m_cCamera;
    public Text m_tTotalAmmo;
    public Text m_tMagAmmo;

    public int m_iMaxMagazine;
    public int m_iMaxAmmo;

    public int m_iMagazine;
    public int m_iAmmo;
    


    private bool m_bShooting;

    private GameObject[] impacts;
    private int m_iCurrentImpact = 0;
    private int m_iMaxImpacts = 5;

    private float m_fReloadTimer;

	// Use this for initialization
	void Start () 
    {
        //m_iAmmo = 220;
        //m_iMagazine = 30;
        //m_iMaxMagazine = 30;
        m_bShooting = false;

        impacts = new GameObject[m_iMaxImpacts];
        for (int i = 0; i < m_iMaxImpacts; i++)
        {
            impacts[i] = (GameObject)Instantiate(m_oImpact);
        }

        UpdateHUD();
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


            if (Physics.Raycast(m_cCamera.transform.position, m_cCamera.transform.forward, out hit, 50.0f))
            {
               
                if (hit.transform.gameObject.GetComponent<Health>())
                {
                    hit.transform.gameObject.GetComponent<Health>().TakeDamage(12.0f);
                }
                else if (hit.transform.gameObject.GetComponent<prop_health>())
                {
                    hit.transform.gameObject.GetComponent<prop_health>().TakeDamage(12.0f);
                    hit.transform.gameObject.GetComponent<Rigidbody>().AddForce(m_cCamera.transform.forward * 400.0f);
                }
                else if(hit.transform.gameObject.GetComponent<Rigidbody>())
                {
                    hit.transform.gameObject.GetComponent<Rigidbody>().AddForce(m_cCamera.transform.forward * 400.0f);
                }


                impacts[m_iCurrentImpact].transform.position = hit.point;
                //impacts[m_iCurrentImpact].GetComponent<ParticleSystem>().Play();
                if (++m_iCurrentImpact >= m_iMaxImpacts)
                {
                    m_iCurrentImpact = 0;
                }
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
        if (m_iMagazine > 0)
        {
            m_iMagazine -= 1;
            // m_pMuzzleFlash.Play();
            m_bShooting = true;
            AudioSource.PlayClipAtPoint(m_aGunShot, GetComponent<Transform>().position);
            UpdateHUD();
        }
        else if (m_iAmmo > 0 && m_iMagazine <= 0)
        {
            Reload();
        }
        else
            AudioSource.PlayClipAtPoint(m_aEmpty, GetComponent<Transform>().position);
    }

    private void Reload()
    {
        AudioSource.PlayClipAtPoint(m_aReload, GetComponent<Transform>().position);
        int difference = m_iMaxMagazine - m_iMagazine;
        m_iAmmo -= difference;
        m_iMagazine = m_iMaxMagazine;
        UpdateHUD();
    }

    private void UpdateHUD()
    {
        m_tMagAmmo.text = m_iMagazine.ToString();
        m_tTotalAmmo.text = m_iAmmo.ToString();
    }

    public void GiveAmmo(int ammo)
    {
        int ammoafterget = m_iAmmo + ammo;

        if (ammoafterget > m_iMaxAmmo)
            m_iAmmo = m_iMaxAmmo;
        else
            m_iAmmo += ammo;

        UpdateHUD();
    }

    public bool IsAmmoFull()
    {
        if (m_iAmmo == m_iMaxAmmo)
            return true;
        else
            return false;
    }
}
