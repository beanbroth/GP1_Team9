using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class S_QuarksUIController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI quarksText;

    private void OnEnable()
    {
        QuarkManager.OnQuarkCountChanged += OnQuarkCountChange;
        OnQuarkCountChange(QuarkManager.quarkCount);
    }

    private void Start()
    {
        OnQuarkCountChange(QuarkManager.quarkCount);
    }

    private void OnDestroy()
    {
        QuarkManager.OnQuarkCountChanged -= OnQuarkCountChange;
    }


    void OnQuarkCountChange(int quarkCount)
    {
        quarksText.text = $"{quarkCount} / {QuarkManager.upgradeCost} quarks";


    }
}
