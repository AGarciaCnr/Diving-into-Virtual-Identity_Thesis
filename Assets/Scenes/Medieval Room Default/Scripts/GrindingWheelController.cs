using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GrindingWheelController : MonoBehaviour
{
    public GameObject sword;
    [SerializeField] private GameObject _sparksEffect;
    private ParticleSystem.EmissionModule _sparksEmission;
    [SerializeField] private float _radious = 0.32f;
    [SerializeField] private float _collisionSize = 0.04f;
    [SerializeField] private AudioSource _audioSource;

    private float timer = 0;

    [SerializeField] private Task _task;
    public float timeToComplete = 5;
    [SerializeField] private bool drawGizmos = false;

    private void Awake()
    {
        ParticleSystem ps = _sparksEffect.GetComponent<ParticleSystem>();
        _sparksEmission = ps.emission;
    }


    private void OnTriggerStay(Collider other)
    {
        Debug.Log(other.name);
        if (other.gameObject.tag == sword.tag)
        {
            Vector3 contact = other.ClosestPoint(transform.position);
            Vector3 wheelSwordVector = contact - transform.position;
            float distanceCollision = wheelSwordVector.magnitude;
            if (!((distanceCollision > _radious - _collisionSize) && (distanceCollision < _radious))) 
            { 
                DisableEffects();  
                return; 
            }

            timer += Time.deltaTime;
            if (other.gameObject.TryGetComponent<SwordController>(out var swordController))
            {
                swordController.VibrateController(0.1f, 0.5f);
            }
            RotateObject();
            if (timer >= timeToComplete) { _task.TaskCompleted(); }

            // SPARKS EFFECT
            wheelSwordVector = wheelSwordVector.normalized;

            Vector3 sparksPosition = transform.position + wheelSwordVector * _radious;
            Vector3 forwardVector = new Vector3(wheelSwordVector.y, -wheelSwordVector.x, 0);
            _sparksEffect.transform.LookAt(sparksPosition + forwardVector, wheelSwordVector);
            _sparksEffect.transform.position = sparksPosition;

            _sparksEmission.enabled = true;

            if(!_audioSource.isPlaying)
            {
                _audioSource.Play();
            }
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == sword.tag)
        {
            DisableEffects();
        }
    }


    private void RotateObject()
    {
        transform.Rotate(Vector3.down * 20 * Time.deltaTime);
    }

    void OnDrawGizmos()
    {
        if (!drawGizmos) { return; }
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, _radious);
        Gizmos.DrawSphere(transform.position, _radious - _collisionSize);
    }

    private void DisableEffects()
    {
        _sparksEmission.enabled = false;
        _audioSource.Stop();
    }
}
