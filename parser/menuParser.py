# coding=UTF-8

import xlrd
import os
import codecs
import json

CODEC = "utf-8"
menuInfo = {}
with xlrd.open_workbook("menu.xlsx") as workbook:
	sheet = workbook.sheet_by_index(0)

	colList = []
	for col in xrange(0, sheet.ncols):
		colList.append((sheet.cell(1, col).value).encode(CODEC));

		
	for row in xrange(2,sheet.nrows):
		tmpDict = {}
		for col in xrange(0, sheet.ncols):
			colName = colList[col]
			tmpDict[colName] = (sheet.cell(row, col).value).encode(CODEC)
		
		menuInfo[tmpDict["menuID"]] = tmpDict

	# print menuInfo
	
# with codecs.open("../Assets/Resources/menu/menu.txt", "w", "ascii") as wf:
with open("../Assets/Resources/menu/menu.txt", "w") as wf:
	wf.write("{\"data\":[\n")
	counter = 1;
	for menuID, menu in menuInfo.iteritems():
		# txt = "\"%s\":%s"%(menuID.encode("ascii"),json.dumps(menu, ensure_ascii=False))
		txt = json.dumps(menu, ensure_ascii=False)
		if ( counter < len(menuInfo) ):
			txt += ","
		
		wf.write("%s\n"%txt)
		counter += 1
		
	wf.write("]}")
	wf.flush()
	
os.system("pause")