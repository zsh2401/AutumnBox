namespace AutumnBox.Basic.Device.ManagementV2.OS
{
    /// <summary>
    /// 电池信息
    /// </summary>
    public struct BatteryInfo
    {
        /// <summary>
        /// 最大充电电流
        /// </summary>
        public int? MaxChargingCurrent { get; set; }
        /// <summary>
        /// 最大充电电压
        /// </summary>
        public int? MaxCharingVoltage { get; set; }
        /// <summary>
        /// 是否为AC充电
        /// </summary>
        public bool? ACPowered { get; set; }
        /// <summary>
        /// 是否为USB充电
        /// </summary>
        public bool? USBPowered { get; set; }
        /// <summary>
        /// 是否为无线充电
        /// </summary>
        public bool? WirelessPowered { get; set; }
        /// <summary>
        /// 电池状态,2为充电,其它为未充电
        /// </summary>
        public int? Status { get; set; }
        /// <summary>
        /// 电池健康状态：只有数字2表示good
        /// </summary>
        public int? Health { get; set; }
        /// <summary>
        /// 电池是否安装在机身
        /// </summary>
        public bool? Present { get; set; }
        /// <summary>
        /// 电量: 百分比
        /// </summary>
        public int? Level { get; set; }
        /// <summary>
        /// 电池总容量?
        /// </summary>
        public int? Scale { get; set; }
        /// <summary>
        /// 电压
        /// </summary>
        public int? Voltage { get; set; }
        /// <summary>
        /// 电流,负数为正在充电
        /// </summary>
        public int? CurrentNow { get; set; }
        /// <summary>
        /// 电池温度,单位是0.1摄氏度
        /// </summary>
        public int? Temperature { get; set; }
        /// <summary>
        /// 电池种类/技术
        /// </summary>
        public string Technology { get; set; }
    }
}
