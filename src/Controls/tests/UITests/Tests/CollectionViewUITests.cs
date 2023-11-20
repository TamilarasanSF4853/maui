﻿using NUnit.Framework;
using UITest.Appium;
using UITest.Core;

namespace Microsoft.Maui.AppiumTests
{
	class CollectionViewUITests : UITest
	{
		const string CollectionViewGallery = "CollectionView Gallery";

		public CollectionViewUITests(TestDevice device)
			: base(device)
		{
		}

		protected override void FixtureSetup()
		{
			base.FixtureSetup();
			App.NavigateToGallery(CollectionViewGallery);
		}

		protected override void FixtureTeardown()
		{
			base.FixtureTeardown();
			this.Back();
		}

		[Test]
		public async Task CollectionViewVerticalList()
		{
			App.Click("VerticalList");
			App.WaitForElement("TestCollectionView");

			// 1. Refresh the data source to verify that it works.
			App.EnterText("entryUpdate", "0");
			App.Click("btnUpdate");

			// Wait for the collection to update
			await Task.Delay(1000);

			// 2. With a snapshot we verify that the CollectionView is rendered with the correct size.
			VerifyScreenshot();

			this.Back();
		}

		[Test]
		public void CollectionViewHorizontalList()
		{
			App.Click("HorizontalList");
			App.WaitForElement("TestCollectionView");

			// 1. Refresh the data source to verify that it works.
			App.EnterText("entryUpdate", "0");
			App.Click("btnUpdate");

			// 2. With a snapshot we verify that the CollectionView is rendered with the correct size.
			VerifyScreenshot();

			this.Back();
		}

		[Test]
		public void CollectionViewVerticalGrid()
		{
			App.Click("VerticalGrid");
			App.WaitForElement("TestCollectionView");

			// 1. Refresh the data source to verify that it works.
			App.EnterText("entryUpdate", "0");
			App.Click("btnUpdate");

			// 2. With a snapshot we verify that the CollectionView is rendered with the correct size.
			VerifyScreenshot();

			this.Back();
		}

		[Test]
		public void CollectionViewHorizontalGrid()
		{
			App.Click("HorizontalGrid");
			App.WaitForElement("TestCollectionView");

			// 1. Refresh the data source to verify that it works.
			App.EnterText("entryUpdate", "0");
			App.Click("btnUpdate");

			// 2. With a snapshot we verify that the CollectionView is rendered with the correct size.
			VerifyScreenshot();

			this.Back();
		}

		[Test]
		public async Task AddItemsCollectionViewList()
		{
			App.Click("AddRemoveItemsList");
			App.WaitForElement("TestCollectionView");

			// 1. With a snapshot we verify the CollectionView items.
			VerifyScreenshot("AddItemsCollectionViewListBefore");

			// 2. Add a new Item in the Index 0
			App.EnterText("entryInsert", "0");
			App.Click("btnInsert");
			
			// Wait for the animation to finish when adding item
			await Task.Delay(1000);

			// 3. Check if the item has been added correctly.
			VerifyScreenshot("AddItemsCollectionViewListAfter");
		
			this.Back();
		}

		[Test]
		public async Task RemoveItemsCollectionViewList()
		{
			App.Click("AddRemoveItemsList");
			App.WaitForElement("TestCollectionView");

			// 1. With a snapshot we verify the CollectionView items.
			VerifyScreenshot("RemoveItemsCollectionViewListBefore");

			// 2. Add a new Item in the Index 0
			App.EnterText("entryRemove", "0");
			App.Click("btnRemove");
			
			// Wait for the animation to finish when remove item
			await Task.Delay(1000);

			// 3. Check if the item has been added correctly.			
			VerifyScreenshot("RemoveItemsCollectionViewListAfter");
		
			this.Back();
		}

