#include <ESP8266WiFi.h>
#include <ESP8266HTTPClient.h>
#include <WiFiClient.h>
#include <SPI.h>
#include <MFRC522.h>
#include <time.h> 

#define STASSID "OMiLAB"   
#define STAPSK "digifofulbs" 

constexpr uint8_t RST_PIN = D3;
constexpr uint8_t SS_PIN = D4;

MFRC522 rfid(SS_PIN, RST_PIN);
WiFiClient client;
HTTPClient http;

const char* serverUrl = "http://10.14.11.101:5151/api/colectari";

// Stocare ID-uri cand WiFi nu functioneaza
#define MAX_SAVED_IDS 10
String idSalvate[MAX_SAVED_IDS];
int nrSalvate = 0;

void setupTime() {
    configTime(0, 0, "pool.ntp.org");  // Conecteaza automat la serverul NTP
    Serial.print("Sincronizare timp...");

    while (time(nullptr) < 100000) {  // Asteapta sincronizarea
        Serial.print(".");
        delay(500);
    }
    Serial.println(" OK!");
}

String getCurrentTime() {
    time_t now = time(nullptr);  // Obtine timpul curent
    struct tm* timeinfo = localtime(&now);  // Converteste în format local

    char buffer[25];
    strftime(buffer, sizeof(buffer), "%Y-%m-%dT%H:%M:%S", timeinfo); // Format ISO 8601
    return String(buffer);
}

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
  setupTime();
}

void loop() {
  if (!rfid.PICC_IsNewCardPresent()) return;
  if (!rfid.PICC_ReadCardSerial()) return;

  String tag = "";
  for (byte i = 0; i < rfid.uid.size; i++) {
    tag += String(rfid.uid.uidByte[i], HEX);
  }
  Serial.println("ID Tag: " + tag + " DATA COLECTARII: " +  getCurrentTime());

  // Formateaza datele JSON
  String adresa = "Strada Test 123";
  String jsonPayload = "{\"PubelaId\": \"" + tag + "\", \"TimpColectare\": \"" + getCurrentTime() + "\", \"Adresa\": \"" + adresa + "\"}";

  if (WiFi.status() == WL_CONNECTED) {
    http.begin(client, serverUrl);
    http.addHeader("Content-Type", "application/json");

    int httpResponseCode = http.POST(jsonPayload);
    
    if (httpResponseCode > 0) {
      Serial.print("Raspuns server: ");
      Serial.println(httpResponseCode);
      Serial.println(http.getString());
    } else {
      Serial.println("Eroare la trimitere, mai incercam o data...");
      
      // A doua incercare
      delay(1000);
      http.begin(client, serverUrl);
      http.addHeader("Content-Type", "application/json");
      httpResponseCode = http.POST(jsonPayload);
      
      if (httpResponseCode > 0) {
        Serial.print("Raspuns server (a doua incercare): ");
        Serial.println(httpResponseCode);
        Serial.println(http.getString());
      } else {
        Serial.println("Trimitere esuata, salvez datele pentru mai tarziu.");
        if (nrSalvate < MAX_SAVED_IDS) {
          idSalvate[nrSalvate++] = jsonPayload;
        } else {
          Serial.println("Memorie plina, nu pot salva mai multe date.");
        }
      }
    }
    http.end();
  } else {
    Serial.println("WiFi deconectat. Salvez datele.");
    if (nrSalvate < MAX_SAVED_IDS) {
      idSalvate[nrSalvate++] = jsonPayload;
    } else {
      Serial.println("Memorie plina, nu pot salva mai multe date.");
    }
  }

  // Challenge 2: Trimitere date salvate cand revine conexiunea
  if (WiFi.status() == WL_CONNECTED && nrSalvate > 0) {
    Serial.println("Trimitere date salvate...");
    for (int i = 0; i < nrSalvate; i++) {
      http.begin(client, serverUrl);
      http.addHeader("Content-Type", "application/json");

      int httpResponseCode = http.POST(idSalvate[i]);
      
      if (httpResponseCode > 0) {
        Serial.print("Raspuns server (date salvate): ");
        Serial.println(httpResponseCode);
      } else {
        Serial.println("Eroare la trimiterea datelor salvate.");
      }

      http.end();
      delay(1000);
    }
    nrSalvate = 0; 
  }

  delay(2000);
}