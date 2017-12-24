using AutumnBox.Basic.Connection;
using AutumnBox.Basic.Devices;
using AutumnBox.Basic.Executer;
using AutumnBox.Basic.Util;
using AutumnBox.GUI.Util;
using AutumnBox.Support;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace AutumnBox.ConsoleTester
{
    class Fuck {
        public string fucl;
        public int fs;
        public int sadasas;
    }
    class Program
    {
        public readonly static DeviceBasicInfo mi6 = new DeviceBasicInfo()
        {
            Serial = new Serial("af0fe186"),
            Status = DeviceStatus.Poweron,
        };
        public readonly static DeviceBasicInfo mi6net = new DeviceBasicInfo()
        {
            Serial = new Serial("192.168.0.12:5555"),
            Status = DeviceStatus.Poweron,
        };
        public readonly static DeviceBasicInfo mi4 = new DeviceBasicInfo()
        {
            Serial = new Serial("9dd1b490"),
            Status = DeviceStatus.Poweron,
        };
        unsafe static void Main(string[] args)
        {
            //Type stype = new Fuck().GetType();
            //FieldInfo[] fields = stype.GetFields(BindingFlags.Public | BindingFlags.Instance);
            //Logger.D(fields.Length.ToString());
            //var fucker = new Fuck();
            //var result = new FieldsFinder<Fuck, string>().FindFrom(fucker);
            //FieldInfo[] fields = fucker.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);
            //Console.WriteLine($"find {fields.Count()} fields");
            //foreach (FieldInfo info in fields)
            //{
            //    Console.WriteLine($"name {info.Name} type {info.FieldType.Name} value {info.GetValue(fucker) ?? "null"}");
            //}
            //var result = from field in fields
            //             where field.FieldType == typeof(String)
            //             select (string)field.GetValue(fucker);
            //Console.WriteLine(result.Count());
            Console.ReadKey();
        }
        public static void WriteWithColor(Action a, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            a();
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
