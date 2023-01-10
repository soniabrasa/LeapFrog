using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogCs : MonoBehaviour
{
    public static FrogCs instance;

    bool jumping;

    Vector3 velocity, toPoint, stopPosition;
    Animator Animator;

    float speed;

    void Awake() { instance = this; }

    void Start()
    {
        speed = 5f;
        // toPoint = transform.position;
    }

    void Update()
    {
        if ( jumping )
        {
            Vector3 diagonalDirection = toPoint;

            // Su velocidad de desplazamiento seguirá siendo la misma
            // Aplicando la misma velocidad al vector normalizado
            velocity = diagonalDirection * speed;

            // Dentro de este método Time.deltaTime = Time.fixedDeltaTime = 0.02
            Vector3 movement = velocity * Time.deltaTime;
            transform.position += movement;

            float dif = Mathf.Abs( transform.position.x - stopPosition.x );

            if ( dif < 0.1 )
            {
                jumping = false;
            }
        }

        else {
            // Stop
            velocity = Vector3.zero;
        }
    }

    public void SetMovement ( bool motion )
    {
        jumping = motion;
    }

    public Vector3 GetPosition () { return transform.position; }

    public void SetPosition ( Vector3 newPosition )
    {
        transform.position = newPosition;
    }

    public void DiagonalDirection ( Vector3 newPosition )
    {
        stopPosition = newPosition;

        // La resta de vectores obtiene un vector que va
        // desde la posición actual a la posición de destino
        Vector3 diagonalDirection = newPosition - transform.position;

        // Normalizando la magnitud a 1
        diagonalDirection.Normalize();

        toPoint = diagonalDirection;
    }
}
