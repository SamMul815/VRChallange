using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonAttackTrigger : MonoBehaviour
{
    [SerializeField]
    private string _attackTag;
    public string AttackTag { get { return _attackTag; } }

    [SerializeField]
    private float _damage;
    public float Damage { get { return _damage; } }

    Collider[] _attackTriggerColliders;

    private void Awake()
    {
        _attackTriggerColliders = GetComponents<Collider>();
    }

    private void Start()
    {
        if (Application.isPlaying)
        {
            for (int i = 0; i < _attackTriggerColliders.Length; i++)
                _attackTriggerColliders[i].enabled = false;

            this.enabled = false;
        }
    }

    public void OnAttackTrigger()
    {
        for (int i = 0; i < _attackTriggerColliders.Length; i++)
            _attackTriggerColliders[i].enabled = true;
        this.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MainCamera")//MainCamera --> PlayerTag
        {
            UtilityManager.Instance.AttackPlayer(_damage);
            for (int i = 0; i < _attackTriggerColliders.Length; i++)
                _attackTriggerColliders[i].enabled = false;
            this.enabled = false;
        }

    }

}
