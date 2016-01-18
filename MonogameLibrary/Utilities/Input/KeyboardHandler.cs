using Microsoft.Xna.Framework.Input;

namespace MonogameLibrary.Utilities
{
    public class KeyboardHandler
    {
        private KeyboardState keyboardState;
        private KeyboardState prevKeyboardState;

        public KeyboardHandler() {
            this.keyboardState = Keyboard.GetState();
            this.prevKeyboardState = keyboardState;
        }

        public bool GetKeyDown(Keys key)
        {
            return keyboardState.IsKeyDown(key) && prevKeyboardState.IsKeyUp(key);
        }

        public bool GetKeyUp(Keys key)
        {
            return keyboardState.IsKeyUp(key) && prevKeyboardState.IsKeyDown(key);
        }

        public bool GetKeyHold(Keys key)
        {
            return keyboardState.IsKeyDown(key) && prevKeyboardState.IsKeyDown(key);
        }

        public bool WasAnyKeyPressed()
        {
            foreach (Keys key in keyboardState.GetPressedKeys())
            {
                if (prevKeyboardState.IsKeyUp(key))
                    return true;
            }

            return false;
        }

        public bool GetKey(Keys key)
        {
            return keyboardState.IsKeyDown(key);
        }

        public void Update()
        {
            prevKeyboardState = keyboardState;
            keyboardState = Keyboard.GetState();
        }
    }
}
