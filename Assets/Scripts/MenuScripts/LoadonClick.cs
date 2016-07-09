using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadonClick : MonoBehaviour {

    public AudioClip m_aClick;
    public AudioClip m_aHover;
    public GameObject m_oLoadingScreen;
    public Camera m_cCamera;
    public Button m_bPlay;
    public Button m_bExit;

    public void LoadScene(int level)
    {
        AudioSource.PlayClipAtPoint(m_aClick, m_cCamera.transform.position);
        m_oLoadingScreen.SetActive(true);
        Application.LoadLevel(level);
    }

    public void ExitGame()
    {
        AudioSource.PlayClipAtPoint(m_aClick, m_cCamera.transform.position);
        Application.Quit();
    }

    //public void OnPointerEnter(int button)
    //{
    //    switch (button)
    //    {
    //        case 0:
    //            m_bPlay.GetComponent<Text>().color = m_bPlay.colors.highlightedColor;
    //            AudioSource.PlayClipAtPoint(m_aHover, m_cCamera.transform.position);
    //            break;
    //        case 1:
    //            m_bExit.GetComponent<Text>().color = m_bExit.colors.highlightedColor;
    //            AudioSource.PlayClipAtPoint(m_aHover, m_cCamera.transform.position);
    //            break;
    //    }
    //}

    //public void OnPointerExit(int button)
    //{
    //    switch (button)
    //    {
    //        case 0:
    //            m_bPlay.GetComponent<Text>().color = m_bPlay.colors.normalColor;
    //            break;
    //        case 1:
    //            m_bExit.GetComponent<Text>().color = m_bExit.colors.normalColor;
    //            break;
    //    }
    //}
}
