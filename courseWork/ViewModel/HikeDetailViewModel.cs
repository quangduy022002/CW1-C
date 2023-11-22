using System;
using courseWork.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using courseWork.Data;
using CommunityToolkit.Mvvm.Input;
using Android.Util;
using courseWork.Views;

namespace courseWork.ViewModel
{
    [QueryProperty(nameof(HikeDetail), "HikeDetail")]
    public partial class HikeDetailViewModel : ObservableObject
	{

        [ObservableProperty]
        private Hike _hikeDetail = new Hike();

        [RelayCommand]
        private void SetHikeDetail(Hike? hike) => HikeDetail = hike ?? new();
    
    }
}


