using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{
    public Vector3 offset;
    public float timeToDestroyConfetti = 200f;

    private bool _hasInteract = false;

    private void OnTriggerEnter(Collider other)
    {
        var player = other.gameObject.GetComponent<Player>();

        if (player != null && !_hasInteract)
        {
            VFXManager.Instance.PlayVFXByTypeWithCollision(VFXManager.VFXType.CONFETTI,
                this.transform.position, offset, null, true, 
                timeToDestroyConfetti);
            _hasInteract = true;
        }
    }
}
