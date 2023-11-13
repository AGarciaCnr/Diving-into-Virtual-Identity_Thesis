using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

public class LiquidMixer : MonoBehaviour
{
    [System.Serializable]
    private struct Liquid
    {
        public LiquidParticleScriptableObject liquidParticle;
        public float neededValue;
    }

    [System.Serializable]
    private struct LiquidColors
    {
        public Color color1;
        public Color color2;

    }

    [SerializeField] private Liquid[] liquids;
    private Dictionary<LiquidParticleScriptableObject, float> liquidCurrentValueDictionary;
    [field: SerializeField] public UnityEvent WrongMixedEvent { get; private set; }
    [field: SerializeField] public UnityEvent CorrectMixedEvent { get; private set; }

    [SerializeField] private LiquidColors goodMixedColor;
    [SerializeField] private LiquidColors badMixedColor;
    [SerializeField] private float timeChangeColor = 3.0f;

    private Material _material;
    private bool correctMixed = false;

    [SerializeField] private GameObject rellenablePotion;

    private void Awake()
    {
        liquidCurrentValueDictionary = new Dictionary<LiquidParticleScriptableObject, float>();
        foreach (Liquid liquid in liquids)
        {
            liquidCurrentValueDictionary[liquid.liquidParticle] = 0.0f;
        }
        _material = GetComponent<MeshRenderer>().material;
    }

    private void OnTriggerStay(Collider other)
    {
        if (correctMixed) { return; }

        // Debug.Log("Colision con " + other.gameObject.name);
        LiquidParticle particle = other.gameObject.GetComponent<LiquidParticle>();
        if(particle == null) { return; }
        if (!particle.FallingLiquid()) { return; }

        LiquidParticleScriptableObject scriptable = particle.liquidParticleScriptableObject;
        // AddParticle(scriptable);
        if(IsRightParticle(scriptable))
        {
            AddParticle(scriptable);
            Debug.Log("Particula correcta");
            if(VerifyCorrectMixed())
            {
                Debug.Log("MEZCLA CORRECTA");
                correctMixed = true;
                CorrectMixedEvent.Invoke();
                rellenablePotion.GetComponent<Outline>().enabled = true;
            }
        }
        else
        {
            Debug.Log("MEZCLA MALA");

            WrongMixedEvent.Invoke();
        }
            
        
    }

    private void AddParticle(LiquidParticleScriptableObject scriptable)
    {
        
        foreach (var liquid in liquids)
        {
            if (liquid.liquidParticle == scriptable)
            {
                liquidCurrentValueDictionary[liquid.liquidParticle] += Time.deltaTime;
                Debug.Log(liquidCurrentValueDictionary[liquid.liquidParticle]);
                break;
            }
        }
    }

    private bool IsRightParticle(LiquidParticleScriptableObject scriptable)
    {
        return liquidCurrentValueDictionary.ContainsKey(scriptable);
    }

    public bool VerifyCorrectMixed()
    {
        foreach(var liquid in liquids)
        {
            if (liquidCurrentValueDictionary[liquid.liquidParticle] < liquid.neededValue) return false;
        }
        return true;
    }

    public void ChangeColorBadMixed()
    {
        // Crea una secuencia de animación utilizando DOTween
        Sequence mySequence1 = DOTween.Sequence();

        // Agrega una animación que modifica la variable float del shader
        mySequence1.Append(_material.DOColor(badMixedColor.color1, "_Color1", timeChangeColor));

        // Crea una secuencia de animación utilizando DOTween
        Sequence mySequence2 = DOTween.Sequence();

        // Agrega una animación que modifica la variable float del shader
        mySequence2.Append(_material.DOColor(badMixedColor.color2, "_Color2", timeChangeColor));

        // _material.DOColor(badMixedColor.color2, "_Color2", 1.0f);
        // _material.SetColor("_Color1", badMixedColor.color1);
        // _material.SetColor("_Color2", badMixedColor.color2);
    }

    public void ChangeColorGoodMixed()
    {
        // Crea una secuencia de animación utilizando DOTween
        Sequence mySequence1 = DOTween.Sequence();

        // Agrega una animación que modifica la variable float del shader
        mySequence1.Append(_material.DOColor(goodMixedColor.color1, "_Color1", timeChangeColor));

        // Crea una secuencia de animación utilizando DOTween
        Sequence mySequence2 = DOTween.Sequence();

        // Agrega una animación que modifica la variable float del shader
        mySequence2.Append(_material.DOColor(goodMixedColor.color2, "_Color2", timeChangeColor));

        //_material.SetColor("_Color1", goodMixedColor.color1);
        //_material.SetColor("_Color2", goodMixedColor.color2);
    }
}
