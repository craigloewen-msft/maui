﻿using NUnit.Framework;
using UITest.Appium;
using UITest.Core;

namespace Microsoft.Maui.AppiumTests.Issues
{
	public class Issue17022 : _IssuesUITest
	{
		public Issue17022(TestDevice device)
			: base(device)
		{ }

		public override string Issue => "UINavigationBar is Translucent";

		// TODO: Add shell navigation bar tests when we can call shell in UITest
		[Test]
        [TestCase("NewNavigationPageButton", false)]
        [TestCase("NewNavigationPageTransparentButton", false)]
        [TestCase("NewNavigationPageTranslucentButton", false)]
        [TestCase("NewNavigationPageTransparentTranslucentButton", false)]
        [TestCase("NewNavigationPageGridButton", false)]
        [TestCase("NewNavigationPageGridTransparentButton", false)]
        // [TestCase("NewNavigationPageGridTranslucentButton", false)] // this test thinks the boxview is at the top of the screen, but it's not. Test this case manually for now.
        [TestCase("NewNavigationPageGridTransparentTranslucentButton", true)]
        [TestCase("NewFlyoutPageButton", false)]
        [TestCase("NewFlyoutPageTransparentButton", false)]
        [TestCase("NewFlyoutPageTranslucentButton", false)]
        [TestCase("NewFlyoutPageTransparentTranslucentButton", false)]
        [TestCase("NewFlyoutPageGridButton", false)]
        [TestCase("NewFlyoutPageGridTransparentButton", false)]
        // [TestCase("NewFlyoutPageGridTranslucentButton", false)] // this test thinks the boxview is at the top of the screen, but it's not. Test this case manually for now.
        [TestCase("NewFlyoutPageGridTransparentTranslucentButton", true)]
		public void Issue17022Test(string testButtonID, bool isTopOfScreen)
		{
			this.IgnoreIfPlatforms(new TestDevice[] { TestDevice.Android, TestDevice.Mac, TestDevice.Windows }, "This test is only for iOS");

            App.WaitForElement(testButtonID).Click();
            var boxView = App.WaitForElement("TopBoxView");
            Assert.NotNull(boxView);
			var rect = boxView.GetRect();
			if (isTopOfScreen)
                Assert.AreEqual(rect.Y, 0);
			else
				Assert.AreNotEqual(rect.Y, 0);
            App.WaitForElement("PopPageButton").Click();
		}
	}
}
