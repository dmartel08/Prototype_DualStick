using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponClass : MonoBehaviour
{
    public struct WEAPON_MELEE
    {
        public string type;
        public Collider hitBox;
        public float power;
    }
    public struct WEAPON_RANGED
    {
        public string type;
        public float power;
        public float castRange;
        public int ammoCount;
        public Collider castPoint;
    }
}
