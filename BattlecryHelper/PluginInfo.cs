using System;
using System.Windows.Controls;
using Hearthstone_Deck_Tracker.API;
using Hearthstone_Deck_Tracker.Plugins;

namespace HDT.Plugins.BattlecryHelper
{
    public class PluginInfo : Hearthstone_Deck_Tracker.Plugins.IPlugin
    {
        Battlecry _cry;
        


        public string Name => "Battlecry Helper";

        public string Description => "Keeps track of played battlecrys.";

        public string ButtonText => "Settings";

        public string Author => "Caedis (/u/510threaded)";

        public Version Version => new Version(0, 1);

        public System.Windows.Controls.MenuItem MenuItem => null;

        public void OnButtonPress()
        {
            SettingsView.Flyout.IsOpen = true;
        }

        public void OnLoad()
        {
            _cry = new Battlecry();
        }

        public void OnUnload()
        {
            _cry.Dispose();
            _cry = null;
        }

        public void OnUpdate()
        {

        }
    }
}
