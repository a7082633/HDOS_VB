Option Strict Off
Option Explicit On
Friend Class Form1
	Inherits System.Windows.Forms.Form
	Dim reader As Integer
	
	Private Sub Command1_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Command1.Click
		
		reader = ICC_Reader_Open("USB1")
		
		If reader < 0 Then
			List1.Items.Add(("打开失败"))
		Else
			List1.Items.Add(("打开成功!"))
		End If
		
		
	End Sub
	
	Private Sub Command2_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Command2.Click
		List1.Items.Clear()
		
		
	End Sub
	
	Private Sub Command3_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Command3.Click
		Dim i As Integer
		Dim uid As New VB6.FixedLengthString(12)
		Dim data(16) As Byte
		Dim cardtype As Byte
		cardtype = 65
		
		i = PICC_Reader_Request(reader)
		If i = 0 Then
			List1.Items.Add(("请求卡片成功!"))
		Else
			List1.Items.Add(("请求卡片失败"))
			Exit Sub
		End If
		
		i = PICC_Reader_anticoll(reader, uid.Value)
		If i = 0 Then
			List1.Items.Add(("放碰撞卡片成功!"))
		Else
			List1.Items.Add(("防碰撞卡片失败"))
			Exit Sub
		End If
		
		i = PICC_Reader_Select(reader, &H41)
		If i = 0 Then
			List1.Items.Add(("选择卡片成功!"))
		Else
			List1.Items.Add(("选择卡片失败"))
			Exit Sub
		End If
		
		Dim hexkey(6) As Byte
		hexkey(0) = &HFF
		hexkey(1) = &HFF
		hexkey(2) = &HFF
		hexkey(3) = &HFF
		hexkey(4) = &HFF
		hexkey(5) = &HFF
		
		i = PICC_Reader_LoadKey(reader, 0, 0, hexkey(0))
		If i = 0 Then
			List1.Items.Add(("下载密钥成功!"))
		Else
			List1.Items.Add(("下载密钥失败"))
			Exit Sub
		End If
		
		
		i = PICC_Reader_Authentication(reader, 0, 0)
		If i = 0 Then
			List1.Items.Add(("认证密钥成功!"))
		Else
			List1.Items.Add(("认证密钥失败"))
			Exit Sub
		End If
		
		i = PICC_Reader_Read(reader, 0, data(0))
		If i = 0 Then
			List1.Items.Add(("读卡成功!"))
		Else
			List1.Items.Add(("读卡失败"))
			Exit Sub
		End If
		
		Dim hexdata(16) As Byte
		hexdata(0) = &H11
		hexdata(1) = &H22
		hexdata(2) = &HFF
		hexdata(3) = &HFF
		hexdata(4) = &HFF
		hexdata(5) = &HFF
		hexdata(6) = &HFF
		hexdata(7) = &HFF
		hexdata(8) = &HFF
		hexdata(9) = &HFF
		hexdata(10) = &HFF
		hexdata(11) = &HFF
		hexdata(12) = &HFF
		hexdata(13) = &HFF
		hexdata(14) = &HFF
		hexdata(15) = &HFF
		
		i = PICC_Reader_Write(reader, 1, hexdata(0))
		If i = 0 Then
			List1.Items.Add(("写卡成功!"))
		Else
			List1.Items.Add(("写卡失败"))
			Exit Sub
		End If
		
		
		
	End Sub
	
	Private Sub Command4_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Command4.Click
		Dim i As Integer
		Dim ICC_Slot_No As Byte
		Dim cpuetu As Byte
		
		ICC_Slot_No = &H1
		cpuetu = &H1
		i = ICC_SetCpupara(reader, ICC_Slot_No, 0, cpuetu)
		If i = 0 Then
			List1.Items.Add(("设置速率成功!"))
		Else
			List1.Items.Add(("设置速率失败"))
			Exit Sub
		End If
		
		
		
		
		Dim Response(256) As Byte
		i = ICC_Reader_pre_PowerOn(reader, ICC_Slot_No, Response(0)) '函数执行成功返回数据长度
		If i > 0 Then
			List1.Items.Add(("冷复位成功!"))
		Else
			List1.Items.Add(("冷复位失败"))
			Exit Sub
		End If
		
		i = ICC_Reader_PowerOn(reader, ICC_Slot_No, Response(0)) '函数执行成功返回数据长度
		If i > 0 Then
			List1.Items.Add(("热复位成功!"))
		Else
			List1.Items.Add(("热复位失败"))
			Exit Sub
		End If
		
		'UPGRADE_NOTE: command 已升级到 command_Renamed。 单击以获得更多信息:“ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"”
		Dim command_Renamed(256) As Byte
		command_Renamed(0) = &H0
		command_Renamed(1) = &H84
		command_Renamed(2) = &H0
		command_Renamed(3) = &H0
		command_Renamed(4) = &H8
		
		i = ICC_Reader_Application(reader, ICC_Slot_No, 5, command_Renamed(0), Response(0)) '函数执行成功返回数据长度
		If i > 0 Then
			List1.Items.Add(("命令执行成功!"))
		Else
			List1.Items.Add(("命令执行失败"))
			Exit Sub
		End If
		
		i = ICC_Reader_PowerOff(reader, ICC_Slot_No)
		If i = 0 Then
			List1.Items.Add(("下电成功!"))
		Else
			List1.Items.Add(("下电失败"))
			Exit Sub
		End If
		
	End Sub
	
	Private Sub Command5_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Command5.Click
		Dim i As Integer
		Dim data(256) As Byte
		
		i = ICC_Reader_4442_PowerOn(reader, data(0))
		If i = 0 And data(0) <> &HFF Then
			List1.Items.Add(("4442上电成功!"))
		Else
			List1.Items.Add(("4442上电失败"))
			Exit Sub
		End If
		
		Dim offset As Integer
		Dim datalen As Integer
		offset = 0
		datalen = 255
		
		i = ICC_Reader_4442_Read(reader, offset, datalen, data(0))
		If i = 0 Then
			List1.Items.Add(("4442读数据成功!"))
		Else
			List1.Items.Add(("4442读数据失败"))
			Exit Sub
		End If
		
		Dim count As Integer
		i = ICC_Reader_4442_ReadCount(reader, count)
		If i = 0 Then
			List1.Items.Add(("4442读密钥剩余认证次数成功!"))
		Else
			List1.Items.Add(("4442读密钥剩余认证次数失败"))
			Exit Sub
		End If
		
		'Dim sz As String * 20
		
		If count = 3 Then
			List1.Items.Add(("剩余3次认证机会"))
		Else
			If count = 2 Then
				List1.Items.Add(("剩余2次认证机会"))
			Else
				If count = 1 Then
					List1.Items.Add(("剩余1次认证机会"))
				Else
					If count = 0 Then
						List1.Items.Add(("卡片被锁"))
					End If
				End If
			End If
		End If
		
		Dim key(3) As Byte
		key(0) = &HFF
		key(1) = &HFF
		key(2) = &HFF
		
		
		i = ICC_Reader_4442_Verify(reader, key(0))
		If i = 0 Then
			List1.Items.Add(("4442密钥认证成功!"))
		Else
			List1.Items.Add(("4442密钥认证失败"))
			Exit Sub
		End If
		
		i = ICC_Reader_4442_ReadCount(reader, count)
		If i = 0 Then
			List1.Items.Add(("4442读密钥剩余认证次数成功!"))
		Else
			List1.Items.Add(("4442读密钥剩余认证次数失败"))
			Exit Sub
		End If
		
		'Dim sz As String * 20
		
		If count = 3 Then
			List1.Items.Add(("剩余3次认证机会"))
		Else
			If count = 2 Then
				List1.Items.Add(("剩余2次认证机会"))
			Else
				If count = 1 Then
					List1.Items.Add(("剩余1次认证机会"))
				Else
					If count = 0 Then
						List1.Items.Add(("卡片被锁"))
					End If
				End If
			End If
		End If
		
		Dim wdata(256) As Byte
		wdata(0) = &H11
		wdata(1) = &H22
		wdata(2) = &H33
		wdata(3) = &H44
		offset = 4
		datalen = 4
		i = ICC_Reader_4442_Write(reader, offset, datalen, wdata(0))
		If i = 0 Then
			List1.Items.Add(("4442写数据成功!"))
		Else
			List1.Items.Add(("4442数写据失败"))
			Exit Sub
		End If
		
		Dim newkey(6) As Byte
		newkey(0) = &HFF
		newkey(1) = &HFF
		newkey(2) = &HFF
		
		i = ICC_Reader_4442_Change(reader, newkey(0))
		If i = 0 Then
			List1.Items.Add(("4442修改密钥成功!"))
		Else
			List1.Items.Add(("4442修改密钥失败"))
			Exit Sub
		End If
		
	End Sub
	
	
	Private Sub Command6_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Command6.Click
		Dim i As Integer
		Dim data(256) As Byte
		
		i = ICC_Reader_4428_PowerOn(reader, data(0))
		If i = 0 And data(0) <> &HFF Then
			List1.Items.Add(("4428上电成功!"))
		Else
			List1.Items.Add(("4428上电失败"))
			Exit Sub
		End If
		
		Dim offset As Integer
		Dim datalen As Integer
		offset = 0
		datalen = 255
		
		i = ICC_Reader_4428_Read(reader, offset, datalen, data(0))
		If i = 0 Then
			List1.Items.Add(("4428读数据成功!"))
		Else
			List1.Items.Add(("4428读数据失败"))
			Exit Sub
		End If
		
		Dim count As Integer
		i = ICC_Reader_4428_ReadCount(reader, count)
		If i = 0 Then
			List1.Items.Add(("4428读密钥剩余认证次数成功!"))
		Else
			List1.Items.Add(("4428读密钥剩余认证次数失败"))
			Exit Sub
		End If
		
		'Dim sz As String * 20
		
		If count = 8 Then
			List1.Items.Add(("剩余8次认证机会"))
		Else
			If count = 7 Then
				List1.Items.Add(("剩余7次认证机会"))
			Else
				If count = 6 Then
					List1.Items.Add(("剩余6次认证机会"))
				Else
					If count = 5 Then
						List1.Items.Add(("剩余5次认证机会"))
					Else
						If count = 4 Then
							List1.Items.Add(("剩余4次认证机会"))
						Else
							If count = 3 Then
								List1.Items.Add(("剩余3次认证机会"))
							Else
								If count = 2 Then
									List1.Items.Add(("剩余2次认证机会"))
								Else
									If count = 1 Then
										List1.Items.Add(("剩余1次认证机会"))
									Else
										If count = 0 Then
											List1.Items.Add(("卡片被锁"))
										End If
									End If
								End If
							End If
						End If
					End If
				End If
			End If
		End If
		Dim key(3) As Byte
		key(0) = &HFF
		key(1) = &HFF
		
		
		
		i = ICC_Reader_4428_Verify(reader, key(0))
		If i = 0 Then
			List1.Items.Add(("4428密钥认证成功!"))
		Else
			List1.Items.Add(("4428密钥认证失败"))
			Exit Sub
		End If
		
		i = ICC_Reader_4428_ReadCount(reader, count)
		If i = 0 Then
			List1.Items.Add(("4428读密钥剩余认证次数成功!"))
		Else
			List1.Items.Add(("44282读密钥剩余认证次数失败"))
			Exit Sub
		End If
		
		'Dim sz As String * 20
		
		If count = 8 Then
			List1.Items.Add(("剩余8次认证机会"))
		Else
			If count = 7 Then
				List1.Items.Add(("剩余7次认证机会"))
			Else
				If count = 6 Then
					List1.Items.Add(("剩余6次认证机会"))
				Else
					If count = 5 Then
						List1.Items.Add(("剩余5次认证机会"))
					Else
						If count = 4 Then
							List1.Items.Add(("剩余4次认证机会"))
						Else
							If count = 3 Then
								List1.Items.Add(("剩余3次认证机会"))
							Else
								If count = 2 Then
									List1.Items.Add(("剩余2次认证机会"))
								Else
									If count = 1 Then
										List1.Items.Add(("剩余1次认证机会"))
									Else
										If count = 0 Then
											List1.Items.Add(("卡片被锁"))
										End If
									End If
								End If
							End If
						End If
					End If
				End If
			End If
		End If
		
		Dim wdata(256) As Byte
		wdata(0) = &H11
		wdata(1) = &H22
		wdata(2) = &H33
		wdata(3) = &H44
		offset = 4
		datalen = 4
		i = ICC_Reader_4428_Write(reader, offset, datalen, wdata(0))
		If i = 0 Then
			List1.Items.Add(("4428写数据成功!"))
		Else
			List1.Items.Add(("4428数写据失败"))
			Exit Sub
		End If
		
		Dim newkey(6) As Byte
		newkey(0) = &HFF
		newkey(1) = &HFF
		
		
		i = ICC_Reader_4428_Change(reader, newkey(0))
		If i = 0 Then
			List1.Items.Add(("4428修改密钥成功!"))
		Else
			List1.Items.Add(("4428修改密钥失败"))
			Exit Sub
		End If
	End Sub
	
	Private Sub Command7_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Command7.Click
		Dim i As Integer
		
		i = PICC_Reader_SetTypeA(reader)
		If i = 0 Then
			List1.Items.Add(("设置为TypeA成功!"))
		Else
			List1.Items.Add(("设置为TypeA失败"))
			Exit Sub
		End If
		
		i = PICC_Reader_Request(reader)
		If i = 0 Then
			List1.Items.Add(("请求TypeA成功!"))
		Else
			List1.Items.Add(("请求TypeA失败"))
			Exit Sub
		End If
		
		
		Dim uid(6) As Byte
		
		i = PICC_Reader_anticoll(reader, CStr(uid(0)))
		If i = 0 Then
			List1.Items.Add(("防碰撞TypeA成功!"))
		Else
			List1.Items.Add(("防碰撞TypeA失败"))
			Exit Sub
		End If
		
		i = PICC_Reader_Select(reader, &H41)
		If i = 0 Then
			List1.Items.Add(("选择卡片成功!"))
		Else
			List1.Items.Add(("选择卡片失败"))
			Exit Sub
		End If
		
		
		Dim Response(256) As Byte
		i = PICC_Reader_PowerOnTypeA(reader, Response(0)) '函数执行成功返回数据长度
		If i > 0 Then
			List1.Items.Add(("TypeA上电成功!"))
		Else
			List1.Items.Add(("TypeA上电失败"))
			Exit Sub
		End If
		
		'UPGRADE_NOTE: command 已升级到 command_Renamed。 单击以获得更多信息:“ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"”
		Dim command_Renamed(256) As Byte
		command_Renamed(0) = &H0
		command_Renamed(1) = &H84
		command_Renamed(2) = &H0
		command_Renamed(3) = &H0
		command_Renamed(4) = &H8
		
		i = PICC_Reader_Application(reader, 5, command_Renamed(0), Response(0)) '函数执行成功返回数据长度
		If i > 0 Then
			List1.Items.Add(("命令执行成功!"))
		Else
			List1.Items.Add(("命令执行失败"))
			Exit Sub
		End If
		
	End Sub
	
	
	Private Sub Command8_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Command8.Click
		Dim i As Integer
		
		i = PICC_Reader_SetTypeB(reader)
		If i = 0 Then
			List1.Items.Add(("设置为TypeB成功!"))
		Else
			List1.Items.Add(("设置为TypeB失败"))
			Exit Sub
		End If
		
		
		
		Dim Response(256) As Byte
		i = PICC_Reader_PowerOnTypeB(reader, Response(0)) '函数执行成功返回数据长度
		If i > 0 Then
			List1.Items.Add(("TypeB上电成功!"))
		Else
			List1.Items.Add(("TypeB上电失败"))
			Exit Sub
		End If
		
		i = PICC_Reader_Select(reader, &H42)
		If i = 0 Then
			List1.Items.Add(("选择卡片成功!"))
		Else
			List1.Items.Add(("选择卡片失败"))
			Exit Sub
		End If
		
		'UPGRADE_NOTE: command 已升级到 command_Renamed。 单击以获得更多信息:“ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"”
		Dim command_Renamed(256) As Byte
		command_Renamed(0) = &H0
		command_Renamed(1) = &H84
		command_Renamed(2) = &H0
		command_Renamed(3) = &H0
		command_Renamed(4) = &H8
		
		i = PICC_Reader_Application(reader, 5, command_Renamed(0), Response(0)) '函数执行成功返回数据长度
		If i > 0 Then
			List1.Items.Add(("命令执行成功!"))
		Else
			List1.Items.Add(("命令执行失败"))
			Exit Sub
		End If
		
	End Sub
End Class