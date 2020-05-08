using System;
using ETModel;
using UnityEngine;

namespace ETHotfix
{
    public static class AircraftjetFactory
    {

        public static void Create()
        {
            try
            {
                ResourcesComponent resourcesComponent = ETModel.Game.Scene.GetComponent<ResourcesComponent>();
                resourcesComponent.LoadBundle(UIType.AircraftJet.StringToAB());
                GameObject bundleGameObject = (GameObject)resourcesComponent.GetAsset(UIType.AircraftJet.StringToAB(), UIType.AircraftJet);
                ETModelGameObject.aircraftJet = UnityEngine.Object.Instantiate(bundleGameObject);
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }
    }
}