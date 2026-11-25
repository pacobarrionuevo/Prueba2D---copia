using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    protected override void Die()
    {
        Debug.Log("Enemy se ha muerto");
    }
}
