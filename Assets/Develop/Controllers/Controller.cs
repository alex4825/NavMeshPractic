public abstract class Controller
{
    private bool _isEnabled;

    public virtual void Enable() => _isEnabled = true;

    public virtual void Disable() => _isEnabled = false;

    public void Update()
    {
        if (_isEnabled == false)
            return;

        UpdateLogic();
    }

    protected abstract void UpdateLogic();
}
