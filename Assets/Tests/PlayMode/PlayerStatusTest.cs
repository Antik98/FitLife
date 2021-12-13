using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class PlayerStatusTest
    {
        private GameObject gameController;
        private GameObject statusController;
        private GameTimer timer; 
        [SetUp]
        public void Setup()
        {
            this.gameController = null; 
            this.timer = null; 
            this.gameController = new GameObject();
            gameController.tag = "GameController";
            gameController.AddComponent<SceneController>();
            this.statusController = null; 
            this.statusController = new GameObject();
            timer = statusController.AddComponent<GameTimer>();
            statusController.tag = "StatusController";
        }
        
        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator PlayerStatusTimerPassed()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.

            var status = statusController.AddComponent<PlayerStatus>();
            status.energy = 100; 
            status.hunger = 100; 
            status.social = 100; 
            status.foundEasterEggs = 0;
            timer.gameTime = new System.TimeSpan(0,0,10);
            yield return new WaitForSeconds(2f);
            status.energy = 98; 
            status.hunger = 98; 
            status.social = 98;
        }

        [TearDown]
        public void TearDown()
        {
            GameObject.Destroy(gameController);
            GameObject.Destroy(statusController);
        }
    }
}
