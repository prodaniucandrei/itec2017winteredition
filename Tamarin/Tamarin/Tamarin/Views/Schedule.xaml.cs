using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tamarin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Schedule : TabbedPage
    {
        public Schedule()
        {
            InitializeComponent();
        }
    }
}