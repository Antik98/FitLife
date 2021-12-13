// using System;
// using System.Collections;
// using NUnit.Framework;
// using UnityEngine;
// using UnityEngine.SceneManagement;
// using UnityEngine.TestTools;
//
// namespace Tests
// {
//     public class TimerTests
//     {
//         private GameObject gameObject; 
//         [SetUp]
//         public void Setup()
//         {
//             this.gameObject = null; 
//             this.gameObject = new GameObject();
//             gameObject.tag = "GameController";
//             gameObject.AddComponent<SceneController>();
//         }
//         
//         [UnityTest]
//         public IEnumerator CurrentTimeTest()
//         {
//             // Use the Assert class to test conditions.
//             // Use yield to skip a frame.
//             var timer = gameObject.AddComponent<GameTimer>();
//             timer.curDeltaTime = 0f;
//             timer.seconds = 0;
//             timer.minutes = 0;
//             timer.inGameHours = 6;
//             timer.inGameMinutes = 0;
//             timer.inGameDay = 1;
//             timer.dayLenMins = 1;
//
//
//             Assert.AreEqual( "06:00" , timer.GetCurrentTime());
//             timer.StartTimer();
//             
//             yield return new WaitForSeconds(2);
//
//             // Assert.AreEqual(2, timer.seconds);  
//         }
//         [UnityTest]
//         public IEnumerator StartStopTest()
//         {
//             // Use the Assert class to test conditions.
//             // Use yield to skip a frame.
//             var timer = gameObject.AddComponent<GameTimer>();
//             timer.curDeltaTime = 0f;
//             timer.seconds = 0;
//             timer.minutes = 0;
//             timer.inGameHours = 6;
//             timer.inGameMinutes = 0;
//             timer.inGameDay = 1;
//             timer.dayLenMins = 1;
//
//             Assert.AreEqual( "06:00" , timer.GetCurrentTime());
//             timer.StartTimer();
//             
//             yield return new WaitForSeconds(2);
//
//             Assert.AreEqual(2, timer.seconds);  
//             timer.StopTimer();
//             Assert.AreEqual(2, timer.seconds);  
//             timer.StartTimer();
//             yield return new WaitForSeconds(1);
//             // Assert.AreEqual(3, timer.seconds);  
//         }
//         
//                 
//         [UnityTest]
//         public IEnumerator RemainingTimeTest()
//         {
//             // Use the Assert class to test conditions.
//             // Use yield to skip a frame.
//             var timer = gameObject.AddComponent<GameTimer>();
//             timer.curDeltaTime = 0f;
//             timer.seconds = 0;
//             timer.minutes = 0;
//             timer.inGameHours = 6;
//             timer.inGameMinutes = 0;
//             timer.inGameDay = 1;
//             timer.dayLenMins = 1;
//             // new GameTimer()
//
//             timer.StartTimer();
//             
//             yield return new WaitForSeconds(2);
//
//             // Assert.AreEqual("01:58", timer.GetRemainingTime());  
//         }
//         
//         [UnityTest]
//         public IEnumerator EndGameTest()
//         {
//             var timer = gameObject.AddComponent<GameTimer>();
//             yield return new WaitForSeconds(2);
//             timer.StartTimer();
//             timer.EndGame();
//             yield return new WaitForSeconds(2);
//             Assert.That(SceneManager.GetActiveScene().name == "endingScene");
//         }
//
//
//         [TearDown]
//         public void TearDown()
//         {
//             
//             
//             GameObject.Destroy(gameObject);
//         }
//     }
// }