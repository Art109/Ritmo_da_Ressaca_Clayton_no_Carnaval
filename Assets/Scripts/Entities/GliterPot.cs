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
        Debug.Log("Disparei o efeito");
        VFXManager.Instance.PlayVFXByTypeWithCollision(VFXManager.VFXType.CONFETTI,
            this.transform.position, offset, null, true,
            timeToDestroyConfetti);

        Destroy(gameObject);
    }

    
}
