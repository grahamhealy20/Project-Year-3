using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Ports;
using System.Web;
using TestClient.Model;

namespace TestClient.Model
{
    public class Sensor
    {
        /* CODE LOADED ONTO ARDUINO MICROPROCESSOR
       
       #include <math.h>         //loads the more advanced math functions
 
        void setup() {            //This function gets called when the Arduino starts
          Serial.begin(115200);   //This code sets up the Serial port at 115200 baud rate
        }
 
        double Thermister(int RawADC) {  //Function to perform the fancy math of the Steinhart-Hart equation
         double Temp;
         Temp = log(((10240000/RawADC) - 10000));
         Temp = 1 / (0.001129148 + (0.000234125 + (0.0000000876741 * Temp * Temp ))* Temp );
         Temp = Temp - 273.15;              // Convert Kelvin to Celsius
         Temp = (Temp * 9.0)/ 5.0 + 32.0; // Celsius to Fahrenheit - comment out this line if you need Celsius
         return Temp;
        }
 
        void loop() {             //This function loops while the arduino is powered
          int val;                //Create an integer variable
          double temp;            //Variable to hold a temperature value
          val=analogRead(0);      //Read the analog port 0 and store the value in val
          temp=Thermister(val);   //Runs the fancy math on the raw analog value
          Serial.println(temp);   //Print the value to the serial port
          delay(1000);            //Wait one second before we do it again
        }       
         */

        // http://stackoverflow.com/questions/195483/c-sharp-check-if-a-com-serial-port-is-already-open
        private SerialPort MyPort = new SerialPort("COM3", 115200);
        private double temperature;

        public delegate void TemperatureHandler(object myObject,
                                    TemperatureEventArgs myArgs);

        public event TemperatureHandler OnTemperatureReceived;

        public delegate void TemperatureGUIHandler(object myObject,
                              TemperatureEventArgs myArgs);

        public event TemperatureGUIHandler OnTemperatureGUIReceived;
        public Sensor()
        {
            MyPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);    
        }

        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            string line = MyPort.ReadLine();
            TemperatureEventArgs args = new TemperatureEventArgs();
            args.temp = line;

            // If temperature is greater than a set temp. (Default: Room Temperature)
            if (line != null || line != "") {
              try
              {
                double tempDouble = Convert.ToDouble(line);
                temperature = tempDouble;
                if (tempDouble > 27)
                {
                  OnTemperatureReceived(this, args);
                }
                OnTemperatureGUIReceived(this, args);
              }
              catch (Exception)
              {
                
              } 
            
          }
        }

        public void Start()
        {
            OpenMyPort();
            Console.WriteLine("BaudRate {0}", MyPort.BaudRate);
        }
        public void Stop()
        {
            try
            {
                MyPort.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void OpenMyPort()
        {
            try
            {
                MyPort.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error opening my port: {0}", ex.Message);
            }
        }
        public double GetTemperature()
        {
            return temperature;
        }
    }
}