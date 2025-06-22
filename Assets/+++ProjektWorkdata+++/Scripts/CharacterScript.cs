using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections;

using TMPro; 



public class CharacterControllerSide : MonoBehaviour



{
    //alle funktionen werden einen namen oder einen wert gegeben um sie im rest vom Script benutzen zu können
    public TMP_Text countdownText;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpforce = 2f;
    private float direction = 0f;
    private Rigidbody2D rb;
// der character soll ab diesem punkt immer springen können, dafür brauchen wir den groundcheck
    [Header("Groundcheck")]
    [SerializeField] private Transform transformGroundCheck;
    [SerializeField] private LayerMask layerGround;

    [Header("Manager")] 
    [SerializeField] private CoinScript coinManager;
    [SerializeField] private UIManager uiManager;

    private bool canMove = false;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Starte den Countdown beim Spielstart
        StartCoroutine(StartCountdown());
    }
    //der character soll sich nichtmehr bewegen können solange disable movement aktiviert bleibt
    public void DisableMovement()
    {
        canMove = false;
    }
    //wenn die vorraussetzung vom coin maanager erfüllt wird dann soll PanelYouWOn im Uimanager aktiviert werden
    void Update()
    {
        if (coinManager.WinCondition())
        {
            uiManager.ShowPanelYouWon();
        }
        
        if (canMove)
            
        {
            //der character darf sich mit den gegebenen tasten um einen bestimmten wert in die entsprechende achse bewegen 
            direction = 0f;
            if (Keyboard.current.aKey.isPressed)
            {
                direction = -1;
            }

            if (Keyboard.current.dKey.isPressed)
            {
                direction = 1;
            }

            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                Jump();
            }

            rb.linearVelocity = new Vector2(direction * speed, rb.linearVelocity.y);
        }
        else
        {
            // Sperre Bewegung, während Countdown läuft
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        }
    }

    void Jump()
    {
        //mit physic wird hier berechnet wie der character springen soll
        if (Physics2D.OverlapCircle(transformGroundCheck.position, 0.1f, layerGround))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpforce);
        }
    }
            // Das ist ein Timer der runterzählen soll, wenn bei 0 kommt der Text "GO" und der Character kann sich dann bewegen
    private IEnumerator StartCountdown()
    {
        int count = 3;

        while (count > 0)
        {
            //per
            countdownText.text = count.ToString();
            yield return new WaitForSeconds(1f);
            count--;
        }
            //
        countdownText.text = "GO!";
        yield return new WaitForSeconds(1f);
        countdownText.text = "";

        canMove = true;  // Bewegung freigeben nach Countdown
    }
    
//alles was sich berühren soll braucht einen collider2d
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            //wenn eine Münze aufgesammelt wird, dann wird das im CoinManager hinterelegt 
            Debug.Log("Es war eine Münze");
            Destroy(other.gameObject);
            for (int i = 0; i < 1; i++)
            {
                coinManager.AddCoin();
            }
        }
        // wenn wir ein Objekt mit diesem Tag berühren dann passiert folgendes
        //Wir greifen zum UiManager rüber und per funktion soll er dort den PanelYouLost angeben. Außerdem können wir uns nichtmehr bewegen
        else if (other.CompareTag("Wall"))
        {
            uiManager.ShowPanelYouLost();
            canMove = false;
        }
        //wenn ein diamont aufgesammelt wird, dann bekomme ich 5 Punkte und der diamant wird zerstört. 
        else if (other.CompareTag("Diamond"))
        {
            Debug.Log("Es war ein Diamant");
            Destroy(other.gameObject);
            //hier kann man flexibelangeben wie viele punkte das jeweilige Objekt bekommen soll, der Loop "for" wird so lange wiederholt bis die angegebene Zahl erreicht wird.
            for (int i = 0; i < 5; i++)
            {
                coinManager.AddDiamond();
                
            }
        }
    }
}