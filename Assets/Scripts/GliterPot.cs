using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GliterPot : MonoBehaviour
{
    [Header("VFX Settings")]
    public Vector3 offset;
    public float timeToDestroyConfetti = 200f;
    bool hasInteracted = false; 


    [Header("Interaction Settings")]
    [SerializeField] float interactionRadius;
    [SerializeField] LayerMask playerMask;

    private void Update()
    {
        Interaction();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(transform.position, interactionRadius);
    }

    void Interaction()
    {

        Collider[] player = Physics.OverlapSphere(transform.position, interactionRadius, playerMask);

        foreach (var playerItem in player)
        {
            if (playerItem != null && !hasInteracted)
            {
                Debug.Log("Disparei o efeito");
                VFXManager.Instance.PlayVFXByTypeWithCollision(VFXManager.VFXType.CONFETTI,
                    this.transform.position, offset, null, true,
                    timeToDestroyConfetti);
                hasInteracted = true;
            }
        }
    }
}
