using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MeleeWeapon : NetworkBehaviour
{

    private bool m_bDidPunch;
    private int m_iWeaponID;
    public float m_fROF;
    private float m_fFireTimer;
    public float m_fDamage;
    public float m_fRange;
    public AudioClip m_aMeleeSound;
  
    public Camera m_cCamera;

    private GameObject[] impacts;
    public GameObject m_oImpact;
    private int m_iCurrentImpact = 0;
    private int m_iMaxImpacts = 5;

    private rpccaller playerrpc;
    private GameObject m_oLocalPlayer;
    public ItemManipulation m_iItemManipScript;

	// Use this for initialization
	void Start () 
    {
        m_iWeaponID = 0;
        m_fFireTimer = 0.0f;
        m_bDidPunch = false;
        m_oLocalPlayer = GameObject.Find("LOCALPLAYER");

        impacts = new GameObject[m_iMaxImpacts];
        for (int i = 0; i < m_iMaxImpacts; i++)
        {
            impacts[i] = (GameObject)Instantiate(m_oImpact);
        }

        playerrpc = GetComponentInParent<rpccaller>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (m_bDidPunch)
        {
            m_fFireTimer += Time.deltaTime;
            if (m_fFireTimer >= m_fROF)
            {
                m_bDidPunch = false;
                m_fFireTimer = 0.0f;
            }
        }
        else
            ButtonInput();
	}

    void ButtonInput()
    {
        if (m_iItemManipScript.IsHoldingObject() == false)
        {
            if (Input.GetButton("Fire1"))
            {
                Punch();
                m_bDidPunch = true;
                AudioSource.PlayClipAtPoint(m_aMeleeSound, GetComponent<Transform>().position);
            }
        }
    }

    void Punch()
    {
        RaycastHit hit;
        if (Physics.Raycast(m_cCamera.transform.position, m_cCamera.transform.forward, out hit, m_fRange))
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

    public string GetAmmo()
    {
        return "PUNCH";
    }

}
