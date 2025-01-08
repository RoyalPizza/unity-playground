using UnityEngine;

namespace Pizza.Runtime
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// The main purpose of this object is to provide control via the PizzaRuntime.
    /// For more details see that class.
    /// </remarks>
    public class PizzaMonoBehaviour : MonoBehaviour
    {
        /// <summary>
        /// An internal state representing whether or not Unity has 
        /// informed us of a validation request.
        /// </summary>
        private bool _validateRequested;

        private void OnValidate()
        {
            // We listen to this so that if someone changes a value in the inspector,
            // our inspector GUI can validate when its time.
            _validateRequested = true;
        }

        /// <summary>
        /// This works the same as OnValidate, but is called on scene open,
        /// on prefab open, and when OnValidate requests an update while selected.
        /// </summary>
        public virtual void OnCustomValidate()
        {
            _validateRequested = false;
        }

        public void SetFilthy()
        {
#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }

        /// <summary>
        /// Only the inspector GUI and editor tools should call this.
        /// This only calls validate if Unity tried to validate the object previously.
        /// </summary>
        public void ValidateIfRequested()
        {
#if UNITY_EDITOR
            if (_validateRequested)
                OnCustomValidate();
#endif
        }
    }
}