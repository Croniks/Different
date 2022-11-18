using UnityEngine;

public abstract class AbstractViewContent<SettingsObjectType> : MonoBehaviour
{
    public abstract void ResetContent();
    public abstract void SetupContent(SettingsObjectType settings);
}