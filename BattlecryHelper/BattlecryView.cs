using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using Hearthstone_Deck_Tracker;
using Hearthstone_Deck_Tracker.Controls;
using Card = Hearthstone_Deck_Tracker.Hearthstone.Card;

namespace HDT.Plugins.BattlecryHelper
{
    public class BattlecryView : StackPanel
    {
        private List<Card> _history;
        private HearthstoneTextBlock Label;
        public AnimatedCardList View;


        public BattlecryView()
        {
            Orientation = Orientation.Vertical;


            Label = new HearthstoneTextBlock();
            Label.FontSize = 16;
            Label.TextAlignment = TextAlignment.Center;
            Label.Text = "Played Battlecrys";
            var margin = Label.Margin;
            margin.Top = 20;
            Label.Margin = margin;
            Children.Add(Label);
            Label.Visibility = Visibility.Hidden;

            View = new AnimatedCardList();
            View.Visibility = Visibility.Visible;
            Children.Add(View);
            _history = new List<Card>();
        }


        public void Reset()
        {
            Label.Visibility = Visibility.Hidden;
            View.Visibility = Visibility.Visible;
            _history = new List<Card>();
            View.Update(_history, true);
        }


        public bool Update(Card card)
        {

            var match = _history.FirstOrDefault(a => a.Name == card.Name);
            if (match != null) {
                _history.Remove(match);
                card = match.Clone() as Card;
                card.Count++;
            }

            _history.Add(card);

            Label.Visibility = Visibility.Visible;


            View.Update(_history.OrderBy(a => a.Cost).ToList(), false);



            return true;
        }


        public void Hide()
        {
            Label.Visibility = Visibility.Hidden;
            View.Visibility = Visibility.Hidden;
        }

        public void Show()
        {
            Label.Visibility = Visibility.Visible;
            View.Visibility = Visibility.Visible;
        }



    }
}
