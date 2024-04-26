﻿using Maui.Controls.Sample;
using NUnit.Framework;
using UITest.Core;

namespace Microsoft.Maui.AppiumTests
{
	public class KeyboardScrollingScrollingPageLargeTitlesTests : UITest
	{
		const string KeyboardScrollingGallery = "Keyboard Scrolling Gallery - Scrolling Page / Large Titles";
		public KeyboardScrollingScrollingPageLargeTitlesTests(TestDevice device)
			: base(device)
		{
		}

		protected override void FixtureSetup()
		{
			base.FixtureSetup();
			App.NavigateToGallery(KeyboardScrollingGallery);
		}

		// [Test]
		// public void EntriesScrollingPageTest()
		// {
		// 	this.IgnoreIfPlatforms(new TestDevice[] { TestDevice.Android, TestDevice.Mac, TestDevice.Windows }, KeyboardScrolling.IgnoreMessage);
		// 	KeyboardScrolling.EntriesScrollingTest(App, KeyboardScrollingGallery);
		// }

		// [Test]
		// public void EditorsScrollingPageTest()
		// {
		// 	this.IgnoreIfPlatforms(new TestDevice[] { TestDevice.Android, TestDevice.Mac, TestDevice.Windows }, KeyboardScrolling.IgnoreMessage);
		// 	KeyboardScrolling.EditorsScrollingTest(App, KeyboardScrollingGallery);
		// }

		// [Test]
		// public void EntryNextEditorTest()
		// {
		// 	this.IgnoreIfPlatforms(new TestDevice[] { TestDevice.Android, TestDevice.Mac, TestDevice.Windows }, KeyboardScrolling.IgnoreMessage);
		// 	KeyboardScrolling.EntryNextEditorScrollingTest(App, KeyboardScrollingGallery);
		// }
	}
}
