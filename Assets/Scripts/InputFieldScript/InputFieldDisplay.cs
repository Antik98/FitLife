using UnityEngine;
using UnityEngine.UI;

namespace InputFieldScript
{
    public class InputFieldDisplay : MonoBehaviour
    {
            public GameObject inputField;
            public GameObject popup;
            public GameObject pauseMenu;
            public bool display = false; 

            // Update is called once per frame
            private void Start()
            {
                inputField.SetActive(false);
            }
            void Update()
            {
                if(!pauseMenu.activeSelf){
                    if(popup.activeSelf)
                    {
                        inputField.SetActive(false);
                    }
                    //switch visibility of 
                    else if(display)
                    {
                        inputField.SetActive(true);
                    }
                }
            }
    }
}