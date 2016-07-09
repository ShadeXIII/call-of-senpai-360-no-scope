using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseMenuEvents : MonoBehaviour {

    public AudioClip m_aClick;
    public Camera m_cCamera;
    public Button m_bMainMenu;
    public Button m_bExit;
    public Button m_bBack;
    private GameObject m_oPlayer;
    private bool m_bFoundPlayer;

	// Use this for initialization
	void Start () {
        m_bFoundPlayer = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ExitGame()
    {
        Debug.Log("Exit game");
        Application.Quit();
    }

    public void ToMainMenu()
    {
        Debug.Log("main menu");
        Application.LoadLevel(1);
    }

    public void BackToGame()
    {
        if(m_bFoundPlayer == false)
        {
            Debug.Log("found player");
            m_oPlayer = GameObject.Find("LOCALPLAYER");
            m_bFoundPlayer = true;
        }
        m_oPlayer.GetComponent<PauseMenuScript>().BackButtonPressed();
    }
}
