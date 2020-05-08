using System;
using System.Net;
using ETModel;
using UnityEngine;
using UnityEngine.UI;

namespace ETHotfix
{
    [ObjectSystem]
    public class UiGameLoginComponentSystem : AwakeSystem<UIGameLoginComponent>
    {
        public override void Awake(UIGameLoginComponent self)
        {
            self.Awake();
        }
    }

    public class UIGameLoginComponent : Component
    {
        private GameObject loginBtn;
        private GameObject registerBtn;

        public void Awake()
        {
            ReferenceCollector rc = this.GetParent<UI>().GameObject.GetComponent<ReferenceCollector>();
            loginBtn = rc.Get<GameObject>("LoginBtn");
            loginBtn.GetComponent<Button>().onClick.Add(OnLogin);

            registerBtn = rc.Get<GameObject>("Register");
            registerBtn.GetComponent<Button>().onClick.Add(OnRegister);
        }

        public void OnLogin()
        {
            LoginHelper.OnLoginAsync().Coroutine();
        }

        public void OnRegister()
        {
            LoginHelper.OnRegisterAsync().Coroutine();
        }
    }
}