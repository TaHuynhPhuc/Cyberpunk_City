using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour
{
    public CharacterDatabase characterDB;
    public TextMeshProUGUI nameText;
    public Image spriteRder;
    private int selectedOptione = 0;
    // Start is called before the first frame update
    public void Start()
    {
        if (!PlayerPrefs.HasKey("selectedOption"))
        {
            selectedOptione = 0;
        }
        else
        {
            Load();
        }
        UpdateCharacter(selectedOptione);
    }

    public void NextOption()
    {
        selectedOptione++;
        if(selectedOptione >= characterDB.characterCount)
        {
            selectedOptione = 0;
        }
        UpdateCharacter(selectedOptione);
        Save();
    }

    public void BackOption()
    {
        selectedOptione--;
        if (selectedOptione < 0)
        {
            selectedOptione = characterDB.characterCount -1;
        }
        UpdateCharacter(selectedOptione);
        Save();
    }

    private void UpdateCharacter(int selectedOption)
    {
        Character character = characterDB.GetCharacter(selectedOption);
        spriteRder.sprite = character.characterSprite;
        nameText.text = character.characterName;
    }

    private void Load()
    {
        selectedOptione = PlayerPrefs.GetInt("selectedOption");
    }

    private void Save()
    {
        PlayerPrefs.SetInt("selectedOption", selectedOptione);
    }
}
