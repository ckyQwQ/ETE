using UnityEngine;

namespace ETModel
{
    [Event(EventIdType.LoadingBegin)]
    public class LoadingBeginEvent_CreateLoadingUI : AEvent
    {
        public override void Run()
        {
            UI ui = UILoadingFactory.CreateProcessBar();
			Game.Scene.GetComponent<UIComponent>().Add(ui);
        }
    }
}
