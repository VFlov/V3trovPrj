using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Microsoft.EntityFrameworkCore;
using V3trovPrj.Database;
using V3trovPrj.Models;

namespace V3trovPrj
{
    public partial class MainWindow : Window
    {
        private List<Software> Softwares;
        private WrapPanel _wrapPanel;
        private List<Categories> Categories = new List<Categories>();

        public MainWindow()
        {
            InitializeComponent();
            _wrapPanel = this.FindControl<WrapPanel>("CardWrapPanel");
            comboBoxSort.Items.Add("Все");
            
            using (ApplicationContext db = new ApplicationContext())
            {
                Categories = db.Categories
                               .OrderBy(x => x.SortOrder)
                               .ToList();

                foreach (var category in Categories)
                    comboBoxSort.Items.Add(category.Name);
            }

            InitializeCards();
            comboBoxSort.SelectedValue = "Все";
            //AddSoftware(0);
        }

        async void InitializeCards()
        {
            Softwares = Additions.GetData();
            /*
            using (ApplicationContext db = new ApplicationContext())
            {
                Softwares = db.Softwares.ToList();
            }
            */
        }

        private void AddSoftware(int sortNum)
        {
            _wrapPanel.Children.Clear();

            foreach (var software in sortNum == 0 ? Softwares : Softwares.Where(s => s.CategoryId == sortNum))
            {
                var border = new Border
                {
                    Padding = new Thickness(0),
                    BorderThickness = new Thickness(1),
                    Width = 100,
                    Height = 100,
                    CornerRadius = new CornerRadius(10),
                    BorderBrush = Brushes.Black,
                    Margin = new Thickness(10)
                };

                var stackPanel = new StackPanel();
                border.Child = stackPanel;

                var image = new Image()
                {
                    Source = Additions.ImageDownload(software.IconPath).Result,
                    Width = 60,
                    Height = 60
                };
                stackPanel.Children.Add(image);

                var textBlock = new TextBlock
                {
                    Text = software.Name,
                    FontSize = 12,
                    HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center,
                    Margin = new Thickness(0, 10, 0, 0),
                    Foreground = Brushes.Black
                };
                stackPanel.Children.Add(textBlock);

                border.Tapped += (sender, e) =>
                {
                    var detailWindow = new SoftwareDetailWindow(software);
                    detailWindow.Show();
                };

                _wrapPanel.Children.Add(border);
            }
        }

        private void ComboBox_SelectionChanged(object? sender, Avalonia.Controls.SelectionChangedEventArgs e)
        {
            if (comboBoxSort.SelectedItem.ToString() != "Все")
                AddSoftware(Categories.FirstOrDefault(o => o.Name == comboBoxSort.SelectedItem.ToString()).Id);
            else
                AddSoftware(0);
        }
    }
}