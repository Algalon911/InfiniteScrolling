using InfiniteScrolling.ViewModels;

namespace InfiniteScrolling.Views;

public partial class AnimalListView : ContentPage
{
	public AnimalListView(AnimalListViewModel viewModel)
	{
		InitializeComponent();
		BindingContext= viewModel;
	}
}