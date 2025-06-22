using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform pointA;              // Startpunkt
    public Transform pointB;              // Zielpunkt
    public float speed = 2f;              // Bewegungsgeschwindigkeit
    public float threshold = 0.01f;       // Abstand, ab dem das Ziel erreicht gilt

    private bool movingToB = true;        // Steuerung der Bewegungsrichtung

    void Update()
    {
        if (pointA == null || pointB == null)
            return;

        Vector3 target = movingToB ? pointB.position : pointA.position;

        // Plattform in Richtung Ziel bewegen
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        // Wenn nahe genug am Ziel, Richtung wechseln
        if (Vector3.Distance(transform.position, target) < threshold)
        {
            movingToB = !movingToB;
        }
    }
}