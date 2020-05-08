using ETModel;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ETHotfix
{
    [ObjectSystem]
    public class SolarResourceLoadSystem : AwakeSystem<SolarResourceLoadComponent>
    {
        public override void Awake(SolarResourceLoadComponent self)
        {
            self.Awake();
        }
    }

    public class SolarResourceLoadComponent : Component
    {
        public void Awake()
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
