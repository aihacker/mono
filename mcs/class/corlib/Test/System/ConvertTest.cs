// TestConvert.cs - NUnit Test Cases for System.Convert class
//
// Krister Hansson (ds99krha@thn.htu.se)
// Andreas Jonsson (ds99anjn@thn.htu.se)
// 
// (C) Krister Hansson & Andreas Jonsson
// 


using NUnit.Framework;
using System;
using System.Globalization;

namespace MonoTests.System
{
	public class ConvertTest : TestCase
	{
		public ConvertTest() {}

		bool boolTrue;
		bool boolFalse;
		byte tryByte;
		char tryChar;
		DateTime tryDT;
		decimal tryDec;
		double tryDbl;
		short tryInt16;
		int tryInt32;
		long tryInt64;
		object tryObj;
		sbyte trySByte;
		float tryFloat;
		string falseString;
		string trueString;
		string nullString;
		string tryStr;
		ushort tryUI16;
		uint tryUI32;
		ulong tryUI64;
		CultureInfo ci;
		
		protected override void SetUp() {
			boolTrue = true;
			boolFalse = false;
			tryByte = 0;
			tryChar = 'a';
			tryDT = new DateTime(2002,1,1);
			tryDec = 1234.2345m;
			tryDbl = 0;
			tryInt16 = 1234;
			tryInt32 = 12345;
			tryInt64 = 123456789012;
			tryObj = new Object();
			trySByte = 123;
			tryFloat = 1234.2345f;
			falseString = "false";
			trueString = "true";
			nullString = "null";
			tryStr = "foobar";
			tryUI16 = 34567;
			tryUI32 = 567891234;
			tryUI64 = 0;
			ci = new CultureInfo("en-US");
			ci.NumberFormat.NumberDecimalDigits = 3;
		}
		protected override void TearDown() {}

		public void TestChangeType() {
			int iTest = 1;
			try {
				AssertEquals("#A01", (short)12345, Convert.ChangeType(tryInt32, typeof(short)));
				iTest++;
				AssertEquals("#A02", 'A', Convert.ChangeType(65, typeof(char)));
				iTest++;
				AssertEquals("#A03", 66, Convert.ChangeType('B', typeof(int)));
				iTest++;
				AssertEquals("#A04", ((ulong)12345), Convert.ChangeType(tryInt32, typeof(ulong)));
				
				iTest++;
				AssertEquals("#A05", true, Convert.ChangeType(tryDec, TypeCode.Boolean));
				iTest++;
				AssertEquals("#A06", 'f', Convert.ChangeType("f", TypeCode.Char));
				iTest++;
				AssertEquals("#A07", (decimal)123456789012, Convert.ChangeType(tryInt64, TypeCode.Decimal));
				iTest++;
				AssertEquals("#A08", (int)34567, Convert.ChangeType(tryUI16, TypeCode.Int32));

				iTest++;
				AssertEquals("#A09", (double)567891234, Convert.ChangeType(tryUI32, typeof(double), ci));
				iTest++;
				AssertEquals("#A10", (ushort)0, Convert.ChangeType(tryByte, typeof(ushort), ci));
				iTest++;
				AssertEquals("#A11", (decimal)567891234, Convert.ChangeType(tryUI32, typeof(decimal), ci));
				iTest++;
				AssertEquals("#A12", (float)1234, Convert.ChangeType(tryInt16, typeof(float), ci));
				iTest++;
				AssertEquals("#A13", null, Convert.ChangeType(null, null, ci));

				iTest++;
				AssertEquals("#A14", (decimal)0, Convert.ChangeType(tryByte, TypeCode.Decimal, ci));
				iTest++;
				AssertEquals("#A15", "f", Convert.ChangeType('f', TypeCode.String, ci));
				iTest++;
				AssertEquals("#A16", 'D', Convert.ChangeType(68, TypeCode.Char, ci));
				iTest++;
				AssertEquals("#A17", (long)34567, Convert.ChangeType(tryUI16, TypeCode.Int64, ci));
				iTest++;
				AssertEquals("#A18", null, Convert.ChangeType(null, TypeCode.Empty, ci));
			} catch (Exception e) {
				Fail ("Unexpected exception at iTest = " + iTest + ": e = " + e);
			}
			
			try {
				Convert.ChangeType(boolTrue, typeof(char));
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#A25", typeof(InvalidCastException), e.GetType());
			}
			
			try {
				Convert.ChangeType(tryChar, typeof(DateTime));
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#A26", typeof(InvalidCastException), e.GetType());
			}

			try {
				Convert.ChangeType(ci, TypeCode.String);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#A27", typeof(InvalidCastException), e.GetType());
			}

			try {
				Convert.ChangeType(tryInt32, null);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#A28", typeof(ArgumentNullException), e.GetType());
			}

			try 
			{
				Convert.ChangeType(boolTrue, typeof(DateTime), ci);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#A29", typeof(InvalidCastException), e.GetType());
			}
			
			try {
				Convert.ChangeType(ci, typeof(DateTime), ci);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#A30", typeof(InvalidCastException), e.GetType());
			}

			/* Should throw ArgumentException but throws InvalidCastException
			try {
				Convert.ChangeType(tryUI32, typeof(FormatException), ci);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#A??", typeof(ArgumentException), e.GetType());
			}*/

			try {
				Convert.ChangeType(tryUI32, TypeCode.Byte, ci);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#A31", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ChangeType(boolTrue, TypeCode.Char, ci);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#A32", typeof(InvalidCastException), e.GetType());
			}

			try {
				Convert.ChangeType(boolTrue, null, ci);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#A33", typeof(ArgumentNullException), e.GetType());
			}		
		}		

		public void TestGetTypeCode() {
			int marker = 1;
			try {
				AssertEquals("#B01", TypeCode.String, Convert.GetTypeCode(tryStr));
				marker++;
				AssertEquals("#B02", TypeCode.UInt16, Convert.GetTypeCode(tryUI16));
				marker++;
				AssertEquals("#B03", TypeCode.UInt32, Convert.GetTypeCode(tryUI32));
				marker++;
				AssertEquals("#B04", TypeCode.UInt64, Convert.GetTypeCode(tryUI64));
				marker++;
				AssertEquals("#B05", TypeCode.Double, Convert.GetTypeCode(tryDbl));
				marker++;
				AssertEquals("#B06", TypeCode.Int16, Convert.GetTypeCode(tryInt16));
				marker++;
				AssertEquals("#B07", TypeCode.Int64, Convert.GetTypeCode(tryInt64));
				marker++;
				AssertEquals("#B08", TypeCode.Object, Convert.GetTypeCode(tryObj));
				marker++;
				AssertEquals("#B09", TypeCode.SByte, Convert.GetTypeCode(trySByte));
				marker++;
				AssertEquals("#B10", TypeCode.Single, Convert.GetTypeCode(tryFloat));
				marker++;
				AssertEquals("#B11", TypeCode.Byte, Convert.GetTypeCode(tryByte));
				marker++;
				AssertEquals("#B12", TypeCode.Char, Convert.GetTypeCode(tryChar));
				marker++;
//				AssertEquals("#B13", TypeCode.DateTime, Convert.GetTypeCode(tryDT));
				marker++;
				AssertEquals("#B14", TypeCode.Decimal, Convert.GetTypeCode(tryDec));
				marker++;
				AssertEquals("#B15", TypeCode.Int32, Convert.GetTypeCode(tryInt32));
				marker++;
				AssertEquals("#B16", TypeCode.Boolean, Convert.GetTypeCode(boolTrue));
			} catch (Exception e) {
				Fail ("Unexpected exception at " + marker + ": " + e);
			}
		}

