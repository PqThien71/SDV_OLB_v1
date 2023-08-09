using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using RTCVision2101.Template;
//using RTCVision2101.Variables;
//using RTCVision2101.Classes;
using HalconDotNet;
//using RTCVision2101.Enums;
using System.Windows.Forms;
using System.Reflection;
//using RTCVision2101.Consts;
using System.IO;
using System.Drawing;
using System.Data;
using System.Globalization;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json;
using System.IO.Ports;
using System.Net.Sockets;
using System.Net;
//using RJCP.IO.Ports;
//using RTCVision2101.Forms;

namespace SDV_OLB_v1
{
    public static class GlobFuncs
    {
        #region "SYSTEM"
        /// <summary>
        /// Lưu lỗi
        /// </summary>
        /// <param name="AEx">Đối tượng lỗi</param>
        /// <param name="AMoreInfo">Thông tin gắn thêm</param>
        //public static void SaveErr(Exception AEx, string AMoreInfo = "")
        //{
        //    GlobVar.ErrHandle.SaveErrors(AEx, GlobVar.RTCVision.OSInfo.FullInfo, AMoreInfo);
        //}

        //public static void SaveErr(string ErrMsg, string AMoreInfo = "")
        //{
        //    Exception AEx = new Exception(ErrMsg);
        //    GlobVar.ErrHandle.SaveErrors(AEx, GlobVar.RTCVision.OSInfo.FullInfo, AMoreInfo);
        //}
        /// <summary>
        /// Đọc thông số chương trình trước khi chạy
        /// </summary>
        /// <summary>
        /// Thiết lập các môi trường làm việc của chương trình
        /// </summary>
        public static void SetupEnvironment()
        {
            GlobVar.MyEngine = new HDevEngine();
            GlobVar.MyEngine.SetProcedurePath(GlobVar.RTCVision.Paths.Procedures);
            GlobVar.MyEngine.SetEngineAttribute("debug_password", "1");
            GlobVar.MyEngine.SetEngineAttribute("debug_port", new HTuple(50));
            GlobVar.MyEngine.SetEngineAttribute("debug_wait_for_connection", "true");
            HOperatorSet.SetSystem("clip_region", "true");

            System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";
            customCulture.NumberFormat.NumberGroupSeparator = ",";

            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;

            /* KHỞI TẠO MÔI TRƯỜNG SERVER NẾU CÓ */
            if (GlobVar.RTCVision.Options.IsServer)
            {
                //GlobVar.Server = new cServer();
                //GlobVar.Server.Start();
            }

            /* KHỞI TẠO DỮ LIỆU ASCII */
            GlobFuncs.GenerateDicASCII();
        }
        #endregion

        #region "HSMARTWINDOW"
        /// <summary>
        /// Thiết lập kích thước cửa sổ HsmartWindow
        /// </summary>
        /// <param name="Image">Ảnh cần view</param>
        /// <param name="HSWindow">Đối tượng HsmartWindow</param>
        public static void SmartSetPart(HObject Image, HSmartWindowControl HSWindow)
        {
            HOperatorSet.GetImageSize(Image, out HTuple width, out HTuple height);
            int wdWidth = HSWindow.Width;
            int wdHeight = HSWindow.Height;

            int imgRow, imgCol;
            int imgWidth, imgHeight;
            //
            if (((float)wdHeight / (float)wdWidth) > ((float)height / (float)width))
            {
                imgCol = 0;
                imgWidth = width;
                imgHeight = wdHeight * imgWidth / wdWidth;
                imgRow = (height - imgHeight) / 2;

            }
            else
            {
                imgRow = 0;
                imgHeight = height;
                if (wdHeight == 0) return;
                imgWidth = wdWidth * imgHeight / wdHeight;
                imgCol = (width - imgWidth) / 2;
            }
            if (imgRow < 0)
                imgRow = 0;
            if (imgCol < 0)
                imgCol = 0;
            HSWindow.HalconWindow.SetPart(new HTuple(imgRow), new HTuple(imgCol), new HTuple(imgHeight + imgRow), new HTuple(imgWidth + imgCol));
        }
        #endregion

