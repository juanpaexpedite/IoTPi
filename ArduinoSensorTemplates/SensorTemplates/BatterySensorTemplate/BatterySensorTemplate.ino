#include <AltSoftSerial.h>

AltSoftSerial altser;
const int mybaud = 9600;
unsigned long prevmillis;  

//Command parameters
String inputMessage = "";
bool messageComplete = false;

//Header parameters
const String measure = "Battery";
const int areaId = 0;
const int sensorId = 0;
const String areaName = "Ship";
const String sensorName = "Main";
double value = 0.0;
const String units = "GENERIC";

//Sensor fields
const int NUM_BYTES = 58;
byte data[NUM_BYTES];



void setup() 
{

 delay(200);
 Serial.begin(9600);
  while (!Serial) ;  // wait for Arduino Serial Monitor
 altser.begin(mybaud);   // to AltSoftSerial RX
  Serial.println("AltSoftSerial Receive Test");
  prevmillis = millis();
// inputMessage.reserve(255);

}

String datamessage="";
void loop() 
{

 if (millis() - prevmillis > 1000)
    {
        prevmillis = millis();
    }

    if (altser.available() > 0)
    {
      
        altser.readBytes(data, NUM_BYTES);
         
        int i = 0;
        for (i = 0; i < NUM_BYTES; i++)
        {
          //datamessage.concat(data[i]);

          Serial.print(data[i]);
          Serial.print(" ");
        }
        Serial.println("");
    }


  if (messageComplete) 
  {
    messageComplete = false;
    SendAnswer();
  }
}

void aserialEvent() 
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
    Serial.print("data");
              Serial.println(datamessage);
  }
  else
  {
    Serial.print(inputMessage);
    Serial.println(" COMMAND NOT FOUND");
  }

  inputMessage = "";
}
