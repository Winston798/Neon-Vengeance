using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginPanel : BasePanel
{
    public InputField AccountInput;
    public InputField PasswordInput;
    public Button Login;
    public Button Register;

    public override void Show()
    {
        base.Show();
        Login.onClick.AddListener(() =>
        {
            AudioService.Instance.PlayEffect("Button");
            if (!DataSystem.Instance.Login(AccountInput.text, PasswordInput.text))
            {
                UIService.Instance.ShowPanel<FloatPanel>().Init("Account or password is incorrect", null);
            }
            else
            {
                //登录
                if (DataSystem.Instance.CurrentLoginData.PlayerName == "")
                {
                    UIService.Instance.ShowPanel<NamePanel>();
                }
                else
                {
                    UIService.Instance.ShowPanel<StartPanel>();
                }
                HideMe();
            }
        });
        Register.onClick.AddListener(() =>
        {
            AudioService.Instance.PlayEffect("Button");
            HideMe();
            UIService.Instance.ShowPanel<RegisterPanel>();
        });
    }
}