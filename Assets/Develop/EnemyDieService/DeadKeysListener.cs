using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DeadKeysListener : MonoBehaviour
{
    [SerializeField] private List<KeyDeadTypeWrapper> _keysToDeadTypes;

    private bool _shooseSeveralKeys;

    private Coroutine _listenSeveralKeysProcess;

    public event Action<DeadTypes[]> KeysSelected;

    private void Update()
    {
        _shooseSeveralKeys = Input.GetKey(KeyCode.LeftShift);

        if (_shooseSeveralKeys && _listenSeveralKeysProcess == null)
            _listenSeveralKeysProcess = StartCoroutine(ListenSeveralKeys());

        if (_shooseSeveralKeys && _listenSeveralKeysProcess != null && IsAllAlphaKeysUp() && AtLeastOneKeySelected())
        {
            StopCoroutine(_listenSeveralKeysProcess);
            _listenSeveralKeysProcess = null;

            KeysSelected?.Invoke(_keysToDeadTypes.Where(keyDead => keyDead.IsActive).Select((keyDead) => keyDead.DeadType).ToArray());

            foreach (var keyDead in _keysToDeadTypes)
                keyDead.IsActive = false;
        }
    }

    private IEnumerator ListenSeveralKeys()
    {
        while (true)
        {
            foreach (var keyDead in _keysToDeadTypes)
                if (Input.GetKeyDown(keyDead.AlphaKey))
                    keyDead.IsActive = true;

            yield return null;
        }
    }

    private bool IsAllAlphaKeysUp()
    {
        bool isSomePressed = false;

        foreach (var keyDead in _keysToDeadTypes)
        {
            isSomePressed = Input.GetKey(keyDead.AlphaKey);

            if (isSomePressed)
                return false;
        }

        return true;
    }

    private bool AtLeastOneKeySelected()
    {
        foreach (var keyDead in _keysToDeadTypes)
            if (keyDead.IsActive)
                return true;

        return false;
    }

    [Serializable]
    private class KeyDeadTypeWrapper
    {
        public DeadTypes DeadType;
        public KeyCode AlphaKey;

        [HideInInspector]
        public bool IsActive;
    }
}