		public void TestIsDBNull() {
			AssertEquals("#C01", false, Convert.IsDBNull(tryInt32));
			AssertEquals("#C02", true, Convert.IsDBNull(Convert.DBNull));
			AssertEquals("#C03", false, Convert.IsDBNull(boolTrue));
			AssertEquals("#C04", false, Convert.IsDBNull(tryChar));
			AssertEquals("#C05", false, Convert.IsDBNull(tryFloat));
		}
		
		public void TestToBoolean() {
			tryObj = (object)tryDbl;
			
			AssertEquals("#D01", true, Convert.ToBoolean(boolTrue));
			AssertEquals("#D02", false, Convert.ToBoolean(tryByte));
			AssertEquals("#D03", true, Convert.ToBoolean(tryDec));
			AssertEquals("#D04", false, Convert.ToBoolean(tryDbl));
			AssertEquals("#D05", true, Convert.ToBoolean(tryInt16));
			AssertEquals("#D06", true, Convert.ToBoolean(tryInt32));
			AssertEquals("#D07", true, Convert.ToBoolean(tryInt64));
			AssertEquals("#D08", false, Convert.ToBoolean(tryObj));
			AssertEquals("#D09", true, Convert.ToBoolean(trySByte));
			AssertEquals("#D10", true, Convert.ToBoolean(tryFloat));
			AssertEquals("#D11", true, Convert.ToBoolean(trueString));
			AssertEquals("#D12", false, Convert.ToBoolean(falseString));
			AssertEquals("#D13", true, Convert.ToBoolean(tryUI16));
			AssertEquals("#D14", true, Convert.ToBoolean(tryUI32));
			AssertEquals("#D15", false, Convert.ToBoolean(tryUI64));
			AssertEquals("#D16", false, Convert.ToBoolean(tryObj,ci));
			AssertEquals("#D17", true, Convert.ToBoolean(trueString, ci));
			AssertEquals("#D18", false, Convert.ToBoolean(falseString, ci));
			
			try {
				Convert.ToBoolean(tryChar);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#D20", typeof(InvalidCastException), e.GetType());
			}
			
			try {
				Convert.ToBoolean(tryDT);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#D21", typeof(InvalidCastException), e.GetType());
			}

			try {
				Convert.ToBoolean(tryStr);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#D22", typeof(FormatException), e.GetType());
			}

			try {
				Convert.ToBoolean(nullString);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#D23", typeof(FormatException), e.GetType());
			}
		}

		public void TestToByte() {
			
			AssertEquals("#E01", (byte)1, Convert.ToByte(boolTrue));
			AssertEquals("#E02", (byte)0, Convert.ToByte(boolFalse));
			AssertEquals("#E03", tryByte, Convert.ToByte(tryByte));
			AssertEquals("#E04", (byte)114, Convert.ToByte('r'));
			AssertEquals("#E05", (byte)201, Convert.ToByte((decimal)200.6));
			AssertEquals("#E06", (byte)125, Convert.ToByte((double)125.4));
			AssertEquals("#E07", (byte)255, Convert.ToByte((short)255));
			AssertEquals("#E08", (byte)254, Convert.ToByte((int)254));
			AssertEquals("#E09", (byte)34, Convert.ToByte((long)34));
			AssertEquals("#E10", (byte)1, Convert.ToByte((object)boolTrue));
			AssertEquals("#E11", (byte)123, Convert.ToByte((float)123.49f));
			AssertEquals("#E12", (byte)57, Convert.ToByte("57"));
			AssertEquals("#E13", (byte)75, Convert.ToByte((ushort)75));
			AssertEquals("#E14", (byte)184, Convert.ToByte((uint)184));
			AssertEquals("#E15", (byte)241, Convert.ToByte((ulong)241));
			AssertEquals("#E16", (byte)123, Convert.ToByte(trySByte, ci));
			AssertEquals("#E17", (byte)27, Convert.ToByte("011011", 2));
			AssertEquals("#E18", (byte)13, Convert.ToByte("15", 8));
			AssertEquals("#E19", (byte)27, Convert.ToByte("27", 10));
			AssertEquals("#E20", (byte)250, Convert.ToByte("FA", 16));

			try {
				Convert.ToByte('\u03A9'); // sign of Omega on Win2k
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#E25", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToByte(tryDT);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#D26", typeof(InvalidCastException), e.GetType());
			}

			try {
				Convert.ToByte((decimal)22000);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#E27", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToByte((double)255.5);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#E28", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToByte(-tryInt16);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#E29", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToByte((int)-256);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#E30", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToByte(tryInt64);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#E31", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToByte((object)ci);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#E32", typeof(InvalidCastException), e.GetType());
			}

			try {
				Convert.ToByte((sbyte)-1);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#E33", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToByte((float)-0.6f);		
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#E34", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToByte("1a1");		
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#E35", typeof(FormatException), e.GetType());
			}

			try {
				Convert.ToByte("457");		
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#E36", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToByte((ushort)30000);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#E37", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToByte((uint)300);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#E38", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToByte((ulong)987654321321);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#E39", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToByte("10010111", 3);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#E40", typeof(ArgumentException), e.GetType());
			}

			try {
				Convert.ToByte("3F3", 16);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#E41", typeof(OverflowException), e.GetType());
			}
		}

		public void TestToChar(){
			tryByte = 58;
			AssertEquals("#F01", ':', Convert.ToChar(tryByte));
			AssertEquals("#F02", 'a', Convert.ToChar(tryChar));
			AssertEquals("#F03", 'A', Convert.ToChar((short)65));
			AssertEquals("#F04", 'x', Convert.ToChar((int)120));
			AssertEquals("#F05", '"', Convert.ToChar((long)34));
			AssertEquals("#F06", '-', Convert.ToChar((sbyte)45));
			AssertEquals("#F07", '@', Convert.ToChar("@"));
			AssertEquals("#F08", 'K', Convert.ToChar((ushort)75));
			AssertEquals("#F09", '=', Convert.ToChar((uint)61));
			// AssertEquals("#F10", '�', Convert.ToChar((ulong)200));
			AssertEquals("#F11", '{', Convert.ToChar((object)trySByte, ci));
			AssertEquals("#F12", 'o', Convert.ToChar(tryStr.Substring(1,1), ci));
			
			try {
				Convert.ToChar(boolTrue);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#F20", typeof(InvalidCastException), e.GetType());
			}

			try {
				Convert.ToChar(tryDT);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#F21", typeof(InvalidCastException), e.GetType());
			}

			try {
				Convert.ToChar(tryDec);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#F22", typeof(InvalidCastException), e.GetType());
			}

			try {
				Convert.ToChar(tryDbl);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#F23", typeof(InvalidCastException), e.GetType());
			}

			try {
				Convert.ToChar((short)-1);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#F24", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToChar(Int32.MinValue);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#F25", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToChar(Int32.MaxValue);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#F26", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToChar(tryInt64);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#F27", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToChar((long)-123);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#F28", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToChar(ci);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#F29", typeof(InvalidCastException), e.GetType());
			}

			try {
				Convert.ToChar(-trySByte);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#F30", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToChar(tryFloat);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#F31", typeof(InvalidCastException), e.GetType());
			}

			try {
				Convert.ToChar("foo");
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#F32", typeof(FormatException), e.GetType());
			}
			
			try {
				Convert.ToChar(null);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#F33", typeof(ArgumentNullException), e.GetType());
			}

			try {
				Convert.ToChar(new Exception(), ci);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#F34", typeof(InvalidCastException), e.GetType());
			}

			try {
				Convert.ToChar(null, ci);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#F35", typeof(ArgumentNullException), e.GetType());
			}

			try {
				Convert.ToChar("", ci);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#F36", typeof(FormatException), e.GetType());
			}

			try {
				Convert.ToChar(tryStr, ci);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#F37", typeof(FormatException), e.GetType());
			}
		}

