Option Strict Off
Option Explicit On
Friend Class Form1
	Inherits System.Windows.Forms.Form
	Dim reader As Integer
	
	Private Sub Command1_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Command1.Click
		
		reader = ICC_Reader_Open("USB1")
		
		If reader < 0 Then
			List1.Items.Add(("��ʧ��"))
		Else
			List1.Items.Add(("�򿪳ɹ�!"))
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
			List1.Items.Add(("����Ƭ�ɹ�!"))
		Else
			List1.Items.Add(("����Ƭʧ��"))
			Exit Sub
		End If
		
		i = PICC_Reader_anticoll(reader, uid.Value)
		If i = 0 Then
			List1.Items.Add(("����ײ��Ƭ�ɹ�!"))
		Else
			List1.Items.Add(("����ײ��Ƭʧ��"))
			Exit Sub
		End If
		
		i = PICC_Reader_Select(reader, &H41)
		If i = 0 Then
			List1.Items.Add(("ѡ��Ƭ�ɹ�!"))
		Else
			List1.Items.Add(("ѡ��Ƭʧ��"))
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
			List1.Items.Add(("������Կ�ɹ�!"))
		Else
			List1.Items.Add(("������Կʧ��"))
			Exit Sub
		End If
		
		
		i = PICC_Reader_Authentication(reader, 0, 0)
		If i = 0 Then
			List1.Items.Add(("��֤��Կ�ɹ�!"))
		Else
			List1.Items.Add(("��֤��Կʧ��"))
			Exit Sub
		End If
		
		i = PICC_Reader_Read(reader, 0, data(0))
		If i = 0 Then
			List1.Items.Add(("�����ɹ�!"))
		Else
			List1.Items.Add(("����ʧ��"))
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
			List1.Items.Add(("д���ɹ�!"))
		Else
			List1.Items.Add(("д��ʧ��"))
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
			List1.Items.Add(("�������ʳɹ�!"))
		Else
			List1.Items.Add(("��������ʧ��"))
			Exit Sub
		End If
		
		
		
		
		Dim Response(256) As Byte
		i = ICC_Reader_pre_PowerOn(reader, ICC_Slot_No, Response(0)) '����ִ�гɹ��������ݳ���
		If i > 0 Then
			List1.Items.Add(("�临λ�ɹ�!"))
		Else
			List1.Items.Add(("�临λʧ��"))
			Exit Sub
		End If
		
		i = ICC_Reader_PowerOn(reader, ICC_Slot_No, Response(0)) '����ִ�гɹ��������ݳ���
		If i > 0 Then
			List1.Items.Add(("�ȸ�λ�ɹ�!"))
		Else
			List1.Items.Add(("�ȸ�λʧ��"))
			Exit Sub
		End If
		
		'UPGRADE_NOTE: command �������� command_Renamed�� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"��
		Dim command_Renamed(256) As Byte
		command_Renamed(0) = &H0
		command_Renamed(1) = &H84
		command_Renamed(2) = &H0
		command_Renamed(3) = &H0
		command_Renamed(4) = &H8
		
		i = ICC_Reader_Application(reader, ICC_Slot_No, 5, command_Renamed(0), Response(0)) '����ִ�гɹ��������ݳ���
		If i > 0 Then
			List1.Items.Add(("����ִ�гɹ�!"))
		Else
			List1.Items.Add(("����ִ��ʧ��"))
			Exit Sub
		End If
		
		i = ICC_Reader_PowerOff(reader, ICC_Slot_No)
		If i = 0 Then
			List1.Items.Add(("�µ�ɹ�!"))
		Else
			List1.Items.Add(("�µ�ʧ��"))
			Exit Sub
		End If
		
	End Sub
	
	Private Sub Command5_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Command5.Click
		Dim i As Integer
		Dim data(256) As Byte
		
		i = ICC_Reader_4442_PowerOn(reader, data(0))
		If i = 0 And data(0) <> &HFF Then
			List1.Items.Add(("4442�ϵ�ɹ�!"))
		Else
			List1.Items.Add(("4442�ϵ�ʧ��"))
			Exit Sub
		End If
		
		Dim offset As Integer
		Dim datalen As Integer
		offset = 0
		datalen = 255
		
		i = ICC_Reader_4442_Read(reader, offset, datalen, data(0))
		If i = 0 Then
			List1.Items.Add(("4442�����ݳɹ�!"))
		Else
			List1.Items.Add(("4442������ʧ��"))
			Exit Sub
		End If
		
		Dim count As Integer
		i = ICC_Reader_4442_ReadCount(reader, count)
		If i = 0 Then
			List1.Items.Add(("4442����Կʣ����֤�����ɹ�!"))
		Else
			List1.Items.Add(("4442����Կʣ����֤����ʧ��"))
			Exit Sub
		End If
		
		'Dim sz As String * 20
		
		If count = 3 Then
			List1.Items.Add(("ʣ��3����֤����"))
		Else
			If count = 2 Then
				List1.Items.Add(("ʣ��2����֤����"))
			Else
				If count = 1 Then
					List1.Items.Add(("ʣ��1����֤����"))
				Else
					If count = 0 Then
						List1.Items.Add(("��Ƭ����"))
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
			List1.Items.Add(("4442��Կ��֤�ɹ�!"))
		Else
			List1.Items.Add(("4442��Կ��֤ʧ��"))
			Exit Sub
		End If
		
		i = ICC_Reader_4442_ReadCount(reader, count)
		If i = 0 Then
			List1.Items.Add(("4442����Կʣ����֤�����ɹ�!"))
		Else
			List1.Items.Add(("4442����Կʣ����֤����ʧ��"))
			Exit Sub
		End If
		
		'Dim sz As String * 20
		
		If count = 3 Then
			List1.Items.Add(("ʣ��3����֤����"))
		Else
			If count = 2 Then
				List1.Items.Add(("ʣ��2����֤����"))
			Else
				If count = 1 Then
					List1.Items.Add(("ʣ��1����֤����"))
				Else
					If count = 0 Then
						List1.Items.Add(("��Ƭ����"))
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
			List1.Items.Add(("4442д���ݳɹ�!"))
		Else
			List1.Items.Add(("4442��д��ʧ��"))
			Exit Sub
		End If
		
		Dim newkey(6) As Byte
		newkey(0) = &HFF
		newkey(1) = &HFF
		newkey(2) = &HFF
		
		i = ICC_Reader_4442_Change(reader, newkey(0))
		If i = 0 Then
			List1.Items.Add(("4442�޸���Կ�ɹ�!"))
		Else
			List1.Items.Add(("4442�޸���Կʧ��"))
			Exit Sub
		End If
		
	End Sub
	
	
	Private Sub Command6_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Command6.Click
		Dim i As Integer
		Dim data(256) As Byte
		
		i = ICC_Reader_4428_PowerOn(reader, data(0))
		If i = 0 And data(0) <> &HFF Then
			List1.Items.Add(("4428�ϵ�ɹ�!"))
		Else
			List1.Items.Add(("4428�ϵ�ʧ��"))
			Exit Sub
		End If
		
		Dim offset As Integer
		Dim datalen As Integer
		offset = 0
		datalen = 255
		
		i = ICC_Reader_4428_Read(reader, offset, datalen, data(0))
		If i = 0 Then
			List1.Items.Add(("4428�����ݳɹ�!"))
		Else
			List1.Items.Add(("4428������ʧ��"))
			Exit Sub
		End If
		
		Dim count As Integer
		i = ICC_Reader_4428_ReadCount(reader, count)
		If i = 0 Then
			List1.Items.Add(("4428����Կʣ����֤�����ɹ�!"))
		Else
			List1.Items.Add(("4428����Կʣ����֤����ʧ��"))
			Exit Sub
		End If
		
		'Dim sz As String * 20
		
		If count = 8 Then
			List1.Items.Add(("ʣ��8����֤����"))
		Else
			If count = 7 Then
				List1.Items.Add(("ʣ��7����֤����"))
			Else
				If count = 6 Then
					List1.Items.Add(("ʣ��6����֤����"))
				Else
					If count = 5 Then
						List1.Items.Add(("ʣ��5����֤����"))
					Else
						If count = 4 Then
							List1.Items.Add(("ʣ��4����֤����"))
						Else
							If count = 3 Then
								List1.Items.Add(("ʣ��3����֤����"))
							Else
								If count = 2 Then
									List1.Items.Add(("ʣ��2����֤����"))
								Else
									If count = 1 Then
										List1.Items.Add(("ʣ��1����֤����"))
									Else
										If count = 0 Then
											List1.Items.Add(("��Ƭ����"))
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
			List1.Items.Add(("4428��Կ��֤�ɹ�!"))
		Else
			List1.Items.Add(("4428��Կ��֤ʧ��"))
			Exit Sub
		End If
		
		i = ICC_Reader_4428_ReadCount(reader, count)
		If i = 0 Then
			List1.Items.Add(("4428����Կʣ����֤�����ɹ�!"))
		Else
			List1.Items.Add(("44282����Կʣ����֤����ʧ��"))
			Exit Sub
		End If
		
		'Dim sz As String * 20
		
		If count = 8 Then
			List1.Items.Add(("ʣ��8����֤����"))
		Else
			If count = 7 Then
				List1.Items.Add(("ʣ��7����֤����"))
			Else
				If count = 6 Then
					List1.Items.Add(("ʣ��6����֤����"))
				Else
					If count = 5 Then
						List1.Items.Add(("ʣ��5����֤����"))
					Else
						If count = 4 Then
							List1.Items.Add(("ʣ��4����֤����"))
						Else
							If count = 3 Then
								List1.Items.Add(("ʣ��3����֤����"))
							Else
								If count = 2 Then
									List1.Items.Add(("ʣ��2����֤����"))
								Else
									If count = 1 Then
										List1.Items.Add(("ʣ��1����֤����"))
									Else
										If count = 0 Then
											List1.Items.Add(("��Ƭ����"))
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
			List1.Items.Add(("4428д���ݳɹ�!"))
		Else
			List1.Items.Add(("4428��д��ʧ��"))
			Exit Sub
		End If
		
		Dim newkey(6) As Byte
		newkey(0) = &HFF
		newkey(1) = &HFF
		
		
		i = ICC_Reader_4428_Change(reader, newkey(0))
		If i = 0 Then
			List1.Items.Add(("4428�޸���Կ�ɹ�!"))
		Else
			List1.Items.Add(("4428�޸���Կʧ��"))
			Exit Sub
		End If
	End Sub
	
	Private Sub Command7_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Command7.Click
		Dim i As Integer
		
		i = PICC_Reader_SetTypeA(reader)
		If i = 0 Then
			List1.Items.Add(("����ΪTypeA�ɹ�!"))
		Else
			List1.Items.Add(("����ΪTypeAʧ��"))
			Exit Sub
		End If
		
		i = PICC_Reader_Request(reader)
		If i = 0 Then
			List1.Items.Add(("����TypeA�ɹ�!"))
		Else
			List1.Items.Add(("����TypeAʧ��"))
			Exit Sub
		End If
		
		
		Dim uid(6) As Byte
		
		i = PICC_Reader_anticoll(reader, CStr(uid(0)))
		If i = 0 Then
			List1.Items.Add(("����ײTypeA�ɹ�!"))
		Else
			List1.Items.Add(("����ײTypeAʧ��"))
			Exit Sub
		End If
		
		i = PICC_Reader_Select(reader, &H41)
		If i = 0 Then
			List1.Items.Add(("ѡ��Ƭ�ɹ�!"))
		Else
			List1.Items.Add(("ѡ��Ƭʧ��"))
			Exit Sub
		End If
		
		
		Dim Response(256) As Byte
		i = PICC_Reader_PowerOnTypeA(reader, Response(0)) '����ִ�гɹ��������ݳ���
		If i > 0 Then
			List1.Items.Add(("TypeA�ϵ�ɹ�!"))
		Else
			List1.Items.Add(("TypeA�ϵ�ʧ��"))
			Exit Sub
		End If
		
		'UPGRADE_NOTE: command �������� command_Renamed�� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"��
		Dim command_Renamed(256) As Byte
		command_Renamed(0) = &H0
		command_Renamed(1) = &H84
		command_Renamed(2) = &H0
		command_Renamed(3) = &H0
		command_Renamed(4) = &H8
		
		i = PICC_Reader_Application(reader, 5, command_Renamed(0), Response(0)) '����ִ�гɹ��������ݳ���
		If i > 0 Then
			List1.Items.Add(("����ִ�гɹ�!"))
		Else
			List1.Items.Add(("����ִ��ʧ��"))
			Exit Sub
		End If
		
	End Sub
	
	
	Private Sub Command8_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Command8.Click
		Dim i As Integer
		
		i = PICC_Reader_SetTypeB(reader)
		If i = 0 Then
			List1.Items.Add(("����ΪTypeB�ɹ�!"))
		Else
			List1.Items.Add(("����ΪTypeBʧ��"))
			Exit Sub
		End If
		
		
		
		Dim Response(256) As Byte
		i = PICC_Reader_PowerOnTypeB(reader, Response(0)) '����ִ�гɹ��������ݳ���
		If i > 0 Then
			List1.Items.Add(("TypeB�ϵ�ɹ�!"))
		Else
			List1.Items.Add(("TypeB�ϵ�ʧ��"))
			Exit Sub
		End If
		
		i = PICC_Reader_Select(reader, &H42)
		If i = 0 Then
			List1.Items.Add(("ѡ��Ƭ�ɹ�!"))
		Else
			List1.Items.Add(("ѡ��Ƭʧ��"))
			Exit Sub
		End If
		
		'UPGRADE_NOTE: command �������� command_Renamed�� �����Ի�ø�����Ϣ:��ms-help://MS.VSCC.v90/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"��
		Dim command_Renamed(256) As Byte
		command_Renamed(0) = &H0
		command_Renamed(1) = &H84
		command_Renamed(2) = &H0
		command_Renamed(3) = &H0
		command_Renamed(4) = &H8
		
		i = PICC_Reader_Application(reader, 5, command_Renamed(0), Response(0)) '����ִ�гɹ��������ݳ���
		If i > 0 Then
			List1.Items.Add(("����ִ�гɹ�!"))
		Else
			List1.Items.Add(("����ִ��ʧ��"))
			Exit Sub
		End If
		
	End Sub
End Class