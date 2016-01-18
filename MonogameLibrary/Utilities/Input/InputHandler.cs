using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace MonogameLibrary.Utilities
{
    public interface IInputHandler
    {
        //Properties
        GamePadHandler GPadHandler { get; }
        KeyboardHandler KeyHandler { get; }
        MouseState CurrentMouseState { get; }
        MouseState PreviousMouseState { get; }

        bool GetKeyDown(Keys key);
        bool GetKeyUp(Keys key);
        bool GetKey(Keys key);
        bool GetButtonDown(int playerIndex, Buttons button);
        bool GetButtonUp(int playerIndex, Buttons button);
        bool GetButton(int playerIndex, Buttons button);
        Vector2 GetStick(int playerIndex, GamePadHandler.GamePadSticks stick);
        bool WasPressed(int playerIndex, Buttons button, Keys key);

    }

    public class InputHandler : GameComponent, IInputHandler
    {
        public enum ButtonType { 
            A, 
            B, 
            Back, 
            LeftShoulder, 
            LeftStick, 
            RightShoulder, 
            RightStick, 
            Start, 
            X, 
            Y 
        }

        private bool allowsExiting;
        private GamePadHandler gamePadHandler;
        private KeyboardHandler keyboard;
        private MouseState mouseState;
        private MouseState prevMouseState;

        public InputHandler(Game game, bool allowsExiting = false)
            : base(game)
        {
            this.allowsExiting = allowsExiting;

            this.keyboard = new KeyboardHandler();
            this.gamePadHandler = new GamePadHandler();

            prevMouseState = Mouse.GetState();
            mouseState = prevMouseState;

            game.Services.AddService<IInputHandler>(this);                
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            keyboard.Update();
            gamePadHandler.Update();

            prevMouseState = mouseState;
            mouseState = Mouse.GetState();

            base.Update(gameTime);
        }

        #region IInputHandler Implementations

        public GamePadHandler GPadHandler
        {
            get { return gamePadHandler; }
        }

        public KeyboardHandler KeyHandler
        {
            get { return keyboard; }
        }

        public MouseState CurrentMouseState
        {
            get { return mouseState; }
        }

        public MouseState PreviousMouseState
        {
            get { return prevMouseState; }
        }

        public bool GetKeyDown(Keys key)
        {
            return keyboard.GetKeyDown(key);
        }
        public bool GetKeyUp(Keys key)
        {
            return keyboard.GetKeyUp(key);
        }

        public bool GetKey(Keys key)
        {
            return keyboard.GetKey(key);
        }

        public bool GetButtonDown(int playerIndex, Buttons button)
        {
            return gamePadHandler.GetButtonDown(playerIndex, button);
        }

        public bool GetButtonUp(int playerIndex, Buttons button)
        {
            return gamePadHandler.GetButtonUp(playerIndex, button);
        }

        public bool GetButton(int playerIndex, Buttons button)
        {
            return gamePadHandler.GetButton(playerIndex, button);
        }

        public Vector2 GetStick(int playerIndex, GamePadHandler.GamePadSticks stick)
        {
            return gamePadHandler.GetStick(playerIndex, stick);
        }

        public bool WasPressed(int playerIndex, Buttons button, Keys key)
        {
            return this.GetButtonDown(playerIndex, button) || this.GetKeyDown(key);
        }

        #endregion
    }
}
