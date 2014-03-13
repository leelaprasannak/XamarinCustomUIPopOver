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
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace XamarinUIPopOverWithCustomBackground
{
	public partial class PopoverContentViewController : UIViewController
	{

		//need to expose as props
		float cellHeight = 0;
		float seperatorOffset = 0;
		float topspacing = 20;

		float _popOverContentWidth  = 200f; //default
		public float PopOverContentWidth {
			get { return _popOverContentWidth;}
			set { _popOverContentWidth = value; }
		}

		float _popOverContentHeight  = 0;
		public float PopOverContentHeight {
			get {
					_popOverContentHeight = topspacing + cellHeight * Items.Count + 
												seperatorOffset * (Items.Count - 1) + topspacing;
					return _popOverContentHeight;
				}
			set { _popOverContentHeight = value; }
		}

		SelectionChanged _buttonSelectionCallBack;
		public SelectionChanged ButtonSelectionCallBack {
			get { return _buttonSelectionCallBack; }
			set { _buttonSelectionCallBack = value; }
		}

		//callback for any button selection event to be notified to the registered observer ( one at a time)
		public delegate void SelectionChanged(int buttonSelectedindex);

		/// <summary>
		/// List of label titles to be displayed in the button's 
		/// Buttons are listed based on the order of titles
		/// </summary>
		List<string> _items = null;
		public List<string> Items {
			get { return _items; }
			set { _items = value; }
		}
			
		public PopoverContentViewController (IntPtr handle) : base (handle)
		{
		}
		
		public PopoverContentViewController () : base ()
		{
		}
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();


			//instead of loading from xib, Creating the buttons dynamically for demo.
			//You can having varying number of buttons based on your layout linearly horizontal
			//tweak values here for any changes
			int i = 0;
			int j = 1;
			cellHeight = 16f;
			seperatorOffset = 20f;
			foreach (string t in Items) {
				var btn = new UIButton(new RectangleF(11,i*cellHeight+seperatorOffset*j,_popOverContentWidth-22f,
																cellHeight));
				btn.Tag = i;
				//btn.SetBackgroundImage (UIImage.FromFile ("button_u466_mouseDown.png"), UIControlState.Normal);
				//btn.SetBackgroundImage (UIImage.FromFile ("button_u466.png"), UIControlState.Highlighted);
				btn.SetTitle (t, UIControlState.Normal);
				btn.SetTitleColor(UIColor.Black, UIControlState.Highlighted);
				btn.SetTitleColor(UIColor.FromRGB(161,161,161), UIControlState.Normal);
				btn.TitleLabel.TextAlignment = UITextAlignment.Right;
				btn.TouchUpInside += (object sender, EventArgs e) => {
					if ( ButtonSelectionCallBack != null) ButtonSelectionCallBack(btn.Tag);
				};
				View.AddSubview (btn);

				UILabel line = new UILabel (new RectangleF (0+5, i * cellHeight + seperatorOffset * j + cellHeight+seperatorOffset/2, _popOverContentWidth-10, 1));
				line.BackgroundColor = UIColor.FromRGB (237, 238, 239);
				View.AddSubview (line);
				View.AddSubview (btn);
				i++;
				j++;
			}
		}
		
		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			return true;
		}
	}
}
