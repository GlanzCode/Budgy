namespace Budgy;

public partial class EntryPage : ContentPage
{
	public EntryPage(EntryViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
    }
}