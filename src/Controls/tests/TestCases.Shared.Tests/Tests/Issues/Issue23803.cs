﻿#if TEST_FAILS_ON_ANDROID && TEST_FAILS_ON_IOS && TEST_FAILS_ON_CATALYST
using NUnit.Framework;
using UITest.Appium;
using UITest.Core;

namespace Microsoft.Maui.TestCases.Tests.Issues
{
	public class Issue23803 : _IssuesUITest
	{
		public override string Issue => "FlyoutItem in overlow menu not fully interactable";

		public Issue23803(TestDevice device)
		: base(device)
		{ }

		[Test]
		[Category(UITestCategories.Shell)]
		public void ClickAroundOverflowMenuItems()
		{
			App.Tap("More");
			App.WaitForElement("Tab18");
			var rect = App.WaitForElement("Tab18").GetRect();
			App.TapCoordinates(rect.X + 80, rect.Y + 15);
			App.WaitForElement("Button18");
			App.Tap("Button18");
		}
	}
}
#endif