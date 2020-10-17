#include <DallasTemperature.h>

//Command parameters
String inputMessage = "";
bool messageComplete = false;

//Header parameters
const String measure = "Temperature";
const int areaId = 0;
const int sensorId = 0;
const String areaName = "Window 0";
const String sensorName = "Left Sensor";
double value = 0.0;
const String units = "C";

//Sensor fields
const int DataPin = 9;
OneWire oneWireInstance(DataPin);
DallasTemperature DS18B20(&oneWireInstance);
DeviceAddress Area0_Address= {0x28, 0xFF, 0x5A, 0xCE, 0x01, 0x15, 0x03, 0x16};

void setup() 
{
Serial.begin(9600);
inputMessage.reserve(255);
DS18B20.begin();
}


void loop() 
{
  if (messageComplete) 
  {
    messageComplete = false;
    SendAnswer();
  }
}

void serialEvent() 
{
  while (Serial.available()) 
  {
    char inChar = (char)Serial.read();
    if (inChar == '\n') 
    {
      messageComplete = true;
      return;
    }
    inputMessage += inChar;
  }
}
long randNumber;

//Data has 7 parts: Measure,AreaId,SensorId,AreaName,SensorName,Value,Units

String message = "";
void SendAnswer()
{
  if(inputMessage == "header")
  {
    DS18B20.requestTemperatures();
    value = DS18B20.getTempC(Area0_Address);

    message = measure;
    message.concat(";");
    message.concat(areaId);
    message.concat(";");
    message.concat(sensorId);
    message.concat(";");
    message.concat(areaName);
    message.concat(";");
    message.concat(sensorName);
    message.concat(";");
    message.concat(value);
    message.concat(";");
    message.concat(units);

    Serial.println(message);  
  }
  else if(inputMessage == "data")
  {
    Serial.println("#");
  }
  else
  {
    Serial.print(inputMessage);
    Serial.println(" COMMAND NOT FOUND");
  }

  inputMessage = "";
}