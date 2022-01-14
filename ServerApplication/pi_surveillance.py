# USAGE
# python pi_surveillance.py --conf conf.json

# import the necessary packages
from pyimagesearch.tempimage import TempImage
from pyimagesearch.tempimage import TempVideo
from picamera.array import PiRGBArray
from picamera import PiCamera
import argparse
import warnings
import datetime
import dropbox
import imutils
import json
import time
import cv2
import socket
import base64
import threading
import picamera
from threading import Thread
import numpy as np
from PIL import Image
import io

bind_ip = '192.168.100.5'
bind_port = 9999
bVideoInProgress = 0
global videoName
bTriggerVideoFlag = 0
bSendPicture = 0
global g_clientSock
global g_address
global bConnectInit
global imageToSend
counter = 0
prevMili = 0

server = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
server.bind((bind_ip, bind_port))
server.listen(5)  # max backlog of connections

print('Listening on {}:{}'.format(bind_ip, bind_port))	

# construct the argument parser and parse the arguments
ap = argparse.ArgumentParser()
ap.add_argument("-c", "--conf", required=True,
	help="path to the JSON configuration file")
args = vars(ap.parse_args())

# filter warnings, load the configuration and initialize the Dropbox
# client
warnings.filterwarnings("ignore")
conf = json.load(open(args["conf"]))
client = None

# check to see if the Dropbox should be used
if conf["use_dropbox"]:
	# connect to dropbox and start the session authorization process
	client = dropbox.Dropbox(conf["dropbox_access_token"])
	print("[SUCCESS] dropbox account linked")

# initialize the camera and grab a reference to the raw camera capture
camera = PiCamera()
camera.resolution = tuple(conf["resolution"])
camera.framerate = conf["fps"]
rawCapture = PiRGBArray(camera, size=tuple(conf["resolution"]))

# allow the camera to warmup, then initialize the average frame, last
# uploaded timestamp, and frame motion counter
print("[INFO] warming up...")
time.sleep(conf["camera_warmup_time"])
avg = None
lastUploaded = datetime.datetime.now()
motionCounter = 0
stopMotionCounter = 0
bConnectInit = 0
#bTriggerVideoFlag = 0

#************************************************************************
# DESCRIPTION : Function for socket connection handling
# PARAMETERS : client_socket - client Socket
# RETURN VALUE : none
#************************************************************************
def handle_client_connection(client_socket):
	request = client_socket.recv(1024)
	print('Received {}'.format(request))
	client_socket.send(('ACK!').encode())

#************************************************************************
# DESCRIPTION : Function for sending warning event to the client
# PARAMETERS : client_socket - client Socket
# 			   time - the exact moment of the event
# RETURN VALUE : none
#************************************************************************
def send_warning_event(client_socket, time):
	print('Warning event fired')
	client_socket.send('NOTIFY'.encode())
	client_socket.send(time.encode())

#************************************************************************
# DESCRIPTION : Function for sending binary picture the client
# PARAMETERS : client_socket - client Socket
# 			   address - address of the client
#			   image - jpg image to send
# RETURN VALUE : none
#************************************************************************	
def send_picture(client_socket, address, image):
	global g_clientSock	
	global g_address
	image = "./new_0.jpg"
	bytesVar = cv2.imencode('.jpg', imageToSend)[1].tostring()
	bytesVar = imageToSend.imu.read()
	cv2.imwrite(image, imageToSend)
	myfile = open(image, 'rb')
	bytes = myfile.read()
	base64_file = base64.encodestring(bytes)

	base64_file = base64.b64encode(rawBytes.read())
	size = len(base64_file)
	g_clientSock.sendto(str(size).encode('utf-8'),g_address)
	conf = g_clientSock.recv(4096)
	g_clientSock.send(base64_file)

#************************************************************************
# DESCRIPTION : Function is invoked when new activity is evaluated 
# PARAMETERS : None
# RETURN VALUE : none
#************************************************************************	
def trigger_warning_event():
	bTriggerVideoFlag =1

#************************************************************************
# DESCRIPTION : Function for starting the server, it accepts 
#				new connections and send events
# PARAMETERS : bTriggerVideoFlag 
#			   bSendPicture
# RETURN VALUE : none
#************************************************************************	
def run_server(bTriggerVideoFlag,bSendPicture):
	while True:
		global bConnectInit
		global g_clientSock	
		global g_address
		client_sock, address = server.accept()
		if( 0 == bConnectInit ):
			
			g_clientSock = client_sock
			g_address = address
			print("Init finished", g_clientSock)
			bConnectInit = 1			
			print('Accepted connection from {}:{}'.format(address[0], address[1]))
		else:
			if( 0 != bTriggerVideoFlag ):
				print("send event")
				client_handler2 = threading.Thread(
						target =send_warning_event,
						args=(client_sock,) )
				bTriggerVideoFlag =0
				client_handler2.start()
				
