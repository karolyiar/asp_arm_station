using System;
using GpioWrapper;

namespace asp_arm.Models
{
    public class LedModel
    {
        private static volatile LedModel ledModel = null;
        Led led;

        private bool state = false;
        private LedModel()
        {
            InitGPIO();
        }
        public static LedModel Instance
        {
            get
            {
                if (ledModel == null)
                {
                    ledModel = new LedModel();
                }
                return ledModel;
            }
        }
        public string State
        {
            get
            {
                return state ? "on" : "off";
            }
            set
            {
                Console.WriteLine("led set start");
                try
                {
                    switch (value)
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
            }
            else
            {
                led.Write(PinValue.Low);
            }
        }
    }
}
