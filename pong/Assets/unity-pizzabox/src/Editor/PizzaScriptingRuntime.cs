using System;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Pizza.Runtime.Editor
{
    /// <summary>
    /// 
    /// </summary>
    [InitializeOnLoad]
    public class PizzaScriptingRuntime : IDisposable
    {
        public static PizzaScriptingRuntime shared;
        private const bool INCLUDE_INACTIVE = false;
        private const bool LOG_RUNTIME_MESSAGES = false;
        private const bool VALIDATE_WHILE_PLAYING = false;

        static PizzaScriptingRuntime()
        {
            shared = new PizzaScriptingRuntime();
        }

        private PizzaScriptingRuntime()
        {
            EditorApplication.update += EditorUpdate;
            EditorApplication.hierarchyChanged += HierarchyChanged;

            EditorSceneManager.activeSceneChangedInEditMode += ActiveSceneChangedInEditMode;
            EditorSceneManager.sceneDirtied += SceneDirtied;
            EditorSceneManager.sceneOpening += SceneOpening;
            EditorSceneManager.sceneOpened += SceneOpened;

            PrefabStage.prefabStageOpened += PrefabStageOpened;
            PrefabStage.prefabStageDirtied += PrefabStageDirtied;

            AssemblyReloadEvents.afterAssemblyReload += AfterAssemblyReload;

            InitializeLogger();
        }

        ~PizzaScriptingRuntime()
        {
            Dispose();
        }

        private void InitializeLogger()
        {
            // This is a bit of a lie. There is nothign to init. 
            // Just log the ID and move on.
            LogMessage($"PizzaScriptingRuntime::InitializeLogger initlialized with ID {PizzaLogger.GetLoggerID()}", true);
        }

        // Note: These functions are pretty much in the order they are fired

        private void AfterAssemblyReload()
        {
            LogMessage("PizzaScriptingRuntime::AfterAssemblyReload");
            ValidateCurrentPrefabStage();
            ValidateCurrentScene();
        }

        private void SceneOpening(string path, OpenSceneMode mode)
        {
            LogMessage($"PizzaScriptingRuntime::SceneOpening \t {path}");
        }

        private void ActiveSceneChangedInEditMode(Scene oldScene, Scene newScene)
        {
            LogMessage($"PizzaScriptingRuntime::ActiveSceneChangedInEditMode \t {oldScene.name}-{newScene.name}");

            // TODO: Decide if we want this or not
            //ValidateCurrentScene();
        }

        private void SceneOpened(Scene scene, OpenSceneMode mode)
        {
            LogMessage($"PizzaScriptingRuntime::SceneOpened \t {scene.name}");
            ValidateCurrentScene();
        }

        private void PrefabStageOpened(PrefabStage prefabStage)
        {
            LogMessage($"PizzaScriptingRuntime::PrefabStageOpened \t Stage:{prefabStage.name}");
            ValidateCurrentPrefabStage();
        }

        private void HierarchyChanged()
        {
            // This fires alot. Uncomment only if you need it.
            //LogMessage("PizzaScriptingRuntime::HierarchyChanged");
        }

        private void SceneDirtied(Scene scene)
        {
            LogMessage($"PizzaScriptingRuntime::SceneDirtied \t {scene.name}");
        }

        private void PrefabStageDirtied(PrefabStage obj)
        {
            LogMessage($"PizzaScriptingRuntime::PrefabStageDirtied \t {obj.name}");
        }

        private static void EditorUpdate()
        {
            // Nothing to do here, but keeping it incase we want it in the future
        }

        private void ValidateCurrentPrefabStage()
        {
            if (!VALIDATE_WHILE_PLAYING && Application.isPlaying)
                return;

            // Cache the current prefab stage and return if there is none
            var prefabStage = PrefabStageUtility.GetCurrentPrefabStage();
            if (prefabStage == null)
                return;

            // Get all Pizza monobehaviours and call validate
            var scripts = prefabStage.prefabContentsRoot.GetComponentsInChildren<PizzaMonoBehaviour>(INCLUDE_INACTIVE);
            foreach (var script in scripts)
                script.OnCustomValidate();
        }

        private void ValidateCurrentScene()
        {
            if (!VALIDATE_WHILE_PLAYING && Application.isPlaying)
                return;

            // Find all scripts in the active scene and call on validate
            var findObjectsInactive = INCLUDE_INACTIVE ? FindObjectsInactive.Include : FindObjectsInactive.Exclude;
            var scripts = MonoBehaviour.FindObjectsByType<PizzaMonoBehaviour>(findObjectsInactive, FindObjectsSortMode.None);
            foreach (var script in scripts)
                script.OnCustomValidate();
        }

        private void LogMessage(string message, bool force = false)
        {
            if (LOG_RUNTIME_MESSAGES || force)
                PizzaLogger.Log(message);
        }

        public void Dispose()
        {
            EditorApplication.update -= EditorUpdate;
            EditorApplication.hierarchyChanged -= HierarchyChanged;

            EditorSceneManager.activeSceneChangedInEditMode -= ActiveSceneChangedInEditMode;
            EditorSceneManager.sceneDirtied -= SceneDirtied;
            EditorSceneManager.sceneOpening -= SceneOpening;
            EditorSceneManager.sceneOpened -= SceneOpened;

            PrefabStage.prefabStageOpened -= PrefabStageOpened;
            PrefabStage.prefabStageDirtied -= PrefabStageDirtied;

            AssemblyReloadEvents.afterAssemblyReload -= AfterAssemblyReload;
        }
    }
}