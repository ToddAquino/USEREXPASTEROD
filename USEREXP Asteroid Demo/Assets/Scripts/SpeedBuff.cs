using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Buffs/SpeedBuff")]
public class SpeedBuff : Buff
{
    public float buffValue = 5f;
    public float duration = 5f;
    public override void Apply(GameObject target)
    {
        Debug.Log($"Applying speed buff with value {buffValue} to {target.name}");
        PlayerMovement playerMovement = target.GetComponent<PlayerMovement>();
        if (playerMovement != null)
        {
            playerMovement.speed += buffValue;
            playerMovement.StartCoroutine(UnapplicationCoroutine(target));
        }
    }
    // SET DURATION & RESET
    public IEnumerator UnapplicationCoroutine(GameObject target)
    {
        PlayerMovement playerMovement = target.GetComponent<PlayerMovement>();
        yield return new WaitForSeconds(duration);
        playerMovement.speed -= buffValue;
    }
}
