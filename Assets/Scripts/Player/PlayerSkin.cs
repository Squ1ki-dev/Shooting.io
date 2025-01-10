using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Player
{
    public class PlayerSkin : MonoBehaviour
    {
        [SerializeField] private PlayerStatsSO _playerConfig;
        [SerializeField] private List<GameObject> _swordsmanItems = new List<GameObject>();
        [SerializeField] private List<GameObject> _mageItems = new List<GameObject>();

        private void Start() => SetSkin();

        private void SetSkin()
        {
            bool isSwordsman = _playerConfig.IsSwordsman; // Boolean flag for character type selection

            ToggleItems(_swordsmanItems, isSwordsman);
            ToggleItems(_mageItems, !isSwordsman);
        }

        private void ToggleItems(List<GameObject> items, bool isActiveCharacterType)
        {
            for (int i = 0; i < items.Count; i++)
            {
                bool isActive = isActiveCharacterType;
                items[i].SetActive(isActive);
            }
        }
    }
}
