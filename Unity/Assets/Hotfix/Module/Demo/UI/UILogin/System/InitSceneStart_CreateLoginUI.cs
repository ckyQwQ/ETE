using ETModel;
using UnityEngine;


namespace ETHotfix
{
	[Event(EventIdType.InitSceneStart)]
	public class InitSceneStart_CreateLoginUI: AEvent
	{
		public override void Run()
		{
			//UI ui = UILoginFactory.Create();
			UI ui = UILoginFactory.CreateGame();
			Game.Scene.GetComponent<UIComponent>().Add(ui);
		}
	}
}
