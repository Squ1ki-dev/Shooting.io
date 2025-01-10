using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Player
{
    [CreateAssetMenu(fileName = "PlayerConfig")]
    public class PlayerStatsSO : ScriptableObject
    {
        public int Level;
        public int SelectedSkinID;
        public List<PlayerSkins> PlayerSkins;
        public float MaxHP = 100f;
        public float RegenerationSpeed;
        public float Speed;
        public float AttackRange;
        public float MagicalAttackRange;
        public float AttackSpeed;
        public float MagicalAttackSpeed;
        public float Damage;
        public float AttackCooldownDuration;
        public float PushStrength;
        public int AmountOfKnives;
        public float KnivesDamage;
        public bool IsSwordsman;
    }

    [System.Serializable]
    public class PlayerSkins
    {
        public int ID;
        public RuntimeAnimatorController runtimeAnimatorController;
    }
}