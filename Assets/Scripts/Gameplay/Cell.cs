using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    Team cellTeam = Team.None;
    [SerializeField] MeshRenderer _meshRenderer;

    public Team CellTeam { get => cellTeam; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 10)
        {
            PawnController controller = other.GetComponent<PawnController>();

            if (controller.Team == cellTeam) return;
            else
            {
                cellTeam = controller.Team;

                _meshRenderer.sharedMaterial = ColorManager.Instance.GetMaterialByTeam(cellTeam);

            }
        }
    }
}
