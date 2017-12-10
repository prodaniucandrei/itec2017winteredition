using Prism.Unity;
using Tamarin.ViewModels;
using Tamarin.Views;
using Xamarin.Forms;
using System.Linq;
using Tamarin.Models;

namespace Tamarin
{
    public partial class App : PrismApplication
    {
        public static SubjectModel subject = new SubjectModel();
        public App(IPlatformInitializer initializer = null) : base(initializer) { }

        protected override void OnInitialized()
        {
            InitializeComponent();
            Application.Current.MainPage = new Login();
            if (Application.Current.Properties.ContainsKey("isLoggedIn"))
            {
                var isLoggedIn = App.Current.Properties["isLoggedIn"] as string;

                if (isLoggedIn == "true")
                {
                    NavigationService.NavigateAsync("Home/Navigation/Dashboard?message=Glad%20you%20read%20the%20code");
                }
                else
                {
                    NavigationService.NavigateAsync("Login");
                }
            }
            NavigationService.NavigateAsync("Login");
        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<NavigationPage>("Navigation");
            Container.RegisterTypeForNavigation<Login>( "Login" );
            Container.RegisterTypeForNavigation<Home>( "Home" );
            Container.RegisterTypeForNavigation<Dashboard>();
            Container.RegisterTypeForNavigation<Schedule>();
            Container.RegisterTypeForNavigation<Colegi>();
            Container.RegisterTypeForNavigation<Orar>();
            Container.RegisterTypeForNavigation<Subject>();
            Container.RegisterTypeForNavigation<SubjectNotice>();
            Container.RegisterTypeForNavigation<OrarNeconfirmate>();
            Container.RegisterTypeForNavigation<OrarConfirmate>();
            Container.RegisterTypeForNavigation<SubjectPrezenta>();
            Container.RegisterTypeForNavigation<SubjectDetalii>();
            Container.RegisterTypeForNavigation<AddNewSubject>();
        }
    }
}
