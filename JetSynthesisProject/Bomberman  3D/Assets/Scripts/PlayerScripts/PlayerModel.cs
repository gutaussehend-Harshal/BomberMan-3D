using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel
{
    public float movementSpeed { get; private set; }

    public PlayerModel(PlayerScriptableObject playerSO)
    {
        movementSpeed = playerSO.movementSpeed;
    }
}
