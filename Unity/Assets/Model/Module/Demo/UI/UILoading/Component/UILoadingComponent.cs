using UnityEngine;
using UnityEngine.UI;

namespace ETModel
{
	[ObjectSystem]
	public class UiLoadingComponentAwakeSystem : AwakeSystem<UILoadingComponent>
	{
		public override void Awake(UILoadingComponent self)
		{
			//self.text = self.GetParent<UI>().GameObject.Get<GameObject>("Text").GetComponent<Text>();
			GameObject t = self.Parent.GameObject;
			Slider slider = t.GetComponent<Slider>();
			if (slider != null)
			{
				slider.value = 0f;
			}
		}
	}

	[ObjectSystem]
	public class UiLoadingComponentStartSystem : StartSystem<UILoadingComponent>
	{
		public override void Start(UILoadingComponent self)
		{
			StartAsync(self).Coroutine();
		}
		
		public async ETVoid StartAsync(UILoadingComponent self)
		{
			GameObject t = self.Parent.GameObject;
			Slider slider = t.GetComponentInChildren<Slider>();
			TimerComponent timerComponent = Game.Scene.GetComponent<TimerComponent>();
			long instanceId = self.InstanceId;
			while (true)
			{
				await timerComponent.WaitAsync(1000);

				if (self.InstanceId != instanceId)
				{
					return;
				}

				BundleDownloaderComponent bundleDownloaderComponent = Game.Scene.GetComponent<BundleDownloaderComponent>();
				if (bundleDownloaderComponent == null)
				{
					continue;
				}
				if (slider != null)
				{
					float v = (float)bundleDownloaderComponent.Progress / 100.0f;
					slider.value = Mathf.Clamp(v, 0f, 1f);
				}
				//self.text.text = $"{bundleDownloaderComponent.Progress}%";
			}
		}
	}

	public class UILoadingComponent : Component
	{
		public Text text;
	}
}
