using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GpioWrapper;

namespace asp_arm.Models
{
    public class InputModel
    {
        private static volatile InputModel inputModel = null;
        Input input;

        private bool state = false;
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
            Console.WriteLine("input init start");
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
            if (e == PinEdge.RisingEdge)
            {
                LedModel led = LedModel.Instance;
                led.Toggle();
            }
        }
    }
}
