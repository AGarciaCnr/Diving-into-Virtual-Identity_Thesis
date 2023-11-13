//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.Android;

//public class Potion : MonoBehaviour
//{

//    [SerializeField] private GameObject _liquid;
//    [SerializeField] private GameObject waterfallLiquidVisualEffect;
//    private Material _liquidMaterial;
//    [Range(0,1)]
//    [SerializeField] public float _fill;
//    private float _initiaFill;
//    [SerializeField] private float _emptyingTime = 5;
//    [SerializeField] private AnimationCurve _dumpAngleFillCurve;
//    private float time = 0;
//    private Transform _transform;
//    private ParticleSystem.EmissionModule emission;



//    // Start is called before the first frame update
//    void Awake()
//    {
//        Renderer rend = _liquid.GetComponent<Renderer>();
//        _liquidMaterial = rend.material;
//        _liquidMaterial.SetFloat("_fill", _fill);
//        _transform = transform;
//        _initiaFill = _fill;
//        ParticleSystem ps = waterfallLiquidVisualEffect.GetComponent<ParticleSystem>();
//        emission = ps.emission;

//    }

//    // Update is called once per frame
//    void Update()
//    {
//        // Debug.Log(this._transform.rotation.eulerAngles);
//        _fill = _liquidMaterial.GetFloat("_fill");
//        if (!IsEmpty() && IsDump())
//        {
//            // waterfallLiquidVisualEffect.SetActive(true);
//            emission.enabled = true;
//            Empty();

//            if(_fill<=0.3f)
//            {
//                _fill = 0;
//                _liquidMaterial.SetFloat("_fill", 0.0f);
//            }
//        }
//        else
//        {
//            // waterfallLiquidVisualEffect.SetActive(false);
//            emission.enabled = false;

//        }


//    }

//    private void Empty()
//    {
//        time += Time.deltaTime;
//        float newFill = Mathf.Lerp(_initiaFill, 0, time / _emptyingTime);
//        // Debug.Log(newFill);
//        _liquidMaterial.SetFloat("_fill", newFill);              
//    }

//    public bool IsEmpty()
//    {
//        return _fill <= 0;
//    }

//    public bool IsDump()
//    {
//        Vector3 angles= new Vector3(_transform.rotation.eulerAngles.x, 0.0f, _transform.rotation.eulerAngles.z);
//        float currentAngle = Quaternion.Angle(Quaternion.Euler(angles), Quaternion.identity);
//        float dumpAngleCurrent = _dumpAngleFillCurve.Evaluate(_fill);
//        return currentAngle >= dumpAngleCurrent;
//    }
//}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class Potion : MonoBehaviour
{

    [SerializeField] private GameObject _liquid;
    [SerializeField] private GameObject waterfallLiquidVisualEffect;
    private Material _liquidMaterial;
    [Range(0, 1)]
    [SerializeField] public float _fill;
    private float _initiaFill;
    [SerializeField] private float _emptyingTime = 5;
    [SerializeField] private AnimationCurve _dumpAngleFillCurve;
    private float time = 0;
    private Transform _transform;
    private ParticleSystem.EmissionModule emission;



    // Start is called before the first frame update
    void Awake()
    {
        Renderer rend = _liquid.GetComponent<Renderer>();
        _liquidMaterial = rend.material;
        _liquidMaterial.SetFloat("_fill", _fill);
        _transform = transform;
        _initiaFill = _fill;
        ParticleSystem ps = waterfallLiquidVisualEffect.GetComponent<ParticleSystem>();
        emission = ps.emission;

    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(this._transform.rotation.eulerAngles);
        _fill = _liquidMaterial.GetFloat("_fill");
        if (!IsEmpty() && IsDump())
        {
            // waterfallLiquidVisualEffect.SetActive(true);
            emission.enabled = true;
            Empty();

            if (_fill <= 0.25f)
            {
                _fill = 0;
                _liquidMaterial.SetFloat("_fill", 0.0f);
            }
        }
        else
        {
            // waterfallLiquidVisualEffect.SetActive(false);
            emission.enabled = false;

        }


    }

    private void Empty()
    {
        time += Time.deltaTime;
        float newFill = Mathf.Lerp(_initiaFill, 0, time / _emptyingTime);
        // Debug.Log(newFill);
        _liquidMaterial.SetFloat("_fill", newFill);
    }

    public bool IsEmpty()
    {
        return _fill <= 0;
    }

    public bool IsDump()
    {
        Vector3 angles = new Vector3(_transform.rotation.eulerAngles.x, 0.0f, _transform.rotation.eulerAngles.z);
        float currentAngle = Quaternion.Angle(Quaternion.Euler(angles), Quaternion.identity);
        float dumpAngleCurrent = _dumpAngleFillCurve.Evaluate(_fill);
        return currentAngle >= dumpAngleCurrent;
    }
}