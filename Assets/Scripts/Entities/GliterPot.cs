using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GliterPot : MonoBehaviour, IInteractable
{
    bool hasInteracted = false;

    [Header("VFX Settings")]
    public Vector3 offset;
    public float timeToDestroyConfetti = 200f;


    AudioSource audioSource;
    [SerializeField]AudioClip clip;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
    }

    public void Interaction()
    {
        if (!hasInteracted)
        {
            hasInteracted = true;
            audioSource.Play();
            VFXManager.Instance.PlayVFXByTypeWithCollision(VFXManager.VFXType.CONFETTI,
                this.transform.position, offset, null, true,
                timeToDestroyConfetti);

            UIImageFillManager.Instance.UpdateGlitterImage(Player.instance.GliterAmount / 100);

            Destroy(gameObject, clip.length);
        }
    }

    
}
