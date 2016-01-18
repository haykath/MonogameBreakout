using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonogameBreakout.Breakout
{
    class BreakoutPaddle : Actor2D
    {
        BreakoutBall ball;
        PlayerController playerController { get { return (PlayerController)controller; } }
        bool disabled;

        public override Rectangle Bounds
        {
            get
            {
                Rectangle r = base.Bounds;
                r.Height = 1;
                return r;
            }
        }

        public BreakoutPaddle(Game game, string spriteName, PlayerController controller, BreakoutBall ball, float speed = 0f, float acceleration = 0f, float friction = 0f)
            : base(game, spriteName, controller, speed, acceleration, friction)
        {
            this.lockedY = true;
            this.ball = ball;
            this.disabled = false;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (ball.Controller.direction == Vector2.Zero && playerController.InHandler.GetKeyDown(Keys.Space))
            {
                if(!Game.Services.GetService<PlayerManager>().gameOver && !disabled)
                    ball.Launch();
            }
        }

        public override void Move(Vector2 direction, float deltaTime)
        {
            base.Move(direction, deltaTime);
            this.transform.rotation = 0f;
        }

        public void Disable()
        {
            disabled = true;
        }
    }
}
