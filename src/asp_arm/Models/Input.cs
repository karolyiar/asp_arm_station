using System;
using GpioWrapper;

namespace asp_arm.Models
{
    public class InputModel
    {
        private static volatile InputModel inputModel = null;
        private Input input;
        
        private InputModel()
        {
            InitGPIO();
        }
        public static InputModel Instance
        {
            get
            {
                if (inputModel == null)
                {
                    inputModel = new InputModel();
                }
                return inputModel;
            }
        }
        public string State
        {
            get
            {
                return input.GetValue() == PinValue.High ? "on" : "off";
            }
        }

        private void InitGPIO()
        {
            try
            {
                input = new Input(6);
                input.InitGPIO();
                input.Changed += ChangedEventHandler;
            }
            catch (Exception e)
            {
                Console.WriteLine("input init failed: " + e);
            }
        }

        private void ChangedEventHandler(Input sender, PinEdge e)
        {
            LedModel led = LedModel.Instance;
            if (e == PinEdge.RisingEdge)
            {
                led.State = "off";
            }
            else
            {
                led.State = "on";
            }
        }
    }
}
