namespace Services.Event
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
        /// 显示充电桩消息窗口，参数：要显示的ChangingStation，位置
        /// </summary>
        ShowChargingStaionMessageWindow,
        /// <summary>
        /// 隐藏充电桩消息窗口
        /// </summary>
        HideChargingStaionMessageWindow,
        /// <summary>
        /// 打开充电桩面板，参数：要显示的ChangingStation
        /// </summary>
        OpenChargingStationPanel,
        /// <summary>
        /// 整体更新所有数据
        /// </summary>
        UpdateData,
        /// <summary>
        /// 用户界面刷新
        /// </summary>
        Refresh,
    }
}