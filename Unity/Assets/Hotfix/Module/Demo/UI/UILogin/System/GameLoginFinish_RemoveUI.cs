using ETModel;

namespace ETHotfix
{
	[Event(EventIdType.GameLoginFinish)]
	public class GameLoginFinish_RemoveUI : AEvent
	{
		public override void Run()
		{
			//UIComponent t = Game.Scene.GetComponent<UIComponent>();
			//t.Camera.SetActive(false);
			Game.Scene.GetComponent<UIComponent>().Remove(UIType.UIGameLogin);
			ETModel.Game.Scene.GetComponent<ResourcesComponent>().UnloadBundle(UIType.UIGameLogin.StringToAB());
		}
	}
}
