import json
import sys
import time
import paho.mqtt.client as mqtt
import ssl
import pyautogui

port = 8883
broker = "065dfdc87c034835a0618c9577b5beb8.s1.eu.hivemq.cloud"
broker_tls="065dfdc87c034835a0618c9577b5beb8.s1.eu.hivemq.cloud:8883"
broker_tlswebsocket="065dfdc87c034835a0618c9577b5beb8.s1.eu.hivemq.cloud:8884/mqtt"
username = "mouse_server"
password = "Mouseclient@1"
topic = "mouse/movements"
client_identity="clientid2"
transport="tcp"

def on_connect(client, userdata, flags, rc):
    print("Connected with result code "+str(rc))

# mqtt_client = mqtt.Client(callback_api_version=mqtt.CallbackAPIVersion.VERSION2
#                           ,client_id="myclient"
#                           ,clean_session=True
#                           ,userdata=None
#                           ,protocol=mqtt.MQTTv311
#                           ,transport="tcp"
#                           ,reconnect_on_failure=True
#                           ,manual_ack=False)

mqtt_client = mqtt.Client(callback_api_version=mqtt.CallbackAPIVersion.VERSION1
                          ,client_id=client_identity
                          ,protocol=mqtt.MQTTv311
                          ,transport=transport)

#mqtt_client.ws_set_options(path="/")
mqtt_client.on_connect = on_connect
mqtt_client.tls_set()
mqtt_client.username_pw_set(username, password)
result = mqtt_client.connect(host=broker,port=port)

#Check if client is connected or not
if result != 0:
    print("Could not connect to MQTT Server")
    sys.exit(-1)

#Format looks like {'type': 'move', 'x': 642, 'y': 970}

def on_message(client, userdata, message):
    data = json.loads(message.payload.decode())
    print(data['x'],data['y'])
    if data['type'] == "move":
        pyautogui.moveTo(data['x'], data['y'])

subscribed = mqtt_client.subscribe(topic)
print("is client subscribed", subscribed.index)

mqtt_client.on_message = on_message
mqtt_client.loop_forever()