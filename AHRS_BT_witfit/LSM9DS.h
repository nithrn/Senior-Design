#include <Adafruit_LSM9DS1.h>
#include <Wire.h>

#define TCAADDR 0x70
void tcaselect(uint8_t i) {
  if (i > 7) return;
  
  Wire.beginTransmission(TCAADDR);
  Wire.write(1 << i);
  Wire.endTransmission();
//  Serial.println("I2C Bus Selected");  
}


//i2c contructor calls
//Adafruit_LSM9DS1 lsm9ds = Adafruit_LSM9DS1();
Adafruit_LSM9DS1 lsm9ds = Adafruit_LSM9DS1(1);
Adafruit_LSM9DS1 lsm9ds2 = Adafruit_LSM9DS1(2);


bool init_sensors(void) {
  tcaselect(4);
  if (!lsm9ds.begin()) {
    return false;
  }
  tcaselect(7);
  if (!lsm9ds2.begin()) {
    return false;
  }
  tcaselect(4);
  accelerometer = &lsm9ds.getAccel();
  gyroscope = &lsm9ds.getGyro();
  magnetometer = &lsm9ds.getMag();
  
  tcaselect(7);
  accelerometer2 = &lsm9ds2.getAccel();
  gyroscope2 = &lsm9ds2.getGyro();
  magnetometer2 = &lsm9ds2.getMag();


  return true;
}

void setup_sensors(void) {
  // set lowest range
#ifdef __LSM9DS0_H__
//  lsm9ds.setupAccel(lsm9ds.LSM9DS0_ACCELRANGE_2G);
//  lsm9ds.setupMag(lsm9ds.LSM9DS0_MAGGAIN_4GAUSS);
//  lsm9ds.setupGyro(lsm9ds.LSM9DS0_GYROSCALE_500DPS);
  lsm9ds.setupGyro(lsm9ds.LSM9DS0_GYROSCALE_245DPS);
  lsm9ds.setupAccel(lsm9ds.LSM9DS0_ACCELRANGE_4G);
  lsm9ds.setupMag(lsm9ds.LSM9DS0_MAGGAIN_4GAUSS);

#else
//  lsm9ds.setupAccel(lsm9ds.LSM9DS1_ACCELRANGE_2G);
//  lsm9ds.setupMag(lsm9ds.LSM9DS1_MAGGAIN_4GAUSS);
//  lsm9ds.setupGyro(lsm9ds.LSM9DS1_GYROSCALE_500DPS);
  tcaselect(4);
  lsm9ds.setupGyro(lsm9ds.LSM9DS1_GYROSCALE_500DPS);
  lsm9ds.setupAccel(lsm9ds.LSM9DS1_ACCELRANGE_8G);
  lsm9ds.setupMag(lsm9ds.LSM9DS1_MAGGAIN_4GAUSS);

  tcaselect(7);
  lsm9ds2.setupGyro(lsm9ds2.LSM9DS1_GYROSCALE_500DPS);
  lsm9ds2.setupAccel(lsm9ds2.LSM9DS1_ACCELRANGE_8G);
  lsm9ds2.setupMag(lsm9ds2.LSM9DS1_MAGGAIN_4GAUSS);

#endif
}
