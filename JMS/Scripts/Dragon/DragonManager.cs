using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DragonController
{
    [RequireComponent(typeof(DragonStat))]
    [RequireComponent(typeof(ObjectMovement))]
    [RequireComponent(typeof(Rigidbody))]
    public class DragonManager : Singleton<DragonManager> {

        [SerializeField]
        private BehaviorTree _dragonBehaviroTree;
        public BehaviorTree DragonBehaviroTree { get { return _dragonBehaviroTree; } }

        private static ObjectMovement _dragonMovement;
        public static ObjectMovement DragonMovement { get { return _dragonMovement; } }

        private static DragonStat _stat;
        public static DragonStat Stat { get { return _stat; } }

        private Rigidbody _dragonRigidBody;
        public Rigidbody DragonRigidBody { get { return _dragonRigidBody; } }

        private ActionTask _preserveActionTask;
        public ActionTask PreserveActionTask { set { _preserveActionTask = value; } get { return _preserveActionTask; } }

        IEnumerator _dragonAiCor;
        
        bool _isInit;

        private void Awake()
        {
            _stat = GetComponent<DragonStat>();
            _dragonMovement = GetComponent<ObjectMovement>();
            _dragonRigidBody = GetComponent<Rigidbody>();
            _dragonAiCor = StartDragonAI();

        }

        // Use this for initialization
        void Start ()
        {

            if (Application.isPlaying)
            {
                CoroutineManager.DoCoroutine(_dragonAiCor);
                _isInit = true;

            }
		
	    }



        public bool IsFindNode(MOVEMENTTYPE Type)
        {
            NodeManager NodesPath = DragonMovement.GetNodeManager(Type);

            if (!NodesPath.IsRotation)
            {
                if (NodesPath.IsStick)
                {
                    Vector3 forward = (transform.position - NodesPath.transform.position).normalized;

                    Vector3 changePos = new Vector3(transform.position.x, NodesPath.transform.position.y, transform.position.z);

                    NodesPath.transform.position = changePos;
                    NodesPath.transform.rotation = transform.rotation;

                    Debug.Log("NodePath Find");
                    return true;
                }
            }
            Debug.Log("NodePath No Find");
            return false;
        }

        public void Hit(float Damege)
        {
            Stat.SaveTakeDamage += Damege;
            Stat.HP -= Damege;
            Debug.Log("Dragon Hit : " + Damege);
        }
        
        IEnumerator StartDragonAI()
        {
            while (!_dragonBehaviroTree.Root.Run())
            {
                yield return CoroutineManager.FiexdUpdate;
            }
        }

    }
}