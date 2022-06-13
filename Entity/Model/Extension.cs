using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Entities.Model
{
        public static class Extension
        {
            private static Random random = new Random();
            public static object GetPropValue(object obj, string propName)
            {
                string[] nameParts = propName.Split('.');
                if (nameParts.Length == 1)
                {
                    var property = obj.GetType().GetRuntimeProperties().FirstOrDefault(p => string.Equals(p.Name, propName, StringComparison.OrdinalIgnoreCase));
                    return property?.GetValue(obj);
                }

                foreach (string part in nameParts)
                {
                    if (obj == null) { return null; }

                    Type type = obj.GetType();
                    PropertyInfo info = type.GetProperty(part);
                    if (info == null) { return null; }

                    obj = info.GetValue(obj, null);
                }
                return obj;
            }
            public static string DrReadToString(this IDataRecord dr)
            {
                var rowValues = new object[dr.FieldCount];
                dr.GetValues(rowValues);
                var sb = new StringBuilder();
                for (var keyValueCounter = 0; keyValueCounter < rowValues.Length; keyValueCounter++)
                {
                    sb.AppendFormat("{0}:{1}", dr.GetName(keyValueCounter),
                        rowValues[keyValueCounter] is DBNull ? string.Empty : rowValues[keyValueCounter])
                      .AppendLine();
                }
                return sb.ToString();
            }

            public static string ConvertToCooardinateLonDD(this string coord)
            {
                string c = coord;

                string[] data = c.Split('.');
                if (data[1].Length > 4)
                {
                    data[1] = data[1].Substring(0, 4);
                }
                string value = data[0] + (data[1].ConvertToInteger() * 60).ToString();
                string degree = "";

                if (data[0].ConvertToInteger() >= 0)
                {
                    degree = "E";
                }
                else
                {
                    degree = "W";
                }


                return "0" + value.Substring(0, 4) + "." + value.Substring(4, value.Length - 4) + degree;

            }

            public static string ConvertToCooardinateLatDD(this string coord)
            {
                string c = coord;

                string[] data = c.Split('.');
                if (data[1].Length > 4)
                {
                    data[1] = data[1].Substring(0, 4);
                }
                string value = data[0] + (data[1].ConvertToInteger() * 60).ToString();
                string degree = "";


                if (data[0].ConvertToInteger() >= 0)
                {
                    degree = "N";
                }
                else
                {
                    degree = "S";
                }

                return value.Substring(0, 4) + "." + value.Substring(4, value.Length - 4) + degree;

            }
            public static int ConvertFromHex(this string value)
            {
                return int.Parse(value, NumberStyles.HexNumber);
            }

            public static string ConvertToString(this object value)
            {
                if (value == null)
                    return "";
                return value.ToString();
            }

            public static string ConvertToCooardinate(this string coord)
            {
                string c = coord.Substring(0, coord.Length - 1);
                string a;
                c = c.Replace(".", "");
                if (c.StartsWith("0"))
                {
                    c = c.Substring(3, c.Length - 3);
                    a = coord.Substring(0, 3);
                }
                else
                {
                    c = c.Substring(2, c.Length - 2);
                    a = coord.Substring(0, 2);
                }
                try
                {
                    string DecimalValue = (int.Parse(c) / 60).ToString();
                    DecimalValue = DecimalValue.PadLeft(4, '0');
                    return a + "." + DecimalValue;
                }
                catch (Exception)
                {
                    return "";
                }
            }

            public static object ConvertToValue(this string value, string type)
            {
                if (type == "lat" || type == "lng")
                {
                    return value.Substring(1).ConvertToFloat();
                }
                else if (type == "int")
                {
                    return value.ConvertToInteger();
                }
                else if (type == "long")
                {
                    return value.ConvertToLong();
                }
                else if (type == "hex")
                {
                    return value.ConvertFromHex();
                }
                else if (type == "decimal")
                {
                    return value.ConvertToDecimal(2);
                }
                else if (type == "mile")
                {
                    return (int)(value.ConvertToInteger() * 1.852);
                }
                else if (type == "sensor")
                {
                    string binarystring = string.Join(string.Empty,
                      value.Select(
                        c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')
                      )
                    );
                    return binarystring;
                }
                else if (type == "analog")
                {
                    return value;
                }
                else if (type == "distance")
                {
                    int _value = value.ConvertFromHex();
                    if (_value <= 0) return _value;
                    return _value / 10;
                }
                else if (type == "alarm")
                {
                    return value;
                }
                else if (type == "datetime")
                {
                    DateTime dt = value.ConvertToDate("ddMMyyHHmmss");
                    DateTime lt = dt.ToLocalTime();
                    return lt;
                }
                else
                {
                    return value;
                }
            }

            public static DateTime ConvertToDate(this string value, string format)
            {
                if (String.IsNullOrEmpty(value))
                    return DateTime.Now;

                DateTime oVal;

                if (DateTime.TryParseExact(
                                            value.Trim(),
                                            format,
                                            CultureInfo.InvariantCulture,
                                            DateTimeStyles.None,
                                            out oVal
                                           ))
                {
                    if (oVal == DateTime.MinValue)
                    {
                        return new DateTime();
                    }
                    else
                    {
                        return oVal;
                    }
                }
                else
                {
                    return DateTime.Now;
                }
            }

            public static DateTime ConvertToLocalTime(this string value)
            {
                DateTime dt = value.ConvertToDate("ddMMyyHHmmss");
                DateTime lt = dt.ToLocalTime();
                return lt;
            }
            public static int ConvertToInteger(this object value)
            {
                if (value == null)
                    return 0;

                int oVal;

                if (int.TryParse(value.ToString(), out oVal))
                {
                    return oVal;
                }
                else
                {
                    return 0;
                }
            }


            public static short ConvertToShort(this object value)
            {
                if (value == null)
                    return 0;

                short oVal;

                if (short.TryParse(value.ToString(), out oVal))
                {
                    return oVal;
                }
                else
                {
                    return 0;
                }
            }


            public static int? ConvertToIntegerNullable(this object value)
            {
                if (value == null)
                    return null;

                int oVal;

                if (int.TryParse(value.ToString(), out oVal))
                {
                    return oVal;
                }
                else
                {
                    return 0;
                }
            }

            public static long ConvertToLong(this object value)
            {
                if (value == null)
                    return 0;

                long oVal;

                if (long.TryParse(value.ToString(), out oVal))
                {
                    return oVal;
                }
                else
                {
                    return 0;
                }

            }
            public static long? ConvertToLongNullable(this object value)
            {
                if (value == null)
                    return null;

                long oVal;

                if (long.TryParse(value.ToString(), out oVal))
                {
                    return oVal;
                }
                else
                {
                    return 0;
                }
            }
            public static short? ConvertToShortNullable(this object value)
            {
                if (value == null)
                    return null;

                short oVal;

                if (short.TryParse(value.ToString(), out oVal))
                {
                    return oVal;
                }
                else
                {
                    return 0;
                }
            }

            public static string StripHTML(this string inputString)
            {
                string HTML_TAG_PATTERN = "<.*?>";
                return Regex.Replace
                  (inputString, HTML_TAG_PATTERN, string.Empty);
            }
            public static string FirstChar(this string value, int limit)
            {

                if (value.Length >= limit)
                {
                    return value.Substring(0, limit);
                }
                else
                {
                    return value.Substring(0, value.Length);
                }
            }



            public static bool ConvertToBoolean(this object value)
            {
                if (value == null)
                    return false;

                if (value.ToString() == "true" || value.ToString() == "True" || value.ToString() == "TRUE" || value.ToString() == "1")
                    return true;

                return false;
            }

            public static bool IsNumeric(this string value)
            {
                if (string.IsNullOrEmpty(value))
                    return false;

                float oVal;
                value = value.Replace('.', ',');
                if (float.TryParse(value, out oVal))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public static float ConvertToFloat(this object value)
            {
                if (value == null)
                    return 0;

                float oVal;
                value = value.ToString().Trim().Replace('.', ',');
                if (float.TryParse(value.ToString(), out oVal))
                {
                    return oVal;
                }
                else
                {
                    return 0;
                }
            }
            public static double ConvertToDouble(this object value)
            {
                if (value == null)
                    return 0;

                double oVal;
                value = value.ToString().Trim().Replace('.', ',');
                if (double.TryParse(value.ToString(), out oVal))
                {
                    return Math.Round(oVal, 2);
                }
                else
                {
                    return 0;
                }
            }
            public static DateTime FromUnixTime(this long value)
            {
                System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
                dtDateTime = dtDateTime.AddSeconds(Math.Round((double)value / 1000)).ToLocalTime();
                return dtDateTime;
            }

            public static decimal ConvertToDecimal(this string value, int Precision = 2)
            {

                value = value.Replace(".", ",");

                if (string.IsNullOrEmpty(value))
                    return 0m;

                decimal oVal;

                if (decimal.TryParse(value, out oVal))
                {
                    return decimal.Round(oVal, Precision);
                }
                else
                {
                    return 0m;
                }
            }
            public static Guid ConvertToGuid(this object value)
            {
                if (value == null)
                    return new Guid();

                Guid oVal;

                if (Guid.TryParse(value.ToString(), out oVal))
                {
                    return oVal;
                }
                else
                {
                    return new Guid();
                }
            }
            public static bool IsValidEmail(this string email)
            {
                try
                {
                    var addr = new System.Net.Mail.MailAddress(email);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            public static string UTF82ASCII(this string text)
            {
                Encoding utf8 = Encoding.UTF8;
                byte[] encodedBytes = utf8.GetBytes(text);
                byte[] convertedBytes = Encoding.Convert(Encoding.UTF8, Encoding.ASCII, encodedBytes);
                Encoding ascii = Encoding.ASCII;
                return ascii.GetString(convertedBytes);
            }

            public static string ToFloatString(this double val)
            {
                return val.ToString("0.00");
            }



            public static bool IsPhoneNumber(this string number)
            {
                if (number.Length == 9 || number.Length == 10)
                {
                    return number.ConvertToInteger() == 0 ? false : true;
                }

                return false;
            }

            public static double Round(this double number, int Places = 2)
            {
                return Math.Round(number, Places);
            }

            public static float Round(this float number, int Places = 2)
            {
                return (float)Math.Round((double)number, Places);
            }

            public static DateTime ConvertToDateTime(this object value, string format = "yyyy-MM-dd HH:mm:ss")
            {
                if (value == null)
                    return DateTime.Now;

                DateTime oVal;

                string[] formats = new string[]
                {
                 "dd-MM-yyyy",
                "dd/MM/yyyy HH:mm",
                "dd/M/yyyy HH:mm",
                "d/M/yyyy HH:mm",
                "d/MM/yyyy HH:mm",
                "dd/MM/yyyy HH:mm:ss",
                "dd.MM.yyyy HH:mm:ss",
                "dd/M/yyyy HH:mm:ss",
                "d/M/yyyy HH:mm:ss",
                "d/MM/yyyy HH:mm:ss",
                "yyyy-MM-dd HH:mm:ss",
                "yyyy-MM-dd",
                "dd/MM/yyyy",
                "dd.MM.yyyy",
                "dd/M/yyyy",
                "d/M/yyyy",
                "d/MM/yyyy",
                "ddMMyyyy",
                "ddMMyy",
                "dMMyy",
                format
                };

                if (DateTime.TryParseExact(value.ToString().Trim(), formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out oVal))
                {
                    return oVal;
                }
                else
                {
                    return DateTime.MinValue;
                }
            }

            public static DateTime ToDateTimeNew(this object value)
            {
                if (value == null || string.IsNullOrEmpty(value.ToString()))
                    return DateTime.MinValue;

                DateTime oval = Convert.ToDateTime(value);
                return oval;
            }


            public static bool IsValidDate(this string val)
            {
                DateTime tTarihi;
                string[] format = new string[]
                {
                "dd/MM/yyyy HH:mm",
                "dd/M/yyyy HH:mm",
                "d/M/yyyy HH:mm",
                "d/MM/yyyy HH:mm",
                "dd/MM/yyyy HH:mm:ss",
                "dd/M/yyyy HH:mm:ss",
                "d/M/yyyy HH:mm:ss",
                "d/MM/yyyy HH:mm:ss",
                "dd/MM/yyyy",
                "dd/M/yyyy",
                "d/M/yyyy",
                "d/MM/yyyy"
                };
                return DateTime.TryParseExact(val.Trim(), format, CultureInfo.InvariantCulture, DateTimeStyles.None, out tTarihi);
            }

            public static T ConvertTo<T>(this IConvertible value)
            {
                return (T)Convert.ChangeType(value, typeof(T));
            }

            public static string ConvertToDateTimeToString(this DateTime value)
            {
                if (DateTime.MinValue == value)
                    return "";
                return value.ToShortDateString();
            }
            public static string ConvertToLongDateTimeToString(this DateTime value)
            {
                if (DateTime.MinValue == value)
                    return "";

                return value.ToString();
            }
            public static DateTime? ToDateTimeNullable(this object value)
            {
                if (value == null || string.IsNullOrEmpty(value.ToString()))
                    return null;

                DateTime oval = Convert.ToDateTime(value);
                return oval;
            }

            public static string ConvertToNullableDateTimeToString(this DateTime? value)
            {
                if (value != null)
                {
                    if (DateTime.MinValue == value)
                        return "";
                    return ((DateTime)value).ToShortDateString();
                }
                else
                {
                    return "";
                }
            }
        }

    }

