﻿using NUnit.Framework;
using UITest.Appium;
using UITest.Core;

namespace Microsoft.Maui.AppiumTests.Issues
{
	public class CarouselViewUpdatePosition : _IssuesUITest
	{
		public CarouselViewUpdatePosition(TestDevice device)
			: base(device)
		{
		}

		public override string Issue => "[Bug] CarouselView Position property fails to update visual while control isn't visible";

		// Issue11224 (src\ControlGallery\src\Issues.Shared\Issue11224.cs
		[Test]
		public void CarouselViewPositionFromVisibilityChangeTest()
		{
			App.WaitForElement("AppearButton");
			App.Click("AppearButton");
			App.WaitForNoElement("Item 4");
			App.Screenshot("Success");
		}
	}
}