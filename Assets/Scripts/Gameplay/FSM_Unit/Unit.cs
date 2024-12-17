using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour
{
    public enum UnitAnimationName
    {
        None,
        Idle,
        Walk,
        Run,
        Die,
    }

    [SerializeField] Animator _animator;
    [SerializeField] NavMeshAgent _agent;
    [SerializeField] PawnController _pawnController;
    [SerializeField] CapsuleCollider _coll;

    FiniteStateMachine<Unit> _stateMachine;

    public NavMeshAgent Agent { get => _agent; }
    public PawnController PawnController { get => _pawnController; }
    public FiniteStateMachine<Unit> StateMachine { get => _stateMachine; }

    public void Init()
    {
        _stateMachine = new FiniteStateMachine<Unit>(this);

        _stateMachine.AddState(new UnitIdleState());
        _stateMachine.AddState(new UnitAttackUnitState());
        _stateMachine.AddState(new UnitAttackBaseState());
        _stateMachine.AddState(new UnitExpandState());
        _stateMachine.AddState(new UnitDieState());

        _stateMachine.SetState<UnitExpandState>();

        _agent.enabled = true;
        _coll.enabled = true;
    }

    private void Update()
    {
        _stateMachine.Update();
    }

    public void PlayAnimation(UnitAnimationName animationName)
    {
        _animator.Play(animationName.ToString());
    }

    public float GetAnimDuration(UnitAnimationName basicEnemyAnimationName)
    {
        AnimationClip[] clips = _animator.runtimeAnimatorController.animationClips;

        foreach (AnimationClip clip in clips)
        {
            if (clip.name == basicEnemyAnimationName.ToString())
            {
                return clip.length;
            }
        }

        return 0f;
    }

    public void SetVelocity(float newSpeed)
    {
        _agent.speed = newSpeed;
    }

    public void SetDestination(Vector3 newDestination)
    {
        _agent.SetDestination(newDestination);
    }

    public void ReleaseInPool()
    {
        _agent.enabled = false;
        _coll.enabled = false;

        StartCoroutine(ReleaseInPoolCor());
    }

    IEnumerator ReleaseInPoolCor()
    {
        yield return GetAnimDuration(UnitAnimationName.Die);

        yield return new WaitForSeconds(1f);

        PawnController.gameObject.SetActive(false);

        PoolManager.Instance.poolDictionary["Pawn"].Enqueue(PawnController.gameObject);
    }
}
