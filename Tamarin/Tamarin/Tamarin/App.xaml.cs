using Prism.Unity;
using Tamarin.ViewModels;
using Tamarin.Views;
using Xamarin.Forms;

namespace Tamarin
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer) { }

        protected override void OnInitialized()
        {
            InitializeComponent();

            if (Application.Current.Properties.ContainsKey("isLoggedIn"))
            {
                var isLoggedIn = App.Current.Properties["isLoggedIn"] as string;

                if (isLoggedIn == "true")
                {
                    NavigationService.NavigateAsync("Home/Navigation/Dashboard?message=Glad%20you%20read%20the%20code");
                }
                else
                {
                    NavigationService.NavigateAsync("Navigation/Login");
                }
            }
        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<NavigationPage>("Navigation");
            Container.RegisterTypeForNavigation<Login>( "Login" );
            Container.RegisterTypeForNavigation<Home>( "Home" );
            Container.RegisterTypeForNavigation<Dashboard>();
            Container.RegisterTypeForNavigation<Schedule>();
        }
    }
}
