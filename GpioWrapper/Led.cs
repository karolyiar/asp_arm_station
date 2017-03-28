using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Gpio;

namespace GpioWrapper
{
    public sealed class Led
    {
        private int pinNumber;
        private GpioPin gpioPin;
        private PinValue pinValue;
        private bool init = false;

        public void Init(int pinNumber, PinValue startValue)
        {
            this.pinNumber = pinNumber;
            this.pinValue = startValue;

            init = true;
            var gpio = GpioController.GetDefault();

            // Show an error if there is no GPIO controller
            if (gpio == null)
            {
                throw new InvalidOperationException("There is no GPIO controller on this device.");
            }

            gpioPin = gpio.OpenPin(pinNumber);

            gpioPin.SetDriveMode(GpioPinDriveMode.Output);
            gpioPin.Write(pinValue == PinValue.High ? GpioPinValue.High : GpioPinValue.Low);
        }

        public void On()
        {
            if (!init)
                return;
            pinValue = PinValue.High;
            gpioPin.Write(pinValue == PinValue.High ? GpioPinValue.High : GpioPinValue.Low);
        }
        public void Off()
        {
            if (!init)
                return;
            pinValue = PinValue.Low;
            gpioPin.Write(pinValue == PinValue.High ? GpioPinValue.High : GpioPinValue.Low);
        }

        public void Write(PinValue pinValue)
        {
            if (!init)
                return;
            this.pinValue = pinValue;
            gpioPin.Write(pinValue == PinValue.High ? GpioPinValue.High : GpioPinValue.Low);
        }
    }
}
