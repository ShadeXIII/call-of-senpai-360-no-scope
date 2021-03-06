﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Pistol : NetworkBehaviour
{
    public float m_fReloadTime;
    private bool m_bReloading;
    private float m_fReloadTimer;
    public float m_fDamage;
    public float m_fROF;
    private float m_fFireTimer;
    private bool m_bFired;
    public float m_fRaySpread;

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
    private int m_iWeaponID = 1;



	// Use this for initialization
	void Start () 
    {
        m_bShooting = false;
        m_bReloading = false;
        m_fReloadTimer = 0.0f;
        m_fFireTimer = 0.0f;
        m_bFired = false;

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
	void Update () 
    {
        if (m_bFired)
        {
            m_fFireTimer += Time.deltaTime;
            if (m_fFireTimer >= m_fROF)
            {
                m_fFireTimer = 0.0f;
                m_bFired = false;
            }
        }

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
        Vector3 direction = m_cCamera.transform.forward;
        direction.x += Random.Range(-m_fRaySpread, m_fRaySpread);
        direction.y += Random.Range(-m_fRaySpread, m_fRaySpread);
        direction.z += Random.Range(-m_fRaySpread, m_fRaySpread);


        if (Physics.Raycast(m_cCamera.transform.position, direction, out hit, 50.0f))
        {

            if (hit.transform.gameObject.GetComponent<Health>())
            {
                playerrpc.HitPlayer(m_oLocalPlayer, m_fDamage);
            }
            else if (hit.transform.gameObject.GetComponent<prop_health>())
            {
                hit.transform.gameObject.GetComponent<prop_health>().TakeDamage(m_fDamage);
                hit.transform.gameObject.GetComponent<Rigidbody>().AddForce(m_cCamera.transform.forward * 400.0f);
            }
            else if (hit.transform.gameObject.GetComponent<Rigidbody>())
            {
                hit.transform.gameObject.GetComponent<Rigidbody>().AddForce(m_cCamera.transform.forward * 400.0f);
            }


            impacts[m_iCurrentImpact].transform.position = hit.point;
            if (++m_iCurrentImpact >= m_iMaxImpacts)
            {
                m_iCurrentImpact = 0;
            }
        }
    }

    private void ButtonInput()
    {
        

        if (Input.GetButton("Fire1"))
        {
            if (m_bReloading == false && m_iItemManipScript.IsHoldingObject() == false && m_bFired == false)
                Shoot();
        }

        if (Input.GetButtonDown("Reload"))
        {
            if (m_iAmmo > 0 && m_iMagazine != m_iMaxMagazine)
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
            m_bFired = true;
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


    void UpdateHUD()
    {
        m_tMagAmmo.text = m_iMagazine.ToString();
        m_tTotalAmmo.text = m_iAmmo.ToString();
    }

    public bool IsAmmoFull()
    {
        if (m_iAmmo == m_iMaxAmmo)
            return true;
        else
            return false;
    }

    public int GetMagAmmo()
    {
        return m_iMagazine;
    }

    public int GetTotalAmmo()
    {
        return m_iAmmo;
    }

    public int GetWeaponID()
    {
        return m_iWeaponID;
    }

    public void GiveAmmo(int ammo, bool updatehud)
    {
        int test = m_iAmmo + ammo;
        if (test < m_iMaxAmmo)
            m_iAmmo += ammo;
        else
            m_iAmmo = m_iMaxAmmo;

        if(updatehud)
            UpdateHUD();
    }

    public void GiveMagAmmo(int ammo, bool updatehud)
    {
        //only used when picking up a weapon
        //check just in case 
        int test = m_iMagazine + ammo;
        if (test < m_iMaxMagazine)
            m_iMagazine += ammo;
        else
            m_iMagazine = m_iMaxMagazine;

        if (updatehud)
            UpdateHUD();
    }

    public void AmmoReset()
    {
        m_iAmmo = 0;
        m_iMagazine = 0;
    }
}
