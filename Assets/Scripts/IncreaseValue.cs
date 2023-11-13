using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseValue : MonoBehaviour
{
    [SerializeField] private string propertyName;
    [SerializeField] private float targetValue;
    [SerializeField] private float duration;
    [SerializeField] private AnimationCurve curve;

    private Material material;

    private void Awake()
    {
        material = GetComponent<Renderer>().material;
    }


    // Start is called before the first frame update
    void OnEnable()
    {
        // Crea una secuencia de animación utilizando DOTween
        Sequence mySequence = DOTween.Sequence();

        // Agrega una animación que modifica la variable float del shader
        mySequence.Append(material.DOFloat(targetValue, propertyName, duration).SetEase(curve));

        //float value = material.GetFloat(propertyName);
        //Debug.Log("VALOR: " + value);
        //material.SetFloat(propertyName, targetValue);
        //value = material.GetFloat(propertyName);
        //Debug.Log("VALOR: " + value);
    }

    private void OnDisable()
    {
        // Crea una secuencia de animación utilizando DOTween
        Sequence mySequence = DOTween.Sequence();

        // Agrega una animación que modifica la variable float del shader
        mySequence.Append(material.DOFloat(0.0f, propertyName, duration));
    }
}
