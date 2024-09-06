import json

import pyautogui
import time
import paho.mqtt.client as mqtt
import ssl

port = 8883
broker = "065dfdc87c034835a0618c9577b5beb8.s1.eu.hivemq.cloud"
broker_tls="065dfdc87c034835a0618c9577b5beb8.s1.eu.hivemq.cloud:8883"
broker_tlswebsocket="065dfdc87c034835a0618c9577b5beb8.s1.eu.hivemq.cloud:8884/mqtt"
username = "mouse_server"
password = "Mouseclient@1"
topic = "mouse/movements"

def on_connect(client, userdata, flags, rc):
    print("Connected with result code "+str(rc))

mqtt_client = mqtt.Client(callback_api_version=mqtt.CallbackAPIVersion.VERSION2
                          ,client_id="mdelta_yclient"
                          ,clean_session=True
                          ,userdata=None
                          ,protocol=mqtt.MQTTv311
                          ,transport="tcp"
                          ,reconnect_on_failure=True
                          ,manual_ack=False)

mqtt_client.ws_set_options(path="/")
mqtt_client.on_connect = on_connect
mqtt_client.tls_set()
mqtt_client.username_pw_set(username, password)
result = mqtt_client.connect(host=broker
                             ,port=port
                             ,keepalive=60)

#Check if client is connected or not
if result != 0:
    print("Could not connect to MQTT Server")
    sys.exit(-1)


#working code that publishes the message for testing purpose
# resultPublish = mqtt_client.publish(topic,"blablabla3",qos=0)

#is message getting published?
#print("Is Message got published? ", resultPublish.is_published())

#Format looks like {'tdelta_ype': 'move', 'delta_x': 642, 'delta_y': 970}

# Changes as per https://docs.google.com/document/d/1cdkMqVwNma-kTNs2hdkxf66jkxs6oZiKX4sQLJf_Xdc/edit#heading=h.3mx4fdelta_ywm1vwn

# Get the screen resolution
screen_width, screen_height = pyautogui.size()

# Calculate the center coordinates
center_x = screen_width / 2
center_y = screen_height / 2

prev_delta_x = 0
prev_delta_y = 0

while True:
    x, y = pyautogui.position()

    # Calculate the delta coordinates
    delta_x = x - center_x
    delta_y = y - center_y
    
    # Create MQTT message based on mouse position        
    # if prev_delta_x == 0 and prev_delta_y == 0:
        
    if (prev_delta_x!=delta_x or prev_delta_y!=delta_y): 
        prev_delta_x = delta_x
        prev_delta_y = delta_y 
        msg=json.dumps({"type": "move","x":  delta_x,"y": delta_y})        
        mqtt_client.publish(topic, str(msg))
        pyautogui.moveTo(center_x, center_y)

    time.sleep(0.01)  # Adjust sleep time based on desired frequencdelta_y

