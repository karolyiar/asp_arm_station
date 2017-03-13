using System;
using Glovebox.IoT.Devices.Actuators;

namespace asp_arm.Models
{
    public class LedModell
    {
        static LedModell ledModell = null;
        Led led;

        //GpioPin button;
        private bool state = false;
        private LedModell()
        {
            InitGPIO();
        }
        public static LedModell getLed()
        {
            if (ledModell == null)
            {
                ledModell = new LedModell();
            }
            return ledModell;
        }
        public string GetState()
        {
            return state ? "on" : "off";
        }
        public void SetState(string _state)
        {
            Console.WriteLine("led set start");
            try
            {
                switch (_state)
                {
                    case "on":
                        led.On();
                        state = true;
                        break;
                    case "off":
                        led.Off();
                        state = false;
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("led set failed: " + e);
            }
        }

        private void InitGPIO()
        {
            Console.WriteLine("led init start");
            try
            {
                led = new Led(5);
            }
            catch (Exception e)
            {
                Console.WriteLine("led init failed: " + e);
            }
            /*try
            {
                button = Pi.Gpio[6];
                button.PinMode = GpioPinDriveMode.Input;
                button.InputPullMode = GpioPinResistorPullMode.PullUp;
                button.RegisterInterruptCallback(EdgeDetection.FallingEdge, Toggle);
            }
            catch (Exception e)
            {
                Console.WriteLine("button init failed: " + e);
            }*/
        }

        /*private void Toggle()
        {
            state = !state;
            if (state)
            {
                led.Write(true);
            } else
            {
                led.Write(false);
            }
        }*/
    }
}