		[Test]
		public async Task ReplaceItemsCollectionViewList()
		{
			App.Click("AddRemoveItemsList");
			App.WaitForElement("TestCollectionView");

			// 1. With a snapshot we verify the CollectionView items.
			VerifyScreenshot("ReplaceItemsCollectionViewListBefore");

			// 2. Add a new Item in the Index 0
			App.EnterText("entryReplace", "0");
			App.Click("btnReplace");
			
			// Wait for the animation to finish when replacing items
			await Task.Delay(1000);

			// 3. Check if the item has been added correctly.			
			VerifyScreenshot("ReplaceItemsCollectionViewListAfter");

			this.Back();
		}

		[Test]
		public async Task AddItemsCollectionViewGrid()
		{
			App.Click("AddRemoveItemsGrid");
			App.WaitForElement("TestCollectionView");

			// 1. With a snapshot we verify the CollectionView items.
			VerifyScreenshot("AddItemsCollectionViewGridBefore");

			// 2. Add a new Item in the Index 0
			App.EnterText("entryInsert", "0");
			App.Click("btnInsert");

			// Wait for the animation to finish when adding item
			await Task.Delay(1000);

			// 3. Check if the item has been added correctly.
			VerifyScreenshot("AddItemsCollectionViewGridAfter");

			this.Back();
		}

		[Test]
		public async Task RemoveItemsCollectionViewGrid()
		{
			App.Click("AddRemoveItemsGrid");
			App.WaitForElement("TestCollectionView");

			// 1. With a snapshot we verify the CollectionView items.
			VerifyScreenshot("RemoveItemsCollectionViewGridBefore");

			// 2. Add a new Item in the Index 0
			App.EnterText("entryRemove", "0");
			App.Click("btnRemove");

			// Wait for the animation to finish when removing item
			await Task.Delay(1000);

			// 3. Check if the item has been added correctly.			
			VerifyScreenshot("RemoveItemsCollectionViewGridAfter");

			this.Back();
		}


		[Test]
		public async Task ReplaceItemsCollectionViewGrid()
		{
			App.Click("AddRemoveItemsGrid");
			App.WaitForElement("TestCollectionView");

			// 1. With a snapshot we verify the CollectionView items.
			VerifyScreenshot("ReplaceItemsCollectionViewGridBefore");

			// 2. Add a new Item in the Index 0
			App.EnterText("entryReplace", "0");
			App.Click("btnReplace");

			// Wait for the animation to finish when replacing items
			await Task.Delay(1000);

			// 3. Check if the item has been added correctly.			
			VerifyScreenshot("ReplaceItemsCollectionViewGridAfter");

			this.Back();
		}

		[Test]
		public void StringEmptyViewAfterFilter()
		{
			App.Click("EmptyViewString");
			App.WaitForElement("TestCollectionView");

			// 1. Filter the items with a non existing term.
			App.EnterText("FilterSearchBar", "no exist");

			// 2. Check if the String EmptyView is visible.
			VerifyScreenshot();

			this.Back();
		}

		[Test]
		public void FilterCollectionViewNoCrash()
		{
			App.Click("EmptyViewString");

			// 1. Filter the items with an existing term.
			App.EnterText("FilterSearchBar", "a");

			// 2. Without exceptions, the test has passed.
			Assert.NotNull(App.AppState);

			this.Back();
		}

		[Test]
		public void RemoveStringEmptyView()
		{
			App.Click("EmptyViewString");
			App.WaitForElement("TestCollectionView");

			// 1. Filter the items with a non existing term.
			App.EnterText("FilterSearchBar", "no exist");

			// 2. Clear filter .
			App.EnterText("FilterSearchBar", "");

			// 3. Check if the CollectionView is visible.
			VerifyScreenshot();

			this.Back();
		}

		[Test]
		public void ViewEmptyViewAfterFilter()
		{
			App.Click("EmptyViewView");
			App.WaitForElement("TestCollectionView");

			// 1. Filter the items with a non existing term.
			App.EnterText("FilterSearchBar", "no exist");

			// 2. Check if the View EmptyView is visible.
			VerifyScreenshot();

			this.Back();
		}

