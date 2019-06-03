using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : InputController
{
    public override Vector2 GetVelocity()
    {
        return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    public override float GetTurn()
    {
        float turn = 0;

        if (Input.GetKey(KeyCode.E))
            turn++;
        if (Input.GetKey(KeyCode.Q))
            turn--;

        return turn;
    }
}
