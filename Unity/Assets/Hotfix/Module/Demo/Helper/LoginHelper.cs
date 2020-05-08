using System;
using ETModel;
using UnityEngine;
using UnityEngine.UI;

namespace ETHotfix
{
    public static class LoginHelper
    {
        public static async ETVoid OnLoginAsync(string account)
        {
            try
            {
                // 创建一个ETModel层的Session
                ETModel.Session session = ETModel.Game.Scene.GetComponent<NetOuterComponent>().Create(GlobalConfigComponent.Instance.GlobalProto.Address);

                // 创建一个ETHotfix层的Session, ETHotfix的Session会通过ETModel层的Session发送消息
                Session realmSession = ComponentFactory.Create<Session, ETModel.Session>(session);
                R2C_Login r2CLogin = (R2C_Login)await realmSession.Call(new C2R_Login() { Account = account, Password = "111111" });
                realmSession.Dispose();

                // 创建一个ETModel层的Session,并且保存到ETModel.SessionComponent中
                ETModel.Session gateSession = ETModel.Game.Scene.GetComponent<NetOuterComponent>().Create(r2CLogin.Address);
                ETModel.Game.Scene.AddComponent<ETModel.SessionComponent>().Session = gateSession;

                // 创建一个ETHotfix层的Session, 并且保存到ETHotfix.SessionComponent中
                Game.Scene.AddComponent<SessionComponent>().Session = ComponentFactory.Create<Session, ETModel.Session>(gateSession);

                G2C_LoginGate g2CLoginGate = (G2C_LoginGate)await SessionComponent.Instance.Session.Call(new C2G_LoginGate() { Key = r2CLogin.Key });

                Log.Info("登陆gate成功!");

                // 创建Player
                Player player = ETModel.ComponentFactory.CreateWithId<Player>(g2CLoginGate.PlayerId);
                PlayerComponent playerComponent = ETModel.Game.Scene.GetComponent<PlayerComponent>();
                playerComponent.MyPlayer = player;

                Game.EventSystem.Run(EventIdType.LoginFinish);

                // 测试消息有成员是class类型
                G2C_PlayerInfo g2CPlayerInfo = (G2C_PlayerInfo)await SessionComponent.Instance.Session.Call(new C2G_PlayerInfo());
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        public static async ETVoid OnLoginAsync()
        {
            try
            {
                GameObject account = GetUIComponentReferenceCollector(UIType.UIGameLogin, "Account");
                GameObject password = GetUIComponentReferenceCollector(UIType.UIGameLogin, "Password");

                if (account == null || password == null)
                {
                    return;
                }
                InputField accountText = account.GetComponentInChildren<InputField>();
                InputField passwordText = password.GetComponentInChildren<InputField>();

                if (accountText == null || passwordText == null)
                {
                    return;
                }

                string pAccount = accountText.text;
                string pPassword = passwordText.text;

                if (pAccount.Length == 0 || pPassword.Length == 0)
                {
                    return;
                }

                // 创建一个ETModel层的Session
                ETModel.Session session = ETModel.Game.Scene.GetComponent<NetOuterComponent>().Create(GlobalConfigComponent.Instance.GlobalProto.Address);

                // 创建一个ETHotfix层的Session, ETHotfix的Session会通过ETModel层的Session发送消息
                Session realmSession = ComponentFactory.Create<Session, ETModel.Session>(session);
                R2C_Login r2CLogin = (R2C_Login)await realmSession.Call(new C2R_Login() { Account = pAccount, Password = pPassword });
                if (r2CLogin.Error != 0)
                {
                    GameObject dialog = GetUIComponentReferenceCollector(UIType.UIGameLogin, "Dialog");
                    if (dialog != null)
                    {
                        dialog.SetActive(true);
                    }
                    return;
                }
                realmSession.Dispose();

                // 创建一个ETModel层的Session,并且保存到ETModel.SessionComponent中
                ETModel.Session gateSession = ETModel.Game.Scene.GetComponent<NetOuterComponent>().Create(r2CLogin.Address);
                ETModel.Game.Scene.AddComponent<ETModel.SessionComponent>().Session = gateSession;

                // 创建一个ETHotfix层的Session, 并且保存到ETHotfix.SessionComponent中
                Game.Scene.AddComponent<SessionComponent>().Session = ComponentFactory.Create<Session, ETModel.Session>(gateSession);

                G2C_LoginGate g2CLoginGate = (G2C_LoginGate)await SessionComponent.Instance.Session.Call(new C2G_LoginGate() { Key = r2CLogin.Key });

                Log.Info("登陆gate成功!");

                // 创建Player
                Player player = ETModel.ComponentFactory.CreateWithId<Player>(g2CLoginGate.PlayerId);
                PlayerComponent playerComponent = ETModel.Game.Scene.GetComponent<PlayerComponent>();
                playerComponent.MyPlayer = player;

                Game.EventSystem.Run(EventIdType.GameLoginFinish);

                // 测试消息有成员是class类型
                G2C_PlayerInfo g2CPlayerInfo = (G2C_PlayerInfo)await SessionComponent.Instance.Session.Call(new C2G_PlayerInfo());
                Camera.main.transform.Find("ParticleSystem").gameObject.SetActive(false);
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        public static async ETVoid OnRegisterAsync()
        {
            try
            {
                GameObject account = GetUIComponentReferenceCollector(UIType.UIGameLogin, "Account");
                GameObject password = GetUIComponentReferenceCollector(UIType.UIGameLogin, "Password");

                InputField accountText = account.GetComponentInChildren<InputField>();
                InputField passwordText = password.GetComponentInChildren<InputField>();

                string pAccount = accountText.text;
                string pPassword = passwordText.text;

                if (pAccount.Length == 0 || pPassword.Length == 0)
                {
                    GameObject dialog = GetUIComponentReferenceCollector(UIType.UIGameLogin, "RegisterDialogNotNullRemind");
                    if (dialog != null)
                    {
                        dialog.SetActive(true);
                    }
                    return;
                }

                GameObject registerDialog = GetUIComponentReferenceCollector(UIType.UIGameLogin, "RegisterDialog");
                if (registerDialog != null)
                {
                    registerDialog.SetActive(true);
                }

                ETModel.Session session = ETModel.Game.Scene.GetComponent<NetOuterComponent>().Create(GlobalConfigComponent.Instance.GlobalProto.Address);
                Session realmSession = ComponentFactory.Create<Session, ETModel.Session>(session);
                R2C_Register r2CRegister = (R2C_Register)await realmSession.Call(new C2R_Register() { Account = pAccount, Password = pPassword });

                registerDialog.SetActive(false);

                if (r2CRegister.Error != 0)
                {
                    GameObject dialog = GetUIComponentReferenceCollector(UIType.UIGameLogin, "RegisterDialogNameExist");
                    if (dialog != null)
                    {
                        dialog.SetActive(true);
                    }
                    return;
                }
                else
                {
                    GameObject dialog = GetUIComponentReferenceCollector(UIType.UIGameLogin, "RegisterDialogSuccess");
                    if (dialog != null)
                    {
                        dialog.SetActive(true);
                    }
                    return;
                }
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }


        public static GameObject GetUIComponentReferenceCollector(string uiType, string gameObjectName)
        {
            UI ui = Game.Scene.GetComponent<UIComponent>().Get(uiType);

            if (ui != null)
            {
                ReferenceCollector rc = ui.GameObject.GetComponent<ReferenceCollector>();
                GameObject gameObj = rc.Get<GameObject>(gameObjectName);

                return gameObj;
            }

            return null;
        }
    }
}