using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialoguePanel : BasePanel
{
    private DialogueData_SO CurrentDialogue;
    public Text CharacterName;
    public Text CharacterContent;
    public Button Next;
    private int CurrentIndex;
    public override void Show()
    {
        base.Show();
        Next.onClick.AddListener(() =>
        {
            CurrentIndex++;
            if (CurrentIndex >= CurrentDialogue.Pieces.Count)
            {
                //关闭对话
                DialogueSystem.Instance.EndDialogue();
                return;
            }
            OnNext();
        });
    }
    public void Init(DialogueData_SO Data)
    {
        CurrentDialogue = Data;
        CurrentIndex = 0;
        OnNext();
    }
    private void OnNext()
    {
        CharacterName.text = CurrentDialogue.Pieces[CurrentIndex].CharacterName;
        StartCoroutine(IEShowText(CurrentDialogue.Pieces[CurrentIndex].CharacterContent));
    }
    private IEnumerator IEShowText(string txt)
    {
        Next.gameObject.SetActive(false);
        CharacterContent.text = "";
        char[] Texts = txt.ToCharArray();
        float temp = 0;
        bool IsInputMouse = false;
        foreach (var i in Texts)
        {
            temp = 0;
            while (temp <= 0.05f)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    IsInputMouse = true;
                    break;
                }
                temp += Time.deltaTime;
                if (temp >= 0.05f)
                {
                    CharacterContent.text += i;
                }
                yield return null;
            }
            if (IsInputMouse)
            {
                CharacterContent.text = txt;
                break;
            }
        }
        Next.gameObject.SetActive(true);
    }
}
