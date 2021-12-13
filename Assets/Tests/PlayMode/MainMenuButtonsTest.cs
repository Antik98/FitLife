// using System;
// using Zenject;
// using System.Collections;
// using NUnit.Framework;
// using UnityEngine;
// using UnityEngine.EventSystems;
// using UnityEngine.SceneManagement;
// using UnityEngine.TestTools;
//
// public class MainMenuButtonsTest
// {
//     [OneTimeSetUp]
//     public void BindInterfaces()
//     {
//         System.Environment.SetEnvironmentVariable("isUnityTestRunning", "true");
//     }
//
//     [UnityTest]
//     public IEnumerator NewGameButton()
//     {
//         SceneManager.LoadScene("OLD_MainMenuScene");
//
//         // waiting a few seconds to load the scene correctly.
//         yield return new WaitForSeconds(2f);
//
//         GameObject newGameButton = null;
//         try
//         {
//             // gets the new game button and clicks on it, which loads the HomeScene
//             newGameButton = GameObject.FindWithTag("NewGameButton");
//             var pointer = new PointerEventData(EventSystem.current);
//             ExecuteEvents.Execute(newGameButton.gameObject, pointer, ExecuteEvents.pointerClickHandler);
//             // ExecuteEvents.Execute(newGameButton.gameObject, pointer, );
//         }
//         catch (Exception e)
//         {
//             // fail the test 
//             Assert.Fail();
//         }
//
//         yield return new WaitForSeconds(2f);
//         Assert.True(SceneManager.GetActiveScene().name == "HomeScene");
//         yield return true;
//     }
//
//     [UnityTest]
//     public IEnumerator CreditsMenuButton()
//     {
//         SceneManager.LoadScene("OLD_MainMenuScene");
//
//         // waiting a few seconds to load the scene correctly.
//         yield return new WaitForSeconds(2f);
//
//         GameObject newGameButton = null;
//         try
//         {
//             // gets the new game button and clicks on it, which loads the HomeScene
//             newGameButton = GameObject.FindWithTag("CreditsButton");
//             var pointer = new PointerEventData(EventSystem.current);
//             ExecuteEvents.Execute(newGameButton.gameObject, pointer, ExecuteEvents.pointerClickHandler);
//             // ExecuteEvents.Execute(newGameButton.gameObject, pointer, );
//         }
//         catch (Exception e)
//         {
//             // fail the test 
//             Assert.Fail();
//         }
//
//         yield return new WaitForSeconds(2f);
//         yield return true;
//     }
//
//     [UnityTest]
//     public IEnumerator LoadGameButton()
//     {
//         SceneManager.LoadScene("OLD_MainMenuScene");
//
//         // waiting a few seconds to load the scene correctly.
//         yield return new WaitForSeconds(2f);
//
//         GameObject newGameButton = null;
//         try
//         {
//             // gets the new game button and clicks on it, which loads the HomeScene
//             newGameButton = GameObject.FindWithTag("LoadGameButton");
//             var pointer = new PointerEventData(EventSystem.current);
//             ExecuteEvents.Execute(newGameButton.gameObject, pointer, ExecuteEvents.pointerClickHandler);
//             // ExecuteEvents.Execute(newGameButton.gameObject, pointer, );
//         }
//         catch (Exception e)
//         {
//             // fail the test 
//             Assert.Fail();
//         }
//
//         yield return new WaitForSeconds(2f);
//
//         yield return true;
//     }
//
//     [UnityTest]
//     public IEnumerator SettingsButton()
//     {
//         SceneManager.LoadScene("OLD_MainMenuScene");
//
//         // waiting a few seconds to load the scene correctly.
//         yield return new WaitForSeconds(2f);
//
//         GameObject newGameButton = null;
//         try
//         {
//             // gets the new game button and clicks on it, which loads the HomeScene
//             newGameButton = GameObject.FindWithTag("SettingsButton");
//             var pointer = new PointerEventData(EventSystem.current);
//             ExecuteEvents.Execute(newGameButton.gameObject, pointer, ExecuteEvents.pointerClickHandler);
//             // ExecuteEvents.Execute(newGameButton.gameObject, pointer, );
//         }
//         catch (Exception e)
//         {
//             // fail the test 
//             Assert.Fail();
//         }
//
//         yield return new WaitForSeconds(2f);
//         yield return true;
//     }
// }