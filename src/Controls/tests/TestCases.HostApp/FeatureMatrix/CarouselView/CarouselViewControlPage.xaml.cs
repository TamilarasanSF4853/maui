using System;
using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;
namespace Maui.Controls.Sample;

public class CarouselViewControlPage : NavigationPage
{
	public CarouselViewControlPage()
	{
		PushAsync(new CarouselViewControlMainPage());
	}
}

public partial class CarouselViewControlMainPage : ContentPage
{
	private CarouselViewViewModel _viewModel;

	public CarouselViewControlMainPage()
	{
		InitializeComponent();
		_viewModel = new CarouselViewViewModel();
		_viewModel.PreviousItemText = "No previous item";
		_viewModel.PreviousItemPosition = "No previous position";
		_viewModel.CurrentItem = _viewModel.Items.FirstOrDefault();
		BindingContext = _viewModel;
	}

	private async void NavigateToOptionsPage_Clicked(object sender, EventArgs e)
	{
		BindingContext = _viewModel = new CarouselViewViewModel();
		_viewModel.PreviousItemText = "No previous item";
		_viewModel.PreviousItemPosition = "No previous position";
		_viewModel.CurrentItem = _viewModel.Items.FirstOrDefault();
		await Navigation.PushAsync(new CarouselViewOptionsPage(_viewModel));
	}

	string _previousItem = null;

	private void OnCarouselView_CurrentItemChanged(object sender, CurrentItemChangedEventArgs e)
	{
		if (BindingContext is CarouselViewViewModel viewModel)
		{
			var currentItem = e.CurrentItem?.ToString();

			if (viewModel.CurrentItemText != currentItem)
			{
				_previousItem = viewModel.CurrentItemText;

				viewModel.PreviousItemText = string.IsNullOrEmpty(_previousItem) ? "No previous item" : _previousItem;
				viewModel.CurrentItemText = currentItem;
			}
		}
	}

	private void OnCarouselView_PositionChanged(object sender, PositionChangedEventArgs e)
	{
		if (BindingContext is CarouselViewViewModel viewModel)
		{
			viewModel.CurrentPosition = e.CurrentPosition;
			viewModel.PreviousItemPosition = e.PreviousPosition.ToString();
		}
	}

	private void OnScrollToButtonClicked(object sender, EventArgs e)
	{
		if (int.TryParse(scrollToIndexEntry.Text, out int index) &&
			index >= 0 && index < carouselView.ItemsSource.Cast<object>().Count())
		{
			carouselView.ScrollTo(index);
		}
	}
}