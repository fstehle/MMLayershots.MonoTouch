using MonoTouch.UIKit;
using MonoTouch.Foundation;
using MonoTouch.CoreAnimation;
using MonoTouch.ObjCRuntime;
using System.Drawing;
using System;
using MonoTouch.CoreGraphics;

namespace MMLayershots
{
    [Category, BaseType (typeof(CALayer))]
    public partial interface MMLayershots_CALayer
    {
        [Export ("beginHidingSublayers")]
        void BeginHidingSublayers ();

        [Export ("endHidingSublayers")]
        void EndHidingSublayers ();
    }

    public interface IMMLayershotsDelegate
    {
    }

    [BaseType (typeof(NSObject))]
    public interface MMLayershots
    {

        [Export ("delegate", ArgumentSemantic.Assign)]
        IMMLayershotsDelegate Delegate { get; set; }

        [Static, Export ("sharedInstance")]
        MMLayershots SharedInstance { get; }

        [Export ("layershotForScreen:")]
        NSData LayershotForScreen (UIScreen screen);
    }

    [BaseType (typeof(NSObject))]
    [Model][Protocol] 
    public interface MMLayershotsDelegate
    {
        [Abstract, Export ("shouldCreateLayershotForScreen:")]
        MMLayershotsCreatePolicy ShouldCreateLayershotForScreen (UIScreen screen);

        [Export ("willCreateLayershotForScreen:")]
        void  WillCreateLayershotForScreen (UIScreen screen);

        [Export ("didCreateLayershotForScreen:data:")]
        void DidCreateLayershotForScreen (UIScreen screen, NSData data);
    }


    [Category, BaseType (typeof(NSMutableData))]
    public partial interface PSDAdditions_NSMutableData
    {

        [Export ("appendValue:withLength:")]
        void AppendValue (int value, int length);
    }

    [Category, BaseType (typeof(NSData))]
    public partial interface PSDAdditions_NSData
    {
    }

    [BaseType (typeof(NSObject))]
    public partial interface PSDLayer
    {

        [Export ("name", ArgumentSemantic.Retain)]
        string Name { get; set; }

        [Export ("imageData", ArgumentSemantic.Retain)]
        NSData ImageData { get; set; }

        [Export ("opacity")]
        float Opacity { get; set; }

        [Export ("rect", ArgumentSemantic.Assign)]
        RectangleF Rect { get; set; }
    }

    [BaseType (typeof(NSObject))]
    public partial interface PSDWriter
    {

        [Export ("layers", ArgumentSemantic.Retain)]
        NSMutableArray Layers { get; set; }

        [Export ("documentSize", ArgumentSemantic.Assign)]
        SizeF DocumentSize { get; set; }

        [Export ("layerChannelCount")]
        int LayerChannelCount { get; set; }

        [Export ("flattenedData", ArgumentSemantic.Retain)]
        NSData FlattenedData { get; set; }

        [Export ("shouldFlipLayerData")]
        bool ShouldFlipLayerData { get; set; }

        [Export ("shouldUnpremultiplyLayerData")]
        bool ShouldUnpremultiplyLayerData { get; set; }

        [Export ("initWithDocumentSize:")]
        IntPtr Constructor (SizeF s);

        [Export ("addLayerWithCGImage:andName:andOpacity:andOffset:")]
        void AddLayerWithCGImage (CGImage image, string name, float opacity, PointF offset);

        [Export ("createPSDData")]
        NSData CreatePSDData { get; }

    }
}

