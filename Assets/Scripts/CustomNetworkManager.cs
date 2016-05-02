using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class CustomNetworkManager : NetworkManager 
{
    public GameObject lobbyCamera;
    public GameObject HUD;


    public override void OnStartClient(NetworkClient client)
    {
        HideLobbyCamera();
        ShowHUD();
    }

    public override void OnStartHost()
    {
        HideLobbyCamera();
        ShowHUD();
    }

    public override void OnStopClient()
    {
        ShowLobbyCamera();
        HideHUD();
    }

    public override void OnStopHost()
    {
        ShowLobbyCamera();
        HideHUD();
    }

    private void HideLobbyCamera()
    {
        if (lobbyCamera)
            lobbyCamera.SetActive(false);
    }

    private void ShowLobbyCamera()
    {
        if (lobbyCamera)
            lobbyCamera.SetActive(true);
    }

    private void HideHUD()
    {
        if (HUD)
            HUD.SetActive(false);
    }

    private void ShowHUD()
    {
        if (HUD)
            HUD.SetActive(true);
    }
	
}

