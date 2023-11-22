namespace courseWork;
using courseWork.Views;
public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

        Routing.RegisterRoute(nameof(AddUpdateHikeDetail), typeof(AddUpdateHikeDetail));
        Routing.RegisterRoute(nameof(HikeDetailPage), typeof(HikeDetailPage));
    }
}

