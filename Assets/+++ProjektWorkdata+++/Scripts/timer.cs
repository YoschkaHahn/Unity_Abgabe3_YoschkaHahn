using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private CharacterControllerSide characterController;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private float remainingTime = 60f;

    private UIManager uiManager;
    private bool timerEnded = false;

    void Start()
    {
        if (characterController == null)
        {
            Debug.LogError("CharacterControllerSide ist nicht zugewiesen!");
        }
        uiManager = GetComponent<UIManager>();

        if (uiManager == null)
        {
            Debug.LogError("UIManager-Komponente wurde nicht gefunden!");
        }
    }

    void Update()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
            remainingTime = Mathf.Max(remainingTime, 0f);
        }
        else if (!timerEnded)
        {
            timerEnded = true;
            uiManager?.ShowPanelYouLost();

            // Bewegung deaktivieren
            characterController.DisableMovement();
        }


        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}