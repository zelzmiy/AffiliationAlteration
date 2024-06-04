using System;
using System.Collections.Generic;
using System.Text;

namespace ForceFriendship.Helpers
{
    static class Extentions
    {
        public static bool IsSleeping(this Follower follower)
        {
            return !(TimeManager.IsNight &&
                follower != null &&
                follower.Brain.CurrentTask != null &&
                follower.Brain.CurrentTask.State == FollowerTaskState.Doing &&
                (follower.Brain.CurrentTaskType == FollowerTaskType.Sleep || follower.Brain.CurrentTaskType == FollowerTaskType.SleepBedRest));
        }
    }
}
