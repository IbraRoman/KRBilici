﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace KRBilici.WinFolder
{
    /// <summary>
    /// Логика взаимодействия для Director.xaml
    /// </summary>
    public partial class Director : Window
    {
        FolderClass.ClassEdit ClassEdit;
        FolderClass.ClassDG classDG;
        SqlConnection sqlConnection = new SqlConnection(App.ConnectionString());

        public Director()
        {
            InitializeComponent();
            classDG = new FolderClass.ClassDG(dgListProduct);
        }

        private void dgListProduct_Loaded(object sender, RoutedEventArgs e)
        {
            classDG.LoadDB("Select * from dbo.ViewProduct");
        }

        private void BtProviderList_Click(object sender, RoutedEventArgs e)
        {
            WinFolder.WinProviderList winProvider = new WinFolder.WinProviderList();
            winProvider.ShowDialog();
        }

        private void BtAddProduct_Click(object sender, RoutedEventArgs e)
        {
            WinFolder.AddProduct addProduct = new WinFolder.AddProduct();
            addProduct.ShowDialog();
        }

        private void BtRedcat_Click(object sender, RoutedEventArgs e)
        {
            if (dgListProduct.SelectedItem == null)
            {
                FolderClass.ClassMB.MBError("Выберете строку");
            }
            else
            {
                FolderClass.ClassProduct.IdProduct = classDG.SelectId();
                try
                {
                    WinFolder.EditProductList editProductList =
                        new EditProductList();
                    editProductList.ShowDialog();
                    classDG.LoadDB("Select * from dbo.ProductList");
                }
                catch (Exception ex)
                {
                    FolderClass.ClassMB.MBError(ex);
                }
            }
        }

        private void TbSearchName_TextChanged(object sender, TextChangedEventArgs e)
        {
            classDG.LoadDB("Select * from dbo.ViewProduct " +
               $"Where NameOfProduct Like '%{TbSearchNameD.Text}%'");
        }
    }
}

