using UnityEngine;

public class ParticlePainter : MonoBehaviour
{
    public RenderTexture paintTexture; // A textura onde a tinta será aplicada
    public Material paintMaterial; // O material do chão
    public LayerMask groundLayer; // Layer do chão

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;

        // Criar uma nova RenderTexture limpa
        RenderTexture rt = new RenderTexture(paintTexture.width, paintTexture.height, 0, RenderTextureFormat.ARGB32);
        Graphics.Blit(Texture2D.blackTexture, rt);
        paintMaterial.SetTexture("_PaintTex", rt);
        paintTexture = rt;
    }

    private void OnParticleCollision(GameObject other)
    {
        if (((1 << other.layer) & groundLayer) != 0)
        {
            // Obtém o contato da colisão
            ContactPoint contact = other.GetComponent<Collision>().contacts[0];
            Vector3 worldPos = contact.point;

            // Converte para coordenadas de UV do chão
            Vector2 uv = GetUVFromWorldPosition(worldPos, other.transform);

            // Aplica a cor na textura
            ApplyPaint(uv);
        }
    }

    private Vector2 GetUVFromWorldPosition(Vector3 worldPos, Transform groundTransform)
    {
        // Converter a posição do mundo para a posição local do chão
        Vector3 localPos = groundTransform.InverseTransformPoint(worldPos);

        // Normaliza para UV (0 a 1)
        float uvX = Mathf.InverseLerp(-5f, 5f, localPos.x);
        float uvY = Mathf.InverseLerp(-5f, 5f, localPos.z);

        return new Vector2(uvX, uvY);
    }

    private void ApplyPaint(Vector2 uv)
    {
        Texture2D brushTexture = new Texture2D(16, 16);
        Color[] brushPixels = new Color[16 * 16];

        for (int i = 0; i < brushPixels.Length; i++)
            brushPixels[i] = new Color(1, 0, 0, 0.5f); // Vermelho semitransparente

        brushTexture.SetPixels(brushPixels);
        brushTexture.Apply();

        RenderTexture.active = paintTexture;
        Graphics.DrawTexture(new Rect(uv.x * paintTexture.width, uv.y * paintTexture.height, 16, 16), brushTexture);
        RenderTexture.active = null;
    }
}
