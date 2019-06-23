using Smod2.EventHandlers;
using Smod2.Events;
using Smod2;
using Smod2.API;

namespace SCPSL_SCP_181.Event {
    
    public class OnPlayerDieEvent : IEventHandlerPlayerDie {

        protected Plugin Plugin = null;

        public OnPlayerDieEvent(Plugin plugin) {
            this.Plugin = plugin;
        }

        public void OnPlayerDie(PlayerDeathEvent ev) {
            
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
            if (Plugin.GetConfigBool("scp181_debug") == true){
                Plugin.Info("======================SCP-181 OnDie [Debug]======================");
                Plugin.Info("SCP-181: " + ev.Player.Name + "(" + ev.Player.PlayerId + ")");
                Plugin.Info("Escape:  " + GlobalVar.scp181_escape);
                Plugin.Info("Team: " + ev.Player.TeamRole.Name);
            }
            
            // 没有SCP-181了
            GlobalVar.scp181 = null;


        }
        
    }
    
}