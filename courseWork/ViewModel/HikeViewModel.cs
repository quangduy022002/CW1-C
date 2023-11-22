using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using courseWork.Data;
using courseWork.Models;
using System.Collections.ObjectModel;
using courseWork.Views;
using Xamarin.KotlinX.Coroutines.Channels;
using static Android.Content.ClipData;

namespace courseWork.ViewModel
{
 
    public partial class HikeViewModel : ObservableObject 
	{
		private readonly DatabaseContext _context;
		public HikeViewModel(DatabaseContext context)
		{
            _context = context;
		}

		[ObservableProperty]
		private ObservableCollection<Hike> _hikes = new();

		[ObservableProperty]
		private Hike _operatingHike = new();

		[ObservableProperty]
		private bool _isLoading;

		[ObservableProperty]
		private string _loadingText;

        public static List<Hike> HikesListForSearch { get; private set; } = new List<Hike>();

        public List<string> DifficultyLevels { get; } = new List<string> { "High", "Medium", "Low" };

        public async Task LoadHikeAsync()
		{
			await ExecuteAsync(async () =>
			{
				Hikes.Clear();
				var hikes = await _context.GetAllAsync<Hike>();
				if (hikes is not null && hikes.Any())
				{
					Hikes ??= new ObservableCollection<Hike>();

					foreach (var hike in hikes)
					{
						Hikes.Add(hike);
					}
					HikesListForSearch.Clear();

                    HikesListForSearch.AddRange(Hikes);
				}
            }, "Loading...");
        }

		[RelayCommand]
        private void SetOperatingHike(Hike? hike) => OperatingHike = hike ?? new();



        [RelayCommand]
		private async Task SaveHikeAsync()
		{
			if (OperatingHike is null)
				return;

            var (isValid, errorMessage) = OperatingHike.Validate();
            if (!isValid)
            {
                await Shell.Current.DisplayAlert("Validation Error", errorMessage, "Ok");
                return;
            }


            var loadingText = OperatingHike.Id == 0 ? "Creating hike..." : "Updating hike...";

			await ExecuteAsync(async () =>
			{
				if (OperatingHike.Id == 0)
				{
					//Create
					await _context.AddItemAsync<Hike>(OperatingHike);
					Hikes.Add(OperatingHike);
                    await Shell.Current.DisplayAlert("Add new trip", "Successfully", "Ok");
                }
                else
				{
					//update
					if (await _context.UpdateItemAsync<Hike>(OperatingHike))
					{
						var hikeCopy = OperatingHike.Clone();
						var index = Hikes.IndexOf(OperatingHike);
						Hikes.RemoveAt(index);

						Hikes.Insert(index, hikeCopy);
                        await Shell.Current.DisplayAlert("Edit a trip", "Successfully", "Ok");
                    }
                    else
                    {
                        await Shell.Current.DisplayAlert("Error", "Hike updation error", "Ok");
                        return;
                    }
                }
				SetOperatingHikeCommand.Execute(new());
            }, loadingText);
		}

		[RelayCommand]
		private async Task DeleteHikeAsync(int id)
		{
			await ExecuteAsync(async () =>
			{
				if (await _context.DeleteItemByKeyAsync<Hike>(id))
				{
					var hike = Hikes.FirstOrDefault(h => h.Id == id);
					Hikes.Remove(hike);
				}
				else
				{
					await Shell.Current.DisplayAlert("Delete Error", "Hike was not deleted", "Ok");
				}
			}, "Deleting hike...");
		}

		private async Task ExecuteAsync(Func<Task> operation, string? loadingText = null)
		{
			IsLoading = true;
			LoadingText = loadingText ?? "Processing...";
			try
			{
				await operation?.Invoke();
			} finally
			{
                IsLoading = false;
                LoadingText = "Processing...";
            }
		}


        [RelayCommand]
        public async void AddUpdateHike()
        {
            await AppShell.Current.GoToAsync(nameof(AddUpdateHikeDetail));
        }
        [RelayCommand]
        public async void MoveToDetailPage(Hike hike)
        {
            var navParam = new Dictionary<string, object>();
            navParam.Add("HikeDetail", hike);
            await AppShell.Current.GoToAsync(nameof(HikeDetailPage), navParam);
        }

        [RelayCommand]
        private async Task<Hike> MoveToEditPage(Hike? hike)
        {
            await Shell.Current.GoToAsync(nameof(AddUpdateHikeDetail));
            OperatingHike = hike ?? new Hike();
            return OperatingHike;
        }

    }
}

