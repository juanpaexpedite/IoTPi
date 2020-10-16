String inputMessage = "";
bool messageComplete = false;
const String measure = "Temperature";
const int areaId = 0;
const int sensorId = 0;
const String areaName = "Window 0";
const String sensorName = "Left Sensor";
double value = 0.0;
const String units = "C";

void setup() 
{
Serial.begin(9600);
inputMessage.reserve(255);
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
    value = random(10, 20);

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