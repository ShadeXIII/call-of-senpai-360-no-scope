using UnityEngine;
using System.Collections;

public interface GunInterface
{

    void CastRay();
    void ButtonInput();
    void Shoot();
    void Reload();
    void UpdateHUD();
    void GiveAmmo();
    bool IsAmmoFull();
    void UpdateReloadTimer();

}