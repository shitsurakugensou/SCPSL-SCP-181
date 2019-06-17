using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Smod2;
using Smod2.EventHandlers;
using Smod2.Events;
using Smod2.API;

namespace SCPSL_SCP_181{

    class OnGameStart : IEventHandlerRoundStart{

        // 插件对象
        private Plugin plugin = null;

        // 构造方法
        public OnGameStart(Plugin plugin) {
            this.plugin = plugin;
        }

        void IEventHandlerRoundStart.OnRoundStart(RoundStartEvent ev){

            // 玩家列表
            List<Player> playerList = this.plugin.Server.GetPlayers();

            // 玩家数量
            int playerNumber = playerList.Count;

            // 获取D级列表
            List<Player> classD = new List<Player>();

            // 筛选玩家
            for (int i=0; i < playerNumber; i++){

                // 当前选中玩家 (不需要-1因为 i开始是0 到i++时就不执行了
                Player currentPlayer = playerList[i];

                // 玩家队伍
                Team playerType = (Team)currentPlayer.TeamRole.Team;

                // DEBUG
                //this.plugin.Info(player.Name.ToString() + " | Type: " + playerType);

                // 判断玩家
                if(playerType == Team.CDP){

                    // 在D-class列表中添加此玩家
                    classD.Add(currentPlayer);
                }

            }

            /*
             * 随机选一个D-Class
             */

            // 随机数
            Random random = new Random();
            GlobalVar.scp181 = classD[random.Next(0, classD.Count - 1)];

            // 提示信息
            this.plugin.Info("[SCP-181][Info] 本局的SCP-181是: " + GlobalVar.scp181.Name.ToString() + " SteamID: " + GlobalVar.scp181.SteamId.ToString());

            return;
        }

    }
}
