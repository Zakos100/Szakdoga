using System;
using SQLite;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiApp1.Database;

namespace MauiApp1.Scheduler
{
    public class SchedulerService
    {
        private List<Tasks> tasks;
        private List<Users> users;
        private List<Timeframe> timeframes;
        private List<Suitability> suitabilityList;
        private List<Resources> resources;
        private List<Database.Device> devices;
        private List<UserTimeframes> userTimeframes;

        public SchedulerService(
            List<Tasks> tasks,
            List<Users> users,
            List<Timeframe> timeframes,
            List<Suitability> suitabilityList,
            List<Resources> resources,
            List<Database.Device> devices,
            List<UserTimeframes> userTimeframes)
        {
            this.tasks = tasks.OrderBy(t => t.Deadline).ToList(); // EDD sorrend
            this.users = users;
            this.timeframes = timeframes;
            this.suitabilityList = suitabilityList;
            this.resources = resources;
            this.devices = devices;
            this.userTimeframes = userTimeframes;
        }

        public Dictionary<Users, List<Tasks>> AssignTasks()
        {
            var schedule = new Dictionary<Users, List<Tasks>>();
            var availableTime = userTimeframes.GroupBy(ut => ut.UserID).ToDictionary(g => g.Key,g => g.Sum(ut =>
        {
            var tf = timeframes.FirstOrDefault(t => t.TimeframeID == ut.TimeFrameID);
            return tf?.Duration ?? 0;
        }) ); // UserID -> szabad idő órában

            foreach (var user in users)
            {
                var tf = timeframes.FirstOrDefault(t => t.TimeframeID == user.UserID); // feltételezve hogy userID == timeframeID
                if (tf != null)
                    availableTime[user.UserID] = tf.Duration;
            }

            foreach (var task in tasks)
            {
                var resource = resources.First(r => r.ResourceID == task.ResourceID);
                var suitability = suitabilityList.First(s => s.SuitabilityID == task.SuitabilityID);
                var device = devices.First(d => d.DeviceID == task.DeviceID);

                var eligibleUsers = users.Where(u =>
                {
                    var userDevice = devices.FirstOrDefault(d => d.DeviceID == u.DeviceID);
                    if (userDevice == null) return false;
                    if (userDevice.Device_type != suitability.Device_type) return false;
                    if (resource.Ability_reg < suitability.Ability_min) return false;
                    if (!availableTime.ContainsKey(u.UserID)) return false;
                    return availableTime[u.UserID] >= task.OperationTime; // még mindig kisebb az availableTime mint a task.OperationTime
                }).ToList();

                if (eligibleUsers.Count == 0) continue; // nem tudtuk kiosztani

                var selectedUser = eligibleUsers.OrderBy(u => availableTime[u.UserID]).First();
                if (!schedule.ContainsKey(selectedUser))
                    schedule[selectedUser] = new List<Tasks>();

                schedule[selectedUser].Add(task);
                availableTime[selectedUser.UserID] -= task.OperationTime;
            }

            return schedule;
        }
    }

}
