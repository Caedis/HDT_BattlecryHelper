using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls;
using Hearthstone_Deck_Tracker;
using HDT.Plugins.BattlecryHelper;

namespace HDT.Plugins.BattlecryHelper
{
    /// <summary>
    /// Interaction logic for SettingsView.xaml
    /// </summary>
    public partial class SettingsView : ScrollViewer
    {

        public enum BattlecrySorting
        {

            Turn = 1,
            Cost = 2,
        }


        private static Flyout _flyout;


        public static Flyout Flyout
        {
            get {
                if (_flyout == null) {
                    _flyout = CreateSettingsFlyout();
                }
                return _flyout;
            }
        }

        private static Flyout CreateSettingsFlyout()
        {
            var settings = new Flyout();
            settings.Position = Position.Left;
            Panel.SetZIndex(settings, 100);
            settings.Header = "Battlecry Settings";
            settings.Content = new SettingsView();
            Core.MainWindow.Flyouts.Items.Add(settings);
            return settings;
        }

        private void BtnUnlock_Click(object sender, RoutedEventArgs e)
        {
            BtnUnlock.Content = Battlecry.Input.Toggle() ? "Lock Battlecry Panel" : "Unlock Battlecry Panel";
        }


        public SettingsView()
        {
            InitializeComponent();

            Settings.Default.PropertyChanged += (sender, e) => {
                Settings.Default.Save();
            };

        }

    }
}
