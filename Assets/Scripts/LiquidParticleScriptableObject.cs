using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/LiquidParticleScriptableObject", order = 1)]
public class LiquidParticleScriptableObject : ScriptableObject
{
    public Material particleMaterial;
}