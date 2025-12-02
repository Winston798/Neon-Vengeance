using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegisterPanel : BasePanel
{
    public InputField AccountInput;
    public InputField PasswordInput;
    public Button Back;
    public Button Register;

    public override void Show()
    {
        base.Show();
        Back.onClick.AddListener(() =>
        {
            AudioService.Instance.PlayEffect("Button");
            HideMe();
            UIService.Instance.ShowPanel<LoginPanel>();
        });
        Register.onClick.AddListener(() =>
        {
            AudioService.Instance.PlayEffect("Button");
            if (AccountInput.text == "" || PasswordInput.text == "")
            {
                UIService.Instance.ShowPanel<FloatPanel>().Init("The account or password cannot be empty", null);
            }
            else
            {
                if (!DataSystem.Instance.Register(AccountInput.text, PasswordInput.text))
                {
                    //注册失败
                    UIService.Instance.ShowPanel<FloatPanel>().Init("The account already exists.", null);
                }
                else
                {
                    UIService.Instance.ShowPanel<FloatPanel>().Init("Registration successful", () =>
                    {
                        HideMe();
                        UIService.Instance.ShowPanel<LoginPanel>();
                    });
                }
            }
        });
    }
}