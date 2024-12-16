using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnController : MonoBehaviour
{
    Team _team;

    [SerializeField] SkinnedMeshRenderer _head, _body;
    [SerializeField] Unit _unit;

    public Team Team { get => _team; }

    public void Init(Team newTeam)
    {
        _team = newTeam;

        SetColor();

        _unit.Init();
    }

    private void Update()
    {
    }

    private void SetColor()
    {
        Material teamMaterial = ColorManager.Instance.GetMaterialByTeam(_team);

        _head.sharedMaterial = teamMaterial;
        _body.sharedMaterial = teamMaterial;
    }
}
