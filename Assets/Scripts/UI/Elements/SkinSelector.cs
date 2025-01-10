using System.Collections.Generic;
using UnityEngine;
using CodeBase.Player;
using UnityEngine.UI;

namespace CodeBase.UI.Elements
{
    public class SkinSelector : MonoBehaviour
    {
        private int _selectedSkinID = 0;
        [SerializeField] private List<GameObject> _skins = new List<GameObject>();
        [SerializeField] private PlayerStatsSO _playerConfig;
        [SerializeField] private Button _nextBtn, _prevBtn;

        private void Awake()
        {
            _nextBtn.onClick.AddListener(NextSkin);
            _prevBtn.onClick.AddListener(PreviousSkin);
        }

        private void Start()
        {
            _selectedSkinID = PlayerPrefs.GetInt("SelectedSkinID", 0);
            UpdateSkins();
        }

        public void NextSkin()
        {
            _selectedSkinID++;
            if (!SkinExists(_selectedSkinID))
                _selectedSkinID = _playerConfig.PlayerSkins[0].ID;

            UpdateSkins();
        }

        public void PreviousSkin()
        {
            _selectedSkinID--;
            if (!SkinExists(_selectedSkinID))
                _selectedSkinID = _playerConfig.PlayerSkins[_playerConfig.PlayerSkins.Count - 1].ID;

            UpdateSkins();
        }

        private void UpdateSkins()
        {
            for (int i = 0; i < _skins.Count; i++)
            {
                bool isActive = _playerConfig.PlayerSkins[i].ID == _selectedSkinID;
                _skins[i].SetActive(isActive);
            }
            UpdateCharacterType();
            SaveSelectedSkin();
        }

        private void UpdateCharacterType() => _playerConfig.IsSwordsman = _selectedSkinID == 0;

        private void SaveSelectedSkin()
        {
            _playerConfig.SelectedSkinID = _selectedSkinID;
            PlayerPrefs.SetInt("SelectedSkinID", _selectedSkinID);
            PlayerPrefs.Save();
        }

        private bool SkinExists(int id) => _playerConfig.PlayerSkins.Exists(skin => skin.ID == id);
    }
}