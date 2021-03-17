// Full orientation sensing using NXP/Madgwick/Mahony and a range of 9-DoF
// sensor sets.
// You *must* perform a magnetic calibration before this code will work.
//
// To view this data, use the Arduino Serial Monitor to watch the
// scrolling angles, or run the OrientationVisualiser example in Processing.
// Based on  https://github.com/PaulStoffregen/NXPMotionSense with adjustments
// to Adafruit Unified Sensor interface

#include <Adafruit_Sensor_Calibration.h>
#include <Adafruit_AHRS.h>


// NEED TO ADD MYOWARE SENSOR READING
// NEED TO ADD USER CALIBRATION
Adafruit_Sensor *accelerometer, *gyroscope, *magnetometer;
Adafruit_Sensor *accelerometer2, *gyroscope2, *magnetometer2;

// uncomment one combo 9-DoF!
//#include "LSM6DS_LIS3MDL.h"  // can adjust to LSM6DS33, LSM6DS3U, LSM6DSOX...
#include "LSM9DS.h"           // LSM9DS1 or LSM9DS0
//#include "NXP_FXOS_FXAS.h"  // NXP 9-DoF breakout

// pick your filter! slower == better quality output
//NXPSensorFusion filter; // slowest
//Adafruit_Madgwick filter;  // faster than NXP
//Adafruit_Madgwick filter2;  // faster than NXP
Adafruit_Mahony filter;  // fastest/smalleset
Adafruit_Mahony filter2;  // fastest/smalleset

#if defined(ADAFRUIT_SENSOR_CALIBRATION_USE_EEPROM)
  Adafruit_Sensor_Calibration_EEPROM cal;
#else
  Adafruit_Sensor_Calibration_SDFat cal;
#endif

#define FILTER_UPDATE_RATE_HZ 250
#define PRINT_EVERY_N_UPDATES 20
//#define AHRS_DEBUG_OUTPUT


//Declare variables
uint32_t timestamp;
int EMG_pin = 14; //A0
int EMG_data = 0;
float x_val = 0.00;


void setup() {
  Serial1.begin(115200);
  Wire.begin();
//  while (!Serial1) yield();

  if (!cal.begin()) {
    Serial1.println("Failed to initialize calibration helper");
  } else if (! cal.loadCalibration()) {
    Serial1.println("No calibration loaded/found");
  }

  if (!init_sensors()) {
    Serial1.println("Failed to find sensors");
    while (1) delay(10);
  }
//  tcaselect(4);
//  accelerometer->printSensorDetails();
//  gyroscope->printSensorDetails();
//  magnetometer->printSensorDetails();
//  tcaselect(7);
//  accelerometer2->printSensorDetails();
//  gyroscope2->printSensorDetails();
//  magnetometer2->printSensorDetails();

  setup_sensors();
  filter.begin(FILTER_UPDATE_RATE_HZ);
  filter2.begin(FILTER_UPDATE_RATE_HZ);
  timestamp = millis();

  Wire.setClock(400000); // 400KHz
}


