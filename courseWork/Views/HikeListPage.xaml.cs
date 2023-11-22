using AndroidX.Lifecycle;
using static Android.App.Assist.AssistStructure;
using courseWork.ViewModel;
namespace courseWork.Views;

public partial class HikeListPage : ContentPage
{
    private readonly HikeViewModel _viewModel;
    public HikeListPage(HikeViewModel viewModel)
	{
		InitializeComponent();

        BindingContext = viewModel;
        _viewModel = viewModel;
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadHikeAsync();
    }
}
