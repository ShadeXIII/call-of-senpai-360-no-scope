using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class rpccaller : NetworkBehaviour {

    public void HitPlayer(GameObject hit, int a)
    {
        CmdHitPlayer(hit, a);
    }

    [Command]
    void CmdHitPlayer(GameObject hit, int a)
    {
        if (hit.GetComponent<NetoworkedPlayerScript>() == null)
            Debug.Log("Did not get component.");

        Debug.Log(a);
        hit.GetComponent<NetoworkedPlayerScript>().RpcResolveHit();
    }


}
