using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseMenuScript : MonoBehaviour {

    public Canvas m_cHUD;
    public Canvas m_cPauseMenu;
    public AudioClip m_aClick;
    private bool m_bPauseActive;

	// Use this for initialization
	void Start () 
    {
        m_bPauseActive = false;
	}
	
	// Update is called once per frame
	void Update () 
    {
        ButtonInput();
	}

    private void ButtonInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && m_bPauseActive == false)
        {
            m_cHUD.enabled = false;
            m_cPauseMenu.enabled = true;
            m_bPauseActive = true;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            GetComponent<fpscontroller>().enabled = false;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && m_bPauseActive == true)
        {
            m_cHUD.enabled = true;
            m_cPauseMenu.enabled = false;
            m_bPauseActive = false;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            GetComponent<fpscontroller>().enabled = true;
        }
    }

    public void BackButtonPressed()
    {
        m_cHUD.enabled = true;
        m_cPauseMenu.enabled = false;
        m_bPauseActive = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        GetComponent<fpscontroller>().enabled = true;
    }
}
