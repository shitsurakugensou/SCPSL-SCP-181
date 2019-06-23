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
            
            // 设置 SCP-181 出逃状态为true
            GlobalVar.scp181_escape = true;
            
            return;
        }

    }
}
