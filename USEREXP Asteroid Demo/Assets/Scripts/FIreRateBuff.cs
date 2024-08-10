using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Buffs/FireRateBuff")]
public class FIreRateBuff : Buff
{
    public float buffValue = 1f;
    public float duration = 5f;
    public override void Apply(GameObject target)
    {
        Debug.Log($"Applying fire rate buff with value {buffValue} to {target.name}");
        Shoot playerShoot = target.transform.GetChild(0).GetComponent<Shoot>();
        if (playerShoot != null)
        {
            playerShoot.fRMod += buffValue;
            playerShoot.StartCoroutine(UnapplicationCoroutine(target));
        }
    }
    // SET DURATION & RESET
    public IEnumerator UnapplicationCoroutine(GameObject target)
    {
        Shoot playerShoot = target.transform.GetChild(0).GetComponent<Shoot>();
        yield return new WaitForSeconds(duration);
        playerShoot.fRMod -= buffValue;
    }
}