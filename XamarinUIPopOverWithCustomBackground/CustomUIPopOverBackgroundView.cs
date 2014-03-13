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

//Ported from custom background view provided by Krzysztof Scianski, at https://github.com/Scianski/KSCustomUIPopover
//It was easy :). Thanks to krzysztof for layout logic
//Reused the backgrounds from the same repo for demo purposes

using System;
using MonoTouch.UIKit;
using System.Drawing;
using MonoTouch.Foundation;

namespace XamarinUIPopOverWithCustomBackground
{
	public class CustomUIPopOverBackgroundView : UIPopoverBackgroundView
	{
		const float  ARROW_WIDTH = 35.0f;
		const float ARROW_HEIGHT = 19.0f;
		const float TOP_CONTENT_INSET = 8.0f;
		const float LEFT_CONTENT_INSET = 8.0f;
		const float BOTTOM_CONTENT_INSET = 8.0f;
		const float RIGHT_CONTENT_INSET = 8.0f;

		UIImage _topArrowImage;
		UIImage _leftArrowImage;
		UIImage _rightArrowImage;
		UIImage _bottomArrowImage;


		float _arrowOffset;
		UIPopoverArrowDirection _arrowDirection;
		UIImageView _arrowImageView;
		UIImageView _popOverBackgroundImageView;

		public CustomUIPopOverBackgroundView (IntPtr handle):base(handle){
		}

		public CustomUIPopOverBackgroundView () {
		}

		[Export ("initWithFrame:")]
		public CustomUIPopOverBackgroundView(RectangleF frame) : base(frame) {
			_popOverBackgroundImageView = new UIImageView(UIImage.FromBundle("popoverbackground.png").CreateResizableImage
				(new UIEdgeInsets(49,46,49,45)));

			_topArrowImage = UIImage.FromBundle ("top-arrow.png");
			_rightArrowImage = UIImage.FromBundle ("right-arrow.png");
			_bottomArrowImage = UIImage.FromBundle ("bottom-arrow.png");
			_leftArrowImage = UIImage.FromBundle ("left-arrow.png");
			this.AddSubview (_popOverBackgroundImageView);

			_arrowImageView = new UIImageView (UIImage.FromBundle("top-arrow.png"));
			this.AddSubview (_arrowImageView);
		}

		public override float ArrowOffset {
			get {
				return _arrowOffset;
			}
			set {
				_arrowOffset = value;
			}
		}

		public override UIPopoverArrowDirection ArrowDirection {
			get {
				return _arrowDirection;
			}
			set {
				_arrowDirection = value;
			}
		}

		[Export ("contentViewInsets")]
		public static new UIEdgeInsets GetContentViewInsets()
		{
			return new UIEdgeInsets (LEFT_CONTENT_INSET, TOP_CONTENT_INSET, RIGHT_CONTENT_INSET, BOTTOM_CONTENT_INSET);
		}

		[Export ("arrowHeight")]
		public static new float GetArrowHeight()
		{
			return ARROW_HEIGHT;
		}

		[Export ("arrowBase")]
		public static new float GetArrowBase()
		{
			return ARROW_WIDTH;
		}

		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();

			float popOverImageOriginX = 0;
			float popOverImageOriginY = 0;

			float popOverImageWidth = this.Bounds.Size.Width;
			float popOverImageHeight = this.Bounds.Size.Height;

			float arrowImageOriginX = 0;
			float arrowImageOriginY = 0;

			float arrowImageWidth = ARROW_WIDTH;
			float arrowImageHeight = ARROW_HEIGHT;

			//value used to make rounded corners
			float cornerRadius = 0;
			switch (this.ArrowDirection) 
			{
			case UIPopoverArrowDirection.Up:
				popOverImageOriginY = ARROW_HEIGHT - 2;
				popOverImageHeight = this.Bounds.Size.Height - ARROW_HEIGHT;
				//calculating arrow x positiong using arrow offset
				arrowImageOriginX = (float)Math.Round((this.Bounds.Size.Width - ARROW_WIDTH) / 2 + this.ArrowOffset);
				arrowImageOriginX = Math.Min (arrowImageOriginX, this.Bounds.Size.Width - ARROW_WIDTH - cornerRadius);
				arrowImageOriginX = Math.Max (arrowImageOriginX, cornerRadius);
				this._arrowImageView.Image = _topArrowImage;
				break;

			case UIPopoverArrowDirection.Down:
				popOverImageHeight = this.Bounds.Size.Height - ARROW_HEIGHT + 2;
				arrowImageOriginX = (float)Math.Round((this.Bounds.Size.Width - ARROW_WIDTH) / 2 + this.ArrowOffset);
				arrowImageOriginX = Math.Min(arrowImageOriginX, this.Bounds.Size.Width - ARROW_WIDTH - cornerRadius);
				arrowImageOriginX = Math.Max(arrowImageOriginX, cornerRadius);
				arrowImageOriginY = popOverImageHeight - 2;
				this._arrowImageView.Image = _bottomArrowImage;
				break;

			case UIPopoverArrowDirection.Left:
				popOverImageOriginX = ARROW_HEIGHT - 2;
				popOverImageWidth = this.Bounds.Size.Width - ARROW_HEIGHT;
				arrowImageOriginY = (float)Math.Round((this.Bounds.Size.Height - ARROW_WIDTH) / 2 + this.ArrowOffset);
				arrowImageOriginY = Math.Min(arrowImageOriginY, this.Bounds.Size.Height - ARROW_WIDTH - cornerRadius);
				arrowImageOriginY = Math.Max(arrowImageOriginY, cornerRadius);
				arrowImageWidth = ARROW_HEIGHT;
				arrowImageHeight = ARROW_WIDTH;
				this._arrowImageView.Image = _leftArrowImage;
				break;

			case UIPopoverArrowDirection.Right:
				popOverImageWidth = this.Bounds.Size.Width - ARROW_HEIGHT + 2;
				arrowImageOriginX = popOverImageWidth - 2;
				arrowImageOriginY = (float)Math.Round ((this.Bounds.Size.Height - ARROW_WIDTH) / 2 + this.ArrowOffset);
				arrowImageOriginY = Math.Min (arrowImageOriginY, this.Bounds.Size.Height - ARROW_WIDTH - cornerRadius);
				arrowImageOriginY = Math.Max (arrowImageOriginY, cornerRadius);
				arrowImageWidth = ARROW_HEIGHT;
				arrowImageHeight = ARROW_WIDTH;
				this._arrowImageView.Image = _rightArrowImage;
				break;

			default:
				popOverImageHeight = this.Bounds.Size.Height - ARROW_HEIGHT - 2;
				break;

			}

			_popOverBackgroundImageView.Frame = new RectangleF (popOverImageOriginX, popOverImageOriginY,
				popOverImageWidth, popOverImageHeight);
			_arrowImageView.Frame = new RectangleF (arrowImageOriginX, arrowImageOriginY, arrowImageWidth, arrowImageHeight);
		}
	}
}

