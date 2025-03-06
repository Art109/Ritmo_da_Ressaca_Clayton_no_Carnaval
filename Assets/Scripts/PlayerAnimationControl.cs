using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationControl : MonoBehaviour
{
    public void DisablePlayerControl()
    {
        Player.instance.CanControl = false;
    }

    public void EnablePlayerControl()
    {
        Player.instance.CanControl = true;
    }
}
