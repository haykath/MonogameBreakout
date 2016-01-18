using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonogameLibrary.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonogameBreakout
{
    public class PlayerController : GameComponent, IActor2DController
    {
        public Vector2 direction { get; private set; }
        public Vector2 dPadDir;
        public Vector2 keyDir;
        public Vector2 stickDir;
        public float rotation { get; private set; }

        private InputHandler input;
        private float dPadRotation;
        private float stickRotation;
        private float keyRotation;

        public InputHandler InHandler { get { return input; } }

        public int playerIndex { private set; get; }

        public PlayerController(Game game, int playerIndex = 0)
            : base(game)
        {
            input = (InputHandler) game.Services.GetService<IInputHandler>();
            if (input == null)
            {
                input = new InputHandler(game);
                game.Components.Add(input);
            }

            this.playerIndex = playerIndex;

            this.direction = this.dPadDir = this.keyDir = this.stickDir = Vector2.Zero;
            this.rotation = this.stickRotation = this.dPadRotation = this.keyRotation = 0f;
        }

        public override void Update(GameTime gameTime)
        {
            HandleKeyboard();
            HandleGamePad();

            direction = keyDir + dPadDir + stickDir;
            if (direction.Length() > 0f)
            {
                direction.Normalize();
                rotation = (float) Math.Atan2(direction.Y, direction.X);
            }

            base.Update(gameTime);
        }

        private void HandleKeyboard()
        {

            keyDir = Vector2.Zero;
            keyRotation = 0f;

            if (input.GetKey(Keys.Up))
                keyDir -= Vector2.UnitY;
            if (input.GetKey(Keys.Down))
                keyDir += Vector2.UnitY;
            if (input.GetKey(Keys.Left))
                keyDir -= Vector2.UnitX;
            if (input.GetKey(Keys.Right))
                keyDir += Vector2.UnitX;

            if (keyDir.Length() > 0f)
            {
                keyDir.Normalize();
                keyRotation = (float) Math.Atan2(keyDir.Y, keyDir.X);
            }

        }

        private void HandleGamePad()
        {
            HandleDPad();
            HandleLThumbstick();
        }

        private void HandleDPad()
        {
            dPadDir = Vector2.Zero;
            dPadRotation = 0f;

            if (input.GetButton(playerIndex, Buttons.DPadUp))
                dPadDir -= Vector2.UnitY;
            if (input.GetButton(playerIndex, Buttons.DPadDown))
                dPadDir += Vector2.UnitY;
            if (input.GetButton(playerIndex, Buttons.DPadLeft))
                dPadDir -= Vector2.UnitX;
            if (input.GetButton(playerIndex, Buttons.DPadRight))
                dPadDir += Vector2.UnitX;

            if (dPadDir.Length() > 0f)
            {
                dPadDir.Normalize();
                dPadRotation = (float)Math.Atan2(dPadDir.Y, dPadDir.X);
            }
        }

        private void HandleLThumbstick()
        {
            stickDir = input.GetStick(playerIndex, GamePadHandler.GamePadSticks.Left);
            if (stickDir.Length() > 0f)
            {
                stickDir.Normalize();
                stickRotation = (float)Math.Atan2(stickDir.Y, stickDir.X);
            }
        }
    }
}
