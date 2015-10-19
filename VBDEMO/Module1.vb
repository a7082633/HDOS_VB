Option Strict Off
Option Explicit On
Module Module1
	Declare Function ICC_Reader_Open Lib "SSSE32.dll" (ByVal dev_Name As String) As Integer
	'Mifare card
	Declare Function PICC_Reader_Request Lib "SSSE32.dll" (ByVal ReaderHandle As Integer) As Integer
	Declare Function PICC_Reader_anticoll Lib "SSSE32.dll" (ByVal ReaderHandle As Integer, ByVal uid As String) As Integer
	Declare Function PICC_Reader_Select Lib "SSSE32.dll" (ByVal ReaderHandle As Integer, ByVal cardtype As Byte) As Integer
	Declare Function PICC_Reader_Authentication Lib "SSSE32.dll" (ByVal ReaderHandle As Integer, ByVal Mode As Byte, ByVal SecNr As Byte) As Integer
	Declare Function PICC_Reader_Read Lib "SSSE32.dll" (ByVal ReaderHandle As Integer, ByVal Addr As Byte, ByRef data As Byte) As Integer
	Declare Function PICC_Reader_LoadKey Lib "SSSE32.dll" (ByVal ReaderHandle As Integer, ByVal Mode As Short, ByVal SecNr As Short, ByRef key As Byte) As Integer
	Declare Function PICC_Reader_Write Lib "SSSE32.dll" (ByVal ReaderHandle As Integer, ByVal Addr As Byte, ByRef data As Byte) As Integer
	
	'CPU card
	Declare Function ICC_SetCpupara Lib "SSSE32.dll" (ByVal ReaderHandle As Integer, ByVal ICC_Slot_No As Byte, ByVal cpupro As Byte, ByVal cpuetu As Byte) As Integer
	Declare Function ICC_Reader_pre_PowerOn Lib "SSSE32.dll" (ByVal ReaderHandle As Integer, ByVal ICC_Slot_No As Byte, ByRef Response As Byte) As Integer
	Declare Function ICC_Reader_PowerOn Lib "SSSE32.dll" (ByVal ReaderHandle As Integer, ByVal ICC_Slot_No As Byte, ByRef Response As Byte) As Integer
	Declare Function ICC_Reader_PowerOff Lib "SSSE32.dll" (ByVal ReaderHandle As Integer, ByVal ICC_Slot_No As Byte) As Integer
	Declare Function ICC_Reader_Application Lib "SSSE32.dll" (ByVal ReaderHandle As Integer, ByVal ICC_Slot_No As Byte, ByVal Lenth_of_Command_APDU As Integer, ByRef Command_APDU As Byte, ByRef Response_APDU As Byte) As Integer
	
	'4442/4428
	Declare Function ICC_Reader_4442_PowerOn Lib "SSSE32.dll" (ByVal ReaderHandle As Integer, ByRef data As Byte) As Integer
	Declare Function ICC_Reader_4442_PowerOff Lib "SSSE32.dll" (ByVal ReaderHandle As Integer) As Integer
	Declare Function ICC_Reader_4442_Read Lib "SSSE32.dll" (ByVal ReaderHandle As Integer, ByVal offset As Short, ByVal datalen As Short, ByRef data As Byte) As Integer
	Declare Function ICC_Reader_4442_Write Lib "SSSE32.dll" (ByVal ReaderHandle As Integer, ByVal offset As Short, ByVal datalen As Short, ByRef data As Byte) As Integer
	Declare Function ICC_Reader_4442_Verify Lib "SSSE32.dll" (ByVal ReaderHandle As Integer, ByRef key As Byte) As Integer
	Declare Function ICC_Reader_4442_Change Lib "SSSE32.dll" (ByVal ReaderHandle As Integer, ByRef newkey As Byte) As Integer
	Declare Function ICC_Reader_4442_ReadProtect Lib "SSSE32.dll" (ByVal ReaderHandle As Integer, ByVal offset As Short, ByVal datalen As Short, ByRef data As Byte) As Integer
	Declare Function ICC_Reader_4442_WriteProtect Lib "SSSE32.dll" (ByVal ReaderHandle As Integer, ByVal offset As Short, ByVal datalen As Short, ByRef data As Byte) As Integer
	Declare Function ICC_Reader_4442_ReadCount Lib "SSSE32.dll" (ByVal ReaderHandle As Integer, ByRef count As Integer) As Integer
	
	Declare Function ICC_Reader_4428_PowerOn Lib "SSSE32.dll" (ByVal ReaderHandle As Integer, ByRef data As Byte) As Integer
	Declare Function ICC_Reader_4428_PowerOff Lib "SSSE32.dll" (ByVal ReaderHandle As Integer) As Integer
	Declare Function ICC_Reader_4428_Read Lib "SSSE32.dll" (ByVal ReaderHandle As Integer, ByVal offset As Short, ByVal datalen As Short, ByRef data As Byte) As Integer
	Declare Function ICC_Reader_4428_Write Lib "SSSE32.dll" (ByVal ReaderHandle As Integer, ByVal offset As Short, ByVal datalen As Short, ByRef data As Byte) As Integer
	Declare Function ICC_Reader_4428_Verify Lib "SSSE32.dll" (ByVal ReaderHandle As Integer, ByRef key As Byte) As Integer
	Declare Function ICC_Reader_4428_Change Lib "SSSE32.dll" (ByVal ReaderHandle As Integer, ByRef newkey As Byte) As Integer
	Declare Function ICC_Reader_4428_ReadProtect Lib "SSSE32.dll" (ByVal ReaderHandle As Integer, ByVal offset As Short, ByVal datalen As Short, ByRef data As Byte) As Integer
	Declare Function ICC_Reader_4428_WriteProtect Lib "SSSE32.dll" (ByVal ReaderHandle As Integer, ByVal offset As Short, ByVal datalen As Short, ByRef data As Byte) As Integer
	Declare Function ICC_Reader_4428_ReadCount Lib "SSSE32.dll" (ByVal ReaderHandle As Integer, ByRef count As Integer) As Integer
	
	'TypeA/B
	Declare Function PICC_Reader_SetTypeA Lib "SSSE32.dll" (ByVal ReaderHandle As Integer) As Integer
	Declare Function PICC_Reader_SetTypeB Lib "SSSE32.dll" (ByVal ReaderHandle As Integer) As Integer
	Declare Function PICC_Reader_PowerOnTypeA Lib "SSSE32.dll" (ByVal ReaderHandle As Integer, ByRef Response As Byte) As Integer '上电 返回数据长度 失败小于0
	Declare Function PICC_Reader_PowerOnTypeB Lib "SSSE32.dll" (ByVal ReaderHandle As Integer, ByRef Response As Byte) As Integer '上电 返回数据长度 失败小于0
	Declare Function PICC_Reader_Application Lib "SSSE32.dll" (ByVal ReaderHandle As Integer, ByVal Lenth_of_Command_APDU As Integer, ByRef Command_APDU As Byte, ByRef Response_APDU As Byte) As Integer 'type a/b执行apdu命令 返回数据长度 失败小于0
End Module