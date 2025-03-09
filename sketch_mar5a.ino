#include <ESP8266WiFi.h>
#include <ESP8266HTTPClient.h>
#include <WiFiClient.h>
#include <SPI.h>
#include <MFRC522.h>

#define STASSID "OMiLAB"   
#define STAPSK "digifofulbs" 

constexpr uint8_t RST_PIN = D3;
constexpr uint8_t SS_PIN = D4;

MFRC522 rfid(SS_PIN, RST_PIN);
WiFiClient client;
HTTPClient http;

const char* serverUrl = "http://10.14.11.141:3000/api/data";

void setup() {
  Serial.begin(115200);
  SPI.begin();
  rfid.PCD_Init();
  
  // Conectare la WiFi
  WiFi.begin(STASSID, STAPSK);
  Serial.print("Conectare la WiFi...");
  while (WiFi.status() != WL_CONNECTED) {
    delay(500);
    Serial.print(".");
  }
  Serial.println("\nConectat la WiFi!");
  Serial.print("IP Address: ");
  Serial.println(WiFi.localIP());
}

void loop() {
  if (!rfid.PICC_IsNewCardPresent()) return;
  if (!rfid.PICC_ReadCardSerial()) return;

  String tag = "";
  for (byte i = 0; i < rfid.uid.size; i++) {
    tag += String(rfid.uid.uidByte[i], HEX);
  }
  Serial.println("ID Tag: " + tag);

  // Formateazz datele JSON
  String jsonPayload = "{\"id\": \"" + tag + "\", \"name\": \"Magda si Edi\"}";

  if (WiFi.status() == WL_CONNECTED) {
    http.begin(client, serverUrl);
    http.addHeader("Content-Type", "application/json");

    int httpResponseCode = http.POST(jsonPayload);
    
    if (httpResponseCode > 0) {
      Serial.print("Raspuns server: ");
      Serial.println(httpResponseCode);
      Serial.println(http.getString());
    } else {
      Serial.print("Eroare la trimitere: ");
      Serial.println(http.errorToString(httpResponseCode));
    }

    http.end();
  } else {
    Serial.println("WiFi deconectat. Nu pot trimite date.");
  }

  delay(2000);
}