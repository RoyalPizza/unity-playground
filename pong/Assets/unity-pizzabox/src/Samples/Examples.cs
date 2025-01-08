namespace Pizza.Runtime.Samples
{
    public class Examples { }

    public class ExampleMonoBehaviour : PizzaMonoBehaviour
    {
        private void Start()
        {
            PizzaLogger.Log($"{name} started");
        }

        public override void OnCustomValidate()
        {
            base.OnCustomValidate();
            PizzaLogger.Log($"{name} validated");
        }
    }

    public class ExampleScriptableObject : PizzaScriptableObject
    {
        public override void OnCustomValidate()
        {
            base.OnCustomValidate();
            PizzaLogger.Log($"{name} validated");
        }
    }
}