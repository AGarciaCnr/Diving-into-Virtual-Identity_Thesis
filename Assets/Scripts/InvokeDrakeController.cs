using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvokeDrakeController : MonoBehaviour
{
    [SerializeField] private List<GameObject> elementsInOrder;
    private int expectedElement = 0;
    
    public void TouchElement(GameObject element)
    {
        if (expectedElement >= elementsInOrder.Count) 
        {
            Debug.Log("Prueba superada");
            return; 
        }
        Debug.Log("Elemento tocado : " + element.name);
        if(element == null) { return; }
        if(!elementsInOrder.Contains(element)) 
        {
            Debug.Log("El objeto no es un elemento");
            return; 
        }


        if (elementsInOrder[expectedElement] != element)
        {
            Debug.Log("ELEMENTO EQUIVOCADO");
            ResetInvoke();
        }
        else
        {
            expectedElement++;
            if(expectedElement >= elementsInOrder.Count)
            {
                GameManager.instance.InvokeDrake();
            }
        }

    }

    private void ResetInvoke()
    {
        GameManager.instance.FailInTask();
        expectedElement = 0;
        foreach (GameObject element in elementsInOrder)
        {
            element.gameObject.GetComponent<OnTriggerEnterEvent>().lightImage.SetActive(false);
        }
    }
}
