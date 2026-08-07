namespace Maui.Controls.Sample;

public class PickerControlPage : NavigationPage
{
	private PickerViewModel _viewModel;

	public PickerControlPage()
	{
		_viewModel = new PickerViewModel();
		PushAsync(new PickerControlMainPage(_viewModel));
	}
}

public partial class PickerControlMainPage : ContentPage
{
	private PickerViewModel _viewModel;
	private int _selectedIndexChangedCount;

	public PickerControlMainPage(PickerViewModel viewModel)
	{
		InitializeComponent();
		_viewModel = viewModel;
		BindingContext = _viewModel;
	}

	private async void NavigateToOptionsPage_Clicked(object sender, EventArgs e)
	{
		_viewModel = new PickerViewModel();
		BindingContext = _viewModel;
		await Navigation.PushAsync(new PickerOptionsPage(_viewModel));
		// Reset counter after the rebind-driven event fires but before the user interacts with the Options page.
		_selectedIndexChangedCount = 0;
		SelectedIndexChangedStatusLabel.Text = string.Empty;
		SelectedIndexChangedCountLabel.Text = string.Empty;
		OpenedEventStatusLabel.Text = string.Empty;
		ClosedEventStatusLabel.Text = string.Empty;
	}

	private void Picker_SelectedIndexChanged(object sender, EventArgs e)
	{
		SelectedIndexChangedStatusLabel.Text = "Triggered";
		_selectedIndexChangedCount++;
		SelectedIndexChangedCountLabel.Text = _selectedIndexChangedCount.ToString();
	}

	private void Picker_Opened(object sender, PickerOpenedEventArgs e)
	{
		OpenedEventStatusLabel.Text = "Opened";
	}

	private void Picker_Closed(object sender, PickerClosedEventArgs e)
	{
		ClosedEventStatusLabel.Text = "Closed";
	}
}