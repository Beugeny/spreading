using System;
using UnityEngine;

namespace Src.utils
{
    public class TimeController:MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.KeypadPlus) || Input.GetKeyDown(KeyCode.Plus))
            {
                Time.timeScale = Mathf.Min((float)(Time.timeScale * 1.5),50);
            }
            else if (Input.GetKeyDown(KeyCode.KeypadMinus) || Input.GetKeyDown(KeyCode.Minus))
            {
                Time.timeScale = Mathf.Max((float)(Time.timeScale / 1.5),0.1f);
            }
            else if (Input.GetKeyDown(KeyCode.Pause))
            {
                if (Math.Abs(Time.timeScale) <= float.Epsilon)
                {
                    Time.timeScale = 1f;    
                }
                else
                {
                    Time.timeScale = 0f;
                }
            }
        }
    }
}