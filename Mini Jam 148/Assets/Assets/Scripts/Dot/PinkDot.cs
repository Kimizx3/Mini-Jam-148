using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkDot : MonoBehaviour
{
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
}
