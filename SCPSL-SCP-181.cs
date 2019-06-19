using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Smod2;
using Smod2.Attributes;

using SCPSL_SCP_181;
using SCPSL_SCP_181.Event;
using Smod2.Config;

namespace SCPSL_SCP_181 {

    [PluginDetails(
        author = "SaigyoujiYuyuko",
        configPrefix = "scp181",
        name = "SCP-181",
        description = "Randomly D-class personnel have randomly chance to open the authorizing door without KeyCards",
        id = "org.shitsurakugensou.scp.scp-181",
        version = "1.0.0",
        SmodMajor = 3,
        SmodMinor = 1,
        SmodRevision = 20
    )]

    public class SCPSL_SCP_181 : Plugin{

        public override void OnDisable(){

            this.Info("[SCP-181][Info] SCP181 disable!");
        }

        public override void OnEnable(){

            this.Info("[SCP-181][Info] SCP181 loaded!");
        }

        public override void Register() {

            /*
             * 插件配置文件
             */

            // 是否启用插件
            this.AddConfig(new ConfigSetting("scp181_enable", true, true, "enable/disable scp181"));

            // 181开门几率
            this.AddConfig(new ConfigSetting("scp181_door_open_chance", 6, true, "How many percentage that SCP181 can open the door"));

            // 181躲避攻击几率
            this.AddConfig(new ConfigSetting("scp181_dodge_chance", 6, true, "How many percentage that SCP181 can dodge the attack"));

            /*
             * 注册事件
             */

            // 开局选取181
            this.AddEventHandlers(new OnGameStartEvent(this));

            // 玩家开门事件
            this.AddEventHandlers(new OnDoorAccessEvent(this));

        }

    }

}
