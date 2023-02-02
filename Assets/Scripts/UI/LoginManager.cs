using DataManager;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UI.Login
{
    public class LoginManager : MonoBehaviour
    {
        [SerializeField] private TMP_InputField NameInput;
        public void Login()
        {
            PlayerManager.AddPlayer(NameInput.text);
        }
        public void Delete()
        {
            PlayerManager.RemovePlayer(NameInput.text);
        }
    }
}
