using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Aeroplane;

namespace MyGame
{
    public class AirBrakesTrigger : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            BaseOperationInput.AirBrakes = false;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OnClick(bool ifPress)
        {
            if (ifPress)
            {
                BaseOperationInput.AirBrakes = true;
            }
            else
            {
                BaseOperationInput.AirBrakes = false;
            }
        }
    }
}