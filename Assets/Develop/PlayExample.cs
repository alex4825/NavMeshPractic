using UnityEngine;
using UnityEngine.AI;

public class PlayExample : MonoBehaviour
{
    [SerializeField] private Character _character;

    private void Awake()
    {
        _character.Initiate();
    }
}
