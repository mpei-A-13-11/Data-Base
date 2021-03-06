﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.ComponentModel;

namespace KIS
{
    /// <summary>
    /// Interaction logic for WindowCompanyPlan.xaml
    /// </summary>
    public partial class WindowCompanyPlan : Window
    {
        KISEntities kisEntites = new KISEntities();
        IBindingList companys;
        public WindowCompanyPlan()
        {
            InitializeComponent();
            companys = ((from e in kisEntites.Companies.Include("CompanyProducts") select e) as IListSource).GetList() as IBindingList;
            listViewCompany.DataContext = companys;
        }

        private void listViewCompany_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ButtonAddRemove.Content = "Добавить";
        }

        private void listViewProductsLists_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ButtonAddRemove.Content = "Удалить";
        }

        private void ButtonAddRemove_Click(object sender, RoutedEventArgs e)
        {
            if (ButtonAddRemove.Content.ToString() == "Добавить")
            {
                WindowHowMuch windowHowMuch = new WindowHowMuch();
                windowHowMuch.ShowDialog();

                if (windowHowMuch.DialogResult.HasValue && windowHowMuch.DialogResult.Value)
                {
                    if (listViewCompany.SelectedItem != null)
                    {
                        foreach (CompanyProduct cp in (listViewCompany.SelectedItem as Company).CompanyProducts)
                        {
                            Tuple<int, string, int> item = new Tuple<int, string, int>(cp.Product, cp.Product1.Name, windowHowMuch.Result * cp.ProuctsNumber);
                            listViewProductsLists.Items.Add(item);
                        }
                        
                    }
                }
            }
            else
            {
                listViewProductsLists.Items.Remove(listViewProductsLists.SelectedItem);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (textBoxPlanName.Text.Length != 0  && datePickerEnd.SelectedDate != null && datePickerStart.SelectedDate != null && listViewProductsLists.Items.Count != 0)
            {
                Plan plan = Plan.CreatePlan(0, textBoxPlanName.Text, datePickerStart.SelectedDate.Value, datePickerEnd.SelectedDate.Value);
                kisEntites.AddToPlans(plan);
                kisEntites.SaveChanges();
                foreach (Tuple<int, string, int> item in listViewProductsLists.Items)
                {
                    kisEntites.AddToProductsLists(new ProductsList { Plan = plan.ID, Product = item.Item1, ProductsNumber = item.Item3 });
                }
                kisEntites.SaveChanges();
                DialogResult = true;
                Close();

            }
            else
            {
                MessageBox.Show("Заполните все поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
