using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MonogameLibrary.Utilities;

namespace MonogameBreakout
{
    public struct ScreenEdgeCollision
    {
        public bool top;
        public bool bottom;
        public bool left;
        public bool right;
    }

    public class Actor2D : CollideableDrawableSprite
    {
        public const float sleepThreshold = 0.05f;

        private float maxSpeed;
        private float moveAcceleration;
        private float moveFriction;
        public Vector2 velocity;

        protected IActor2DController controller;

        public ScreenEdgeCollision screenCollisions;

        private Vector2 minTransformPosition;
        private Vector2 maxTransformPosition;

        public float Speed
        {
            get
            {
                return maxSpeed;
            }
            set
            {
                if (value > 0f)
                    maxSpeed = value;
                else
                    maxSpeed = 0f;
            }
        }
        public float Acceleration
        {
            get
            {
                return moveAcceleration;
            }
            set
            {
                if (value > 0f)
                    moveAcceleration = value;
                else
                    moveAcceleration = 0f;
            }
        }
        public float Friction
        {
            get
            {
                return moveFriction;
            }
            set
            {
                if (value > 0f)
                    moveFriction = value;
                else
                    moveFriction = 0f;
            }
        }

        public IActor2DController Controller { get { return controller; } }

        public bool lockedX;
        public bool lockedY;

        public Actor2D(Game game, string spriteName, IActor2DController controller, float speed = 0f, float acceleration = 0f, float friction = 0f)
            : base(game, spriteName)
        {
            this.Speed = speed;
            this.Acceleration = acceleration;
            this.moveFriction = friction;

            this.controller = controller;

            lockedX = false;
            lockedY = false;

            this.screenCollisions = new ScreenEdgeCollision();
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            this.minTransformPosition = new Vector2(this.sprite.Width / 2f, this.sprite.Height / 2f);
            this.maxTransformPosition = new Vector2(Game.GraphicsDevice.Viewport.Width - this.sprite.Width / 2f, Game.GraphicsDevice.Viewport.Height - this.sprite.Height / 2f);
        }

        public override void Update(GameTime gameTime)
        {
            float deltaTime = gameTime.ElapsedGameTime.Milliseconds / 1000f;
            Move(controller.direction, deltaTime);

            base.Update(gameTime);
        }

        public virtual void Move(Vector2 direction, float deltaTime)
        {
            if (direction.Length() > 0)
            {

                if (moveAcceleration > 0f)
                {
                    velocity += moveAcceleration * direction * deltaTime;
                }
                else
                    velocity = maxSpeed * direction;

                if (lockedX)
                    velocity.X = 0f;
                if (lockedY)
                    velocity.Y = 0f;

                this.transform.rotation = (float)Math.Atan2(velocity.Y, velocity.X);
            }
            else
            {
                if (velocity.Length() > sleepThreshold && moveFriction > 0f)
                {
                    Vector2 normalizedVelocity = velocity;
                    if (normalizedVelocity.Length() > 1f)
                        normalizedVelocity.Normalize();
                    velocity -= moveFriction * normalizedVelocity * deltaTime;
                }
                else
                    velocity = Vector2.Zero;
            }
            if (velocity.Length() > maxSpeed)
                velocity = Vector2.Normalize(velocity) * maxSpeed;
            this.transform.Translate(velocity * deltaTime);

            ScreenEdgeCheck();
        }

        protected void ScreenEdgeCheck()
        {
            screenCollisions.top = screenCollisions.bottom = screenCollisions.left = screenCollisions.right = false;

            if (transform.position.X >= Game.GraphicsDevice.Viewport.Width - this.sprite.Width / 2f)
                screenCollisions.right = true;
            else if (transform.position.X <= this.sprite.Width / 2f)
                screenCollisions.left = true;

            if (transform.position.Y >= Game.GraphicsDevice.Viewport.Height - this.sprite.Height / 2f)
                screenCollisions.bottom = true;
            else if (transform.position.Y <= this.sprite.Height / 2f)
                screenCollisions.top = true;

            this.transform.position = Vector2.Clamp(this.transform.position, this.minTransformPosition, this.maxTransformPosition);
        }

    }
}
