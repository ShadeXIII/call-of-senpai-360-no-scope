using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.UI;

public class WeaponsManager : NetworkBehaviour
{


   // private SortedDictionary<int,bool> m_dWeaponsDictionary;
    private int m_iCurrentWeaponID;
    private int m_iNumberofWeapons;
    private WeaponPickup m_oWeapontoThow;
    public float m_fDelay;
    public AudioClip m_aSwitchWeapon;
    public Text m_tTotalAmmo;
    public Text m_tMagAmmo;
    public Text m_tDebugWeapon;

    //weapons===========
    public MeleeWeapon m_sMeleeWeapon;
    public Gun m_sGun;
    public Pistol m_sPistol;
    //private rifle m_sRifle;
    //private shotgun m_sShotGun;
    //private autorifle m_sAutoRifle;
    //==================
    //renderers=========
    public MeshRenderer m_rGunRenderer;
    public MeshRenderer m_rPistolRenderer;
    //==================
    private bool[] m_bInventory;

	// Use this for initialization
	void Start () 
    {
        m_iCurrentWeaponID = 0;
        m_iNumberofWeapons = 2; //add more numbers as more weapons are made.
        m_bInventory = new bool[m_iNumberofWeapons];
        for (int i = 0; i < m_iNumberofWeapons; i++)
        {
            m_bInventory[i] = false;
        }
        m_bInventory[0] = true;
        ActivateProperScript();
	}

    // Update is called once per frame
    void Update() 
    {
        ButtonsInput();
	}

    private void ButtonsInput()
    {
        SwitchWeaponScroll();
        SwitchWeaponButtons();
        ThrowWeapon();
    }

    private void SwitchWeaponScroll()
    {
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            AudioSource.PlayClipAtPoint(m_aSwitchWeapon, GetComponent<Transform>().position);
            //m_dWeaponsDictionary[m_iCurrentWeaponID] = false;

            if (m_iCurrentWeaponID - 1 < 0)
                m_iCurrentWeaponID = m_iNumberofWeapons;
            else
                m_iCurrentWeaponID -= 1;

            ActivateProperScript();
        }
        else if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            AudioSource.PlayClipAtPoint(m_aSwitchWeapon, GetComponent<Transform>().position);


            if (m_iCurrentWeaponID + 1 > m_iNumberofWeapons)
                m_iCurrentWeaponID = 0;
            else
                m_iCurrentWeaponID += 1;

            ActivateProperScript();
        }
    }

    private void SwitchWeaponButtons()
    {

    }

    public void WeaponPickedUp(int weaponID)
    {
        if (m_bInventory[weaponID] == false)
        {
            m_bInventory[weaponID] = true;
        }
    }

    private void ThrowWeapon()
    {
        if (m_iCurrentWeaponID != 0 && Input.GetButtonDown("DropWeapon")) //can't thow away your arms.
        {
            m_bInventory[m_iCurrentWeaponID] = false;
            //throw an instance of that weapon with all ammo.
            m_oWeapontoThow.m_iWeaponID = m_iCurrentWeaponID;
            //m_oWeapontoThow.m_iAmmo = 
            //Instantiate(WeaponPickup, gameObject.transform.position, gameObject.transform.rotation);
        }
    }

    public void DeathReset()
    {
        //use when the player dies.
        Debug.Log(m_iCurrentWeaponID);
        m_iCurrentWeaponID = 0;
        for (int i = 0; i < m_iNumberofWeapons; i++)
        {
           m_bInventory[i] = false;
        }
        m_bInventory[0] = true; //the first value is always true.(melee)
        ActivateProperScript();
    }

    private void ActivateProperScript()
    {
        Debug.Log(m_iCurrentWeaponID);
        switch (m_iCurrentWeaponID)
        {
            case 0:
                m_tDebugWeapon.text = "Melee " + m_iCurrentWeaponID.ToString();
                //change UI to weapons ammo
                m_tMagAmmo.text = m_sMeleeWeapon.GetAmmo();
                m_tTotalAmmo.text = m_sMeleeWeapon.GetAmmo();
                //disable and enable scripts
                m_sGun.enabled = false;
                m_sPistol.enabled = false;
                m_sMeleeWeapon.enabled = true;
                //disable renderers
                m_rGunRenderer.enabled = false;
                m_rPistolRenderer.enabled = false;
                break;
            case 1:
                m_tDebugWeapon.text = "Pistol " + m_iCurrentWeaponID.ToString();
                //change UI to weapons ammo
                m_tMagAmmo.text = m_sPistol.GetMagAmmo().ToString();
                m_tTotalAmmo.text = m_sPistol.GetTotalAmmo().ToString();
                //disable and enable scripts
                m_sMeleeWeapon.enabled = false;
                m_sGun.enabled = false;
                m_sPistol.enabled = true;
                //disable renderers
                m_rPistolRenderer.enabled = true;
                m_rGunRenderer.enabled = false;
                break;
            case 2:
                 m_tDebugWeapon.text = "Gun " + m_iCurrentWeaponID.ToString();
                //change UI to weapons ammo
                m_tMagAmmo.text = m_sGun.GetMagAmmo().ToString();
                m_tTotalAmmo.text = m_sGun.GetTotalAmmo().ToString();
                //disable and enable scripts
                m_sMeleeWeapon.enabled = false;
                m_sPistol.enabled = false;
                m_sGun.enabled = true;
                //disable renderers
                m_rPistolRenderer.enabled = false;
                m_rGunRenderer.enabled = true;
                break;
            default:
                m_sMeleeWeapon.enabled = true;
                m_sGun.enabled = false;
                break;
        }
    }



   
}
