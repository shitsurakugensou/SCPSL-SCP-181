using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Smod2;
using Smod2.EventHandlers;
using Smod2.Events;
using Smod2.API;

namespace SCPSL_SCP_181.Event{

    class OnPlayerHurtEvent : IEventHandlerPlayerHurt{

        private Plugin plugin = null;

        OnPlayerHurtEvent(Plugin plugin){
            this.plugin = plugin;
        }

        void IEventHandlerPlayerHurt.OnPlayerHurt(PlayerHurtEvent ev) {

            // 判断玩家是不是SCP-181 不是则退出本次事件
            if (ev.Player.Name.Equals(GlobalVar.scp181.Name) == false){
                return;
            }

            if (ev.Player.PlayerId != GlobalVar.scp181.PlayerId){
                return;
            }


            /*
             * 几率判断
             */

            // 随机数~  1-100 百分比
            Random random = new Random();
            int number = random.Next(1, 100);

            // 获取设置的概率
            int luckyNumber = this.plugin.GetConfigInt("scp181_dodge_chance");

            // 如果随机数<=设置的概率 就免伤害
            if (number <= luckyNumber) {
                GlobalVar.scp181.PersonalBroadcast(8, "<color=orange>[SCP-181]</color> <color=green>SCP-181免除本次伤害~</color>", false);
                ev.Damage = 0;
            }
            
            return;
        }

    }

}
