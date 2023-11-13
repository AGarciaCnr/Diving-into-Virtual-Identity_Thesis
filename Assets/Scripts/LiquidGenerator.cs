using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidGenerator : MonoBehaviour
{
    public bool autoUpdate;

    //[Header("Particle properties")]
    [SerializeField] private GameObject _particlePrefab;
    [SerializeField] private float _particleDiameter = 0.3f; // diameter
    [SerializeField] private LiquidParticleScriptableObject _liquidParticle;

    [SerializeField] private Vector3 _dimensionsLiquid = Vector3.one;
    public List<GameObject> liquidParticles;


    // Start is called before the first frame update
    void Start()
    {
        // Generate();   
    }

    public void Generate()
    {
        if(_liquidParticle == null) { return; }
       
        float _particleRadius = _particleDiameter / 2;
        Transform _transform = transform;
        for (float x = _particleRadius; x < _dimensionsLiquid.x - _particleRadius; x += _particleDiameter)
        {
            for (float z = _particleRadius; z < _dimensionsLiquid.z - _particleRadius; z += _particleDiameter)
            {
                for (float y = _particleRadius; y < _dimensionsLiquid.y - _particleRadius; y += _particleDiameter)
                {

                    GameObject newSphere = Instantiate(_particlePrefab,
                       _transform.position + new Vector3(x, y, z) - _dimensionsLiquid / 2,
                       Quaternion.identity, _transform);
                    newSphere.transform.localScale = new Vector3(_particleDiameter, _particleDiameter, _particleDiameter);
                    newSphere.GetComponent<LiquidParticle>().liquidParticleScriptableObject = _liquidParticle;
                    newSphere.GetComponentInChildren<MeshRenderer>().material = _liquidParticle.particleMaterial;

                    liquidParticles.Add(newSphere);
                }
            }

        }
    }

    public void Clear()
    {
        foreach(GameObject sphere in liquidParticles)
        {
            DestroyImmediate(sphere);
        }
        liquidParticles.Clear();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, _dimensionsLiquid);
        // Gizmos.DrawSphere(transform.position - _dimensionsLiquid / 2, 0.05f);
    }
}
