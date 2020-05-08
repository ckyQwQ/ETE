using ETModel;

namespace ETHotfix
{
	[Event(EventIdType.GameLoginFinish)]
	public class GameLoginFinish_AddJoyStick : AEvent
	{
		public override void Run()
		{
			UIJoyStickFactory.Create();
		}
	}
}
