﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Smod2;
using Smod2.EventHandlers;
using Smod2.Events;
using Smod2.API;


namespace SCPSL_SCP_181.Event {

    class OnDoorAccessEvent : IEventHandlerDoorAccess{

        // 插件对象
        private Plugin plugin = null;

        public OnDoorAccessEvent(Plugin plugin) {
            this.plugin = plugin;
        }

        public void OnDoorAccess(PlayerDoorAccessEvent ev){
            
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

            // 如果手上有权限卡就不检测了
            if (ev.Player.GetCurrentItem().ItemType == ItemType.O5_LEVEL_KEYCARD ||
                ev.Player.GetCurrentItem().ItemType == ItemType.MTF_LIEUTENANT_KEYCARD ||
                ev.Player.GetCurrentItem().ItemType == ItemType.MTF_COMMANDER_KEYCARD ||
                ev.Player.GetCurrentItem().ItemType == ItemType.CHAOS_INSURGENCY_DEVICE ||
                ev.Player.GetCurrentItem().ItemType == ItemType.SCIENTIST_KEYCARD ||
                ev.Player.GetCurrentItem().ItemType == ItemType.GUARD_KEYCARD ||
                ev.Player.GetCurrentItem().ItemType == ItemType.JANITOR_KEYCARD ||
                ev.Player.GetCurrentItem().ItemType == ItemType.MAJOR_SCIENTIST_KEYCARD ||
                ev.Player.GetCurrentItem().ItemType == ItemType.ZONE_MANAGER_KEYCARD ||
                ev.Player.GetCurrentItem().ItemType == ItemType.SENIOR_GUARD_KEYCARD ||
                ev.Player.GetCurrentItem().ItemType == ItemType.CONTAINMENT_ENGINEER_KEYCARD){

                return;
            }
            
            // 判断是否是权限门 不是就退出
            if (ev.Door.Permission.Equals("") == true) {
                return;
            }

            /*
             * 几率判断
             */

            // 随机数~  1-100 百分比
            Random random = new Random();
            int number = random.Next(1, 100);

            // 获取设置的概率
            int luckyNumber = this.plugin.GetConfigInt("scp181_door_open_chance");
            
            // 如果debug模式开启了
            if (plugin.GetConfigBool("scp181_debug") == true){
                plugin.Info("======================SCP-181 DoorAccess [Debug]======================");
                plugin.Info("SCP-181: " + ev.Player.Name + "(" + ev.Player.PlayerId + ")");
                plugin.Info("LuckyNumber: " + luckyNumber + " | RandomNumber: " + number);
                plugin.Info("Door: " + ev.Door.Name + " | Permission: " + ev.Door.Permission + " | Opened: " + ev.Door.Open);
            }
            
            // 如果随机数<=设置的概率 就开门/关门 | 锁住的门不能开
            if (number <= luckyNumber && ev.Door.Locked == false){
                ev.Allow = true;
                GlobalVar.scp181.PersonalBroadcast(6, "<color=orange>[SCP-181]</color> <color=green>你太幸运了~ 使用了SCP-181的技能</color>", false);
                
                return;
            }

            return;
        }

    }
}
