using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GliterPot : MonoBehaviour, IInteractable
{
    [Header("VFX Settings")]
    public Vector3 offset;
    public float timeToDestroyConfetti = 200f;

    public void Interaction()
    {
        VFXManager.Instance.PlayVFXByTypeWithCollision(VFXManager.VFXType.CONFETTI,
            this.transform.position, offset, null, true,
            timeToDestroyConfetti);

        UIImageFillManager.Instance.UpdateGlitterImage(Player.instance.GliterAmount / 100);

        Destroy(gameObject);
    }
}
