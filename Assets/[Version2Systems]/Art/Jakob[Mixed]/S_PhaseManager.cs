using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class S_PhaseManager : MonoBehaviour
{
    [SerializeField] GameObject phasesUICollectionObject;
    [Header("Phase Pop-up")]
    [SerializeField] GameObject phaseUIObject;
    [SerializeField] Image phaseIconUI;
    [SerializeField] TextMeshProUGUI phaseNameUI;
    [SerializeField] SO_PhaseInfo[] phases;
    [Header("Passive Card Selection")]
    [SerializeField] GameObject passiveCardsUIObject;
    [SerializeField] TextMeshProUGUI leftCardName;
    [SerializeField] TextMeshProUGUI rightCardName;

    private S_PlayerControls playerControls;
    private S_Health healthManager;
    private S_QuarkController quarkController;
    private S_WinTimer winTimer;
    private bool isUpgrading = false;
    private void OnEnable()
    {
        S_WinTimer.newPhase += NewPhase;
        playerControls.Enable();
    }
    private void OnDisable()
    {
        S_WinTimer.newPhase -= NewPhase;
        playerControls.Disable();
    }

    private void Awake()
    {
        healthManager = FindFirstObjectByType<S_Health>();
        quarkController = FindFirstObjectByType<S_QuarkController>();
        winTimer = FindFirstObjectByType<S_WinTimer>();
        playerControls = new S_PlayerControls();
        playerControls.Player.Turn.performed += context =>
        {
            if (isUpgrading)
            {
                float turnDirection = context.ReadValue<float>();
                if (turnDirection < 0)
                {
                    healthManager.AddHealth(1); //Hard coded left button, need to randomize cards
                }
                else if (turnDirection > 0)
                {
                    quarkController.AddPickupRange(3f); //Hard coded right button, need to randomize cards
                }
                AudioManager.Instance.PlaySound3D("CardSelect", transform.position);
                CloseCards();
            }
        };
    }
    private void Start()
    {
        phasesUICollectionObject.SetActive(false);
        phaseUIObject.SetActive(false);
        passiveCardsUIObject.SetActive(false);
    }
    private void NewPhase(int phase)
    {
        phasesUICollectionObject.SetActive(true);
        Time.timeScale = 0;
        phaseNameUI.text = phases[phase-1].GetPhaseName();
        phaseIconUI.sprite = phases[phase-1].GetPhaseIcon();
        phaseUIObject.SetActive(true);
        StartCoroutine(OpenCardSelect());
    }

    IEnumerator OpenCardSelect()
    {
        yield return new WaitForSecondsRealtime(2);
        phaseUIObject.SetActive(false); // Add animations out
        leftCardName.text = "+1 HEALTH"; //Hard coded left selection, need to randomize cards
        rightCardName.text = "+3 PICKUP RANGE"; //Hard coded right selection, need to randomize cards
        passiveCardsUIObject.SetActive(true); //Add animations in
        isUpgrading = true;
    }

    void CloseCards()
    {
        passiveCardsUIObject.SetActive(false); //Add animations out
        phasesUICollectionObject.SetActive(false);
        Time.timeScale = 1;
        isUpgrading = false;
    }

    public string GetCurrentPhaseString()
    {
        int curPhase = winTimer.currentPhase;
        return "LEVEL " + curPhase + " | " + phases[curPhase - 1].phaseName;
    }
}