		[Ignore("this test hangs mono")]
		public void TestToDateTime() {
			string dateString = "01/01/2002";
			
			int iTest = 1;
			try {
				AssertEquals("#G01", tryDT, Convert.ToDateTime(tryDT));
				iTest++;
				AssertEquals("#G02", tryDT, Convert.ToDateTime(dateString));
				iTest++;
				AssertEquals("#G03", tryDT, Convert.ToDateTime(dateString, ci));
			} catch (Exception e) {
				Fail ("Unexpected exception at iTest = " + iTest + ": e = " + e);
			}

			try {
				Convert.ToDateTime(boolTrue);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#G10", typeof(InvalidCastException), e.GetType());
			}

			try {
				Convert.ToDateTime(tryByte);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#G11", typeof(InvalidCastException), e.GetType());
			}

			try {
				Convert.ToDateTime(tryChar);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#G12", typeof(InvalidCastException), e.GetType());
			}

			try {
				Convert.ToDateTime(tryDec);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#G13", typeof(InvalidCastException), e.GetType());
			}

			try {
				Convert.ToDateTime(tryDbl);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#G14", typeof(InvalidCastException), e.GetType());
			}

			try {
				Convert.ToDateTime(tryInt16);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#G15", typeof(InvalidCastException), e.GetType());
			}

			try {
				Convert.ToDateTime(tryInt32);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#G16", typeof(InvalidCastException), e.GetType());
			}

			try {
				Convert.ToDateTime(tryInt64);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#G17", typeof(InvalidCastException), e.GetType());
			}

			try {
				Convert.ToDateTime(ci);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#G18", typeof(InvalidCastException), e.GetType());
			}

			try {
				Convert.ToDateTime(trySByte);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#G19", typeof(InvalidCastException), e.GetType());
			}

			try {
				Convert.ToDateTime(tryFloat);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#G20", typeof(InvalidCastException), e.GetType());
			}

			try {
				Convert.ToDateTime("20a2-01-01");
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#G21", typeof(FormatException), e.GetType());
			}

			try {
				Convert.ToDateTime("20002-25-01");
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#G22", typeof(ArgumentOutOfRangeException), e.GetType());
			} 

			try {
				Convert.ToDateTime(tryUI16);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#G23", typeof(InvalidCastException), e.GetType());
			}

			try {
				Convert.ToDateTime(tryUI32);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#G24", typeof(InvalidCastException), e.GetType());
			}

			try {
				Convert.ToDateTime(tryUI64);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#G25", typeof(InvalidCastException), e.GetType());
			}

			try {
				Convert.ToDateTime(ci, ci);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#G26", typeof(InvalidCastException), e.GetType());
			}

			try {
				Convert.ToDateTime("20a2-01-01", ci);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#G27", typeof(FormatException), e.GetType());
			}
		}

		public void TestToDecimal() {
			AssertEquals("#H01", (decimal)1, Convert.ToDecimal(boolTrue));
			AssertEquals("#H02", (decimal)0, Convert.ToDecimal(boolFalse));
			AssertEquals("#H03", (decimal)tryByte, Convert.ToDecimal(tryByte));
			AssertEquals("#H04", tryDec, Convert.ToDecimal(tryDec));
			AssertEquals("#H05", (decimal)tryDbl, Convert.ToDecimal(tryDbl));
			AssertEquals("#H06", (decimal)tryInt16, Convert.ToDecimal(tryInt16));
			AssertEquals("#H07", (decimal)tryInt32, Convert.ToDecimal(tryInt32));
			AssertEquals("#H08", (decimal)tryInt64, Convert.ToDecimal(tryInt64));
			AssertEquals("#H09", (decimal)trySByte, Convert.ToDecimal(trySByte));
			AssertEquals("#H10", (decimal)tryFloat, Convert.ToDecimal(tryFloat));
			AssertEquals("#H11", (decimal)23456.432, Convert.ToDecimal("23456.432"));
			AssertEquals("#H12", (decimal)tryUI16, Convert.ToDecimal(tryUI16));
			AssertEquals("#H13", (decimal)tryUI32, Convert.ToDecimal(tryUI32));
			AssertEquals("#H14", (decimal)tryUI64, Convert.ToDecimal(tryUI64));
			AssertEquals("#H15", (decimal)63784, Convert.ToDecimal("63784", ci));
			
			try {
				Convert.ToDecimal(tryChar);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#H20", typeof(InvalidCastException), e.GetType());
			}

			try {
				Convert.ToDecimal(tryDT);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#H21", typeof(InvalidCastException), e.GetType());
			}

			try {
				Convert.ToDecimal(double.MaxValue);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#H22", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToDecimal(double.MinValue);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#H23", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToDecimal(ci);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#H24", typeof(InvalidCastException), e.GetType());
			}
			
			try {
				Convert.ToDecimal(tryStr);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#H25", typeof(FormatException), e.GetType());
			}
			
			try {
				string maxDec = decimal.MaxValue.ToString();
				maxDec = maxDec + "1";				
				Convert.ToDecimal(maxDec);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#H26", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToDecimal(ci, ci);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#H27", typeof(InvalidCastException), e.GetType());
			}

			try {
				Convert.ToDecimal(tryStr, ci);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#H28", typeof(FormatException), e.GetType());
			}
			
			try {
				string maxDec = decimal.MaxValue.ToString();
				maxDec = maxDec + "1";
				Convert.ToDecimal(maxDec, ci);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#H29", typeof(OverflowException), e.GetType());
			}
		}
		
		public void TestToDouble() {
			int iTest = 1;
			try {
				AssertEquals("#I01", (double)1, Convert.ToDouble(boolTrue));
				iTest++;
				AssertEquals("#I02", (double)0, Convert.ToDouble(boolFalse));
				iTest++;
				AssertEquals("#I03", (double)tryByte, Convert.ToDouble(tryByte));
				iTest++;
				AssertEquals("#I04", tryDbl, Convert.ToDouble(tryDbl));
				iTest++;
				AssertEquals("#I05", (double)tryDec, Convert.ToDouble(tryDec));
				iTest++;
				AssertEquals("#I06", (double)tryInt16, Convert.ToDouble(tryInt16));
				iTest++;
				AssertEquals("#I07", (double)tryInt32, Convert.ToDouble(tryInt32));
				iTest++;
				AssertEquals("#I08", (double)tryInt64, Convert.ToDouble(tryInt64));
				iTest++;
				AssertEquals("#I09", (double)trySByte, Convert.ToDouble(trySByte));
				iTest++;
				AssertEquals("#I10", (double)tryFloat, Convert.ToDouble(tryFloat));
				iTest++;
				AssertEquals("#I11", (double)23456.432, Convert.ToDouble("23456.432"));
				iTest++;
				AssertEquals("#I12", (double)tryUI16, Convert.ToDouble(tryUI16));
				iTest++;
				AssertEquals("#I13", (double)tryUI32, Convert.ToDouble(tryUI32));
				iTest++;
				AssertEquals("#I14", (double)tryUI64, Convert.ToDouble(tryUI64));
				iTest++;
				AssertEquals("#H15", (double)63784, Convert.ToDouble("63784", ci));
			} catch (Exception e) {
				Fail ("Unexpected exception at iTest = " + iTest + ": e = " + e);
			}
			
			try {
				Convert.ToDouble(tryChar);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#I20", typeof(InvalidCastException), e.GetType());
			}

			try {
				Convert.ToDouble(tryDT);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#I21", typeof(InvalidCastException), e.GetType());
			}

			try {
				Convert.ToDouble(ci);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#I22", typeof(InvalidCastException), e.GetType());
			}
			
			try {
				Convert.ToDouble(tryStr);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#I23", typeof(FormatException), e.GetType());
			}
			
			try {
				string maxDec = double.MaxValue.ToString();
				maxDec = maxDec + "1";				
				Convert.ToDouble(maxDec);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#I24", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToDouble(ci, ci);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#I25", typeof(InvalidCastException), e.GetType());
			}

			try {
				Convert.ToDouble(tryStr, ci);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#I26", typeof(FormatException), e.GetType());
			}
			
			try {
				string maxDec = double.MaxValue.ToString();
				maxDec = maxDec + "1";
				Convert.ToDouble(maxDec, ci);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#I27", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToDouble(tryObj, ci);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#I28", typeof(InvalidCastException), e.GetType());
			}
		}

