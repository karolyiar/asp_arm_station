using System;
using GpioWrapper;

namespace asp_arm.Models
{
    public class WaterLevel
    {
        public enum Levels
        {
            NONE = 0,
            LOW = 1,
            MID = 2,
            HIGH = 3
        };
        public delegate void WaterChangedEventHandler(Levels level);
        public event WaterChangedEventHandler Changed;

        private static volatile WaterLevel waterLevel = null;
        private Input[] inputs;
        private Levels currentLevel;

        private WaterLevel()
        {
            InitializeInputs();
        }
        public static WaterLevel Instance
        {
            get
            {
                if (waterLevel == null)
                {
                    waterLevel = new WaterLevel();
                }
                return waterLevel;
            }
        }
        private Levels ReadLevel
        {
            get
            {
                int currentLevel = 0;
                while (inputs[currentLevel].GetValue() == PinValue.High)
                    currentLevel++;
                return (Levels)(Object)currentLevel;
            }
        }

        public Levels Level
        {
            get
            {
                return currentLevel;
            }
        }

        private void InitializeInputs()
        {
            inputs = new[]
                {
                    new Input(1),
                    new Input(2),
                    new Input(3)
                };
            try
            {
                foreach (var input in inputs)
                {
                    input.InitGPIO();
                    input.Changed += InputChangedEventHandler;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("input init failed: " + e);
            }
        }

        private void InputChangedEventHandler(Input sender, PinEdge edge)
        {
            if (currentLevel != ReadLevel)
            {
                currentLevel = ReadLevel;
                Changed.Invoke(currentLevel);
            }
        }
    }
}
