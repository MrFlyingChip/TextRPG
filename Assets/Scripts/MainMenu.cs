
using System.Collections;
using System.Linq;
using System.Xml;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Xml.Serialization;

public class Language
{
    [XmlElement("Name")]
    public string Name { get; set; }
}


[XmlRoot("LanguageList")]
public class LanguageList
{
    [XmlElement("Language")]
    public List<Language> Languages { get; set; }
}

public class MainMenu : MonoBehaviour {

    public LanguageList languages;
    public GameObject[] menuObjects;
    public InputField inputName;
    public Dropdown languageDropdown;
    public int sound;
    public Sprite[] soundIcons;
    public Image soundButtonImage;
    public Button loadButton;

    public GameObject playerButtonPrefab;
    public RectTransform scrollViewContent;
    public PlayerList players;

    // Use this for initialization
    void Start() {
        LoadLanguages();
        FillLanguageDropDown();
        LoadSound();
        LoadPlayers();
    }

    private void LoadPlayers()
    {
        players = LoaderManager.LoadPlayers();
        if(players.Players.Count == 0)
        {
            loadButton.interactable = false;
        }
        else
        {
            for (int i = 0; i < players.Players.Count; i++)
            {
                GameObject playerButton = Instantiate(playerButtonPrefab, scrollViewContent) as GameObject;
                playerButton.GetComponent<RectTransform>().anchorMax = new Vector2(0.99f, 0.99f - i * 0.13f - 0.01f);
                playerButton.GetComponent<RectTransform>().anchorMin = new Vector2(0.01f, 0.99f - (i + 1) * 0.13f);
                playerButton.GetComponentsInChildren<Text>()[0].text = players.Players[i].Name;
                playerButton.GetComponentsInChildren<Text>()[1].text = "Level " + players.Players[i].Level;
                playerButton.GetComponentsInChildren<Text>()[2].text = "Gold " + players.Players[i].Gold;
            }
        }
    }

    private void LoadLanguages()
    {
        languages = LoaderManager.LoadLanguages();
    }

    public void OnCreateButtonClicked()
    {
        SaveManager.CreatePlayer(inputName.text);
    }

    private void FillLanguageDropDown()
    {
        languageDropdown.AddOptions((from s in languages.Languages select s.Name).ToList<string>());
    }

    private void LoadSound()
    {
        if (!PlayerPrefs.HasKey("Sound"))
        {
            PlayerPrefs.SetInt("Sound", 1);
        }
        sound = PlayerPrefs.GetInt("Sound");
        SetSoundIcon();
    }

	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape)) { ShowMenuObject(0); }
    }

    public void ShowMenuObject(int menu)
    {
        foreach (var item in menuObjects)
        {
            item.SetActive(false);
        }
        inputName.text = string.Empty;
        menuObjects[menu].SetActive(true);
    }

    private void SetSoundIcon()
    {
        soundButtonImage.sprite = soundIcons[sound];
    }

    public void ChangeSound()
    {
        sound = 1 - sound;
        PlayerPrefs.SetInt("Sound", sound);
        SetSoundIcon();
    }

    public void OnExitButtonClicked()
    {
        Application.Quit();
    }
}
