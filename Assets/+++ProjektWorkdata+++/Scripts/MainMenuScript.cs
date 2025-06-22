using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    public void PressStart()
    {
        SceneManager.LoadScene("Gameplay");
    }
    
    public void PressQuit()
    {
        Application.Quit();
    }
}
