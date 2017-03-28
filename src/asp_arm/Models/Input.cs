﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GpioWrapper;

namespace asp_arm.Models
{
    public class InputModel
    {
        static InputModel inputModel = null;
        Input input;

        private bool state = false;
        private InputModel()
        {
            InitGPIO();
        }
        public static InputModel getInput()
        {
            if (inputModel == null)
            {
                inputModel = new InputModel();
            }
            return inputModel;
        }
        public string GetState()
        {
            return input.GetValue() == PinValue.High ? "on" : "off";
        }

        private void InitGPIO()
        {
            Console.WriteLine("input init start");
            try
            {
                input = new Input(6);
                input.InitGPIO();
            }
            catch (Exception e)
            {
                Console.WriteLine("input init failed: " + e);
            }
        }
    }
}
