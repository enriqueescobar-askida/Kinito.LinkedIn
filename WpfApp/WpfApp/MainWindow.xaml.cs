﻿/**
* MainWindow.xaml.cs
* BY DESKTOP-BG640NB\EESCOBAR
* ON 10-04-2019
* OR 4/10/2019 6:35:18 PM
**/

using WpfApp.DataAccessLayer.Jobs;

namespace WpfApp
{
    using DataAccessLayer.Files;
    using DataAccessLayer.URLs;
    using Microsoft.Win32;
    using System.Collections.Generic;
    using System.IO;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        #region PrivateAttributes
        /// <summary>The thickness</summary>
        private readonly double _thickness = 50.0;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the CsvFile
        /// </summary>
        public CsvFile CsvFile { get; internal set; }

        /// <summary>
        /// Gets or sets the OpenFileDialog
        /// </summary>
        public OpenFileDialog OpenFileDialog { get; internal set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            this.Title = "Amaris Consulting: International consulting company | " + ToString().Split('.')[1];
            this.Left = this._thickness;
            this.Top = this._thickness;
            this.Height = this._thickness * 12.0;
            this.Width = this._thickness * 16.0;
            this.CsvFile = new CsvFile();
            this.InitializeComponent();
            this.WpfAppMainStatusBarProgressBar.Value = this._thickness / 2.0;
        }
        #endregion

        /// <summary>Initializes the WPF application main ListBox.</summary>
        /// <param name="urlLinks">The URL links.</param>
        private void InitializeWpfAppMainListBox(List<UrlLink> urlLinks)
        {
            this.WpfAppMainListBox.SelectionMode = SelectionMode.Single;
            this.WpfAppMainListBox.DisplayMemberPath = "Url";
            this.WpfAppMainListBox.MouseDoubleClick += WpfAppMainListBox_OnMouseDoubleClick;

            foreach (UrlLink urlLink in urlLinks)
                this.WpfAppMainListBox.Items.Add(urlLink);
            /*object selectedItem = this.WpfAppMainListBox.SelectedItem;
            if (selectedItem != null)
            {
                ListBoxItem item = (ListBoxItem)selectedItem;
                ListBox listView = ItemsControl.ItemsControlFromItemContainer(item) as ListBox;
                object index = listView.ItemContainerGenerator.ItemFromContainer(item);
                item.IsEnabled = ((UrlLink)index).IsValid;
            }*/
        }

        /// <summary>Handles the OnMouseDoubleClick event of the WpfAppMainListBox control.
        /// int selectedIndex = this.WpfAppMainListBox.SelectedIndex
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseButtonEventArgs"/> instance containing the event data.</param>
        private void WpfAppMainListBox_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            UrlLink urlLink = (UrlLink)this.WpfAppMainListBox.SelectedItem;

            if (this.WpfAppMainListBox.SelectedItem != null)
                if(!urlLink.IsValid)
                {
                    string showTitle = "Amaris Consulting: International consulting company | URL link message";
                    string showMessage = urlLink.IsValid ? urlLink.Query + "is valid" : urlLink.AbsolutePath + " is invalid " + urlLink.HttpStatusCode;
                    MessageBoxButton showBoxButton = urlLink.IsValid ? MessageBoxButton.OKCancel : MessageBoxButton.OK;
                    MessageBoxImage showBoxImage = urlLink.IsValid ? MessageBoxImage.Question : MessageBoxImage.Error;
                    MessageBoxResult showBoxResultDefault = urlLink.IsValid ? MessageBoxResult.Cancel : MessageBoxResult.OK;
                    MessageBox.Show(
                        this
                        , showMessage
                        , showTitle
                        , showBoxButton
                        , showBoxImage
                        , showBoxResultDefault);
                }
                else
                {
                    System.Net.HttpStatusCode v = urlLink.HttpStatusCode;
                    Clipboard.SetText(urlLink.Link);
                    // System.Diagnostics.Process.Start("iexplore.exe", "http://www.msn.com");Process.Start(urlLink.Link);
                    WebJobPosting webJobPosting = new WebJobPosting(urlLink.Url);
                }
        }

        #region Methods
        /// <summary>
        /// The WpfAppMainOpen_OnClick
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void WpfAppMainOpen_OnClick(object sender, RoutedEventArgs e)
        {
            this.OpenFileDialog = new OpenFileDialog
            {
                AddExtension = true,
                InitialDirectory = new DirectoryInfo(Directory.GetCurrentDirectory()).Root.FullName,
                Filter = CsvFile.Filter
            };
            bool? result = this.OpenFileDialog.ShowDialog(this);

            if (result != null && result == true)
            {
                this.CsvFile.SetFileInfo(new FileInfo(this.OpenFileDialog.FileName));
                this.WpfAppMainStatusBarTextBlockCenter.Text = this.CsvFile.FileInfo.FullName;
                this.WpfAppMainStatusBarProgressBar.Value = 30.0;

                if (this.CsvFile.Read())
                {
                    this.WpfAppMainStatusBarProgressBar.Value = 80.0;
                    this.WpfAppMainStatusBarTextBlockLeft.Text = this.CsvFile.FileInfo.Length + " bytes.";
                    this.WpfAppMainStatusBarTextBlockCenter.Text += " ".PadRight(12, '.');
                    this.WpfAppMainStatusBarTextBlockCenter.Text += this.CsvFile.IsReadable ? " is " : " is not ";
                    this.WpfAppMainStatusBarTextBlockCenter.Text += "readable with " + this.CsvFile.Rows + " rows ";
                    this.WpfAppMainStatusBarTextBlockCenter.Text += this.CsvFile.URLs.Count + " results.";
                    this.WpfAppMainStatusBarProgressBar.Value = 100.0;
                    this.InitializeWpfAppMainListBox(this.CsvFile.URLs);
                }
            }
        }

        /// <summary>
        /// The WpfAppMainSave_OnClick
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void WpfAppMainSave_OnClick(object sender, RoutedEventArgs e) => this.WpfAppMainExit_OnClick(sender, e);

        /// <summary>
        /// The WpfAppMainExit_OnClick
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void WpfAppMainExit_OnClick(object sender, RoutedEventArgs e) => this.Close();

        /// <summary>
        /// The WpfAppMainAbout_OnClick
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void WpfAppMainAbout_OnClick(object sender, RoutedEventArgs e) => this.WpfAppMainExit_OnClick(sender, e);
        #endregion
    }
}
