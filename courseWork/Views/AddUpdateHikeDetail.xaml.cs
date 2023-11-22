using courseWork.ViewModel;
namespace courseWork.Views;

public partial class AddUpdateHikeDetail : ContentPage
{

	public AddUpdateHikeDetail(HikeViewModel viewModel)
	{
        InitializeComponent();

        BindingContext = viewModel;
    }   
}
