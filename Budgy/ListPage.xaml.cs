namespace Budgy;

public partial class ListPage : ContentPage
{
	public ListPage(ListViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

    private void ContentPage_Appearing(object sender, EventArgs e)
    {
		if (BindingContext is ListViewModel viewModel)
			viewModel.ShowPageCommand.Execute(null);
    }
}