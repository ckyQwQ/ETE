namespace UnityStandardAssets.Vehicles.Aeroplane
{
    public class BaseOperationInput
    {
        public static bool AirBrakes
        {
            set
            {
                airBrakes = value;
            }
            get
            {
                return airBrakes;
            }
        }

        static bool airBrakes;
    }
}
