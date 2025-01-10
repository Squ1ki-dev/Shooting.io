using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Player
{
    public class PlayerSkin : MonoBehaviour
    {
        [SerializeField] private PlayerStatsSO _playerConfig;
        [SerializeField] private List<GameObject> _skin = new List<GameObject>();

        private void Start()
        {
            int selectedSkinID = _playerConfig.SelectedSkinID;

            for (int i = 0; i < _skin.Count; i++)
            {
                bool isActive = _playerConfig.PlayerSkins[i].ID == selectedSkinID;
                _skin[i].SetActive(isActive);
            }
        }
    }
}