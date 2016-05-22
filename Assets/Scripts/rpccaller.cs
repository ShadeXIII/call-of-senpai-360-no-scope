using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class rpccaller : NetworkBehaviour {

    public void HitPlayer(GameObject hit, float damage)
    {
        CmdHitPlayer(hit, damage);
    }

    public void HitProp(GameObject hit, float damage)
    {
        CmdHitProp(hit, damage);
    }

    [Command]
    void CmdHitPlayer(GameObject hit, float damage)
    {
        if (hit.GetComponent<NetoworkedPlayerScript>() == null)
            Debug.Log("Did not get NetworkedPlayerScript. RpcCaller: CmdHitPlayer");

        Debug.Log(damage);
        hit.GetComponent<NetoworkedPlayerScript>().RpcResolveHit(damage);
    }

    [Command]
    void CmdHitProp(GameObject hit, float damage)
    {
        if (hit.GetComponent<prop_health>() == null)
            Debug.Log("Did not get prop_health component. RpcCaller: CmdHitProp");

        hit.GetComponent<prop_health>().RpcResolveHit(damage);
    }


}
