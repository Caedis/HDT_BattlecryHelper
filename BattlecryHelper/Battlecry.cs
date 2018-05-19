using System;
using System.Linq;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Collections.Generic;
using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.API;
using Core = Hearthstone_Deck_Tracker.API.Core;
using Card = Hearthstone_Deck_Tracker.Hearthstone.Card;

namespace HDT.Plugins.BattlecryHelper
{
    public class Battlecry
    {
        private BattlecryView _view = null;
        private StackPanel _panel;

        public static InputManager Input;

        public Battlecry()
        {
            _panel = new StackPanel();
            _panel.Orientation = Orientation.Vertical;
            Core.OverlayCanvas.Children.Add(_panel);
            Canvas.SetTop(_panel, Settings.Default.PanelTop);
            Canvas.SetLeft(_panel, Settings.Default.PanelLeft);

            Input = new InputManager(_panel);

            Settings.Default.PropertyChanged += SettingsChanged;
            SettingsChanged(null, null);

            _view = new BattlecryView();

            _panel.Children.Add(_view);


            GameEvents.OnGameStart.Add(GameStart);
            GameEvents.OnPlayerPlay.Add(PlayerPlay);
            GameEvents.OnInMenu.Add(InMenu);
        }

        private void SettingsChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            _panel.RenderTransform = new ScaleTransform(Settings.Default.PanelScale / 100, Settings.Default.PanelScale / 100);
            _panel.Opacity = Settings.Default.PanelOpacity / 100;


            _view?.Update(null);
        }

        public void Dispose()
        {
            Core.OverlayCanvas.Children.Remove(_panel);
            Input.Dispose();
        }

        internal void InMenu()
        {    
            _view.Hide();
        }

        internal void GameStart()
        {
            _view.Reset();
        }

        internal void PlayerPlay(Card card)
        {
            if (card.Mechanics.Any(a => a == "Battlecry")) {
                _view.Update(card);
            }
        }

    }
}
