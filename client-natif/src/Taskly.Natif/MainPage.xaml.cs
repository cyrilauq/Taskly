using Taskly.Natif.Application.ViewModels;

namespace Taskly.Natif
{
    public partial class MainPage : ContentPage
    {
        private HomeViewModel _homeViewModel;

        public MainPage(HomeViewModel vm)
        {
            InitializeComponent();

            BindingContext = _homeViewModel = vm;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await _homeViewModel.LoadState();
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("auth/login");
        }
    }

}
