using UnityEditor;
using UnityEngine;

namespace Pizza.Runtime.Editor
{
    /// <summary>
    /// 
    /// </summary>
    //[CustomEditor(typeof(PizzaMonoBehaviour), true), CanEditMultipleObjects]
    [CustomEditor(typeof(PizzaMonoBehaviour))]
    public sealed class PizzaMonoBehaviourEditor : UnityEditor.Editor
    {
        private bool _initialized;
        private PizzaMonoBehaviour[] _scripts;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            // Force validate all objects on first show
            if (!_initialized)
            {
                // We use a for so we dont cache a type of 'object' for no reason
                _scripts = new PizzaMonoBehaviour[serializedObject.targetObjects.Length];
                for (int i = 0; i < serializedObject.targetObjects.Length; i++)
                {
                    _scripts[i] = serializedObject.targetObjects[i] as PizzaMonoBehaviour;
                    _scripts[i].OnCustomValidate();
                }

                _initialized = true;
            }

            foreach (var script in _scripts)
                script.ValidateIfRequested();
        }
    }
}