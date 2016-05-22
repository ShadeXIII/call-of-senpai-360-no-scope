using UnityEngine;
using System.Collections;

public class ExplosiveBarrelDeath : MonoBehaviour, DeathInterface
{

    public AudioClip m_aDeath;
    public float m_fMaxDamage;
    public float m_fDamageRadius;
    public float m_fExplosionForce;
    public bool m_bExplodeImmediatly;
    public float m_fExplosionTime;
    public float m_FExplosionTimer;
    public LayerMask m_mLayerMask;
    private rpccaller networkrpccaller;

    private bool m_bFoundPlayer;

    // Use this for initialization
    void Start()
    {
        //playerrpc = null;
        m_bFoundPlayer = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_bFoundPlayer == false)
        {
            //playerrpc = GameObject.Find("LOCALPLAYER").GetComponent<rpccaller>();
            //if (playerrpc != null)
            //    m_bFoundPlayer = true;
            //Debug.Log(playerrpc);
        }
    }

    void DeathInterface.Death()
    {
        AudioSource.PlayClipAtPoint(m_aDeath, GetComponent<Transform>().position);
        //GetComponent<Rigidbody>().AddExplosionForce(250.0f, GetComponent<Rigidbody>().transform.position, 1.0f);

        //Do Damage in diameter
        Collider[] colliders = Physics.OverlapSphere(transform.position, m_fDamageRadius, m_mLayerMask);

        //Debug.Log(colliders.Length);
        for (int i = 0; i < colliders.Length; ++i)
        {
            Rigidbody targetrigidbody = colliders[i].GetComponent<Rigidbody>();

            if (targetrigidbody == null)
            {
                //Debug.Log("fucked no rigidbody");
                continue;
            }
            //else
            // Debug.Log("rigidbody found");


            float damage = CalculateDamage(targetrigidbody.position);
            float force = CalculateForce(targetrigidbody.position);


            Health playerhealth = null;
            if (targetrigidbody.tag == "Player")
            {
                //Debug.Log("player found");
                playerhealth = targetrigidbody.GetComponent<Health>();
            }

            prop_health prophealth = targetrigidbody.GetComponent<prop_health>();


            //modify force by how close
            targetrigidbody.AddExplosionForce(force, transform.position, m_fDamageRadius);

            networkrpccaller = targetrigidbody.GetComponent<rpccaller>();
            if (networkrpccaller)
            {
                if (playerhealth != null)
                    networkrpccaller.HitPlayer(targetrigidbody.gameObject, damage);


                if (prophealth != null)
                {
                    //Debug.Log("should go in here");
                    networkrpccaller.HitProp(targetrigidbody.gameObject, damage);
                }
            }
        }

        Destroy(transform.root.gameObject);
    }

    private float CalculateDamage(Vector3 position)
    {
        //Debug.Log(position);

        Vector3 explosiontotarget = position - transform.position;
        float explosionDistance = explosiontotarget.magnitude;

        float relativeDistance = (m_fDamageRadius - explosionDistance) / m_fDamageRadius;
        //Debug.Log(relativeDistance);

        float damage = relativeDistance * m_fMaxDamage;

        return damage;
    }


    private float CalculateForce(Vector3 position)
    {
        Vector3 explosiontotarget = position - transform.position;
        float explosionDistance = explosiontotarget.magnitude;

        float relativeDistance = (m_fDamageRadius - explosionDistance) / m_fDamageRadius;

        float force = relativeDistance * m_fExplosionForce;

        //Debug.Log("force: " + force);

        return force;
    }
}
