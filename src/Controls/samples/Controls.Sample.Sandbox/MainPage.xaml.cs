namespace Maui.Controls.Sample;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
		this.SetAppThemeColor(BackgroundColorProperty, Colors.White, Colors.Black);
	}

	public void OnLightThemeButtonClicked(object sender, EventArgs e)
	{
		if (Application.Current is not null)
		{
			Application.Current.UserAppTheme = AppTheme.Light;
		}
	}

	public void OnDarkThemeButtonClicked(object sender, EventArgs e)
	{
		if (Application.Current is not null)
		{
			Application.Current.UserAppTheme = AppTheme.Dark;
		}
	}
}