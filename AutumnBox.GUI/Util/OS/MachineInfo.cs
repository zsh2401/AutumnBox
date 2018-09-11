/* =============================================================================*\
*
* Filename: MachineInfo
* Description: 
*
* Version: 1.0
* Created: 2017/11/28 23:41:58 (UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using System;
using System.IO;
using System.Management;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
/*Code from http://blog.csdn.net/qq_23833037/article/details/78304738*/
namespace AutumnBox.GUI.Util.OS
{
    /// <summary>
    /// 此类用于获取当前主机的相关信息
    /// </summary>
    internal class MachineInfo
    {
#if NEED_MACHINE_INFO
        /*
         * 提供对大量管理信息和管理事件集合的访问，这些信息和事件是与根据 Windows 管理规范 (WMI) 结构对系统、设备和应用程序设置检测点有关的。
         * 应用程序和服务可以使用从 ManagementObjectSearcher 和 ManagementQuery 派生的类，查询感兴趣的管理信息（例如在磁盘上还剩多少可用空间、当前 CPU 利用率是多少、某一应用程序正连接到哪一数据库等等）；
         * 或者应用程序和服务可以使用 ManagementEventWatcher 类预订各种管理事件。这些可访问的数据可以来自分布式环境中托管的和非托管的组件。
         * 
         * 获取主机设备信息时，需要使用到。Management命名空间
         * 在项目-》添加引用....里面引用System.Management
         * ************************************************************************************
         * 
         * ManagementObjectSearcher类 //获取主机所有信息的集合
         * 基于指定的查询检索管理对象的集合。 此类是用于检索管理信息的较为常用的入口点之一。 
         * 例如，它可以用于枚举系统中的所有磁盘驱动器、网络适配器、进程及更多管理对象，或者用于查询所有处于活动状态的网络连接以及暂停的服务等。
         * 在实例化之后，此类的实例可以接受在System.Management.ObjectQuery 或其派生类中表示的 WMI 查询作为输入，并且还可以选择接受一个 System.Management.ManagementScope（表示执行查询时所在的
         * 
         * ManagementObjectCollection//从ManagementObjectSearcher获取到的主机设备集合进行管理
         * 
         * ManagementObject//表示WMI的实例，获取或者使用
         * 
         * ManagementClass表示一个通用信息模型的管理类，这个管理类是WMI类。
         * WMI类是Windows 2K/XP管理系统的核心；对于其他的Win32操作系统，WMI是一个有用的插件。
         * WMI以CIMOM为基础，CIMOM即公共信息模型对象管理器（Common Information Model Object Manager）
         * 
         * ManagementClass类//此类的成员可以访问 WMI 数据
         * Win32_LogicalDisk, ，该类型可表示一个磁盘驱动器
         * Win32_Process, ，它表示的进程 Notepad.exe 等。 
         * 此类的成员可以访问 WMI 数据，使用一个特定的 WMI 类路径(如：Win32_LogicalDisk或者Win32_Process之类)
         * 
         */
        /// <summary>
        /// 获取本地IP,多个IP
        /// </summary>
        /// <returns></returns>
        public string[] GetIPAddress()
        {
            string hostName = Dns.GetHostName();//获取主机名
            IPAddress[] addresses = Dns.GetHostAddresses(hostName);//解析主机IP地址

            string[] IP = new string[addresses.Length];
            for (int i = 0; i < addresses.Length; i++)
            {
                IP[i] = addresses[i].ToString().Trim();//每个IP保存在数组列表中
            }

            return IP;
        }

        /// <summary>
        /// 获取公网IP
        /// </summary>
        /// <returns></returns>
        public string[] GetExtenalAddress()
        {
            string[] IP = new string[] { "未获取到公网ip" };

            try
            {
                WebRequest WR = WebRequest.Create(@"http://ip.qq.com/");
                WebResponse WS = WR.GetResponse();
                Stream s = WS.GetResponseStream();
                StreamReader sr = new StreamReader(s, Encoding.UTF8);

                Match m = Regex.Match(sr.ReadToEnd(), @"((25[0-5]|2[0-4]\d|[01]?\d?\d)\.){3}(25[0-5]|2[0-4]\d|[01]?\d?\d)", RegexOptions.None);
                if (m.Groups[0].Success)
                {
                    IP[0] = m.Groups[0].Value.ToString().Trim();
                }

                WS.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
            }

            return IP;
        }

