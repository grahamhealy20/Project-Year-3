using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows;
using System.ComponentModel;
using System.Windows.Threading;

namespace ClientWPF.Model
{
    class Kinect
    {
        private int frameIterator = 0;
        private int noOfFrames = 100;
        private int frameCounter = 0;
        private bool alertWait = true;
        private int noOfMovedPixels = 0;
        private int noOfNotMovedPixels = 0;
        private double percentage = 0;
        private int iter = 0;


        public Kinect()
        {

        }

        public delegate void DetectionHandler(object myObject, EventArgs myArgs);
        public event DetectionHandler OnMotionDetected;

        public delegate void PerecentUpdater(object myObject, Model.EventArguments.PercentEventArgs myArgs);
        public event PerecentUpdater OnDepthFramePercent;

        public delegate void InformationHandler(object myObject, Model.EventArguments.InformationArgs myArgs);
        public event InformationHandler OnInformationEvent;

        /// Active Kinect sensor
        private KinectSensor sensor;

        /// Intermediate storage for the depth data received from the camera
        private DepthImagePixel[] depthPixelsComp = new DepthImagePixel[307200];
        private short[] depthPixelsRes = new short[307200];
        private DepthImagePixel[] depthPixels;
        private byte[] colorPixels;
        private WriteableBitmap colorBitmap;

        private void enumerate()
        {
            foreach (var potentialSensor in KinectSensor.KinectSensors)
            {
                if (potentialSensor.Status == KinectStatus.Connected)
                {
                    this.sensor = potentialSensor;
                    break;
                }
            }
        }

        public WriteableBitmap getImageSource()
        {
            return colorBitmap;
        }

        public bool StartKinect()
        {
            // load sensor
            this.enumerate();
            if (null != this.sensor)
            {
                // Turn on the depth stream to receive depth frames
                this.sensor.DepthStream.Enable(DepthImageFormat.Resolution640x480Fps30);
                this.depthPixels = new DepthImagePixel[this.sensor.DepthStream.FramePixelDataLength];
                this.colorPixels = new byte[this.sensor.DepthStream.FramePixelDataLength * sizeof(int)];

                // This is the bitmap we'll display on-screen
                this.colorBitmap = new WriteableBitmap(this.sensor.DepthStream.FrameWidth, this.sensor.DepthStream.FrameHeight, 96.0, 96.0, PixelFormats.Bgr32, null);
                this.sensor.DepthFrameReady += this.SensorDepthFrameReady;

                // Start the sensor!
                try
                {
                    this.sensor.Start();
                    EventArguments.InformationArgs args = new EventArguments.InformationArgs();
                    args.InfoMessage = "Kinect Started";
                    if (OnInformationEvent != null)
                        OnInformationEvent(this, args);
                    return true;
                }
                catch (IOException)
                {
                    this.sensor = null;
                    return false;
                }
            }
            if (null == this.sensor)
            {
                EventArguments.InformationArgs args = new EventArguments.InformationArgs();
                args.InfoMessage = Properties.Resources.NoKinectReady;
                if(OnInformationEvent != null)
                    OnInformationEvent(this, args);

                return false;
            }
            return false;
        }

        public bool Stop()
        {
            if (null != this.sensor)
            {
                this.sensor.Stop();
                return true;
            }
            return false;
        }

        private void SensorDepthFrameReady(object sender, DepthImageFrameReadyEventArgs e)
        {
            iter++;
            using (DepthImageFrame depthFrame = e.OpenDepthImageFrame())
            {
                if (depthFrame != null)
                {
                    frameCounter++;
                    if (frameCounter == 100)
                    {
                        frameCounter = 0;
                    }

                    frameIterator++;
                    // 30 fps so wait for a second after each detection
                    if (frameIterator == 100)
                    {
                        alertWait = false;
                        frameIterator = 0;
                    }
                    // Copy the pixel data from the image to a temporary array
                    depthFrame.CopyDepthImagePixelDataTo(this.depthPixels);

                    //If first frame, make ref frame.
                    if (frameCounter == 0 || frameCounter >= noOfFrames)
                    {
                        depthFrame.CopyDepthImagePixelDataTo(this.depthPixelsComp);
                        frameCounter = 0;
                    }

                    int minDepth = depthFrame.MinDepth;
                    int maxDepth = depthFrame.MaxDepth;

                    int colorPixelIndex = 0;
                    for (int i = 0; i < this.depthPixels.Length; ++i)
                    {
                        // Get the depth for this pixel
                        short depth = depthPixels[i].Depth;
                        depthPixelsRes[i] = depth;

                        byte intensity = (byte)(depth >= minDepth && depth <= maxDepth ? depth : 0);

                        // Write out blue byte
                        this.colorPixels[colorPixelIndex++] = intensity;

                        // Write out green byte
                        this.colorPixels[colorPixelIndex++] = intensity;

                        // Write out red byte                        
                        this.colorPixels[colorPixelIndex++] = intensity;

                        // We're outputting BGR, the last byte in the 32 bits is unused so skip it
                        // If we were outputting BGRA, we would write alpha here.
                        ++colorPixelIndex;
                    }

                    // Code to count number of displaced pixels compared to reference frame. It creates a mask which can then be compared as a % against ref frame.
                    noOfMovedPixels = 0;
                    noOfNotMovedPixels = 0;
                    for (int i = 0; i < depthPixels.Length; i++)
                    {
                        depthPixelsRes[i] = (short)(depthPixels[i].Depth - depthPixelsComp[i].Depth);
                        short diffNo = depthPixelsRes[i];
                        if (diffNo == 0)
                        {
                            noOfNotMovedPixels++;
                        }
                        else if (depthPixelsRes[i] > 0)
                        {
                            noOfMovedPixels++;
                        }
                    }

                    // Calculate percentage
                    percentage = ((double)noOfMovedPixels / (double)307200) * 100;
                    if (OnDepthFramePercent != null)
                    {
                        EventArguments.PercentEventArgs args = new EventArguments.PercentEventArgs();
                        args.Percentage = Math.Round(percentage, 2);
                        OnDepthFramePercent(this, args);
                    }

                    if (percentage > 40.00)
                    {
                        EventArgs eventargs = new EventArgs();
                        // Only fire if bool is false 
                        if (alertWait == false)
                        {
                            alertWait = true;
                            if (OnMotionDetected != null)
                                OnMotionDetected(this, eventargs);
                        }
                    }
                    this.colorBitmap.WritePixels(
                         new Int32Rect(0, 0, this.colorBitmap.PixelWidth, this.colorBitmap.PixelHeight),
                         this.colorPixels,
                         this.colorBitmap.PixelWidth * sizeof(int),
                         0);

                    //this.colorBitmap.WritePixels(
                    //        new Int32Rect(0, 0, this.colorBitmap.PixelWidth, this.colorBitmap.PixelHeight),
                    //        this.colorPixels,
                    //        this.colorBitmap.PixelWidth * sizeof(int),
                    //        0);
                }
            }
        }

    }
}
