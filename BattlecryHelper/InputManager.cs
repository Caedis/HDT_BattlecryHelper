using System;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using Hearthstone_Deck_Tracker;
using Core = Hearthstone_Deck_Tracker.API.Core;
using System.Windows.Media;

namespace HDT.Plugins.BattlecryHelper
{
    public class InputManager
    {
        private User32.MouseInput _mouseInput;
        private StackPanel _panel;

        private string _selected;

        public InputManager(StackPanel panel)
        {
            _panel = panel;
        }

        public bool Toggle()
        {
            if (_mouseInput == null) {

                _mouseInput = new User32.MouseInput();
                _mouseInput.LmbDown += MouseInputOnLmbDown;
                _mouseInput.LmbUp += MouseInputOnLmbUp;
                _mouseInput.MouseMoved += MouseInputOnMouseMoved;
                return true;
            } else {
                _mouseInput.Dispose();
                _mouseInput = null;
                return false;
            }
        }

        public void Dispose()
        {
            _mouseInput.Dispose();
        }

        private void MouseInputOnLmbDown(object sender, EventArgs eventArgs)
        {
            var pos = User32.GetMousePos();
            var _mousePos = new Point(pos.X, pos.Y);
            if (PointInsideControl(_mousePos, _panel)) {
                _selected = "panel";
            } 
        }

        private void MouseInputOnLmbUp(object sender, EventArgs eventArgs)
        {
            var pos = User32.GetMousePos();
            var p = Core.OverlayCanvas.PointFromScreen(new Point(pos.X, pos.Y));
            if (_selected == "panel") {
                Settings.Default.PanelTop = p.Y;
                Settings.Default.PanelLeft = p.X;
            }

            _selected = null;
        }

        private void MouseInputOnMouseMoved(object sender, EventArgs eventArgs)
        {
            if (_selected == null) {
                return;
            }

            var pos = User32.GetMousePos();
            var p = Core.OverlayCanvas.PointFromScreen(new Point(pos.X, pos.Y));
            if (_selected == "panel") {
                Canvas.SetTop(_panel, p.Y);
                Canvas.SetLeft(_panel, p.X);
            }
        }

        private bool PointInsideControl(Point p, FrameworkElement control)
        {
            var pos = control.PointFromScreen(p);
            return pos.X > 0 && pos.X < control.ActualWidth && pos.Y > 0 && pos.Y < control.ActualHeight;
        }
    }
}