        #region ManagementObjectSearcher
        /// <summary>
        /// 获取本机MAC
        /// </summary>
        /// <returns></returns>
        public string GetLocatMac()
        {
            string mac = null;
            try
            {
                ManagementObjectSearcher Mac = new ManagementObjectSearcher("select * from Win32_NetworkAdapterConfiguration");//搜索主机设备对象
                ManagementObjectCollection queryCollection = Mac.Get();//管理获取到的主机设备的集合
                foreach (ManagementObject mo in queryCollection)
                {
                    if (mo["IPEnabled"].ToString() == "True")
                    {
                        mac = mo["MacAddress"].ToString().Trim();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
            }

            return mac;
        }

        /// <summary>
        /// 获取主板序列号
        /// </summary>
        /// <returns></returns>
        public string GetBLOSSerialNumber()
        {
            string sBIOSSerialNumber = "";
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from Win32_BIOS");
                foreach (ManagementObject mo in searcher.Get())
                {
                    sBIOSSerialNumber = mo["SerialNumber"].ToString().Trim();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
            }

            return sBIOSSerialNumber;
        }

        /// <summary>
        /// 获取CPU序列号
        /// </summary>
        /// <returns></returns>
        public string GetCPUSerialNumber()
        {
            string Cpu = "";
            try
            {
                ManagementObjectSearcher MOS = new ManagementObjectSearcher("select * from Win32_Processor");
                foreach (ManagementObject mo in MOS.Get())
                {
                    Cpu = mo["ProcessorId"].ToString().Trim();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
            }

            return Cpu;
        }

        /// <summary>
        /// 获取硬盘序列号
        /// </summary>
        /// <returns></returns>
        public string GetCalicheNumber()
        {
            string Caliche = "";
            try
            {
                ManagementObjectSearcher MOS = new ManagementObjectSearcher("select * from Win32_PhysicalMedia");
                foreach (ManagementObject mo in MOS.Get())
                {
                    Caliche = mo["SerialNumber"].ToString().Trim();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
            }

            return Caliche;
        }

        /// <summary>
        /// 获取网卡地址
        /// </summary>
        /// <returns></returns>
        public string GetNetworkCarNumber()
        {
            string NetworkCar = "";
            try
            {
                ManagementObjectSearcher MOS = new ManagementObjectSearcher("select * from Win32_NetworkAdapter where ((MACAddress Is Not NULL) and (Manufacturer <> 'Microsoft'))");
                ManagementObjectCollection MOC = MOS.Get();

                foreach (ManagementObject mo in MOC)
                {
                    NetworkCar = mo["MACAddress"].ToString().Trim();
                }
            }
            catch (Exception e) { Console.WriteLine(e.Message.ToString()); }

            return NetworkCar;
        }
        #endregion


        #region ManagementClass
        /// <summary>
        /// 获取CPU编号WMI
        /// </summary>
        /// <returns></returns>
        public string GetCPUNumberWMI()
        {
            string CPUNumber = "";
            try
            {
                ManagementClass MC = new ManagementClass("Win32_Processor");
                ManagementObjectCollection MOC = MC.GetInstances();
                foreach (ManagementObject mo in MOC)
                {
                    CPUNumber = mo.Properties["ProcessorId"].Value.ToString();
                }
            }
            catch (Exception e) { Console.WriteLine(e.Message.ToString()); }

            return CPUNumber;
        }

        /// <summary>
        /// 获取硬盘编号WMI
        /// </summary>
        /// <returns></returns>
        public string GetCailcheNumberWMI()
        {
            string Cailche = "";
            try
            {
                ManagementClass mc = new ManagementClass("Win32_DiskDrive");
                ManagementObjectCollection moc = mc.GetInstances();

                foreach (ManagementObject mo in moc)
                {
                    Cailche = mo.Properties["Model"].Value.ToString().Trim();
                    break;
                }
            }
            catch (Exception e) { Console.WriteLine(e.Message.ToString()); }

            return Cailche;
        }

        /// <summary>
        /// 获取网卡硬件WMI
        /// </summary>
        /// <returns></returns>
        public string GetNetworkcardNumberWMI()
        {
            string Networkcard = "";
            try
            {
                ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    if ((bool)mo["IPEnabled"] == true)
                    {
                        Networkcard = mo.Properties["MacAddress"].Value.ToString().Trim();
                        break;
                    }
                }
            }
            catch (Exception e) { Console.WriteLine(e.Message.ToString()); }

            return Networkcard;
        }

        /// <summary>
        /// 获取IP地址WMI
        /// </summary>
        /// <returns></returns>
        public string GetIPAddressWMI()
        {
            string[] IP = new string[2];
            try
            {
                ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    if ((bool)mo["IPEnabled"] == true)
                    {
                        IP = (string[])(mo.Properties["IpAddress"].Value);
                        break;
                    }
                }
            }
            catch (Exception e) { Console.WriteLine(e.Message.ToString()); }

            return IP[0];
        }
        #endregion

        /// <summary>
        /// 操作系统的登陆用户名
        /// </summary>
        /// <returns></returns>
        public string GetUserName()
        {
            string UserName = Environment.UserName;
            return UserName;
        }

        /// <summary>
        /// 操作系统类型
        /// </summary>
        /// <returns></returns>
        public string GetSystemType()
        {
            string st = "";
            try
            {
                ManagementClass mc = new ManagementClass("Win32_ComputerSystem");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    st = mo["SystemType"].ToString().Trim();
                }
            }
            catch (Exception e) { Console.WriteLine(e.Message.ToString()); }

            return st;
        }

        /// <summary>
        /// 物理内存
        /// </summary>
        /// <returns></returns>
        public string GetRAM()
        {
            string RAM = "";
            try
            {
                ManagementClass mc = new ManagementClass("Win32_ComputerSystem");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    RAM = mo["TotalPhysicalMemory"].ToString().Trim();
                }
            }
            catch (Exception e) { Console.WriteLine(e.Message.ToString()); }

            return RAM;
        }

