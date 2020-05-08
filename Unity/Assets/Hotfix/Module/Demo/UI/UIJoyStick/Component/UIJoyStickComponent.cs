using ETModel;
using UnityEngine;
using UnityEngine.UI;

namespace ETHotfix
{
	[ObjectSystem]
	public class UIJoyStickComponentSystem : AwakeSystem<UIJoyStickComponent>
	{
		public override void Awake(UIJoyStickComponent self)
		{
			self.Awake();
		}
	}

	public class UIJoyStickComponent : Component
	{
		private GameObject enterMap;
		private Text text;

		public void Awake()
		{
			//ReferenceCollector rc = this.GetParent<UI>().GameObject.GetComponent<ReferenceCollector>();

			//enterMap = rc.Get<GameObject>("EnterMap");
			//enterMap.GetComponent<Button>().onClick.Add(this.EnterMap);

			//this.text = rc.Get<GameObject>("Text").GetComponent<Text>();
		}

		private void EnterMap()
		{
			MapHelper.EnterMapAsync().Coroutine();
		}
	}
}
