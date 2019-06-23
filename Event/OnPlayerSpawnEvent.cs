using Smod2.EventHandlers;
using Smod2.Events;
using Smod2;
using Smod2.API;


namespace SCPSL_SCP_181.Event {
    
    
    public class OnPlayerSpawnEvent : IEventHandlerSpawn{
        
        // 插件对象
        protected Plugin Plugin = null;

        public OnPlayerSpawnEvent(Plugin plugin) {
            this.Plugin = plugin;
        }

        public void OnSpawn(PlayerSpawnEvent ev) {
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

            if (GlobalVar.scp181_escape == false) {
                return;
            }
            
            // 如果debug模式开启了
            if (Plugin.GetConfigBool("scp181_debug") == true){
                Plugin.Info("======================SCP-181 OnSpawn [Debug]======================");
                Plugin.Info("SCP-181: " + ev.Player.Name + "(" + ev.Player.PlayerId + ")");
                Plugin.Info("Escape:  " + GlobalVar.scp181_escape);
                Plugin.Info("Team: " + ev.Player.TeamRole.Name);
            }
            
            
            // 如果加入了 NTF
            if (ev.Player.TeamRole.Team == Smod2.API.Team.NINETAILFOX) {
                
                // 设置成NTF科学家
                ev.Player.ChangeRole(Role.NTF_SCIENTIST);

                // 给个电磁炮
                ev.Player.GiveItem(ItemType.MICROHID);
            
                // 温馨提示
                GlobalVar.scp181.PersonalBroadcast(
                    8, 
                    "<color=orange>[SCP-181]</color> <color=blue>作为 [NTF-氪学家] 你获得了:</color>\n<color=pink>MicroHID 电磁炮*1</color>", 
                    false
                );

                GlobalVar.scp181 = null;
                return;
            }
            
            
            // 如果加入了 CI
            if (ev.Player.TeamRole.Team == Smod2.API.Team.CHAOS_INSURGENCY) {
                
                ev.Player.GiveItem(ItemType.FLASHBANG);
                ev.Player.GiveItem(ItemType.WEAPON_MANAGER_TABLET);
                ev.Player.GiveItem(ItemType.E11_STANDARD_RIFLE);
                ev.Player.SetAmmo(AmmoType.DROPPED_5, 300);
                
                ev.Player.PersonalBroadcast(
                    8, 
                    "<color=orange>[SCP-181]</color> <color=green>作为 [馄饨叛乱者] 你获得了:</color>\n<color=pink>手雷*1 | 面板*1 | E11步枪&300发子弹</color>", 
                    false
                );
                
                GlobalVar.scp181 = null;
                return;
            }
            
        }

    }
    
}