        /// <summary>
        /// 显卡设备
        /// </summary>
        /// <returns></returns>
        public string GetVideoPNPID()
        {
            string PNP = "";
            try
            {
                //ManagementClass mc = new ManagementClass("");
                ManagementObjectSearcher mos = new ManagementObjectSearcher("Select * from Win32_VideoController");
                ManagementObjectCollection moc = mos.Get();

                foreach (ManagementObject mo in moc)
                {
                    PNP = mo["PNPDeviceID"].ToString();
                }
            }
            catch (Exception e) { Console.WriteLine(e.Message.ToString()); }

            return PNP;
        }

        /// <summary>
        /// 声卡设备
        /// </summary>
        /// <returns></returns>
        public string GetSoundCard()
        {
            string st = "";
            try
            {
                ManagementObjectSearcher mos = new ManagementObjectSearcher("Select * from Win32_SoundDevice");
                ManagementObjectCollection moc = mos.Get();
                foreach (ManagementObject mo in moc)
                {
                    st = mo["PNPDeviceID"].ToString().Trim();
                }
            }
            catch (Exception e) { Console.WriteLine(e.Message.ToString()); }

            return st;
        }

        /// <summary>
        /// 获取CPU版本信息
        /// </summary>
        /// <returns></returns>
        public string GetCPUVersionInformation()
        {
            string st = "";
            try
            {
                ManagementObjectSearcher mos = new ManagementObjectSearcher("Select * from Win32_Processor");
                foreach (ManagementObject mo in mos.Get())
                {
                    st = mo["Version"].ToString();
                }
            }
            catch (Exception e) { Console.WriteLine(e.Message.ToString()); }

            return st;
        }

        /// <summary>
        /// 获取CPU名称信息
        /// </summary>
        /// <returns></returns>
        public string GetCPUName()
        {
            string st = "";
            try
            {
                ManagementObjectSearcher mos = new ManagementObjectSearcher("Select * from Win32_Processor");
                foreach (ManagementObject mo in mos.Get())
                {
                    st = mo["Name"].ToString().Trim();
                }
            }
            catch (Exception e) { Console.WriteLine(e.Message.ToString()); }

            return st;
        }

        /// <summary>
        /// 获取CPU制造厂商
        /// </summary>
        /// <returns></returns>
        public string GetCPUManufacturer()
        {
            string st = "";
            try
            {
                ManagementObjectSearcher mos = new ManagementObjectSearcher("select * from Win32_Processor");
                foreach (ManagementObject mo in mos.Get())
                {
                    st = mo["Manufacturer"].ToString().Trim();
                }
            }
            catch (Exception e) { Console.WriteLine(e.Message.ToString()); }

            return st;
        }

