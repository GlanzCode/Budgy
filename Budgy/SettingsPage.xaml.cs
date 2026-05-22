namespace Budgy;

public partial class SettingsPage : ContentPage
{
	public SettingsPage(SettingsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
    }

    private void ContentPage_Appearing(object sender, EventArgs e)
    {
        if (BindingContext is SettingsViewModel viewModel)
            viewModel.ShowPageCommand.Execute(null);
    }
}