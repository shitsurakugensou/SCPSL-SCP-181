using Smod2.EventHandlers;
using Smod2.Events;
using Smod2;
using Smod2.API;

namespace SCPSL_SCP_181.Event {
    public class OnCheckEscapeEvent : IEventHandlerCheckEscape
    {
        
        // 插件对象
        private Plugin plugin = null;
        
        // 构造
        public OnCheckEscapeEvent(Plugin plugin) {
            this.plugin = plugin;
        }
        
        public void OnCheckEscape(PlayerCheckEscapeEvent ev) {
            
            // 判断有木有SCP-181
            if (GlobalVar.scp181 == null) {
                return;
            }
            
            // 判断玩家是不是SCP-181 不是则退出本次事件
            if (ev.Player.Name.Equals(GlobalVar.scp181.Name) == false){
                return;
            }

            if (ev.Player.PlayerId != GlobalVar.scp181.PlayerId){
                return;
            }
            
            // 如果debug模式开启了
            if (plugin.GetConfigBool("scp181_debug") == true){
                plugin.Info("======================SCP-181 CheckEscape [Debug]======================");
                plugin.Info("SCP-181: " + ev.Player.Name + "(" + ev.Player.PlayerId + ")");
                plugin.Info("Team: " + ev.Player.TeamRole.Name);
            }

            
            // SCP-181 是否被NTF抓 | 否: 变成混沌 物品: 手雷 面板 E11 300发5.56子弹
            if (ev.Player.IsHandcuffed() == false) {
                ev.Player.GiveItem(ItemType.FLASHBANG);
                ev.Player.GiveItem(ItemType.WEAPON_MANAGER_TABLET);
                ev.Player.GiveItem(ItemType.E11_STANDARD_RIFLE);
                ev.Player.SetAmmo(AmmoType.DROPPED_5, 300);
                
                GlobalVar.scp181.PersonalBroadcast(8, 
                    "<color=orange>[SCP-181]</color> <color=green>作为 [混沌叛乱者] 你获得了:</color>\n<color=pink>手雷*1 | 面板*1 | E11步枪&300发子弹</color>", 
                    false);
                
                return;
            }
            
            /*
             * 作为 NTF | 获得物品: MicroHID | 角色: NTF-指挥官
             */
            
            // 设置成NTF科学家
            ev.Player.ChangeRole(Role.NTF_SCIENTIST);

            // 给个电磁炮
            ev.Player.GiveItem(ItemType.MICROHID);
            
            // 温馨提示
            GlobalVar.scp181.PersonalBroadcast(8, "<color=orange>[SCP-181]</color> <color=blue>作为 [NTF-指挥官] 你获得了:</color>\n<color=pink>MicroHID 电磁炮*1</color>", false);
            
            // SCP-181到此结束
            GlobalVar.scp181 = null;
            
            return;
        }
        
    }
}
