using System;
using ETModel;
using UnityEngine;

namespace ETHotfix
{
	public static class UIJoyStickFactory
	{
		public static UI Create()
		{
			try
			{
				ResourcesComponent resourcesComponent = ETModel.Game.Scene.GetComponent<ResourcesComponent>();
				resourcesComponent.LoadBundle(UIType.UIJoyStick.StringToAB());
				GameObject bundleGameObject = (GameObject)resourcesComponent.GetAsset(UIType.UIJoyStick.StringToAB(), UIType.UIJoyStick);
				GameObject gameObject = UnityEngine.Object.Instantiate(bundleGameObject);
				UI ui = ComponentFactory.Create<UI, string, GameObject>(UIType.UIJoyStick, gameObject, false);

				//ui.AddComponent<UILobbyComponent>();
				return ui;
			}
			catch (Exception e)
			{
				Log.Error(e);
				return null;
			}
		}
	}
}