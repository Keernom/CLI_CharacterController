using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace StateMachine
{
    public class Character : MonoBehaviour
    {
        [SerializeField] internal Vector2 MovementDirection; // Hide In Inspector
        internal StandingState Standing;
        internal RunningState Running;
        internal JumpingState Jumping;
        internal FallingState Falling;

        private StateMachine _movementSM;
        private BoxCollider2D _playerCollider;
        private Transform _playerTransform;

        private Vector2 _UpDownOverlapSize;
        private Vector2 _LeftRightOverlapSize;

        [Header("Layers Setting")]
        [SerializeField] LayerMask _obstacleLayer;

        [Header("Movement Settings")]
        [SerializeField] private float _speed;

        [Header("Air State Params")]
        [SerializeField] float _jumpCount;

        [Header("Debug")]
        [SerializeField] float _jumpsRemain;

        #region Properties

        public Transform PlayerTransform { get { return _playerTransform; } }
        public BoxCollider2D PlayerCollider { get { return _playerCollider; } }
        public StateMachine StateMachine { get { return _movementSM; } }

        public Vector2 Direction { get; set; }
        public LayerMask ObstacleLayer { get { return _obstacleLayer; } }
        
        public float JumpHeight { get; set; }
        public float Speed { get { return _speed; } }
        
        public float JumpsRemain
        {
            get { return _jumpsRemain; }
            set { _jumpsRemain = value; }
        }

        #endregion

        public void Start()
        {
            _playerCollider = GetComponent<BoxCollider2D>();
            _playerTransform = GetComponent<Transform>();

            _UpDownOverlapSize = new Vector2(_playerCollider.size.x * 0.75f, _playerCollider.size.y / 25);
            _LeftRightOverlapSize = new Vector2(_playerCollider.size.x / 25, _playerCollider.size.y * 0.75f);
            _jumpsRemain = _jumpCount;

            _movementSM = new StateMachine();

            Standing = new StandingState(this, _movementSM);
            Running = new RunningState(this, _movementSM);
            Jumping = new JumpingState(this, _movementSM);
            Falling = new FallingState(this, _movementSM);

            _movementSM.Initialize(Standing);
            
        }

        private void Update()
        {
            _movementSM.CurrentState.LogicUpdate();
        }

        private void FixedUpdate()
        {
            Debug.Log(_movementSM.CurrentState);
            _movementSM.CurrentState.StateUpdate();
        }

        public void DebugMessage(string message)
        {
            Debug.Log(message);
        }

        public void RestoreJumps()
        {
            _jumpsRemain = _jumpCount;
        }

        #region MovementTriggers

        public bool DownSideCheck(LayerMask layer)
        {
            Vector2 position = new Vector2(transform.position.x, transform.position.y - transform.localScale.y / 2);
            return Physics2D.OverlapBoxAll(position, _UpDownOverlapSize, 0, layer).Length > 0;
        }

        public bool UpSideCheck(LayerMask layer)
        {
            Vector2 position = new Vector2(transform.position.x, transform.position.y + transform.localScale.y / 2);
            return Physics2D.OverlapBoxAll(position, _UpDownOverlapSize, 0, layer).Length > 0;
        }

        public bool LeftSideCheck(LayerMask layer)
        {
            Vector2 position = new Vector2(transform.position.x - transform.localScale.x / 2, transform.position.y);
            return Physics2D.OverlapBoxAll(position, _LeftRightOverlapSize, 0, layer).Length > 0;
        }

        public bool RightSideCheck(LayerMask layer)
        {
            Vector2 position = new Vector2(transform.position.x + transform.localScale.x / 2, transform.position.y);
            return Physics2D.OverlapBoxAll(position, _LeftRightOverlapSize, 0, layer).Length > 0;
        }

        public bool IsBumpRightWall(LayerMask layer)
        {
            return MovementDirection.x > 0 && RightSideCheck(layer);
        }

        public bool IsBumpLeftWall(LayerMask layer)
        {
            return MovementDirection.x < 0 && LeftSideCheck(layer);
        }

        public bool IsBumpUpWall(LayerMask layer)
        {
            return MovementDirection.y > 0 && UpSideCheck(layer);
        }

        public bool IsBumpDownWall(LayerMask layer)
        {
            return MovementDirection.y < 0 && DownSideCheck(layer);
        }

        #endregion

        private void OnDrawGizmos()
        {
            //Downside overlap drawing
            Vector2 downPosition = new Vector2(transform.position.x, transform.position.y - transform.localScale.y / 2);
            Gizmos.DrawCube(downPosition, _UpDownOverlapSize);

            //Upside overlap drawing
            Vector2 upPosition = new Vector2(transform.position.x, transform.position.y + transform.localScale.y / 2);
            Gizmos.DrawCube(upPosition, _UpDownOverlapSize);

            //Leftside overlap drawing
            Vector2 leftPosition = new Vector2(transform.position.x - transform.localScale.x / 2, transform.position.y);
            Gizmos.DrawCube(leftPosition, _LeftRightOverlapSize);

            //Rightside overlap drawing
            Vector2 rightPosition = new Vector2(transform.position.x + transform.localScale.x / 2, transform.position.y);
            Gizmos.DrawCube(rightPosition, _LeftRightOverlapSize);
        }
    }
}
