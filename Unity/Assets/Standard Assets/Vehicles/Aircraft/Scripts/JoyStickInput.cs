using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UnityStandardAssets.Vehicles.Aeroplane
{
    public class JoyStickInput
    {
        public static Vector2 Direction
        {
            get
            {
                return _direction;
            }

            set
            {
                _direction = value;
                _direction.Normalize();
            }
        }

        static Vector2 _direction = new Vector2(0, 0);
    }
}