void loop() {
  float roll, pitch, heading;
  float gx, gy, gz;
  
  float roll2, pitch2, heading2;
  float gx2, gy2, gz2;
  static uint8_t counter = 0;

  if ((millis() - timestamp) < (1000 / FILTER_UPDATE_RATE_HZ)) {
    return;
  }
  timestamp = millis();
  tcaselect(4);
  // Read the motion sensors
  sensors_event_t accel, gyro, mag;
  accelerometer->getEvent(&accel);
  gyroscope->getEvent(&gyro);
  magnetometer->getEvent(&mag);
  
  tcaselect(7);
  sensors_event_t accel2, gyro2, mag2;
  accelerometer2->getEvent(&accel2);
  gyroscope2->getEvent(&gyro2);
  magnetometer2->getEvent(&mag2);

  
#if defined(AHRS_DEBUG_OUTPUT)
  Serial1.print("I2C took "); Serial1.print(millis()-timestamp); Serial1.println(" ms");
#endif

  cal.calibrate(mag);
  cal.calibrate(accel);
  cal.calibrate(gyro);

  cal.calibrate(mag2);
  cal.calibrate(accel2);
  cal.calibrate(gyro2);
  
  // Gyroscope needs to be converted from Rad/s to Degree/s
  // the rest are not unit-important
  gx = gyro.gyro.x * SENSORS_RADS_TO_DPS;
  gy = gyro.gyro.y * SENSORS_RADS_TO_DPS;
  gz = gyro.gyro.z * SENSORS_RADS_TO_DPS;

  gx2 = gyro2.gyro.x * SENSORS_RADS_TO_DPS;
  gy2 = gyro2.gyro.y * SENSORS_RADS_TO_DPS;
  gz2 = gyro2.gyro.z * SENSORS_RADS_TO_DPS;

  // Update the SensorFusion filter
  filter.update(gx, gy, gz, 
                accel.acceleration.x, accel.acceleration.y, accel.acceleration.z, 
                mag.magnetic.x, mag.magnetic.y, mag.magnetic.z);
  filter2.update(gx2, gy2, gz2, 
                accel2.acceleration.x, accel2.acceleration.y, accel2.acceleration.z, 
                mag2.magnetic.x, mag2.magnetic.y, mag2.magnetic.z);  
#if defined(AHRS_DEBUG_OUTPUT)
  Serial1.print("Update took "); Serial1.print(millis()-timestamp); Serial1.println(" ms");
#endif

  // only print the calculated output once in a while
  if (counter++ <= PRINT_EVERY_N_UPDATES) {
    return;
  }
  // reset the counter
  counter = 0;

#if defined(AHRS_DEBUG_OUTPUT)
  Serial1.print("Raw: ");
  Serial1.print(accel.acceleration.x, 4); Serial1.print(", ");
  Serial1.print(accel.acceleration.y, 4); Serial1.print(", ");
  Serial1.print(accel.acceleration.z, 4); Serial1.print(", ");
  Serial1.print(gx, 4); Serial1.print(", ");
  Serial1.print(gy, 4); Serial1.print(", ");
  Serial1.print(gz, 4); Serial1.print(", ");
  Serial1.print(mag.magnetic.x, 4); Serial1.print(", ");
  Serial1.print(mag.magnetic.y, 4); Serial1.print(", ");
  Serial1.print(mag.magnetic.z, 4); Serial1.println("");
  
  Serial1.print("2 Raw: ");
  Serial1.print(accel2.acceleration.x, 4); Serial1.print(", ");
  Serial1.print(accel2.acceleration.y, 4); Serial1.print(", ");
  Serial1.print(accel2.acceleration.z, 4); Serial1.print(", ");
  Serial1.print(gx2, 4); Serial1.print(", ");
  Serial1.print(gy2, 4); Serial1.print(", ");
  Serial1.print(gz2, 4); Serial1.print(", ");
  Serial1.print(mag2.magnetic.x, 4); Serial1.print(", ");
  Serial1.print(mag2.magnetic.y, 4); Serial1.print(", ");
  Serial1.print(mag2.magnetic.z, 4); Serial1.println("");
#endif

//  if (x_val < 25.0) {
    EMG_data = analogRead(EMG_pin);
//    Serial1.print(x_val);
//    Serial1.print(", ");
    Serial1.print(EMG_data);
    Serial1.print(", ");
  
  // print the heading, pitch and roll for upper arm
    roll = filter.getRoll();
    pitch = filter.getPitch();
    heading = filter.getYaw();
    Serial1.print(roll); //x
    Serial1.print(", ");
    Serial1.print(pitch); //y
    Serial1.print(", ");
    Serial1.print(heading); //z
    Serial1.print(", ");
    
  // print roll, pitch, and heading for forearm
    roll2 = filter2.getRoll();
    pitch2 = filter2.getPitch();
    heading2 = filter2.getYaw();
    Serial1.print(roll2);
    Serial1.print(", ");
    Serial1.print(pitch2);
    Serial1.print(", ");
    Serial1.println(heading2);

//    x_val += 0.1;
//  }
  
  
#if defined(AHRS_DEBUG_OUTPUT)
  Serial1.print("Took "); Serial1.print(millis()-timestamp); Serial1.println(" ms");
#endif
}
