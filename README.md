# MMLayershots.MonoTouch

MonoTouch (Xamarin.iOS) bindings for [MMLayershots](https://github.com/vpdn/MMLayershots)

## Requirements

* iOS SDK
* [Xamarin Studio](https://xamarin.com/studio)
* Cocoapods
* Make
* NuGet (optional for packaging)

## Building the bindings

Just run

	make

This downloads sources via CocoaPods, creates the static objective-c library and finally creates a .dll-file.

## Creating the NuGet package

Run

	make package

## Pushing the NuGet package to a private feed

Setup a private NuGet feed, for example on [myget](https://www.myget.org/).
Push the package to the feed source:

	nuget push MMLayershots.$(version).nupkg -Source "$(source_url)" -ApiKey "$(api_key)"

## How to use Layershots?

Add the generated ddl-File to your Project or use NuGet to import it.

In the Application delegate, initialize the MMLayershots shared instance:

```csharp
public override bool FinishedLaunching (UIApplication application, NSDictionary launchOptions)
{
    MMLayershots.MMLayershots.SharedInstance.Delegate = new MyMMLayershotsDelegate ();
    return true;
}
```

Create a subclass of ``MMLayershotsDelegate``:

```csharp
private class MyMMLayershotsDelegate: MMLayershotsDelegate
{
    public override MMLayershotsCreatePolicy ShouldCreateLayershotForScreen (UIScreen screen)
    {
    		return MMLayershotsCreatePolicy.OnUserRequestPolicy;
    }

		...
}
```

There's only on then mandatory delegate method, that tells Layershots what to do when a screenshot is taken:

```csharp
public override MMLayershotsCreatePolicy ShouldCreateLayershotForScreen (UIScreen screen)
{
    return MMLayershotsCreatePolicy.OnUserRequestPolicy;
}
```

Return ``MMLayershotsCreatePolicy.NeverPolicy`` to disable Layershots, ``MMLayershotsCreatePolicy.OnUserRequestPolicy`` to pass on the request to the user (popup) or ``MMLayershotsCreatePolicy.NowPolicy`` to trigger the generation of a psd immediately.

There are two optional delegate methods, one called before (``WillCreateLayershotForScreen``) and one after (``DidCreateLayershotForScreen``) the psd has been generated. Use the later to save the data into a file or present 'Open in...' options to the user.

```csharp
public override void WillCreateLayershotForScreen (UIScreen screen)
{
    Console.WriteLine ("Creating psd now...");
}

public override void DidCreateLayershotForScreen (UIScreen screen, NSData data)
{
    var documentsDirectory = Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments);
    string filePath = System.IO.Path.Combine (documentsDirectory, "layershots_" + DateTime.Now.ToString () + ".psd");
    NSError err = null;
    data.Save (filePath, false, out err);
    Console.WriteLine ("Saving psd to {0}", filePath);
}
```
