using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Common;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(LoginRequest))]
public class LoginPanel : MonoBehaviour {

    public GameObject registerPanel;
    public InputField usernameIF;
    public InputField passwordIF;
    public Text hintMessage;

    private LoginRequest loginRequest;

    void Start()
    {
        loginRequest = GetComponent<LoginRequest>();              
    }

    public void OnLoginButton()
    {
        hintMessage.text = "";
        loginRequest.username = usernameIF.text;
        loginRequest.password = passwordIF.text;
        loginRequest.DefaultRequest();
    }
    public void OnRegisterButton()
    {
        gameObject.SetActive(false);
        registerPanel.SetActive(true);
    }
    public void OnPerationResponse(ReturnCode returnCode)
    {
        if (returnCode == ReturnCode.Success)
        {
            // 跳转到下一个场景 
            SceneManager.LoadScene("Game");
        }
        else
        {
            hintMessage.text = "用户名或密码错误";
        }
    }
}
