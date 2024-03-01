namespace Services
{
    public enum EEvent
    {
        /// <summary>
        /// 加载场景前，参数：即将加载的场景号
        /// </summary>
        BeforeLoadScene,
        /// <summary>
        /// 加载场景后（至少一帧以后），参数：刚加载好的场景号
        /// </summary>
        AfterLoadScene,
        /// <summary>
        /// 显示充电桩信息，参数：要显示的ChangingStation，位置
        /// </summary>
        ShowChargingStaionMessage,
        /// <summary>
        /// 隐藏充电桩信息
        /// </summary>
        HideChargingStaionMessage,
    }
}