		public void TestToInt16() {
			AssertEquals("#J01", (short)0, Convert.ToInt16(boolFalse));
			AssertEquals("#J02", (short)1, Convert.ToInt16(boolTrue));
			AssertEquals("#J03", (short)97, Convert.ToInt16(tryChar));
			AssertEquals("#J04", (short)1234, Convert.ToInt16(tryDec));
			AssertEquals("#J05", (short)0, Convert.ToInt16(tryDbl));
			AssertEquals("#J06", (short)1234, Convert.ToInt16(tryInt16));
			AssertEquals("#J07", (short)12345, Convert.ToInt16(tryInt32));
			AssertEquals("#J08", (short)30000, Convert.ToInt16((long)30000));
			AssertEquals("#J09", (short)123, Convert.ToInt16(trySByte));
			AssertEquals("#J10", (short)1234, Convert.ToInt16(tryFloat));
			AssertEquals("#J11", (short)578, Convert.ToInt16("578"));
			AssertEquals("#J12", (short)15500, Convert.ToInt16((ushort)15500));
			AssertEquals("#J13", (short)5489, Convert.ToInt16((uint)5489));
			AssertEquals("#J14", (short)9876, Convert.ToInt16((ulong)9876));
			AssertEquals("#J15", (short)14, Convert.ToInt16("14", ci));
			AssertEquals("#J16", (short)11, Convert.ToInt16("01011", 2));
			AssertEquals("#J17", (short)1540, Convert.ToInt16("3004", 8));
			AssertEquals("#J18", (short)321, Convert.ToInt16("321", 10));
			AssertEquals("#J19", (short)2748, Convert.ToInt16("ABC", 16));

			try {
				Convert.ToInt16(char.MaxValue);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#J25", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToInt16(tryDT);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#J26", typeof(InvalidCastException), e.GetType());
			}

			try {
				Convert.ToInt16((decimal)(short.MaxValue + 1));
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#J27", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToInt16((decimal)(short.MinValue - 1));
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#J28", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToInt16((double)(short.MaxValue + 1));
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#J29", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToInt16((double)(short.MinValue - 1));
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#J30", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToInt16(50000);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#J31", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToInt16(-50000);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#J32", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToInt16(tryInt64);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#J33", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToInt16(-tryInt64);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#J34", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToInt16(tryObj);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#J35", typeof(InvalidCastException), e.GetType());
			}

			try {
				Convert.ToInt16((float)32767.5);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#J36", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToInt16((float)-33000.54);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#J37", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToInt16(tryStr);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#J38", typeof(FormatException), e.GetType());
			}
			
			try {							
				Convert.ToInt16("-33000");
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#J39", typeof(OverflowException), e.GetType());
			}

			try {							
				Convert.ToInt16(ushort.MaxValue);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#J40", typeof(OverflowException), e.GetType());
			}

			try {							
				Convert.ToInt16(uint.MaxValue);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#J41", typeof(OverflowException), e.GetType());
			}

			try {							
				Convert.ToInt16(ulong.MaxValue);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#J42", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToInt16(tryObj, ci);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#J43", typeof(InvalidCastException), e.GetType());
			}

			try {
				Convert.ToInt16(tryStr, ci);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#J44", typeof(FormatException), e.GetType());
			}
			
			try {							
				Convert.ToInt16("-33000", ci);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#J45", typeof(OverflowException), e.GetType());
			}

			try {							
				Convert.ToInt16("321", 11);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#J46", typeof(ArgumentException), e.GetType());
			}

			try {							
				Convert.ToInt16("D8BF1", 16);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#J47", typeof(OverflowException), e.GetType());
			}
		}

