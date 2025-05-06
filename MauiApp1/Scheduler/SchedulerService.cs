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
        private List<Timeframes> timeframes;
        private List<Suitability> suitabilityList;
        private List<Resources> resources;
        private List<Database.Devices> devices;
        private List<UserTimeframes> userTimeframes;

        public List<(int Iteration, int Lateness)> LatenessHistory { get; private set; } = new();

        public SchedulerService(
            List<Tasks> tasks,
            List<Users> users,
            List<Timeframes> timeframes,
            List<Suitability> suitabilityList,
            List<Resources> resources,
            List<Database.Devices> devices,
            List<UserTimeframes> userTimeframes)
        {
            this.tasks = tasks.OrderBy(t => t.Deadline).ToList();
            this.users = users;
            this.timeframes = timeframes;
            this.suitabilityList = suitabilityList;
            this.resources = resources;
            this.devices = devices;
            this.userTimeframes = userTimeframes;
        }

        public Dictionary<Users, List<Tasks>> AssignTasks(ScheduleMode mode)
        {
            return mode switch
            {
                ScheduleMode.EDD => RunEDD(),
                ScheduleMode.TabuSearch => TabuSearchSchedule(),
                _ => RunEDD()
            };
        }
        private string GetTaskOrderKey(List<Tasks> tasks)
        {
            return string.Join(",", tasks.Select(t => t.TaskID));
        }

        private List<List<Tasks>> GenerateNeighbors(List<Tasks> currentOrder)
        {
            var neighbors = new List<List<Tasks>>();
            for (int i = 0; i < currentOrder.Count - 1; i++)
            {
                var copy = new List<Tasks>(currentOrder);
                (copy[i], copy[i + 1]) = (copy[i + 1], copy[i]);
                neighbors.Add(copy);
            }
            return neighbors;
        }


        private int EvaluateSchedule(List<Tasks> taskOrder)
        {
            DateTime simulatedStart = new DateTime(2025, 04, 01);
            int currentTime = 0;
            int totalLateness = 0;

            foreach (var task in taskOrder)
            {
                currentTime += task.OperationTime;

                if (!DateTime.TryParseExact(task.Deadline, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out DateTime deadline))
                {
                    totalLateness += 100000;
                    continue;
                }

                int deadlineInMinutes = (int)((deadline - simulatedStart).TotalMinutes);

                int lateness = Math.Max(0, currentTime - deadlineInMinutes);
                totalLateness += lateness;
            }

            return totalLateness;
        }

        private Dictionary<Users, List<Tasks>> AssignTasksToUsers(List<Tasks> orderedTasks)
        {
            var schedule = new Dictionary<Users, List<Tasks>>();
            var userFreeBlocks = new Dictionary<int, List<(int start, int end)>>();

            foreach (var user in users)
            {
                var blocks = userTimeframes
                    .Where(ut => ut.UserID == user.UserID)
                    .Select(ut => timeframes.First(tf => tf.TimeframeID == ut.TimeFrameID))
                    .Select(tf => (tf.Start * 60, tf.End * 60))
                    .ToList();

                userFreeBlocks[user.UserID] = blocks;
            }

            foreach (var task in orderedTasks)
            {
                var resource = resources.First(r => r.ResourceID == task.ResourceID);
                var suitability = suitabilityList.First(s => s.SuitabilityID == task.SuitabilityID);

                var eligibleUsers = users.Where(u =>
                {
                    var device = devices.FirstOrDefault(d => d.DeviceID == u.DeviceID);
                    if (device == null || device.Device_type != suitability.Device_type)
                        return false;

                    if (!userFreeBlocks.ContainsKey(u.UserID))
                        return false;

                    return CanFitTask(userFreeBlocks[u.UserID], task.OperationTime * 60);
                }).ToList();

                if (!eligibleUsers.Any())
                    continue;

                var selectedUser = eligibleUsers.OrderBy(u =>
                    userFreeBlocks[u.UserID].Sum(b => b.end - b.start)).First();

                if (!schedule.ContainsKey(selectedUser))
                    schedule[selectedUser] = new List<Tasks>();

                schedule[selectedUser].Add(task);
                userFreeBlocks[selectedUser.UserID] =
                    UpdateBlocks(userFreeBlocks[selectedUser.UserID], task.OperationTime * 60);
            }

            return schedule;
        }

        private Dictionary<Users, List<Tasks>> TabuSearchSchedule()
        {
            var currentOrder = tasks.OrderBy(t => t.Deadline).ToList();
            var bestOrder = new List<Tasks>(currentOrder);
            int bestScore = EvaluateSchedule(bestOrder);

            LatenessHistory.Clear();
            LatenessHistory.Add((0, bestScore));

            var tabuList = new Queue<string>();
            int tabuTenure = 10;
            int maxIterations = 100;

            for (int iter = 1; iter <= maxIterations; iter++)
            {
                var neighbors = GenerateNeighbors(currentOrder);

                var bestNeighbor = neighbors
                    .Where(n => !tabuList.Contains(GetTaskOrderKey(n)))
                    .OrderBy(n => EvaluateSchedule(n))
                    .FirstOrDefault();

                if (bestNeighbor == null)
                    continue;

                currentOrder = bestNeighbor;
                int currentScore = EvaluateSchedule(currentOrder);

                LatenessHistory.Add((iter, currentScore));

                if (currentScore < bestScore)
                {
                    bestScore = currentScore;
                    bestOrder = new List<Tasks>(currentOrder);
                }

                var key = GetTaskOrderKey(currentOrder);
                tabuList.Enqueue(key);
                if (tabuList.Count > tabuTenure)
                    tabuList.Dequeue();
            }

            return AssignTasksToUsers(bestOrder);
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

                    if (resource.Ability_req < suitability.Ability_min)
                    {
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
