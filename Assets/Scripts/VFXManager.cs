using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VFXManager : MonoBehaviour
{
    // All VFX
    public enum VFXType
    {
        WALK,
        CONFETTI
    }

    public List<VFXManagerSetup> vfxSetup;

    // Singleton
    public static VFXManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    #region PlayVFXByType
    public void PlayVFXByType(VFXType vfxType, Vector3 position,
        bool willDestroy = true, float timeToDestroy = 3f)
    {
        foreach (var vfx in vfxSetup)
        {
            if (vfx.vfxType == vfxType)
            {
                var item = Instantiate(vfx.prefab, null);
                item.transform.position = position;
                if (willDestroy)
                    Destroy(item.gameObject, timeToDestroy);
                break;
            }
        }
    }

    public void PlayVFXByType(VFXType vfxType, Vector3 position, Vector3 offset,
        bool willDestroy = true, float timeToDestroy = 3f)
    {
        foreach (var vfx in vfxSetup)
        {
            if (vfx.vfxType == vfxType)
            {
                var item = Instantiate(vfx.prefab, null);
                item.transform.position = position + offset;
                if (willDestroy)
                    Destroy(item.gameObject, timeToDestroy);
                break;
            }
        }
    }

    public void PlayVFXByType(VFXType vfxType, Vector3 position, Transform parent,
        bool willDestroy = true, float timeToDestroy = 3f)
    {
        foreach (var vfx in vfxSetup)
        {
            if (vfx.vfxType == vfxType)
            {
                var item = Instantiate(vfx.prefab, null);
                item.transform.position = position;
                if (willDestroy)
                    Destroy(item.gameObject, timeToDestroy);
                break;
            }
        }
    }

    public void PlayVFXByType(VFXType vfxType, Vector3 position, Vector3 offset,
        Transform parent, bool willDestroy = true, float timeToDestroy = 3f)
    {
        foreach (var vfx in vfxSetup)
        {
            if (vfx.vfxType == vfxType)
            {
                var item = Instantiate(vfx.prefab, parent);
                item.transform.position = position + offset;
                if (willDestroy)
                    Destroy(item.gameObject, timeToDestroy);
                break;
            }
        }
    }
    #endregion

    #region PlayVFBByTypeWithCollision
    public void PlayVFXByTypeWithCollision(VFXType vfxType, Vector3 position, Vector3 offset,
        Transform parent, bool willDestroy = true, 
        float timeToDestroy = 3f, List<Transform> planeTransforms = null)
    {
        foreach (var vfx in vfxSetup)
        {
            if (vfx.vfxType == vfxType)
            {
                var item = Instantiate(vfx.prefab, null);
                item.transform.position = position + offset;

                var particleSystem = item.GetComponent<ParticleSystem>();
                if (particleSystem != null)
                {
                    var collisionModule = particleSystem.collision;
                    if (planeTransforms != null)
                    {
                        for (int i = 0; i < planeTransforms.Count; i++)
                        {
                            collisionModule.SetPlane(i, planeTransforms[i]);
                        }
                    }
                }

                if (willDestroy)
                    Destroy(item.gameObject, timeToDestroy);
                break;
            }
        }
    }
    #endregion

    #region PlayPermanentVFX
    public ParticleSystem GetPermanentVFXByType(VFXType vfxType, Vector3 position, Vector3 offset,
        Transform parent)
    {
        foreach (var vfx in vfxSetup)
        {
            if (vfx.vfxType == vfxType)
            {
                var item = Instantiate(vfx.prefab, parent);
                item.transform.position = position + offset;

                var particleSystem = item.GetComponent<ParticleSystem>();
                return particleSystem;
            }
        }
        return null;
    }

    public void PlayPermanentVFXByType(VFXType vfxType, Vector3 position, Vector3 offset,
        Transform parent)
    {
        foreach (var vfx in vfxSetup)
        {
            if (vfx.vfxType == vfxType)
            {
                var item = Instantiate(vfx.prefab, parent);
                item.transform.position = position + offset;
            }
        }
    }
    #endregion
}

[System.Serializable]
public class VFXManagerSetup
{
    public VFXManager.VFXType vfxType;
    public GameObject prefab; // Particle System Prefab
}