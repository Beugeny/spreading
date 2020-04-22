using System;
using UnityEngine;

namespace Src.utils
{
    public class MoveController:IService
    {
        private float _speed;

        private bool _leftKeyPressed = false;
        private bool _rightKeyPressed = false;
        private bool _upKeyPressed = false;
        private bool _downKeyPressed = false;
        private bool _targetFixed;

        private Vector3 _dragTargetPosition;
        private Vector3 _dragMousePosition;
        private RectTransform _target;


        public MoveController(RectTransform Target)
        {
            _target = Target;
            _speed = 3f;
        }

        public void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _targetFixed = true;
                _dragMousePosition = Input.mousePosition;
                _dragTargetPosition = _target.position;
            }

            if (Input.GetMouseButtonUp(0))
            {
                _targetFixed = false;
            }

            if (_targetFixed) drag();
            else KeyboardMove();


            boundPosition();
        }

        private void boundPosition()
        {
            Vector3[] corners=new Vector3[4];
            _target.GetWorldCorners(corners);
          
            var screenPointMin = RectTransformUtility.WorldToScreenPoint(Camera.main,corners[0]);
            var screenPointMax = RectTransformUtility.WorldToScreenPoint(Camera.main,corners[2]);
            Vector3 delta=Vector3.zero;
            if (screenPointMin.x > 0) delta.x = screenPointMin.x;
            if (screenPointMin.y > 0) delta.y = screenPointMin.y;
            if (screenPointMax.x < 0) delta.x = screenPointMax.x;
            if (screenPointMax.y < 0) delta.y = screenPointMax.y;
            _target.position -= delta;
        }


        private void drag()
        {
            _target.position = _dragTargetPosition + (Input.mousePosition - _dragMousePosition);
        }


        private void KeyboardMove()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                _leftKeyPressed = true;
            }
            else if (Input.GetKeyUp(KeyCode.A))
            {
                _leftKeyPressed = false;
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                _rightKeyPressed = true;
            }
            else if (Input.GetKeyUp(KeyCode.D))
            {
                _rightKeyPressed = false;
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                _upKeyPressed = true;
            }
            else if (Input.GetKeyUp(KeyCode.W))
            {
                _upKeyPressed = false;
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                _downKeyPressed = true;
            }
            else if (Input.GetKeyUp(KeyCode.S))
            {
                _downKeyPressed = false;
            }

            var move = Vector3.zero;
            if (_leftKeyPressed)
            {
                if (_upKeyPressed)
                {
                    move = new Vector2(-_speed / 2, _speed / 2);
                }
                else if (_downKeyPressed)
                {
                    move = new Vector2(-_speed / 2, -_speed / 2);
                }
                else
                {
                    move = new Vector2(-_speed, 0);
                }
            }
            else if (_rightKeyPressed)
            {
                if (_upKeyPressed)
                {
                    move = new Vector2(_speed / 2, _speed / 2);
                }
                else if (_downKeyPressed)
                {
                    move = new Vector2(_speed / 2, -_speed / 2);
                }
                else
                {
                    move = new Vector2(_speed, 0);
                }
            }
            else if (_upKeyPressed)
            {
                move = new Vector2(0, _speed);
            }
            else if (_downKeyPressed)
            {
                move = new Vector2(0, -_speed);
            }

            if (Math.Abs(move.x) > float.Epsilon || Math.Abs(move.y) > float.Epsilon)
            {
                _target.localPosition -= move;
            }
        }

        public void Dispose()
        {
        }
    }
}