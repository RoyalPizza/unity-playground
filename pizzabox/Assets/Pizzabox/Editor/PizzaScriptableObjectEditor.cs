using UnityEditor;
using UnityEngine;

namespace Pizza.Runtime.Editor
{
    /// <summary>
    /// 
    /// </summary>
    //[CustomEditor(typeof(PizzaScriptableObject), true), CanEditMultipleObjects]
    [CustomEditor(typeof(PizzaScriptableObject))]
    public sealed class PizzaScriptableObjectEditor : UnityEditor.Editor
    {
        private bool _initialized;
        private PizzaScriptableObject[] _scripts;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            // Force validate all objects on first show
            if (!_initialized)
            {
                // We use a for so we dont cache a type of 'object' for no reason
                _scripts = new PizzaScriptableObject[serializedObject.targetObjects.Length];
                for (int i = 0; i < serializedObject.targetObjects.Length; i++)
                {
                    _scripts[i] = serializedObject.targetObjects[i] as PizzaScriptableObject;
                    _scripts[i].OnCustomValidate();
                }

                _initialized = true;
            }

            foreach (var script in _scripts)
                script.ValidateIfRequested();
        }
    }
}