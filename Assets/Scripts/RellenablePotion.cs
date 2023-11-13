using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class RellenablePotion : MonoBehaviour
{
    [SerializeField] private GameObject _liquid;
    [SerializeField] private GameObject liquidCauldron;
    [SerializeField] private GameObject cork;
    [SerializeField] private GameObject innerCork;
    [SerializeField] private Task _task;
    [SerializeField] private float _fillTime = 2.0f;
    private Material _liquidMaterial;
    private bool filled = false;

    private void Awake()
    {
        Renderer rend = _liquid.GetComponent<Renderer>();
        _liquidMaterial = rend.material;
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if(other.gameObject == liquidCauldron)
        {
            LiquidMixer liquidMixer = liquidCauldron.GetComponent<LiquidMixer>();
            if (liquidMixer != null)
            {
                if(liquidMixer.VerifyCorrectMixed())
                {
                    Debug.Log("TAREA POCIONES COMPLETADA");
                    _task.TaskCompleted();
                    // _liquidMaterial.SetFloat("_fill", 0.7f);
                    
                    // Crea una secuencia de animación utilizando DOTween
                    Sequence mySequence = DOTween.Sequence();

                    // Agrega una animación que modifica la variable float del shader
                    mySequence.Append(_liquidMaterial.DOFloat(0.7f, "_fill", _fillTime));
                    filled = true;
                }
            }
        }
    }

    public void CorkPotion()
    {
        if (filled)
        {
            innerCork.gameObject.GetComponent<MeshRenderer>().enabled = true;
            cork.gameObject.SetActive(false);
        }
    }
}
