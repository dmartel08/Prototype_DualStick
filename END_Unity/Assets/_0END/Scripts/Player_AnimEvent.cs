using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// If you want to access events from animation calls, a script (like this one)
/// MUST be attached to the object that uses the animation. In this case, 
/// the "SKELETON" object uses the Attack1h1_player (_player is the animation I tweaked)
/// so this script must be attched to the SKELETON. Any higher (PlayerGO for example) won't work.
/// </summary>
public class Player_AnimEvent : MonoBehaviour
{

    public bool hitting = false;

    public void HitStart()
    {
        Debug.Log("Hit Start was called");
        hitting = true;
    }

    public void HitEnd()
    {
        Debug.Log("Hit end was called");
        hitting = false;
    }
}
