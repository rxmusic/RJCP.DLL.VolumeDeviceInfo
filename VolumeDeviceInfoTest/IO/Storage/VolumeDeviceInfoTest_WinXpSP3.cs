﻿namespace RJCP.IO.Storage
{
    using System.IO;
    using NUnit.Framework;

    [TestFixture]
    public class VolumeDeviceInfoTest_WinXpSP3
    {
        private static readonly string WinXPSim = Path.Combine(TestContext.CurrentContext.TestDirectory,
            "Test", "Win32", "VolumeInfoTest.WinXPSP3.xml");

        // The Physical Drive containing C:
        private const string Phys0 = @"\\.\PhysicalDrive0";
        private const string Phys0S = @"\\.\PhysicalDrive0\";

        // Floppy disk 3.5", disk present
        public const string A = @"A:";
        public const string AS = @"A:\";
        public const string AD = @"\Device\Floppy0";
        public const string AV = @"\\?\Volume{5619d5dc-ffd3-11ea-8e38-000c29553b8c}";
        public const string AVS = @"\\?\Volume{5619d5dc-ffd3-11ea-8e38-000c29553b8c}\";

        // Floppy disk 3.5", disk NOT present
        public const string B = @"B:";
        public const string BS = @"B:\";
        public const string BD = @"\Device\Floppy1";
        public const string BV = @"\\?\Volume{1833df12-ffe0-11ea-8e39-000c29553b8c}";
        public const string BVS = @"\\?\Volume{1833df12-ffe0-11ea-8e39-000c29553b8c}\";

        public const string C = @"C:";
        public const string CS = @"C:\";
        public const string CD = @"\Device\HarddiskVolume1";
        public const string CV = @"\\?\Volume{77f8a1bc-e9e9-11ea-95c7-806d6172696f}";
        public const string CVS = @"\\?\Volume{77f8a1bc-e9e9-11ea-95c7-806d6172696f}\";

        public const string D = @"D:";
        public const string DS = @"D:\";
        public const string DD = @"\Device\CdRom0";
        public const string DV = @"\\?\Volume{77f8a1ba-e9e9-11ea-95c7-806d6172696f}";
        public const string DVS = @"\\?\Volume{77f8a1ba-e9e9-11ea-95c7-806d6172696f}\";

        public const string M = @"M:";
        public const string MS = @"M:\";
        public const string MD = @"\Device\LanmanRedirector\;M:000000000000dc0d\devtest\share";

        public const string N = @"N:";
        public const string NS = @"N:\";
        public const string ND = @"\??\C:";
        public const string NV = CV;
        public const string NVS = CVS;

        public const string O = @"O:";
        public const string OS = @"O:\";
        public const string OD = @"\??\D:\I386";
        public const string OV = CV;
        public const string OVS = CVS;

        public const string Z = @"Z:";
        public const string ZS = @"Z:\";

        [Test]
        public void DriveA()
        {
            VolumeDeviceInfo vinfo = new Win32.VolumeDeviceInfoWin32Sim(WinXPSim, @"A:");
            Assert.That(vinfo.Path, Is.EqualTo(A));
            Assert.That(vinfo.Volume.Path, Is.EqualTo(AS));
            Assert.That(vinfo.Volume.DevicePath, Is.EqualTo(AV));
            Assert.That(vinfo.Volume.DosDevicePath, Is.EqualTo(AD));
            Assert.That(vinfo.Volume.DriveLetter, Is.EqualTo(A));
            IsDriveFloppyA(vinfo);
        }

        [Test]
        public void DriveAS()
        {
            VolumeDeviceInfo vinfo = new Win32.VolumeDeviceInfoWin32Sim(WinXPSim, @"A:\");
            Assert.That(vinfo.Path, Is.EqualTo(AS));
            Assert.That(vinfo.Volume.Path, Is.EqualTo(AS));
            Assert.That(vinfo.Volume.DevicePath, Is.EqualTo(AV));
            Assert.That(vinfo.Volume.DosDevicePath, Is.EqualTo(AD));
            Assert.That(vinfo.Volume.DriveLetter, Is.EqualTo(A));
            IsDriveFloppyA(vinfo);
        }

        [Test]
        public void DriveAV()
        {
            VolumeDeviceInfo vinfo = new Win32.VolumeDeviceInfoWin32Sim(WinXPSim, @"\\?\Volume{5619d5dc-ffd3-11ea-8e38-000c29553b8c}");
            Assert.That(vinfo.Path, Is.EqualTo(AV));
            Assert.That(vinfo.Volume.Path, Is.EqualTo(AVS));
            Assert.That(vinfo.Volume.DevicePath, Is.EqualTo(AV));
            Assert.That(vinfo.Volume.DosDevicePath, Is.EqualTo(AD));
            Assert.That(vinfo.Volume.DriveLetter, Is.EqualTo(A));
            IsDriveFloppyA(vinfo);
        }

        [Test]
        public void DriveAVS()
        {
            VolumeDeviceInfo vinfo = new Win32.VolumeDeviceInfoWin32Sim(WinXPSim, @"\\?\Volume{5619d5dc-ffd3-11ea-8e38-000c29553b8c}\");
            Assert.That(vinfo.Path, Is.EqualTo(AVS));
            Assert.That(vinfo.Volume.Path, Is.EqualTo(AVS));
            Assert.That(vinfo.Volume.DevicePath, Is.EqualTo(AV));
            Assert.That(vinfo.Volume.DosDevicePath, Is.EqualTo(AD));
            Assert.That(vinfo.Volume.DriveLetter, Is.EqualTo(A));
            IsDriveFloppyA(vinfo);
        }

        private static void IsDriveFloppyA(VolumeDeviceInfo vinfo)
        {
            Assert.That(vinfo.Disk.Extents, Is.Null);
            Assert.That(vinfo.DriveType, Is.EqualTo(DriveType.Floppy));
            Assert.That(vinfo.Disk.Device, Is.Null);
            Assert.That(vinfo.Disk.IsRemovableMedia, Is.True);
            Assert.That(vinfo.Disk.IsMediaPresent, Is.True);
            Assert.That(vinfo.Disk.IsReadOnly, Is.False);
            Assert.That(vinfo.Disk.Geometry.Cylinders, Is.EqualTo(80));
            Assert.That(vinfo.Disk.Geometry.TracksPerCylinder, Is.EqualTo(2));
            Assert.That(vinfo.Disk.Geometry.SectorsPerTrack, Is.EqualTo(18));
            Assert.That(vinfo.Disk.Geometry.BytesPerSector, Is.EqualTo(512));
            Assert.That(vinfo.Disk.Geometry.BytesPerPhysicalSector, Is.EqualTo(vinfo.Disk.Geometry.BytesPerSector));
            Assert.That(vinfo.Disk.HasSeekPenalty, Is.EqualTo(BoolUnknown.Unknown));
            Assert.That(vinfo.Partition, Is.Null);
            Assert.That(vinfo.FileSystem.Label, Is.EqualTo("FLOPPY"));
            Assert.That(vinfo.FileSystem.Serial, Is.EqualTo("6C26-C2C2"));
            Assert.That(vinfo.FileSystem.Name, Is.EqualTo("FAT"));
            Assert.That((int)vinfo.FileSystem.Flags, Is.EqualTo(0x06));
        }

        [Test]
        public void DriveB()
        {
            VolumeDeviceInfo vinfo = new Win32.VolumeDeviceInfoWin32Sim(WinXPSim, @"B:");
            Assert.That(vinfo.Path, Is.EqualTo(B));
            Assert.That(vinfo.Volume.Path, Is.EqualTo(BS));
            Assert.That(vinfo.Volume.DevicePath, Is.EqualTo(BV));
            Assert.That(vinfo.Volume.DosDevicePath, Is.EqualTo(BD));
            Assert.That(vinfo.Volume.DriveLetter, Is.EqualTo(B));
            IsDriveFloppyB(vinfo);
        }

        [Test]
        public void DriveBS()
        {
            VolumeDeviceInfo vinfo = new Win32.VolumeDeviceInfoWin32Sim(WinXPSim, @"B:\");
            Assert.That(vinfo.Path, Is.EqualTo(BS));
            Assert.That(vinfo.Volume.Path, Is.EqualTo(BS));
            Assert.That(vinfo.Volume.DevicePath, Is.EqualTo(BV));
            Assert.That(vinfo.Volume.DosDevicePath, Is.EqualTo(BD));
            Assert.That(vinfo.Volume.DriveLetter, Is.EqualTo(B));
            IsDriveFloppyB(vinfo);
        }

        [Test]
        public void DriveBV()
        {
            VolumeDeviceInfo vinfo = new Win32.VolumeDeviceInfoWin32Sim(WinXPSim, @"\\?\Volume{1833df12-ffe0-11ea-8e39-000c29553b8c}");
            Assert.That(vinfo.Path, Is.EqualTo(BV));
            Assert.That(vinfo.Volume.Path, Is.EqualTo(BVS));
            Assert.That(vinfo.Volume.DevicePath, Is.EqualTo(BV));
            Assert.That(vinfo.Volume.DosDevicePath, Is.EqualTo(BD));
            Assert.That(vinfo.Volume.DriveLetter, Is.EqualTo(B));
            IsDriveFloppyB(vinfo);
        }

        [Test]
        public void DriveBVS()
        {
            VolumeDeviceInfo vinfo = new Win32.VolumeDeviceInfoWin32Sim(WinXPSim, @"\\?\Volume{1833df12-ffe0-11ea-8e39-000c29553b8c}\");
            Assert.That(vinfo.Path, Is.EqualTo(BVS));
            Assert.That(vinfo.Volume.Path, Is.EqualTo(BVS));
            Assert.That(vinfo.Volume.DevicePath, Is.EqualTo(BV));
            Assert.That(vinfo.Volume.DosDevicePath, Is.EqualTo(BD));
            Assert.That(vinfo.Volume.DriveLetter, Is.EqualTo(B));
            IsDriveFloppyB(vinfo);
        }

        private static void IsDriveFloppyB(VolumeDeviceInfo vinfo)
        {
            Assert.That(vinfo.Disk.Extents, Is.Null);
            Assert.That(vinfo.DriveType, Is.EqualTo(DriveType.Floppy));
            Assert.That(vinfo.Disk.Device, Is.Null);
            Assert.That(vinfo.Disk.IsRemovableMedia, Is.True);
            Assert.That(vinfo.Disk.IsMediaPresent, Is.False);
            Assert.That(vinfo.Disk.IsReadOnly, Is.True);
            Assert.That(vinfo.Disk.Geometry, Is.Null);
            Assert.That(vinfo.Disk.HasSeekPenalty, Is.EqualTo(BoolUnknown.Unknown));
            Assert.That(vinfo.Partition, Is.Null);
            Assert.That(vinfo.FileSystem, Is.Null);
        }

        [Test]
        public void PhysicalDrive0()
        {
            VolumeDeviceInfo vinfo = new Win32.VolumeDeviceInfoWin32Sim(WinXPSim, @"\\.\PhysicalDrive0");
            Assert.That(vinfo.Path, Is.EqualTo(Phys0));
            Assert.That(vinfo.Volume.Path, Is.EqualTo(Phys0S));
            Assert.That(vinfo.Volume.DevicePath, Is.EqualTo(Phys0));
            Assert.That(vinfo.Volume.DosDevicePath, Is.Empty);     // Not a volume
            Assert.That(vinfo.Volume.DriveLetter, Is.Empty);       // Not a volume
            IsDrivePhys0(vinfo);
        }

        [Test]
        public void PhysicalDrive0S()
        {
            VolumeDeviceInfo vinfo = new Win32.VolumeDeviceInfoWin32Sim(WinXPSim, @"\\.\PhysicalDrive0\");
            Assert.That(vinfo.Path, Is.EqualTo(Phys0S));
            Assert.That(vinfo.Volume.Path, Is.EqualTo(Phys0S));
            Assert.That(vinfo.Volume.DevicePath, Is.EqualTo(Phys0));
            Assert.That(vinfo.Volume.DosDevicePath, Is.Empty);     // Not a volume
            Assert.That(vinfo.Volume.DriveLetter, Is.Empty);       // Not a volume
            IsDrivePhys0(vinfo);
        }

        private static void IsDrivePhys0(VolumeDeviceInfo vinfo)
        {
            IsPhysicalDrive0(vinfo);
            Assert.That(vinfo.Disk.Extents, Is.Null);
            Assert.That(vinfo.Partition.Style, Is.EqualTo(PartitionStyle.MasterBootRecord));
            Assert.That(vinfo.Partition.Number, Is.EqualTo(0));
            Assert.That(vinfo.Partition.Offset, Is.EqualTo(0));
            Assert.That(vinfo.Partition.Length, Is.EqualTo(42949672960));
            Assert.That(((VolumeDeviceInfo.IMbrPartition)vinfo.Partition).Type, Is.EqualTo(0));
            Assert.That(((VolumeDeviceInfo.IMbrPartition)vinfo.Partition).Bootable, Is.False);
            Assert.That(((VolumeDeviceInfo.IMbrPartition)vinfo.Partition).MbrSectorsOffset, Is.EqualTo(0));
            Assert.That(vinfo.FileSystem, Is.Null);
        }

        private static void IsPhysicalDrive0(VolumeDeviceInfo vinfo)
        {
            Assert.That(vinfo.DriveType, Is.EqualTo(DriveType.Fixed));
            Assert.That(vinfo.Disk.Device.VendorId, Is.Empty);
            Assert.That(vinfo.Disk.Device.ProductId, Is.EqualTo("VMware Virtual IDE Hard Drive"));
            Assert.That(vinfo.Disk.Device.ProductRevision, Is.EqualTo("00000001"));
            Assert.That(vinfo.Disk.Device.SerialNumber, Is.EqualTo("3030303030303030303"));
            Assert.That(vinfo.Disk.Device.BusType, Is.EqualTo(BusType.Ata));
            Assert.That(vinfo.Disk.Device.HasCommandQueueing, Is.False);
            Assert.That(vinfo.Disk.Device.ScsiDeviceType, Is.EqualTo(ScsiDeviceType.DirectAccessDevice));
            Assert.That(vinfo.Disk.Device.ScsiDeviceModifier, Is.EqualTo(0));
            Assert.That(vinfo.Disk.Device.GuidFlags, Is.EqualTo(DeviceGuidFlags.None));  // Windows XP doesn't support GUIDs
            Assert.That(vinfo.Disk.Device.Guid.ToString(), Is.EqualTo("00000000-0000-0000-0000-000000000000"));
            Assert.That(vinfo.Disk.IsRemovableMedia, Is.False);
            Assert.That(vinfo.Disk.IsMediaPresent, Is.True);
            Assert.That(vinfo.Disk.IsReadOnly, Is.False);
            Assert.That(vinfo.Disk.Geometry.Cylinders, Is.EqualTo(5221));
            Assert.That(vinfo.Disk.Geometry.TracksPerCylinder, Is.EqualTo(255));
            Assert.That(vinfo.Disk.Geometry.SectorsPerTrack, Is.EqualTo(63));
            Assert.That(vinfo.Disk.Geometry.BytesPerSector, Is.EqualTo(512));
            Assert.That(vinfo.Disk.Geometry.BytesPerPhysicalSector, Is.EqualTo(vinfo.Disk.Geometry.BytesPerSector)); // Not supported on WinXP
            Assert.That(vinfo.Disk.HasSeekPenalty, Is.EqualTo(BoolUnknown.Unknown));
        }

        [Test]
        public void DriveC()
        {
            VolumeDeviceInfo vinfo = new Win32.VolumeDeviceInfoWin32Sim(WinXPSim, @"C:");
            Assert.That(vinfo.Path, Is.EqualTo(C));
            Assert.That(vinfo.Volume.Path, Is.EqualTo(CS));
            Assert.That(vinfo.Volume.DevicePath, Is.EqualTo(CV));
            Assert.That(vinfo.Volume.DosDevicePath, Is.EqualTo(CD));
            Assert.That(vinfo.Volume.DriveLetter, Is.EqualTo(C));
            IsDriveBoot(vinfo);
        }

        [Test]
        public void DriveCS()
        {
            VolumeDeviceInfo vinfo = new Win32.VolumeDeviceInfoWin32Sim(WinXPSim, @"C:\");
            Assert.That(vinfo.Path, Is.EqualTo(CS));
            Assert.That(vinfo.Volume.Path, Is.EqualTo(CS));
            Assert.That(vinfo.Volume.DevicePath, Is.EqualTo(CV));
            Assert.That(vinfo.Volume.DosDevicePath, Is.EqualTo(CD));
            Assert.That(vinfo.Volume.DriveLetter, Is.EqualTo(C));
            IsDriveBoot(vinfo);
        }

        [Test]
        public void DriveCV()
        {
            VolumeDeviceInfo vinfo = new Win32.VolumeDeviceInfoWin32Sim(WinXPSim, @"\\?\Volume{77f8a1bc-e9e9-11ea-95c7-806d6172696f}");
            Assert.That(vinfo.Path, Is.EqualTo(CV));
            Assert.That(vinfo.Volume.Path, Is.EqualTo(CVS));
            Assert.That(vinfo.Volume.DevicePath, Is.EqualTo(CV));
            Assert.That(vinfo.Volume.DosDevicePath, Is.EqualTo(CD));
            Assert.That(vinfo.Volume.DriveLetter, Is.EqualTo(C));
            IsDriveBoot(vinfo);
        }

        [Test]
        public void DriveCVS()
        {
            VolumeDeviceInfo vinfo = new Win32.VolumeDeviceInfoWin32Sim(WinXPSim, @"\\?\Volume{77f8a1bc-e9e9-11ea-95c7-806d6172696f}\");
            Assert.That(vinfo.Path, Is.EqualTo(CVS));
            Assert.That(vinfo.Volume.Path, Is.EqualTo(CVS));
            Assert.That(vinfo.Volume.DevicePath, Is.EqualTo(CV));
            Assert.That(vinfo.Volume.DosDevicePath, Is.EqualTo(CD));
            Assert.That(vinfo.Volume.DriveLetter, Is.EqualTo(C));
            IsDriveBoot(vinfo);
        }

        [Test]
        public void DriveCFolder()
        {
            VolumeDeviceInfo vinfo = new Win32.VolumeDeviceInfoWin32Sim(WinXPSim, @"C:\Documents and Settings\User\My Documents");
            Assert.That(vinfo.Path, Is.EqualTo(@"C:\Documents and Settings\User\My Documents"));
            Assert.That(vinfo.Volume.Path, Is.EqualTo(CS));
            Assert.That(vinfo.Volume.DevicePath, Is.EqualTo(CV));
            Assert.That(vinfo.Volume.DosDevicePath, Is.EqualTo(CD));
            Assert.That(vinfo.Volume.DriveLetter, Is.EqualTo(C));
            IsDriveBoot(vinfo);
        }

        private static void IsDriveBoot(VolumeDeviceInfo vinfo)
        {
            IsPhysicalDrive0(vinfo);
            Assert.That(vinfo.Disk.Extents.Length, Is.EqualTo(1));
            Assert.That(vinfo.Disk.Extents[0].Device, Is.EqualTo(@"\\.\PhysicalDrive0"));
            Assert.That(vinfo.Disk.Extents[0].StartingOffset, Is.EqualTo(vinfo.Partition.Offset));
            Assert.That(vinfo.Disk.Extents[0].ExtentLength, Is.EqualTo(vinfo.Partition.Length));
            Assert.That(vinfo.Partition.Style, Is.EqualTo(PartitionStyle.MasterBootRecord));
            Assert.That(vinfo.Partition.Number, Is.EqualTo(1));
            Assert.That(vinfo.Partition.Offset, Is.EqualTo(32256));
            Assert.That(vinfo.Partition.Length, Is.EqualTo(42935929344));
            Assert.That(((VolumeDeviceInfo.IMbrPartition)vinfo.Partition).Type, Is.EqualTo(7));
            Assert.That(((VolumeDeviceInfo.IMbrPartition)vinfo.Partition).Bootable, Is.True);
            Assert.That(((VolumeDeviceInfo.IMbrPartition)vinfo.Partition).MbrSectorsOffset, Is.EqualTo(63));
            Assert.That(vinfo.FileSystem.Label, Is.Empty);
            Assert.That(vinfo.FileSystem.Serial, Is.EqualTo("544D-DD66"));
            Assert.That(vinfo.FileSystem.Name, Is.EqualTo("NTFS"));
            Assert.That((int)vinfo.FileSystem.Flags, Is.EqualTo(0x000700FF));
        }

        [Test]
        public void DriveD()
        {
            VolumeDeviceInfo vinfo = new Win32.VolumeDeviceInfoWin32Sim(WinXPSim, @"D:");
            Assert.That(vinfo.Path, Is.EqualTo(D));
            Assert.That(vinfo.Volume.Path, Is.EqualTo(DS));
            Assert.That(vinfo.Volume.DevicePath, Is.EqualTo(DV));
            Assert.That(vinfo.Volume.DosDevicePath, Is.EqualTo(DD));
            Assert.That(vinfo.Volume.DriveLetter, Is.EqualTo(D));
            IsDriveCdRom(vinfo);
        }

        [Test]
        public void DriveDS()
        {
            VolumeDeviceInfo vinfo = new Win32.VolumeDeviceInfoWin32Sim(WinXPSim, @"D:\");
            Assert.That(vinfo.Path, Is.EqualTo(DS));
            Assert.That(vinfo.Volume.Path, Is.EqualTo(DS));
            Assert.That(vinfo.Volume.DevicePath, Is.EqualTo(DV));
            Assert.That(vinfo.Volume.DosDevicePath, Is.EqualTo(DD));
            Assert.That(vinfo.Volume.DriveLetter, Is.EqualTo(D));
            IsDriveCdRom(vinfo);
        }

        [Test]
        public void DriveDV()
        {
            VolumeDeviceInfo vinfo = new Win32.VolumeDeviceInfoWin32Sim(WinXPSim, @"\\?\Volume{77f8a1ba-e9e9-11ea-95c7-806d6172696f}");
            Assert.That(vinfo.Path, Is.EqualTo(DV));
            Assert.That(vinfo.Volume.Path, Is.EqualTo(DVS));
            Assert.That(vinfo.Volume.DevicePath, Is.EqualTo(DV));
            Assert.That(vinfo.Volume.DosDevicePath, Is.EqualTo(DD));
            Assert.That(vinfo.Volume.DriveLetter, Is.EqualTo(D));
            IsDriveCdRom(vinfo);
        }

        [Test]
        public void DriveDVS()
        {
            VolumeDeviceInfo vinfo = new Win32.VolumeDeviceInfoWin32Sim(WinXPSim, @"\\?\Volume{77f8a1ba-e9e9-11ea-95c7-806d6172696f}\");
            Assert.That(vinfo.Path, Is.EqualTo(DVS));
            Assert.That(vinfo.Volume.Path, Is.EqualTo(DVS));
            Assert.That(vinfo.Volume.DevicePath, Is.EqualTo(DV));
            Assert.That(vinfo.Volume.DosDevicePath, Is.EqualTo(DD));
            Assert.That(vinfo.Volume.DriveLetter, Is.EqualTo(D));
            IsDriveCdRom(vinfo);
        }

        private static void IsDriveCdRom(VolumeDeviceInfo vinfo)
        {
            Assert.That(vinfo.DriveType, Is.EqualTo(DriveType.CdRom));
            Assert.That(vinfo.Disk.Extents, Is.Null);
            Assert.That(vinfo.Disk.Device.VendorId, Is.Empty);
            Assert.That(vinfo.Disk.Device.ProductId, Is.EqualTo("NECVMWar VMware IDE CDR10"));
            Assert.That(vinfo.Disk.Device.ProductRevision, Is.EqualTo("1.00"));
            Assert.That(vinfo.Disk.Device.SerialNumber, Is.EqualTo("3031303030303030303"));
            Assert.That(vinfo.Disk.Device.BusType, Is.EqualTo(BusType.Atapi));
            Assert.That(vinfo.Disk.Device.HasCommandQueueing, Is.False);
            Assert.That(vinfo.Disk.Device.ScsiDeviceType, Is.EqualTo(ScsiDeviceType.CdRomDevice));
            Assert.That(vinfo.Disk.Device.ScsiDeviceModifier, Is.EqualTo(0));
            Assert.That(vinfo.Disk.Device.GuidFlags, Is.EqualTo(DeviceGuidFlags.None));  // Windows XP doesn't support GUIDs
            Assert.That(vinfo.Disk.Device.Guid.ToString(), Is.EqualTo("00000000-0000-0000-0000-000000000000"));
            Assert.That(vinfo.Disk.IsRemovableMedia, Is.True);
            Assert.That(vinfo.Disk.IsMediaPresent, Is.True);
            Assert.That(vinfo.Disk.IsReadOnly, Is.True);
            Assert.That(vinfo.Disk.Geometry.Cylinders, Is.EqualTo(904));
            Assert.That(vinfo.Disk.Geometry.TracksPerCylinder, Is.EqualTo(64));
            Assert.That(vinfo.Disk.Geometry.SectorsPerTrack, Is.EqualTo(32));
            Assert.That(vinfo.Disk.Geometry.BytesPerSector, Is.EqualTo(2048));
            Assert.That(vinfo.Disk.Geometry.BytesPerPhysicalSector, Is.EqualTo(vinfo.Disk.Geometry.BytesPerSector)); // Not supported on WinXP
            Assert.That(vinfo.Disk.HasSeekPenalty, Is.EqualTo(BoolUnknown.Unknown));
            Assert.That(vinfo.Partition.Style, Is.EqualTo(PartitionStyle.MasterBootRecord));
            Assert.That(vinfo.Partition.Number, Is.EqualTo(0));
            Assert.That(vinfo.Partition.Offset, Is.EqualTo(0));
            Assert.That(vinfo.Partition.Length, Is.EqualTo(3794823168));
            Assert.That(((VolumeDeviceInfo.IMbrPartition)vinfo.Partition).Type, Is.EqualTo(11));
            Assert.That(((VolumeDeviceInfo.IMbrPartition)vinfo.Partition).Bootable, Is.False);
            Assert.That(((VolumeDeviceInfo.IMbrPartition)vinfo.Partition).MbrSectorsOffset, Is.EqualTo(0));
            Assert.That(vinfo.FileSystem.Label, Is.EqualTo("winxpsp3_090429"));
            Assert.That(vinfo.FileSystem.Serial, Is.EqualTo("1686-338B"));
            Assert.That(vinfo.FileSystem.Name, Is.EqualTo("CDFS"));
            Assert.That((int)vinfo.FileSystem.Flags, Is.EqualTo(0x00080005));
        }

        [Test]
        public void NetM()
        {
            VolumeDeviceInfo vinfo = new Win32.VolumeDeviceInfoWin32Sim(WinXPSim, @"M:");
            Assert.That(vinfo.Path, Is.EqualTo(M));
            Assert.That(vinfo.DriveType, Is.EqualTo(DriveType.Remote));
            Assert.That(vinfo.Volume.Path, Is.Empty);        // Not a local mount
            Assert.That(vinfo.Volume.DevicePath, Is.Empty);  // Not a local mount
            Assert.That(vinfo.Volume.DosDevicePath, Is.EqualTo(MD));
            Assert.That(vinfo.Volume.DriveLetter, Is.EqualTo(M));
            Assert.That(vinfo.Disk, Is.Null);
            Assert.That(vinfo.Partition, Is.Null);
            Assert.That(vinfo.FileSystem, Is.Null);
        }

        [Test]
        public void NetMS()
        {
            VolumeDeviceInfo vinfo = new Win32.VolumeDeviceInfoWin32Sim(WinXPSim, @"M:\");
            Assert.That(vinfo.Path, Is.EqualTo(MS));
            Assert.That(vinfo.DriveType, Is.EqualTo(DriveType.Remote));
            Assert.That(vinfo.Volume.Path, Is.Empty);        // Not a local mount
            Assert.That(vinfo.Volume.DevicePath, Is.Empty);  // Not a local mount
            Assert.That(vinfo.Volume.DosDevicePath, Is.EqualTo(MD));
            Assert.That(vinfo.Volume.DriveLetter, Is.EqualTo(M));
            Assert.That(vinfo.Disk, Is.Null);
            Assert.That(vinfo.Partition, Is.Null);
            Assert.That(vinfo.FileSystem, Is.Null);
        }

        [Test]
        public void SubstN()
        {
            VolumeDeviceInfo vinfo = new Win32.VolumeDeviceInfoWin32Sim(WinXPSim, @"N:");
            Assert.That(vinfo.Path, Is.EqualTo(N));
            Assert.That(vinfo.Volume.Path, Is.EqualTo(CS));  // Drive N: is mapped to C:
            Assert.That(vinfo.Volume.DevicePath, Is.EqualTo(CV));
            Assert.That(vinfo.Volume.DosDevicePath, Is.EqualTo(ND));
            Assert.That(vinfo.Volume.DriveLetter, Is.EqualTo(N));
            IsDriveBoot(vinfo);
        }

        [Test]
        public void SubstNS()
        {
            VolumeDeviceInfo vinfo = new Win32.VolumeDeviceInfoWin32Sim(WinXPSim, @"N:\");
            Assert.That(vinfo.Path, Is.EqualTo(NS));
            Assert.That(vinfo.Volume.Path, Is.EqualTo(CS));  // Drive N: is mapped to C:
            Assert.That(vinfo.Volume.DevicePath, Is.EqualTo(CV));
            Assert.That(vinfo.Volume.DosDevicePath, Is.EqualTo(ND));
            Assert.That(vinfo.Volume.DriveLetter, Is.EqualTo(N));
            IsDriveBoot(vinfo);
        }

        [Test]
        public void SubstO()
        {
            VolumeDeviceInfo vinfo = new Win32.VolumeDeviceInfoWin32Sim(WinXPSim, @"O:");
            Assert.That(vinfo.Path, Is.EqualTo(O));
            Assert.That(vinfo.Volume.Path, Is.EqualTo(DS));  // Drive O: is mapped to D:\I386
            Assert.That(vinfo.Volume.DevicePath, Is.EqualTo(DV));
            Assert.That(vinfo.Volume.DosDevicePath, Is.EqualTo(OD));
            Assert.That(vinfo.Volume.DriveLetter, Is.EqualTo(O));
            IsDriveCdRom(vinfo);
        }

        [Test]
        public void SubstOS()
        {
            VolumeDeviceInfo vinfo = new Win32.VolumeDeviceInfoWin32Sim(WinXPSim, @"O:\");
            Assert.That(vinfo.Path, Is.EqualTo(OS));
            Assert.That(vinfo.Volume.Path, Is.EqualTo(DS));  // Drive O: is mapped to D:\I386
            Assert.That(vinfo.Volume.DevicePath, Is.EqualTo(DV));
            Assert.That(vinfo.Volume.DosDevicePath, Is.EqualTo(OD));
            Assert.That(vinfo.Volume.DriveLetter, Is.EqualTo(O));
            IsDriveCdRom(vinfo);
        }

        [Test]
        public void UnmappedDriveZ()
        {
            Assert.That(() => {
                _ = new Win32.VolumeDeviceInfoWin32Sim(WinXPSim, @"Z:");
            }, Throws.TypeOf<FileNotFoundException>());
        }
    }
}
