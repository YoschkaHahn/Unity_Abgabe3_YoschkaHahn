using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text textCoinCollector;
    [SerializeField] private GameObject panelYouLost;
    [SerializeField] private GameObject panelYouWon;
    [SerializeField] private Button buttonReloadLevel;

    void Start()
    {
        panelYouLost.SetActive(false);
        
        panelYouWon.SetActive(false);
    }

    public void ReloadLevel() //void = funktionen enabler
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //Was genau ist active Scene? GetActiveScene?
    }

    public void UpdateCoinText(int newcoinNumber)
    {
        textCoinCollector.text = newcoinNumber.ToString();
    }

    public void ShowPanelYouLost()
    {
        panelYouLost.SetActive(true);
    }

    public void ShowPanelYouWon()
    {
        panelYouWon.SetActive(true);
    }
    public void PressQuit()
    {
        Application.Quit(); 
    }
}