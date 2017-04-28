using System;
using GpioWrapper;

namespace asp_arm.Models
{
    public class LedModel
    {
        private static volatile LedModel ledModel = null;
        private Led led;

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
                state = value == "on" ? true : false;
                try
                {  
                    if (state)
                        led.On();
                    else
                        led.Off();
                }
                catch (Exception e)
                {
                    Console.WriteLine("led set failed: " + e);
                }
            }
        }

        private void InitGPIO()
        {
            try
            {
                led = new Led();
                led.Init(5, PinValue.Low);
            }
            catch (Exception e)
            {
                Console.WriteLine("led init failed: " + e);
            }
        }

        public void Toggle()
        {
            state = !state;
            led.Write(state ? PinValue.High : PinValue.Low);
        }
    }
}
