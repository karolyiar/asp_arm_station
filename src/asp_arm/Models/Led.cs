using System;
using GpioWrapper;

namespace asp_arm.Models
{
    public class LedModell
    {
        static LedModell ledModell = null;
        Led led;
        
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
                led = new Led();
                led.Init(5, GpioWrapper.PinValue.Low);
            }
            catch (Exception e)
            {
                Console.WriteLine("led init failed: " + e);
            }
        }

        public void Toggle()
        {
            state = !state;
            if (state)
            {
                led.Write(PinValue.High);
            } else
            {
                led.Write(PinValue.Low);
            }
        }
    }
}
