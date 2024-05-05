using System.IO;
using System.Threading.Tasks;

namespace QMK_Toolbox.Usb.Bootloader
{
    class Ht32DfuDevice : BootloaderDevice
    {
        public Ht32DfuDevice(UsbDevice d) : base(d)
        {
            Type = BootloaderType.Ht32Dfu;
            Name = "HT32 DFU";
            PreferredDriver = "WinUSB";
            IsResettable = true;
        }

        public async override Task Flash(string mcu, string file)
        {
            if (Path.GetExtension(file)?.ToLower() == ".bin")
            {
                await RunProcessAsync("ht32-dfu-tool.exe", $"--reset write 0 \"{file}\"");
            }
            else
            {
                PrintMessage("Only firmware files in .bin format can be flashed with ht32-dfu-tool!", MessageType.Error);
            }
        }

        public async override Task Reset(string mcu) => await RunProcessAsync("ht32-dfu-tool.exe", "reset");
    }
}
