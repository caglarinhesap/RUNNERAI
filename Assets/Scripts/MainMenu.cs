using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] TMP_InputField inputField;
    public void Play()
    {
        PlayerPrefs.SetString("username", inputField.text);
        SceneManager.LoadScene(1);
    }
}
