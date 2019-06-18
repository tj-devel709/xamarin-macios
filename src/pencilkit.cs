//
// PencilKit C# bindings
//
// Authors:
//	TJ Lambert  <t-anlamb@microsoft.com>
//
// Copyright 2019 Microsoft Corporation All rights reserved.
//

#if MONOMAC
using AppKit;
using UIColor = AppKit.NSColor;
using UIImage = AppKit.NSImage;
#else
using UIKit;
#endif

using System;
using ObjCRuntime;
using Foundation;
using CoreGraphics;

namespace PencilKit {

	[iOS (13, 0), Mac (10, 15, onlyOn64: true)]
	[Native]
	enum PKEraserType : long {
		Vector,
		Bitmap,
	}

	[iOS (13, 0), Mac (10, 15, onlyOn64: true)]
	enum PKInkType {
		[Field ("PKInkTypePen")]
		Pen,

		[Field ("PKInkTypePencil")]
		Pencil,

		[Field ("PKInkTypeMarker")]
		Marker,
	}

	[iOS (13, 0), Mac (10, 15, onlyOn64: true)]
	[BaseType (typeof (NSObject))]
	[Model (AutoGeneratedName = true)] [Protocol]
	interface PKCanvasViewDelegate : IUIScrollViewDelegate {

		[Export ("canvasViewDrawingDidChange:")]
		void DrawingDidChange (PKCanvasView canvasView);

		[Export ("canvasViewDidFinishRendering:")]
		void DidFinishRendering (PKCanvasView canvasView);

		[Export ("canvasViewDidBeginUsingTool:")]
		void DidBeginUsingTool (PKCanvasView canvasView);

		[Export ("canvasViewDidEndUsingTool:")]
		void EndUsingTool (PKCanvasView canvasView);
	}

	interface IPKCanvasViewDelegate {}

	[iOS (13, 0), Mac (10, 15, onlyOn64: true)]
	[BaseType (typeof (UIScrollView))]
	interface PKCanvasView : PKToolPickerObserver {

		// This exists in the base class
		//[Export ("delegate", ArgumentSemantic.Weak), NullAllowed]
		//NSObject WeakDelegate { get; set; }

		[Wrap ("WeakDelegate"), NullAllowed, New]
		IPKCanvasViewDelegate Delegate { get; set; }

		[Export ("drawing", ArgumentSemantic.Copy)]
		PKDrawing Drawing { get; set; }

		[Export ("tool", ArgumentSemantic.Copy)]
		PKTool Tool { get; set; }

		[Export ("rulerActive")]
		bool RulerActive { [Bind ("isRulerActive")] get; set; }

		[Export ("drawingGestureRecognizer")]
		UIGestureRecognizer DrawingGestureRecognizer { get; }

		[Export ("allowsFingerDrawing")]
		bool AllowsFingerDrawing { get; set; }
	}

	[iOS (13, 0), Mac (10, 15, onlyOn64: true)]
	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PKDrawing : INSCopying, INSSecureCoding {

		[DesignatedInitializer]
		[Export("init")]
		IntPtr Constructor();

		[DesignatedInitializer]
		[Export ("initWithData:error:")]
		IntPtr Constructor (NSData data, [NullAllowed] out NSError error);

		[Export ("dataRepresentation")]
		NSData DataRepresentation { get; }

		[Export ("bounds")]
		CGRect Bounds { get; }

		[Export ("imageFromRect:scale:")]
		UIImage GetImage (CGRect rect, nfloat scale);

		[Export ("drawingByApplyingTransform:")]
		PKDrawing GetDrawing (CGAffineTransform transform);

		[Export ("drawingByAppendingDrawing:")]
		PKDrawing GetDrawing (PKDrawing drawing);
	}

	[iOS (13, 0), Mac (10, 15, onlyOn64: true)]
	[BaseType (typeof (PKTool))]
	[DisableDefaultCtor]
	interface PKEraserTool {

		[Export ("eraserType")]
		PKEraserType EraserType { get; }

		[DesignatedInitializer]
		[Export ("initWithEraserType:")]
		IntPtr Constructor (PKEraserType eraserType);
	}

	[iOS (13, 0), Mac (10, 15, onlyOn64: true)]
	[BaseType (typeof (PKTool))]
	[DisableDefaultCtor]
	interface PKInkingTool {

		[DesignatedInitializer]
		[Export ("initWithInkType:color:width:")]
		IntPtr Constructor ([BindAs (typeof (PKInkType))] NSString type, UIColor color, nfloat width);

		[Export ("initWithInkType:color:")]
		IntPtr Constructor ([BindAs (typeof (PKInkType))] NSString type, UIColor color);

		[Static]
		[Export ("defaultWidthForInkType:")]
		nfloat GetDefaultWidth ([BindAs (typeof (PKInkType))] NSString inkType);

		[Static]
		[Export ("minimumWidthForInkType:")]
		nfloat GetMinimumWidth ([BindAs (typeof (PKInkType))] NSString inkType);

		[Static]
		[Export ("maximumWidthForInkType:")]
		nfloat GetMaximumWidth ([BindAs (typeof (PKInkType))] NSString inkType);

		[Export ("inkType")]
		[BindAs (typeof (PKInkType))]
		NSString InkType { get; }

		[Export ("color")]
		UIColor Color { get; }

		[Export ("width")]
		nfloat Width { get; }
	}

	[iOS (13, 0), Mac (10, 15, onlyOn64: true)]
	[BaseType (typeof (PKTool))]
	[DisableDefaultCtor]
	interface PKLassoTool {

		[DesignatedInitializer]
		[Export("init")]
		IntPtr Constructor();

	}

	[iOS (13, 0), Mac (10, 15, onlyOn64: true)]
	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PKTool : INSCopying {}

	interface IPKToolPickerObserver {}

	[iOS (13, 0), Mac (10, 15, onlyOn64: true)]
	[Protocol]
	interface PKToolPickerObserver {

		[Export ("toolPickerSelectedToolDidChange:")]
		void SelectedToolDidChange (PKToolPicker toolPicker);

		[Export ("toolPickerIsRulerActiveDidChange:")]
		void IsRulerActiveDidChange (PKToolPicker toolPicker);

		[Export ("toolPickerVisibilityDidChange:")]
		void VisibilityDidChange (PKToolPicker toolPicker);

		[Export ("toolPickerFramesObscuredDidChange:")]
		void FramesObscuredDidChange (PKToolPicker toolPicker);
	}

	[iOS (13, 0), Mac (10, 15, onlyOn64: true)]
	[DisableDefaultCtor]
	[BaseType (typeof (NSObject))]
	interface PKToolPicker {

		[Export ("addObserver:")]
		void AddObserver (IPKToolPickerObserver observer);

		[Export ("removeObserver:")]
		void RemoveObserver (IPKToolPickerObserver observer);

		[Export ("setVisible:forFirstResponder:")]
		void SetVisible (bool visible, UIResponder responder);

		[Export ("selectedTool", ArgumentSemantic.Strong)]
		PKTool SelectedTool { get; set; }

		[Export ("rulerActive")]
		bool RulerActive { [Bind ("isRulerActive")] get; set; }

		[Export ("isVisible")]
		bool IsVisible { get; }

		[Export ("frameObscuredInView:")]
		CGRect GetFrameObscured (UIView view);

		[Static]
		[return: NullAllowed]
		[Export ("sharedToolPickerForWindow:")]
		PKToolPicker GetSharedToolPicker (UIWindow window);
	}
}
