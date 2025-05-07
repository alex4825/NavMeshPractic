using UnityEngine;
using UnityEngine.AI;

public class PlayExample : MonoBehaviour
{
    [SerializeField] private Character _character;

    private Controller _characterController;

    private void Awake()
    {
        _character.Initiate();
        //_characterController = new ClickPointAgentController(_character.GetComponent<NavMeshAgent>());

        //_characterController.Enable();
    }
}
