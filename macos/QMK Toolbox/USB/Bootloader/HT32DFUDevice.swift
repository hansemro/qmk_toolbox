import Foundation

class HT32DFUDevice: BootloaderDevice {
    override init(usbDevice: USBDevice) {
        super.init(usbDevice: usbDevice)
        name = "HT32 DFU"
        type = .ht32Dfu
        resettable = true
    }

    override func flash(_ mcu: String, file: String) {
        guard file.lowercased().hasSuffix(".bin") else {
            print(message: "Only firmware files in .bin format can be flashed with ht32-dfu-tool!", type: .error)
            return
        }

        runProcess("ht32-dfu-tool", args: ["--reset", "write", "0", file])
    }

    override func reset(_ mcu: String) {
        runProcess("ht32-dfu-tool", args: ["reset"])
    }
}
