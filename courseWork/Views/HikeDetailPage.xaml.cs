namespace courseWork.Views;
using courseWork.ViewModel;

public partial class HikeDetailPage : ContentPage
{
	public HikeDetailPage(HikeDetailViewModel viewModel)
	{
		InitializeComponent();
        this.BindingContext = viewModel;
    }

}
