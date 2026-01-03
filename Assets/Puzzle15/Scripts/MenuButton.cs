using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    private Button _button;

    private void OnEnable() => _button.onClick.AddListener(SelectMenuScene);

    private void SelectMenuScene() => SceneManager.LoadScene("MainMenu");

    private void Awake() => _button = GetComponent<Button>();

    private void OnDisable() => _button.onClick.RemoveListener(SelectMenuScene);
}
