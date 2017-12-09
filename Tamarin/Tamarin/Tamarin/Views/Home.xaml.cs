using Prism.Navigation;
using Xamarin.Forms;

namespace Tamarin.Views
{
    public partial class Home : MasterDetailPage, IMasterDetailPageOptions
    {
        public Home()
        {
            InitializeComponent();
        }

        public bool IsPresentedAfterNavigation => Device.Idiom != TargetIdiom.Phone;
    }
}