		[Test]
		public void RemoveViewEmptyView()
		{
			App.Click("EmptyViewView");
			App.WaitForElement("TestCollectionView");

			// 1. Filter the items with a non existing term.
			App.EnterText("FilterSearchBar", "no exist");

			// 2. Clear filter .
			App.EnterText("FilterSearchBar", "");

			// 3. Check if the CollectionView is visible.
			VerifyScreenshot();

			this.Back();
		}

		[Test]
		public void TemplateEmptyViewAfterFilter()
		{
			App.Click("EmptyViewTemplateView");
			App.WaitForElement("TestCollectionView");

			// 1. Filter the items with a non existing term.
			App.EnterText("FilterSearchBar", "no exist");

			// 2. Check if the Templated EmptyView is visible.
			VerifyScreenshot();

			this.Back();
		}

		[Test]
		public void RemoveTemplateViewEmptyView()
		{
			App.Click("EmptyViewTemplateView");
			App.WaitForElement("TestCollectionView");

			// 1. Filter the items with a non existing term.
			App.EnterText("FilterSearchBar", "no exist");

			// 2. Clear filter .
			App.EnterText("FilterSearchBar", "");

			// 3. Check if the CollectionView is visible.
			VerifyScreenshot();

			this.Back();
		}

		[Test]
		public void PreselectedItemCollectionView()
		{
			App.Click("PreselectedItem");
			App.WaitForElement("TestCollectionView");

			// 1. Check the preselected item.
			App.WaitForElement("WaitForStubControl");
			VerifyScreenshot();

			this.Back();
		}

		[Test]
		public void PreselectedItemsCollectionView()
		{
			App.Click("PreselectedItems");
			App.WaitForElement("TestCollectionView");

			// 1. Check the preselected items.
			App.WaitForElement("WaitForStubControl");
			VerifyScreenshot();

			this.Back();
		}

		[Test]
		public void ListGrouping()
		{
			App.ScrollTo("ListGrouping", true);
			App.Click("ListGrouping");
			App.WaitForElement("TestCollectionView");

			// 1. Check the grouped CollectionView layout.
			VerifyScreenshot();

			this.Back();
		}

		[Test]
		public void GridGrouping()
		{
			App.ScrollTo("GridGrouping", true);
			App.Click("GridGrouping");
			App.WaitForElement("TestCollectionView");

			// 1. Check the grouped CollectionView layout.
			VerifyScreenshot();

			this.Back();
		}

		[Test]
		public void StringHeaderFooter()
		{
			App.ScrollTo("HeaderFooterString", true);
			App.Click("HeaderFooterString");

			// 1. Check CollectionView header and footer using a string.
			App.WaitForElement("TestCollectionView");
			VerifyScreenshot();

			this.Back();
		}

		[Test]
		public void ViewHeaderFooter()
		{
			this.IgnoreIfPlatforms(new TestDevice[] { TestDevice.iOS },	
				"Currently fails on Android.");
	
				App.ScrollTo("HeaderFooterView", true);
			App.Click("HeaderFooterView");

			// 1. Both Header and Footer must be visible with or without items.
			// Let's add items.
			App.Click("AddButton");

			// 2. Clear the items.
			App.Click("ClearButton");

			// 3. Repeat the previous steps.
			App.Click("AddButton");
			App.Click("ClearButton");

			// 3. Check CollectionView header and footer using a View.
			App.WaitForElement("TestCollectionView");
			VerifyScreenshot();

			this.Back();
		}

		[Test]
		public void TemplateHeaderFooter()
		{
			App.ScrollTo("HeaderFooterTemplate", true);
			App.Click("HeaderFooterTemplate");

			// 1. Check CollectionView header and footer using a TemplatedView.
			App.WaitForElement("TestCollectionView");
			VerifyScreenshot();

			this.Back();
		}
	}
}