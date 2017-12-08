﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Tamarin.Models
{
    public class HomePageMenuItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public ImageSource Icon { get; set; }

        public Type TargetType { get; set; }
    }
}
