using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Gpio;

namespace GpioWrapper
{

    public class Input
    {
        private int pinNumber;
        private GpioPin gpioPin;
        public delegate void ChangedEventHandler(Input sender, PinEdge e);
        public event ChangedEventHandler Changed;

        public Input(int pinNumber)
        {
            this.pinNumber = pinNumber;
        }
        public void InitGPIO()
        {
            var gpio = GpioController.GetDefault();

            // Show an error if there is no GPIO controller
            if (gpio == null)
            {
                throw new InvalidOperationException("There is no GPIO controller on this device.");
            }

            gpioPin = gpio.OpenPin(pinNumber);
            // Check if input pull-up resistors are supported
            if (gpioPin.IsDriveModeSupported(GpioPinDriveMode.InputPullUp))
                gpioPin.SetDriveMode(GpioPinDriveMode.InputPullUp);
            else
                gpioPin.SetDriveMode(GpioPinDriveMode.Input);
            // Set a debounce timeout to filter out switch bounce noise from a button press
            gpioPin.DebounceTimeout = TimeSpan.FromMilliseconds(50);
            // Register for the ValueChanged event so our buttonPin_ValueChanged
            // function is called when the button is pressed
            gpioPin.ValueChanged += pin_ValueChanged;
        }

        public PinValue GetValue()
        {
            return gpioPin.Read() == GpioPinValue.High ? PinValue.High : PinValue.Low;
        }

        private void pin_ValueChanged(GpioPin sender, GpioPinValueChangedEventArgs e)
        {
            Changed(this, e.Edge == GpioPinEdge.FallingEdge ? PinEdge.FallingEdge : PinEdge.RisingEdge);
        }
    }

}
