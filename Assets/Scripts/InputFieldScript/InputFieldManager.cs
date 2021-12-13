using System;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

namespace InputFieldScript
{
    public class InputFieldManager : MonoBehaviour
    {
        public delegate void CallBack(string text);

        public InputField inputField;


        private CallBack callBack;

        // private Text inputField;
        private string text = "";

        // public  GameObject obj; 
        private InputFieldDisplay inputFieldDisplay;
        private bool textSubmitted;
        private GameObject gameController;
        private PopUpMessage popupMessage;

        private void Start()
        {
            text = inputField.textComponent.text;
            inputFieldDisplay = GameObject.FindGameObjectWithTag("UI").GetComponent<InputFieldDisplay>();
            inputField.onValidateInput += delegate(string input, int charIndex, char addedChar)
            {
                return Validate(addedChar);
            };
            // print(GameObject.FindGameObjectWithTag("UI").ToString());

            callBack = null;
        }

        private void Update()
        {
            if (textSubmitted && callBack != null)
            {
                callBack.Invoke(text);
                text = "";
                callBack = null;
            }
        }

        public void SetCallback(CallBack callBack)
        {
            this.callBack = callBack;
            inputFieldDisplay.display = true;
        }

        public void SetTextSubmitted()
        {
            print(inputField.text); 
            this.textSubmitted = true;
            inputFieldDisplay.display = false;
            text = inputField.text;
        }
        
        public char Validate(char inputChar)
        {
            if (inputChar != '-' && int.TryParse("" + inputChar, out _))
            {
                return inputChar; 
            }
            else
            {
                return '\0'; 
            }
        }

        public void Restore()
        {
            inputField.Select();
            inputField.text = "";
            this.textSubmitted = false;
            inputFieldDisplay.display = false;
        }
    }
}