using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class DrawZone : MonoBehaviour
{
    public float areaSize = 10f;

    LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 5; // 4 coins + retour au premier point
        lineRenderer.loop = false;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;

        // Mettre à jour les positions pour dessiner un carré
        UpdateZone();
    }

    void UpdateZone()
    {
        float halfSize = areaSize / 2;
        Vector3[] corners = new Vector3[]
        {
            transform.position + new Vector3(-halfSize, 0, -halfSize), // Bas-gauche
            transform.position + new Vector3(-halfSize, 0, halfSize),  // Haut-gauche
            transform.position + new Vector3(halfSize, 0, halfSize),   // Haut-droite
            transform.position + new Vector3(halfSize, 0, -halfSize),  // Bas-droite
            transform.position + new Vector3(-halfSize, 0, -halfSize)  // Retour au premier point
        };

        lineRenderer.SetPositions(corners);
    }
}
