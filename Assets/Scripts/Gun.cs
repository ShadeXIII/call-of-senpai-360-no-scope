using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Gun : NetworkBehaviour {

    public float m_fReloadTime;
    private bool m_bReloading;
    private float m_fReloadTimer;

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
    public Slider m_hReloadSlider;
    public CanvasGroup m_cReloadAlpha;

    public int m_iMaxMagazine;
    public int m_iMaxAmmo;

    public int m_iMagazine;
    public int m_iAmmo;

    private GameObject m_oLocalPlayer;

    private bool m_bShooting;


    private GameObject[] impacts;
    private int m_iCurrentImpact = 0;
    private int m_iMaxImpacts = 5;

    private rpccaller playerrpc;

    private ItemManipulation m_iItemManipScript;

	// Use this for initialization
	void Start () 
    {
        //m_iAmmo = 220;
        //m_iMagazine = 30;
        //m_iMaxMagazine = 30;
        m_bShooting = false;
        m_bReloading = false;
        m_fReloadTimer = 0.0f;

        impacts = new GameObject[m_iMaxImpacts];
        for (int i = 0; i < m_iMaxImpacts; i++)
        {
            impacts[i] = (GameObject)Instantiate(m_oImpact);
        }
        UpdateHUD();
        m_hReloadSlider.value = 0;
        m_hReloadSlider.enabled = false;
        m_cReloadAlpha = m_hReloadSlider.GetComponent<CanvasGroup>();
        m_cReloadAlpha.alpha = 0.0f;
        m_hReloadSlider.maxValue = 1;

        playerrpc = GetComponentInParent<rpccaller>();
        m_iItemManipScript = m_cCamera.GetComponent<ItemManipulation>();
        m_oLocalPlayer = GameObject.Find("LOCALPLAYER");
	}

    // Update is called once per frame
    void Update() 
    {
        ButtonInput();
	}

    void FixedUpdate()
    {
        if (m_bShooting && m_bReloading == false)
        {
            CastRay();
        }

        if (m_bReloading)
        {
            UpdateReloadTimer();
        }
    }

    private void CastRay()
    {
        m_bShooting = false;

        RaycastHit hit;


        if (Physics.Raycast(m_cCamera.transform.position, m_cCamera.transform.forward, out hit, 50.0f))
        {

            if (hit.transform.gameObject.GetComponent<Health>())
            {
                //hit.transform.gameObject.GetComponent<Health>().TakeDamage(12.0f);
                playerrpc.HitPlayer(m_oLocalPlayer, 12.0f);
            }
            else if (hit.transform.gameObject.GetComponent<prop_health>())
            {
                hit.transform.gameObject.GetComponent<prop_health>().TakeDamage(12.0f);
                hit.transform.gameObject.GetComponent<Rigidbody>().AddForce(m_cCamera.transform.forward * 400.0f);
            }
            else if (hit.transform.gameObject.GetComponent<Rigidbody>())
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

    

    private void ButtonInput()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            //Debug.Log(m_iItemManipScript.IsHoldingObject());
            if (m_bReloading == false && m_iItemManipScript.IsHoldingObject() == false)
                Shoot();
        }

        if (Input.GetButtonDown("Reload"))
        {
            if(m_iAmmo > 0 && m_iMagazine != m_iMaxMagazine)
                Reload();
        }

        //if (Input.GetButtonDown("Fire2"))
        //{
        //    Debug.Log("fire2");
        //    playerrpc.HitPlayer(m_oLocalPlayer, 3);
        //}

        
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
        m_bReloading = true;
        m_hReloadSlider.enabled = true;
        m_cReloadAlpha.alpha = 1.0f;
    }

    private void UpdateReloadTimer()
    {
        m_fReloadTimer += Time.deltaTime;
        float percent = m_fReloadTimer / m_fReloadTime;
        m_hReloadSlider.value = percent;

        if (m_fReloadTimer > m_fReloadTime)
        {
            m_bReloading = false;
            AudioSource.PlayClipAtPoint(m_aReload, GetComponent<Transform>().position);
            int difference = m_iMaxMagazine - m_iMagazine;
            m_iAmmo -= difference;
            m_iMagazine = m_iMaxMagazine;
            UpdateHUD();
            m_hReloadSlider.enabled = false;
            m_hReloadSlider.value = 0;
            m_cReloadAlpha.alpha = 0.0f;
            m_fReloadTimer = 0.0f;
        }
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
