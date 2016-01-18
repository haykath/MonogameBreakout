using Microsoft.Xna.Framework;
using MonogameLibrary.Physics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonogameBreakout.Breakout
{

    class BreakoutBall : Actor2D
    {
        public BallController ballController { get { return (BallController)this.controller; } }
        PlayerManager manager;

        public BreakoutBall(Game game, string spriteName, BallController controller, float speed = 0f)
            : base(game, spriteName, controller, speed, 0f, 0f)
        {
        }

        public void Launch()
        {
            ballController.Launch();
        }


        public override void Update(GameTime gameTime)
        {
            ballController.flippedThisFrameX = false;
            ballController.flippedThisFrameY = false;
            base.Update(gameTime);

            if (this.screenCollisions.top)
                ballController.FlipY();
            if (this.screenCollisions.right || this.screenCollisions.left)
                ballController.FlipX();

            if (this.screenCollisions.bottom)
            {
                Game.Services.GetService<PlayerManager>().BallDeath();
            }
        }

        public override void Move(Vector2 direction, float deltaTime)
        {
            base.Move(direction, deltaTime);
            this.transform.rotation = 0f;
        }

        public override void OnCollisionEnter(CollisionInfo collision)
        {
            if (ballController.direction == Vector2.Zero)
                return;

            BreakoutPaddle paddle = collision.collidingObject as BreakoutPaddle;
            if (paddle != null)
            {
                Vector2 distance = transform.position - paddle.Transform.position;
                if (distance != Vector2.Zero) distance.Normalize();

                Vector2 ballDir = ballController.direction;
                ballDir.Y *= -1;
                if (Math.Sign(distance.X) != Math.Sign(ballDir.X))
                    ballDir.X *= -1;

                ballDir.X += paddle.Controller.direction.X * 0.7f;
                ballDir.Normalize();
                ballController.direction = ballDir;
            }
            IBlock block = collision.collidingObject as IBlock;
            if (block != null)
            {
                Vector2 distance = collision.collisionPoint - block.BlockSprite.Transform.position + new Vector2(block.BlockSprite.spriteData.Width/2f, block.BlockSprite.spriteData.Height/2f);
                float angle = (MathHelper.ToDegrees((float) Math.Atan2(-distance.Y, distance.X)) + 360) % 360;

                if (((angle >= 0 && angle < 45) || angle > 315) && Math.Sign(ballController.direction.X) == -1)
                    ballController.FlipX();
                else if ((angle > 135 && angle < 225) && Math.Sign(ballController.direction.X) == 1)
                    ballController.FlipX();
                else if ((angle >= 45 && angle <= 135) && Math.Sign(ballController.direction.Y) == 1)
                    ballController.FlipY();
                else if ((angle >= 225 && angle <= 315) && Math.Sign(ballController.direction.Y) == -1)
                    ballController.FlipY();
                else
                {
                    ballController.FlipY();
                    ballController.FlipX();
                }

                Console.WriteLine("Reflection angle:" + angle.ToString());
                    
            }

        }
    }
}
