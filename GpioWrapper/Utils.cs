using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Gpio;

namespace GpioWrapper
{
    public enum PinValue { Low, High };

    public sealed class PinValueChangedEventArgs
    {
        //
        // Summary:
        //     Gets the type of change that occurred to the value of the general-purpose I/O
        //     (GPIO) pin for the GpioPin.ValueChanged event.
        //
        // Returns:
        //     An enumeration value that indicates the type of change that occurred to the value
        //     of the GPIO pin for the GpioPin.ValueChanged event.
        public PinEdge Edge { get; }
    }

    public enum PinEdge
    {
        //
        // Summary:
        //     The value of the GPIO pin changed from high to low.
        FallingEdge = 0,
        //
        // Summary:
        //     The value of the GPIO pin changed from low to high.
        RisingEdge = 1
    }
}
