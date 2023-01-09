using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogCs : MonoBehaviour
{
    public static FrogCs instance;

    float speed;
    Vector3 velocity;

    Animator Animator;

    void Awake() { instance = this; }

    void Start()
    {
        speed = 5f;
    }

    void Update()
    {

    }
}
