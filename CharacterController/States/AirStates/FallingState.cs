using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace StateMachine
{
    public class FallingState : AiredState
    {
        public FallingState(Character character, StateMachine stateMachine) : base(character, stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
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
                    else if (character.IsBumpDownWall(obstacleLayer))
                    {
                        character.PlayerTransform.Translate(colliderDistance.pointA - colliderDistance.pointB);

                        if (character.MovementDirection.x != 0)
                            stateMachine.ChangeState(character.Running);
                        else
                            stateMachine.ChangeState(character.Standing);
                    }
                    else if (character.IsBumpRightWall(obstacleLayer) || character.IsBumpLeftWall(obstacleLayer))
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
