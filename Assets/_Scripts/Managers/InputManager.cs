using Giroo.Core;
using UnityEngine;

namespace _Scripts.Managers
{
    public class InputManager : IUpdateable, IInitializable, IDisposable
    {
        private Vector2 _mousePosition;
        private bool _isPressing;
        private bool _isMouseDown;
        private bool _isMouseUp;

        private bool _isInputLocked;


        private void InputUnlock()
        {
            _isInputLocked = false;
        }

        private void InputLock()
        {
            _isInputLocked = true;
        }

        public bool IsPressing()
        {
            if (_isInputLocked)
            {
                return false;
            }

            return _isPressing;
        }

        public bool IsMouseDown()
        {
            if (_isInputLocked)
            {
                return false;
            }

            return _isMouseDown;
        }

        public bool IsMouseUp()
        {
            if (_isInputLocked)
            {
                return false;
            }

            return _isMouseUp;
        }

        public bool IsTap()
        {
            if (_isInputLocked)
            {
                return false;
            }

            return true;
        }

        public Vector2 GetMousePosition()
        {
            return _mousePosition;
        }


        public void Update(float deltaTime)
        {
            _mousePosition = Input.mousePosition;
            if (Input.GetMouseButtonDown(0))
            {
                _isMouseDown = true;
                _isPressing = true;
            }
            else
            {
                _isMouseDown = false;
            }

            if (Input.GetMouseButtonUp(0))
            {
                _isMouseUp = true;
                _isPressing = false;
            }
            else
            {
                _isMouseUp = false;
            }
        }

        public void Initialize()
        {
            Input.multiTouchEnabled = false;
        }

        public void Dispose()
        {
        }
    }
}