using UnityEngine;

namespace CodeBase.Player
{
    [CreateAssetMenu(fileName = "PlayerConfig")]
    public class PlayerStatsSO : ScriptableObject
    {
        public int Level;
        public float Speed;
        public float AttackRange;
        public float AttackSpeed;
        public float Damage;
        public float AttackCooldownDuration;
    }
}