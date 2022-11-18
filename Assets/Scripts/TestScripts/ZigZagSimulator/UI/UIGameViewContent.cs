using TMPro;
using UnityEngine;


public class UIGameViewContent : AbstractViewContent<SettingsObject>
{
    [SerializeField] private TextMeshProUGUI _scoresText;
    private int _scores = 0;

    public override void ResetContent()
    {
        _scores = 0;
        _scoresText.text = _scores.ToString();
    }
    
    public override void SetupContent(SettingsObject settings){}
}