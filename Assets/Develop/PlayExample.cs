using UnityEngine;

public class PlayExample : MonoBehaviour
{
    [SerializeField] private Character _character;

    private Controller _characterController;

    private void Awake()
    {
        _character.Initiate();
    }
}
