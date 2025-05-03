using UnityEngine;

public class PlayExample : MonoBehaviour
{
    [SerializeField] private CharacterController _playerController;
    [SerializeField] private float _playerMoveSpeed;
    [SerializeField] private float _playerRotationSpeed;

    private Player _player;

    private void Awake()
    {
        _player = new Player(
            new CameraForwardWASDInput(),
            new CharacterControllerMover(_playerController, _playerMoveSpeed),
            new TransformRotator(_playerController.transform, _playerRotationSpeed));
    }

    private void Update()
    {
        _player.UpdateLogic();
    }
}
