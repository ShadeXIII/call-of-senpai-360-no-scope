using UnityEngine;
using System.Collections;

public class TriggerUse : MonoBehaviour {

    [SerializeField]
    bool m_bOneUse;

    [SerializeField]
    float m_fUseTime;

    [SerializeField]
    GameObject[] m_tTarget;

    [SerializeField]
    bool m_bUseButton;


	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    void OnTriggerEnter(Collider collision)
    {
       // m_tTarget.GetComponent<Use>().Use();
    }

    void OnTriggerStay(Collider collision)
    {
        //m_tTarget.GetComponent<Use>().Use();
    }

}
