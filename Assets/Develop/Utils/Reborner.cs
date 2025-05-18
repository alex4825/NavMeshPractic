using UnityEngine;

public class Reborner : MonoBehaviour
{
    [SerializeField] private AgentCharacter _character;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && _character.IsAlive == false)
        {
            _character.Reborn();
        }
    }
}
