using ETModel;

namespace ETHotfix
{
	[Event(EventIdType.GameLoginFinish)]
	public class GameLoginFinish_AddAircraftjet : AEvent
	{
		public override void Run()
		{
			AircraftjetFactory.Create();
		}
	}
}
