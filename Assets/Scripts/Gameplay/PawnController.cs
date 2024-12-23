using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class PawnController : MonoBehaviour
{
    public UnityAction<PawnController> OnPawnDie;

    Team _team;

    [SerializeField] SkinnedMeshRenderer _head, _body;
    [SerializeField] TMP_Text _index;
    [SerializeField] Unit _unit;

    public Team Team { get => _team; }
    public Unit Unit { get => _unit; }

    public void Init(Team newTeam, int index)
    {
        _team = newTeam;

        _index.gameObject.SetActive(_team == PlayerManager.Instance.PlayerTeamColor);
        _index.text = "#" + index;

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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 10)
        {
            PawnController pawn = collision.gameObject.GetComponent<PawnController>();

            if (pawn.Team != Team)
            {
                pawn.Unit.StateMachine.SetState<UnitDieState>();
            }
        }
    }
}
