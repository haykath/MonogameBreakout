using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace MonogameLibrary.Utilities
{
    public class GamePadHandler
    {
        public enum GamePadSticks { Left, Right };

        private GamePadState[] gamePadsState;
        private GamePadState[] prevGamePadsState;

        public GamePadState[] GamePadStates { get { return gamePadsState; } }

        public GamePadHandler()
        {
            gamePadsState = new GamePadState[4];
            prevGamePadsState = new GamePadState[4];

            for (int i = 0; i < 4; i++)
            {
                gamePadsState[i] = GamePad.GetState((Microsoft.Xna.Framework.PlayerIndex)i);
            }
            gamePadsState.CopyTo(prevGamePadsState, 0);
        }

        public bool GetButtonDown(int playerIndex, Buttons button)
        {
            if (playerIndex < 0 || playerIndex > 3)
                throw new ArgumentException();

            return gamePadsState[playerIndex].IsButtonDown(button) && prevGamePadsState[playerIndex].IsButtonUp(button);
        }

        public bool GetButtonUp(int playerIndex, Buttons button)
        {
            if (playerIndex < 0 || playerIndex > 3)
                throw new ArgumentException();

            return gamePadsState[playerIndex].IsButtonUp(button) && prevGamePadsState[playerIndex].IsButtonDown(button);
        }

        public bool GetButton(int playerIndex, Buttons button)
        {
            if (playerIndex < 0 || playerIndex > 3)
                throw new ArgumentException();

            return gamePadsState[playerIndex].IsButtonDown(button);
        }

        public Vector2 GetStick(int playerIndex, GamePadSticks stick)
        {
            if (playerIndex < 0 || playerIndex > 3)
                throw new ArgumentException();

            switch (stick)
            {
                case GamePadSticks.Left:
                    return gamePadsState[playerIndex].ThumbSticks.Left;
                case GamePadSticks.Right:
                    return gamePadsState[playerIndex].ThumbSticks.Right;
                default:
                    throw new ArgumentException();
            }
            
        }

        public void Update()
        {
            gamePadsState.CopyTo(prevGamePadsState, 0);

            for (int i = 0; i < 4; i++)
            {
                gamePadsState[i] = GamePad.GetState((Microsoft.Xna.Framework.PlayerIndex)i);
            }
        }
    }
}
