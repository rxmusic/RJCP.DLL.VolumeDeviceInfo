﻿using System;

namespace RJCP.IO.Storage
{
    using NUnit.Framework;

    [TestFixture]
    public class VolumeDeviceInfoTest
    {
        private bool isJenkins()
        {
            var jk = Environment.GetEnvironmentVariable("JENKINS_HOME");
            return !string.IsNullOrEmpty(jk);
        }

        [Test]
        [Platform("Win")]
        public void BootPartition()
        {
            if (isJenkins())
            {
                Console.WriteLine("Running in Jenkins. Skipping test.");
                return;
            }

            // Checks that in general we can query the volume information.
            VolumeDeviceInfo volumeInfo = VolumeDeviceInfo.Create(@"\\.\BootPartition");
            Assert.That(volumeInfo.Path, Is.EqualTo(@"\\.\BootPartition"));
            Assert.That(volumeInfo.DriveType, Is.EqualTo(DriveType.Fixed));
            Assert.That(volumeInfo.Volume.DriveLetter, Is.Not.Null.Or.Empty);
            Assert.That(volumeInfo.Volume.DevicePath, Is.Not.Null.Or.Empty);
            Assert.That(volumeInfo.Disk.IsMediaPresent, Is.True);
            Assert.That(volumeInfo.Disk.IsReadOnly, Is.False);
            Assert.That(volumeInfo.FileSystem.Label, Is.Not.Null);
            Assert.That(volumeInfo.FileSystem.Serial, Is.Not.Null);
            Assert.That(volumeInfo.FileSystem.Name, Is.Not.Null);
        }

        [Test]
        [Platform("Win")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Assertion", "NUnit2010:Use EqualConstraint for better assertion messages in case of failure.", Justification = "Specific Test")]
        public void ObjectEquality()
        {
            if (isJenkins())
            {
                Console.WriteLine("Running in Jenkins. Skipping test.");
                return;
            }

            VolumeDeviceInfo bootPart = VolumeDeviceInfo.Create(@"\\.\BootPartition");
            VolumeDeviceInfo bootDrive = VolumeDeviceInfo.Create(bootPart.Volume.DevicePath);

            // Object Equality
            Assert.That(bootPart, Is.EqualTo(bootDrive));
            Assert.That(bootDrive, Is.EqualTo(bootPart));
            Assert.That(bootPart.Equals(bootDrive));
            Assert.That(bootDrive.Equals(bootPart));
            Assert.That(bootPart.ToString(), Is.EqualTo(bootDrive.ToString()));
            Assert.That(bootPart.GetHashCode(), Is.EqualTo(bootDrive.GetHashCode()));

            // Reference Equality
            Assert.That(bootPart != bootDrive);
            Assert.That(bootDrive != bootPart);
            Assert.That(ReferenceEquals(bootPart, bootDrive), Is.False);
        }
    }
}