namespace Budgy.Feature.Category;

public partial class CategoryPage : ContentPage
{
	public CategoryPage(CategoryViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}