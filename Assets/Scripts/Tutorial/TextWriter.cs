using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextWriter : MonoBehaviour
{
    private TextMeshProUGUI text;
    private string textToWrite;
    private int characterIndex;
    private float timePerCharacter;
    private float timer;
    private bool invisibleCharacters;

    public void AddWriter(TextMeshProUGUI text, string textToWrite, float timePerCharacter, bool invisibleCharacters)
    {
        this.text = text;
        this.textToWrite = textToWrite;
        this.timePerCharacter = timePerCharacter;
        this.invisibleCharacters = invisibleCharacters;
        characterIndex = 0;
    }

    private void Update()
    {
        if(text != null)
        {
            timer -= Time.unscaledDeltaTime;
            if (timer <= 0f)
            {
                timer += timePerCharacter;
                characterIndex++;
                string msg = textToWrite.Substring(0, characterIndex);
                if (invisibleCharacters)
                {
                    msg += "<color=#00000000>" + textToWrite.Substring(characterIndex) + "</color>";
                }
                text.text = msg;

                if(characterIndex >= textToWrite.Length)
                {
                    text = null;
                    return;
                }
            }
        }
    }
}
