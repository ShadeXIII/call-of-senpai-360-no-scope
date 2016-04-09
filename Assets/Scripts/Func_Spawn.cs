using UnityEngine;
using System.Collections;

public class Func_Spawn : MonoBehaviour, IUseinterface {

    public GameObject m_oSpawn;

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    void IUseinterface.Use()
    {
        Instantiate(m_oSpawn, this.gameObject.transform.position, this.gameObject.transform.rotation);
    }
}