		public void TestToInt32() {
			long tryMax = long.MaxValue;
			long tryMin = long.MinValue;
			AssertEquals("#K01", (int)0, Convert.ToInt32(boolFalse));
			AssertEquals("#K02", (int)1, Convert.ToInt32(boolTrue));
			AssertEquals("#K03", (int)0, Convert.ToInt32(tryByte));
			AssertEquals("#K04", (int)97, Convert.ToInt32(tryChar));
			AssertEquals("#K05", (int)1234, Convert.ToInt32(tryDec));
			AssertEquals("#K06", (int)0, Convert.ToInt32(tryDbl));
			AssertEquals("#K07", (int)1234, Convert.ToInt32(tryInt16));
			AssertEquals("#K08", (int)12345, Convert.ToInt32(tryInt32));
			AssertEquals("#K09", (int)60000, Convert.ToInt32((long)60000));
			AssertEquals("#K10", (int)123, Convert.ToInt32(trySByte));
			AssertEquals("#K11", (int)1234, Convert.ToInt32(tryFloat));
			AssertEquals("#K12", (int)9876, Convert.ToInt32((string)"9876"));
			AssertEquals("#K13", (int)34567, Convert.ToInt32(tryUI16));
			AssertEquals("#K14", (int)567891234, Convert.ToInt32(tryUI32));
			AssertEquals("#K15", (int)0, Convert.ToInt32(tryUI64));
			AssertEquals("#K16", (int)123, Convert.ToInt32("123", ci));
			AssertEquals("#K17", (int)128, Convert.ToInt32("10000000", 2));
			AssertEquals("#K18", (int)302, Convert.ToInt32("456", 8));
			AssertEquals("#K19", (int)456, Convert.ToInt32("456", 10));
			AssertEquals("#K20", (int)1110, Convert.ToInt32("456", 16));

			try {							
				Convert.ToInt32(tryDT);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#K25", typeof(InvalidCastException), e.GetType());
			}

			try {			    
				Convert.ToInt32((decimal)tryMax);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#K26", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToInt32((decimal)tryMin);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#K27", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToInt32((double)tryMax);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#K28", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToInt32((double)tryMin);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#K29", typeof(OverflowException), e.GetType());
			}

			try {							
				Convert.ToInt32(tryInt64);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#K30", typeof(OverflowException), e.GetType());
			}

			try {							
				Convert.ToInt32(-tryInt64);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#K31", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToInt32(tryObj);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#K32", typeof(InvalidCastException), e.GetType());
			}

			try {							
				Convert.ToInt32((float)tryMax);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#K33", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToInt32((float)tryMin);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#K34", typeof(OverflowException), e.GetType());
			}
			
			try {							
				Convert.ToInt32(tryStr, ci);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#K35", typeof(FormatException), e.GetType());
			}

			try {							
				Convert.ToInt32("-46565465123");
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#K36", typeof(OverflowException), e.GetType());
			}

			try {							
				Convert.ToInt32("46565465123");
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#K37", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToInt32((uint)tryMax);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#K38", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToInt32((ulong)tryMax);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#K39", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToInt32(tryObj, ci);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#K40", typeof(InvalidCastException), e.GetType());
			}

			try {
				Convert.ToInt32(tryStr, ci);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#K41", typeof(FormatException), e.GetType());
			}

			try {
				Convert.ToInt32("-46565465123", ci);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#K42", typeof(OverflowException), e.GetType());
			}
			
			try {
				Convert.ToInt32("654", 9);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#K43", typeof(ArgumentException), e.GetType());
			}
		}
		public void TestToInt64() {
			decimal longMax = long.MaxValue;
			longMax += 1000000;
			decimal longMin = long.MinValue;
			longMin -= 1000000;

			AssertEquals("#L01", (long)0, Convert.ToInt64(boolFalse));
			AssertEquals("#L02", (long)1, Convert.ToInt64(boolTrue));
			AssertEquals("#L03", (long)97, Convert.ToInt64(tryChar));
			AssertEquals("#L04", (long)1234, Convert.ToInt64(tryDec));
			AssertEquals("#L05", (long)0, Convert.ToInt64(tryDbl));
			AssertEquals("#L06", (long)1234, Convert.ToInt64(tryInt16));
			AssertEquals("#L07", (long)12345, Convert.ToInt64(tryInt32));
			AssertEquals("#L08", (long)123456789012, Convert.ToInt64(tryInt64));
			AssertEquals("#L09", (long)123, Convert.ToInt64(trySByte));
			AssertEquals("#L10", (long)1234, Convert.ToInt64(tryFloat));
			AssertEquals("#L11", (long)564897, Convert.ToInt64("564897"));
			AssertEquals("#L12", (long)34567, Convert.ToInt64(tryUI16));
			AssertEquals("#L13", (long)567891234, Convert.ToInt64(tryUI32));
			AssertEquals("#L14", (long)0, Convert.ToInt64(tryUI64));
			AssertEquals("#L15", (long)-2548751, Convert.ToInt64("-2548751", ci));
			AssertEquals("#L16", (long)24987562, Convert.ToInt64("1011111010100011110101010", 2));
			AssertEquals("#L17", (long)-24578965, Convert.ToInt64("1777777777777642172153", 8));
			AssertEquals("#L18", (long)248759757, Convert.ToInt64("248759757", 10));
			AssertEquals("#L19", (long)256, Convert.ToInt64("100", 16));

			try {
				Convert.ToInt64(tryDT);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#L20", typeof(InvalidCastException), e.GetType());
			}

			try {
				Convert.ToInt64((decimal)longMax + 1);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#L21", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToInt64((decimal)longMin);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#L24", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToInt64((double)longMax);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#L25:"+longMax, typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToInt64((double)longMin);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#L26", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToInt64(new Exception());
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#L27", typeof(InvalidCastException), e.GetType());
			}

			try {
				Convert.ToInt64(((float)longMax)*100);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#L28:"+longMax, typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToInt64(((float)longMin)*100);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#L29", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToInt64("-567b3");
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#L30", typeof(FormatException), e.GetType());
			}

			try {
				Convert.ToInt64(longMax.ToString());
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#L31:", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToInt64(ulong.MaxValue);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#L32", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToInt64(tryStr, ci);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#L32b", typeof(FormatException), e.GetType());
			}
			
			try {							
				Convert.ToInt64(longMin.ToString(), ci);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#L33", typeof(OverflowException), e.GetType());
			}

			try {							
				Convert.ToInt64("321", 11);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#L34", typeof(ArgumentException), e.GetType());
			}
		}

		public void TestToSByte() {
			int iTest = 1;
			try {
				AssertEquals("#M01", (sbyte)0, Convert.ToSByte(boolFalse));
				iTest++;
				AssertEquals("#M02", (sbyte)1, Convert.ToSByte(boolTrue));
				iTest++;
				AssertEquals("#M03", (sbyte)97, Convert.ToSByte(tryChar));
				iTest++;
				AssertEquals("#M04", (sbyte)15, Convert.ToSByte((decimal)15));
				iTest++;
				AssertEquals("#M05", (sbyte)0, Convert.ToSByte(tryDbl));
				iTest++;
				AssertEquals("#M06", (sbyte)127, Convert.ToSByte((short)127));
				iTest++;
				AssertEquals("#M07", (sbyte)-128, Convert.ToSByte((int)-128));
				iTest++;
				AssertEquals("#M08", (sbyte)30, Convert.ToSByte((long)30));
				iTest++;
				AssertEquals("#M09", (sbyte)123, Convert.ToSByte(trySByte));
				iTest++;
				AssertEquals("#M10", (sbyte)12, Convert.ToSByte((float)12.46987f));
				iTest++;
				AssertEquals("#M11", (sbyte)1, Convert.ToSByte("1"));
				iTest++;
				AssertEquals("#M12", (sbyte)99, Convert.ToSByte((ushort)99));
				iTest++;
				AssertEquals("#M13", (sbyte)54, Convert.ToSByte((uint)54));
				iTest++;
				AssertEquals("#M14", (sbyte)127, Convert.ToSByte((ulong)127));
				iTest++;
				AssertEquals("#M15", (sbyte)14, Convert.ToSByte("14", ci));
				iTest++;
				AssertEquals("#M16", (sbyte)11, Convert.ToSByte("01011", 2));
				iTest++;
				AssertEquals("#M17", (sbyte)5, Convert.ToSByte("5", 8));
				iTest++;
				AssertEquals("#M18", (sbyte)100, Convert.ToSByte("100", 10));
				iTest++;
				AssertEquals("#M19", (sbyte)-1, Convert.ToSByte("FF", 16));
			} catch (Exception e) {
				Fail ("Unexpected exception at iTest = " + iTest + ": e = " + e);
			}

			try {
				Convert.ToSByte((byte)200);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#M25", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToSByte((char)130);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#M26", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToSByte(tryDT);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#M27", typeof(InvalidCastException), e.GetType());
			}

			try {
				Convert.ToSByte((decimal)127.5m);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#M28", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToSByte((decimal)-200m);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#M29", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToSByte((double)150);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#M30", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToSByte((double)-128.6);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#M31", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToSByte((short)150);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#M32", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToSByte((short)-300);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#M33", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToSByte((int)1500);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#M34", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToSByte((int)-1286);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#M35", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToSByte((long)128);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#M36", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToSByte((long)-129);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#M37", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToSByte(new NumberFormatInfo());
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#M38", typeof(InvalidCastException), e.GetType());
			}

			try {
				Convert.ToSByte((float)333);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#M39", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToSByte((float)-666);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#M40", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToSByte("B3");
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#M41", typeof(FormatException), e.GetType());
			}

			try {
				Convert.ToSByte("251");
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#M42", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToSByte(ushort.MaxValue);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#M43", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToSByte((uint)600);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#M44", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToSByte(ulong.MaxValue);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#M45", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToSByte(ci, ci);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#M46", typeof(InvalidCastException), e.GetType());
			}

			try {
				Convert.ToSByte(tryStr, ci);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#M47", typeof(FormatException), e.GetType());
			}
			
			try {							
				Convert.ToSByte("325", ci);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#M48", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToSByte("5D", 15);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#M49", typeof(ArgumentException), e.GetType());
			}
			
			try {							
				Convert.ToSByte("111111111", 2);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#M50", typeof(OverflowException), e.GetType());
			}
		}

