﻿namespace VolumeInfo
{
    using System;
    using IO.Storage;

    public static class Program
    {
        private class Options
        {
            public bool LogApi { get; set; }

            public bool ParseOption(string arg)
            {
                if (arg == null) throw new ArgumentNullException(nameof(arg));
                if (arg.Length < 2) return false;
                if (arg[0] != '-') return false;

                string option = arg.Substring(1);
                if (option.Equals("l", StringComparison.Ordinal) || option.Equals("log", StringComparison.OrdinalIgnoreCase)) {
                    LogApi = true;
                    return true;
                }

                return true;
            }
        }

        static int Main(string[] args)
        {
            Options options = new Options();

            foreach (string device in args) {
                if (options.ParseOption(device)) continue;

                Console.WriteLine("Device Path: {0}", device);

                if (options.LogApi) LogApi.LogDeviceData.Capture(device);

                VolumeDeviceInfo info;
                try {
                    info = new VolumeDeviceInfo(device);
                    Console.WriteLine("  Volume");
                    Console.WriteLine("    Volume Path     : {0}", info.Volume.Path);
                    Console.WriteLine("    Volume Device   : {0}", info.Volume.DevicePath);
                    Console.WriteLine("    Volume Drive    : {0}", info.Volume.DriveLetter);
                    Console.WriteLine("    NT DOS Device   : {0}", info.Volume.DosDevicePath);
                    Console.WriteLine("    Label           : {0}", info.FileSystem.Label);
                    Console.WriteLine("    Serial Number   : {0}", info.FileSystem.Serial);
                    Console.WriteLine("    Flags           : {0}", info.FileSystem.Flags);
                    Console.WriteLine("    File System     : {0}", info.FileSystem.Name);
                    if (info.Partition != null) {
                        Console.WriteLine("    Partition Style : {0}", info.Partition.Style);
                        Console.WriteLine("    Part Number     : {0}", info.Partition.Number);
                        Console.WriteLine("    Part Offset     : {0:X8}", info.Partition.Offset);
                        Console.WriteLine("    Part Length     : {0:F1} GB", info.Partition.Length / 1024.0 / 1024.0 / 1024.0);
                        switch (info.Partition.Style) {
                        case PartitionStyle.MasterBootRecord:
                            var mbrPart = (VolumeDeviceInfo.IMbrPartition)info.Partition;
                            Console.WriteLine("    MBR bootable    : {0}", mbrPart.Bootable);
                            Console.WriteLine("    MBR Type        : {0}", mbrPart.Type);
                            Console.WriteLine("    MBR Offset      : {0}", mbrPart.MbrSectorsOffset);
                            break;
                        case PartitionStyle.GuidPartitionTable:
                            var gptPart = (VolumeDeviceInfo.IGptPartition)info.Partition;
                            Console.WriteLine("    GPT Attributes  : {0}", gptPart.Attributes);
                            Console.WriteLine("    GPT Name        : {0}", gptPart.Name);
                            Console.WriteLine("    GPT Type        : {0}", gptPart.Type);
                            Console.WriteLine("    GPT Id          : {0}", gptPart.Id);
                            break;
                        }
                    }
                    Console.WriteLine("  Device");
                    Console.WriteLine("    Vendor          : {0}", info.Disk.VendorId);
                    Console.WriteLine("    Product         : {0}; Revision {1}", info.Disk.ProductId, info.Disk.ProductRevision);
                    Console.WriteLine("    SerialNumber    : {0}", info.Disk.SerialNumber);
                    Console.WriteLine("    Bus Type        : {0}", info.Disk.BusType.ToDescription(true));
                    Console.WriteLine("    SCSI Device Type: {0}; SCSI Modifier: {1}", info.Disk.ScsiDeviceType.ToDescription(), info.Disk.ScsiDeviceModifier);
                    Console.WriteLine("    Command Queueing: {0}", info.Disk.HasCommandQueueing);
                    Console.WriteLine("    Removable Media : {0}", info.Disk.IsRemovableMedia);
                    Console.WriteLine("    Media Present   : {0}", info.Disk.IsMediaPresent);
                    Console.WriteLine("    Disk Read Only  : {0}", info.Disk.IsReadOnly);
                    Console.WriteLine("    Device GUID:    : {0} ({1})", info.Disk.Guid, info.Disk.GuidFlags);
                    Console.WriteLine("    Device Number   : {0} #{1}", info.Disk.DeviceType, info.Disk.DeviceNumber);
                    Console.WriteLine("    Media Type      : {0}", info.Disk.MediaType);
                    Console.WriteLine("    Cyl/Trk/Sec/Byte: {0}/{1}/{2}/{3} ({4:F1} GB)",
                        info.Disk.Cylinders, info.Disk.TracksPerCylinder, info.Disk.SectorsPerTrack, info.Disk.BytesPerSector,
                        info.Disk.Cylinders * info.Disk.TracksPerCylinder * info.Disk.SectorsPerTrack * info.Disk.BytesPerSector / 1024.0 / 1024.0 / 1024.0);
                    Console.WriteLine("    Bytes/Sector    : Physical {0}; Logical {1}", info.Disk.BytesPerPhysicalSector, info.Disk.BytesPerSector);
                    Console.WriteLine("    Seek Penalty    : {0}", info.Disk.HasSeekPenalty);
                } catch (Exception ex) {
                    Console.WriteLine("  Error: {0}", ex.Message);
                }
                Console.WriteLine("");
            }

            return 0;
        }
    }
}
