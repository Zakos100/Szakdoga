using System;
using SQLite;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiApp1.Database;
using System.Diagnostics;

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

        public Dictionary<Users, List<Tasks>> AssignTasks(ScheduleMode mode)
        {
            if (mode == ScheduleMode.FlowShop)
            {
                return FlowShopSchedule();
            }

            return RunEDD();
        }

        private Dictionary<Users, List<Tasks>> FlowShopSchedule()
        {
            var schedule = new Dictionary<Users, List<Tasks>>();

            var operationsPerTask = tasks.Select(t => new
            {
                Task = t,
                Op1 = new Operation { TaskID = t.TaskID, MachineIndex = 1, ProcessingTime = t.OperationTime / 2 },
                Op2 = new Operation { TaskID = t.TaskID, MachineIndex = 2, ProcessingTime = t.OperationTime / 2 }
            }).ToList();

            var orderedTasks = operationsPerTask
                .OrderBy(t => Math.Min(t.Op1.ProcessingTime, t.Op2.ProcessingTime))
                .ThenBy(t => t.Op1.ProcessingTime < t.Op2.ProcessingTime ? 0 : 1)
                .Select(t => t.Task)
                .ToList();

            foreach (var task in orderedTasks)
            {
                var resource = resources.FirstOrDefault(r => r.ResourceID == task.ResourceID);
                var suitability = suitabilityList.FirstOrDefault(s => s.SuitabilityID == task.SuitabilityID);

                if (resource == null || suitability == null)
                    continue;

                var eligibleUsers = users.Where(u =>
                {
                    var device = devices.FirstOrDefault(d => d.DeviceID == u.DeviceID);
                    if (device == null || device.Device_type != suitability.Device_type)
                        return false;

                    if (resource.Ability_reg < suitability.Ability_min)
                        return false;

                    var userTfs = userTimeframes
                        .Where(ut => ut.UserID == u.UserID)
                        .Select(ut => timeframes.FirstOrDefault(tf => tf.TimeframeID == ut.TimeFrameID))
                        .Where(tf => tf != null)
                        .ToList();

                    var totalMinutesAvailable = userTfs.Sum(tf =>
                    {
                        var start = tf.Start;
                        var end = tf.End;
                        if (end < start)
                            return (1440 - start) + end;
                        return end - start;
                    });

                    return totalMinutesAvailable >= task.OperationTime;
                }).ToList();

                if (!eligibleUsers.Any())
                    continue;

                var selectedUser = eligibleUsers.OrderBy(u =>
                {
                    var userTfs = userTimeframes
                        .Where(ut => ut.UserID == u.UserID)
                        .Select(ut => timeframes.FirstOrDefault(tf => tf.TimeframeID == ut.TimeFrameID))
                        .Where(tf => tf != null)
                        .ToList();

                    return userTfs.Sum(tf =>
                    {
                        var start = tf.Start;
                        var end = tf.End;
                        if (end < start)
                            return (1440 - start) + end;
                        return end - start;
                    });
                }).First();

                if (!schedule.ContainsKey(selectedUser))
                    schedule[selectedUser] = new List<Tasks>();

                schedule[selectedUser].Add(task);
            }

            return schedule;
        }

        private Dictionary<Users, List<Tasks>> RunEDD()
        {
            var schedule = new Dictionary<Users, List<Tasks>>();
            var userFreeBlocks = new Dictionary<int, List<(int start, int end)>>();

            foreach (var user in users)
            {
                var userTfs = userTimeframes
                    .Where(ut => ut.UserID == user.UserID)
                    .Select(ut => timeframes.First(tf => tf.TimeframeID == ut.TimeFrameID));

                var blocks = new List<(int start, int end)>();

                foreach (var tf in userTfs)
                {
                    int tfStart = tf.Start;
                    int tfEnd = tf.End;

                    if (tfEnd >= tfStart)
                        blocks.Add((tfStart * 60, tfEnd * 60));
                    else
                    {
                        blocks.Add((tfStart * 60, 1440));
                        blocks.Add((0, tfEnd * 60));
                    }
                }

                userFreeBlocks[user.UserID] = blocks.OrderBy(b => b.start).ToList();
            }

            var sortedTasks = tasks.OrderBy(t => t.Deadline).ToList();

            foreach (var task in sortedTasks)
            {
                var resource = resources.First(r => r.ResourceID == task.ResourceID);
                var suitability = suitabilityList.First(s => s.SuitabilityID == task.SuitabilityID);

                var eligibleUsers = users.Where(u =>
                {
                    if (!userFreeBlocks.ContainsKey(u.UserID))
                        return false;

                    var device = devices.FirstOrDefault(d => d.DeviceID == u.DeviceID);
                    if (device == null || device.Device_type != suitability.Device_type)
                        return false;

                    if (resource.Ability_reg < suitability.Ability_min)
                    {
                        Debug.WriteLine($"KIZÁRVA - TaskID {task.TaskID}: {resource.Ability_reg} < {suitability.Ability_min}");
                        return false;
                    }

                    return CanFitTask(userFreeBlocks[u.UserID], task.OperationTime * 60);
                }).ToList();

                if (eligibleUsers.Count == 0)
                    continue;

                var selectedUser = eligibleUsers.OrderBy(u =>
                    userFreeBlocks[u.UserID].Sum(b => b.end - b.start)).First();

                if (!schedule.ContainsKey(selectedUser))
                    schedule[selectedUser] = new List<Tasks>();

                schedule[selectedUser].Add(task);
                userFreeBlocks[selectedUser.UserID] = UpdateBlocks(userFreeBlocks[selectedUser.UserID], task.OperationTime * 60);
            }

            return schedule;
        }

        private bool CanFitTask(List<(int start, int end)> blocks, int duration)
        {
            int totalAvailable = blocks.Sum(b => b.end - b.start);
            return totalAvailable >= duration;
        }

        private List<(int start, int end)> UpdateBlocks(List<(int start, int end)> blocks, int duration)
        {
            var newBlocks = new List<(int start, int end)>();

            foreach (var block in blocks)
            {
                int blockDuration = block.end - block.start;

                if (duration <= 0)
                {
                    newBlocks.Add(block);
                    continue;
                }

                if (blockDuration <= 0)
                    continue;

                if (blockDuration <= duration)
                {
                    duration -= blockDuration;
                }
                else
                {
                    newBlocks.Add((block.start + duration, block.end));
                    duration = 0;
                }
            }

            return newBlocks;
        }
    }
}
