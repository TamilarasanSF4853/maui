using NUnit.Framework;
using UITest.Appium;
using UITest.Core;

namespace Microsoft.Maui.TestCases.Tests.Issues;

public class Issue10025 : _IssuesUITest
{
	public Issue10025(TestDevice device) : base(device)
	{
	}

	public override string Issue => "Assigning null to the SelectedItem of the CollectionView in the SelectionChanged event does not clear the selection as expected";

	[Test]
	[ShardedTestCategory(UITestCategories.CollectionView, shard: 6)]
	public void VerifySelectedItemClearsOnNullAssignment()
	{
		App.WaitForElement("Item1");
		App.Tap("Item1");
		App.WaitForElement("DescriptionLabel");
		App.Tap("DescriptionLabel");
		VerifyScreenshot();
	}
}