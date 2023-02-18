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
        public JumpingState(Character character, StateMachine stateMachine) : base(character, stateMachine)
        {
            
        }

        public override void Enter()
        {
            base.Enter();

            character.JumpsRemain--;
            float jumpHeight = character.JumpHeight;
            character.MovementDirection.y = Mathf.Sqrt(2 * jumpHeight * Mathf.Abs(Physics2D.gravity.y));
        }

        public override void StateUpdate()
        {
            base.StateUpdate();

            Collider2D[] hits = Physics2D.OverlapBoxAll(character.PlayerTransform.position, character.transform.localScale, 0, obstacleLayer);

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
                        character.PlayerTransform.Translate(colliderDistance.pointA - colliderDistance.pointB);
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
