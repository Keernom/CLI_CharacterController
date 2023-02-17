using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace StateMachine
{
    public class JumpingState : AiredState
    {
        private float _jumpHeight;

        public JumpingState(Character character, StateMachine stateMachine) : base(character, stateMachine)
        {
            _jumpHeight = character.JumpHeight;
        }

        public override void Enter()
        {
            base.Enter();
            character.MovementDirection.y = Mathf.Sqrt(2 * _jumpHeight * Mathf.Abs(Physics2D.gravity.y));
        }

        public override void StateUpdate()
        {
            base.StateUpdate();

            Collider2D[] hits = Physics2D.OverlapBoxAll(character.PlayerTransform.position, character.PlayerCollider.size, 0, obstacleLayer);

            foreach (Collider2D hit in hits)
            {
                ColliderDistance2D colliderDistance = hit.Distance(character.PlayerCollider);

                if (colliderDistance.isOverlapped)
                {
                    if (character.IsBumpUpWall(obstacleLayer))
                    {
                        character.MovementDirection.y = 0;
                    }
                    else if (character.IsBumpRightWall(obstacleLayer) || character.IsBumpLeftWall(obstacleLayer))
                    {
                        character.MoveCharacter(colliderDistance.pointA - colliderDistance.pointB);
                    }
                }
            }
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (character.MovementDirection.y < 0)
            {
                stateMachine.ChangeState(character.Falling);
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}
