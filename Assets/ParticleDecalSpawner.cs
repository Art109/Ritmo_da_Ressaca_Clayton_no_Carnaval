using UnityEngine;

public class ParticleDecalSpawner : MonoBehaviour
{
    public GameObject decalPrefab; // Arraste o prefab do decal aqui
    public LayerMask groundLayer; // Defina a layer do chão

    void OnParticleCollision(GameObject other)
    {
        if (((1 << other.layer) & groundLayer) != 0) // Se for chão
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit, 5f, groundLayer))
            {
                // Instancia o decal na posição da colisão
                GameObject decal = Instantiate(decalPrefab, hit.point, Quaternion.LookRotation(hit.normal));

                // Ajustar rotação para ficar alinhado ao chão
                decal.transform.Rotate(90, 0, 0);

                // Opcional: Destruir o decal após um tempo para evitar sobrecarga na cena
                Destroy(decal, 10f);
            }
        }
    }
}
