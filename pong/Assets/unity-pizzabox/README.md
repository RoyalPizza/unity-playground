# unity-pizzabox
Pizzabox is a unity package of utilities. Runtimes, logging, etc..


Pizza runtime
The Pizza runtime gives better control over mono behaviours and scriptable objects.
I created this because of how Unity calls OnValidate. Working on games that use alot of
configuration (via ScriptableObjects and prefabs), I need OnValidate called more reliably.

Unity only calls OnValidate under certain circumstances. Typically when its in the scene,
or cached by the AssetLoader. Obviously, Unity does not cache every single object in the
asset loader.

This runtime simply hooks into different calls from the engine and calls OnValidate on all
"Pizza" objects more consistently. Instead of doing the scene/prefab, and "some assets"; 
it will do the opened scene/prefab and "no assets".

This avoids the issue where some assets were updated and some were not. If you want
an asset to be refreshed you must go and select the asset, or make an editor script
that will call the function.

The reason I do not call it on every asset in the proejct is for performance reasons.
That would be very slow. And its also no ideal if you are making rapid changes.


TODO: Something that I think is deceiving is the name. This is more of the "editor runtime". 
For example, bootstraping certain scenes on play coudld have been called "runtime". I will 
change the name once I develop that system, and if I think it is needeed. 