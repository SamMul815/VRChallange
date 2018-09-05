using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DragonController
{ 
    public class DragonAttack : MonoBehaviour
    {
        [SerializeField]
        private float _damage;
        public float Damage { get { return _damage; } }

        private void OnTriggerEnter(Collider other)
        {
            if (BlackBoard.Instance.IsGroundAttacking)
            {
                if (other.tag == "Player")
                {

                    Debug.Log("HitHitHIt");
                    //UtilityManager.Instance.AttackPlayer(_damage);
                }
            }
        }

    }

}