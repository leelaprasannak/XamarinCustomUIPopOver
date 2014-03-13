// Created by Leela Prasanna kumar chintagunta 
// Copyright (c) 2014 Leela Prasanna Kumar. All rights reserved.
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Collections.Generic;

namespace XamarinUIPopOverWithCustomBackground
{
	public partial class XamarinUIPopOverWithCustomBackgroundViewController : UIViewController
	{
		UIPopoverController _mypopView;

		public XamarinUIPopOverWithCustomBackgroundViewController () : base ("XamarinUIPopOverWithCustomBackgroundViewController", null)
		{
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			launchPopoverButton.TouchUpInside += (sender, e) => {
				LaunchPop(sender);
			};
			// Perform any additional setup after loading the view, typically from a nib.
		}

		void LaunchPop(object origin)
		{
			var content = new PopoverContentViewController ();
			content.Items = new List<string> { "Test item1", "Test item2", "Test item3", "Test item4", "Test item5" };
			content.ButtonSelectionCallBack = new PopoverContentViewController.SelectionChanged(PopoverSelected);
			_mypopView = new UIPopoverController (content);	
			_mypopView.PopoverContentSize = new SizeF (content.PopOverContentWidth, content.PopOverContentHeight);
			_mypopView.PopoverBackgroundViewType = typeof(CustomUIPopOverBackgroundView);
			_mypopView.PresentFromRect ((origin as UIView).Frame,this.View, UIPopoverArrowDirection.Any, true);

			//_popOver.ContentViewController.View.BackgroundColor = UIColor.Gray;
			//_popOver.ContentViewController.View.Layer.BorderColor = UIColor.FromRGB(161,161,161).CGColor;

			_mypopView.Delegate = null;
		}

		void PopoverSelected(int buttonindex)
		{
			new UIAlertView ("selection changed", "button"+buttonindex.ToString()+" selected", null, "OK", null).Show ();
			_mypopView.Dismiss (true);
		}

	}
}

