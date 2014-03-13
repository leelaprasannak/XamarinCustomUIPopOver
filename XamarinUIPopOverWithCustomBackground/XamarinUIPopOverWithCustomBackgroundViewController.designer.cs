// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace XamarinUIPopOverWithCustomBackground
{
	[Register ("XamarinUIPopOverWithCustomBackgroundViewController")]
	partial class XamarinUIPopOverWithCustomBackgroundViewController
	{
		[Outlet]
		MonoTouch.UIKit.UIButton launchPopoverButton { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (launchPopoverButton != null) {
				launchPopoverButton.Dispose ();
				launchPopoverButton = null;
			}
		}
	}
}
