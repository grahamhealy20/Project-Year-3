using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Forms;
//using System.Windows.Media;
//using System.Windows.Media.Imaging;
using Microsoft.Kinect;
using System.ComponentModel;
using System.Timers;
using System.Drawing;


namespace TestClient.Model
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

        /// Active Kinect sensor
        private KinectSensor sensor;

        /// Intermediate storage for the depth data received from the camera
        private DepthImagePixel[] depthPixelsComp = new DepthImagePixel[307200];
        private short[] depthPixelsRes = new short[307200];
        private DepthImagePixel[] depthPixels;

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

        public bool StartKinect()
        {
            // load sensor
            this.enumerate();
            if (null != this.sensor)
            {
                // Turn on the depth stream to receive depth frames
                this.sensor.DepthStream.Enable(DepthImageFormat.Resolution640x480Fps30);
                //this.sensor.DepthStream.Enable(DepthImageFormat.Resolution80x60Fps30);
                // Allocate space to put the depth pixels we'll receive
                this.depthPixels = new DepthImagePixel[this.sensor.DepthStream.FramePixelDataLength];
                // Allocate space to put the color pixels we'll create
                // Add an event handler to be called whenever there is new depth frame data
                this.sensor.DepthFrameReady += this.SensorDepthFrameReady;

                // Start the sensor!
                try
                {
                    this.sensor.Start();
                    // Fire information event
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
                // Fire connect camera message
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
                    if (frameIterator == 150) {
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

                    // Get the min and max reliable depth for the current frame
                    int minDepth = depthFrame.MinDepth;
                    int maxDepth = depthFrame.MaxDepth;

                    for (int i = 0; i < this.depthPixels.Length; ++i)
                    {
                        // Get the depth for this pixel
                        short depth = depthPixels[i].Depth;
                        depthPixelsRes[i] = depth;
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
                    percentage = ((double)noOfMovedPixels / (double) 307200) * 100;
                    if (percentage > 22.00)
                    {
                        EventArgs eventargs = new EventArgs();
                        // Only fire if bool is false 
                        if (alertWait == false) {
                            alertWait = true;
                            OnMotionDetected(this, eventargs);
                        }
                    }                  
                }
            }
        }

    }
}
