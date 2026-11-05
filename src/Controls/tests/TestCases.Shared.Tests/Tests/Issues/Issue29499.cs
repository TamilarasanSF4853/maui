using NUnit.Framework;
using UITest.Appium;
using UITest.Core;

namespace Microsoft.Maui.TestCases.Tests.Issues;

public class Issue29499 : _IssuesUITest
{
	public Issue29499(TestDevice testDevice) : base(testDevice) { }

	public override string Issue => "[Android] The number of SearchHandler toolbar item increases abnormally";

	[Test]
	[Category(UITestCategories.SearchBar)]
	public void NumberOfToolbarItemsShouldNotIncrease()
	{
		App.WaitForElement("GotoIssue29499Subpage");
		App.Click("GotoIssue29499Subpage");
		App.WaitForElement("GoBackButton");
		App.Click("GoBackButton");
		App.WaitForElement("GotoIssue29499Subpage");
		App.WaitForMoreButton();
		App.TapMoreButton();
		VerifyScreenshot();
	}
}