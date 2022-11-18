public interface ISetupable<SettingsObjectType>
{
    public void SetupContent(SettingsObjectType settings);
    public void ResetContent();
}
