using System.Collections.Generic;
using UnityEngine;
using CodeBase.Logic;
using System;

namespace CodeBase.Player
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimator : MonoBehaviour, IAnimatorStateReader
    {
        private static readonly int MoveHash = Animator.StringToHash("Walking");
        private static readonly int AttackHash = Animator.StringToHash("AttackNormal");
        private static readonly int HitHash = Animator.StringToHash("GetHit");
        private static readonly int DieHash = Animator.StringToHash("Die");

        private readonly int _idleStateHash = Animator.StringToHash("Idle");
        private readonly int _idleStateFullHash = Animator.StringToHash("Base Layer.Idle");
        private readonly int _attackStateHash = Animator.StringToHash("Attack Normal");
        private readonly int _walkingStateHash = Animator.StringToHash("Run");
        private readonly int _deathStateHash = Animator.StringToHash("Die");

        private int _currentSkinIndex;
        [SerializeField] private PlayerStatsSO _playerStatsSO;
        [SerializeField] private CharacterController CharacterController;

        private Animator _animator;
        public event Action<AnimatorState> StateEntered;
        public event Action<AnimatorState> StateExited;
        public RuntimeAnimatorController _swordController, _mageController;

        public AnimatorState State { get; private set; }

        private void Awake() => _animator = GetComponent<Animator>();

        private void Start()
        {
            _currentSkinIndex = PlayerPrefs.GetInt("SelectedSkinID");
            SetAnimatorControllerByID(_currentSkinIndex); 
        }

        private void Update() => _animator.SetFloat(MoveHash, CharacterController.velocity.magnitude, 0.1f, Time.deltaTime);

        public bool IsAttacking => State == AnimatorState.Attack;

        public void PlayHit() => _animator.SetTrigger(HitHash);
        
        public void PlayAttack() => _animator.SetTrigger(AttackHash);

        public void PlayDeath() => _animator.SetTrigger(DieHash);

        public void ResetToIdle() => _animator.Play(_idleStateHash, -1);

        public void EnteredState(int stateHash)
        {
            State = StateFor(stateHash);
            StateEntered?.Invoke(State);
        }

        public void ExitedState(int stateHash) => StateExited?.Invoke(StateFor(stateHash));

        private AnimatorState StateFor(int stateHash)
        {
            AnimatorState state;
            if (stateHash == _idleStateHash)
                state = AnimatorState.Idle;
            else if (stateHash == _attackStateHash)
                state = AnimatorState.Attack;
            else if (stateHash == _walkingStateHash)
                state = AnimatorState.Walking;
            else if (stateHash == _deathStateHash)
                state = AnimatorState.Died;
            else
                state = AnimatorState.Unknown;
            
            return state;
        }

        public void SetAnimatorControllerByID(int id)
        {
            var skin = _playerStatsSO.PlayerSkins.Find(s => s.ID == id);
            if (skin != null)
            {
                this.enabled = true;
                _animator.enabled = false;
                _animator.runtimeAnimatorController = skin.runtimeAnimatorController;
                _animator.enabled = true;
                _animator.Rebind();
                _animator.WriteDefaultValues();

                Debug.Log($"Animator Controller set to ID {id}: {skin.runtimeAnimatorController.name}");
            }
            else
            {
                Debug.LogWarning($"Animator Controller with ID {id} not found!");
            }
        }
    }
}
