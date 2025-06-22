using UnityEngine;

public class CoinScript : MonoBehaviour
{
    [SerializeField] public int coinNumber = 0;
    [SerializeField] private UIManager uIManager;
    
    void Start()
    {
        uIManager.UpdateCoinText(coinNumber);
    }
    public void AddCoin()
    {
        coinNumber++; 
        uIManager.UpdateCoinText(coinNumber);
    }
    public void AddDiamond()
    {
        coinNumber++; 
        uIManager.UpdateCoinText(coinNumber);
    }

    public bool WinCondition()
    {
        if (coinNumber >= 25)
        {
            return true;
        }
        else return false;
    }


}

