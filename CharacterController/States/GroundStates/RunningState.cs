using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace StateMachine
{
    public class RunningState : GroundedState
    {
        public RunningState(Character character, StateMachine stateMachine) : base(character, stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();

            Vector2 runningDirection = new Vector2(character.Speed, 0);
            character.MovementDirection = runningDirection * character.Direction;
        }

        public override void StateUpdate()
        {
            base.StateUpdate();
            character.PlayerTransform.Translate(character.MovementDirection * Time.deltaTime);

            Collider2D[] hits = Physics2D.OverlapBoxAll(character.PlayerTransform.position, character.transform.localScale, 0, obstacleLayer);

            foreach (Collider2D hit in hits)
            {
                ColliderDistance2D colliderDistance = hit.Distance(character.PlayerCollider);

                if (colliderDistance.isOverlapped)
                {
                    if (character.IsBumpRightWall(obstacleLayer) || character.IsBumpLeftWall(obstacleLayer))
                    {
                        character.PlayerTransform.Translate(colliderDistance.pointA - colliderDistance.pointB);
                    }
                }
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}