# capture frames from the camera
for f in camera.capture_continuous(rawCapture, format="bgr", use_video_port=True):
	useSocket = conf["sockets_use"]
	if( 0 != useSocket ):
		thread.join()
		bTriggerFlag = 1
		thread = Thread(target = run_server,
		args=(bTriggerVideoFlag,bSendPicture))
		thread.start()

	# grab the raw NumPy array representing the image and initialize
	# the timestamp and occupied/unoccupied text
	frame = f.array
	timestamp = datetime.datetime.now()
	text = "Unoccupied"

	# resize the frame, convert it to grayscale, and blur it
	img1 = frame
	frame = imutils.resize(frame, width=500)	
	gray = cv2.cvtColor(frame, cv2.COLOR_BGR2GRAY)
	gray = cv2.GaussianBlur(gray, (21, 21), 0)
	
	# if the average frame is None, initialize it
	if avg is None:
		print("[INFO] starting background model...")
		avg = gray.copy().astype("float")
		rawCapture.truncate(0)
		continue

	# accumulate the weighted average between the current frame and
	# previous frames, then compute the difference between the current
	# frame and running average
	cv2.accumulateWeighted(gray, avg, 0.5)
	frameDelta = cv2.absdiff(gray, cv2.convertScaleAbs(avg))

	# threshold the delta image, dilate the thresholded image to fill
	# in holes, then find contours on thresholded image
	thresh = cv2.threshold(frameDelta, conf["delta_thresh"], 255,
		cv2.THRESH_BINARY)[1]
	thresh = cv2.dilate(thresh, None, iterations=2)
	cnts = cv2.findContours(thresh.copy(), cv2.RETR_EXTERNAL,
		cv2.CHAIN_APPROX_SIMPLE)
	cnts = cnts[0] if imutils.is_cv2() else cnts[1]

	# loop over the contours
	for c in cnts:
		# if the contour is too small, ignore it
		if cv2.contourArea(c) < conf["min_area"]:
			continue

		# compute the bounding box for the contour, draw it on the frame,
		# and update the text
		(x, y, w, h) = cv2.boundingRect(c)
		cv2.rectangle(frame, (x, y), (x + w, y + h), (0, 255, 0), 2)
		text = "Occupied"

	# draw the text and timestamp on the frame
	ts = timestamp.strftime("%A %d %B %Y %I:%M:%S%p")
	cv2.putText(frame, "Room Status: {}".format(text), (10, 20),
		cv2.FONT_HERSHEY_SIMPLEX, 0.5, (0, 0, 255), 2)
	cv2.putText(frame, ts, (10, frame.shape[0] - 10), cv2.FONT_HERSHEY_SIMPLEX,
		0.35, (0, 0, 255), 1)

	seconds = round(time.time())
	if((seconds - prevMili) > 1):
		imageToSend = imutils.resize(frame, width=320)
		t = TempImage()
		counter = counter + 1
		cv2.imwrite(t.path , imageToSend)

	# check to see if the room is occupied
	if text == "Occupied":
		# check to see if enough time has passed between uploads
		if (timestamp - lastUploaded).seconds >= conf["min_upload_seconds"]:
			# increment the motion counter
			motionCounter += 1

			# check to see if the number of frames with consistent motion is
			# high enough
			if motionCounter >= conf["min_motion_frames"]:
				# check to see if dropbox sohuld be used
				if conf["use_dropbox"]:
					if conf["use_video"]:
						if( 0 == bVideoInProgress ):
							print("startRecordingVideo")
							if( 0 != useSocket ):
								trigger_warning_event()
								#client_handler2 = threading.Thread(
								#		target =send_warning_event,
								#		args=(g_clientSock,ts,) )
								#client_handler2.start()
							vid = TempVideo()
							bVideoInProgress = 1
							videoName = vid
							writer = cv2.VideoWriter(vid.path, cv2.cv.CV_FOURCC(*"XVID"), 2,(640,480))
					else:
						# write the image to temporary file
						t = TempImage()
						cv2.imwrite(t.path, frame)

						# upload the image to Dropbox and cleanup the tempory image
						print("[UPLOAD] {}".format(ts))
						path = "/{base_path}/{timestamp}.jpg".format(
						  base_path=conf["dropbox_base_path"], timestamp=ts)
						client.files_upload(open(t.path, "rb").read(), path)
						t.cleanup()
				else:
					
					#trigger_warning_event()
					client_handler2 = threading.Thread(
									target =send_warning_event,
									args=(g_clientSock,ts,) )
					client_handler2.start()
					if conf["use_video"]:
						if( 0 == bVideoInProgress ):
							print("startRecordingVideo")
							vid = TempVideo()
							bVideoInProgress = 1
							writer = cv2.VideoWriter(vid.path, cv2.cv.CV_FOURCC(*"XVID"), 2,(640,480))
					else:
						t = TempImage()
						cv2.imwrite(t.path, frame)


				# update the last uploaded timestamp and reset the motion
				# counter
				lastUploaded = timestamp
				motionCounter = 0

	# otherwise, the room is not occupied
	else:
		motionCounter = 0
		if( 1 == bVideoInProgress ):
			stopMotionCounter += 1
		
			if stopMotionCounter >= conf["min_motion_frames"]:
				print("Stop recordig")
				bVideoInProgress = 0
				#cv2.destroyAllWindows()
				writer.release()
				if conf["use_video"]:
					print("[UPLOAD] {}".format(ts))
					name = 'video_' + str(round(time.time())) + '.avi'
					path = "/{base_path}/{timestamp}.avi".format(
					   base_path=conf["dropbox_base_path"], timestamp=name)
					client.files_upload(open(videoName.path, "rb").read(), path)
	if( 0 != bVideoInProgress ):
		writer.write(img1)
		#writer.write(np.random.randint(0, 255, (480,640,3)).astype('uint8'))
			

	# check to see if the frames should be displayed to screen
	if conf["show_video"]:
		# display the security feed
		cv2.imshow("Security Feed", frame)
		key = cv2.waitKey(1) & 0xFF

		# if the `q` key is pressed, break from the lop
		if key == ord("q"):
			break

	# clear the stream in preparation for the next frame
	rawCapture.truncate(0)
