using UnityEngine;
using UnityEngine.SceneManagement;
using System;

namespace Unity.FPS.Gameplay {
    public class Scene_Manager : Singleton<Scene_Manager>
    {

        private int _currentScene;

        public bool HasBlaster { get; set; }
        public bool HasShotgun { get; set; }
        public bool HasLauncher { get; set; }

        public override void Awake()
        {
            base.Awake();
        }

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
        public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            _currentScene = scene.buildIndex;
            Debug.Log($"[Scene_Manager] Scene Loaded: {scene.name} (Index: {_currentScene}) | Shotgun: {HasShotgun} | Launcher: {HasLauncher}");
        }

        //public void SceneLoadingDebug() 
        //{
        //    Scene CurrentScene = SceneManager.GetActiveScene();
        //    Debug.Log($"[Scene_Manager] Scene Loaded: {CurrentScene.name} (Index: {_currentScene}) | Shotgun: {HasShotgun} | Launcher: {HasLauncher}");
        //} ---------------------------- Previously Used to test if the Instance was staying or not

        public void ReloadLevel()
        {
            SceneManager.LoadScene(_currentScene);
        }

        public void LoadNextLevel()
        {
            int totalScenes = SceneManager.sceneCountInBuildSettings;
            int nextScene = _currentScene + 1;

            Debug.Log(nextScene);
            

            if (nextScene >= totalScenes - 1)
            {
                SceneManager.LoadScene(1);
                nextScene = 1;
            }

            SceneManager.LoadScene(nextScene);
        }

        public void LoadDeathMenu()
        {
            _currentScene = 5;
            SceneManager.LoadScene("LoseScene");
        }

        public void LoadWinMenu()
        {
            SceneManager.LoadScene("WinScene");
        }

        public void LoadMainMenu()
        {
            SceneManager.LoadScene(0);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}



