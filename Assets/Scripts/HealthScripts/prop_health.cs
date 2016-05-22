using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class prop_health : NetworkBehaviour {

    
     public float m_fMaxHealth;

    [SyncVar]
     public float m_fCurrentHealth;

	// Use this for initialization
	void Start () 
    {
	    
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

   
    public void TakeDamage(float damage)
    {
        m_fCurrentHealth -= damage;
        if (m_fCurrentHealth <= 0)
        {
            //call death script
            SendMessage("DeathInterface.Death");
        }
    }

    [ClientRpc]
    public void RpcResolveHit(float damage)
    {
        Debug.Log("prop takes damage rpcresolved");
        TakeDamage(damage);
    }

}
