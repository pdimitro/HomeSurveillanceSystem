# import the necessary packages
import uuid
from time import time
import os

class TempImage:
	def __init__(self, basePath="./tmp/", ext=".jpg"):
		# construct the file path
		#self.path = "{base_path}/{rand}{ext}".format(base_path=basePath,
		#	rand=str(uuid.uuid4()), ext=ext)
		counter = (int(time()) % 10)
		self.path = "{base_path}/{rand}{ext}".format(base_path=basePath,
			rand=str(counter), ext=ext)

	def cleanup(self):
		# remove the file
		os.remove(self.path)
		
class TempVideo:
	def __init__(self, basePath="./", ext=".avi"):
		# construct the file path
		self.path = "{base_path}/{rand}{ext}".format(base_path=basePath,
			rand=str(uuid.uuid4()), ext=ext)

	def cleanup(self):
		# remove the file
		os.remove(self.path)