        /// <summary>
        /// 主板制造厂商
        /// </summary>
        /// <returns></returns>
        public string GetBoardMotherboard()
        {
            SelectQuery query = new SelectQuery("select * from Win32_BaseBoard");
            ManagementObjectSearcher mos = new ManagementObjectSearcher(query);
            ManagementObjectCollection.ManagementObjectEnumerator data = mos.Get().GetEnumerator();

            data.MoveNext();
            ManagementBaseObject board = data.Current;
            return board.GetPropertyValue("Manufacturer").ToString();
        }

        /// <summary>
        /// 主板编号
        /// </summary>
        /// <returns></returns>
        public string GetBoardID()
        {
            string st = "";
            ManagementObjectSearcher mos = new ManagementObjectSearcher("select * from Win32_BaseBoard");
            foreach (ManagementObject mo in mos.Get())
            {
                st = mo["SerialNumber"].ToString();
            }

            return st;
        }

        /// <summary>
        /// 主板型号
        /// </summary>
        /// <returns></returns>
        public string GetModelID()
        {
            string st = "";
            ManagementObjectSearcher mos = new ManagementObjectSearcher("select * from Win32_BaseBoard");
            foreach (ManagementObject mo in mos.Get())
            {
                st = mo["Product"].ToString();
            }
            return st;
        }
#endif
    }


