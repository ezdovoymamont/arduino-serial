unsigned char adc0[] = "$ADC0=XX,C\r\n";
unsigned char adc1[] = "$ADC1=XX,C\r\n";
unsigned char get_adc0[] = "$GET_ADCX,C\r\n";
unsigned char buff[] = "$GET_ADCX,C\r\n";

#define VAL_OFFSET 6
#define C_OFFSET 9
#define C_GET_OFFSET 10
#define ADC_NUM_OFFSET 8

void fillADC(unsigned char *adc, short value){
  *((short*)(adc+VAL_OFFSET)) = value;
  //
  unsigned char c = 0;
  for(int i=0;i<C_OFFSET;i++)
    c ^= adc[i];
  adc[C_OFFSET] = c;
}
/*
void timeIsOut(){
  unsigned char odd = 0;
  if(odd){
    short val = 0x4142;
    fillADC(adc0, val);
    Serial.write(adc0,sizeof(adc0));
  } else {
    short val = 0x5354;
    fillADC(adc1, val);
    Serial.write(adc1,sizeof(adc1));
  }
  odd = !odd;  
}
*/
void setup() {
  Serial.begin(9600); 
}

void loop() {
  int data;
  while(Serial.available() < 1){
  }
  data = Serial.read();
  if(data != '$')
      return;
  
  while(Serial.available() < sizeof(get_adc0)-1){
  }
  Serial.readBytes(buff+1, sizeof(get_adc0)-1);
  unsigned char c = 0;
  for(int i=0;i<C_GET_OFFSET;i++)
    c ^= buff[i];

  if (c != buff[C_GET_OFFSET]){
    //Serial.write(c);
    return;
  }
    
  if(memcmp(buff+1, "GET_ADC0" ,8) == 0){
    fillADC(adc0, 0x4142);
    Serial.write(adc0, sizeof(adc0));
    return;
  }
  if(memcmp(buff+1, "GET_ADC1" ,8) == 0){
    fillADC(adc1, 0x4344);
    Serial.write(adc1, sizeof(adc1));
    return;
  }
/*  if(buff[ADC_NUM_OFFSET] == '0'){
    fillADC(adc0, 0x4142);
    Serial.write(adc0, sizeof(adc0));
    return;
  }
  if(buff[ADC_NUM_OFFSET] == '1'){
    fillADC(adc1, 0x4344);
    Serial.write(adc1, sizeof(adc1));
    return;
  }
 */ 
  
/*  unsigned long currentMillis = millis();
  if (currentMillis - previousMillis >= interval) {
    previousMillis = currentMillis;
    timeIsOut();
  }*/
}