		public void TestToSingle() {
			int iTest = 1;
			try {
				AssertEquals("#N01", (float)0, Convert.ToSingle(boolFalse));
				iTest++;
				AssertEquals("#N02", (float)1, Convert.ToSingle(boolTrue));
				iTest++;
				AssertEquals("#N03", (float)0, Convert.ToSingle(tryByte));
				iTest++;
				AssertEquals("#N04", (float)1234,234, Convert.ToSingle(tryDec));
				iTest++;
				AssertEquals("#N05", (float)0, Convert.ToSingle(tryDbl));
				iTest++;
				AssertEquals("#N06", (float)1234, Convert.ToSingle(tryInt16));
				iTest++;
				AssertEquals("#N07", (float)12345, Convert.ToSingle(tryInt32));
				iTest++;
				AssertEquals("#N08", (float)123456789012, Convert.ToSingle(tryInt64));
				iTest++;
				AssertEquals("#N09", (float)123, Convert.ToSingle(trySByte));
				iTest++;
				AssertEquals("#N10", (float)1234,2345, Convert.ToSingle(tryFloat));
				iTest++;
				AssertEquals("#N11", (float)987, Convert.ToSingle("987"));
				iTest++;
				AssertEquals("#N12", (float)34567, Convert.ToSingle(tryUI16));
				iTest++;
				AssertEquals("#N13", (float)567891234, Convert.ToSingle(tryUI32));
				iTest++;
				AssertEquals("#N14", (float)0, Convert.ToSingle(tryUI64));
				iTest++;
				AssertEquals("#N15", (float)654.234, Convert.ToSingle("654.234", ci));
			} catch (Exception e) {
				Fail ("Unexpected exception at iTest = " + iTest + ": e = " + e);
			}

			try {
				Convert.ToSingle(tryChar);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#N25", typeof(InvalidCastException), e.GetType());
			}

			try {
				Convert.ToSingle(tryDT);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#N26", typeof(InvalidCastException), e.GetType());
			}

			try {
				Convert.ToSingle(tryObj);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#N27", typeof(InvalidCastException), e.GetType());
			}
			
			try {
				Convert.ToSingle("A345H");
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#N28", typeof(FormatException), e.GetType());
			}
			
			try {
				Convert.ToSingle(double.MaxValue.ToString());
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#N29", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToSingle(tryObj, ci);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#N30", typeof(InvalidCastException), e.GetType());
			}

			try {
				Convert.ToSingle("J345K", ci);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#N31", typeof(FormatException), e.GetType());
			}

			try {
				Convert.ToSingle("11000000000000000000000000000000000000000000000", ci);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#N32", typeof(OverflowException), e.GetType());
			}
		}

		public void TestToString() {
			
			tryByte = 123;
			AssertEquals("#O01", "False", Convert.ToString(boolFalse));
			AssertEquals("#O02", "True", Convert.ToString(boolTrue));
			AssertEquals("#O03", "123", Convert.ToString(tryByte));
			AssertEquals("#O04", "a", Convert.ToString(tryChar));
			AssertEquals("#O05", tryDT.ToString(), Convert.ToString(tryDT));
			AssertEquals("#O06", tryDec.ToString(), Convert.ToString(tryDec));
			AssertEquals("#O07", tryDbl.ToString(), Convert.ToString(tryDbl));
			AssertEquals("#O08", "1234", Convert.ToString(tryInt16));
			AssertEquals("#O09", "12345", Convert.ToString(tryInt32));
			AssertEquals("#O10", "123456789012", Convert.ToString(tryInt64));
			AssertEquals("#O11", "123", Convert.ToString(trySByte));
			AssertEquals("#O12", tryFloat.ToString(), Convert.ToString(tryFloat));
			AssertEquals("#O13", "foobar", Convert.ToString(tryStr));
			AssertEquals("#O14", "34567", Convert.ToString(tryUI16));
			AssertEquals("#O15", "567891234", Convert.ToString(tryUI32));
			AssertEquals("#O16", "True", Convert.ToString(boolTrue, ci));
			AssertEquals("#O17", "False", Convert.ToString(boolFalse, ci));
			AssertEquals("#O18", "123", Convert.ToString(tryByte, ci));
			AssertEquals("#O19", "1111011", Convert.ToString(tryByte, 2));
			AssertEquals("#O20", "173", Convert.ToString(tryByte, 8));
			AssertEquals("#O21", "123", Convert.ToString(tryByte, 10));
			AssertEquals("#O22", "7b", Convert.ToString(tryByte, 16));
			AssertEquals("#O23", "a", Convert.ToString(tryChar, ci));
			AssertEquals("#O24", tryDT.ToString(ci), Convert.ToString(tryDT, ci));
			AssertEquals("#O25", tryDec.ToString(ci), Convert.ToString(tryDec,ci));
			AssertEquals("#O26", tryDbl.ToString(ci), Convert.ToString(tryDbl, ci));
			AssertEquals("#O27", "1234", Convert.ToString(tryInt16, ci));
			AssertEquals("#O28", "10011010010", Convert.ToString(tryInt16, 2));
			AssertEquals("#O29", "2322", Convert.ToString(tryInt16, 8));
			AssertEquals("#O30", "1234", Convert.ToString(tryInt16, 10));
			AssertEquals("#O31", "4d2", Convert.ToString(tryInt16, 16));
			AssertEquals("#O32", "12345", Convert.ToString(tryInt32, ci));
			AssertEquals("#O33", "11000000111001", Convert.ToString(tryInt32, 2));
			AssertEquals("#O34", "30071", Convert.ToString(tryInt32, 8));
			AssertEquals("#O35", "12345", Convert.ToString(tryInt32, 10));
			AssertEquals("#O36", "3039", Convert.ToString(tryInt32, 16));
			AssertEquals("#O37", "123456789012", Convert.ToString(tryInt64, ci));
			AssertEquals("#O38", "1110010111110100110010001101000010100",
				Convert.ToString(tryInt64, 2));
			AssertEquals("#O39", "1627646215024", Convert.ToString(tryInt64, 8));
			AssertEquals("#O40", "123456789012", Convert.ToString(tryInt64, 10));
			AssertEquals("#O41", "1cbe991a14", Convert.ToString(tryInt64, 16));
			AssertEquals("#O42", "123", Convert.ToString((trySByte), ci));
			AssertEquals("#O43", tryFloat.ToString(ci), Convert.ToString((tryFloat), ci));
			AssertEquals("#O44", "foobar", Convert.ToString((tryStr), ci));
			AssertEquals("#O45", "34567", Convert.ToString((tryUI16), ci));
			AssertEquals("#O46", "567891234", Convert.ToString((tryUI32), ci));
			AssertEquals("#O47", "0", Convert.ToString(tryUI64));
			AssertEquals("#O48", "0", Convert.ToString((tryUI64), ci));

			try {
				Convert.ToString(tryInt16, 5);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#O55", typeof(ArgumentException), e.GetType());
			}

			try {
				Convert.ToString(tryInt32, 17);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#O56", typeof(ArgumentException), e.GetType());
			}

			try {
				Convert.ToString(tryInt64, 1);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#O57", typeof(ArgumentException), e.GetType());
			}			
		}

