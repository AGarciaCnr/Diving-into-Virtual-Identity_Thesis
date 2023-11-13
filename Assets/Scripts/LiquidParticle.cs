using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidParticle : MonoBehaviour
{
    public LiquidParticleScriptableObject liquidParticleScriptableObject;

    [SerializeField] private Potion _potion;

    public bool FallingLiquid()
    {
        // Debug.Log("IsEmpty: " + _potion.IsEmpty() + "\t" + _potion.IsDump());
        return !_potion.IsEmpty() && _potion.IsDump();
    }

}
