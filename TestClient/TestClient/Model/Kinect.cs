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
        private bool detectedMotion = false;
        private bool alertWait = true;
        private int noOfMovedPixels = 0;
        private int noOfNotMovedPixels = 0;
        private double percentage = 0;

        public Kinect()
        {
            
        }

        public delegate void DetectionHandler(object myObject, EventArgs myArgs);
        public event DetectionHandler OnMotionDetected;

        /// <summary>
        /// Active Kinect sensor
        /// </summary>
        private KinectSensor sensor;

        /// <summary>
        /// Bitmap that will hold color information
        /// </summary>

        /// <summary>
        /// Intermediate storage for the depth data received from the camera
        /// </summary>

        private DepthImagePixel[] depthPixelsComp = new DepthImagePixel[307200];
        private short[] depthPixelsRes = new short[307200];
        private DepthImagePixel[] depthPixels;

        //private DepthImageFrame depthFrameComp;

        /// <summary>
        /// Intermediate storage for the depth data converted to color
        /// </summary>
        private byte[] colorPixels;


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

        public bool Start()
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
                this.colorPixels = new byte[this.sensor.DepthStream.FramePixelDataLength * sizeof(int)];

                // This is the bitmap we'll display on-screen
                //this.colorBitmap = new Bitmap(this.sensor.DepthStream.FrameWidth, this.sensor.DepthStream.FrameHeight, 96.0, 96.0, PixelFormats.Bgr32, null);
                // Set the image we display to point to the bitmap where we'll put the image data
                //this.Image.Source = this.colorBitmap;
                //ProjClient.Form1.pictureBox2.Soruce = this.colorBitmap;
                // ProjClient.Form1.set_image(colorBitmap);

                // Add an event handler to be called whenever there is new depth frame data
                this.sensor.DepthFrameReady += this.SensorDepthFrameReady;

                // Start the sensor!
                try
                {
                    this.sensor.Start();
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
                // Problem here
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
                    if (frameIterator == 100) {
                        alertWait = false;
                        frameIterator = 0;
                    }
                    // Copy the pixel data from the image to a temporary array
                    depthFrame.CopyDepthImagePixelDataTo(this.depthPixels);

                    //If first frame, make ref frame.
                    if (frameCounter == 0 || frameCounter >= noOfFrames)
                    {
                        //detected.Content = "NoOfFrames reached" ;
                        depthFrame.CopyDepthImagePixelDataTo(this.depthPixelsComp);
                        frameCounter = 0;
                    }

                    // Get the min and max reliable depth for the current frame
                    int minDepth = depthFrame.MinDepth;
                    int maxDepth = depthFrame.MaxDepth;

                    // Convert the depth to RGB
                    int colorPixelIndex = 0;
                    for (int i = 0; i < this.depthPixels.Length; ++i)
                    {
                        // Get the depth for this pixel
                        short depth = depthPixels[i].Depth;
                        depthPixelsRes[i] = depth;

                        // To convert to a byte, we're discarding the most-significant
                        // rather than least-significant bits.
                        // We're preserving detail, although the intensity will "wrap."
                        // Values outside the reliable depth range are mapped to 0 (black).

                        // Note: Using conditionals in this loop could degrade performance.
                        // Consider using a lookup table instead when writing production code.
                        // See the KinectDepthViewer class used by the KinectExplorer sample
                        // for a lookup table example.
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
                            //detectedMotion = true;
                        }
                        else
                        {
                            //detectedMotion = false;
                        }
                    }

                    percentage = ((double)noOfMovedPixels / (double) 307200) * 100;

                    if (percentage > 22.00)
                    {
                        detectedMotion = true;
                        // Fire event
                        EventArgs eventargs = new EventArgs();
                        // Only fire if bool is false 
                        if (alertWait == false) {
                            alertWait = true;
                            OnMotionDetected(this, eventargs);
 
                        }
                        
                        // Need to include some sort of wait
                    }
                    else {
                        detectedMotion = false;
                    }
                }
            }
        }


        public bool getDetected()
        {
            return detectedMotion;
        }
    }
}
