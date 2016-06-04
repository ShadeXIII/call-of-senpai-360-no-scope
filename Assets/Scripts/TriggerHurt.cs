using UnityEngine;
using System.Collections;

public class TriggerHurt : MonoBehaviour, IUseinterface
{

    
    public float m_fDamage;
    
    public float m_fDamageRate;

    public bool m_bEnabled;

    private float m_fTime;

    private bool m_bAssignedrpcCaller;

    private rpccaller m_sRpccaller;

    // Use this for initialization
    void Start()
    {
        m_fTime = 0.0f;
        m_bAssignedrpcCaller = false;
    }

    void OnDrawGizmos()
    {
        if (m_bEnabled)
            Gizmos.color = Color.green;
        else
            Gizmos.color = Color.red;

        Gizmos.DrawSphere(this.gameObject.transform.position, 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        m_fTime += Time.deltaTime;
    }

    void OnTriggerEnter(Collider collision)
    {
        Trigger(collision);
    }

    void OnTriggerStay(Collider collision)
    {
        Trigger(collision);
    }

    private void Trigger(Collider collision)
    {
        if (m_bEnabled)
        {
            if (m_fTime > m_fDamageRate)
            {
                //do the dmg
                m_fTime = 0.0f;
                //Debug.Log(m_fDamage);

                if (collision.gameObject.GetComponent<Health>() != null)
                {
                    if(m_bAssignedrpcCaller == false)
                        AssignrpcCaller(collision.gameObject.GetComponent<rpccaller>());

                    m_sRpccaller.HitPlayer(collision.gameObject, m_fDamage);
                }
                else if (collision.gameObject.GetComponent<prop_health>() != null)
                {
                    if (m_bAssignedrpcCaller == false)
                        AssignrpcCaller(GameObject.Find("LOCALPLAYER").GetComponent<rpccaller>());
                    m_sRpccaller.HitProp(collision.gameObject, m_fDamage);
                }
            }
        }
    }

    void AssignrpcCaller(rpccaller rpc)
    {
        m_sRpccaller = rpc;
        m_bAssignedrpcCaller = true;
    }

    void IUseinterface.Use()
    {
        m_bEnabled = !m_bEnabled;
    }
}
