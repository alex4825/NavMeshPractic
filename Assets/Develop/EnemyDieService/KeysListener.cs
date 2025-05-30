using System;
using System.Collections;
using UnityEngine;

public class KeysListener : MonoBehaviour
{
    private bool _shooseSeveralKeys;

    private bool _oneKeySelected;
    private bool _twoKeySelected;
    private bool _threeKeySelected;

    private Coroutine _listenSeveralKeysProcess;

    public event Action<bool, bool, bool> KeysSelected;

    private void Update()
    {
        _shooseSeveralKeys = Input.GetKey(KeyCode.LeftShift);

        if (_shooseSeveralKeys && _listenSeveralKeysProcess == null)
            _listenSeveralKeysProcess = StartCoroutine(ListenSeveralKeys());

        if (_shooseSeveralKeys && _listenSeveralKeysProcess != null && IsAllAlphaKeysUp() && AtLeastOneKeySelected())
        {
            StopCoroutine(_listenSeveralKeysProcess);
            _listenSeveralKeysProcess = null;

            KeysSelected?.Invoke(_oneKeySelected, _twoKeySelected, _threeKeySelected);

            _oneKeySelected = false;
            _twoKeySelected = false;
            _threeKeySelected = false;
        }
    }

    private IEnumerator ListenSeveralKeys()
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                _oneKeySelected = true;

            if (Input.GetKeyDown(KeyCode.Alpha2))
                _twoKeySelected = true;

            if (Input.GetKeyDown(KeyCode.Alpha3))
                _threeKeySelected = true;

            yield return null;
        }
    }

    private bool IsAllAlphaKeysUp()
        => Input.GetKey(KeyCode.Alpha1) == false &&
        Input.GetKey(KeyCode.Alpha2) == false &&
        Input.GetKey(KeyCode.Alpha3) == false;

    private bool AtLeastOneKeySelected() => _oneKeySelected || _twoKeySelected || _threeKeySelected;
}
