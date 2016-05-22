using UnityEngine;
using System.Collections;

public class Prop_Basic : MonoBehaviour{

    private bool m_bIsHeld;


	// Use this for initialization
	void Start () 
    {
        m_bIsHeld = false;
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}


    public void Held()
    {
        m_bIsHeld = true;
    }

    public void Droped()
    {
        m_bIsHeld = false;
    }

    public bool isHeld()
    {
        return m_bIsHeld;
    }
}