        #region "TREELIST"
        #endregion

        #region "OTHERS"
        public static string FixedDirSepChar(string path)
        {
            if (path!="" && !path.EndsWith(Path.DirectorySeparatorChar.ToString()))
                return path + Path.DirectorySeparatorChar;
            return path;
        }
        public static string CreateSaveFolderWithDay(string _Path)
        {
            if (_Path == "") return _Path;
            return FixedDirSepChar(FixedDirSepChar(_Path) + DateTime.Now.Date.ToString("ddMMyyyy"));
        }

        public static string FirstCharToUpper(this string input)
        {
            switch (input)
            {
                case null: throw new ArgumentNullException(nameof(input));
                case "": throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input));
                default: return input.First().ToString().ToUpper() + input.Substring(1);
            }
        }
        public static void SaveAllControlEnableStatus(Control _Control)
        {
            if (_Control != null)
            {
                _Control.Tag = _Control.Enabled ? 1 : 0;
                if (_Control.Controls != null && _Control.Controls.Count > 0)
                    foreach (Control item in _Control.Controls)
                        SaveAllControlEnableStatus(item);
            }
        }
        public static void DisableAllControls(Control _Control,bool _SaveOldEnable = true)
        {
            if (_Control!=null)
            {
                _Control.Enabled = false;
                if (_Control.Controls!=null && _Control.Controls.Count>0)
                    foreach (Control item in _Control.Controls)
                        DisableAllControls(item, _SaveOldEnable);
            }
        }

        public static void EnableAllControls(Control _Control,bool _SetOldEnable = true)
        {
            if (_Control != null)
            {
                if (_SetOldEnable) 
                    _Control.Enabled = (_Control.Tag!=null && int.Parse(_Control.Tag.ToString()) == 1);
                else
                    _Control.Enabled = true;
                if (_Control.Controls != null && _Control.Controls.Count > 0)
                    foreach (Control item in _Control.Controls)
                        EnableAllControls(item, _SetOldEnable);
            }
        }
        /// <summary>
        /// Gán giá trị cho 1 ô text bằng 1 giá trị HTuple
        /// </summary>
        /// <param name="_TextEdit">Ô text cần gán giá trị</param>
        /// <param name="_Value">Dữ liệu gán dạng HTuple</param>
        //public static void SetTextEditValueByHtuple(TextEdit _TextEdit, HTupleElements _Value)
        //{
        //    switch (_Value.Type)
        //    {
        //        case HalconDotNet.HTupleType.EMPTY:
        //            break;
        //        case HalconDotNet.HTupleType.INTEGER:
        //            _TextEdit.EditValue = _Value.I;
        //            break;
        //        case HalconDotNet.HTupleType.LONG:
        //            _TextEdit.EditValue = _Value.L;
        //            break;
        //        case HalconDotNet.HTupleType.DOUBLE:
        //            _TextEdit.EditValue = _Value.D;
        //            break;
        //        case HalconDotNet.HTupleType.STRING:
        //            _TextEdit.EditValue = _Value.S;
        //            break;
        //        case HalconDotNet.HTupleType.HANDLE:
        //            break;
        //        case HalconDotNet.HTupleType.MIXED:
        //            if(_Value.O!=null)
        //                _TextEdit.EditValue = _Value.O.ToString();
        //            break;
        //        default:
        //            break;
        //    }
        //}
        public static List<string> String2ListString(string _Value, char _Sep)
        {
            string[] s = _Value.Split(_Sep);
            List<string> obj = new List<string>();
            obj.AddRange(s);
            return obj;
        }
        private const int WM_SETREDRAW = 11;

        /// Suspends painting for the target control. Do NOT forget to call EndControlUpdate!!!
        /// </summary>
        /// <param name="control">visual control</param>
        public static void BeginControlUpdate(Control control)
        {
            Message msgSuspendUpdate = Message.Create(control.Handle, WM_SETREDRAW, IntPtr.Zero,
                  IntPtr.Zero);

            NativeWindow window = NativeWindow.FromHandle(control.Handle);
            window.DefWndProc(ref msgSuspendUpdate);
        }

        /// <summary>
        /// Resumes painting for the target control. Intended to be called following a call to BeginControlUpdate()
        /// </summary>
        /// <param name="control">visual control</param>
        public static void EndControlUpdate(Control control)
        {
            // Create a C "true" boolean as an IntPtr
            IntPtr wparam = new IntPtr(1);
            Message msgResumeUpdate = Message.Create(control.Handle, WM_SETREDRAW, wparam,
                  IntPtr.Zero);

            NativeWindow window = NativeWindow.FromHandle(control.Handle);
            window.DefWndProc(ref msgResumeUpdate);
            control.Invalidate();
            control.Refresh();
        }
        public static void EnableTransparency(Control c)
        {
            MethodInfo method = c.GetType().GetMethod("SetStyle", BindingFlags.NonPublic | BindingFlags.Instance);
            method.Invoke(c, new object[] { ControlStyles.SupportsTransparentBackColor | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true });
            c.BackColor = Color.Transparent;
            c.ForeColor = Color.Black;
        }
        //public static void ValidateTextEditIntValue(TextEdit txt,int MaxValue=0, int MinValue=0, bool CheckMaxValue = false, bool CheckMinValue = false, bool AcceptInfinity = false)
        //{
        //    int dvalue = 0;
        //    if ((txt.Text.ToLower() == "infinity" || txt.Text.ToLower() == "inf") && AcceptInfinity)
        //    {
        //        txt.Text = "Inf";
        //    }
        //    else
        //    if (int.TryParse(txt.Text, out dvalue))
        //    {
        //        if (CheckMinValue && dvalue < MinValue)
        //        {
        //            txt.EditValue = MinValue;
        //        }
        //        if (CheckMaxValue && dvalue > MaxValue)
        //        {
        //            txt.EditValue = MaxValue;
        //        }
        //    }
        //    else
        //    {
        //        txt.EditValue = txt.OldEditValue;
        //    }
        //}
        //public static void ValidateTextEditDoubleValue(TextEdit txt, double MaxValue = 0, double MinValue = 0, bool CheckMaxValue = false, bool CheckMinValue = false, bool AcceptInfinity = false)
        //{
        //    double dvalue = 0;
        //    if ((txt.Text.ToLower() == "infinity" || txt.Text.ToLower() == "inf") && AcceptInfinity)
        //    {
        //        txt.Text = "Inf";
        //    }
        //    else
        //    if (double.TryParse(txt.Text, out dvalue))
        //    {
        //        if (CheckMinValue && dvalue < MinValue)
        //        {
        //            txt.EditValue = MinValue;
        //        }
        //        if (CheckMaxValue && dvalue > MaxValue)
        //        {
        //            txt.EditValue = MaxValue;
        //        }
        //    }
        //    else
        //    {
        //        txt.EditValue = txt.OldEditValue;
        //    }
        //}
        public static IEnumerable<Control> GetAll(Control control, Type type,bool withRTCPrefix = true)
        {
            var controls = control.Controls.Cast<Control>();
            if (withRTCPrefix)
            {
                return controls.SelectMany(ctrl => GetAll(ctrl, type, withRTCPrefix))
                                      .Concat(controls)
                                      .Where(c => c.GetType() == type && c.Name.StartsWith("RTC"));
            }
            else
                return controls.SelectMany(ctrl => GetAll(ctrl, type, withRTCPrefix))
                                      .Concat(controls)
                                      .Where(c => c.GetType() == type && !c.Name.StartsWith("RTC"));
        }
        public static IEnumerable<Control> GetAllControls(Control control)
        {
            var controls = control.Controls.Cast<Control>();
            return controls.SelectMany(ctrl => GetAllControls(ctrl)
                                      .Concat(controls));
        }
        public static IEnumerable<Control> GetAllControls(Control control,Type type)
        {
            var controls = control.Controls.Cast<Control>();
            return controls.SelectMany(ctrl => GetAllControls(ctrl, type)
                                      .Concat(controls)
                                      .Where(c => c.GetType() == type));
        }
       
       
        public static string HTupleElements2Str(HTupleElements hTupleElements)
        {
            string Result = string.Empty;

            switch (hTupleElements.Type)
            {
                case HalconDotNet.HTupleType.EMPTY:
                    break;
                case HalconDotNet.HTupleType.INTEGER:
                    Result = hTupleElements.I.ToString();
                    break;
                case HalconDotNet.HTupleType.LONG:
                    Result = hTupleElements.L.ToString();
                    break;
                case HalconDotNet.HTupleType.DOUBLE:
                    Result = hTupleElements.D.ToString();
                    break;
                case HalconDotNet.HTupleType.STRING:
                    Result = hTupleElements.S;
                    break;
                case HalconDotNet.HTupleType.HANDLE:
                    Result = hTupleElements.H.ToString();
                    break;
                case HalconDotNet.HTupleType.MIXED:
                    if(hTupleElements.O!=null)
                        Result = hTupleElements.O.ToString();
                    break;
                default:
                    break;
            }

            return Result;
        }

        public static string HTuple2Str(HTuple hTuple,int elementnumber=-1)
        {
            if (hTuple == null || hTuple.Length<=0)
                return string.Empty;

            string Result = string.Empty;
            int _elementnumber = elementnumber == -1 ? hTuple.Length : elementnumber;
            for (int i = 0; i < _elementnumber; i++)
            {
                if (Result==string.Empty)
                    Result = HTupleElements2Str(hTuple[i]);
                else
                    Result = Result + "," + HTupleElements2Str(hTuple[i]);
            }

            return Result;
        }
        /// <summary>
        /// HTupleElement To Double
        /// </summary>
        /// <param name="hTupleElements"></param>
        /// <returns></returns>
        public static double HTEToD(HTupleElements hTupleElements)
        {
            double Result = 0;

            switch (hTupleElements.Type)
            {
                case HalconDotNet.HTupleType.EMPTY:
                    break;
                case HalconDotNet.HTupleType.INTEGER:
                    Result = hTupleElements.I;
                    break;
                case HalconDotNet.HTupleType.LONG:
                    Result = hTupleElements.L;
                    break;
                case HalconDotNet.HTupleType.DOUBLE:
                    Result = hTupleElements.D;
                    break;
                case HalconDotNet.HTupleType.STRING:
                    double.TryParse(hTupleElements.S, out Result);
                    break;
                case HalconDotNet.HTupleType.HANDLE:
                    break;
                case HalconDotNet.HTupleType.MIXED:
                    if (hTupleElements.O != null)
                        double.TryParse(hTupleElements.O.ToString(), out Result);
                    break;
                default:
                    break;
            }

            return Result;
        }
        
        
        
        
        public static bool IsNumeric(string _sNeedCheck)
        {
            return double.TryParse(_sNeedCheck, out double _dNeedCheck);
        }
        public static bool IsInt(string _sNeedCheck)
        {
            return int.TryParse(_sNeedCheck, out int _dNeedCheck);
        }
        public static bool IsIntList(string[] _sValues)
        {
            foreach (string item in _sValues)
            {
                if (!int.TryParse(item,out int result))
                {
                    return false;
                }
            }
            return true;
        }
        public static bool IsDoubleList(string[] _sValues)
        {
            foreach (string item in _sValues)
            {
                if (!double.TryParse(item, out double result))
                {
                    return false;
                }
            }
            return true;
        }
        
        public static Dictionary<TKey, TValue> CloneDictionaryROIs<TKey, TValue>
                      (Dictionary<TKey, TValue> original) where TValue : ICloneable
        {
            Dictionary<TKey, TValue> ret = new Dictionary<TKey, TValue>(original.Count,
                                                                    original.Comparer);
            foreach (KeyValuePair<TKey, TValue> entry in original)
            {
                ret.Add(entry.Key, (TValue)entry.Value.Clone());
            }
            return ret;
        }

        public static List<string> GetDirectories(string path, string searchPattern = "*",
        SearchOption searchOption = SearchOption.AllDirectories)
        {
            if (searchOption == SearchOption.TopDirectoryOnly)
                return Directory.GetDirectories(path, searchPattern).ToList();

            var directories = new List<string>(GetDirectories(path, searchPattern));

            for (var i = 0; i < directories.Count; i++)
                directories.AddRange(GetDirectories(directories[i], searchPattern));

            return directories;
        }

        private static List<string> GetDirectories(string path, string searchPattern)
        {
            try
            {
                return Directory.GetDirectories(path, searchPattern).ToList();
            }
            catch (UnauthorizedAccessException)
            {
                return new List<string>();
            }
        }
        /// <summary>
        /// Chuyển từ có dấu sang không dấu
        /// </summary>
        /// <param name="_Text">Chuỗi có dấu cần chuyển</param>
        /// <returns></returns>
        public static string SwitchToUnsigned(string _Text)
        {
            string[] CoDau = new[] { "aàáảãạăằắẳẵặâầấẩẫậ", "AÀÁẢÃẠĂẰẮẲẴẶÂẦẤẨẪẬ", "đ", "Đ", "eèéẻẽẹêềếểễệ", "EÈÉẺẼẸÊỀẾỂỄỆ", "iìíỉĩị", "IÌÍỈĨỊ", "oòóỏõọôồốổỗộơờớởỡợ", "ÒÒÓỎÕỌÔỒỐỔỖỘƠỜỚỞỠỢ", "uùúủũụưừứửữự", "UÙÚỦŨỤƯỪỨỬỮỰ", "yỳýỷỹỵ", "YỲÝỶỸỴ" };
            string[] KoDau = new[] { "a", "A", "d", "D", "e", "E", "i", "I", "o", "O", "u", "U", "y", "Y" };
            string str = _Text;
            string strReturn = "";
            for (int i = 0; i <= str.Length - 1; i++)
            {
                string iStr = str.Substring(i, 1);
                string rStr = iStr;
                for (int j = 0; j <= CoDau.Length - 1; j++)
                {
                    if (CoDau[j].IndexOf(iStr) >= 0)
                    {
                        rStr = KoDau[j];
                        break;
                    }
                }
                strReturn += rStr;
            }
            return strReturn;
        }
        public static bool FileIsUsed(string filename)
        {
            bool Locked = false;
            try
            {
                FileStream fs =
                    File.Open(filename, FileMode.OpenOrCreate,
                    FileAccess.ReadWrite, FileShare.None);
                fs.Close();
            }
            catch (IOException)
            {
                Locked = true;
            }
            return Locked;
        }
        public static T DeepClone<T>(T obj)
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                ms.Position = 0;
                return (T)formatter.Deserialize(ms);
            }
        }
      
        #endregion

        #region "ACTIONS FUNCTIONS"
        public static object GetPropDefaultValueByBaseType(Type PropType)
        {
            object obj = null;

            switch (PropType.Name)
            {
                case "SString":
                    obj = string.Empty;
                    break;
                case "SInt":
                    obj = 0;
                    break;
                case "SDouble":
                    obj = 0;
                    break;
                case "SBool":
                    obj = false;
                    break;
                case "Guild":
                    obj = Guid.Empty;
                    break;
                default:
                    break;
            }

            return obj;
        }
       
        public static T Clone<T>(T source)
        {
            var serialized = JsonConvert.SerializeObject(source);
            return JsonConvert.DeserializeObject<T>(serialized);
        }
        #endregion

        #region "CAMERA"
        public static string GetVendorName(string _InterfaceName)
        {
            string Result = string.Empty;

            if (_InterfaceName == "") return Result;
            string[] sp = new string[] { " | " };
            string[] s = _InterfaceName.Split(sp,StringSplitOptions.RemoveEmptyEntries);

            foreach (string item in s)
            {
                if (item.StartsWith("vendor:"))
                {
                    Result = item.Substring(7);
                    break;
                }
            }
            return Result;
        }
        public static string GenCameraName(string _InterfaceName)
        {
            string Result = string.Empty;

            if (_InterfaceName == "") return Result;
            string[] sp = new string[] { " | " };
            string[] s = _InterfaceName.Split(sp, StringSplitOptions.RemoveEmptyEntries);
            string sVendor = string.Empty;
            string sDevice = string.Empty;
            foreach (string item in s)
            {
                if (item.StartsWith("vendor:"))
                {
                    sVendor = item.Substring(7);
                }
                else if (item.StartsWith("device:"))
                {
                    sDevice = item.Substring(7);
                }
            }
            Result = sVendor != "" ? sVendor + " - " + (sDevice != "" ? sDevice : "") : "";
            if (Result=="")
            {
                Result = _InterfaceName;
            }
            return Result;
        }
        #endregion

        #region "DATAROW"
        public static Guid GetDataRowValue_Guid(DataRow _Row,string _ColName)
        {
            Guid Result = Guid.Empty;
            if (_Row == null) return Result;
            if (!Guid.TryParse(_Row[_ColName].ToString(), out Result)) Result = Guid.Empty;
            return Result;
        }
        public static string GetDataRowValue_String(DataRow _Row, string _ColName)
        {
            string Result = string.Empty;
            if (_Row == null) return Result;
            Result = _Row[_ColName].ToString();
            return Result;
        }
        public static bool GetDataRowValue_Boolean(DataRow _Row, string _ColName)
        {
            bool Result = false;
            if (_Row == null) return Result;
            if (!bool.TryParse(_Row[_ColName].ToString(), out Result)) Result = false;
            return Result;
        }
        public static int GetDataRowValue_Int(DataRow _Row, string _ColName)
        {
            int Result = 0;
            if (_Row == null) return Result;
            if (!int.TryParse(_Row[_ColName].ToString(), out Result)) Result = 0;
            return Result;
        }
        public static long GetDataRowValue_Long(DataRow _Row, string _ColName)
        {
            long Result = 0;
            if (_Row == null) return Result;
            if (!long.TryParse(_Row[_ColName].ToString(), out Result)) Result = 0;
            return Result;
        }
        public static double GetDataRowValue_Double(DataRow _Row, string _ColName)
        {
            double Result = 0;
            if (_Row == null) return Result;
            if (!double.TryParse(_Row[_ColName].ToString(), out Result)) Result = 0;
            return Result;
        }
        #endregion

        #region CONNECTION
        public static bool CheckConnectTCPIP(string _IPAddress,int _Port,out string _ErrMessage)
        {
            Socket SocketConn = null;
            try
            {
                _ErrMessage = string.Empty;

                IPAddress ipaddress = IPAddress.Parse(_IPAddress);
                IPEndPoint ipe = new IPEndPoint(ipaddress, _Port);

                SocketConn = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                SocketConn.Connect(ipe);
                return SocketConn.Connected;
            }
            catch (Exception ex)
            {
                _ErrMessage = ex.Message;
                return false;
            }
            finally
            {
                if (SocketConn != null && SocketConn.Connected)
                    SocketConn.Close();
                SocketConn = null;
            }
        }
        public static bool CheckStartServer(int _Port, out string _ErrMessage)
        {
            _ErrMessage = string.Empty;
            Socket SocketConn = null;
            try
            {
                if (SocketConn == null) SocketConn = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                SocketConn.Bind(new IPEndPoint(IPAddress.Any, _Port));
                SocketConn.Listen(0);

                SocketConn.Close();
                SocketConn = null;

                return true;
            }
            catch (Exception ex)
            {
                _ErrMessage = ex.Message;
                return false;
            }
            finally
            {
                if (SocketConn != null && SocketConn.Connected)
                    SocketConn.Close();
                SocketConn = null;
            }
        }
        private static void CloseCOM(SerialPort _COMConn, bool _currentCOMPortRemoved = false)
        {
            //if (_COMConn == null) return;
            ////if (_currentCOMPortRemoved)
            ////{
            ////    _COMConn.DtrEnable = false;
            ////    _COMConn.RtsEnable = false;
            ////    _COMConn.DiscardInBuffer();
            ////    _COMConn.DiscardOutBuffer();
            ////}
            ////else
            ////{
            ////    _COMConn.Close();
            ////}

            //if (_COMConn.IsOpen)
            //{
            //    while (_COMConn.BytesToWrite > 0) { }
            //    _COMConn.ErrorReceived -= SerialErrorReceivedEventHandler;
            //    _COMConn.DataReceived -= SerialDataReceivedEventHandler;
            //    _COMConn.DiscardInBuffer();
            //    _COMConn.Close();
            //    _COMConn = null;
            //}
        }

        public static void TestInsert()
        {
            DataTable BangDL = new DataTable();
            BangDL.Columns.Add("C1", typeof(string));
            BangDL.Columns.Add("C2", typeof(int));
            BangDL.Columns.Add("C3", typeof(double));
            BangDL.Columns.Add("C4", typeof(bool));


            BangDL.Rows.Add(new object[] { "Test", 1, 2.13, true });
            BangDL.Rows.Add(new object[] { "Test", 1, 2.13, true });
            BangDL.Rows.Add(new object[] { "Test", 1, 2.13, true });
            BangDL.Rows.Add(new object[] { "Test", 1, 2.13, true });
            BangDL.Rows.Add(new object[] { "Test", 1, 2.13, true });

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("BEGIN \n");
            string sSQL = "INSERT INTO TenBang VALUES('{0}',{1},{2},'{3}')";
            
            foreach (DataRow r in BangDL.Rows)
            {
                stringBuilder.Append(string.Format(sSQL,
                    new object[] {GetDataRowValue_String(r,"C1"),
                                  GetDataRowValue_Int(r,"C2"),
                                   GetDataRowValue_Double(r,"C3"),
                                    GetDataRowValue_Boolean(r,"C4")}) + ";\n");
                
            }
            stringBuilder.Append("END");

            Clipboard.SetText(stringBuilder.ToString());
        }
        public static string GetIPAddress()
        {
            try
            {
                var host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (var ip in host.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        return ip.ToString();
                    }
                }

                return string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }
        #endregion

        #region WAITFORM
        /// <summary>
        /// Hàm show wait form
        /// </summary>
        /// <param name="_Caption">Caption cần đổi</param>
        /// <param name="_Description">Nội dung cần đổi</param>

        #endregion
        public static void ShowCrossOrigin(List<HTuple> origin, HWindow hWindow)
        {
            try
            {
                if (origin.Count > 2)
                {
                    HOperatorSet.GenCrossContourXld(out HObject cross, origin[0], origin[1], 120, 0);
                    hWindow.SetColor("yellow");
                    hWindow.DispObj(cross);
                    hWindow.SetColor("green");
                    cross = null;
                }
            }
            catch
            {
            }


        }
        #region FORM ASCII TABLE
        /// <summary>
        /// Hiển thị bảng ASCII để người dùng lựa chọn
        /// </summary>
        /// <param name="_TextEditSetValue">Textbox nhận giá trị</param>
        public static byte[] ASCII = { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0a, 0x0b, 0x0c, 0x0d, 0x0e, 0x0f, 
            0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16, 0x17, 0x18, 0x19, 0x1a, 0x1b, 0x1c, 0x1d, 0x1e, 0x1f, 
            0x20, 0x21, 0x22, 0x23, 0x24, 0x25, 0x26, 0x27, 0x28, 0x29, 0x2a, 0x2b, 0x2c, 0x2d, 0x2e, 0x2f, 
            0x30, 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39, 0x3a, 0x3b, 0x3c, 0x3d, 0x3e, 0x3f, 
            0x40, 0x41, 0x42, 0x43, 0x44, 0x45, 0x46, 0x47, 0x48, 0x49, 0x4a, 0x4b, 0x4c, 0x4d, 0x4e, 0x4f, 
            0x50, 0x51, 0x52, 0x53, 0x54, 0x55, 0x56, 0x57, 0x58, 0x59, 0x5a, 0x5b, 0x5c, 0x5d, 0x5e, 0x5f, 
            0x60, 0x61, 0x62, 0x63, 0x64, 0x65, 0x66, 0x67, 0x68, 0x69, 0x6a, 0x6b, 0x6c, 0x6d, 0x6e, 0x6f, 
            0x70, 0x71, 0x72, 0x73, 0x74, 0x75, 0x76, 0x77, 0x78, 0x79, 0x7a, 0x7b, 0x7c, 0x7d, 0x7e, 0x7f, 
            0x80, 0x81, 0x82, 0x83, 0x84, 0x85, 0x86, 0x87, 0x88, 0x89, 0x8a, 0x8b, 0x8c, 0x8d, 0x8e, 0x8f, 
            0x90, 0x91, 0x92, 0x93, 0x94, 0x95, 0x96, 0x97, 0x98, 0x99, 0x9a, 0x9b, 0x9c, 0x9d, 0x9e, 0x9f, 
            0xa0, 0xa1, 0xa2, 0xa3, 0xa4, 0xa5, 0xa6, 0xa7, 0xa8, 0xa9, 0xaa, 0xab, 0xac, 0xad, 0xae, 0xaf, 
            0xb0, 0xb1, 0xb2, 0xb3, 0xb4, 0xb5, 0xb6, 0xb7, 0xb8, 0xb9, 0xba, 0xbb, 0xbc, 0xbd, 0xbe, 0xbf, 
            0xc0, 0xc1, 0xc2, 0xc3, 0xc4, 0xc5, 0xc6, 0xc7, 0xc8, 0xc9, 0xca, 0xcb, 0xcc, 0xcd, 0xce, 0xcf, 
            0xd0, 0xd1, 0xd2, 0xd3, 0xd4, 0xd5, 0xd6, 0xd7, 0xd8, 0xd9, 0xda, 0xdb, 0xdc, 0xdd, 0xde, 0xdf, 
            0xe0, 0xe1, 0xe2, 0xe3, 0xe4, 0xe5, 0xe6, 0xe7, 0xe8, 0xe9, 0xea, 0xeb, 0xec, 0xed, 0xee, 0xef, 
            0xf0, 0xf1, 0xf2, 0xf3, 0xf4, 0xf5, 0xf6, 0xf7, 0xf8, 0xf9, 0xfa, 0xfb, 0xfc, 0xfd, 0xfe, 0xff };
        /// <summary>
        /// Chuyển đổi từ byte sang HEX
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        static string ToHex(byte[] bytes)
        {
            char[] c = new char[bytes.Length * 2];

            byte b;

            for (int bx = 0, cx = 0; bx < bytes.Length; ++bx, ++cx)
            {
                b = ((byte)(bytes[bx] >> 4));
                c[cx] = (char)(b > 9 ? b - 10 + 'A' : b + '0');

                b = ((byte)(bytes[bx] & 0x0F));
                c[++cx] = (char)(b > 9 ? b - 10 + 'A' : b + '0');
            }

            string result = new string(c);
            return "0x" + result;
        }
        /// <summary>
        /// Khởi tạo thư viện dữ liệu ASCII
        /// </summary>
        public static void GenerateDicASCII()
        {
            if (GlobVar.DicASCII != null) return;
            GlobVar.DicASCII = new Dictionary<string, byte>();
            for (int i = 0; i < ASCII.Length; i++)
            {
                GlobVar.DicASCII.Add(ToHex(new byte[] { ASCII[i] }), ASCII[i]);
            }
        }
        #endregion
    }
}
