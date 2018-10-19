using libcomservice.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace libcomservice.Request.Player
{
    public struct StageInfo
    {
        public int StageID;
        public byte DificutyLv1;
        public byte DificutyLv2;
        public byte DificutyLv3;
    }

    public class StagesInfo
    {
        public List<StageInfo> StageList = new List<StageInfo>();

        public void LoadStages(Session player)
        {
            Querys.Execute_VerifyStages(player);
        }

        public void AddStage(int Id, int stageID, byte Lv1, byte Lv2, byte Lv3)
        {
            StageList.Add(
                    new StageInfo { StageID = stageID, DificutyLv1 = Lv1, DificutyLv2 = Lv2, DificutyLv3 = Lv3 }
                );
        }

        public void ChargeStages(int stageID, byte Lv1, byte Lv2, byte Lv3)
        {
            StageList.Add(
                    new StageInfo { StageID = stageID, DificutyLv1 = Lv1, DificutyLv2 = Lv2, DificutyLv3 = Lv3 }
                );
        }
    }
}
