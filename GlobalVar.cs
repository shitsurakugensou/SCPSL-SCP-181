using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Smod2;
using Smod2.EventHandlers;
using Smod2.Events;
using Smod2.API;

namespace SCPSL_SCP_181 { 

    /**
     * 全局变量类
     */
    class GlobalVar{

        // scp-181 玩家对象
        public static Player scp181 = null;
        
        // scp-181 是否重生 | after player escape
        public static bool scp181_escape = false;

    }

}
