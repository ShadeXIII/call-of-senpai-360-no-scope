using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class NetoworkedPlayerScript : NetworkBehaviour
{
    public fpscontroller fpsController;
    public Camera fpsCamera;
    public AudioListener audioListener;
    public Gun gunScript;
    public Health health;
    public CharacterMover mover;
    public CameraObjectFollower cameraObjFollower;


    public override void OnStartLocalPlayer()
    {
        fpsCamera.enabled = true;
        fpsController.enabled = true;
        audioListener.enabled = true;
        gunScript.enabled = true;
        health.enabled = true;
        mover.enabled = true;
        cameraObjFollower.enabled = true;

        gameObject.name = "LOCALPLAYER";

        SetUpHUD();

        base.OnStartLocalPlayer();
    }

    void SetUpHUD()
    {
        health.m_UHealthBar = GameObject.Find("Health Bar").GetComponent<UnityEngine.UI.Slider>();
        health.m_UDebugHealth = GameObject.Find("DebugHealth").GetComponent<UnityEngine.UI.Text>();
        health.m_UHealthBarColor = GameObject.Find("HealthBarFill").GetComponent<UnityEngine.UI.Image>();

        gunScript.m_tTotalAmmo = GameObject.Find("totalammoremaining").GetComponent<UnityEngine.UI.Text>();
        gunScript.m_tMagAmmo = GameObject.Find("magammo").GetComponent<UnityEngine.UI.Text>();
        gunScript.m_hReloadSlider = GameObject.Find("ReloadSlider").GetComponent<UnityEngine.UI.Slider>();

    }


    void ToggleRenderer(bool isAlive)
    {
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].enabled = isAlive;
        }
    }


    void ToggleControls(bool isAlive)
    {
        fpsController.enabled = isAlive;
        gunScript.enabled = isAlive;
        //fpsCamera.cullingMask = ~fpsCamera.cullingMask;
    }

    void PlayerDeath()
    {
        ToggleRenderer(false);

        if (isLocalPlayer)
        {
            Transform Spawn = NetworkManager.singleton.GetStartPosition();
            transform.position = Spawn.position;
            transform.rotation = Spawn.rotation;
            ToggleControls(false);
        }


        Invoke("Respawn", 2);
    }

    [ClientRpc]
    public void RpcResolveHit(float damage)
    {
        health.TakeDamage(damage);

        if (health.IsDead())
            PlayerDeath();
    }

    void Respawn()
    {
        ToggleRenderer(true);
        if (isLocalPlayer)
        {
            ToggleControls(true);
        }

        health.Heal(health.m_fMaxHealth);
    }
}
