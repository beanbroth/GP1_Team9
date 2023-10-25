using UnityEngine;

[System.Serializable]
public class SO_PhaseInfo
{
    public int phaseNumber;
    public string phaseName;
    public Sprite phaseSprite;
    public int GetPhaseNumber()
    {
        return phaseNumber;
    }
    public string GetPhaseName()
    {
        return phaseName;
    }
    public Sprite GetPhaseIcon()
    {
        return phaseSprite;
    }
}
