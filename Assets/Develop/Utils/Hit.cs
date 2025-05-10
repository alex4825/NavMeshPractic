using UnityEngine;

public class Hit : MonoBehaviour
{
    [SerializeField] private Character _character;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) 
        {
            _character.TakeDamage(10);
        }
    }
}
