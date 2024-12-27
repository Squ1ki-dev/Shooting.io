using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMove : MonoBehaviour
    {
        [SerializeField] private PlayerStatsSO playerConfig;
        private CharacterController _characterController;
        private IInputService _inputService;
        private Camera _camera;

        private void Awake()
        {
            _inputService = AllServices.Container.Single<IInputService>();
            _characterController = GetComponent<CharacterController>();
        }

        private void Start() => _camera = Camera.main;

        private void Update()
        {
            Vector3 movementVector = Vector3.zero;

            if(_inputService.Axis.sqrMagnitude > Constants.Epsilon)
            {
                movementVector = _camera.transform.TransformDirection(_inputService.Axis);
                movementVector.y = 0;
                movementVector.Normalize();

                transform.forward = movementVector;
            }

            movementVector += Physics.gravity;
            
            _characterController.Move(playerConfig.Speed * movementVector * Time.deltaTime);
        }
    }
}