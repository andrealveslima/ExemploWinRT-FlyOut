using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyOutSample
{
    public static class FlyOutHelper
    {
        public const double _settingsWidth = 346;
        public static Windows.UI.Xaml.Controls.Primitives.Popup _settingsPopup;

        public static Windows.UI.Popups.UICommandInvokedHandler SetupFlyOut<USERCONTROLTYPE>()
            where USERCONTROLTYPE : Windows.UI.Xaml.Controls.UserControl
        {
            return (uiCommand) =>
            {
                _settingsPopup = new Windows.UI.Xaml.Controls.Primitives.Popup();
                _settingsPopup.Closed += OnFlyOutPopupClosed;
                Windows.UI.Xaml.Window.Current.Activated += OnFlyOutActivated;
                _settingsPopup.IsLightDismissEnabled = true;
                _settingsPopup.Width = _settingsWidth;
                _settingsPopup.Height = Windows.UI.Xaml.Window.Current.Bounds.Height;

                USERCONTROLTYPE myPane = (USERCONTROLTYPE)Activator.CreateInstance(typeof(USERCONTROLTYPE));

                myPane.Width = _settingsWidth;
                myPane.Height = Windows.UI.Xaml.Window.Current.Bounds.Height;

                _settingsPopup.Child = myPane;
                _settingsPopup.SetValue(Windows.UI.Xaml.Controls.Canvas.LeftProperty, Windows.UI.Xaml.Window.Current.Bounds.Width - _settingsWidth);
                _settingsPopup.SetValue(Windows.UI.Xaml.Controls.Canvas.TopProperty, 0);
                _settingsPopup.IsOpen = true;
            };
        }

        private static void OnFlyOutActivated(object sender, Windows.UI.Core.WindowActivatedEventArgs e)
        {
            if (e.WindowActivationState == Windows.UI.Core.CoreWindowActivationState.Deactivated)
                _settingsPopup.IsOpen = false;
        }

        private static void OnFlyOutPopupClosed(object sender, object e)
        {
            Windows.UI.Xaml.Window.Current.Activated -= OnFlyOutActivated;
        }
    }
}