        public void TestToUInt16() {
			AssertEquals("#P01", (ushort)0, Convert.ToUInt16(boolFalse));
			AssertEquals("#P02", (ushort)1, Convert.ToUInt16(boolTrue));
			AssertEquals("#P03", (ushort)0, Convert.ToUInt16(tryByte));
			AssertEquals("#P04", (ushort)97, Convert.ToUInt16(tryChar));
			AssertEquals("#P05", (ushort)1234, Convert.ToUInt16(tryDec));
			AssertEquals("#P06", (ushort)0, Convert.ToUInt16(tryDbl));
			AssertEquals("#P07", (ushort)1234, Convert.ToUInt16(tryInt16));
			AssertEquals("#P08", (ushort)12345, Convert.ToUInt16(tryInt32));
			AssertEquals("#P09", (ushort)43752, Convert.ToUInt16((long)43752));
			AssertEquals("#P10", (ushort)123, Convert.ToUInt16(trySByte));
			AssertEquals("#P11", (ushort)1234, Convert.ToUInt16(tryFloat));
			AssertEquals("#P12", (ushort)123, Convert.ToUInt16((string)"123"));
			AssertEquals("#P13", (ushort)34567, Convert.ToUInt16(tryUI16));
			AssertEquals("#P14", (ushort)56789, Convert.ToUInt16((uint)56789));
			AssertEquals("#P15", (ushort)0, Convert.ToUInt16(tryUI64));
			AssertEquals("#P16", (ushort)31, Convert.ToUInt16("31", ci));
			AssertEquals("#P17", (ushort)14, Convert.ToUInt16("1110", 2));
			AssertEquals("#P18", (ushort)32, Convert.ToUInt16("40", 8));
			AssertEquals("#P19", (ushort)40, Convert.ToUInt16("40", 10));
			AssertEquals("#P20", (ushort)64, Convert.ToUInt16("40", 16));


			try {
				Convert.ToUInt16(tryDT);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#P25", typeof(InvalidCastException), e.GetType());
			}

			try {
				Convert.ToUInt16(decimal.MaxValue);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#P26", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToUInt16(decimal.MinValue);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#P27", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToUInt16(double.MaxValue);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#P28", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToUInt16(double.MinValue);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#P29", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToUInt16(short.MinValue);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#P30", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToUInt16(int.MaxValue);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#P31", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToUInt16(int.MinValue);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#P32", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToUInt16(long.MaxValue);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#P33", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToUInt16(long.MinValue);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#P34", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToUInt16(tryObj);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#P35", typeof(InvalidCastException), e.GetType());
			}

			try {
				Convert.ToUInt16(sbyte.MinValue);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#P36", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToUInt16(float.MaxValue);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#P37", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToUInt16(float.MinValue);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#P38", typeof(OverflowException), e.GetType());
			}
			
			try {
				Convert.ToUInt16("1A2");
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#P39", typeof(FormatException), e.GetType());
			}

			try {
				Convert.ToUInt16("-32800");
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#P40", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToUInt16(int.MaxValue.ToString());
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#P41", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToUInt16(ulong.MaxValue);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#P42", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToUInt16("1A2", ci);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#P43", typeof(FormatException), e.GetType());
			}

			try {
				Convert.ToUInt16("-32800", ci);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#P44", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToUInt16("456987", ci);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#P45", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToUInt16("40", 9);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#P46", typeof(ArgumentException), e.GetType());
			}
		}

		public void TestToUInt32() {
			AssertEquals("#Q01", (uint)1, Convert.ToUInt32(boolTrue));
			AssertEquals("#Q02", (uint)0, Convert.ToUInt32(boolFalse));
			AssertEquals("#Q03", (uint)0, Convert.ToUInt32(tryByte));
			AssertEquals("#Q04", (uint)97, Convert.ToUInt32(tryChar));
			AssertEquals("#Q05", (uint)1234, Convert.ToUInt32(tryDec));
			AssertEquals("#Q06", (uint)0, Convert.ToUInt32(tryDbl));
			AssertEquals("#Q07", (uint)1234, Convert.ToUInt32(tryInt16));
			AssertEquals("#Q08", (uint)12345, Convert.ToUInt32(tryInt32));
			AssertEquals("#Q09", (uint)1234567890, Convert.ToUInt32((long)1234567890));
			AssertEquals("#Q10", (uint)123, Convert.ToUInt32(trySByte));
			AssertEquals("#Q11", (uint)1234, Convert.ToUInt32(tryFloat));
			AssertEquals("#Q12", (uint)3456789, Convert.ToUInt32("3456789"));
			AssertEquals("#Q13", (uint)34567, Convert.ToUInt32(tryUI16));
			AssertEquals("#Q14", (uint)567891234, Convert.ToUInt32(tryUI32));
			AssertEquals("#Q15", (uint)0, Convert.ToUInt32(tryUI64));
			AssertEquals("#Q16", (uint)415, Convert.ToUInt32("110011111", 2));
			AssertEquals("#Q17", (uint)156, Convert.ToUInt32("234" ,8));
			AssertEquals("#Q18", (uint)234, Convert.ToUInt32("234" ,10));
			AssertEquals("#Q19", (uint)564, Convert.ToUInt32("234" ,16));
			

			try {
				Convert.ToUInt32(tryDT);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#Q25", typeof(InvalidCastException), e.GetType());
			}

			try {
				Convert.ToUInt32(decimal.MaxValue);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#Q26", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToUInt32((decimal)-150);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#Q27", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToUInt32(double.MaxValue);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#Q28", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToUInt32((double)-1);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#Q29", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToUInt32(short.MinValue);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#Q30", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToUInt32(int.MinValue);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#Q31", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToUInt32(long.MaxValue);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#Q32", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToUInt32((long)-50000);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#Q33", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToUInt32(new Exception());
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#Q34", typeof(InvalidCastException), e.GetType());
			}

			try {
				Convert.ToUInt32(sbyte.MinValue);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#Q35", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToUInt32(float.MaxValue);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#Q36", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToUInt32(float.MinValue);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#Q37", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToUInt32("45t54");
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#Q38", typeof(FormatException), e.GetType());
			}

			try {
				Convert.ToUInt32("-55");
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#Q39", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToUInt32(ulong.MaxValue);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#Q40", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToUInt32(new Exception(), ci);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#Q41", typeof(InvalidCastException), e.GetType());
			}

			try {
				Convert.ToUInt32(tryStr, ci);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#Q42", typeof(FormatException), e.GetType());
			}

			try {
				Convert.ToUInt32("-50", ci);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#Q43", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToUInt32(decimal.MaxValue.ToString(), ci);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#Q44", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToUInt32("1001110", 1);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#Q45", typeof(ArgumentException), e.GetType());
			}
		}

