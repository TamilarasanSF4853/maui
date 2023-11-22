﻿using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace Controls.Sample.UITests.Elements
{
	class LayoutCoreGalleryPage : ContentPage
	{
		public LayoutCoreGalleryPage()
		{
			var descriptionLabel =
				   new Label { AutomationId = "WaitForStubControl", Text = "Layout Galleries", Margin = new Thickness(2) };

			Title = "Layout Galleries";

			Content = new ScrollView
			{
				Content = new StackLayout
				{
					Padding = new Thickness(2),
					Children =
					{
						descriptionLabel,
						new Label { Text = "AbsoluteLayout" },   
						TestBuilder.NavButton("AbsoluteLayout Position Proportional", () =>
							new PositionProportional(), Navigation),
						TestBuilder.NavButton("Chessboard", () =>
							new ChessboardPage(), Navigation), 
						TestBuilder.NavButton("Stylish Header", () =>
							new StylishHeaderPage(), Navigation),
						new Label { Text = "StackLayout" },
						TestBuilder.NavButton("Horizontal StackLayout", () =>
							new HorizontalStackLayout(), Navigation),
						TestBuilder.NavButton("Vertical StackLayout", () =>
							new VerticalStackLayout(), Navigation),
						TestBuilder.NavButton("Combined StackLayout", () =>
							new CombinedStackLayout(), Navigation),
						TestBuilder.NavButton("StackLayout Alignment", () =>
							new StackLayoutAlignment(), Navigation),
						TestBuilder.NavButton("StackLayout Expansion", () =>
							new StackLayoutExpansion(), Navigation),
						TestBuilder.NavButton("StackLayout Spacing", () =>
							new StackLayoutSpacing(), Navigation),
						new Label { Text = "ScrollView" },
						TestBuilder.NavButton("ScrollView Layout", () =>
							new ScrollViewLayout(), Navigation),
						TestBuilder.NavButton("ScrollView ScrollBar Visibility", () =>
							new ScrollBarVisibility(), Navigation),
						TestBuilder.NavButton("ScrollView ScrollTo", () =>
							new ScrollTo(), Navigation),   
						TestBuilder.NavButton("Empty ScrollView", () =>
							new ScrollTo(), Navigation),
					}
				}
			};
		}
	}
}