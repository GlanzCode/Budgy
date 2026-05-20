namespace Budgy
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        private void ContentPage_Appearing(object sender, EventArgs e)
        {
            if (BindingContext is MainViewModel viewModel)
                viewModel.ShowPageCommand.Execute(null);        
        }
    }
}