		public void TestToUInt64() 
		{
			int iTest = 1;
			try {
				AssertEquals("#R01", (ulong)1, Convert.ToUInt64(boolTrue));
				iTest++;
				AssertEquals("#R02", (ulong)0, Convert.ToUInt64(boolFalse));
				iTest++;
				AssertEquals("#R03", (ulong)0, Convert.ToUInt64(tryByte));
				iTest++;
				AssertEquals("#R04", (ulong)97, Convert.ToUInt64(tryChar));
				iTest++;
				AssertEquals("#R05", (ulong)1234, Convert.ToUInt64(tryDec));
				iTest++;
				AssertEquals("#R06", (ulong)0, Convert.ToUInt64(tryDbl));
				iTest++;
				AssertEquals("#R07", (ulong)1234, Convert.ToUInt64(tryInt16));
				iTest++;
				AssertEquals("#R08", (ulong)12345, Convert.ToUInt64(tryInt32));
				iTest++;
				AssertEquals("#R09", (ulong)123456789012, Convert.ToUInt64(tryInt64));
				iTest++;
				AssertEquals("#R10", (ulong)123, Convert.ToUInt64(trySByte));
				iTest++;
				AssertEquals("#R11", (ulong)1234, Convert.ToUInt64(tryFloat));
				iTest++;
				AssertEquals("#R12", (ulong)345678, Convert.ToUInt64("345678"));
				iTest++;
				AssertEquals("#R13", (ulong)34567, Convert.ToUInt64(tryUI16));
				iTest++;
				AssertEquals("#R14", (ulong)567891234, Convert.ToUInt64(tryUI32));
				iTest++;
				AssertEquals("#R15", (ulong)0, Convert.ToUInt64(tryUI64));
				iTest++;
				AssertEquals("#R16", (ulong)123, Convert.ToUInt64("123", ci));
				iTest++;
				AssertEquals("#R17", (ulong)4, Convert.ToUInt64("100", 2));
				iTest++;
				AssertEquals("#R18", (ulong)64, Convert.ToUInt64("100", 8));
				iTest++;
				AssertEquals("#R19", (ulong)100, Convert.ToUInt64("100", 10));
				iTest++;
				AssertEquals("#R20", (ulong)256, Convert.ToUInt64("100", 16));
			} catch (Exception e) {
				Fail ("Unexpected exception caught when iTest = " + iTest + ": e = " + e);
			}

			try {
				Convert.ToUInt64(tryDT);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#R25", typeof(InvalidCastException), e.GetType());
			}

			try {
				Convert.ToUInt64(decimal.MaxValue);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#R26", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToUInt64((decimal)-140);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#R27", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToUInt64(double.MaxValue);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#R28", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToUInt64((double)-1);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#R29", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToUInt64(short.MinValue);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#R30", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToUInt64(int.MinValue);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#R31", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToUInt64(long.MinValue);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#R32", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToUInt64(tryObj);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#R33", typeof(InvalidCastException), e.GetType());
			}

			try {
				Convert.ToUInt64(sbyte.MinValue);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#R34", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToUInt64(float.MinValue);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#R35", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToUInt64(float.MaxValue);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#R36", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToUInt64("234rt78");
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#R37", typeof(FormatException), e.GetType());
			}

			try {
				Convert.ToUInt64("-68");
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#R38", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToUInt64(decimal.MaxValue.ToString());
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#R39", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToUInt64("23rd2", ci);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#R40", typeof(FormatException), e.GetType());
			}

			try {
				Convert.ToUInt64(decimal.MinValue.ToString(), ci);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#R41", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToUInt64(decimal.MaxValue.ToString(), ci);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#R42", typeof(OverflowException), e.GetType());
			}

			try {
				Convert.ToUInt64("132", 9);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#R43", typeof(ArgumentException), e.GetType());
			}
		}

		public void TestToBase64CharArray() {
			byte[] byteArr = {33, 127, 255, 109, 170, 54};
			//						   0    1    2    3    4    5    6    7
			char[] expectedCharArr = {'I', 'X', '/', '/', 'b', 'a', 'o', '2'};
			char[] result = new Char[8];
			
			Convert.ToBase64CharArray(byteArr, 0, byteArr.Length, result, 0);

			for (int i = 0; i < expectedCharArr.Length; i++) {
				AssertEquals("#S0" + i, expectedCharArr[i], result[i]);
			}

			try {
				Convert.ToBase64CharArray(null, 0, byteArr.Length, result, 0);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#S10", typeof(ArgumentNullException), e.GetType());
			}

			try {
				Convert.ToBase64CharArray(byteArr, -1, byteArr.Length, result, 0);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#S11", typeof(ArgumentOutOfRangeException), e.GetType());
			}

			try {
				Convert.ToBase64CharArray(byteArr, 0, -5, result, 0);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#S12", typeof(ArgumentOutOfRangeException), e.GetType());
			}

			try {
				Convert.ToBase64CharArray(byteArr, 0, byteArr.Length, result, -2);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#S13", typeof(ArgumentOutOfRangeException), e.GetType());
			}

			try {
				Convert.ToBase64CharArray(byteArr, 4, byteArr.Length, result, 0);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#S14", typeof(ArgumentOutOfRangeException), e.GetType());
			}

			try {
				Convert.ToBase64CharArray(byteArr, 0, byteArr.Length, result, 2);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#S15", typeof(ArgumentOutOfRangeException), e.GetType());
			}
		}		
		public void TestToBase64String() {
			byte[] byteArr = {33, 127, 255, 109, 170, 54};
			string expectedStr = "IX//bao2";
			string result1;
			string result2;
			
			result1 = Convert.ToBase64String(byteArr);
			result2 = Convert.ToBase64String(byteArr, 0, byteArr.Length);

			AssertEquals("#T01", expectedStr, result1);
			AssertEquals("#T02", expectedStr, result2);

			try {
				Convert.ToBase64String(null);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#T05", typeof(ArgumentNullException), e.GetType());
			}
			
			try {
				Convert.ToBase64String(byteArr, -1, byteArr.Length);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#T06", typeof(ArgumentOutOfRangeException), e.GetType());
			}

			try {
				Convert.ToBase64String(byteArr, 0, -10);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#T07", typeof(ArgumentOutOfRangeException), e.GetType());
			}

			try {
				Convert.ToBase64String(byteArr, 4, byteArr.Length);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#T08", typeof(ArgumentOutOfRangeException), e.GetType());
			}		
		}

		/* Have experienced some problems with FromBase64CharArray using mono. Something 
		 * about error in a unicode file.
		 *
		 * However the test seems to run fine using mono in a cygwin environment
		 */
		public void TestFromBase64CharArray() {

			char[] charArr = {'M','o','n','o','m','o','n','o'};
			byte[] expectedByteArr = {50, 137, 232, 154, 137, 232};
			
			byte[] fromCharArr = Convert.FromBase64CharArray(charArr, 0, 8);			

			for (int i = 0; i < fromCharArr.Length; i++){
				AssertEquals("#U0" + i, expectedByteArr[i], fromCharArr[i]);
			}

			try {
				Convert.FromBase64CharArray(null, 0, charArr.Length);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#U10", typeof(ArgumentNullException), e.GetType());
			}
			
			try {
				Convert.FromBase64CharArray(charArr, -1, charArr.Length);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#U11", typeof(ArgumentOutOfRangeException), e.GetType());
			}

			try {
				Convert.FromBase64CharArray(charArr, 0, -5);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#U12", typeof(ArgumentOutOfRangeException), e.GetType());
			}

			try {
				Convert.FromBase64CharArray(charArr, 0, 3);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#U13", typeof(FormatException), e.GetType());
			}

			try {
				Convert.FromBase64CharArray(charArr, 0, 9);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#U14", typeof(ArgumentOutOfRangeException), e.GetType());
			}

			try {
				Convert.FromBase64CharArray(charArr, 0, 5);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#U15", typeof(FormatException), e.GetType());
			}
		}

		/* Have experienced some problems with FromBase64String using mono. Something about 
		 * error in a unicode file.
		 *
		 * However the test seems to run fine using mono in a cygwin environment
		 */
		public void TestFromBase64String() {
			string str = "Monomono";
			string err01 = "foo";

			byte[] expectedByteArr = {50, 137, 232, 154, 137, 232};
			byte[] fromStr = Convert.FromBase64String(str);	

			for (int i = 0; i < fromStr.Length; i++){
				AssertEquals("#V0" + i, expectedByteArr[i], fromStr[i]);
			}

			try {
				Convert.FromBase64String(null);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#V10", typeof(ArgumentNullException), e.GetType());
			}

			try {
				Convert.FromBase64String(err01);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#V11", typeof(FormatException), e.GetType());
			}

			try {
				Convert.FromBase64String(tryStr);
				Fail();
			}
			catch (Exception e) {
				AssertEquals("#V12", typeof(FormatException), e.GetType());
			}
		}
	}
}
