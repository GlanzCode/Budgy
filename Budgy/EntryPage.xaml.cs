namespace Budgy;

public partial class EntryPage : ContentPage
{
	public EntryPage(EntryViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
    }

    private void ContentPage_Appearing(object sender, EventArgs e)
    {
        if (BindingContext is EntryViewModel viewModel)
            viewModel.ShowPageCommand.Execute(null);
    }
}