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

            NavigationService.NavigateAsync("Navigation/Login");
        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<NavigationPage>("Navigation");
            Container.RegisterTypeForNavigation<Login>( "Login" );
            Container.RegisterTypeForNavigation<Home>( "Home" );
            Container.RegisterTypeForNavigation<Dashboard>();
        }
    }
}