    #region 学习资料
    /*
    为了获取硬件信息，你还需要创建一个ManagementObjectSearcher 对象。
    ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from " + Key);
    // 硬件 
    Win32_Processor, // CPU 处理器 
    Win32_PhysicalMemory, // 物理内存条 
    Win32_Keyboard, // 键盘 
    Win32_PointingDevice, // 点输入设备，包括鼠标。 
    Win32_FloppyDrive, // 软盘驱动器 
    Win32_DiskDrive, // 硬盘驱动器 
    Win32_CDROMDrive, // 光盘驱动器 
    Win32_BaseBoard, // 主板 
    Win32_BIOS, // BIOS 芯片 
    Win32_ParallelPort, // 并口 
    Win32_SerialPort, // 串口 
    Win32_SerialPortConfiguration, // 串口配置 
    Win32_SoundDevice, // 多媒体设置，一般指声卡。 
    Win32_SystemSlot, // 主板插槽 (ISA & PCI & AGP) 
    Win32_USBController, // USB 控制器 
    Win32_NetworkAdapter, // 网络适配器 
    Win32_NetworkAdapterConfiguration, // 网络适配器设置 
    Win32_Printer, // 打印机 
    Win32_PrinterConfiguration, // 打印机设置 
    Win32_PrintJob, // 打印机任务 
    Win32_TCPIPPrinterPort, // 打印机端口 
    Win32_POTSModem, // MODEM 
    Win32_POTSModemToSerialPort, // MODEM 端口 
    Win32_DesktopMonitor, // 显示器 
    Win32_DisplayConfiguration, // 显卡 
    Win32_DisplayControllerConfiguration, // 显卡设置 
    Win32_VideoController, // 显卡细节。 
    Win32_VideoSettings, // 显卡支持的显示模式。 


    // 操作系统 
    Win32_TimeZone, // 时区 
    Win32_SystemDriver, // 驱动程序 
    Win32_DiskPartition, // 磁盘分区 
    Win32_LogicalDisk, // 逻辑磁盘 
    Win32_LogicalDiskToPartition, // 逻辑磁盘所在分区及始末位置。 
    Win32_LogicalMemoryConfiguration, // 逻辑内存配置 
    Win32_PageFile, // 系统页文件信息 
    Win32_PageFileSetting, // 页文件设置 
    Win32_BootConfiguration, // 系统启动配置 
    Win32_ComputerSystem, // 计算机信息简要 
    Win32_OperatingSystem, // 操作系统信息 
    Win32_StartupCommand, // 系统自动启动程序 
    Win32_Service, // 系统安装的服务 
    Win32_Group, // 系统管理组 
    Win32_GroupUser, // 系统组帐号 
    Win32_UserAccount, // 用户帐号 
    Win32_Process, // 系统进程 
    Win32_Thread, // 系统线程 
    Win32_Share, // 共享 
    Win32_NetworkClient, // 已安装的网络客户端 
    Win32_NetworkProtocol, // 已安装的网络协议 

    上面代码的Key是一个将被对应正确的数据填入的值。例如，获取CPU的信息，就需要把Key值设成Win32_Processor。所有Key可能的值，列举如下：

    Win32_1394Controller
    Win32_1394ControllerDevice
    Win32_Account
    Win32_AccountSID
    Win32_ACE
    Win32_ActionCheck
    Win32_AllocatedResource
    Win32_ApplicationCommandLine
    Win32_ApplicationService
    Win32_AssociatedBattery
    Win32_AssociatedProcessorMemory
    Win32_BaseBoard
    Win32_BaseService
    Win32_Battery
    Win32_Binary
    Win32_BindImageAction
    Win32_BIOS
    Win32_BootConfiguration
    Win32_Bus
    Win32_CacheMemory
    Win32_CDROMDrive
    Win32_CheckCheck
    Win32_CIMLogicalDeviceCIMDataFile
    Win32_ClassicCOMApplicationClasses
    Win32_ClassicCOMClass
    Win32_ClassicCOMClassSetting
    Win32_ClassicCOMClassSettings
    Win32_ClassInfoAction
    Win32_ClientApplicationSetting
    Win32_CodecFile
    Win32_COMApplication
    Win32_COMApplicationClasses
    Win32_COMApplicationSettings
    Win32_COMClass
    Win32_ComClassAutoEmulator
    Win32_ComClassEmulator
    Win32_CommandLineAccess
    Win32_ComponentCategory
    Win32_ComputerSystem
    Win32_ComputerSystemProcessor
    Win32_ComputerSystemProduct
    Win32_COMSetting
    Win32_Condition
    Win32_CreateFolderAction
    Win32_CurrentProbe
    Win32_DCOMApplication
    Win32_DCOMApplicationAccessAllowedSetting
    Win32_DCOMApplicationLaunchAllowedSetting
    Win32_DCOMApplicationSetting
    Win32_DependentService
    Win32_Desktop
    Win32_DesktopMonitor
    Win32_DeviceBus
    Win32_DeviceMemoryAddress
    Win32_DeviceSettings
    Win32_Directory
    Win32_DirectorySpecification
    Win32_DiskDrive
    Win32_DiskDriveToDiskPartition
    Win32_DiskPartition
    Win32_DisplayConfiguration
    Win32_DisplayControllerConfiguration
    Win32_DMAChannel
    Win32_DriverVXD
    Win32_DuplicateFileAction
    Win32_Environment
    Win32_EnvironmentSpecification
    Win32_ExtensionInfoAction
    Win32_Fan
    Win32_FileSpecification
    Win32_FloppyController
    Win32_FloppyDrive
    Win32_FontInfoAction
    Win32_Group
    Win32_GroupUser
    Win32_HeatPipe
    Win32_IDEController
    Win32_IDEControllerDevice
    Win32_ImplementedCategory
    Win32_InfraredDevice
    Win32_IniFileSpecification
    Win32_InstalledSoftwareElement
    Win32_IRQResource
    Win32_Keyboard
    Win32_LaunchCondition
    Win32_LoadOrderGroup
    Win32_LoadOrderGroupServiceDependencies
    Win32_LoadOrderGroupServiceMembers
    Win32_LogicalDisk
    Win32_LogicalDiskRootDirectory
    Win32_LogicalDiskToPartition
    Win32_LogicalFileAccess
    Win32_LogicalFileAuditing
    Win32_LogicalFileGroup
    Win32_LogicalFileOwner
    Win32_LogicalFileSecuritySetting
    Win32_LogicalMemoryConfiguration
    Win32_LogicalProgramGroup
    Win32_LogicalProgramGroupDirectory
    Win32_LogicalProgramGroupItem
    Win32_LogicalProgramGroupItemDataFile
    Win32_LogicalShareAccess
    Win32_LogicalShareAuditing
    Win32_LogicalShareSecuritySetting
    Win32_ManagedSystemElementResource
    Win32_MemoryArray
    Win32_MemoryArrayLocation
    Win32_MemoryDevice
    Win32_MemoryDeviceArray
    Win32_MemoryDeviceLocation
    Win32_MethodParameterClass
    Win32_MIMEInfoAction
    Win32_MotherboardDevice
    Win32_MoveFileAction
    Win32_MSIResource
    Win32_networkAdapter
    Win32_networkAdapterConfiguration
    Win32_networkAdapterSetting
    Win32_networkClient
    Win32_networkConnection
    Win32_networkLoginProfile
    Win32_networkProtocol
    Win32_NTEventlogFile
    Win32_NTLogEvent
    Win32_NTLogEventComputer
    Win32_NTLogEventLog
    Win32_NTLogEventUser
    Win32_ODBCAttribute
    Win32_ODBCDataSourceAttribute
    Win32_ODBCDataSourceSpecification
    Win32_ODBCDriverAttribute
    Win32_ODBCDriverSoftwareElement
    Win32_ODBCDriverSpecification
    Win32_ODBCSourceAttribute
    Win32_ODBCTranslatorSpecification
    Win32_OnBoardDevice
    Win32_OperatingSystem
    Win32_OperatingSystemQFE
    Win32_OSRecoveryConfiguration
    Win32_PageFile
    Win32_PageFileElementSetting
    Win32_PageFileSetting
    Win32_PageFileUsage
    Win32_ParallelPort
    Win32_Patch
    Win32_PatchFile
    Win32_PatchPackage
    Win32_PCMCIAController
    Win32_Perf
    Win32_PerfRawData
    Win32_PerfRawData_ASP_ActiveServerPages
    Win32_PerfRawData_ASPnet_114322_ASPnetAppsv114322
    Win32_PerfRawData_ASPnet_114322_ASPnetv114322
    Win32_PerfRawData_ASPnet_ASPnet
    Win32_PerfRawData_ASPnet_ASPnetApplications
    Win32_PerfRawData_IAS_IASAccountingClients
    Win32_PerfRawData_IAS_IASAccountingServer
    Win32_PerfRawData_IAS_IASAuthenticationClients
    Win32_PerfRawData_IAS_IASAuthenticationServer
    Win32_PerfRawData_InetInfo_InternetInformationServicesGlobal
    Win32_PerfRawData_MSDTC_DistributedTransactionCoordinator
    Win32_PerfRawData_MSFTPSVC_FTPService
    Win32_PerfRawData_MSSQLSERVER_SQLServerAccessMethods
    Win32_PerfRawData_MSSQLSERVER_SQLServerBackupDevice
    Win32_PerfRawData_MSSQLSERVER_SQLServerBufferManager
    Win32_PerfRawData_MSSQLSERVER_SQLServerBufferPartition
    Win32_PerfRawData_MSSQLSERVER_SQLServerCacheManager
    Win32_PerfRawData_MSSQLSERVER_SQLServerDatabases
    Win32_PerfRawData_MSSQLSERVER_SQLServerGeneralStatistics
    Win32_PerfRawData_MSSQLSERVER_SQLServerLatches
    Win32_PerfRawData_MSSQLSERVER_SQLServerLocks
    Win32_PerfRawData_MSSQLSERVER_SQLServerMemoryManager
    Win32_PerfRawData_MSSQLSERVER_SQLServerReplicationAgents
    Win32_PerfRawData_MSSQLSERVER_SQLServerReplicationDist
    Win32_PerfRawData_MSSQLSERVER_SQLServerReplicationLogreader
    Win32_PerfRawData_MSSQLSERVER_SQLServerReplicationMerge
    Win32_PerfRawData_MSSQLSERVER_SQLServerReplicationSnapshot
    Win32_PerfRawData_MSSQLSERVER_SQLServerSQLStatistics
    Win32_PerfRawData_MSSQLSERVER_SQLServerUserSettable
    Win32_PerfRawData_netFramework_netCLRExceptions
    Win32_PerfRawData_netFramework_netCLRInterop
    Win32_PerfRawData_netFramework_netCLRJit
    Win32_PerfRawData_netFramework_netCLRLoading
    Win32_PerfRawData_netFramework_netCLRLocksAndThreads
    Win32_PerfRawData_netFramework_netCLRMemory
    Win32_PerfRawData_netFramework_netCLRRemoting
    Win32_PerfRawData_netFramework_netCLRSecurity
    Win32_PerfRawData_Outlook_Outlook
    Win32_PerfRawData_PerfDisk_PhysicalDisk
    Win32_PerfRawData_Perfnet_Browser
    Win32_PerfRawData_Perfnet_Redirector
    Win32_PerfRawData_Perfnet_Server
    Win32_PerfRawData_Perfnet_ServerWorkQueues
    Win32_PerfRawData_PerfOS_Cache
    Win32_PerfRawData_PerfOS_Memory
    Win32_PerfRawData_PerfOS_Objects
    Win32_PerfRawData_PerfOS_PagingFile
    Win32_PerfRawData_PerfOS_Processor
    Win32_PerfRawData_PerfOS_System
    Win32_PerfRawData_PerfProc_FullImage_Costly
    Win32_PerfRawData_PerfProc_Image_Costly
    Win32_PerfRawData_PerfProc_JobObject
    Win32_PerfRawData_PerfProc_JobObjectDetails
    Win32_PerfRawData_PerfProc_Process
    Win32_PerfRawData_PerfProc_ProcessAddressSpace_Costly
    Win32_PerfRawData_PerfProc_Thread
    Win32_PerfRawData_PerfProc_ThreadDetails_Costly
    Win32_PerfRawData_RemoteAccess_RASPort
    Win32_PerfRawData_RemoteAccess_RASTotal
    Win32_PerfRawData_RSVP_ACSPerRSVPService
    Win32_PerfRawData_Spooler_PrintQueue
    Win32_PerfRawData_TapiSrv_Telephony
    Win32_PerfRawData_Tcpip_ICMP
    Win32_PerfRawData_Tcpip_IP
    Win32_PerfRawData_Tcpip_NBTConnection
    Win32_PerfRawData_Tcpip_networkInterface
    Win32_PerfRawData_Tcpip_TCP
    Win32_PerfRawData_Tcpip_UDP
    Win32_PerfRawData_W3SVC_WebService
    Win32_PhysicalMedia
    Win32_PhysicalMemory
    Win32_PhysicalMemoryArray
    Win32_PhysicalMemoryLocation
    Win32_PNPAllocatedResource
    Win32_PnPDevice
    Win32_PnPEntity
    Win32_PointingDevice
    Win32_PortableBattery
    Win32_PortConnector
    Win32_PortResource
    Win32_POTSModem
    Win32_POTSModemToSerialPort
    Win32_PowerManagementEvent
    Win32_Printer
    Win32_PrinterConfiguration
    Win32_PrinterController
    Win32_PrinterDriverDll
    Win32_PrinterSetting
    Win32_PrinterShare
    Win32_PrintJob
    Win32_PrivilegesStatus
    Win32_Process
    Win32_Processor
    Win32_ProcessStartup
    Win32_Product
    Win32_ProductCheck
    Win32_ProductResource
    Win32_ProductSoftwareFeatures
    Win32_ProgIDSpecification
    Win32_ProgramGroup
    Win32_ProgramGroupContents
    Win32_ProgramGroupOrItem
    Win32_Property
    Win32_ProtocolBinding
    Win32_PublishComponentAction
    Win32_QuickFixEngineering
    Win32_Refrigeration
    Win32_Registry
    Win32_RegistryAction
    Win32_RemoveFileAction
    Win32_RemoveIniAction
    Win32_ReserveCost
    Win32_ScheduledJob
    Win32_SCSIController
    Win32_SCSIControllerDevice
    Win32_SecurityDescriptor
    Win32_SecuritySetting
    Win32_SecuritySettingAccess
    Win32_SecuritySettingAuditing
    Win32_SecuritySettingGroup
    Win32_SecuritySettingOfLogicalFile
    Win32_SecuritySettingOfLogicalShare
    Win32_SecuritySettingOfObject
    Win32_SecuritySettingOwner
    Win32_SelfRegModuleAction
    Win32_SerialPort
    Win32_SerialPortConfiguration
    Win32_SerialPortSetting
    Win32_Service
    Win32_ServiceControl
    Win32_ServiceSpecification
    Win32_ServiceSpecificationService
    Win32_SettingCheck
    Win32_Share
    Win32_ShareToDirectory
    Win32_ShortcutAction
    Win32_ShortcutFile
    Win32_ShortcutSAP
    Win32_SID
    Win32_SMBIOSMemory
    Win32_SoftwareElement
    Win32_SoftwareElementAction
    Win32_SoftwareElementCheck
    Win32_SoftwareElementCondition
    Win32_SoftwareElementResource
    Win32_SoftwareFeature
    Win32_SoftwareFeatureAction
    Win32_SoftwareFeatureCheck
    Win32_SoftwareFeatureParent
    Win32_SoftwareFeatureSoftwareElements
    Win32_SoundDevice
    Win32_StartupCommand
    Win32_SubDirectory
    Win32_SystemAccount
    Win32_SystemBIOS
    Win32_SystemBootConfiguration
    Win32_SystemDesktop
    Win32_SystemDevices
    Win32_SystemDriver
    Win32_SystemDriverPNPEntity
    Win32_SystemEnclosure
    Win32_SystemLoadOrderGroups
    Win32_SystemLogicalMemoryConfiguration
    Win32_SystemMemoryResource
    Win32_SystemnetworkConnections
    Win32_SystemOperatingSystem
    Win32_SystemPartitions
    Win32_SystemProcesses
    Win32_SystemProgramGroups
    Win32_SystemResources
    Win32_SystemServices
    Win32_SystemSetting
    Win32_SystemSlot
    Win32_SystemSystemDriver
    Win32_SystemTimeZone
    Win32_SystemUsers
    Win32_TapeDrive
    Win32_TemperatureProbe
    Win32_Thread
    Win32_TimeZone
    Win32_Trustee
    Win32_TypeLibraryAction
    Win32_UninterruptiblePowerSupply
    Win32_USBController
    Win32_USBControllerDevice
    Win32_UserAccount
    Win32_UserDesktop
    Win32_VideoConfiguration
    Win32_VideoController
    Win32_VideoSettings
    Win32_VoltageProbe
    Win32_WMIElementSetting
    Win32_WMISetting

    首先，调用ManagementObjectSearcher实例（在本文中的例子里为searcher ）中的Get()方法，该方法将会把返回信息填在这个实例中。然后，你所要做的就是处理这个实例searcher中的数据。
    foreach (ManagementObject share in searcher.Get()){// Some Codes ...}
    每个ManagementObject的对象中都有一些，我们所需要的数据，当然我们可以接着这么处理这些数据：
    foreach (PropertyData PC in share.Properties){//some codes ...}

    常用的操作类：
    ConnectionOptions

    用于设置wmi连接远端计算机时的域名、用户名和密码等

    ManagementScope

    用于连接远端计算机。需要设置连接的wmi命名空间和ConnectionOptions

    InvokeMethodOptions

    调用wmi相关方法时的一些选项，比如调用服务的Start时的服务开启的超时等设置

    ManagementBaseObject

    包含管理对象的基本元素。它用作更具体的管理对象类的基类。

    ManagementClass

    表示公共信息模型 (CIM) 管理类。管理类是一个 WMI 类，如 Win32_LogicalDisk 和 Win32_Process

    ManagementException

    表示管理异常。

    ManagementObject

    表示wmi的具体实例

    ManagementObjectSearcher

    基于指定的查询检索管理对象的集合。此类是用于检索管理信息的较为常用的入口点之一。例如，它可以用于枚举系统中的所有磁盘驱动器、网络适配器、进程及更多管理对象，或者用于查询所有处于活动状态的网络连接以及暂停的服务等。

    ManagementPath

    提供一个包装，用于分析和生成 WMI 对象的路径。比如root/cimv2/win32_service等等。

    ManagementQuery

    提供所有管理查询对象的抽象基类。建议使用它的继承类来实现相关的查询。

    MethodData

    包含关于 WMI 方法的信息。比如MethodData 类列出有关 Win32_Process.Create 方法的信息(例子详见msdn)。

    ObjectQuery

    继承自ManagementQuery，表示返回实例或类的管理查询。

    PropertyData

    表示关于 WMI 属性的信息。比如使用 PropertyData 类列出有关 Win32_OperatingSystem 类的信息(例子详见msdn)。

    QualifierData

    包含关于 WMI 限定符的信息。比如使用 QualifierData 类列出有关 Win32_Service 类的限定符信息(例子详见msdn)。

    WqlObjectQuery

    继承自ObjectQuery。表示 WQL 格式的 WMI 数据。

    SelectQuery

    继承自WqlObjectQuery。表示 WQL SELECT 数据查询。
    */

    #endregion

}
